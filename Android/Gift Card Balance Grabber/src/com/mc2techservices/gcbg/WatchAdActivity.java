package com.mc2techservices.gcbg;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.Timer;
import java.util.TimerTask;

import com.google.android.gms.ads.AdListener;
import com.google.android.gms.ads.AdRequest;
import com.google.android.gms.ads.InterstitialAd;
import com.mc2techservices.gcbg.ConfirmLoginActivity.AsyncWebCallRunner;

import android.app.Activity;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

public class WatchAdActivity extends Activity {
    InterstitialAd mInterstitialAd;
	Timer t=new Timer();
	int DotCounter;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_watch_ad);
        mInterstitialAd = new InterstitialAd(this);
        mInterstitialAd.setAdUnitId("ca-app-pub-2250341510214691/5929167165");
        
		t.scheduleAtFixedRate(new TimerTask() {
			public void run() {
				runOnUiThread(new Runnable() {
					public void run() {
						DotCounter++;
						Log.d("DotCounter: ", String.valueOf(DotCounter));
						if (DotCounter == 1) {
					        TextView tvLabel = (TextView) findViewById(R.id.textView1);
							tvLabel.setText(".");  
							//t.cancel();
						}
						else if (DotCounter == 2) {
					        TextView tvLabel = (TextView) findViewById(R.id.textView1);
							tvLabel.setText("..");  

						}
						else if (DotCounter == 3) {
					        TextView tvLabel = (TextView) findViewById(R.id.textView1);
							tvLabel.setText("...");  
							DotCounter =0;
						}
					}
				});
			}
		}, 0, 500); // 1000 means start delay (1 sec), and the second is the loop delay.

		requestNewInterstitial();
        //if (mInterstitialAd.isLoaded()) {
        //    mInterstitialAd.show();
        //}
        
        mInterstitialAd.setAdListener(new AdListener() {
            @Override
            public void onAdLoaded() {
	    		Log.d("APP", "Ad is loaded!");
	    		t.cancel();
                //Toast.makeText(getApplicationContext(), "Ad is loaded!", Toast.LENGTH_SHORT).show();
            	mInterstitialAd.show();
            }
 
            @Override
            public void onAdClosed() {
	    		Log.d("APP", "Ad is closed!");
                //requestNewInterstitial();
                //Toast.makeText(getApplicationContext(), "Ad is closed!", Toast.LENGTH_SHORT).show();
            }
 
            @Override
            public void onAdFailedToLoad(int errorCode) {
	    		Log.d("APP", "Ad failed to load! error code: " + errorCode);
                //Toast.makeText(getApplicationContext(), "Ad failed to load! error code: " + errorCode, Toast.LENGTH_SHORT).show();
            }
 
            @Override
            public void onAdLeftApplication() {
                //Toast.makeText(getApplicationContext(), "Ad left application!", Toast.LENGTH_SHORT).show();
	    		Log.d("APP", "Ad left application!");
	    		LogPurchase();
            }
 
            @Override
            public void onAdOpened() {
	    		Log.d("APP", "Ad is opened!");
                //Toast.makeText(getApplicationContext(), "Ad is opened!", Toast.LENGTH_SHORT).show();
            }
        });
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.watch_ad, menu);
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
	private void requestNewInterstitial() {
		AdRequest adRequest = new AdRequest.Builder()
		.addTestDevice("E0C6CEB3413BBA174F125A98F2ED7789")
		.addTestDevice(AdRequest.DEVICE_ID_EMULATOR)
		.build();

		mInterstitialAd.loadAd(adRequest);
	}
	private void LogPurchase()
	{
		GlobalClass.gloGoToPage="";
		String pKey = AppSpecificFunctions.PMMakeKey(GlobalClass.gloLoggedInAs);
		String pParams = "pGCGKey=" + GlobalClass.gloLoggedInAs + "&pPurchType=Interstitial&pKey="+ pKey + "&pChannel=Android";
		String pURL = GlobalClass.gloWebServiceURL + "/LogPurchase";
		//pGCGKey=GlobalClass.gloUUID pPurchType		pKey		pChannel
		AsyncWebCallRunner runner = new AsyncWebCallRunner();
		runner.execute(pURL, pParams);		
		//String ValidationCode=GlobalClass.gloUUID.substring(0,8);
		//String MagicNumber=com.mc2techservices.common.GeneralFunctions.cgfPurchaseOverride(ValidationCode); //GeneralFunctions.PMPurchaseOverride(ValidationCode);
		//GeneralFunctions.WriteSharedPreference(this, "Purchased", MagicNumber);
		finish();
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
