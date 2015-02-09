﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAdminSite
{
    public class clsBasicSupport : System.Web.UI.Page
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
        public string SetSessionVal(string WhatToSet, string value)
        {
            string retVal = "";
            try
            {
                Session[WhatToSet] = value;
            }
            catch (Exception)
            {
            }
            return retVal;
        }

    }
}