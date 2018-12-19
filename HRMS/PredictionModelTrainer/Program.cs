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
                RoomRatesModelHelper.CreateModelPipeline(context);
                RoomRatesModelHelper.Evaluate(context);
            }
            catch (Exception ex)
            {
                ConsoleWriteException(ex.Message);
            }
            ConsolePressAnyKey();
        }
    }
}
