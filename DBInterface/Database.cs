using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace WordsDB
{
    public class Database
    {
        private const int WORD_COUNT_MIN = 3;
        public Database(string dbPath)
        {
            TestDatabaseContext.DatabasePath = dbPath;
        }
        private bool CheckCount(long repCount)
        {
            return repCount >= WORD_COUNT_MIN;
        }
        public void InsertWord(string word, long repCount)
        {
            if (CheckCount(repCount))
            {
                word = word.ToLower();
                using (TestDatabaseContext db = new TestDatabaseContext())
                {
                    db.UsingWords.Add(new UsingWord() { Word = word, RepetionRate = repCount });
                    db.SaveChanges();
                }
            }
            else
            {
                return;
            }
        }
        public void InsertOrUpdateWord(string word, long repCount)
        {
            word = word.ToLower();
            using (TestDatabaseContext db = new TestDatabaseContext())
            {
                if (!db.UsingWords.Where(uw => uw.Word == word).Any())
                {
                    InsertWord(word, repCount);
                }
                else
                {
                    var existedWord = db.UsingWords.Where(uw => uw.Word == word).First();
                    existedWord.RepetionRate += repCount;
                    db.Update(existedWord);
                }
                db.SaveChanges();
            }
        }
        public void DeleteAllWords()
        {
            using (TestDatabaseContext db = new TestDatabaseContext())
            {
                db.UsingWords.RemoveRange(db.UsingWords);
                db.SaveChanges();
            }
        }
        public List<UsingWord> GetAllWords()
        {
            using (TestDatabaseContext db = new TestDatabaseContext())
            {
                return db.UsingWords.AsNoTracking().OrderBy(uw => uw.Word.Length).ToList();
            }
        }
        public List<UsingWord> GetSameWords(string wordPart, int count = 0)
        {
            using (TestDatabaseContext db = new TestDatabaseContext())
            {
                return db.UsingWords.Where(uw => uw.Word.StartsWith(wordPart))
                                    .OrderByDescending(uw => uw.RepetionRate)
                                    .ThenBy(uw => uw.Word)
                                    .Take(count <= 0 ? db.UsingWords.Count() : count)
                                    .ToList();
            }
        }
    }
}
