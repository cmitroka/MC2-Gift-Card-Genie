package com.mc2techservices.ads;

import android.os.AsyncTask;
import android.util.Log;
import android.view.View;

import com.mc2techservices.gcg.GeneralFunctions01;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.Timer;
import java.util.TimerTask;

/**
 * Created by Administrator on 10/21/2017.
 */


public class AdsMain {
    public static String pXMLNS;
    public static String pWebServiceURL;
    public static String pWebAdImagesAddress;
    public String pLD = "XZQX";
    public String pPD = "~_~";
    public String AdsResponse;

    public AdsMain() {
        pXMLNS = "xmlns=\"common.mc2techservices.com\">";
        pWebServiceURL = "http://www.mc2techservices.com/Common/AdsWS.asmx";
        pWebAdImagesAddress="http://www.mc2techservices.com/Common/Ads/Android/";
    }
    
    public void DoWRGetAdInfo00(String pUUID, String pApp) {
        String pURL = pWebServiceURL + "/GetAdInfo00";
        String pParams = "pUniqueID=" + pUUID + "&pApp=" + pApp ;
        AsyncWebCallRunner runner = new AsyncWebCallRunner();
        runner.execute(pURL, pParams);
    }
    public void DoWRGetAd00(String pUUID, String pPlatform, String pApp) {
        String pURL = pWebServiceURL + "/GetAd00";
        String pParams = "pUniqueID=" + pUUID + "&pPlatform=" + pPlatform + "&pApp=" + pApp;
        AsyncWebCallRunner runner = new AsyncWebCallRunner();
        runner.execute(pURL, pParams);
    }
    public void DoWRSetAdInfo00(String pUUID, String pTypeLogged, String pApp) {
        String pURL = pWebServiceURL + "/SetAdInfo00";
        String pParams = "pUniqueID=" + pUUID + "&pTypeLogged=" +pTypeLogged+ "&pApp=" + pApp ;
        AsyncWebCallRunner runner = new AsyncWebCallRunner();
        runner.execute(pURL, pParams);
    }

    public class AsyncWebCallRunner extends AsyncTask<String, String, String> {

        private String resp;
        //private String USER_AGENT = "Mozilla/5.0";

        @Override
        protected String doInBackground(String... params) {
            publishProgress("Sleeping..."); // Calls onProgressUpdate()
            AdsResponse=null;
            try {
                String url = params[0];
                String urlParameters = params[1];
                URL obj = new URL(url);
                HttpURLConnection con = (HttpURLConnection) obj
                        .openConnection();
                con.setRequestMethod("POST");
                con.setDoOutput(true);
                try {
                    DataOutputStream wr = new DataOutputStream(
                            con.getOutputStream());
                    wr.writeBytes(urlParameters);
                    wr.flush();
                    wr.close();
                } catch (Exception e) {
                    resp = "CallWebService Exception Occured 1";
                    return resp;
                }

                int responseCode = con.getResponseCode();
                if (responseCode != 200) {
                    resp = "responseCode!=200";
                    return resp;
                }

                BufferedReader in = new BufferedReader(new InputStreamReader(
                        con.getInputStream()));
                String inputLine;
                StringBuffer response = new StringBuffer();
                while ((inputLine = in.readLine()) != null) {
                    response.append(inputLine);
                }
                in.close();
                resp = response.toString();
            } catch (Exception e) {
                Log.d("AdsMain", "Error 002");
                Log.d("AdsMain", e.getMessage());
                resp = "CallWebService Exception Occured 2";
            }
            return resp;
        }

        @Override
        protected void onPostExecute(String result) {
            Log.d("AdsMain", "Result: " + result);
            String Response = GetResponseData(result); // "http://AsyncWebCall.mc2techservices.com/Barcodes/9876543321.jpg";
            Log.d("AdsMain", "Clean Response: " + Response);
            AdsResponse=Response;
        }

        @Override
        protected void onPreExecute() {
            Log.d("AdsMain", "onPreExecute");
            // Things to be done before execution of long running operation. For
            // example showing ProgessDialog
            //0 is for VISIBLE
            //4 is for INVISIBLE
        }

        @Override
        protected void onProgressUpdate(String... text) {
            Log.d("AdsMain", "onProgressUpdate");
            Log.d("AdsMain", "onProgressUpdate");
            // finalResult.setText(text[0]);
            // Things to be done while execution of long running operation is in
            // progress. For example updating ProgessDialog
        }
        private String GetResponseData(String DataIn)
        {
            String retVal="";
            String startText= pXMLNS;
            String endText="</string>";
            try 		{
                int startPos=DataIn.indexOf(startText);
                if (startPos>-1)
                {
                    startPos=startPos+startText.length();
                }
                int endPos=DataIn.indexOf(endText,startPos);
                retVal=DataIn.substring(startPos,endPos);
                retVal= GeneralFunctions01.Text.EncodedHTMLToText(retVal);
            }
            catch (Exception e)
            {
                Log.d("AdsMain", e.getMessage());
            }
            return retVal;
        }

    }
}
