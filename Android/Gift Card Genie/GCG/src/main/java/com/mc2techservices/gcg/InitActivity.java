package com.mc2techservices.gcg;

import java.util.Timer;
import java.util.TimerTask;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Typeface;
import android.os.Bundle;
import android.os.SystemClock;
import android.provider.Settings;
import android.util.Log;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.ads.MobileAds;
import com.mc2techservices.ads.AdsMain;
import com.mc2techservices.ads.AdsSetup;

public class InitActivity extends Activity {
	Timer t1;
	WebComm wc1;
	Timer wc1Tmr;
	int wc1Cnt;


	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_init);
		AppSpecific.gloPackageName = getApplicationContext().getPackageName();
		AppSpecific.gloLD="XZQX";
		AppSpecific.gloPD="~_~";
		AppSpecific.gloFinishIt=false;
		AppSpecific.gloxmlns= "xmlns=\"mc2techservices.com\">";
		AppSpecific.gloWebURL="http://192.168.199.1/UGCB/";
		//AppSpecific.gloWebURL="http://ugcb.mc2techservices.com/";
		AppSpecific.gloWebServiceURL=AppSpecific.gloWebURL + "UGCBWS.asmx";
		MobileAds.initialize(this,"ca-app-pub-2250341510214691~5656960838");
		ConfigUser();
		//GoToAdTest();
		//DoWRGetAdInfo00();
	    /*
		TextView txtAppName=(TextView)findViewById(R.id.txtAppVersion);
		Typeface face=Typeface.createFromAsset(getAssets(),
                "fonts/MATURASC.TTF");
		txtAppName.setTypeface(face);
		*/
	    TextView txtAppVersion=(TextView)findViewById(R.id.txtAppVersion);
		txtAppVersion.setText(GeneralFunctions01.Sys.GetVersion(this));
		GoToAdTesting();

		/*
		LogAppLaunch();
		IsPurchased();
		Toast.makeText(this,"Screen may freeze 5 seconds; please wait",Toast.LENGTH_SHORT);
		StartGetCardInfo();
		*/
	}
	private void CompleteOrExit()
	{
		if (CanWeProceed())
		{
			DoConvert00();
			GoToAllGCs();  //test
		}
		else
		{
			GeneralFunctions01.Cfg.WriteSharedPreference(this,"DBWSSize","-999");
			ShowIssue("There seems to be a connectivity issue - we cant send you our list of supported merchants.  Please try again later, and let us know if you repeatedly get this message.");
		}
	}

	private void LogAppLaunch()
	{
		String pUUID=GeneralFunctions01.Cfg.ReadSharedPreference(this, "UUID");
		String pParams = "pUUID=" + pUUID + "&pChannel=Android_GCG";
		String pURL=AppSpecific.gloWebServiceURL + "/LogAppLaunched";
		//GeneralFunctions01.Comm.NonAsyncWebCall(pURL, pParams);
		//new GeneralFunctions01.AsyncWebCall().execute(pURL,pParams);

		WebComm webComm=new WebComm(AppSpecific.gloxmlns);
		webComm.ExecuteWebRequest(pURL,pParams);

	}
	private void ResetDB()
	{
		DBAdapter myDb = new DBAdapter(this);
		myDb.open();
		myDb.deleteDB(this);
		myDb.close();
		myDb.open();
		myDb.close();
	}
	private boolean CanWeProceed()
	{
		DBAdapter myDb = new DBAdapter(this);
		myDb.open();
		String pRetVal=myDb.getSingleValAsString("SELECT COUNT(*) FROM tblCardInfo");
		myDb.close();
		if (pRetVal.equals("0"))
		{
			return false;
		}
		return true;
	}
	private void ShowIssue(String pMsg)
	{
		AlertDialog.Builder alert = new AlertDialog.Builder(this);
		alert.setTitle("Oops");
		alert.setMessage(pMsg);
		alert.setPositiveButton("OK", new DialogInterface.OnClickListener() {
			public void onClick(DialogInterface dialog, int whichButton) {
				System.exit(0);
			}
		});
		alert.show();
	}

	private void ConfigUser()
	{
		//GeneralFunctions01.Cfg.WriteSharedPreference("UUID", "");
		if (GeneralFunctions01.Cfg.ReadSharedPreference(this, "UUID").equals(""))
		{
			//GeneralFunctions01.Cfg.WriteSharedPreference("UUID", GeneralFunctions01.Text.GetRandomString("ANF", 8));  //Old way
			String android_id = Settings.Secure.getString(this.getContentResolver(), Settings.Secure.ANDROID_ID);
			if (android_id.length()<8) android_id=GeneralFunctions01.Text.GetRandomString("ANF", 8)+"_AGCG";
			GeneralFunctions01.Cfg.WriteSharedPreference(this,"UUID",android_id);
		}
		AppSpecific.gloUUID=GeneralFunctions01.Cfg.ReadSharedPreference(this,"UUID");
		String pShortUUID=AppSpecific.gloUUID.substring(0, 8);
		AppSpecific.gloShortUUID=pShortUUID;
		String pKey=AppSpecific.PMMakeKey(pShortUUID);
		AppSpecific.gloKey=pKey;
	}
	private void IsPurchased()
	{
		//GeneralFunctions01.Cfg.WriteSharedPreference("Purchased","0");
		AppSpecific.gloPurchased = GeneralFunctions01.Cfg.ReadSharedPreference(this, "Purchased");
		Log.d("APP", "Purchased: " + AppSpecific.gloPurchased);

	}

	private void GoToAllGCs()
	{
		Intent intent = new Intent(this, AllGCsActivity.class);
		startActivity(intent);
		finish();
	}
	private void GoToAdTesting()
	{
		Intent intent = new Intent(this, AdsSetup.class);
		startActivity(intent);
		finish();
	}
	private void GetLookupsRemaining()
	{
		//if (!isNetworkConnected()) return;
		String AllData=GeneralFunctions01.Comm.NonAsyncWebCall(AppSpecific.gloWebServiceURL + "/GetLookupsRemaining", "pUUID="+AppSpecific.gloUUID+"pChannel=Android");
		String[] AmntAllowedOKFailsRem = AllData.split("XZQX");
		//Allowed, used, failed, remaining
		String Remaining=AmntAllowedOKFailsRem[0];

	}
	private void DoConvert00()
	{
		DBAdapter myDb = new DBAdapter(this);
		myDb.open();
		myDb.Convert00();
		myDb.close();
	}
	private void DoWipe()
	{
		DBAdapter myDb = new DBAdapter(this);
		myDb.open();
		myDb.execSQL("DELETE FROM tblCardInfo");
		myDb.close();
	}

	private void StartGetCardInfo()
	{
		//DoWipe();  //test
		wc1=new WebComm(AppSpecific.gloxmlns);
		String pUUID=GeneralFunctions01.Cfg.ReadSharedPreference(this, "UUID");
		String pParams = "pRequestFrom=GCG";
		wc1.ExecuteWebRequest(AppSpecific.gloWebServiceURL + "/UnivGetCardInfo",pParams);
		WatchGetCardInfo();
	}
	private void WatchGetCardInfo()
	{
		wc1Cnt=0;
		wc1Tmr=new Timer();
		wc1Tmr.scheduleAtFixedRate(new TimerTask() {
			public void run() {
				runOnUiThread(new Runnable() {
					public void run() {
						wc1Cnt++;
						Log.d("APP", String.valueOf(wc1Tmr));
						if (wc1Cnt > 10) {
							//timed out
							wc1Tmr.cancel();
							Log.d("APP", "Timed Out");
							CompleteOrExit();
						} else if (!wc1.wcWebResponse.equals("...")) {
							wc1Tmr.cancel();
							Log.d("APP", "Theres a Response");
							FinishGetCardInfo();
							CompleteOrExit();
						}
					}
				});
			}
		}, 1, 1000); // 1000 means start delay (1 sec), and the second is the loop delay.
	}
	private void FinishGetCardInfo()
	{
		String AllData=wc1.wcWebResponse;
		if (AllData.equals("..."))
		{
			return;
		}
		//Dont proceed if we didn't get much data...
		if (AllData.length()<500)
		{
			return;
		}
		//Dont proceed if the data from this time is the same as last time...
		String pPrevSize=GeneralFunctions01.Cfg.ReadSharedPreference(this,"DBWSSize");
		if (pPrevSize.equals(String.valueOf(AllData.length())))
		{
			return;
		}
		DBAdapter myDb  = new DBAdapter(this);
		myDb.open();
		myDb.execSQL("DELETE FROM tblCardInfo");
		GeneralFunctions01.Cfg.WriteSharedPreference(this,"DBWSSize",String.valueOf(AllData.length()));
		String CurrRow;
		String CardType,Category,Phone,URL,ELP,CID;
		String[] separated = AllData.split("XZQX");
		for (int i = 0; i < separated.length; i++) {
			CurrRow=separated[i]+"~_~";
			CID=GeneralFunctions01.Text.GetValueAtPosition(CurrRow, "~_~", 0);
			CardType=GeneralFunctions01.Text.GetValueAtPosition(CurrRow, "~_~", 1);
			Category=GeneralFunctions01.Text.GetValueAtPosition(CurrRow, "~_~", 2);
			Phone=GeneralFunctions01.Text.GetValueAtPosition(CurrRow, "~_~", 3);
			URL=GeneralFunctions01.Text.GetValueAtPosition(CurrRow, "~_~", 4);
			ELP=GeneralFunctions01.Text.GetValueAtPosition(CurrRow, "~_~", 5);

			String pTemp=GeneralFunctions01.Text.PutValsInSingQuotes(CardType+","+Category+","+Phone+","+URL+","+ELP+","+CID);
			String OK=myDb.execSQL("INSERT INTO tblCardInfo (CICardType,CICategory,CIPhone,CIURL,CIShowLP,CIMDBID) VALUES ("+pTemp+")");
			Log.d("APP", "OK: " + OK);
		}
		myDb.close();
	}

}
