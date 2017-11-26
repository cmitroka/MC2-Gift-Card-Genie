package com.mc2techservices.ads;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.SystemClock;
import android.support.constraint.ConstraintLayout;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import com.mc2techservices.ads.AdsMain;
import com.mc2techservices.gcg.AppSpecific;
import com.mc2techservices.gcg.GeneralFunctions01;
import com.mc2techservices.gcg.R;

import java.io.File;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.URL;
import java.util.Timer;
import java.util.TimerTask;

public class WatchInternalAd extends Activity {
    String pBonus;
    String pURLIn;
    String pPackageName;
    String pDescription;

    String wcWebResponse;

    Timer t1;
    int pCounter;
    ProgressBar progressBarSS;
    Button cmdCloseOffer;
    TextView txtDescription;
    ImageView imgLoadAd;
    boolean pLeftApplication;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        t1=new Timer();
        pBonus=(getIntent().getStringExtra("Bonus"));
        pURLIn=(getIntent().getStringExtra("URL"));
        pPackageName=(getIntent().getStringExtra("PackageName"));
        pDescription=(getIntent().getStringExtra("Description"));
        setContentView(R.layout.activity_watch_internal_ad);


        cmdCloseOffer=(Button)findViewById(R.id.cmdCloseOffer);
        cmdCloseOffer.setEnabled(false);
        txtDescription=(TextView)findViewById(R.id.txtDescription);
        imgLoadAd=(ImageView)findViewById(R.id.imgLoadAd);
        DownloadAdImage();
        LoadPicture();
        txtDescription.setText(pDescription);
        ShowWaiting();
    }
    private void DownloadAdImage(){
        String pURL=AdsMain.pWebAdImagesAddress + pURLIn;
        DownloadFile(pURL,"ad");
        do {
            SystemClock.sleep(100);
        } while (!wcWebResponse.equals("Download Completed!"));
    }

    private void LoadPicture()
    {
        String tempFileLoc=String.valueOf(getFilesDir());
        File imageFile = new File(tempFileLoc,"ad");
        if(imageFile.exists())
        {
            Bitmap myBitmap = BitmapFactory.decodeFile(imageFile.getAbsolutePath());
            imgLoadAd.setImageBitmap(myBitmap);
        }
        else
            Toast.makeText(this, "The ad doesn't exist", Toast.LENGTH_LONG).show();
    }
    private void GoToGooglePlay()
    {
        {
            try {
                startActivity(new Intent(Intent.ACTION_VIEW, Uri.parse("market://details?id=" + pPackageName)));
            } catch (android.content.ActivityNotFoundException anfe) {
                startActivity(new Intent(Intent.ACTION_VIEW, Uri.parse("https://play.google.com/store/apps/details?id=" + pPackageName)));
            }
            pLeftApplication = true;
        }
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
    private void LogAdInfo(String pAmnt)
    {
        String pType="";
        if (pAmnt.equals("1")) pType="InternalAdWatched";
        if (pAmnt.equals("3")) pType="InternalAdClicked";
        String pUUID= AppSpecific.gloUUID;
        AdsMain am=new AdsMain();
        am.DoWRSetAdInfo00(pUUID,pType,"android_gcg");

    }
    public void onCheckOutOfferClicked(View arg0) { CheckOutAd(); }
    public void onCloseAdClicked(View arg0) {
        CloseAd();
    }
    private void CheckOutAd()
    {
        t1.cancel();
        //LogAdInfo("1");
        LogAdInfo("3");
        //LogReward("1");
        //if (pBonus.equals("1")) LogReward("3");
        GoToGooglePlay();
    }
    private void CloseAd()
    {
        LogAdInfo("1");
        //LogReward("1");
        //SwitchScreens();
    }
    /*
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
    */
    /*
    private void SwitchScreens()
    {
        Intent intent = new Intent(this, LookupActivity.class);
        startActivity(intent);
        finish();
    }
    */
    @Override
    public void onResume () {
        super.onResume();
        //if (pLeftApplication==true)SwitchScreens();
    }


    public void DownloadFile(final String pURLIn, final String pWriteToLocIn) {
        new AsyncTask<String, String, String>() {
            @Override
            protected String doInBackground(String... params) {
                wcWebResponse = "...";
                publishProgress("Sleeping..."); // Calls onProgressUpdate()
                try {
                    URL pURL = new URL(pURLIn);
                    InputStream input = pURL.openStream();
                    int pInputSize=input.available();
                    byte[] buffer = new byte[1500];
                    String tempFileLoc=String.valueOf(getFilesDir());
                    OutputStream output = new FileOutputStream(tempFileLoc+"/ad");
                    try
                    {
                        int bytesRead = 0;
                        int bytesTotal=0;
                        while ((bytesRead = input.read(buffer, 0, buffer.length)) >= 0)
                        {
                            output.write(buffer, 0, bytesRead);
                            bytesTotal=bytesTotal+buffer.length;
                            wcWebResponse=bytesTotal+" wrote";
                            Log.d("DownloadFile", "Wrote " + bytesTotal);
                        }
                        wcWebResponse="Download Completed!";
                        Log.d("DownloadFile","Download Completed!");
                    }
                    catch (Exception e1) {
                        // TODO Auto-generated catch block
                        Log.d("DownloadFile", e1.getMessage());
                        wcWebResponse="DownloadFile Exception 2";
                    }
                } catch (Exception e) {
                    Log.d("DownloadFile", e.getMessage());
                    wcWebResponse = "DownloadFile Exception 2";
                }
                return "";
            }

            @Override
            protected void onPostExecute(String result) {
                //This does come back; what to do with it/how to handle it... not sure.
                Log.d("DownloadFile", "onPostExecute");
            }

            @Override
            protected void onPreExecute()
            {
                Log.d("DownloadFile", "onPreExecute");
            }

            @Override
            protected void onProgressUpdate(String... progress) {
                Log.d("DownloadFile", "onProgressUpdate");
            }
        }.execute(pURLIn, pWriteToLocIn);
    }

}
