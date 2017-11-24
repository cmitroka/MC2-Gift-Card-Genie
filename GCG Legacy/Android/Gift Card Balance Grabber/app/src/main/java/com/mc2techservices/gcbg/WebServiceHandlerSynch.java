package com.mc2techservices.gcbg;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.apache.http.util.EntityUtils;

import android.os.StrictMode;
import android.util.Log;

public class WebServiceHandlerSynch {
	//private String xmlns = "xmlns=\"gcg.mc2techservices.com\">";
	//private String WSURL = "http://192.168.0.189/AdminSite/GCGWebWS.asmx";

	private static String xmlns = GlobalClass.gloxmlns;
	private static String WSURL = GlobalClass.gloWebServiceURL;

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
			Log.d("WebServiceHandlerSynch Error", e.getMessage());
		}
		return retVal;
	}

    public static String DoWSCall(String pOperation, String pParams)
    {
    	String retVal="";
    	StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
    	StrictMode.setThreadPolicy(policy); 
    	HttpPost httppost = new HttpPost(WSURL + "/" + pOperation);
        HttpClient httpclient = new DefaultHttpClient();  

        try {
            String[] separated = pParams.split("&");
            int ArraySize=separated.length;
        	List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(ArraySize);
            for (int i = 0; i < ArraySize; i++) {
                String[] keyval = separated[i].split("=");
                String pKey=keyval[0];
                String pVal=keyval[1];
            	nameValuePairs.add(new BasicNameValuePair(pKey, pVal));
			}            
            httppost.setEntity(new UrlEncodedFormEntity(nameValuePairs));

            // Execute HTTP Post Request
            HttpResponse response = httpclient.execute(httppost);
            HttpEntity resEntity = response.getEntity();  
            if (resEntity != null) {    
            	
            	String temp=EntityUtils.toString(resEntity);
            	Log.i("WebServiceHandlerSynch FULLRESPONSE",temp);
            	retVal=GetResonseData(temp);
            	Log.i("WebServiceHandlerSynch RESPONSE",retVal);
            }

        } catch (ClientProtocolException e) {
            e.printStackTrace();

        } catch (IOException e) {
        	e.printStackTrace();
        }
        return retVal;
    }

}
