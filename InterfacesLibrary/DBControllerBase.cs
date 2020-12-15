using System.Collections.Generic;
using System.Linq;
using WordsDB;

namespace BaseLibrary
{
    public abstract class DBControllerBase
    {     
        protected  Database db;
        protected readonly OutputControllerBase outputController;
        public DBControllerBase(OutputControllerBase _outputController, string dbPath = "TestDatabase.db")
        {
            outputController = _outputController;
            ConnectToDB(dbPath);
        }
        public  void ConnectToDB(string dbPath)
        {
           db = new Database(dbPath);
        }
        protected void UploadModel(UsingWordModel model)
        {
            db.InsertOrUpdateWord(model.Word, model.RepetionRate);
        }
        public void SetWordArray(List<UsingWordModel> models)
        {
            foreach (var model in models)
            {
                UploadModel(model);
            }
        }
        public  List<UsingWordModel> GetAllWords()
        {
            return (from dbWord in db.GetAllWords()
                    select new UsingWordModel(dbWord.Word, dbWord.RepetionRate)
                   ).ToList();

        }        
        public List<string> GetWordsByPart(string wordPart, int count)
        {
            return (from dbWord in db.GetSameWords(wordPart, count)
                    select dbWord.Word 
                   ).ToList();
        }
        public  void ClearDictionary()
        {
            db.DeleteAllWords();
        }
    }
}
