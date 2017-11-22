package com.mc2techservices.gcg;

import android.os.AsyncTask;
import android.util.Log;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;

/**
 * Created by Administrator on 11/8/2017.
 */

public class WebComm {
    public String wcURL;
    public String wcWebResponse;
    private String resp;
    private String isOnline;
    private static String xmlns;
    public WebComm(String pXMLNS)
    {
        xmlns=pXMLNS;
        wcWebResponse = "...";
    }
    private static String GetResonseData(String DataIn)
    {
        String retVal="";
        String startText=xmlns;
        String endText="</string>";
        try
        {
            int startPos=DataIn.indexOf(startText);
            if (startPos>-1)
            {
                startPos=startPos+startText.length();
            }
            int endPos=DataIn.indexOf(endText,startPos);
            retVal=DataIn.substring(startPos,endPos);
            retVal=EncodedHTMLToText(retVal);
        }
        catch (Exception e)
        {
            Log.d("barcodefastpassErr", e.getMessage());
        }
        return retVal;
    }

    private static String EncodedHTMLToText(String str)
    {
        String newstr=str;
        newstr=newstr.replaceAll("&lt;", "<");
        newstr=newstr.replaceAll("&gt;", ">");
        newstr=newstr.replaceAll("&amp;", "&");
        newstr=newstr.replaceAll("&quot;", "\"");
        newstr=newstr.replaceAll("&#39;", "'");
        return newstr;
    }
    private String GetFileName(String pURLIn)
    {
        String[] pPieces=pURLIn.split("/");
        String pLastPiece=pPieces[pPieces.length-1];
        return pLastPiece;
    }


    public void ExecuteWebRequest(String pURL, String pParams) {
        new AsyncTask<String, String, String>() {
            @Override
            protected String doInBackground(String... params) {
                wcWebResponse = "...";
                publishProgress("Sleeping..."); // Calls onProgressUpdate()
                try {
                    String url = params[0];
                    String urlParameters = params[1];
                    URL obj = new URL(url);
                    HttpURLConnection con = (HttpURLConnection) obj
                            .openConnection();
                    con.setRequestMethod("POST");
                    con.setRequestProperty("User-Agent", "Chrome");
                    con.setRequestProperty("Accept-Language", "en-US,en;q=0.5");
                    con.setDoOutput(true);
                    Log.d("GF AsyncWebCall", "Ready to send...: " + url);

                    try {
                        DataOutputStream wr = new DataOutputStream(
                                con.getOutputStream());
                        wr.writeBytes(urlParameters);
                        wr.flush();
                        wr.close();
                    } catch (Exception e) {
                        Log.d("GF AsyncWebCall", "Error 001");
                        Log.d("GF AsyncWebCall", e.getMessage());
                        resp = "CallWebService Exception Occured 1";
                    }

                    int responseCode = con.getResponseCode();

                    Log.d("GF AsyncWebCall",
                            "\nSending 'POST' request to URL : " + url);
                    Log.d("GF AsyncWebCall", "Post parameters : "
                            + urlParameters);
                    Log.d("GF AsyncWebCall", "Response Code : " + responseCode);

                    BufferedReader in = new BufferedReader(new InputStreamReader(
                            con.getInputStream()));
                    String inputLine;
                    StringBuffer response = new StringBuffer();

                    while ((inputLine = in.readLine()) != null) {
                        response.append(inputLine);
                    }
                    in.close();

                    // print result
                    System.out.println(response.toString());
                    Log.d("GF AsyncWebCall", response.toString());
                    resp = response.toString();
                } catch (Exception e) {
                    Log.d("GF AsyncWebCall", "Error 002");
                    Log.d("GF AsyncWebCall", e.getMessage());
                    resp = "CallWebService Exception Occured 2";
                }
                return resp;
            }

            @Override
            protected void onPostExecute(String result) {
                //This does come back; what to do with it/how to handle it... not sure.
                Log.d("GF AsyncWebCall", "onPostExecute");
                Log.d("GF AsyncWebCall", "onPostExecute Raw Result:" + result);
                String Response = GetResonseData(result); // "http://AsyncWebCall.mc2techservices.com/Barcodes/9876543321.jpg";
                Log.d("GF AsyncWebCall", "onPostExecute Clean Response" + Response);
                wcWebResponse = Response;
            }

            @Override
            protected void onPreExecute() {
                Log.d("GF AsyncWebCall", "onPreExecute");
            }

            @Override
            protected void onProgressUpdate(String... progress) {
                Log.d("GF AsyncWebCall", "onProgressUpdate");
            }
        }.execute(pURL, pParams);
   }

    public void DownloadFile(final String pURLIn, final String pWriteToLocIn, String pExtarInfo1, String pExtarInfo2) {
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
                    String pWriteFile=GetFileName(pURLIn);
                    OutputStream output = new FileOutputStream (pWriteToLocIn+"/"+pWriteFile);
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
