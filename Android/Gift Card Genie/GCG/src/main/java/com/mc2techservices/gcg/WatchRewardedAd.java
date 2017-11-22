package com.mc2techservices.gcg;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.ads.AdRequest;
import com.google.android.gms.ads.MobileAds;
import com.google.android.gms.ads.reward.RewardItem;
import com.google.android.gms.ads.reward.RewardedVideoAd;
import com.google.android.gms.ads.reward.RewardedVideoAdListener;
import com.mc2techservices.ads.AdsMain;

import java.util.Timer;
import java.util.TimerTask;


public class WatchRewardedAd extends Activity implements RewardedVideoAdListener {
    private RewardedVideoAd mAd;
    boolean pRewarded;
    String pBonus;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        pBonus=(getIntent().getStringExtra("Bonus"));
        setContentView(R.layout.activity_watch_rewarded_ad);
        pRewarded=false;
        mAd = MobileAds.getRewardedVideoAdInstance(this);
        mAd.setRewardedVideoAdListener(this);
        Toast.makeText(getApplicationContext(), "Ad is loading, please wait...", Toast.LENGTH_SHORT).show();
        mAd.loadAd("ca-app-pub-2250341510214691/3116915607", new AdRequest.Builder().build());
        StartUI();
    }
    private void StartUI()
    {
        final Timer t = new Timer();
        t.scheduleAtFixedRate(new TimerTask() {
            public void run() {
                runOnUiThread(new Runnable() {
                    public void run() {
                        if (mAd.isLoaded()) {
                            t.cancel();
                            mAd.show();
                        }
                    }
                });
            }
        }, 0, 100);
    }

    private void LogReward(String pAmnt)
    {
        String pUUID=AppSpecific.gloUUID;
        String pKey1=GeneralFunctions01.Text.GetRandomString("ANF",15);
        String pKey2=Decode.PMConvertIDtoValue(pKey1);
        String pParams = "pUUID=" + pUUID + "&pKey1=" + pKey1 + "&pKey2=" + pKey2 + "&pDetails=Ad" + pAmnt;
        String pURL=AppSpecific.gloWebServiceURL + "/LogCredits";
        //String temp=GeneralFunctions01.Comm.NonAsyncWebCall(pURL,pParams);
        new GeneralFunctions01.AsyncWebCall().execute(pURL,pParams);
    }
    private void LogAdInfo(String pAmnt)
    {
        if (pAmnt.equals("1")) pAmnt="AdmobAdWatched";
        if (pAmnt.equals("3")) pAmnt="AdmobAdClicked";
        String pUUID=AppSpecific.gloUUID;
        String pParams = "pUniqueID=" + pUUID + "&pTypeLogged=" + pAmnt;
        String pURL=AppSpecific.gloWebServiceURL + "/SetAdInfo";
        AdsMain am = new AdsMain();
        am.DoWRSetAdInfo00(pUUID,pAmnt,"gcg");

    }
    private void SwitchScreens()
    {
        /*
        Intent intent = new Intent(this, SubmitEmailForFriends.class);
        startActivity(intent);
        */
        finish();
    }
    private void SwitchScreensBack()
    {
        //Intent intent = new Intent(this, WhatsTheCatch.class);
        //startActivity(intent);
        finish();
    }


    public void onUseClicked(View arg0) {
        SwitchScreens();
    }
    public void onGetMoreClicked(View arg0) {
        SwitchScreensBack();
    }


    // Required to reward the user.
    @Override
    public void onRewarded(RewardItem reward) {
        Log.d("APP", "onRewarded");
        LogAdInfo("1");
        LogReward("1");
        SwitchScreens();
        pRewarded=true;
        // Reward the user.
    }

    // The following listener methods are optional.
    @Override
    public void onRewardedVideoAdLeftApplication() {
        Log.d("APP", "onRewardedVideoAdLeftApplication");
        LogAdInfo("3");
        if (pBonus.equals("1")) LogReward("3");
        pRewarded=true;
        //finish();
    }

    @Override
    public void onRewardedVideoAdClosed() {
        Log.d("APP", "onRewardedVideoAdClosed");
        if (pRewarded==false)
        {
            SwitchScreensBack();
        }
        else
        {
            SwitchScreens();
        }
    }

    @Override
    public void onRewardedVideoAdFailedToLoad(int errorCode) {
        Log.d("APP", "onRewardedVideoAdFailedToLoad");
        finish();
    }

    @Override
    public void onRewardedVideoAdLoaded() {
        Log.d("APP", "onRewardedVideoAdLoaded");
    }

    @Override
    public void onRewardedVideoAdOpened() {
        Log.d("APP", "onRewardedVideoAdOpened");
    }

    @Override
    public void onRewardedVideoStarted() {
        Log.d("APP", "onRewardedVideoStarted");
    }

}
