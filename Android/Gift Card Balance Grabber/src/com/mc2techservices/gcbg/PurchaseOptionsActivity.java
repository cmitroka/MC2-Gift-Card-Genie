package com.mc2techservices.gcbg;

import com.android.vending.billing.util.*;
import com.mc2techservices.gcbg.R;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;

public class PurchaseOptionsActivity extends Activity {
	//private static final String NAMESPACE = "http://tempuri.org/";
	String pFinalResult="";
    IabHelper mHelper;
    boolean mIsPremium = false;
    static final String TAG = "GCG IAP";
    static final String SKU_15= "15for1dollar";
    static final String SKU_999 = "unlimitedor3dollars";
    static final int RC_REQUEST = 10001;

	protected void onCreate(Bundle savedInstanceState) {
		final Context context = this;
		//setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_UNSPECIFIED);
		//setRequestedOrientation (ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE);
		//setRequestedOrientation (ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_purchaseoptions);
		Log.d("PurchaseOptionsActivity", "");

        mHelper = new IabHelper(this, "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAiN6f1yPzXs+tag1L5HIJzF0ABsWBqhKeaJE0DQ/Nm3lyq+sTz2jRSPNh/fHAH7Rs6yTfT9zNo4+PPlKELwMgD+7lWVg8HN91yGQgM6zZ9dC2mW+3K8FX/43pypYpIi+bKv8mPrH/cONLh3tUApua3AqCR8P6hdDBIBTD0zav7hfsGtue++p5aG2strWiGdShmTFxcnz6lm99WA0XoJSPfhWLUawPt14lGGW18CFX+3GAHvosFoQNSM/kHzoIO8pIsuo4nCPjOufHpsiIP+dPfYN7bgPmx2Hj6M6sQPcfkOR2+0TE5KTbTI4nZTj6EPNBG4fHLesqkImPYywf/DQNTQIDAQAB");

        // enable debug logging (for a production application, you should set this to false).
        mHelper.enableDebugLogging(true);

        // Start setup. This is asynchronous and the specified listener
        // will be called once setup completes.
        Log.d(TAG, "Starting setup.");
        mHelper.startSetup(new IabHelper.OnIabSetupFinishedListener() {
            public void onIabSetupFinished(IabResult result) {
                Log.d(TAG, "Setup finished.");

                if (!result.isSuccess()) {
                    // Oh noes, there was a problem.
                    complain("Problem setting up in-app billing: " + result);
                    return;
                }

                // Have we been disposed of in the meantime? If so, quit.
                if (mHelper == null) return;

                // IAB is fully set up. Now, let's get an inventory of stuff we own.
                Log.d(TAG, "Setup successful. Querying inventory.");
                mHelper.queryInventoryAsync(mGotInventoryListener);
            }
        });
	}

	public void onWatchAdClicked(View arg0) {

			DialogInterface.OnClickListener dialogClickListener = new DialogInterface.OnClickListener() {
			    @Override
			    public void onClick(DialogInterface dialog, int which) {
			        switch (which){
			        case DialogInterface.BUTTON_POSITIVE:
			    		Log.d("APP", "Watch an ad.");
			    		WatchAd();
			    		break;

			        case DialogInterface.BUTTON_NEGATIVE:
			    		Log.d("APP", "Dont Delete");
			            break;
			        }
			    }
			};
			AlertDialog.Builder builder = new AlertDialog.Builder(this);
			builder.setMessage("You're going to have to actually click the ad for it to count; still good with this?").setPositiveButton("Yes", dialogClickListener)
			    .setNegativeButton("No", dialogClickListener).show();		
	}
	public void WatchAd() {
		Intent intent = new Intent(this, WatchAdActivity.class);
		startActivity(intent);
		return;
	}	
	
