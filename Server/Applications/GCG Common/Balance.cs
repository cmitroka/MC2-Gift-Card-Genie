using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCGCommon
{
    public class Balance
    {
        public string StartTag;
        public string EndTag;
        public string Result;
        public Balance(string pStartTag, string pEndTag)
        {
            StartTag = pStartTag;
            EndTag = pEndTag;
        }

    }
}
