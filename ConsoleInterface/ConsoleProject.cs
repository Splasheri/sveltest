using BaseLibrary;
using System;

namespace ConsoleProject
{
    class ConsoleProject
    {
        private static InputController consoleInput;
        private static OutputControllerBase consoleOutput;
        private static DBController dbController;
        private static DictionaryController dictionaryController;
        static void Main(string[] args)
        {
            StartProject(args);
            consoleInput.WaitForInput();
        }
        static void CreateControllers()
        {
            consoleOutput = new OutputControllerBase();
            dbController = new DBController(consoleOutput);
            dictionaryController = new DictionaryController(dbController,consoleOutput);
            consoleInput = new InputController(consoleOutput,dbController);
        }
        static void StartProject(string[] args)
        {
            CreateControllers();
            CheckParameters(args);
        }
        static void CheckParameters(string[] args)
        {
            try
            {
                AnalyzeParameters(args);
            }
            catch (Exception e)
            {
                consoleOutput.PrintError(e);
            }           
        }
        static void AnalyzeParameters(string[] args)
        {
            switch (args[0])
            {
                case "create-dict":
                case "create":
                    CreateControllers();
                    dictionaryController.CreateDictionary(args[1]);
                    break;
                case "update-dict":
                case "update":
                    CreateControllers();
                    dictionaryController.UpdateDictionary(args[1]);
                    break;
                case "delete-dict":
                case "delete":
                    CreateControllers();
                    dictionaryController.DeleteDictionary();
                    break;
            }
        }
    }
}
