using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

    public static class SupportMethods00
    {
        public static string ValidateSession(string SessionID, string Checksum)
        {

            return "1";
        }
        public static string RemoveNonAlphaNumericChars(string dataIn)
        {
            string retVal = "";
            System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]");
            retVal = rgx.Replace(dataIn, "");
            return retVal;
        }

        public static string[] SplitByString(this string OriginalText, string ParseWith)
        {
            return OriginalText.Split(new string[] { ParseWith }, StringSplitOptions.None);
        }
        public static string[] GetFilePathPieces(string fileLocation)
        {
            string[] retme = new string[5];
            try
            {
                retme[0] = System.IO.Path.GetFileName(fileLocation);
            }
            catch (Exception ex0) { }
            try
            {
                retme[1] = System.IO.Path.GetDirectoryName(fileLocation);
            }
            catch (Exception ex1) { }
            try
            {
                retme[2] = System.IO.Path.GetExtension(fileLocation);
            }
            catch (Exception ex2) { }
            try
            {
                retme[3] = System.IO.Path.GetFileNameWithoutExtension(fileLocation);
            }
            catch (Exception ex3) { }
            try
            {
                retme[4] = System.IO.Path.GetPathRoot(fileLocation);
            }
            catch (Exception ex4) { }
            return retme;
        }

        public static string CreateHexKey(int length)
        {
            Random r = new Random();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int rndInt;
            string retVal = "";
            //char[] chars = new char[61];
            //chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            char[] chars = new char[15];
            chars = "ABCDEF1234567890".ToCharArray();
            for (int i = 0; i < length; i++)
            {
                rndInt = r.Next(0, 15);
                char c = chars[rndInt];
                string s = c.ToString();
                sb.Append(s);
            }
            retVal = sb.ToString();
            //CJMUtilities.File.QuickWrite("C:\\out.txt", retVal);
            return retVal;
        }
    }