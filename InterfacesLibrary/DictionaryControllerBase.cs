using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace BaseLibrary
{
     public abstract class DictionaryControllerBase
    {
        private const int WORD_SIZE_MIN = 3;
        private const int WORD_SIZE_MAX = 15;
        private readonly string SEARCH_WORD_REG = @"\b(\w{" + $"{WORD_SIZE_MIN},{WORD_SIZE_MAX}" + @"})\b";
        private const string NO_FILE_EXCEPTION = "Указан неверный путь к файлу";
        private const string WRONG_FILE_ENCODIING = "Неверная кодировка файла. Файл должен быть загружен в кодировке UTF-8";
        public const string CREATE_DICTIONARY = "Создан словарь";
        public const string UPDATE_DICTIONARY = "Словарь дополнен";
        public const string DELETE_DICTIONARY = "Словарь удалён";

        private readonly DBControllerBase dBController;
        private readonly OutputControllerBase outputController;
        public DictionaryControllerBase(DBControllerBase _dBController, OutputControllerBase _outputController)
        {
            dBController = _dBController;
            outputController = _outputController;
        }
        private  void CheckExistance(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            if (!fileInfo.Exists)
            {
                throw new Exception(NO_FILE_EXCEPTION);
            }
        }
        private  void CheckEncoding(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                if (sr.CurrentEncoding != Encoding.UTF8)
                {
                    throw new Exception(WRONG_FILE_ENCODIING);
                }
            }
        }
        private  string GetFileContent(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                byte[] array = new byte[fs.Length];
                fs.Read(array, 0, array.Length);

                return Encoding.UTF8.GetString(array).ToLower();
            }
        }
        private  List<UsingWordModel> GetWords(string fileName)
        {
            string fileContent = GetFileContent(fileName);
            Regex regex = new Regex(SEARCH_WORD_REG);
            var x = regex.Matches(fileContent).Count;
            return (from match in regex.Matches(fileContent)
                    group match by match.Groups[1].Value into matchGroup
                    select new UsingWordModel(matchGroup.Key, matchGroup.Count())
                   ).ToList();
        }
        private  List<UsingWordModel> ReadWordsFrom(string fileName)
        {
            return GetWords(fileName);
        }
        public void CreateDictionary(string fileName)
        {
            CheckExistance(fileName);
            CheckEncoding(fileName);
            DeleteDictionary();
            dBController.SetWordArray(ReadWordsFrom(fileName));
            outputController.PrintLine(CREATE_DICTIONARY);
        }
        public  void UpdateDictionary(string fileName)
        {
            dBController.SetWordArray(ReadWordsFrom(fileName));
            outputController.PrintLine(UPDATE_DICTIONARY);
        }
        public  void DeleteDictionary()
        {
            dBController.ClearDictionary();
            outputController.PrintLine(DELETE_DICTIONARY);
        }
    }
}
