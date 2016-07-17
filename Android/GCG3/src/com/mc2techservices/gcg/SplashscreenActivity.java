package com.mc2techservices.gcg;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

import javax.net.ssl.HttpsURLConnection;

import com.android.vending.billing.util.IabHelper;
import com.android.vending.billing.util.IabResult;
import com.android.vending.billing.util.Inventory;
import com.android.vending.billing.util.Purchase;
import com.mc2techservices.gcg.R;
import com.mc2techservices.common.GeneralFunctions;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Environment;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.Button;
import android.widget.TextView;

public class SplashscreenActivity extends Activity {
	private static final String TAG = "gcgx";
	int intLaunches;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_splashscreen);		
		GeneralFunctions.cgfWriteSharedPreference(this, "A", "A");
		String strTestMode=GeneralFunctions.cgfReadSharedPreference(this, "A");
		GlobalClass.gloGoToPage="";
		GlobalClass.gloDelim="~_~";
		
		String strTemp=GeneralFunctions.cgfNVDtoEmpty(GeneralFunctions.cgfReadSharedPreference(this, "Purchased"));
		GlobalClass.gloPurchased=strTemp;
		
		strTemp=GeneralFunctions.cgfNVDtoEmpty(GeneralFunctions.cgfReadSharedPreference(this, "Registered"));
		GlobalClass.gloRegistered=strTemp;
		
        Log.d(TAG, "Starting setup.");
		String AutoStart=GeneralFunctions.cgfReadSharedPreference(this, "AutoStart");
		if (AutoStart.equals("1"))
		{
			DoAutostartUI();
		}
		else
		{
			DoMainUI();
		}
    }
	private void SetupParams()
	{
		//GeneralFunctions.WriteSharedPreference(this, "Launches", "0");
		String strLaunches = GeneralFunctions.cgfReadSharedPreference(this,"Launches");
		intLaunches=0;
		if (strLaunches.equals("<No Value Defined>")) {
			intLaunches =1;
			GeneralFunctions.cgfWriteSharedPreference(this, "Launches", "1");
		}
		else
		{
			intLaunches = Integer.parseInt(strLaunches);
			intLaunches++;
			GeneralFunctions.cgfWriteSharedPreference(this, "Launches", String.valueOf(intLaunches));
		}
		GlobalClass.gloLaunches=String.valueOf(intLaunches);
		
		GlobalClass.gloUUID = GeneralFunctions.cgfReadSharedPreference(this,
				"UUID");
		if (GlobalClass.gloUUID.equals("<No Value Defined>")) {
			GlobalClass.gloUUID = GeneralFunctions.cgfCreateUUID();
			GeneralFunctions.cgfWriteSharedPreference(this, "UUID",
					GlobalClass.gloUUID);
		}
		String ValidationCode=GlobalClass.gloUUID.substring(0,8);
		String MagicNumber=GeneralFunctions.cgfPurchaseOverride(ValidationCode);
		String CodeInSharedPreferences=GeneralFunctions.cgfReadSharedPreference(this, "Purchased");
		if (CodeInSharedPreferences.matches(MagicNumber)) {
			GlobalClass.gloPurchased = "1";
		}
		else
		{
			GlobalClass.gloPurchased = "0";    		
		}
		if (GlobalClass.gloFileLoc == null) {
			GlobalClass.gloRegistered = GeneralFunctions.cgfReadSharedPreference(
					this, "Registered");
			GlobalClass.gloVersion = GeneralFunctions.cgfGetVersion(this);
			// GeneralFunctions.DirectoryExists(Environment.getExternalStorageDirectory().toString());
			// REQUIRES ProxyAuthenticator.java
			// Authenticator.setDefault(new ProxyAuthenticator("chmitro",
			// "@Fiserv026"));
			// System.setProperty("http.proxyHost", "10.141.105.1");
			// System.setProperty("http.proxyPort", "9090");
		}
	
		
		String pParams = "pBCFPID=" + GlobalClass.gloUUID + "&pAmount=" + GlobalClass.gloLaunches;
		//String pURL = "http://barcodefastpass.mc2techservices.com/barcodefastpassWS.asmx/MakeBarcode";
	}
	private void DoAutostartUI()
	{
		Intent intent = new Intent(this, AutoStartActivity.class);
		startActivity(intent);										
	}
	private void DoMainUI()
	{
		Intent intent = new Intent(this, LoginActivity.class);
		startActivity(intent);										
	}	
}