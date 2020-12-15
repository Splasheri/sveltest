using BaseLibrary;
using System;
using System.Text.RegularExpressions;

namespace ServerProject
{
    class ConsoleInputController : InputControllerBase<string>
    {
        protected new readonly string START_TEXT = "Открыта работа со словарём. Доступны команды create-dict(create) update-dict(update) delete-dict(delete)";
        private const string GET_CONSOLE_COMMAND = @"\b(\w+)\b(?:\s+(\S*))?\z";
        DictionaryController dictionaryController;
        public ConsoleInputController(OutputControllerBase _consoleOutput, DictionaryController _dictionaryController) : base(_consoleOutput)
        {
            dictionaryController = _dictionaryController;
        }
        protected override void StartConsole()
        {
            consoleOutput.PrintLine(START_TEXT);
        }
        protected override void AnalyzeInput(string inputInfo)
        {
            Regex regex = new Regex(GET_CONSOLE_COMMAND);
            string command = regex.Match(inputInfo).Groups[1].Value.ToLower();
            string path = string.Empty;
            switch (command)
            {
                case "create-dict":
                case "create":
                    path = regex.Match(inputInfo).Groups[2].Value.ToLower();
                    dictionaryController.CreateDictionary(path);
                    break;
                case "update-dict":
                case "update":
                    path = regex.Match(inputInfo).Groups[2].Value.ToLower();
                    dictionaryController.UpdateDictionary(path);
                    break;
                case "delete-dict":
                case "delete":
                    dictionaryController.DeleteDictionary();
                    break;
                case "exit": 
                    Environment.Exit(0);
                    break;
            }
        }

        protected override void AskForHints()
        {
            throw new System.NotImplementedException();
        }

        protected override string GetDataForAnalyz()
        {
            return Console.ReadLine();
        }
    }
}
