package com.mc2techservices.gcg;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.os.SystemClock;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import com.mc2techservices.ads.AdsMain;
import com.mc2techservices.ads.AdsSetup;
import com.mc2techservices.ads.WatchRewardedAd;

import java.util.Timer;
import java.util.TimerTask;

public class PurchaseOptionsActivity extends Activity {
	int pRewardedVideoAdsWatched;
	int pRewardedVideoAdsClicked;
	int pInterstitialAdClicked;
	int wc1Cnt;
	AdsMain am;
	Button cmdRewardedVideo;
	Button cmdWatchAd;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_purchaseoptions);
		cmdRewardedVideo = (Button)findViewById(R.id.cmdRewardedVideo);
		cmdWatchAd = (Button)findViewById(R.id.cmdWatchAd);
		CheckIfWatchAdIsAllowed00();
	}
	private String DetermineAdMsgToShow(String AdRequest)
	{
		if (AdRequest.equals("Ad"))
		{
			if (pInterstitialAdClicked>0)
			{
				String test=DetermineAdMsgToShow("Rewarded");
				if (test.equals(""))
				{
					return "Sorry, you can only do this once a day.  Try a rewarded ad for more credits!";
				}
				else
				{
					return "You've maxed out how many credits you can get; you can get more tomorrow.";
				}
			}
		}
		else if (AdRequest.equals("Rewarded"))
		{
			if (pInterstitialAdClicked==0)
			{
				return "You can this after watching a normal ad.";
			}
			else
			{
				if (pRewardedVideoAdsClicked>1)
				{
					//they've clicked 2 rewarded videos, we cant show them any more rewarded videos.
					return "You've maxed out how many credits you can get; you can get more tomorrow.";
				}
				else if (pRewardedVideoAdsWatched>4)
				{
					//they've watched 5 rewarded videos, we cant show them any more rewarded videos.
					return "You've maxed out how many credits you can get; you can get more tomorrow.";
				}
			}
		}
		return "";
	}
	public void onRewardedVideoClicked(View arg0) {
		String pWarning=DetermineAdMsgToShow("Rewarded");
		if (!pWarning.equals(""))
		{
			GeneralFunctions01.Oth.Alert(this, pWarning);
			return;
		}
		DialogInterface.OnClickListener dialogClickListener = new DialogInterface.OnClickListener() {
			@Override
			public void onClick(DialogInterface dialog, int which) {
				switch (which){
					case DialogInterface.BUTTON_POSITIVE:
						Log.d("APP", "Watch an ad.");
						WatchRewardedAd();
						break;

					case DialogInterface.BUTTON_NEGATIVE:
						Log.d("APP", "Dont Delete");
						break;
				}
			}
		};
		AlertDialog.Builder builder = new AlertDialog.Builder(this);
		builder.setMessage("If you close the ad, no credits are given.\nIf you watch the entire ad, you'll get one credit.\nIf you click on the ad, you'll get three credits.").setPositiveButton("Continue", dialogClickListener)
				.setNegativeButton("Forget It", dialogClickListener).show();

	}
	public void onWatchAdClicked(View arg0) {
		String pWarning=DetermineAdMsgToShow("Ad");
		if (!pWarning.equals(""))
		{
			GeneralFunctions01.Oth.Alert(this, pWarning);
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
	private void WatchAMHang()
	{
		int cnt=0;
		do {
			SystemClock.sleep(100);
			cnt++;
			if (am.AdsResponse!=null) break; //got response
			if (cnt>50) return;  //timed out
		} while (1==1);
	}
	private void WatchAM()
	{
		wc1Cnt=0;
		final Timer wc1Tmr=new Timer();
		wc1Tmr.scheduleAtFixedRate(new TimerTask() {
			public void run() {
				runOnUiThread(new Runnable() {
					public void run() {
						wc1Cnt++;
						Log.d("APP", String.valueOf(wc1Tmr));
						if (am.AdsResponse==null) return;
						if (wc1Cnt > 10) {
							//timed out
							wc1Tmr.cancel();
							Log.d("APP", "Timed Out");
						}
						String[] pArray=am.AdsResponse.split(am.pPD);
						int iFail=am.GetIntValInArray(pArray,100);
						// RewardedVideoWatched, RewardedVideoClicked, InterstitialAdClicked, BannerAdClicked, InternalAdShown, ResetTime
						pRewardedVideoAdsWatched=am.GetIntValInArray(pArray,0);
						pRewardedVideoAdsClicked=am.GetIntValInArray(pArray,1);
						pInterstitialAdClicked=am.GetIntValInArray(pArray,2);
						int iBannerAdClicked=am.GetIntValInArray(pArray,3);
						int iInternalAdShown=am.GetIntValInArray(pArray,4);
						String sResetTime=am.GetStringValInArray(pArray,5);
						cmdRewardedVideo.setEnabled(true);
						cmdWatchAd.setEnabled(true);
					}
				});
			}
		}, 1, 1000); // 1000 means start delay (1 sec), and the second is the loop delay.
	}


	private void WatchAd() {
		Intent intent = new Intent(this, WatchAdActivity.class);
		startActivity(intent);
		finish();
	}
	private void WatchRewardedAd() {
		Intent intent = new Intent(this, WatchRewardedAd.class);
		startActivity(intent);
		finish();
	}

	private void CheckIfWatchAdIsAllowed00()
	{
		am = new AdsMain();
		am.DoWRGetAdInfo00(AppSpecific.gloUUID,"android_gcg");
		WatchAM();
	}

	public void onClickFailTest(View arg0) {
		Intent intent = new Intent(this, PurchaseActivity.class);
		intent.putExtra("PurchType", "XXX");
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
