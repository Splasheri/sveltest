using BaseLibrary;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerProject
{
    class ServerProject
    {
        private static TCPServerBase serverSocket;
        private static OutputControllerBase outputController;
        private static ConsoleInputController inputController;
        private static ServerInputController serverInputController;
        private static DictionaryController dictionaryController;
        private static DBController dbController;
        private static Thread waitingThread;
        static void Main(string[] args)
        {
            StartProject(args);
            try
            {
                waitingThread = new Thread(new ThreadStart(serverInputController.WaitingForConnection));
                waitingThread.Start();
                inputController.WaitForInput();
            }
            catch (Exception e)
            {
                outputController.PrintError(e);
                Environment.Exit(0);
            }
            finally
            {
                serverSocket.listener.Shutdown(SocketShutdown.Both);
                serverSocket.listener.Close();
            }
        }
        static void StartProject(string[] args)
        {
            outputController = new OutputControllerBase();
            dbController = new DBController(outputController,args[0]);
            serverSocket = new TCPServerBase(int.Parse(args[1]));
            dictionaryController = new DictionaryController(dbController, outputController);
            inputController = new ConsoleInputController(outputController,dictionaryController);
            serverInputController = new ServerInputController(dbController,serverSocket.listener);
        }
    }
}

