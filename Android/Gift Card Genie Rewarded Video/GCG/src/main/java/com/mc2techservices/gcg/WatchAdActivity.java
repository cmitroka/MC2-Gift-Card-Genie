package com.mc2techservices.gcg;

import java.util.Timer;
import java.util.TimerTask;

import com.google.android.gms.ads.AdListener;
import com.google.android.gms.ads.AdRequest;
import com.google.android.gms.ads.InterstitialAd;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.widget.TextView;

public class WatchAdActivity extends Activity {

    InterstitialAd mInterstitialAd;
	Timer t=new Timer();
	int	pCounter;
	TextView txtCountup;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_watch_ad);
        mInterstitialAd = new InterstitialAd(this);
        mInterstitialAd.setAdUnitId("ca-app-pub-2250341510214691/7840271042");
		txtCountup= (TextView) findViewById(R.id.txtCountup);
		MonitorStatus();
		requestNewInterstitial();
        
        mInterstitialAd.setAdListener(new AdListener() {
            @Override
            public void onAdLoaded() {
	    		Log.d("APP", "Ad is loaded!");
	    		t.cancel();
                //Toast.makeText(getApplicationContext(), "Ad is loaded!", Toast.LENGTH_SHORT).show();
	    		//AppSpecific.gloAdViewed="1";
            	mInterstitialAd.show();
            }
 
            @Override
            public void onAdClosed() {
	    		Log.d("APP", "Ad is closed!");
	    		GoToLookup();
                //requestNewInterstitial();
                //Toast.makeText(getApplicationContext(), "Ad is closed!", Toast.LENGTH_SHORT).show();
            }
 
            @Override
            public void onAdFailedToLoad(int errorCode) {
	    		Log.d("APP", "Ad failed to load! error code: " + errorCode);
	    		GoToLookup();
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
		//getMenuInflater().inflate(R.menu.watch_ad, menu);
		return true;
	}
	private void MonitorStatus()
	{
		t.scheduleAtFixedRate(new TimerTask() {
			public void run() {
				runOnUiThread(new Runnable() {
					public void run() {
						pCounter++;
						txtCountup.setText(GeneralFunctions01.Conv.IntToString(pCounter));
						Log.d("YMCA_Check Cnt", String.valueOf(pCounter));
						if (pCounter>9)
						{
							t.cancel();
							AlertThem();
						}
					}
				});
			}
		}, 0, 1000); // 1000 means start delay (1 sec), and the second is the loop delay.

	}
	private void AlertThem()
	{
			DialogInterface.OnClickListener dialogClickListener = new DialogInterface.OnClickListener() {
				@Override
				public void onClick(DialogInterface dialog, int which) {
					Log.d("Ad Info", "Ad Offline");
					finish();
				}
			};
			AlertDialog.Builder builder = new AlertDialog.Builder(this);
			builder.setTitle("Full Disclosure");
			builder.setMessage("Seems there's a connectivity issue, try again later please.");
			builder.setNeutralButton("OK", dialogClickListener);
			AlertDialog dialog = builder.create();
			dialog.show();
	}

	private void requestNewInterstitial() {
		AdRequest adRequest = new AdRequest.Builder()
		.addTestDevice("E0C6CEB3413BBA174F125A98F2ED7789")  //moto g
		.addTestDevice("4B84B68A45947ADBA8F4D155C51B02E2")  //moto 4g
		.addTestDevice(AdRequest.DEVICE_ID_EMULATOR)
		.build();

		mInterstitialAd.loadAd(adRequest);
	}
	private void GoToLookup()
	{
		GeneralFunctions01.Cfg.WriteSharedPreference(this, "JustWatchedAdDate","1");
		finish();		
	}
	private void LogPurchase()
	{
		String testWatchAdDate=GeneralFunctions01.Cfg.ReadSharedPreference(this, "WatchAdDate");
		String CurrDate=GeneralFunctions01.Dte.GetCurrentDate();
		if (!testWatchAdDate.equals(CurrDate))
		{
			GeneralFunctions01.Cfg.WriteSharedPreference(this, "WatchAdDate", CurrDate);
			String pParams = "pUUID=" + AppSpecific.gloUUID + "&pKey=" +AppSpecific.gloKey + "&pDetails=UGCB Android&pAmount=3";
			String pURL=AppSpecific.gloWebServiceURL + "/LogLookupIncrease";
			new GeneralFunctions01.AsyncWebCall().execute(pURL,pParams);
		}
		GoToLookup();
	}
}
