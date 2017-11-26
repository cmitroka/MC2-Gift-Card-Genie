package com.mc2techservices.gcg;

import com.google.android.gms.ads.AdListener;
import com.google.android.gms.ads.AdRequest;
import com.google.android.gms.ads.AdView;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

public class AllGCsActivity extends Activity {
	String pGType;
	String pGSubType;
	String pGIssuer;
	String pGURL;
	String pGID;
	AdView pBannerAd;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_all_gcs);
		pBannerAd=(AdView)findViewById(R.id.pBannerAdView);
		AdRequest pAdRequest = new AdRequest.Builder()
				.addTestDevice(AdRequest.DEVICE_ID_EMULATOR)
				.build();
		pBannerAd.loadAd(pAdRequest);
		pBannerAd.setAdListener(new AdListener() {
			@Override
			public void onAdLoaded() {
				Log.d("APP", "Ad is loaded!");
			}
			@Override
			public void onAdClosed() {
				Log.d("APP", "Ad is closed!");
			}
			@Override
			public void onAdFailedToLoad(int errorCode) {
				Log.d("APP", "Ad failed to load! error code: " + errorCode);
			}
			@Override
			public void onAdLeftApplication() {
				Log.d("APP", "Ad left application!");
				LogWatchAd();
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
		getMenuInflater().inflate(R.menu.all_gcs, menu);
		return true;
	}
	@Override
	public void onResume () {
	    super.onResume();
		Populate();
	}
	private void ShowOfflineWarning()
	{
		DialogInterface.OnClickListener dialogClickListener = new DialogInterface.OnClickListener() {
		    @Override
		    public void onClick(DialogInterface dialog, int which) {
		        switch (which)
		        {
		        case DialogInterface.BUTTON_NEUTRAL:
		    		Log.d("APP", "They've approved");
		    	}
		    }
		};
		AlertDialog.Builder builder = new AlertDialog.Builder(this);
		builder.setTitle("Someone's Offline");
		builder.setMessage("Either you (or we) are offline.  You can still use the app, but most things won't work properly.  Try restarting later.");
		builder.setNeutralButton("OK", dialogClickListener);
		AlertDialog dialog = builder.create();
		dialog.show();
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.mnuGML) {
			Intent intent = new Intent(this, PurchaseOptionsActivity.class);
			startActivity(intent);		
			return true;
		}
		if (id == R.id.mnuAbout) {
			Intent intent = new Intent(this, AboutActivity.class);
			startActivity(intent);		
			return true;
		}
		else if (id == R.id.mnuContactUs) {
			ContactUs();
			return true;
		}
		else if (id == R.id.mnuResetDB) {
			ResetDB();
			return true;
		}
		
		
		return super.onOptionsItemSelected(item);
	}

	
	private void DispLookupsLeft()
	{
		String AllData=GeneralFunctions01.Comm.NonAsyncWebCall(AppSpecific.gloWebServiceURL + "/GetLookupsRemaining", "pUUID="+AppSpecific.gloUUID);
	    Toast.makeText(this, "You have " + AllData + " lookups remaining.", Toast.LENGTH_LONG).show();
	}

	
	private void PossiblyCreateDB()
	{
		DBAdapter myDb = new DBAdapter(this);
		myDb.deleteDB(this);
		myDb.open();
		myDb.close();
	}
	public void Populate() {

	    String pUCDID, pUCDCardType, pUCDCardNumber, pUCDCardPIN, pUCDLogin, pBHLastKnownBalance, pBHLastKnownBalanceDate;
		LinearLayout pMainView = (LinearLayout) findViewById(R.id.pMainView);
		LinearLayout llAdArea = (LinearLayout) findViewById(R.id.llAdArea);
		LinearLayout llMainCardTypes = (LinearLayout) findViewById(R.id.llMainCardTypes);
		llMainCardTypes.removeAllViews();

		DBAdapter myDb  = new DBAdapter(this);
		myDb.open();
		String AllSavedData=myDb.getAllRowsAsString("SELECT * FROM qryUserCardData");
		myDb.close();
		if (AllSavedData.equals(""))return;
		String[] lines=AllSavedData.split("l#d");
		String templine="";
		String rowBGColor="#ffffff";
		for (int i = 0; i < lines.length; i++) {
			if (lines[0].equals("")) break;
			templine=lines[i]+"p#d";
			String[] pVal=GeneralFunctions01.Text.Split(templine, "p#d");
		    if (GeneralFunctions01.Oth.IsOddNumer(i)){
		    	rowBGColor="#EBEBEB";
		    }
		    else
		    {
		    	rowBGColor="#ffffff";		    	
		    }
			pUCDID=GeneralFunctions01.Text.GetValInArray(pVal, 0);
			pUCDCardType=GeneralFunctions01.Text.GetValInArray(pVal, 1);
			pUCDCardNumber=GeneralFunctions01.Text.GetValInArray(pVal, 2);
			pUCDCardPIN=GeneralFunctions01.Text.GetValInArray(pVal, 3);
			pUCDLogin=GeneralFunctions01.Text.GetValInArray(pVal, 4);
			pBHLastKnownBalance=GeneralFunctions01.Text.GetValInArray(pVal, 5);
			pBHLastKnownBalanceDate=GeneralFunctions01.Text.GetValInArray(pVal, 6);
		    if (pBHLastKnownBalance.equals("")) pBHLastKnownBalance="$?.??";
		    if (pBHLastKnownBalanceDate.equals(""))
	    	{
				pBHLastKnownBalanceDate="N/A";
	    	}
		    else
		    {
				pBHLastKnownBalanceDate=GeneralFunctions01.Conv.DBDateToStandardDate(pBHLastKnownBalanceDate, true);
		    }
		    LinearLayout row = BuildTableRow(pUCDID, pUCDCardType, pUCDCardNumber, pUCDCardPIN, pUCDLogin, pBHLastKnownBalance, pBHLastKnownBalanceDate);
			row.setBackgroundColor(Color.parseColor(rowBGColor));
			Log.d("APP", "Completed " + i);		
			llMainCardTypes.addView(row);
		}
		if (AppSpecific.gloPurchased.equals("1"))
		{
			pMainView.removeView(llAdArea);
		}
		String AdViewDate=GeneralFunctions01.Cfg.ReadSharedPreference(this, "WatchAdDate");
		if (!AdViewDate.equals(""))
		{
			String currDate1=GeneralFunctions01.Dte.GetCurrentDate();
			if (currDate1.equals(AdViewDate))
			{
				pMainView.removeView(llAdArea);
			}
		}
	}

	private void LogWatchAd()
	{
		String testWatchAdDate=GeneralFunctions01.Cfg.ReadSharedPreference(this, "WatchAdDate");
		String CurrDate=GeneralFunctions01.Dte.GetCurrentDate();
		if (!testWatchAdDate.equals(CurrDate))
		{
			GeneralFunctions01.Cfg.WriteSharedPreference(this, "WatchAdDate", CurrDate);
			String pParams = "pUUID=" + AppSpecific.gloUUID + "&pKey=" +AppSpecific.gloKey + "&pDetails=PCB Android&pAmount=1";
			String pURL=AppSpecific.gloWebServiceURL + "/LogLookupIncrease";
			new GeneralFunctions01.AsyncWebCall().execute(pURL,pParams);
		}
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

	private LinearLayout BuildTableRow(String pUCDID, String pUCDCardType, String pUCDCardNumber, String pUCDCardPIN, String pUCDLogin, String pBHLastKnownBalance, String pBHLastKnownBalanceDate) {
		LinearLayout row = (LinearLayout)LayoutInflater.from(this).inflate(R.layout.cell_user_card_entry, null);
		((TextView)row.findViewById(R.id.lblCellType)).setText(pUCDCardType);
		((TextView)row.findViewById(R.id.lblCellCardNumber)).setText(pUCDCardNumber);
		((TextView)row.findViewById(R.id.lblCellCardPIN)).setText(pUCDCardPIN);
		((TextView)row.findViewById(R.id.lblCellLogin)).setText(pUCDLogin);
		((TextView)row.findViewById(R.id.lblCellBalance)).setText(pBHLastKnownBalance);
		((TextView)row.findViewById(R.id.lblCellBalDate)).setText(pBHLastKnownBalanceDate);
		((TextView)row.findViewById(R.id.lblCellID)).setText(pUCDID);
		
	    row.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				TextView lblCellCardID = (TextView)v.findViewById(R.id.lblCellID);
				pGID=lblCellCardID.getText().toString();
				SwitchScreens();
			}
		});
		return row;
	}
	public void SwitchScreens()
	{
		
		Intent intent = new Intent(this, AddModCardActivity.class);
		intent.putExtra("CardID", pGID);
		startActivity(intent);
	}
	
	public void ContactUs()
	{
		
		Intent intent = new Intent(this, ContactActivity.class);
		startActivity(intent);
	}
	
	public void onClickNewEntry(View arg0)
	{
		Intent intent = new Intent(this, PreCardTypeSelectActivity.class);  //CardTypeSelectActivity
		startActivity(intent);
	}
	
	
}
