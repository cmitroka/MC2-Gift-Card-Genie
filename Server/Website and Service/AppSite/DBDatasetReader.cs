using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;

public static class DBDatasetReader
{
    private static OleDbConnection aConnection;
    private static OleDbCommand aCommand;
    public static OleDbDataReader reader = null;
    private static string pMDBPath;
    private static void ConfigureDB()
    {
        pMDBPath = HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"].ToString();
        pMDBPath += "\\CTS.mdb";
        aConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pMDBPath);
        aConnection.Open();
    }
    public static OleDbDataReader QueryData(string Query)
    {
        ConfigureDB();
        try
        {
            OleDbCommand cmd = new OleDbCommand(Query, aConnection);
            reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    Console.WriteLine("{0} | {1}", reader["FirstName"].ToString().PadLeft(10), reader[1].ToString().PadLeft(10));
            //}
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
        }
        return reader;
    }

}
