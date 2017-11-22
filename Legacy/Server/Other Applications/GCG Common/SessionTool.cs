using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCGCommon
{
    public class SessionTool : System.Web.UI.Page
    {
        public string RetSessionVal(string WhatToGet)
        {
            string retVal = "";
            try
            {
                retVal = Session[WhatToGet].ToString();
            }
            catch (Exception)
            {
            }
            return retVal;
        }
    }
}
