using BaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ClientProject
{
    internal class ClientInputController : InputControllerBase<ConsoleKeyInfo>
    {
        protected readonly TCPClientBase tcpClient;  
        public ClientInputController(OutputControllerBase _consoleOutput, TCPClientBase _client) : base(_consoleOutput)
        {
            tcpClient = _client;
        }
        protected override void AnalyzeInput(ConsoleKeyInfo inputInfo)
        {
            SwitchKeyValue(inputInfo);
        }
        protected override void AskForHints()
        {
            byte[] msg = Encoding.UTF8.GetBytes($"get {inputWord} {EOF}");
            tcpClient.socket.Send(msg);
            consoleOutput.PrintList(WaitForHints());
            inputWord = "";
        }
        protected List<string> WaitForHints()
        {
            byte[] bytes = new byte[1024];
            tcpClient.socket.Receive(bytes);
            string bytesToText = Encoding.UTF8.GetString(bytes).TrimEnd('\0');
            tcpClient.socket.Shutdown(SocketShutdown.Both);
            tcpClient.socket.Close();
            return bytesToText.Split(UNIQUE_CHARS, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        protected override ConsoleKeyInfo GetDataForAnalyz()
        {
            return Console.ReadKey(true);
        }
        public override void CloseConsole()
        {
            inputMarker = false;
        }
    }
}
