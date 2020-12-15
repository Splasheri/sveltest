using BaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace ServerProject
{
    internal class ServerInputController
    {
        protected const string START_TEXT = "Подключён новый сокет";
        protected readonly string GET_INPUT_REGEX = @"get\s\b(\w+)\b\s" + $"{InputControllerBase<object>.EOF}";
        protected readonly Socket listener;
        protected readonly DBController dbController;
        public ServerInputController(DBController _dbController, Socket _handler)
        {
            listener = _handler;
            dbController = _dbController;
        }
        protected void WaitForMessage(object handlerObject)
        {                
            Socket handler = (Socket)handlerObject;
            Regex regex = new Regex(GET_INPUT_REGEX);
            byte[] bytes = new Byte[1024];
            string data = null;
            while (true)
            {
                int bytesRec = handler.Receive(bytes);
                data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                if (data.IndexOf("<EOF>") > -1)
                {
                    break;
                }
            }
            data = regex.Match(data).Groups[1].Value;
            handler.Send(GetHintsByWord(data));
            handler.Close();
        }
        protected byte[] GetHintsByWord(string inputStr)
        {
            return Encoding.UTF8.GetBytes(dbController.GetWordsByPart(inputStr, InputControllerBase<object>.MAX_HINTS, InputControllerBase<object>.UNIQUE_CHARS));            
        }
        public void WaitingForConnection()
        {
            while (true)
            {
                Socket handler = listener.Accept();
                Thread thread = new Thread(new ParameterizedThreadStart(WaitForMessage));
                thread.Start(handler);
            }
        }
    }
}
