// ------------------------------------ DBADapter.java ---------------------------------------------

// TODO: Change the package to match your project.
package com.mc2techservices.gcg;

import java.io.File;

import android.content.Context;
import android.content.ContextWrapper;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;
public class DBAdapter {

	/////////////////////////////////////////////////////////////////////
	//	Constants & Data
	/////////////////////////////////////////////////////////////////////
	// For logging:
	private static final String TAG = "DBAdapter";
	public static final String DATABASE_NAME = "MC2DB_UGCB";
	public static final int DATABASE_VERSION = 1;	
	private final Context context;
	
	private DatabaseHelper myDBHelper;
	private SQLiteDatabase db;

	/////////////////////////////////////////////////////////////////////
	//	Public methods:
	/////////////////////////////////////////////////////////////////////
	public boolean doesDatabaseExist(ContextWrapper context) {
	    File dbFile = context.getDatabasePath(DATABASE_NAME);
	    return dbFile.exists();
	}

	public void Convert00()
	{
		execSQL("UPDATE tblUserCardData SET GCISubType = 'Non-Reloadable' WHERE GCISubType = 'Gift Card'");
		execSQL("UPDATE tblUserCardData SET GCISubType = 'Reloadable' WHERE GCISubType = 'Prepaid Credit'");
		execSQL("UPDATE tblUserCardData SET GCISubType = 'Reloadable' WHERE GCISubType = 'Prepaid Debit'");
		execSQL("UPDATE tblCardInfo SET CardSubType = 'Non-Reloadable' WHERE CardSubType = 'Gift Card'");
		execSQL("UPDATE tblCardInfo SET CardSubType = 'Reloadable' WHERE CardSubType = 'Prepaid Debit'");
		execSQL("UPDATE tblCardInfo SET CardSubType = 'Reloadable' WHERE CardSubType = 'Prepaid Credit'");
	}

	public void deleteDB(Context ctx) {
		context.deleteDatabase(DATABASE_NAME);
		}

	public DBAdapter(Context ctx) {
		this.context = ctx;
		myDBHelper = new DatabaseHelper(context);
	}
	
	// Open the database connection.
	public DBAdapter open() {
		db = myDBHelper.getWritableDatabase();
		return this;
	}
	
	// Close the database connection.
	public void close() {
		myDBHelper.close();
	}
	public String execSQL(String pQueryIn) {
		String retVal="1";
		try {
			db.execSQL(pQueryIn);			
		} catch (Exception e) {
			retVal=e.getMessage();
		}
		return retVal;
	}
	public String getSingleValAsString(String pQueryIn) {
		String retVal = "";
		boolean empty = false;
		Cursor c = null;
		try {
			c = db.rawQuery(pQueryIn, null);
		} catch (Exception e) {
			Log.w(TAG, "getSingleValAsString SQL error: " + e.getMessage());
			return e.getMessage();
		}
		if (c == null) empty = true;
		if (c.getCount() == 0) empty = true;
		if (empty == false) {
			c.moveToFirst();
			try {
				retVal = c.getString(0);
			} catch (Exception e) {
				Log.w(TAG, "getSingleValAsString Data Retrieval error: " + e.getMessage());
			}
		}
		return retVal;
	}

	public String getAllRowsAsString(String pQueryIn) {
		String retVal="";
		String pPieceDelim="p#d";
		String pLineDelim="l#d";
		String temp="";
		String tempLine="";
		String tempAllData="";
		boolean empty=false;
		int CurrRecCount=0;

		Cursor c=null;
		try {
			c = db.rawQuery(pQueryIn,null);
		} catch (Exception e) {
			Log.w(TAG, "getAllRowsAsString error: " + e.getMessage());
			return e.getMessage();
		}
		if (c == null) empty=true;
		if (c.getCount()==0) empty=true;
		if (empty==false) {
			c.moveToFirst();
			int cc=c.getColumnCount();
			do {
				for (int i = 0; i < cc; i++) {
					try {
						temp=c.getString(i);						
					} catch (Exception e) {
						Log.w(TAG, "getAllRowsAsString error: " + e.getMessage());
					}
					if (cc==1)
					{
						//there's just one column, so no need for a pieceDel
						tempLine=tempLine+temp;
					}
					else if (cc>1)
					{
						tempLine=tempLine+temp+pPieceDelim;						
					}
				}
				tempAllData=tempAllData+tempLine+pLineDelim;
				tempLine="";
				CurrRecCount++;
			} while(c.moveToNext());
		}
		retVal=tempAllData;
		return retVal;
	}


