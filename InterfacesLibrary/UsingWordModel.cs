using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLibrary
{
    public class UsingWordModel
    {
        public string Word { get; set; }
        public long RepetionRate { get; set; }
        public UsingWordModel()
        {
            this.Word = "No word";
            this.RepetionRate = 0;
        }
        public UsingWordModel(string _word, long _repetionRate)
        {
            this.Word = _word;
            this.RepetionRate = _repetionRate;
        }
    }
}
