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
    private static Context GetEmptyContext()
    {
    	Context c = new Context() {
			@Override
			public void unregisterReceiver(BroadcastReceiver receiver) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void unbindService(ServiceConnection conn) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public boolean stopService(Intent service) {
				// TODO Auto-generated method stub
				return false;
			}
			
			@Override
			public ComponentName startService(Intent service) {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public void startIntentSender(IntentSender intent, Intent fillInIntent,
					int flagsMask, int flagsValues, int extraFlags, Bundle options)
					throws SendIntentException {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void startIntentSender(IntentSender intent, Intent fillInIntent,
					int flagsMask, int flagsValues, int extraFlags)
					throws SendIntentException {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public boolean startInstrumentation(ComponentName className,
					String profileFile, Bundle arguments) {
				// TODO Auto-generated method stub
				return false;
			}
			
			@Override
			public void startActivity(Intent intent, Bundle options) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void startActivity(Intent intent) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void startActivities(Intent[] intents, Bundle options) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void startActivities(Intent[] intents) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			@Deprecated
			public void setWallpaper(InputStream data) throws IOException {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			@Deprecated
			public void setWallpaper(Bitmap bitmap) throws IOException {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void setTheme(int resid) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void sendStickyOrderedBroadcastAsUser(Intent intent,
					UserHandle user, BroadcastReceiver resultReceiver,
					Handler scheduler, int initialCode, String initialData,
					Bundle initialExtras) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void sendStickyOrderedBroadcast(Intent intent,
					BroadcastReceiver resultReceiver, Handler scheduler,
					int initialCode, String initialData, Bundle initialExtras) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void sendStickyBroadcastAsUser(Intent intent, UserHandle user) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void sendStickyBroadcast(Intent intent) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void sendOrderedBroadcastAsUser(Intent intent, UserHandle user,
					String receiverPermission, BroadcastReceiver resultReceiver,
					Handler scheduler, int initialCode, String initialData,
					Bundle initialExtras) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void sendOrderedBroadcast(Intent intent, String receiverPermission,
					BroadcastReceiver resultReceiver, Handler scheduler,
					int initialCode, String initialData, Bundle initialExtras) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void sendOrderedBroadcast(Intent intent, String receiverPermission) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void sendBroadcastAsUser(Intent intent, UserHandle user,
					String receiverPermission) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void sendBroadcastAsUser(Intent intent, UserHandle user) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void sendBroadcast(Intent intent, String receiverPermission) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void sendBroadcast(Intent intent) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void revokeUriPermission(Uri uri, int modeFlags) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void removeStickyBroadcastAsUser(Intent intent, UserHandle user) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void removeStickyBroadcast(Intent intent) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public Intent registerReceiver(BroadcastReceiver receiver,
					IntentFilter filter, String broadcastPermission, Handler scheduler) {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public Intent registerReceiver(BroadcastReceiver receiver,
					IntentFilter filter) {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			@Deprecated
			public Drawable peekWallpaper() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public SQLiteDatabase openOrCreateDatabase(String name, int mode,
					CursorFactory factory, DatabaseErrorHandler errorHandler) {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public SQLiteDatabase openOrCreateDatabase(String name, int mode,
					CursorFactory factory) {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public FileOutputStream openFileOutput(String name, int mode)
					throws FileNotFoundException {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public FileInputStream openFileInput(String name)
					throws FileNotFoundException {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public void grantUriPermission(String toPackage, Uri uri, int modeFlags) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			@Deprecated
			public int getWallpaperDesiredMinimumWidth() {
				// TODO Auto-generated method stub
				return 0;
			}
			
			@Override
			@Deprecated
			public int getWallpaperDesiredMinimumHeight() {
				// TODO Auto-generated method stub
				return 0;
			}
			
			@Override
			@Deprecated
			public Drawable getWallpaper() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public Theme getTheme() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public Object getSystemService(String name) {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public SharedPreferences getSharedPreferences(String name, int mode) {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public Resources getResources() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public String getPackageResourcePath() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public String getPackageName() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public PackageManager getPackageManager() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public String getPackageCodePath() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public File[] getObbDirs() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public File getObbDir() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public Looper getMainLooper() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public File getFilesDir() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public File getFileStreamPath(String name) {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public File[] getExternalFilesDirs(String type) {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public File getExternalFilesDir(String type) {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public File[] getExternalCacheDirs() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public File getExternalCacheDir() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public File getDir(String name, int mode) {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public File getDatabasePath(String name) {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public ContentResolver getContentResolver() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public ClassLoader getClassLoader() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public File getCacheDir() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public AssetManager getAssets() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public ApplicationInfo getApplicationInfo() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public Context getApplicationContext() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public String[] fileList() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public void enforceUriPermission(Uri uri, String readPermission,
					String writePermission, int pid, int uid, int modeFlags,
					String message) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void enforceUriPermission(Uri uri, int pid, int uid, int modeFlags,
					String message) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void enforcePermission(String permission, int pid, int uid,
					String message) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void enforceCallingUriPermission(Uri uri, int modeFlags,
					String message) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void enforceCallingPermission(String permission, String message) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void enforceCallingOrSelfUriPermission(Uri uri, int modeFlags,
					String message) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public void enforceCallingOrSelfPermission(String permission, String message) {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public boolean deleteFile(String name) {
				// TODO Auto-generated method stub
				return false;
			}
			
			@Override
			public boolean deleteDatabase(String name) {
				// TODO Auto-generated method stub
				return false;
			}
			
			@Override
			public String[] databaseList() {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public Context createPackageContext(String packageName, int flags)
					throws NameNotFoundException {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public Context createDisplayContext(Display display) {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			public Context createConfigurationContext(
					Configuration overrideConfiguration) {
				// TODO Auto-generated method stub
				return null;
			}
			
			@Override
			@Deprecated
			public void clearWallpaper() throws IOException {
				// TODO Auto-generated method stub
				
			}
			
			@Override
			public int checkUriPermission(Uri uri, String readPermission,
					String writePermission, int pid, int uid, int modeFlags) {
				// TODO Auto-generated method stub
				return 0;
			}
			
			@Override
			public int checkUriPermission(Uri uri, int pid, int uid, int modeFlags) {
				// TODO Auto-generated method stub
				return 0;
			}
			
			@Override
			public int checkPermission(String permission, int pid, int uid) {
				// TODO Auto-generated method stub
				return 0;
			}
			
			@Override
			public int checkCallingUriPermission(Uri uri, int modeFlags) {
				// TODO Auto-generated method stub
				return 0;
			}
			
			@Override
			public int checkCallingPermission(String permission) {
				// TODO Auto-generated method stub
				return 0;
			}
			
			@Override
			public int checkCallingOrSelfUriPermission(Uri uri, int modeFlags) {
				// TODO Auto-generated method stub
				return 0;
			}
			
			@Override
			public int checkCallingOrSelfPermission(String permission) {
				// TODO Auto-generated method stub
				return 0;
			}
			
			@Override
			public boolean bindService(Intent service, ServiceConnection conn, int flags) {
				// TODO Auto-generated method stub
				return false;
			}
		};
		return c;
    }
}