	/////////////////////////////////////////////////////////////////////
	//	Private Helper Classes:
	/////////////////////////////////////////////////////////////////////
	
	/**
	 * Private class which handles database creation and upgrading.
	 * Used to handle low-level database access.
	 */
	private static class DatabaseHelper extends SQLiteOpenHelper
	{
		DatabaseHelper(Context context) {
			super(context, DATABASE_NAME, null, DATABASE_VERSION);
		}

		@Override
		public void onCreate(SQLiteDatabase _db) {
			String DATABASE_CREATE_SQL="";
			DATABASE_CREATE_SQL = 
					"create table tblUserCardData (UCDID integer primary key, "
					+ "UCDCardType text not null, UCDURL text, "
					+ "UCDCardNumber text, UCDCardPIN text, UCDLogin text, UCDPassword text, UCDNotes text, UCDDateLogged text);";
			_db.execSQL(DATABASE_CREATE_SQL);
			DATABASE_CREATE_SQL = 
					"create table tblCardInfo (CIID integer primary key, "
					+ "CICardType text not null, "
					+ "CICategory text, CIPhone text, CIURL text, CIShowLP text, CIMDBID text not null);";
			_db.execSQL(DATABASE_CREATE_SQL);
			DATABASE_CREATE_SQL = 
					"create table tblBalanceHistory (BHID integer primary key, "
					+ "BHCardNumber text not null, BHLastKnownBalance text, BHLastKnownBalanceDate text);";
			_db.execSQL(DATABASE_CREATE_SQL);
			DATABASE_CREATE_SQL = 
					"CREATE VIEW qryBalanceHistory01 AS SELECT BHCardNumber, Max(BHLastKnownBalanceDate) AS MaxOfLastKnownBalanceDate FROM tblBalanceHistory GROUP BY BHCardNumber;";
			_db.execSQL(DATABASE_CREATE_SQL);

			DATABASE_CREATE_SQL = 
					"CREATE VIEW qryBalanceHistory AS SELECT * FROM qryBalanceHistory01 INNER JOIN tblBalanceHistory ON (qryBalanceHistory01.MaxOfLastKnownBalanceDate = tblBalanceHistory.BHLastKnownBalanceDate) AND (qryBalanceHistory01.BHCardNumber = tblBalanceHistory.BHCardNumber);";
			_db.execSQL(DATABASE_CREATE_SQL);
				
			DATABASE_CREATE_SQL =
					"CREATE VIEW qryUserCardData AS SELECT DISTINCT UCDID, UCDCardType, UCDCardNumber, UCDCardPIN, UCDLogin, qryBalanceHistory.BHLastKnownBalance, qryBalanceHistory.BHLastKnownBalanceDate FROM tblUserCardData LEFT JOIN qryBalanceHistory ON tblUserCardData.UCDCardNumber = qryBalanceHistory.BHCardNumber;";
			_db.execSQL(DATABASE_CREATE_SQL);

		}

		@Override
		public void onUpgrade(SQLiteDatabase _db, int oldVersion, int newVersion) {
			Log.w(TAG, "Upgrading application's database from version " + oldVersion
					+ " to " + newVersion + ", which will destroy all old data!");
			
			// Destroy old database:
			String DATABASE_TABLE="";
			_db.execSQL("DROP TABLE IF EXISTS " + DATABASE_TABLE);
			
			// Recreate new database:
			onCreate(_db);
		}

	}
}
