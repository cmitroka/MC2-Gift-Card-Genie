package com.mc2techservices.gcg;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.EditText;

import com.mc2techservices.ads.AdsSetup;

public class PurchaseOptionsActivity extends Activity {
    static boolean watched = false;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_purchaseoptions);
		CheckIfWatchAdIsAllowed();
	}
	public void onWatchAdClicked(View arg0) {
		DoWatchAd00();
	}
	private void DoWatchAd00() {
		Intent intent = new Intent(this, AdsSetup.class);
		intent.putExtra("EntryPoint", "PURCHASE SCREEN");
		startActivity(intent);
		finish();

	}
	private void DoWatchAd()
	{
		//watched=false;
		if (watched)
		{
			GeneralFunctions01.Oth.Alert(this, "Sorry, you can only do this once a day.");
			return;
		}

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

	private void CheckIfWatchAdIsAllowed()
	{
		String AdViewDate=GeneralFunctions01.Cfg.ReadSharedPreference(this, "WatchAdDate");
		if (!AdViewDate.equals(""))
		{
			String currDate1=GeneralFunctions01.Dte.GetCurrentDate();
			if (currDate1.equals(AdViewDate))
			{
				watched=true;
			}
		}
	}

	private void WatchAd() {
		Intent intent = new Intent(this, WatchAdActivity.class);
		startActivity(intent);			
		finish();
	}	
	
	private void PromptForInput()
	{
		AlertDialog.Builder alert = new AlertDialog.Builder(this);
		
		alert.setTitle("Override Code");
		alert.setMessage("Please enter " + AppSpecific.gloShortUUID + " override code:");
	
		// Set an EditText view to get user input 
		final EditText input = new EditText(this);
		input.setText("");
		alert.setView(input);
	
		alert.setPositiveButton("OK", new DialogInterface.OnClickListener() {
		@Override
		public void onClick(DialogInterface dialog, int whichButton) {
		String inputValue = input.getText().toString();
			if (inputValue.equals(AppSpecific.gloKey))
	    	{
				DoOverride();
			}
			finish();
		 }
		});
	
		alert.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
		 @Override
		public void onClick(DialogInterface dialog, int whichButton) {
		     // Canceled.
		}
		});
		 alert.show();
	}

	public void onClickOverride(View arg0) {
		Log.d("APP", "Key: " + AppSpecific.gloKey);
		PromptForInput();
	}

	public void onClickFailTest(View arg0) {
		Intent intent = new Intent(this, PurchaseActivity.class);
		intent.putExtra("PurchType", "XXX");
		startActivity(intent);
		finish();
	}

	private void DoOverride()
	{
		Intent intent = new Intent(this, PurchaseActivity.class);
		intent.putExtra("PurchType", "998");
		startActivity(intent);
		finish();
	}
	
	public void onPurchaseInfClicked(View arg0) {
		Intent intent = new Intent(this, PurchaseActivity.class);
		intent.putExtra("PurchType", "999");
		startActivity(intent);
		finish();
	}

	public void onClickCancel(View arg0) {
		finish();
	}

}
