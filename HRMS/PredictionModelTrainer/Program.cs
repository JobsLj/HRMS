using System;
using Microsoft.ML;
using static PredictionModelTrainer.ConsoleView;

namespace PredictionModelTrainer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Training model...");
            try
            {
                MLContext context = new MLContext(seed: 1);

                // SPR 
                SprRoomRatesModelHelper.CreateModelPipeline(context);
                SprRoomRatesModelHelper.Evaluate(context);

                // STD
                StdRoomRatesModelHelper.CreateModelPipeline(context);
                StdRoomRatesModelHelper.Evaluate(context);

                // FAM
                FamRoomRatesModelHelper.CreateModelPipeline(context);
                FamRoomRatesModelHelper.Evaluate(context);

                // SUITE
                SuiteRoomRatesModelHelper.CreateModelPipeline(context);
                SuiteRoomRatesModelHelper.Evaluate(context);

                // DLX
                DlxRoomRatesModelHelper.CreateModelPipeline(context);
                DlxRoomRatesModelHelper.Evaluate(context);

                OccupancyModelHelper.CreateModelPipeline(context);
                OccupancyModelHelper.Evaluate(context);
            }
            catch (Exception ex)
            {
                ConsoleWriteException(ex.Message);
            }
            ConsolePressAnyKey();
        }
    }
}
