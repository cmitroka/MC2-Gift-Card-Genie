﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCGCommon
{
    public class AllDetails
    {
        public string GCBALANCE;
        public string GCBALANCEERR;
        public string GCCAPTCHA;
        public string GCERR;
        public string GCNEEDSMOREINFO;
        public string GCTIMEOUT;
        public string POSDEL;
        public string LINEDEL;
        public string RxPath;
        public string RxFileWOExt;
        public string RsFileWOExt;
        public string RqFileWOExt;
        public string RqPathAndFileToRead;
        public string RsPathAndFileToWrite;
        public string NextRxFileWOExt;
        public string NextRqPathAndFileToRead;
        public string CAPTCHAPathAndFileToWrite;
        public AllDetails(string RqPathFile, string CAPTCHAPath)
        {
            UpdateAllDetails(RqPathFile, CAPTCHAPath);
        }
        private string HandleOOB(string[] arrIn, int index)
        {
            string retVal = "";
            int ub=arrIn.GetUpperBound(0);
            if (index <= ub)
            {
                retVal = arrIn[index];
            }
            return retVal;
        }
        public void UpdateAllDetails(string RqPathFile, string CAPTCHAPath)
        {
            LINEDEL = GCGCommon.EnumExtensions.Description(GCGCommon.Delimiters.LINEDEL);
            POSDEL = GCGCommon.EnumExtensions.Description(GCGCommon.Delimiters.POSDEL);
            GCBALANCE = GCGCommon.GCTypes.GCBALANCE.ToString();
            GCBALANCEERR = GCGCommon.GCTypes.GCBALANCEERR.ToString();
            GCCAPTCHA = GCGCommon.GCTypes.GCCAPTCHA.ToString();
            GCERR = GCGCommon.GCTypes.GCERR.ToString();
            GCNEEDSMOREINFO = GCGCommon.GCTypes.GCNEEDSMOREINFO.ToString();
            GCTIMEOUT = GCGCommon.GCTypes.GCTIMEOUT.ToString();
            string[] temp = GCGCommon.SupportMethods.GetFilePathPieces(RqPathFile);
            string[] temp2 = GCGCommon.SupportMethods.SplitByString(temp[3], "-");
            string fileid = HandleOOB(temp2, 0);
            string filerxnum = HandleOOB(temp2, 1);
            string x = filerxnum.Substring(0, 1);
            int irxnum = Convert.ToInt16(x);
            int inextrxnum = irxnum + 1;
            string filemerch = HandleOOB(temp2, 2);
            RxPath = temp[1];
            RxFileWOExt = temp[3].Replace("rq", "rx");
            RqFileWOExt = temp[3];
            RsFileWOExt = temp[3].Replace("rq", "rs");
            RqPathAndFileToRead = RqPathFile;
            RsPathAndFileToWrite = RqPathFile.Replace("rq", "rs");
            //NextRxFileWOExt = AllDetailsMethods.GetNextRxFileWOExt(rqnum);

            if (filemerch=="")
            {
                NextRxFileWOExt = fileid + "-" + inextrxnum.ToString() + "rx";
            }
            else if (filemerch!="")
            {
                NextRxFileWOExt = fileid+"-"+inextrxnum.ToString()+"rx"+"-"+filemerch;
            }
            string NextRqFileToRead = NextRxFileWOExt.Replace("rx", "rq");

            NextRqPathAndFileToRead = RxPath + "\\" + NextRqFileToRead + ".txt";
            CAPTCHAPathAndFileToWrite = CAPTCHAPath + "\\" + NextRxFileWOExt + ".bmp";
        }
    }
    public static class AllDetailsMethods
    {
        public static string GetNextRxFileWOExt(string pRxFileWOExt)
        {
            string retVal = "";           
            string[] temp = GCGCommon.SupportMethods.SplitByString(pRxFileWOExt, "-");
            string strRqNum = temp[0].Substring(0, 1);
            int rqNum = Convert.ToInt16(strRqNum);
            string strRqNumP1 = (rqNum + 1).ToString();
            retVal = pRxFileWOExt.Replace(strRqNum, strRqNumP1);
            return retVal;
        }

    }
}