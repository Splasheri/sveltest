using BaseLibrary;

namespace ServerProject
{
    internal class DBController : DBControllerBase
    {
        public DBController(OutputControllerBase _outputController, string dbPath = "TestDatabase.db") : base(_outputController, dbPath)
        {
        }
        public string GetWordsByPart(string wordPart, int count, string uniqueChars)
        {
            return string.Join(uniqueChars, base.GetWordsByPart(wordPart, count));
        }
    }
}
