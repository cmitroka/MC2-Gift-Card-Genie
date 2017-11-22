package com.mc2techservices.gcg;
import android.content.Context;
import android.util.Log;



public class AppSpecific {
    public static String gloxmlns;
    public static String gloWebServiceURL;
    public static String gloWebURL;
    public static String gloPackageName;
    public static String gloKey;
	public static String gloPurchased;
	public static boolean gloFinishIt;

    public static String gloUUID;
    public static String gloShortUUID;
    public static String gloLD;
    public static String gloPD;
    public static String gloPurchaseType;

    public static String PMMakeKey(String pUUIDIn)
    {
    	String tempVal;
    	int iVal=0;
    	int totalVal=0;
    	int addVal=0;
    	for (int i = 0; i < pUUIDIn.length(); i++) {
    		tempVal=pUUIDIn.substring(i,i+1);
    		iVal=GeneralFunctions01.Conv.StringToInt(tempVal);
    		addVal=addVal+iVal;
    		addVal=addVal*2;
		}
    	totalVal=addVal+123;
    	tempVal=GeneralFunctions01.Conv.IntToString(totalVal);
    	return tempVal;
    }
    public static String PMRunQuery(String pQuery)
    {
	    Context pContext= gcg.getAppContext();
		DBAdapter myDb  = new DBAdapter(pContext);
		myDb.open();
		String AllSavedData=myDb.getAllRowsAsString(pQuery);
		myDb.close();
		return AllSavedData;
    }
    public static String PMMakeCardTitle(String pType, String pSubType, String pIssuedBy) 
    {
    	String RetVal="";
    	RetVal=pType+ " " + pSubType;
    	if (pIssuedBy.length()>0)
    	{
    		RetVal=RetVal + " by " + pIssuedBy;
    	}
    	return RetVal;
    }
    public static String[] PMGetUserCardData(int pIDIn) 
    {
	    Context pContext= gcg.getAppContext();
		DBAdapter myDb  = new DBAdapter(pContext);
		String AllSavedData=myDb.getAllRowsAsString("SELECT * FROM tblUserCardData WHERE _UCDID="+pIDIn);
		String[] lines=AllSavedData.split("l#d");
		String templine="";
		String rowBGColor="";
		for (int i = 0; i < lines.length; i++) {
			
		}
		myDb.close();
		return lines;
    }
	public static void PMLogPurchase(String pAmnt, String pOrderID)
	{
		String pKey = AppSpecific.PMMakeKey(AppSpecific.gloUUID);
		String pParams = "pGCGKey=" + AppSpecific.gloUUID + "&pPurchType=" + AppSpecific.gloPurchaseType + "&pKey="+ pKey + "&pChannel=ugcb_"+pOrderID;
		String pURL = AppSpecific.gloWebServiceURL + "/LogPurchase";
		GeneralFunctions01.Comm.NonAsyncWebCall(pURL, pParams);
	}
    public static void PMUpdateDBBalance(Context c, String CardNum, String NewBal)
    {
		String DateLogged=GeneralFunctions01.Dte.GetCurrentDateTime();
		DBAdapter myDb = new DBAdapter(c);
		myDb.open();
		String OK=myDb.execSQL("INSERT INTO tblBalanceHistory (CardNumber, LastKnownBalance, LastKnownBalanceDate) VALUES ('" + CardNum + "','" + NewBal + "','"+ DateLogged + "')");
		Log.d("APP", "OK: " + OK);		
		myDb.close();    	
    }
    public static String PMValidateInfo(String pPassword, String pPaswwordVer, String pLogin)
    {
    	String retVal="";
		if (pPassword.equals(pPaswwordVer)==false)
		{
			retVal=retVal+ "The password and verification password didn't match.\n";
		}
		if (pLogin.length()<4)
		{
			retVal=retVal+ "The username has to be at least 4 characters.\n";
		}
		if (pPassword.length()<4)
		{
			retVal=retVal+ "The password has to be at least 4 characters.\n";
		}
		if (retVal.length()>1) {
			retVal=retVal.substring(0,retVal.length()-1);
		}
    	return retVal;
    }
}
