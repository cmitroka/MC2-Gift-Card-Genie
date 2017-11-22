package com.mc2techservices.gcbg;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

import android.os.AsyncTask;
import android.util.Log;

public class WebServiceHandlerASynch extends AsyncTask<String, String, String>{
	private String resp;
	//private String xmlns = "xmlns=\"gcg.mc2techservices.com\">";
	//private String WSURL = "http://192.168.0.189/AdminSite/GCGWebWS.asmx";

	private String xmlns = GlobalClass.gloxmlns;
	private String WSURL = GlobalClass.gloWebServiceURL;
	
	private String USER_AGENT = "Mozilla/5.0";

	@Override
	protected String doInBackground(String... params) {
		publishProgress("Sleeping..."); // Calls onProgressUpdate()
		try {
			String pOperation = params[0];
			String urlParameters = params[1];
			String url = WSURL + "/" + pOperation;
			URL obj = new URL(url);
			HttpURLConnection con = (HttpURLConnection) obj
					.openConnection();
			con.setRequestMethod("POST");
			con.setRequestProperty("User-Agent", USER_AGENT);
			con.setRequestProperty("Accept-Language", "en-US,en;q=0.5");
			con.setDoOutput(true);
			Log.d("WebServiceHandlerASynch", "Ready to send...: " + url);

			try {
				DataOutputStream wr = new DataOutputStream(
						con.getOutputStream());
				wr.writeBytes(urlParameters);
				wr.flush();
				wr.close();
			} catch (Exception e) {
				Log.d("WebServiceHandlerASynch", "Error 001");
				Log.d("WebServiceHandlerASynch", e.getMessage());
				resp = "CallWebService Exception Occured 1";
			}

			int responseCode = con.getResponseCode();
			Log.d("WebServiceHandlerASynch",
					"\nSending 'POST' request to URL : " + url);
			Log.d("WebServiceHandlerASynch", "Post parameters : "
					+ urlParameters);
			Log.d("WebServiceHandlerASynch", "Response Code : " + responseCode);

			BufferedReader in = new BufferedReader(new InputStreamReader(
					con.getInputStream()));
			String inputLine;
			StringBuffer response = new StringBuffer();

			while ((inputLine = in.readLine()) != null) {
				response.append(inputLine);
			}
			in.close();

			System.out.println(response.toString());
			Log.d("WebServiceHandlerASynch", response.toString());
			resp = response.toString();
		} catch (Exception e) {
			Log.d("WebServiceHandlerASynch", "Error 002");
			Log.d("WebServiceHandlerASynch", e.getMessage());
			resp = "CallWebService Exception Occured 2";
		}
		return resp;
	}

	@Override
	protected void onPostExecute(String result) {
		Log.d("WebServiceHandlerASynch", "onPostExecute");
		Log.d("WebServiceHandlerASynch", result);
		String Response = GetResonseData(result); // "http://WebServiceHandlerASynch.mc2techservices.com/Barcodes/9876543321.jpg";
																	// //
		Log.d("WebServiceHandlerASynch", "onPostExecute Response 1:" + Response);
		Log.d("WebServiceHandlerASynch", "onPostExecute Response 2:" + Response);
	}

	@Override
	protected void onPreExecute() {
		Log.d("WebServiceHandlerASynch", "onPreExecute");
		// Things to be done before execution of long running operation. For
		// example showing ProgessDialog
	}

	@Override
	protected void onProgressUpdate(String... text) {
		Log.d("WebServiceHandlerASynch", "onProgressUpdate");
		// finalResult.setText(text[0]);
		// Things to be done while execution of long running operation is in
		// progress. For example updating ProgessDialog
	}
	
	public String GetResonseData(String DataIn)
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
			Log.d("WebServiceHandlerASynch", e.getMessage());
		}
		return retVal;
	}
	public static String EncodedHTMLToText(String str)
	{
		String newstr=str;
		newstr=newstr.replaceAll("&lt;", "<");
		newstr=newstr.replaceAll("&gt;", ">");
		newstr=newstr.replaceAll("&amp;", "&");
		newstr=newstr.replaceAll("&quot;", "\"");
		newstr=newstr.replaceAll("&#39;", "'");
		return newstr;
	}

}
