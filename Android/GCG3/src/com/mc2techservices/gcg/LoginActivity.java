package com.mc2techservices.gcg;
import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.io.Writer;
import java.net.URL;
import android.content.Intent;
import java.net.URLConnection;

import javax.net.ssl.HttpsURLConnection;


import android.app.Activity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.ActivityInfo;
import android.net.wifi.WifiManager;
import android.os.AsyncTask;
import android.os.Bundle;
import android.text.format.Formatter;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;

import com.mc2techservices.common.GeneralFunctions;
import com.mc2techservices.gcg.R;

public class LoginActivity extends Activity {
	String pFinalResult="";
	EditText txtUsername;
	EditText txtPassword;
	Context context;
	ProgressDialog progress;
	protected void onCreate(Bundle savedInstanceState) {
		context = this;
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_login);
		
		
		if (GlobalClass.gloGoToPage.equals("Purchase")) {
			Intent intent = new Intent(context, PurchaseActivity.class);
			startActivity(intent);
			finish();
		}
		
		txtUsername=(EditText) findViewById(R.id.etUsername);
		txtPassword=(EditText) findViewById(R.id.etPassword);
		CheckBox chkAutoLogin= (CheckBox) findViewById(R.id.chkAutoLogin);
		String pUsername = AppSpecificFunctions.ReadSharedPreference(context, "Username");
		String pPassword = AppSpecificFunctions.ReadSharedPreference(context, "Password");
		
		
		if (!pUsername.equals("<No Value Defined>"))
		{
			txtUsername.setText(pUsername);
		}
		if (!pPassword.equals("<No Value Defined>"))
		{
			txtPassword.setText(pPassword);
		}
		if (AppSpecificFunctions.ReadSharedPreference(context, "AutoStart").equals("1"))
		{
			chkAutoLogin.setChecked(true);
			if (AppSpecificFunctions.ReadSharedPreference(context, "DoAutoStart").equals("1"))
			{
        		AppSpecificFunctions.WriteSharedPreference(context, "DoAutoStart", "0");
				DoLogin();				
			}
		}
		getWindow().setSoftInputMode( WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN );
		if (GlobalClass.gloRegistered.equals("1")) {
			Button cmdRegistered= (Button) findViewById(R.id.cmdRegister);
			//Actually, never hide this, no point
			//cmdRegistered.setAlpha(0);
		}
	}        

	public void onRegisterClick(View arg0)
	{
		Intent intent = new Intent(context, RegisterActivity.class);
		startActivity(intent);
	}


	private String validateInfo()
	{
		String retVal="";
		String pTempPassword=txtPassword.getText().toString().trim();
		String pTempPasswordVer=txtPassword.getText().toString().trim();
		String pTempLogin=txtUsername.getText().toString().trim();
		retVal=AppSpecificFunctions.PMValidateInfo(pTempPassword,pTempPasswordVer,pTempLogin);
		return retVal;
	}
	public void onLoginClick(View arg0)
	{
		try {
			DoLogin();
		} catch (Exception e) {
			GeneralFunctions.cgfAlert(context, "Sorry, an error has occured");
		}
	}

	public void onDemoClick(View arg0)
	{
        progress = new ProgressDialog(this);
        progress.setTitle("Login");
        progress.setMessage("Performing Login...");
        progress.show();
		WifiManager wm = (WifiManager) getSystemService(WIFI_SERVICE);
		String ip = "1.2.3.4";
		try {
			ip=Formatter.formatIpAddress(wm.getConnectionInfo().getIpAddress());
		} catch (Exception e) {
		}
		String pURL="https://gcg.mc2techservices.com/GCGWebWS.asmx/DemoGCG";
		AsyncWebCallRunner runner = new AsyncWebCallRunner();
		runner.execute("https://gcg.mc2techservices.com/GCGWebWS.asmx/DemoGCG","pIP="+ip);
	}

	private void GoToWebformActivity()
	{
		Intent intent = new Intent(context, WebformActivity.class);
		startActivity(intent);		
	}
	private void ReportError(String error)
	{
		GeneralFunctions.cgfAlert(this, error);
	}
	
	public void onAutoLoginClick(View arg0) {
		CheckBox chkAutoLogin= (CheckBox) findViewById(R.id.chkAutoLogin);
		if (chkAutoLogin.isChecked())
		{
    		AppSpecificFunctions.WriteSharedPreference(context, "AutoStart", "1");
    		EditText txtUsername=(EditText) findViewById(R.id.etUsername);
    		EditText txtPassword=(EditText) findViewById(R.id.etPassword);
    		AppSpecificFunctions.WriteSharedPreference(context, "Username", txtUsername.getText().toString());
    		AppSpecificFunctions.WriteSharedPreference(context, "Password", txtPassword.getText().toString());
		}
		else
		{
    		AppSpecificFunctions.WriteSharedPreference(context, "AutoStart", "0");
    		AppSpecificFunctions.WriteSharedPreference(context, "Username", "");
    		AppSpecificFunctions.WriteSharedPreference(context, "Password", "");
		}
	}
	
	public void onPurchaseClick(View arg0) {
        Log.d("GCGX", "Login button clicked.");
		Intent intent = new Intent(context, ConfirmLoginActivity.class);
		startActivity(intent);
    }	
	
	private String BuildParamString()
	{
		String retVal="";
		String zUsername = ((EditText)findViewById(R.id.etUsername)).getText().toString();
		String zPassword = ((EditText)findViewById(R.id.etPassword)).getText().toString();
		retVal="pGCGLogin=" + zUsername + "&pGCGPassword=" + zPassword;
		return retVal;
	}

	private void DoLogin()
	{
		String temp=validateInfo();
		if (!temp.equals("")) 
		{
			GeneralFunctions.cgfAlert(this, temp);
			return;
		}
        progress = new ProgressDialog(this);
        progress.setTitle("Login");
        progress.setMessage("Performing Login...");
        progress.show();
		String pParams=BuildParamString();
		String pURL="https://gcg.mc2techservices.com/GCGWebWS.asmx/GCGLogin";
		AsyncWebCallRunner runner = new AsyncWebCallRunner();
		runner.execute(pURL, pParams); //"pGCGLogin=cjm&pGCGPassword=cjm"
	}


	public class AsyncWebCallRunner extends AsyncTask<String, String, String> {
		
		private String resp;
		private String USER_AGENT = "Mozilla/5.0";
		@Override
		protected String doInBackground(String... params) {
			publishProgress("Sleeping..."); // Calls onProgressUpdate()
			try {
				try {
						String url = params[0];
						String urlParameters = params[1];

						URL obj = new URL(url);
						HttpsURLConnection con = (HttpsURLConnection) obj.openConnection();
				 
						con.setRequestMethod("POST");
						con.setRequestProperty("User-Agent", USER_AGENT);
						con.setRequestProperty("Accept-Language", "en-US,en;q=0.5");
						con.setDoOutput(true);
						DataOutputStream wr = new DataOutputStream(con.getOutputStream());
						wr.writeBytes(urlParameters);
						wr.flush();
						wr.close();
				 
						int responseCode = con.getResponseCode();

						Log.d("WS Call Detail", "\nSending 'POST' request to URL : " + url);
						Log.d("WS Call Detail","Post parameters : " + urlParameters);
						Log.d("WS Call Detail","Response Code : " + responseCode);
				 
						BufferedReader in = new BufferedReader(
						        new InputStreamReader(con.getInputStream()));
						String inputLine;
						StringBuffer response = new StringBuffer();
				 
						while ((inputLine = in.readLine()) != null) {
							response.append(inputLine);
						}
						in.close();
				 
						//print result
						System.out.println(response.toString());
						Log.d("GCG Good Result", response.toString());
						resp=response.toString();
				}
				catch (Exception e)
				{
					Log.d("GCG", "e1");
					resp="CallWebService Exception Occured 1";
					//invalid params to webservice, wifi not up...
				}
			}
			catch (Exception e) {
				Log.d("GCG", "e2");
				resp="CallWebService Exception Occured 2";
			}
			return resp;
		}

		@Override
		protected void onPostExecute(String result) {
			Log.d("GCG onPostExecute", result);
	        progress.dismiss();
			String Response = AppSpecificFunctions.GetResonseData(result); // "http://barcodefastpass.mc2techservices.com/Barcodes/9876543321.jpg";
			Log.d("barcodefastpass", "onPostExecute Response 1:" + Response);
			if (Response.length()==15) {
				GlobalClass.gloLoggedInAs=Response;
				GlobalClass.gloLaunchURL="https://gcg.mc2techservices.com/GCGWeb.htm?Session=" + Response;
				GoToWebformActivity();
			}
			else if (result.equals("CallWebService Exception Occured 1"))
			{
				ReportError("Are you sure your online?  GCG is showing there's no connection.  Please check and try again.");

			}
			else if (Response.equals("1234567890"))
			{
				ReportError("Couldn't log in, seems there's a problem with the username/password.");				
			}
			else
			{
				ReportError("Something went wrong with the login, please try again later.");				
			}
		}

		@Override
		protected void onPreExecute() {
			Log.d("GCG", "GCG onPostExecute");
			// Things to be done before execution of long running operation. For
			// example showing ProgessDialog
		}

		@Override
		protected void onProgressUpdate(String... text) {
			Log.d("GCG", "onProgressUpdate");
			//finalResult.setText(text[0]);
			// Things to be done while execution of long running operation is in
			// progress. For example updating ProgessDialog
		}
	}
}
