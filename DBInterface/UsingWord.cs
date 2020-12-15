using System;
using System.Collections.Generic;

#nullable disable

namespace WordsDB
{
    public partial class UsingWord
    {
        public long Tuid { get; set; }
        public string Word { get; set; }
        public long RepetionRate { get; set; }

        public UsingWord()
        {
        }
    }
}
