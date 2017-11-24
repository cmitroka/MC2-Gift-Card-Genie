package com.mc2techservices.gcbg;

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
import com.mc2techservices.gcbg.R;
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

public class InitActivity extends Activity {
	private static final String TAG = "gcgx";
	int intLaunches;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_init);		
		Log.d("InitActivity", "");

		GlobalClass.gloGoToPage="";
		GlobalClass.gloDelim="~_~";
		GlobalClass.gloxmlns= "xmlns=\"gcg.mc2techservices.com\">";
		GlobalClass.gloWebServiceURL="https://gcg.mc2techservices.com/GCGWebWS.asmx";
		GlobalClass.gloWebURL="https://gcg.mc2techservices.com/";
		//GlobalClass.gloWebServiceURL="http://192.168.0.186/AdminSite/GCGWebWS.asmx";
		//GlobalClass.gloWebURL="http://192.168.0.186/AdminSite/";
		//GlobalClass.gloWebServiceURL="http://10.76.186.221/AdminSite/GCGWebWS.asmx";
		//GlobalClass.gloWebURL="http://10.76.186.221/AdminSite/";
				
		
		
		String strTemp=GeneralFunctions.cgfNVDtoEmpty(GeneralFunctions.cgfReadSharedPreference(this, "Purchased"));
		GlobalClass.gloPurchased=strTemp;
		
		strTemp=GeneralFunctions.cgfNVDtoEmpty(GeneralFunctions.cgfReadSharedPreference(this, "Registered"));
		GlobalClass.gloRegistered=strTemp;
		
		Intent intent = new Intent(this, LoginActivity.class);
		startActivity(intent);										
    }
}