using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using Access = Microsoft.Office.Interop.Access;
namespace DataProtector
{
    public partial class Form1 : Form
    {
        bool AutoRun;
        bool Compressed;
        int CompressedCnt;
        GCGCommon.DB db;
        string pvAssemblyName;
        string pvAssemblyPath;
        string pvFilesPath;
        string pvDateToUse;
        public Form1()
        {
            InitializeComponent();
            Init();
            mRecreateAllExecutableResources();
        }
        public Form1(string[] commands)
            : this()
        {
            AutoRun = true;
            DoIt();
        }

        private void cmdReadData_Click(object sender, EventArgs e)
        {
            //unencrypt prior nights backup
            //read the DB table and append the reults to the prior night, then deleting that info from the DB table
            //reencrypt the new file
            //securely delete the old original file
            //Compact and Repair the database

            DoIt();
        }
        private void DoIt()
        {
            //Init();
            string TableToDo;
            TableToDo = "tblNewRequests";
            EncryptOrDecrpytFile(TableToDo, "D");
            WriteTabDelFile(TableToDo);
            SecureDeleteFile("\"" + txtEncFileLoc.Text + "\\" + TableToDo + ".enc" + "\"");
            EncryptOrDecrpytFile(TableToDo, "E");
            SecureDeleteFile("\"" + txtEncFileLoc.Text + "\\" + TableToDo + ".txt" + "\"");

            TableToDo = "tblArchivedMerchantRequests";
            EncryptOrDecrpytFile(TableToDo, "D");
            WriteTabDelFile(TableToDo);
            SecureDeleteFile("\"" + txtEncFileLoc.Text + "\\" + TableToDo + ".enc" + "\"");
            EncryptOrDecrpytFile(TableToDo, "E");
            SecureDeleteFile("\"" + txtEncFileLoc.Text + "\\" + TableToDo + ".txt" + "\"");
            DeleteResourceFiles();
            //StopIIS();
            //CompressAccessDB();
            //DeleteOldAccessDB()
            //StartIIS();
            timer2.Enabled = true;
        }
        private void mRecreateAllExecutableResources()
        {
            // Get Current Assembly refrence
            // Get all imbedded resources
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            string[] arrResources = currentAssembly.GetManifestResourceNames();

            foreach (string resourceName in arrResources)
            {
                System.Diagnostics.Debug.WriteLine(resourceName);
                if (resourceName.Contains("dsc"))
                { //resourceName.EndsWith(".exe") or other extension desired
                    //Name of the file saved on disk
                    string saveAsName = resourceName.Replace(pvAssemblyName + ".", "");
                    string saveAsComplete = pvAssemblyPath + "\\" + saveAsName;
                    FileInfo fileInfoOutputFile = new FileInfo(saveAsComplete);
                    //CHECK IF FILE EXISTS AND DO SOMETHING DEPENDING ON YOUR NEEDS
                    if (fileInfoOutputFile.Exists)
                    {
                        //overwrite if desired  (depending on your needs)
                        //fileInfoOutputFile.Delete();
                    }
                    //OPEN NEWLY CREATING FILE FOR WRITTING
                    FileStream streamToOutputFile = fileInfoOutputFile.OpenWrite();
                    //GET THE STREAM TO THE RESOURCES
                    Stream streamToResourceFile =
                                        currentAssembly.GetManifestResourceStream(resourceName);

                    //---------------------------------
                    //SAVE TO DISK OPERATION
                    //---------------------------------
                    const int size = 4096;
                    byte[] bytes = new byte[4096];
                    int numBytes;
                    while ((numBytes = streamToResourceFile.Read(bytes, 0, size)) > 0)
                    {
                        streamToOutputFile.Write(bytes, 0, numBytes);
                    }
                    System.Diagnostics.Debug.WriteLine(saveAsComplete);
                    streamToOutputFile.Close();
                    streamToResourceFile.Close();
                }//end_if

            }//end_foreach
        }//end_mRecreateAllExecutableResources 
        private void Init()
        {
            //db = new GCGCommon.DB(txtMDBLoc.Text);
            DateTime datetouse = DateTime.Now.AddDays(-7);  //How many days of user data should we keep available in the DB, unencrypted?  A week should be OK.
            pvDateToUse = datetouse.ToString();
            string[] a = GCGCommon.SupportMethods.GetFilePathPieces(txtMDBLoc.Text);
            pvFilesPath = a[1];
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            pvAssemblyName = currentAssembly.GetName().Name;
            pvAssemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //get the folder that's in
            pvAssemblyPath = Path.GetDirectoryName(pvAssemblyPath);
            StreamReader sr = new StreamReader(Application.StartupPath + "\\config.txt");
            txtMDBLoc.Text = sr.ReadLine();
            txtEncFileLoc.Text = sr.ReadLine();
        }
        private void DeleteResourceFiles()
        {
            string EXE = "\"" + Application.StartupPath + "\\sdelete.exe\"";
            string Args = " –p3 \"" + Application.StartupPath + "\\dsc.*\"";
            GCGCommon.SupportMethods.LaunchIt(EXE, Args, true,true);
        }
        private void SecureDeleteFile(string FileName)
        {
            string EXE = "\"" + Application.StartupPath + "\\sdelete.exe\"";
            string Args = " –p3 " + FileName;
            GCGCommon.SupportMethods.LaunchIt(EXE, Args, true,true);
        }
        private void EncryptOrDecrpytFile(string TableToDo, string EorD)
        {
            string eord = EorD.Substring(0, 1).ToLower();
            //"C:\Users\chmitro\Desktop\EncDec\dsc.exe" "C:\Users\chmitro\Desktop\EncDec\test.key" e "C:\All\a.txt" "C:\All\a.enc"
            //"C:\Users\chmitro\Desktop\EncDec\dsc.exe" "C:\Users\chmitro\Desktop\EncDec\test.key" d "C:\All\a.enc" "C:\All\a2.txt"
            string EXE = "\"" + Application.StartupPath + "\\dsc.exe\"";
            string Args = "";
            string ArgKey = "\"" + Application.StartupPath + "\\dsc.key\""; // +" d " +
            string ArgEncedFile = "\"" + txtEncFileLoc.Text + "\\" + TableToDo + ".enc\"";
            string ArgDecedFile = "\"" + txtEncFileLoc.Text + "\\" + TableToDo + ".txt\"";

            if (eord == "e")
            {
                Args = ArgKey + " e " + ArgDecedFile + " " + ArgEncedFile;
            }
            else if (eord == "d")
            {
                Args = ArgKey + " d " + ArgEncedFile + " " + ArgDecedFile;
            }
            GCGCommon.SupportMethods.LaunchIt(EXE, Args, true, true);
            System.Diagnostics.Debug.WriteLine(EXE + " " + Args);
            System.Diagnostics.Debug.WriteLine("Done.");
        }

        
        private void CompressAccessDB()
        {
            Access.Application oAccess = new Access.ApplicationClass();
            bool SkipIt=false;
            string tempdb = txtMDBLoc.Text + "x";
            SecureDeleteFile("\"" + tempdb + "\"");
            StreamWriter sw = new System.IO.StreamWriter(txtEncFileLoc.Text + "//log.txt", true);
            try
            {
                oAccess.CompactRepair(txtMDBLoc.Text, tempdb, false);
                sw.WriteLine("OK " + CompressedCnt.ToString());
                System.Diagnostics.Debug.WriteLine("OK " + CompressedCnt.ToString());
                Compressed = true;
            }
            catch (Exception ex)
            {
                SkipIt = true;
                if (CompressedCnt >= 25)
                {
                    sw.WriteLine("Error " + CompressedCnt.ToString());
                    System.Diagnostics.Debug.WriteLine("Error " + CompressedCnt.ToString());
                }
            }
            finally
            {
                sw.Close();
                oAccess.Quit();
            }
            //oAccess.DBEngine.CompactDatabase(txtMDBLoc.Text, tempdb, null, null, null);
            if (SkipIt == true) return;
            FileInfo fiold = new FileInfo(txtMDBLoc.Text);
            if (fiold.Exists == true)
            {
                //System.Threading.Thread.Sleep(500);
                SecureDeleteFile("\"" + txtMDBLoc.Text + "\"");
            }

            FileInfo finew = new FileInfo(tempdb);
            if (finew.Exists == true)
            {
                finew.CopyTo(txtMDBLoc.Text);
                SecureDeleteFile("\"" + tempdb + "\"");
            }
            //oAccess.OpenCurrentDatabase(txtMDBLoc.Text, false);
            //oAccess.DBEngine.CompactDatabase(txtMDBLoc.Text, temp, null, null, null);
            //oAccess.CommandBars("Menu Bar").Controls("Tools").Controls("Database utilities").Controls("Compact and repair database...").accDoDefaultAction
            //oAccess.CloseCurrentDatabase();
            //oAccess.Quit();
        }
        private void WriteTabDelFile(string TableToDo)
        {
            db = new GCGCommon.DB(txtMDBLoc.Text);
            string[][] s = db.GetMultiValuesOfSQL("Select * from " + TableToDo + " WHERE TimeLogged<@ddatetouse", pvDateToUse);
            System.IO.StreamWriter sw = null;
            StringBuilder sb = null;
            int Max = 0;
            int ColCount = 0;
            string LineToWrite;
            ColCount = s[0].GetUpperBound(0);
            if (ColCount == 49)
            {
                Max = 0;
            }
            else
            {
                ColCount = ColCount + 1;
                Max = s.GetUpperBound(0);
                sw = new System.IO.StreamWriter(txtEncFileLoc.Text + "//" + TableToDo + ".txt", true);
                sb = new StringBuilder();
            }
            for (int LineNumb = 0; LineNumb < Max; LineNumb++)
            {
                sb.Clear();
                string LTW = "";
                for (int CurrCol = 0; CurrCol < ColCount; CurrCol++)
                {
                    sb.Append(s[LineNumb][CurrCol]);
                    if (CurrCol < ColCount - 1)
                    {
                        sb.Append("\t");
                    }
                }
                LTW = sb.ToString();
                sw.WriteLine(LTW);
                System.Diagnostics.Debug.WriteLine(LineNumb);
            }
            if (sw!=null)sw.Close();
            int result = db.ExecuteSQLParamed("DELETE FROM " + TableToDo + " WHERE TimeLogged<@ddatetouse", pvDateToUse);
            System.Diagnostics.Debug.WriteLine("WriteTabDelFile Done.");
            db.CloseOleDbConnection();
            db = null;
        }

        private void cmdDecryptFiles_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo(Application.StartupPath + "\\dsc.exe");
            if (fi.Exists == false) MessageBox.Show("Restart to Decrypt");

            string TableToDo = "";
            TableToDo = "tblNewRequests";
            EncryptOrDecrpytFile(TableToDo, "D");
            TableToDo = "tblArchivedMerchantRequests";
            EncryptOrDecrpytFile(TableToDo, "D");

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (AutoRun==true) this.Visible = false;
            CompressedCnt++;
            try
            {
                db.CloseOleDbConnection();
                db = null;
            }
            catch (Exception ex)
            {
                
            }

                this.Text = "Compressing... " + CompressedCnt.ToString();
                CompressAccessDB();
                if (CompressedCnt >= 150) Compressed = true;
                if (Compressed == true)
                {
                    timer2.Enabled = false;
                    if (AutoRun == true) timer1.Enabled = true;
                }
        }
    }
}
