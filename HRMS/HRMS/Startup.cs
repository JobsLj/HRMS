using HRMS.Data;
using HRMS.Prediction;
using HRMS.Prediction.DataStructures;
using HRMS.Prediction.DataStructures.RoomRates;
using HRMS.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ML;
using Microsoft.ML.Runtime.Data;

namespace HRMS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<ISeederRepository, SeederRepository>();

            //MLContext created as singleton for the whole ASP.NET Core app
            services.AddSingleton<MLContext, MLContext>((ctx) =>
            {
                //Seed set to any number so you have a deterministic environment
                return new MLContext(seed: 1);
            });

            // Prediction models created as singleton for the whole ASP.NET Core app
            // since it is threadsafe and models can be pretty large objects
            services.AddSingleton<StdRoomRateModel>();

            // PredictionFunction created as scoped because it is not thread-safe
            // Prediction Functions should be be re-used across calls because there are expensive initializations
            // If set to be used as Singleton is very important to use critical sections "lock(predFunct" in the code
            // because the 'Predict()' method is not reentrant. 
            services.AddScoped<PredictionFunction<RoomRateData, RoomRatePrediction>>((ctx) =>
            {
                //Create the Prediction Function object from its related model
                var model = ctx.GetRequiredService<StdRoomRateModel>();
                return model.CreatePredictionFunction();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationContext>();
                context.Database.Migrate();
                context.Database.EnsureCreated();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
