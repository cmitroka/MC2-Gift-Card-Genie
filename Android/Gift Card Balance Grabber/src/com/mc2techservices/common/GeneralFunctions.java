package com.mc2techservices.common;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.UUID;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.BroadcastReceiver;
import android.content.ComponentName;
import android.content.ContentResolver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.IntentSender;
import android.content.ServiceConnection;
import android.content.SharedPreferences;
import android.content.IntentSender.SendIntentException;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.content.res.AssetManager;
import android.content.res.Configuration;
import android.content.res.Resources;
import android.content.res.Resources.Theme;
import android.database.DatabaseErrorHandler;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteDatabase.CursorFactory;
import android.graphics.Bitmap;
import android.graphics.drawable.Drawable;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.os.UserHandle;
import android.util.Log;
import android.view.Display;

public class GeneralFunctions {

    public static void cgfWriteSharedPreference(Context ctxt, String PrefName, String ValToWrite)
	{
    	String packagename=ctxt.getApplicationContext().getPackageName();
    	SharedPreferences prefs = ctxt.getSharedPreferences(
    			packagename, Context.MODE_PRIVATE);
		prefs.edit().putString(PrefName, ValToWrite).commit();
	}
    public static String cgfReadSharedPreference(Context ctxt, String PrefName)
	{
    	String packagename=ctxt.getApplicationContext().getPackageName();
    	String retVal="<No Value Defined>";
		SharedPreferences prefs = ctxt.getSharedPreferences(
				packagename, Context.MODE_PRIVATE);

		retVal = prefs.getString(PrefName, retVal);
		return retVal;
	}
    public static String cgfNVDtoEmpty(String PossibleNVD)
	{
    	if (PossibleNVD.equals("<No Value Defined>")) {
			return "";
		}
    	else
    	{
    		return PossibleNVD;
    	}
	}
    public static void cgfAlert(Context pContext, String pMessage) 
    {
    	AlertDialog.Builder builder = new AlertDialog.Builder(pContext);
    	builder.setTitle("Message")
    	    .setMessage(pMessage)
    	    .setNeutralButton("OK", null);
    	AlertDialog dialog = builder.create();
    	dialog.show();
    }

	public static String cgfGetVersion(Context pContext) {
	    String vn = "";
	    int vc = 0;
	    try {
	        vn = pContext.getPackageManager().getPackageInfo(pContext.getPackageName(), 0).versionName;
	        vc = pContext.getPackageManager().getPackageInfo(pContext.getPackageName(), 0).versionCode;
	    } catch (Exception e) {
	        // Huh? Really?
	    }
	    String temp=vn + "." + String.valueOf(vc);
	    return temp;
	}

    public static String cgfCreateUUID()
    {
        UUID uuid = UUID.randomUUID();
        String randomUUIDString = uuid.toString();
        return randomUUIDString;
    }

	public static String cgfPurchaseOverride(String ValIn) {
		int total=0;
		for (int i = 0; i < ValIn.length(); i++) {
			char mychar = ValIn.charAt(i); //char="a"
			int ascii = (int)mychar;
			int addval=GetAdjustedValFromASCII(ascii);
			total=total+addval;
		}
		return String.valueOf(total);
	}
    private static int GetAdjustedValFromASCII(int ASCIIin)
    {
        //0=48...9=57
        //A=65...H=72
        //a=97...h=104
        int ASCIItoRet = 0;
        if (ASCIIin == 48) { ASCIItoRet = ASCIIin + 6; }
        else if (ASCIIin == 49) { ASCIItoRet = ASCIIin + 63; }
        else if (ASCIIin == 50) { ASCIItoRet = ASCIIin + 21; }
        else if (ASCIIin == 51) { ASCIItoRet = ASCIIin + 97; }
        else if (ASCIIin == 52) { ASCIItoRet = ASCIIin + 43; }
        else if (ASCIIin == 53) { ASCIItoRet = ASCIIin + 31; }
        else if (ASCIIin == 54) { ASCIItoRet = ASCIIin + 88; }
        else if (ASCIIin == 55) { ASCIItoRet = ASCIIin + 55; }
        else if (ASCIIin == 56) { ASCIItoRet = ASCIIin + 14; }
        else if (ASCIIin == 57) { ASCIItoRet = ASCIIin + 3; }
        else if (ASCIIin == 65) { ASCIItoRet = ASCIIin + 70; }
        else if (ASCIIin == 66) { ASCIItoRet = ASCIIin + 19; }
        else if (ASCIIin == 67) { ASCIItoRet = ASCIIin + 76; }
        else if (ASCIIin == 68) { ASCIItoRet = ASCIIin + 43; }
        else if (ASCIIin == 69) { ASCIItoRet = ASCIIin + 32; }
        else if (ASCIIin == 70) { ASCIItoRet = ASCIIin + 93; }
        else if (ASCIIin == 71) { ASCIItoRet = ASCIIin + 82; }
        else if (ASCIIin == 72) { ASCIItoRet = ASCIIin + 65; }
        else if (ASCIIin == 97) { ASCIItoRet = ASCIIin + 14; }
        else if (ASCIIin == 97) { ASCIItoRet = ASCIIin + 73; }
        else if (ASCIIin == 97) { ASCIItoRet = ASCIIin + 92; }
        else if (ASCIIin == 100) { ASCIItoRet = ASCIIin + 32; }
        else if (ASCIIin == 101) { ASCIItoRet = ASCIIin + 53; }
        else if (ASCIIin == 102) { ASCIItoRet = ASCIIin + 44; }
        else if (ASCIIin == 103) { ASCIItoRet = ASCIIin + 73; }
        else if (ASCIIin == 104) { ASCIItoRet = ASCIIin + 69; }
        return ASCIItoRet;
    }
}
