package com.mc2techservices.gcbg;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStreamReader;
import java.net.URL;
import java.net.URLConnection;

import javax.net.ssl.HttpsURLConnection;

import com.mc2techservices.common.GeneralFunctions;
import com.mc2techservices.gcbg.GlobalClass;
import com.mc2techservices.gcbg.LoginActivity.AsyncWebCallRunner;
import com.mc2techservices.gcbg.R;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Environment;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;

public class RegisterActivity extends Activity {
	EditText pETLogin, pETPassword, pETPasswordVer, pETUsername, pETContact;
	Button bCancel;
	ProgressDialog progress;
	Button bRegister;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		final Context context = this;
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_register);
		Log.d("RegisterActivity", "");
		getWindow().setSoftInputMode( WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN );
		pETLogin=(EditText)findViewById(R.id.etUsername);
		pETPassword=(EditText)findViewById(R.id.etPassword);
		pETPasswordVer=(EditText)findViewById(R.id.etVerifyPassword);
		pETContact=(EditText)findViewById(R.id.etEmailAddress);
		pETUsername=(EditText)findViewById(R.id.etYourName);
	}
	public void onClickCancelRegister(View arg0)
	{
        finish();
	}
	public void onClickRegister(View arg0)
	{
		String Result="";
		String temp=validInfo();
		if (temp.equals(""))
		{
	        progress = new ProgressDialog(this);
	        progress.setTitle("Login");
	        progress.setMessage("Performing Login...");
	        progress.show();
			AsyncWebCallRunner runner = new AsyncWebCallRunner();
			String pLogin=pETLogin.getText().toString();
			String pPassword=pETPassword.getText().toString();
			String pUsername=pETUsername.getText().toString();
			String pContact=pETContact.getText().toString();
			String cmdLine="pGCGLogin=" + pLogin + "&pGCGPassword=" + pPassword + "&pUsersName=" + pUsername + "&pUsersEmail=" + pContact;
			runner.execute("https://gcg.mc2techservices.com/GCGWebWS.asmx/RegisterUserIns", cmdLine);
		}
		else
		{
			GeneralFunctions.cgfAlert(this, temp);
		}
	}
	private void RegisterIt()
	{
		GeneralFunctions.cgfAlert(this, "Congratulation, you've sucessfully registered!  You may now login.");		
		com.mc2techservices.common.GeneralFunctions.cgfWriteSharedPreference(this, "Registered", "1");
		GlobalClass.gloRegistered="1";
		finish();
	}
	private void ReportError(String error)
	{
		GeneralFunctions.cgfAlert(this, error);		
	}

	private String validInfo()
	{
		String retVal="";
		String pTempPassword=pETPassword.getText().toString().trim();
		String pTempPasswordVer=pETPasswordVer.getText().toString().trim();
		String pTempLogin=pETLogin.getText().toString().trim();
		retVal=AppSpecificFunctions.PMValidateInfo(pTempPassword,pTempPasswordVer,pTempLogin);
		return retVal;
	}
	
	public class AsyncWebCallRunner extends AsyncTask<String, String, String> {
		private String resp;
		private String USER_AGENT = "Mozilla/5.0";
		@Override
		protected String doInBackground(String... params) {
			publishProgress("Sleeping..."); // Calls onProgressUpdate()
			try {
				// Do your long operations here and return the result
				StringBuffer buffer=new StringBuffer();
				try {
					//String url = "https://selfsolve.apple.com/wcResults.do";
					//String urlParameters = "sn=C02G8416DRJM&cn=&locale=&caller=&num=12345";
					String url = params[0];
					String urlParameters = params[1];

					URL obj = new URL(url);
					HttpsURLConnection con = (HttpsURLConnection) obj.openConnection();

					//add reuqest header
					con.setRequestMethod("POST");
					con.setRequestProperty("User-Agent", USER_AGENT);
					con.setRequestProperty("Accept-Language", "en-US,en;q=0.5");
					// Send post request
					con.setDoOutput(true);
					DataOutputStream wr = new DataOutputStream(con.getOutputStream());
					wr.writeBytes(urlParameters);
					wr.flush();
					wr.close();

					int responseCode = con.getResponseCode();

					System.out.println("\nSending 'POST' request to URL : " + url);
					System.out.println("Post parameters : " + urlParameters);
					System.out.println("Response Code : " + responseCode);

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
					Log.d("GCG", response.toString());
					resp=response.toString();
				}
				catch (Exception e)
				{
					Log.d("GCG", "e1");
					resp="Error"; //CallWebService Exception Occured 1
				}
			}
			catch (Exception e) {
				Log.d("GCG", "e2");
				resp="Error";
			}
			return resp;
		}

		@Override
		protected void onPostExecute(String result) {
			Log.d("GCG", "onPostExecute");
			progress.dismiss();
			String Response = AppSpecificFunctions.GetResonseData(result); // "http://barcodefastpass.mc2techservices.com/Barcodes/9876543321.jpg";
			Log.d("barcodefastpass", "onPostExecute Response 1:" + Response);
			String temp=Response.substring(0,1);
			String[] temparr = Response.split(GlobalClass.gloDelim);

			if (temp.equals("1")) {
				RegisterIt();
			}
			else
			{
				ReportError(temparr[1]);
			}

		}

		@Override
		protected void onPreExecute() {
			Log.d("GCG", "onPostExecute");
		}

		@Override
		protected void onProgressUpdate(String... text) {
			Log.d("GCG", "onProgressUpdate");
		}
	}

}
