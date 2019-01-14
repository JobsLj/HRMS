#pragma checksum "C:\Users\Siew Mun\Desktop\FYP-APP\HRMS\HRMS\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a16f86c007ae24663900d27207114c1b89a00e67"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
namespace AspNetCore
{
    #line hidden
    using System;
#line 1 "C:\Users\Siew Mun\Desktop\FYP-APP\HRMS\HRMS\Views\Home\Index.cshtml"
using System.Collections.Generic;

#line default
#line hidden
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\Siew Mun\Desktop\FYP-APP\HRMS\HRMS\Views\_ViewImports.cshtml"
using HRMS;

#line default
#line hidden
#line 2 "C:\Users\Siew Mun\Desktop\FYP-APP\HRMS\HRMS\Views\_ViewImports.cshtml"
using HRMS.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a16f86c007ae24663900d27207114c1b89a00e67", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ce1d7316414513ea68c01560b78a295bf5398467", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<CalendarEvent>>
    {
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::HRMS.TagHelpers.CalendarTagHelper __HRMS_TagHelpers_CalendarTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(63, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 4 "C:\Users\Siew Mun\Desktop\FYP-APP\HRMS\HRMS\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Dashboard";
    List<CalendarEvent> events = new List<CalendarEvent>();
    foreach (var item in Model)
    {
        events.Add(new CalendarEvent { Adr = item.Adr, Date = item.Date, Occupancy = item.Occupancy, RevPar = item.RevPar, Type = item.Type, Percent = item.Percent });
    }
    var currentMonth = DateTime.Today.Month;
    var currentYear = DateTime.Today.Year;

#line default
#line hidden
            BeginContext(477, 184, true);
            WriteLiteral("\r\n<!-- pageheader -->\r\n<div class=\"row\">\r\n    <div class=\"col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12\">\r\n        <div class=\"page-header\">\r\n            <h2 class=\"pageheader-title\">");
            EndContext();
            BeginContext(662, 17, false);
#line 19 "C:\Users\Siew Mun\Desktop\FYP-APP\HRMS\HRMS\Views\Home\Index.cshtml"
                                    Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(679, 288, true);
            WriteLiteral(@" </h2>
            <div class=""page-breadcrumb"">
            </div>
        </div>
    </div>
</div>
<!-- Body -->
<div class=""row"">
    <div class=""col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12"">
        <div class=""card"">
            <div class=""card-body"">
                ");
            EndContext();
            BeginContext(967, 71, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("calendar", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "97e133b961fe470483e8dd6c21f208bd", async() => {
            }
            );
            __HRMS_TagHelpers_CalendarTagHelper = CreateTagHelper<global::HRMS.TagHelpers.CalendarTagHelper>();
            __tagHelperExecutionContext.Add(__HRMS_TagHelpers_CalendarTagHelper);
#line 30 "C:\Users\Siew Mun\Desktop\FYP-APP\HRMS\HRMS\Views\Home\Index.cshtml"
__HRMS_TagHelpers_CalendarTagHelper.Month = currentMonth;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("month", __HRMS_TagHelpers_CalendarTagHelper.Month, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 30 "C:\Users\Siew Mun\Desktop\FYP-APP\HRMS\HRMS\Views\Home\Index.cshtml"
__HRMS_TagHelpers_CalendarTagHelper.Year = currentYear;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("year", __HRMS_TagHelpers_CalendarTagHelper.Year, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 30 "C:\Users\Siew Mun\Desktop\FYP-APP\HRMS\HRMS\Views\Home\Index.cshtml"
__HRMS_TagHelpers_CalendarTagHelper.Events = events;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("events", __HRMS_TagHelpers_CalendarTagHelper.Events, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1038, 56, true);
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<CalendarEvent>> Html { get; private set; }
    }
}
#pragma warning restore 1591
