package com.mc2techservices.gcbg;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.Timer;
import java.util.TimerTask;

import com.mc2techservices.gcbg.RegisterActivity.AsyncWebCallRunner;
import com.android.vending.billing.util.IabHelper;
import com.android.vending.billing.util.IabResult;
import com.android.vending.billing.util.Inventory;
import com.android.vending.billing.util.Purchase;
import com.mc2techservices.gcbg.R;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Intent;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.Window;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class PurchaseActivity extends Activity {
	private static final String TAG = "barcodefastpass";
	private static final String base64EncodedPublicKey = 
            "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAuxfiDz2qW+nCrB4JHZWwsBNqu5LOwUi9TU86p9uectZu8U3eRNuCltzXDmqZcgNmqcSjwyuKloAWGk2RbHssJAQH3KRGGtkxHL0C+mgFh6nir2qHgIbgeuMsRuLb+w3dla1/+0DyWHyE10AMBKI3T9+TBzHszN+Yaug/k0ALWH4XxEo4DHNL0ms1CwwUCyOXM1XAOozA743yo0yoXpKaEyUosL9/DJZ50AkStv/EiRLEMS+HkuIWKTKDNQeuP8WetAiHzx0C1yArm7L6rMwXPKoz1PG41J8a97LcDKrPfrLS2YlwztIwWbqUWhtSkxXjZ7q3R+rU+Rj52gYPnEwaWQIDAQAB";
	//static final String ITEM_SKU = "purchase";
	static String ITEM_SKU;
    static final int RC_REQUEST = 10001;	
    IabHelper mHelper;
	boolean Ready=false;
	private int TimeCounter = 0;
	private Timer t;
	private String pOrderID;
	
	
	private void LogPurchase()
	{
        GlobalClass.gloGoToPage="";
		String pKey = AppSpecificFunctions.PMMakeKey(GlobalClass.gloLoggedInAs);
		String pParams = "pGCGKey=" + GlobalClass.gloLoggedInAs + "&pPurchType="+ GlobalClass.gloPurchaseType+"&pKey="+ pKey + "&pChannel=GCBG-"+pOrderID;
		String pURL = GlobalClass.gloWebServiceURL + "/LogPurchase";
					//pGCGKey=GlobalClass.gloUUID pPurchType		pKey		pChannel
		AsyncWebCallRunner runner = new AsyncWebCallRunner();
		runner.execute(pURL, pParams);		
		//String ValidationCode=GlobalClass.gloUUID.substring(0,8);
	    //String MagicNumber=com.mc2techservices.common.GeneralFunctions.cgfPurchaseOverride(ValidationCode); //GeneralFunctions.PMPurchaseOverride(ValidationCode);
	    //GeneralFunctions.WriteSharedPreference(this, "Purchased", MagicNumber);
    	finish();
	}
	
	private void updateStatus(String newTextIn)
	{
		if (1==1) return;
		TextView textViewToUse=(TextView)findViewById(R.id.txtDefaultPurchaseStatus);
		String temp0=textViewToUse.getText().toString();
		int amnt=temp0.length();
		if (amnt>500)
		{
			amnt=500;
		}
		String finalText=temp0.substring(0,amnt);
		finalText=newTextIn + System.getProperty("line.separator") + finalText;
        textViewToUse.setText(finalText);
	}

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        this.requestWindowFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.activity_purchase);
        pOrderID="N/A";
        //Log.d("gloLoggedInAs", GlobalClass.gloLoggedInAs);
    	Log.d("PurchaseActivity", "");
		if (GlobalClass.gloPurchaseType.equals("15"))
		{
			ITEM_SKU = "gcbg15for1dollar";
		}
		else if (GlobalClass.gloPurchaseType.equals("999"))
		{
			ITEM_SKU = "unlimitedor3dollars";
		}	

		if (GlobalClass.gloPurchaseType.equals("override"))
		{
			GlobalClass.gloPurchaseType="999";
			pOrderID="Override";					
			LogPurchase();
		}
        SetupIAP();
    }
    private void SetupIAP()
    {
        updateStatus("SetupIAP()");
    	mHelper = new IabHelper(this, base64EncodedPublicKey);
    	mHelper.enableDebugLogging(true);
    	mHelper.startSetup(new IabHelper.OnIabSetupFinishedListener() {
    	public void onIabSetupFinished(IabResult result) {
	    	if (!result.isSuccess()) {
	            updateStatus("Problem setting up in-app billing: " + result);
	        	//return;
	        }
	    	if (mHelper == null)
	    	{
	            updateStatus("mHelper == null");    		
	        	//return;
	    	}
        updateStatus("Setup successful. Querying inventory.");
    	mHelper.queryInventoryAsync(mGotInventoryListener);
    	}
    	});
    }
    
    IabHelper.QueryInventoryFinishedListener mGotInventoryListener = new IabHelper.QueryInventoryFinishedListener() {
    	@Override
    	public void onQueryInventoryFinished(final IabResult result, final Inventory inventory) {
	    	if (mHelper == null) {
	    		return;
	    	}
			if (result.isFailure()) {
	            updateStatus("Failed to query inventory: " + result);
				return;
			}
			// They've bought the  Consumable
			Purchase ConsumablePurchase = inventory.getPurchase(ITEM_SKU);
			if (ConsumablePurchase == null) {
	            updateStatus("We've got to buy it");
				LaunchPurchase();
			}
			else
			{
	            updateStatus("Consuming ConsumablePurchase.");
				mHelper.consumeAsync(inventory.getPurchase(ITEM_SKU),
				mConsumeFinishedListener);
			}
    	}
    };
   
    private void LaunchPurchase()
    {
        updateStatus("LaunchPurchase() Started");
		mHelper.launchPurchaseFlow(this, ITEM_SKU, RC_REQUEST,
                mPurchaseFinishedListener, "");
    }
    
    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        Log.d(TAG, "onActivityResult(" + requestCode + "," + resultCode + "," + data);
        updateStatus("onActivityResult eval");
        if (mHelper == null) return;

        // Pass on the activity result to the helper for handling
        if (!mHelper.handleActivityResult(requestCode, resultCode, data)) {
            updateStatus("onActivityResult 1");

            // not handled, so handle it ourselves (here's where you'd
            // perform any handling of activity results not related to in-app
            // billing...
            super.onActivityResult(requestCode, resultCode, data);
        }
        else {
            updateStatus("onActivityResult 2");
            Log.d(TAG, "onActivityResult handled by IABUtil.");
        }
        updateStatus("onActivityResult Done");
    }

    /** Verifies the developer payload of a purchase. */
    boolean verifyDeveloperPayload(Purchase p) {
        String payload = p.getDeveloperPayload();

        /*
         * TODO: verify that the developer payload of the purchase is correct. It will be
         * the same one that you sent when initiating the purchase.
         *
         * WARNING: Locally generating a random string when starting a purchase and
         * verifying it here might seem like a good approach, but this will fail in the
         * case where the user purchases an item on one device and then uses your app on
         * a different device, because on the other device you will not have access to the
         * random string you originally generated.
         *
         * So a good developer payload has these characteristics:
         *
         * 1. If two different users purchase an item, the payload is different between them,
         *    so that one user's purchase can't be replayed to another user.
         *
         * 2. The payload must be such that you can verify it even when the app wasn't the
         *    one who initiated the purchase flow (so that items purchased by the user on
         *    one device work on other devices owned by the user).
         *
         * Using your own server to store and verify developer payloads across app
         * installations is recommended.
         */

        return true;
    }

    // Callback for when a purchase is finished
    IabHelper.OnIabPurchaseFinishedListener mPurchaseFinishedListener = new IabHelper.OnIabPurchaseFinishedListener() {
        public void onIabPurchaseFinished(IabResult result, Purchase purchase) {
            updateStatus("Purchase eval: " + result + ", purchase: " + purchase);

            // if we were disposed of in the meantime, quit.
            if (mHelper == null) return;

            if (result.isFailure()) {
                updateStatus("Purchase fail 1");
                complain("Error purchasing: " + result);
                return;
            }
            if (!verifyDeveloperPayload(purchase)) {
                updateStatus("Purchase fail 2");
                complain("Error purchasing. Authenticity verification failed.");
                return;
            }

            updateStatus("Purchase successful");
            if (purchase.getSku().equals(ITEM_SKU)) {
                updateStatus("Now we have to consume it");
                mHelper.consumeAsync(purchase, mConsumeFinishedListener);
            }
        }
    };

    // Called when consumption is complete
    IabHelper.OnConsumeFinishedListener mConsumeFinishedListener = new IabHelper.OnConsumeFinishedListener() {
        public void onConsumeFinished(Purchase purchase, IabResult result) {
            updateStatus("Beging Consumption");
            if (mHelper == null) return;
            if (result.isSuccess()) {
                updateStatus("Consumption OK");
                pOrderID=purchase.getOrderId();
                LogPurchase();
            }
            else {
                updateStatus("Consumption Failed");
                complain("Error while consuming: " + result);
            }
        }
    };

    // We're being destroyed. It's important to dispose of the helper here!
    @Override
    public void onDestroy() {
        updateStatus("Destory");
    	super.onDestroy();

        // very important:
        Log.d(TAG, "Destroying helper.");
        if (mHelper != null) {
            mHelper.dispose();
            mHelper = null;
        }
    }



    void complain(String message) {
        Log.e(TAG, "**** TrivialDrive Error: " + message);
        alert("Error: " + message);
    }

    void alert(String message) {
        AlertDialog.Builder bld = new AlertDialog.Builder(this);
        bld.setMessage(message);
        bld.setNeutralButton("OK", null);
        Log.d(TAG, "Showing alert dialog: " + message);
        bld.create().show();
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
				Log.d("AsyncWebCallRunner", "Error 002");
				Log.d("AsyncWebCallRunner", e.getMessage());
				resp = "CallWebService Exception Occured 2";
			}
			return resp;
		}
		@Override
		protected void onPostExecute(String result) {
			Log.d("AsyncWebCallRunner", "onPostExecute");
			String Response = AppSpecificFunctions.GetResonseData(result); // "http://barcodefastpass.mc2techservices.com/Barcodes/9876543321.jpg";
			Log.d("barcodefastpass", "onPostExecute Response 1:" + Response);
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
