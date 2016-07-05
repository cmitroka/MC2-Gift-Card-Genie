using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Data.OleDb;
using System.Runtime.InteropServices;

namespace AutomationViaExternalApp
{
    public static class CJMFunctions
    {
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        public static string DoesTextExistInClipboard(string pSearchFor)
        {
            string retVal = "-1";
            pSearchFor = pSearchFor.ToUpper();
            try
            {
                string pClipboard = Clipboard.GetText().ToUpper();
                if (pClipboard.Contains(pSearchFor))
                {
                    retVal = "1";
                }
            }
            catch (Exception ex)
            {
                
            }
            return retVal;
        }
        public static string TrimQuotesFromBandE(string lineIn)
        {
            string retVal = "";
            int cntr = 0;
            try
            {
                string bChar = lineIn.Substring(0, 1);
                if (bChar == @"""")
                {
                    retVal = lineIn.Substring(1, lineIn.Length - 1);
                    cntr++;
                }
                string eChar = lineIn.Substring(lineIn.Length - 1, 1);
                if (eChar == @"""")
                {
                    retVal = retVal.Substring(0, retVal.Length - 1);
                    cntr++;
                }
            }
            catch (Exception ex)
            {
            }

            if (cntr != 2)
            {
                retVal = lineIn;
            }
            return retVal;
        }
        public static string GetPlainTextFromHTML(string HTMLIn)
        {
            string retVal = "";
            try
            {
                //retVal = FrameDoc.body.innerHTML;
                string temp = System.Text.RegularExpressions.Regex.Replace(HTMLIn, "(&lt;(((?!/&gt;).)*)/&gt;)|(&lt;(((?!&gt;).)*)&gt;)|(<(((?!/>).)*)/>)|(<(((?!>).)*)>)", String.Empty);
                retVal = temp;
            }
            catch (Exception ex)
            {
            }
            return retVal;
        }
        public static string RoughExtract(string StringInStart, string StringInStop, string EnitreHTML)
        {
            EnitreHTML = EnitreHTML.ToUpper();
            StringInStart = StringInStart.ToUpper();
            StringInStop = StringInStop.ToUpper();
            int index = 0;
            int startloc = 0;
            int endloc = 0;
            string retVal = "";
            try
            {
                do
                {
                    startloc = EnitreHTML.IndexOf(StringInStart, startloc + 1);
                    if (startloc == -1) break;
                    int a = startloc + StringInStart.Length;
                    endloc = EnitreHTML.IndexOf(StringInStop, a + 1);
                    if (StringInStop == "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX")
                    {
                        endloc = EnitreHTML.Length;
                    }
                    int b = endloc - a;
                    string temphtml = EnitreHTML.Substring(a, b);
                    retVal = temphtml;
                    return retVal;
                } while (true);
            }
            catch (Exception ex)
            {
                retVal = "";
            }
            return retVal;
        }
        public static string ReturnStandardBalance(string StringIn)
        {
            string retVal = "";
            try
            {
                retVal = System.Text.RegularExpressions.Regex.Replace(StringIn, "[^.0-9]", "");
                string decCheck;
                do
                {
                    decCheck = retVal.Substring(retVal.Length - 1, 1);
                    if (decCheck == ".")
                    {
                        retVal = retVal.Substring(0, retVal.Length - 1);
                    }
                    else
                    {
                        break;
                    }
                } while (1 == 1);
                decimal test = Convert.ToDecimal(retVal);
                retVal = String.Format("{0:c}", test);
                return retVal;
            }
            catch (Exception ex)
            {
                retVal = "";
            }
            return retVal;
        }
        public static bool AdjustWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight)
        {
            bool OK = MoveWindow(hWnd, X, Y, nWidth, nHeight, true);
            return OK;
        }

    }

    
    
    
    public class CJMAccessDB
    {
        public OleDbConnection aConnection;
        public CJMAccessDB(string PathOfDB)
        {
            GetAndOpenOleDbConnection(PathOfDB);
        }
        public void CloseOleDbConnection()
        {
            aConnection.Close();
            aConnection.Dispose();
            aConnection = null;
        }
        public static bool isDatasetBad(string[][] dataIn)
        {
            if (dataIn == null) return true;
            try
            {
                if (dataIn[0][49] == "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private OleDbConnection GetAndOpenOleDbConnection(string mdbtouse)
        {
            try
            {
                aConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbtouse);
                aConnection.Open();
            }
            catch (Exception ex)
            {
                //retVal = ex.Message;
                //return "E10 - " + retVal;
                return null;
            }
            return aConnection;
        }
        public string[][] GetMultiValuesOfSQL(string SQLin)
        {
            string[][] retVal;
            retVal = GetMultiValuesOfSQLParamed(SQLin, "", "", "", "", "", "", "", "", "", "");
            return retVal;
        }
        public string[][] GetMultiValuesOfSQL(string SQLin, string P0)
        {
            string[][] retVal;
            retVal = GetMultiValuesOfSQLParamed(SQLin, P0, "", "", "", "", "", "", "", "", "");
            return retVal;
        }
        public string[][] GetMultiValuesOfSQL(string SQLin, string P0, string P1)
        {
            string[][] retVal;
            retVal = GetMultiValuesOfSQLParamed(SQLin, P0, P1, "", "", "", "", "", "", "", "");
            return retVal;
        }
        public string[][] GetMultiValuesOfSQL(string SQLin, string P0, string P1, string P2)
        {
            string[][] retVal;
            retVal = GetMultiValuesOfSQLParamed(SQLin, P0, P1, P2, "", "", "", "", "", "", "");
            return retVal;
        }
        public string[][] GetMultiValuesOfSQL(string SQLin, string P0, string P1, string P2, string P3)
        {
            string[][] retVal;
            retVal = GetMultiValuesOfSQLParamed(SQLin, P0, P1, P2, P3, "", "", "", "", "", "");
            return retVal;
        }
        public string[][] GetMultiValuesOfSQL(string SQLin, string P0, string P1, string P2, string P3, string P4)
        {
            string[][] retVal;
            retVal = GetMultiValuesOfSQLParamed(SQLin, P0, P1, P2, P3, P4, "", "", "", "", "");
            return retVal;
        }
        public string[][] GetMultiValuesOfSQL(string SQLin, string P0, string P1, string P2, string P3, string P4, string P5)
        {
            string[][] retVal;
            retVal = GetMultiValuesOfSQLParamed(SQLin, P0, P1, P2, P3, P4, P5, "", "", "", "");
            return retVal;
        }
        public string[][] GetMultiValuesOfSQL(string SQLin, string P0, string P1, string P2, string P3, string P4, string P5, string P6)
        {
            string[][] retVal;
            retVal = GetMultiValuesOfSQLParamed(SQLin, P0, P1, P2, P3, P4, P5, P6, "", "", "");
            return retVal;
        }
        public string[][] GetMultiValuesOfSQL(string SQLin, string P0, string P1, string P2, string P3, string P4, string P5, string P6, string P7)
        {
            string[][] retVal;
            retVal = GetMultiValuesOfSQLParamed(SQLin, P0, P1, P2, P3, P4, P5, P6, P7, "", "");
            return retVal;
        }
        public string[][] GetMultiValuesOfSQL(string SQLin, string P0, string P1, string P2, string P3, string P4, string P5, string P6, string P7, string P8)
        {
            string[][] retVal;
            retVal = GetMultiValuesOfSQLParamed(SQLin, P0, P1, P2, P3, P4, P5, P6, P7, P8, "");
            return retVal;
        }
        public string[][] GetMultiValuesOfSQL(string SQLin, string P0, string P1, string P2, string P3, string P4, string P5, string P6, string P7, string P8, string P9)
        {
            string[][] retVal;
            retVal = GetMultiValuesOfSQLParamed(SQLin, P0, P1, P2, P3, P4, P5, P6, P7, P8, P9);
            return retVal;
        }
        public int ExecuteSQLParamed(string SQLin)
        {
            int retVal;
            retVal = ExecuteSQLParamed(SQLin, "", "", "", "", "", "", "", "", "", "");
            return retVal;
        }
        public int ExecuteSQLParamed(string SQLin, string P0)
        {
            int retVal;
            retVal = ExecuteSQLParamed(SQLin, P0, "", "", "", "", "", "", "", "", "");
            return retVal;
        }
        public int ExecuteSQLParamed(string SQLin, string P0, string P1)
        {
            int retVal;
            retVal = ExecuteSQLParamed(SQLin, P0, P1, "", "", "", "", "", "", "", "");
            return retVal;
        }
        public int ExecuteSQLParamed(string SQLin, string P0, string P1, string P2)
        {
            int retVal;
            retVal = ExecuteSQLParamed(SQLin, P0, P1, P2, "", "", "", "", "", "", "");
            return retVal;
        }
        public int ExecuteSQLParamed(string SQLin, string P0, string P1, string P2, string P3)
        {
            int retVal;
            retVal = ExecuteSQLParamed(SQLin, P0, P1, P2, P3, "", "", "", "", "", "");
            return retVal;
        }
        public int ExecuteSQLParamed(string SQLin, string P0, string P1, string P2, string P3, string P4)
        {
            int retVal;
            retVal = ExecuteSQLParamed(SQLin, P0, P1, P2, P3, P4, "", "", "", "", "");
            return retVal;
        }
        public int ExecuteSQLParamed(string SQLin, string P0, string P1, string P2, string P3, string P4, string P5)
        {
            int retVal;
            retVal = ExecuteSQLParamed(SQLin, P0, P1, P2, P3, P4, P5, "", "", "", "");
            return retVal;
        }
        public int ExecuteSQLParamed(string SQLin, string P0, string P1, string P2, string P3, string P4, string P5, string P6)
        {
            int retVal;
            retVal = ExecuteSQLParamed(SQLin, P0, P1, P2, P3, P4, P5, P6, "", "", "");
            return retVal;
        }
        public int ExecuteSQLParamed(string SQLin, string P0, string P1, string P2, string P3, string P4, string P5, string P6, string P7)
        {
            int retVal;
            retVal = ExecuteSQLParamed(SQLin, P0, P1, P2, P3, P4, P5, P6, P7, "", "");
            return retVal;
        }
        public int ExecuteSQLParamed(string SQLin, string P0, string P1, string P2, string P3, string P4, string P5, string P6, string P7, string P8)
        {
            int retVal;
            retVal = ExecuteSQLParamed(SQLin, P0, P1, P2, P3, P4, P5, P6, P7, P8, "");
            return retVal;
        }


        private string[][] GetMultiValuesOfSQLParamed(string SQLin, string P0, string P1, string P2, string P3, string P4, string P5, string P6, string P7, string P8, string P9)
        {
            string[][] retVal;
            //string sql = "INSERT INTO tblFeedback (UDID, Name, ContactInfo, Feedback, IP, TimeLogged) VALUES  (@UDID, @NameP, @ContactInfo, @Feedback, @IP, @TimeLogged);";
            OleDbCommand cmdQry = new OleDbCommand(SQLin, aConnection);
            cmdQry.Connection = aConnection;
            //cmdQry.CommandType = CommandType.Text;
            if (P0 != "") { cmdQry.Parameters.AddWithValue("P0", P0); } else { cmdQry.Parameters.AddWithValue("P0", DBNull.Value); }
            if (P1 != "") { cmdQry.Parameters.AddWithValue("P1", P1); } else { cmdQry.Parameters.AddWithValue("P1", DBNull.Value); }
            if (P2 != "") { cmdQry.Parameters.AddWithValue("P2", P2); } else { cmdQry.Parameters.AddWithValue("P2", DBNull.Value); }
            if (P3 != "") { cmdQry.Parameters.AddWithValue("P3", P3); } else { cmdQry.Parameters.AddWithValue("P3", DBNull.Value); }
            if (P4 != "") { cmdQry.Parameters.AddWithValue("P4", P4); } else { cmdQry.Parameters.AddWithValue("P4", DBNull.Value); }
            if (P5 != "") { cmdQry.Parameters.AddWithValue("P5", P5); } else { cmdQry.Parameters.AddWithValue("P5", DBNull.Value); }
            if (P6 != "") { cmdQry.Parameters.AddWithValue("P6", P6); } else { cmdQry.Parameters.AddWithValue("P6", DBNull.Value); }
            if (P7 != "") { cmdQry.Parameters.AddWithValue("P7", P7); } else { cmdQry.Parameters.AddWithValue("P7", DBNull.Value); }
            if (P8 != "") { cmdQry.Parameters.AddWithValue("P8", P8); } else { cmdQry.Parameters.AddWithValue("P8", DBNull.Value); }
            if (P9 != "") { cmdQry.Parameters.AddWithValue("P9", P9); } else { cmdQry.Parameters.AddWithValue("P9", DBNull.Value); }
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                if (P0 == "")
                {
                    OleDbDataAdapter da = new OleDbDataAdapter(SQLin, aConnection);
                    da.Fill(ds, "RandomData");
                }
                else
                {
                    OleDbDataAdapter da = new OleDbDataAdapter(cmdQry);
                    da.Fill(ds, "RandomData");
                }
                int curP1Index = 0;
                int arrP1Size = ds.Tables["RandomData"].Rows.Count - 1;
                if (arrP1Size == -1)
                {
                    string[] fake = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
                    retVal = new string[1][];
                    retVal[0] = fake;
                    return retVal;
                }
                retVal = new string[arrP1Size + 1][];
                foreach (System.Data.DataRow dr in ds.Tables["RandomData"].Rows)
                {
                    List<string> myCollection = new List<string>();
                    for (int i = 0; i < ds.Tables["RandomData"].Columns.Count; i++)
                    {
                        string tempLine = dr[i].ToString();
                        myCollection.Add(tempLine);
                    }
                    string[] addToMulti = myCollection.ToArray();
                    retVal[curP1Index] = addToMulti;
                    if (curP1Index == arrP1Size) break;
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
        public int ExecuteSQLParamed(string SQLin, string P0, string P1, string P2, string P3, string P4, string P5, string P6, string P7, string P8, string P9)
        {
            int retVal;
            OleDbCommand cmdID = new OleDbCommand("Select @@IDENTITY", aConnection);
            string ID = null;
            OleDbCommand cmdExec = new OleDbCommand(SQLin, aConnection);
            cmdExec.Connection = aConnection;
            cmdExec.CommandText = SQLin;
            if (P0 != "") { cmdExec.Parameters.AddWithValue("P0", P0); } else { cmdExec.Parameters.AddWithValue("P0", DBNull.Value); }
            if (P1 != "") { cmdExec.Parameters.AddWithValue("P1", P1); } else { cmdExec.Parameters.AddWithValue("P1", DBNull.Value); }
            if (P2 != "") { cmdExec.Parameters.AddWithValue("P2", P2); } else { cmdExec.Parameters.AddWithValue("P2", DBNull.Value); }
            if (P3 != "") { cmdExec.Parameters.AddWithValue("P3", P3); } else { cmdExec.Parameters.AddWithValue("P3", DBNull.Value); }
            if (P4 != "") { cmdExec.Parameters.AddWithValue("P4", P4); } else { cmdExec.Parameters.AddWithValue("P4", DBNull.Value); }
            if (P5 != "") { cmdExec.Parameters.AddWithValue("P5", P5); } else { cmdExec.Parameters.AddWithValue("P5", DBNull.Value); }
            if (P6 != "") { cmdExec.Parameters.AddWithValue("P6", P6); } else { cmdExec.Parameters.AddWithValue("P6", DBNull.Value); }
            if (P7 != "") { cmdExec.Parameters.AddWithValue("P7", P7); } else { cmdExec.Parameters.AddWithValue("P7", DBNull.Value); }
            if (P8 != "") { cmdExec.Parameters.AddWithValue("P8", P8); } else { cmdExec.Parameters.AddWithValue("P8", DBNull.Value); }
            if (P9 != "") { cmdExec.Parameters.AddWithValue("P9", P9); } else { cmdExec.Parameters.AddWithValue("P9", DBNull.Value); }
            int OK = -1;
            try
            {
                OK = cmdExec.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retVal = -1;
                return retVal;
            }
            try
            {
                string temp = cmdID.ExecuteScalar().ToString();
                retVal = Convert.ToInt32(temp);
            }
            catch (Exception ex)
            {
                retVal = -2;
                return retVal;
            }
            return retVal;
        }
    }

    public class CJMRegistry
    {
        private bool showError = false;
        public bool ShowError
        {
            get { return showError; }
            set { showError = value; }
        }

        private string subKey = "SOFTWARE\\" + "XXX"; //Application.ProductName.ToUpper();
        /// <summary>
        /// A property to set the SubKey value
        /// (default = "SOFTWARE\\" + Application.ProductName.ToUpper())
        /// </summary>
        public string SubKey
        {
            get { return subKey; }
            set { subKey = value; }
        }

        private RegistryKey baseRegistryKey = Microsoft.Win32.Registry.LocalMachine;
        /// <summary>
        /// A property to set the BaseRegistryKey value.
        /// (default = Registry.LocalMachine)
        /// </summary>
        public RegistryKey BaseRegistryKey
        {
            get { return baseRegistryKey; }
            set { baseRegistryKey = value; }
        }

        /* **************************************************************************
         * **************************************************************************/

        public string[] Read(int KeyIndex)
        {
            string[] retVal = new string[2];
            string temp = "";
            RegistryKey rk = baseRegistryKey;
            // Open a subKey as read-only
            RegistryKey sk1 = rk.OpenSubKey(subKey);
            string[] s = sk1.GetValueNames();
            try
            {
                temp = s[KeyIndex];
                retVal[0] = temp;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
            retVal[1] = Read(temp);
            return retVal;
        }

        /// <summary>
        /// To read a registry key.
        /// input: KeyName (string)
        /// output: value (string) 
        /// </summary>
        public string Read(string KeyName)
        {
            // Opening the registry key
            RegistryKey rk = baseRegistryKey;
            // Open a subKey as read-only
            RegistryKey sk1 = rk.OpenSubKey(subKey);
            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {
                return null;
            }
            else
            {
                try
                {
                    // If the RegistryKey exists I get its value
                    // or null is returned.
                    return (string)sk1.GetValue(KeyName.ToUpper());
                }
                catch (Exception e)
                {
                    // AAAAAAAAAAARGH, an error!
                    ShowErrorMessage(e, "Reading registry " + KeyName.ToUpper());
                    return null;
                }
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        /// <summary>
        /// To write into a registry key.
        /// input: KeyName (string) , Value (object)
        /// output: true or false 
        /// </summary>
        public bool Write(string KeyName, object Value)
        {
            try
            {
                // Setting
                RegistryKey rk = baseRegistryKey;
                // I have to use CreateSubKey 
                // (create or open it if already exits), 
                // 'cause OpenSubKey open a subKey as read-only
                RegistryKey sk1 = rk.CreateSubKey(subKey);
                // Save the value
                sk1.SetValue(KeyName.ToUpper(), Value);

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                ShowErrorMessage(e, "Writing registry " + KeyName.ToUpper());
                return false;
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        /// <summary>
        /// To delete a registry key.
        /// input: KeyName (string)
        /// output: true or false 
        /// </summary>
        public bool DeleteKey(string KeyName)
        {
            try
            {
                // Setting
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk1 = rk.CreateSubKey(subKey);
                // If the RegistrySubKey doesn't exists -> (true)
                if (sk1 == null)
                    return true;
                else
                    sk1.DeleteValue(KeyName);

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                ShowErrorMessage(e, "Deleting SubKey " + subKey);
                return false;
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        /// <summary>
        /// To delete a sub key and any child.
        /// input: void
        /// output: true or false 
        /// </summary>
        public bool DeleteSubKeyTree()
        {
            try
            {
                // Setting
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk1 = rk.OpenSubKey(subKey);
                // If the RegistryKey exists, I delete it
                if (sk1 != null)
                    rk.DeleteSubKeyTree(subKey);

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                ShowErrorMessage(e, "Deleting SubKey " + subKey);
                return false;
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        /// <summary>
        /// Retrive the count of subkeys at the current key.
        /// input: void
        /// output: number of subkeys
        /// </summary>
        public int SubKeyCount()
        {
            try
            {
                // Setting
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk1 = rk.OpenSubKey(subKey);
                // If the RegistryKey exists...
                if (sk1 != null)
                    return sk1.SubKeyCount;
                else
                    return 0;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                ShowErrorMessage(e, "Retriving subkeys of " + subKey);
                return 0;
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        /// <summary>
        /// Retrive the count of values in the key.
        /// input: void
        /// output: number of keys
        /// </summary>
        public int ValueCount()
        {
            try
            {
                // Setting
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk1 = rk.OpenSubKey(subKey);
                // If the RegistryKey exists...
                if (sk1 != null)
                    return sk1.ValueCount;
                else
                    return 0;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                ShowErrorMessage(e, "Retriving keys of " + subKey);
                return 0;
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        private void ShowErrorMessage(Exception e, string Title)
        {
            return;
            /*
            if (showError == true)
                MessageBox.Show(e.Message,
                                Title
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);

             */
        }
    }

}
