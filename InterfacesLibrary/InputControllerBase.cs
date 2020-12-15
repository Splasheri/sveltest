using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace BaseLibrary
{
    public abstract class InputControllerBase<T>
    {
        public const int MAX_HINTS = 5;
        protected readonly string START_TEXT = "Введите слово для получения подсказок из словаря: ";
        public const string EOF = "<EOF>";
        public const string UNIQUE_CHARS = "<??>";
        protected string inputWord;
        protected bool inputMarker;
        protected readonly OutputControllerBase consoleOutput;
        public InputControllerBase(OutputControllerBase _consoleOutput)
        {
            inputWord = "";
            inputMarker = true;
            consoleOutput = _consoleOutput;
        }
        protected virtual void StartConsole()
        {
            consoleOutput.PrintLine(this.START_TEXT);
        }
        public void WaitForInput()
        {
            StartConsole();
            while (inputMarker)
            {
                try
                {
                    AnalyzeInput(GetDataForAnalyz());
                }
                catch (Exception e)
                {
                    inputMarker = false;
                    consoleOutput.PrintError(e);
                }
            }
        }
        protected abstract T GetDataForAnalyz();
        protected abstract void AnalyzeInput(T inputInfo);
        protected void SwitchKeyValue(ConsoleKeyInfo inputInfo)
        {
            switch (inputInfo.Key)
            {
                case ConsoleKey.Escape:
                    CloseConsole();
                    break;
                case ConsoleKey.Enter:
                    consoleOutput.PrintLine();
                    AskForHints();
                    break;
                case ConsoleKey.Tab:
                    break;
                case ConsoleKey.Backspace:
                    if (inputWord.Length >= 1)
                    {
                        int cursorCol = Console.CursorLeft - 1;
                        inputWord = inputWord.Substring(0, inputWord.Length - 1);
                        Console.CursorLeft = 0;
                        consoleOutput.Print(inputWord + ' ');
                        Console.CursorLeft = cursorCol;
                    }
                    break;
                default:
                    consoleOutput.Print(inputInfo.KeyChar.ToString());
                    inputWord += inputInfo.KeyChar;
                    break;
            }
        }
        protected abstract void AskForHints();
        public virtual void CloseConsole()
        {
            inputMarker = false;
        }
    }
}
;