using System;
using BaseLibrary;

namespace ConsoleProject
{
    internal class InputController : InputControllerBase<ConsoleKeyInfo>
    {
        protected readonly DBControllerBase dbController;
        public InputController(OutputControllerBase _consoleOutput, DBController _dBController) : base(_consoleOutput)
        {
            dbController = _dBController;
        }
        protected override void AnalyzeInput(ConsoleKeyInfo inputInfo)
        {
            SwitchKeyValue(inputInfo);
        }

        protected override void AskForHints()
        {
            if (String.IsNullOrWhiteSpace(inputWord))
            {
                CloseConsole();
            }
            else
            {
                consoleOutput.PrintList(dbController.GetWordsByPart(inputWord, MAX_HINTS));
                inputWord = "";
            }
        }

        protected override ConsoleKeyInfo GetDataForAnalyz()
        {
            return Console.ReadKey(true);
        }
    }
}
;