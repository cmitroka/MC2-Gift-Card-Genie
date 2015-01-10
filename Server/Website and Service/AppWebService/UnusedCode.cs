/*
public string[][] GetMultiValuesOfSQL(string SQLin)
        {
            string[][] retVal;
            string tempLine = "";
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter(SQLin, aConnection);
                da.Fill(ds, "RandomData");
                int curP1Index=0;
                int arrP1Size=ds.Tables["RandomData"].Rows.Count-1;
                if (arrP1Size==-1) return null;
                retVal=new string[arrP1Size+1][];
                foreach (System.Data.DataRow dr in ds.Tables["RandomData"].Rows)
                {
                    List<string> myCollection = new List<string>();
                    for (int i = 0; i < ds.Tables["RandomData"].Columns.Count; i++)
			        {
                        tempLine = dr[i].ToString();
                        myCollection.Add(tempLine);
			        }
                    string[] addToMulti=myCollection.ToArray();
                    retVal[curP1Index] = addToMulti;
                    if (curP1Index==arrP1Size)break;
                    curP1Index++;
                }
                return retVal;
            }
            catch (OleDbException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return retVal;
        }

        public string GetSingleValueOfSQL(string SQLin)
        {
            string retVal = "";
            string tempLine = "";
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter(SQLin, aConnection);
                da.Fill(ds, "RandomData");
                foreach (System.Data.DataRow dr in ds.Tables["RandomData"].Rows)
                {
                    tempLine = dr[0].ToString();
                    retVal = tempLine;
                }
            }
            catch (OleDbException ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
            return retVal;
        }
        public string SendAdditionalInfo(string pUDID, string pIDFileName, string pAnswer)
        {
            string retVal = "";
            string rsType = "SERVERERROR";
            string rsValue = "";
            try
            {
                string TestForValid = SetGlobalVariables();
                if (TestForValid != "") return rsType + "~We seem to have a setup problem, try again or send us an email.";
                bool FileMade = false;
                string MakeRqFile = gloPathToRqRs + "\\" + pIDFileName + "rq.txt";
                string RsFileToRead = gloPathToRqRs + "\\" + pIDFileName + "rs.txt";
                while (System.IO.File.Exists(MakeRqFile) == true)
                {
                    System.IO.File.Delete(MakeRqFile);
                }
                while (System.IO.File.Exists(RsFileToRead) == true)
                {
                    System.IO.File.Delete(RsFileToRead);
                }
                System.IO.StreamWriter s = new System.IO.StreamWriter(MakeRqFile);
                s.WriteLine(pAnswer);
                s.Close();
                FileMade = WaitForResponseFileCreation(RsFileToRead);
                if (FileMade == true)
                {
                    retVal = ProcessResponse(RsFileToRead);
                }
                else
                {
                    retVal = GCGCommon.EnumExtensions.WebserviceTypes.WSTIMEOUT.ToString() + LINEDEL + "Webservice Timed Out";
                }
            }
            catch (Exception ex)
            {
                retVal = GCGCommon.EnumExtensions.WebserviceTypes.WSERR.ToString() + LINEDEL + ex.Message;
            }
            return retVal;
        }
        public string AnswerCAPTCHA(string pUDID, string pIDFileName, string pAnswer)
        {
            string retVal = "";
            string rsType = "SERVERERROR";
            string rsValue = "";
            try
            {
                string TestForValid = SetGlobalVariables();
                if (TestForValid != "") return rsType + "~We seem to have a setup problem, try again or send us an email.";
                bool FileMade = false;
                double test;
                string MakeRqFile = gloPathToRqRs + "\\" + pIDFileName + "CAPTCHArq.txt";
                string RsFileToRead = gloPathToRqRs + "\\" + pIDFileName + "CAPTCHArs.txt";
                while (System.IO.File.Exists(MakeRqFile) == true)
                {
                    System.IO.File.Delete(MakeRqFile);
                }
                while (System.IO.File.Exists(RsFileToRead) == true)
                {
                    System.IO.File.Delete(RsFileToRead);
                }
                System.IO.StreamWriter s = new System.IO.StreamWriter(MakeRqFile);
                s.WriteLine(pAnswer);
                s.Close();
                int x = 0;
                do
                {
                    x = x + 1;
                    try
                    {
                        FileMade = System.IO.File.Exists(RsFileToRead);
                    }
                    catch (Exception ex0)
                    {
                        Console.WriteLine(ex0.Message);
                    }
                    if (FileMade == true) break;
                    System.Threading.Thread.Sleep(1000);
                } while (x < gloWebserviceTimeout);
                if (FileMade == true)
                {
                    retVal = ProcessResponse(RsFileToRead);
                }
                else
                {
                    retVal = GCGCommon.EnumExtensions.WebserviceTypes.WSTIMEOUT.ToString() + LINEDEL + "Webservice Timed Out";
                }
            }
            catch (Exception ex)
            {
                retVal = GCGCommon.EnumExtensions.WebserviceTypes.WSERR.ToString() + LINEDEL + "Genreal Error";
            }
            return retVal;
        }

*/