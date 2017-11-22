package com.mc2techservices.gcg;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.net.Uri;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.TextView;

import java.util.Timer;
import java.util.TimerTask;

public class WatchInternalAd extends Activity {
    Timer t1;
    int pCounter;
    ProgressBar progressBarSS;
    Button cmdCloseOffer;
    TextView txtDescription;
    ImageView imgLoadAd;
    String pPackageNameForOffer;
    String pBonus;
    boolean pLeftApplication;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        t1=new Timer();
        pBonus=(getIntent().getStringExtra("Bonus"));
        setContentView(R.layout.activity_watch_internal_ad);
        cmdCloseOffer=(Button)findViewById(R.id.cmdCloseOffer);
        cmdCloseOffer.setEnabled(false);
        txtDescription=(TextView)findViewById(R.id.txtDescription);
        imgLoadAd=(ImageView)findViewById(R.id.imgLoadAd);
        SelectRandomOffer();
        ShowWaiting();
    }
    private void SelectRandomOffer()
    {
        String pOfferNum=GeneralFunctions01.Text.GetRandomInt(1,6);
        if (pOfferNum.equals("1"))
        {
            LoadAdImage("ad_internal_bcb");
            txtDescription.setText("Allows you to view several barcodes on one screen in a vertical orientation");
            pPackageNameForOffer="com.mc2techservices.BarcodeBundler";
        }
        else if (pOfferNum.equals("2"))
        {
            LoadAdImage("ad_internal_fpf");
            txtDescription.setText("Instantly connects to friends for 500MB - takes two minutes!");
            pPackageNameForOffer="com.mc2techservices.fpfriendsfor500mb";
        }
        else if (pOfferNum.equals("3"))
        {
            LoadAdImage("ad_internal_gcb");
            txtDescription.setText("Gets gift card balances for merchants such as Target, Walmart, GameStop, etc");
            pPackageNameForOffer="com.mc2techservices.gift_card_balances";
        }
        else if (pOfferNum.equals("4"))
        {
            LoadAdImage("ad_internal_pccb");
            txtDescription.setText("Lookup balances on American Express AmEx, Visa, MasterCard credit & debit cards");
            pPackageNameForOffer="com.mc2techservices.pccb";
        }
        else if (pOfferNum.equals("5"))
        {
            LoadAdImage("ad_internal_pcs");
            txtDescription.setText("This app is for anyone who wants to solve the 2X2 Rubik's Cube, the pocket cube");
            pPackageNameForOffer="com.mc2techservices.pocketcube";
        }
        else if (pOfferNum.equals("6"))
        {
            LoadAdImage("ad_internal_yci");
            txtDescription.setText("Gets your family in fast with all your YMCA barcodes on one screen!");
            pPackageNameForOffer="com.mc2techservices.com.mc2techservices.YMCA_Check_In";
        }
        //Don't do this for the app were using...
        if (pOfferNum.equals("2")) SelectRandomOffer();
    }
    private void LoadAdImage(String pNameOfImage)
    {
        int pID=getResources().getIdentifier(pNameOfImage,"drawable",this.getPackageName());
        Bitmap myBitmapBgr = BitmapFactory.decodeResource(this.getResources(), pID);
        Drawable mBackground = new BitmapDrawable(getResources(), myBitmapBgr);
        imgLoadAd.setBackground(mBackground);
    }
    private void GoToGooglePlay()
    {
        try {
            startActivity(new Intent(Intent.ACTION_VIEW, Uri.parse("market://details?id=" + pPackageNameForOffer)));
        } catch (android.content.ActivityNotFoundException anfe) {
            startActivity(new Intent(Intent.ACTION_VIEW, Uri.parse("https://play.google.com/store/apps/details?id=" + pPackageNameForOffer)));
        }
        pLeftApplication=true;
    }
    private void ShowWaiting()
    {
        t1.scheduleAtFixedRate(new TimerTask() {
            int iMaxAmnt=5;
            public void run() {
                runOnUiThread(new Runnable() {
                    public void run() {
                        pCounter++;
                        if (pCounter>iMaxAmnt)
                        {
                            t1.cancel();
                            cmdCloseOffer.setText("Dismiss Offer");
                            cmdCloseOffer.setEnabled(true);
                        }
                        else
                        {
                            int iDispSecs=(iMaxAmnt-pCounter)+1;
                            cmdCloseOffer.setText(GeneralFunctions01.Conv.IntToString(iDispSecs));
                        }
                    }
                });
            }
        }, 0, 1000); // 1000 means start delay (1 sec), and the second is the loop delay.
    }
    private void LogReward(String pAmnt)
    {
        //string pUUID, string pDetails

        String pUUID=AppSpecific.gloUUID;
        String pKey1=GeneralFunctions01.Text.GetRandomString("ANF",15);
        String pKey2=Decode.PMConvertIDtoValue(pKey1);
        String pParams = "pUUID=" + pUUID + "&pKey1=" + pKey1 + "&pKey2=" + pKey2 + "&pDetails=Ad" + pAmnt;
        String pURL=AppSpecific.gloWebServiceURL + "/LogCredits";
        new GeneralFunctions01.AsyncWebCall().execute(pURL,pParams);
    }
    private void LogAdInfo(String pAmnt)
    {
        if (pAmnt.equals("1")) pAmnt="InternalAdWatched";
        if (pAmnt.equals("3")) pAmnt="InternalAdClicked";
        String pUUID=AppSpecific.gloUUID;
        String pParams = "pUniqueID=" + pUUID + "&pTypeLogged=" + pAmnt;
        String pURL=AppSpecific.gloWebServiceURL + "/SetAdInfo";
        new GeneralFunctions01.AsyncWebCall().execute(pURL,pParams);

    }
    public void onCheckOutOfferClicked(View arg0) { CheckOutAd(); }
    public void onCloseAdClicked(View arg0) {
        CloseAd();
    }
    private void CheckOutAd()
    {
        LogAdInfo("1");
        LogAdInfo("3");
        LogReward("1");
        if (pBonus.equals("1")) LogReward("3");
        GoToGooglePlay();
    }
    private void CloseAd()
    {
        LogAdInfo("1");
        LogReward("1");
        SwitchScreens();
    }
    private void SwitchScreens()
    {
        Intent intent = new Intent(this, LookupActivity.class);
        startActivity(intent);
        finish();
    }
    @Override
    public void onResume () {
        super.onResume();
        if (pLeftApplication==true)SwitchScreens();
    }

}
