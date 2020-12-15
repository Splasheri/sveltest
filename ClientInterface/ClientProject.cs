using System;
using System.Collections.Generic;
using BaseLibrary;

namespace ClientProject
{
    class ClientProject
    {
        static TCPClientBase tcpClient;
        static ClientInputController inputController;
        static OutputControllerBase outputController;
        static void Main(string[] args)
        {
            StartProject(args);
            inputController.WaitForInput();
        }
        static void StartProject(string[] args)
        {
            outputController = new OutputControllerBase();
            CheckParameters(args);
            inputController = new ClientInputController(outputController,tcpClient);
        }
        static void CheckParameters(string[] args)
        {
            try
            {
                AnalyzeParameters(args);
            }
            catch (Exception e)
            {
                outputController.PrintError(e);
            }
        }
    }
}
