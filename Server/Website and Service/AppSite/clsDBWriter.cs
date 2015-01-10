using System;
using System.Collections.Generic;
using System.Web;
using System.Data.Sql;
using System.Data.OleDb;
using System.Data;


/// <summary>
/// Summary description for clsDBWriter
/// </summary>
public class clsDBWriter
{

    private static OleDbConnection aConnection;
    private static OleDbCommand aCommand;
    private static string pMDBPath;
    public clsDBWriter(string mdbfileloc)
    {
        ConfigureDB(mdbfileloc);
    }
    public void dealloc()
    {
        aCommand = null;
        aConnection.Close();
        aConnection = null;
    }
    public void ConfigureDB(string mdbfileloc)
    {
        aConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbfileloc);
        aConnection.Open();
    }
    public string CorrectSQLAnomolies(string SQLin)
    {
        string CurrChar=null;
        string PrevChar=null;
        string NextChar = null;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < SQLin.Length; i++)
        {
            CurrChar = SQLin.Substring(i, 1);
            if (CurrChar=="'")
            {
                try
                {
                NextChar = SQLin.Substring(i + 1, 1);
                }
                catch (Exception exc)
                {
                }
                if ((PrevChar == "(") | (PrevChar == "=") | (PrevChar == ",") | (NextChar == ")") | (NextChar == ","))
                {
                }
                else
                {
                    CurrChar = "''";
                    PrevChar = "''";
                }
            }
            PrevChar = CurrChar;
            sb.Append(CurrChar);
        }
        return sb.ToString();
    }
    public string ExecuteQueryForOneValue(string Query)
    {
        string temp = "";
        try
        {
            aConnection.Open();
        }
        catch (Exception e) {Console.WriteLine("Error: "+e.Message);}
        aCommand = new OleDbCommand(Query, aConnection);
        temp = aCommand.ExecuteScalar().ToString();

        aConnection.Close();
        return temp;
    }

    public int ExecutePQueryFeedback(string pName, string pEmail, string pComment)
    {
        // assuming a textbox with id txtUserId exists on the aspx page      
                     
        string sql = "SELECT name, password FROM users WHERE id=@userid";
        sql = "INSERT INTO tblFeedback ( Name, ContactInfo, Feedback ) VALUES  (@name, @email, @comment);";
        aCommand = new OleDbCommand(sql, aConnection);
        aCommand.Connection = aConnection;
        aCommand.CommandType = CommandType.Text;
        aCommand.CommandText = sql;
        aCommand.Parameters.AddWithValue("name", pName);
        aCommand.Parameters.AddWithValue("email", pEmail);
        aCommand.Parameters.AddWithValue("comment", pComment);
        int OK = -1;
        try
        {
            OK = aCommand.ExecuteNonQuery();
        }
        catch (Exception)
        {            
        }
        return OK;
    }

    public int ExecuteQuery(string Query)
    {
        int RowsAffected = -1;
        try
        {
            //aConnection.Open();
            aCommand = new OleDbCommand(Query, aConnection);
            Console.WriteLine(Query);
            RowsAffected = aCommand.ExecuteNonQuery();
        }
        catch (OleDbException e)
        {
            Console.WriteLine("Error: {0}", e.Errors[0].Message);
        }
        finally
        {
            aConnection.Close();
        }
        return RowsAffected;
    }

    public DataSet MakeDataset(string Query)
    {
        DataSet ds = new DataSet();
        try
        {
            OleDbDataAdapter da = new OleDbDataAdapter(Query, aConnection);
            da.Fill(ds, "RandomData");
        }
        catch (OleDbException e)
        {
            Console.WriteLine("Error: {0}", e.Errors[0].Message);
            ds = null;
        }
        finally
        {
            aConnection.Close();
        }
        return ds;

        /* HOW IT'S USED.
        foreach (DataRow dr in dset.Tables["RandomData"].Rows)
        {
            System.Console.WriteLine("{0}", dr["firstName"]);
            System.Console.WriteLine(dr["firstName"].ToString());
        }
        */
    }
    public string DoInsertReturnId(string InsertQuery)
    {
        OleDbCommand cmdIns = new OleDbCommand(InsertQuery, aConnection);
        OleDbCommand cmdID = new OleDbCommand("Select @@IDENTITY", aConnection);
        string ID = null;
        try
        {
            //aConnection.Open();
            cmdIns.ExecuteNonQuery();
            ID = cmdID.ExecuteScalar().ToString();
        }
        catch(Exception ex1)
        {
        }
        finally {aConnection.Close();}
        return ID;
    }
    //string sql = "INSERT INTO Employee (Name,Age) VALUES('Smith',44); SELECT @@IDENTITY";
}