	public void onPurchaseInfClicked(View arg0) {
        Log.d(TAG, "Buy Infinte button clicked.");
        GlobalClass.gloPurchaseType="999";
        GlobalClass.gloGoToPage="Purchase";
		Intent intent = new Intent(this, PurchaseActivity.class);
		startActivity(intent);
	}	
	
	
	public void onPurchase15Clicked(View arg0) {
        Log.d(TAG, "Buy 15 button clicked.");
        GlobalClass.gloPurchaseType="15";
        GlobalClass.gloGoToPage="Purchase";
		Intent intent = new Intent(this, PurchaseActivity.class);
		startActivity(intent);
    }	
	public void onClickCancel(View arg0) {
		finish();
	}
    boolean verifyDeveloperPayload(Purchase p) {
        String payload = p.getDeveloperPayload();
        return true;
    }

    
    // Callback for when a purchase is finished
    IabHelper.OnIabPurchaseFinishedListener mPurchaseFinishedListener = new IabHelper.OnIabPurchaseFinishedListener() {
        public void onIabPurchaseFinished(IabResult result, Purchase purchase) {
            Log.d(TAG, "Purchase finished: " + result + ", purchase: " + purchase);

            // if we were disposed of in the meantime, quit.
            if (mHelper == null) return;

            if (result.isFailure()) {
                complain("Error purchasing: " + result);
                setWaitScreen(false);
                return;
            }
            if (!verifyDeveloperPayload(purchase)) {
                complain("Error purchasing. Authenticity verification failed.");
                setWaitScreen(false);
                return;
            }

            Log.d(TAG, "Purchase successful.");

            if (purchase.getSku().equals(SKU_15)) {
                // bought 1/4 tank of gas. So consume it.
                Log.d(TAG, "Purchase is gas. Starting gas consumption.");
                mHelper.consumeAsync(purchase, mConsumeFinishedListener);
            }
            else if (purchase.getSku().equals(SKU_999)) {
                // bought the premium upgrade!
                Log.d(TAG, "Purchase is premium upgrade. Congratulating user.");
                alert("Thank you for upgrading to premium!");
                setWaitScreen(false);
            }
        }
    };

    // Called when consumption is complete
    IabHelper.OnConsumeFinishedListener mConsumeFinishedListener = new IabHelper.OnConsumeFinishedListener() {
        public void onConsumeFinished(Purchase purchase, IabResult result) {
            Log.d(TAG, "Consumption finished. Purchase: " + purchase + ", result: " + result);

            // if we were disposed of in the meantime, quit.
            if (mHelper == null) return;

            // We know this is the "gas" sku because it's the only one we consume,
            // so we don't check which sku was consumed. If you have more than one
            // sku, you probably should check...
            if (result.isSuccess()) {
                // successfully consumed, so we apply the effects of the item in our
                // game world's logic, which in our case means filling the gas tank a bit
                Log.d(TAG, "Consumption successful. Provisioning.");
                //mTank = mTank == TANK_MAX ? TANK_MAX : mTank + 1;
                //saveData();
                //alert("You filled 1/4 tank. Your tank is now " + String.valueOf(mTank) + "/4 full!");
            }
            else {
                complain("Error while consuming: " + result);
            }
//            updateUi();
            setWaitScreen(false);
            Log.d(TAG, "End consumption flow.");
        }
    };

    
    IabHelper.QueryInventoryFinishedListener mGotInventoryListener = new IabHelper.QueryInventoryFinishedListener() {
        public void onQueryInventoryFinished(IabResult result, Inventory inventory) {
            Log.d(TAG, "Query inventory finished.");

            // Have we been disposed of in the meantime? If so, quit.
            if (mHelper == null) return;

            // Is it a failure?
            if (result.isFailure()) {
                complain("Failed to query inventory: " + result);
                return;
            }

            Log.d(TAG, "Query inventory was successful.");

            /*
             * Check for items we own. Notice that for each purchase, we check
             * the developer payload to see if it's correct! See
             * verifyDeveloperPayload().
             */

            // Do we have the premium upgrade?
            Purchase premiumPurchase = inventory.getPurchase(SKU_999);
            mIsPremium = (premiumPurchase != null && verifyDeveloperPayload(premiumPurchase));
            Log.d(TAG, "User is " + (mIsPremium ? "PREMIUM" : "NOT PREMIUM"));

            setWaitScreen(false);
            Log.d(TAG, "Initial inventory query finished; enabling main UI.");
        }
    };

    
    
    // Enables or disables the "please wait" screen.
    void setWaitScreen(boolean set) {
        Log.e(TAG, "setWaitScreen");
    	//findViewById(R.id.screen_main).setVisibility(set ? View.GONE : View.VISIBLE);
        //findViewById(R.id.screen_wait).setVisibility(set ? View.VISIBLE : View.GONE);
    }

    void complain(String message) {
        Log.e(TAG, "**** Error: " + message);
        alert("Error: " + message);
    }

    void alert(String message) {
        AlertDialog.Builder bld = new AlertDialog.Builder(this);
        bld.setMessage(message);
        bld.setNeutralButton("OK", null);
        Log.d(TAG, "Showing alert dialog: " + message);
        bld.create().show();
    }

}
