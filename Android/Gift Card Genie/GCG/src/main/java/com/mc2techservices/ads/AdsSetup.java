package com.mc2techservices.ads;

import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.app.Activity;
import android.util.Log;

import com.mc2techservices.gcg.AllGCsActivity;
import com.mc2techservices.gcg.AppSpecific;
import com.mc2techservices.gcg.GeneralFunctions01;
import com.mc2techservices.gcg.R;
import com.mc2techservices.gcg.WatchInternalAd;
import com.mc2techservices.gcg.WatchRewardedAd;
import com.mc2techservices.gcg.WebComm;

import java.util.Timer;
import java.util.TimerTask;


public class AdsSetup extends Activity {
    AdsMain am1;
    AdsMain am2;
    boolean am1Complete;
    boolean am2Complete;
    Timer timer1;
    Timer timer2;
    int counter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_ads_setup);
        am1Complete=false;
        am2Complete=false;
        DoWRGetAdInfo00();
        DoWRGetAd00();
        ShowDetailsOnRewardStructure();  //This will happen while what ad to use is determined in the background.
    }
    private void ShowDetailsOnRewardStructure()
    {
        DialogInterface.OnClickListener dialogClickListener = new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                switch (which){
                    case DialogInterface.BUTTON_POSITIVE:
                        Log.d("APP", "BUTTON_POSITIVE");
                        GoToAdWhenReady();
                        break;

                    case DialogInterface.BUTTON_NEGATIVE:
                        Log.d("APP", "BUTTON_NEGATIVE");
                        finish();
                        break;
                }
            }
        };
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setMessage("You'll be shown a rewarded video ad; you can do three things: 1-close it, 2-complete it, 3 interact with it").setPositiveButton("Yes", dialogClickListener)
                .setNegativeButton("No", dialogClickListener).show();
    }
    private void DetermineAdToUse() {
        String[] pieces = am1.AdsResponse.split(am1.pPD);
        int iAdmobAdsWatched = GeneralFunctions01.Conv.StringToInt(pieces[0]);
        int iAdmobAdsClicked = GeneralFunctions01.Conv.StringToInt(pieces[1]);
        int iInternalAdWatched = GeneralFunctions01.Conv.StringToInt(pieces[2]);
        String pDateTimeBeforeReset = pieces[3];
        if (iAdmobAdsClicked >= 2) {
            WatchInternalAd();
        } else {
            WatchRewardedAd();
        }
    }

    private void WatchRewardedAd()
    {
        Intent intent = new Intent(this, WatchRewardedAd.class);
        intent.putExtra("Bonus", "XXX");
        startActivity(intent);
        finish();
    }
    private void WatchInternalAd()
    {
        String[] pieces=am2.AdsResponse.split(am2.pPD);
        String pImageURL=pieces[0];
        String pPackageName=pieces[1];
        String pDescription=pieces[2];
        Intent intent = new Intent(this, WatchInternalAd.class);
        intent.putExtra("Bonus", "XXX");
        intent.putExtra("URL", pImageURL);
        intent.putExtra("PackageName", pPackageName);
        intent.putExtra("Description", pDescription);
        startActivity(intent);
        finish();
    }

    private void DoWRGetAdInfo00()
    {
        counter=0;
        am1=new AdsMain("xmlns=\"shared.mc2techservices.com\">","http://192.168.199.1/AdsWebservice/AdsWS.asmx");
        am1.DoWRGetAdInfo00(AppSpecific.gloUUID,"gcg");  //GetAdInfo00, GetAd00
        MonitorWR1();
    }
    private void DoWRGetAd00()
    {
        counter=0;
        am2=new AdsMain("xmlns=\"shared.mc2techservices.com\">","http://192.168.199.1/AdsWebservice/AdsWS.asmx");
        am2.DoWRGetAd00(AppSpecific.gloUUID, "Android","gcg");  //GetAdInfo00, GetAd00
        MonitorWR2();
    }

    private void MonitorWR1()
    {
        timer1 = new Timer();
        timer1.scheduleAtFixedRate(new TimerTask() {

            public void run() {
                if (am1==null) return;
                if (am1.AdsResponse==null) return;
                //if this isn't null, we got a response and are done monitoring it
                am1Complete=true;
                timer1.cancel();
            }
        }, 0, 1000);
    }
    private void MonitorWR2()
    {
        timer2 = new Timer();
        timer2.scheduleAtFixedRate(new TimerTask() {
            public void run() {
                if (am2==null) return;
                if (am2.AdsResponse==null) return;
                //if this isn't null, we got a response and are done monitoring it
                am2Complete=true;
                timer2.cancel();
            }
        }, 0, 1000);
    }

    private void GoToAdWhenReady()
    {
        final Timer t=new Timer();
        t.scheduleAtFixedRate(new TimerTask() {
            public void run() {
                runOnUiThread(new Runnable() {
                    public void run() {
                        counter++;
                        Log.d("APP", "Private timer running; count" + counter);
                        if (am1Complete == true && am2Complete == true) {
                            Log.d("APP", "We have our ad data, figure out what to do now");
                            t.cancel();
                            DetermineAdToUse();
                        }
                        else if (counter>5)
                        {
                            t.cancel();
                            ShowCantContinue();
                        }
                    }
                });
            }
        }, 0, 1000); // 1000 means start delay (1 sec), and the second is the loop delay.
    }

    private void ShowCantContinue()
    {
        AlertDialog.Builder alert = new AlertDialog.Builder(this);
        alert.setTitle("Oops");
        alert.setMessage("We cant connect to the ad server right now (weak signal, server down, etc.) - please try again later.");
        alert.setPositiveButton("OK", new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int whichButton) {
                System.exit(0);
            }
        });
        alert.show();
    }

}
