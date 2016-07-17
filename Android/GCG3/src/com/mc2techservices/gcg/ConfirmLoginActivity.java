package com.mc2techservices.gcg;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

import com.mc2techservices.common.GeneralFunctions;
import com.mc2techservices.gcg.R;
import com.mc2techservices.gcg.PurchaseActivity.AsyncWebCallRunner;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.CheckBox;
import android.widget.EditText;

public class ConfirmLoginActivity extends Activity {
	ProgressDialog progress;
	private void Login()
	{
		EditText ptxtLogin=(EditText)findViewById(R.id.txtUsername);
		String pLogin=ptxtLogin.getText().toString();

		EditText ptxtPassword=(EditText)findViewById(R.id.txtPassword); 
		String pPassword=ptxtPassword.getText().toString();
	
		String temp=AppSpecificFunctions.PMValidateInfo(pPassword, pPassword, pLogin);
		if (!temp.equals("")) 
		{
			GeneralFunctions.cgfAlert(this, temp);
			return;
		}
        progress = new ProgressDialog(this);
        progress.setTitle("Login");
        progress.setMessage("Performing Login...");
        progress.show();
		String pParams = "pGCGLogin=" + pLogin + "&pGCGPassword="+ pPassword;
		//String pURL = "https://gcg.mc2techservices.com/GCGWebWS.asmx/HelloWorld";
		String pURL = "https://gcg.mc2techservices.com/GCGWebWS.asmx/GCGLogin";
		AsyncWebCallRunner runner = new AsyncWebCallRunner();
		runner.execute(pURL, pParams);		
	}
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_confirm_login);
		EditText txtUsername=(EditText) findViewById(R.id.txtUsername);
		EditText txtPassword=(EditText) findViewById(R.id.txtPassword);
		String pUsername = AppSpecificFunctions.ReadSharedPreference(this, "Username");
		String pPassword = AppSpecificFunctions.ReadSharedPreference(this, "Password");
		pUsername=GeneralFunctions.cgfNVDtoEmpty(pUsername);
		pPassword=GeneralFunctions.cgfNVDtoEmpty(pPassword);
		txtUsername.setText(pUsername);
		txtPassword.setText(pPassword);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.confirm_login, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.action_settings) {
			return true;
		}
		return super.onOptionsItemSelected(item);
	}
	
	public void onCLLoginClicked(View arg0) {
        Log.d("GCGX", "Login button clicked.");
        
		//Intent intent = new Intent(this, OverlayActivity.class);
		//startActivity(intent);
        
        Login();
    }	
	public void onCLCancelClicked(View arg0) {
        Log.d("GCGX", "Cancel button clicked.");
        finish();
    }	
	private void ProceedToPurchaseScreen()
	{
		Intent intent = new Intent(this, PurchaseOldActivity.class);
		startActivity(intent);
	}
	private void ReportError(String error)
	{
		GeneralFunctions.cgfAlert(this, error);
	}
		
	
	
	
	
	
	
	
	
	
	
	
	
    //The only time LogPurchase will actually be done from here is with an override...
    public class AsyncWebCallRunner extends AsyncTask<String, String, String> {

		private String resp;
		private String USER_AGENT = "Mozilla/5.0";
		@Override
		protected String doInBackground(String... params) {
			publishProgress("Sleeping..."); // Calls onProgressUpdate()
			try {
				String url = params[0];
				String urlParameters = params[1];
				URL obj = new URL(url);
				HttpURLConnection con = (HttpURLConnection) obj
						.openConnection();
				con.setRequestMethod("POST");
				con.setRequestProperty("User-Agent", USER_AGENT);
				con.setRequestProperty("Accept-Language", "en-US,en;q=0.5");
				con.setDoOutput(true);
				Log.d("AsyncWebCallRunner", "Ready to send...: " + url);

				try {
					DataOutputStream wr = new DataOutputStream(
							con.getOutputStream());
					wr.writeBytes(urlParameters);
					wr.flush();
					wr.close();
				} catch (Exception e) {
					Log.d("AsyncWebCallRunner", "Error 001");
					Log.d("AsyncWebCallRunner", e.getMessage());
					resp = "CallWebService Exception Occured 1";
				}

				int responseCode = con.getResponseCode();

				Log.d("AsyncWebCallRunner",
						"\nSending 'POST' request to URL : " + url);
				Log.d("AsyncWebCallRunner", "Post parameters : "
						+ urlParameters);
				Log.d("AsyncWebCallRunner", "Response Code : " + responseCode);

				BufferedReader in = new BufferedReader(new InputStreamReader(
						con.getInputStream()));
				String inputLine;
				StringBuffer response = new StringBuffer();

				while ((inputLine = in.readLine()) != null) {
					response.append(inputLine);
				}
				in.close();

				// print result
				System.out.println(response.toString());
				Log.d("AsyncWebCallRunner", response.toString());
				resp = response.toString();
			} catch (Exception e) {
				Log.d("AsyncWebCallRunner", e.getMessage());
				resp = "CallWebService Exception Occured 2";
			}
			return resp;
		}
		@Override
		protected void onPostExecute(String result) {
			String Response = AppSpecificFunctions.GetResonseData(result); // "http://barcodefastpass.mc2techservices.com/Barcodes/9876543321.jpg";
	        progress.dismiss();
			if (Response.length()==15) {
				GlobalClass.gloLoggedInAs=Response;
				ProceedToPurchaseScreen();
			}
			else if (result.equals("CallWebService Exception Occured 2"))
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
			Log.d("AsyncWebCallRunner", "onPreExecute");
		}

		@Override
		protected void onProgressUpdate(String... text) {
			Log.d("AsyncWebCallRunner", "onProgressUpdate");
		}
    }

}
