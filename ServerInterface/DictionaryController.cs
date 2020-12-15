using BaseLibrary;


namespace ServerProject
{
     internal class DictionaryController : DictionaryControllerBase
    {
        public DictionaryController(DBControllerBase _dBController, OutputControllerBase _outputController) : base(_dBController,_outputController)
        {
        }
    }
}
