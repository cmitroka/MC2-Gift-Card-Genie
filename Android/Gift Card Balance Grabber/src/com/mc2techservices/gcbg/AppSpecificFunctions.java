package com.mc2techservices.gcbg;

import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.io.Writer;
import java.net.URL;
import java.net.URLConnection;
import java.net.HttpURLConnection;
import javax.net.ssl.HttpsURLConnection;


import android.annotation.SuppressLint;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.StrictMode;
import android.util.Log;
import android.preference.PreferenceManager;

public class AppSpecificFunctions {
	public static String PMSplit(String DataIn, String Delimiter) {
		String RetVal = "PMSplit Error";
		String[] result = DataIn.split(Delimiter);
		if (result.length > 0) {
			RetVal = result[0];
		}
		return RetVal;
	}

	public static String PMGetContent(String DataIn) {
		String RetVal = "PMSplit Error";
		Integer loc1 = DataIn.indexOf("gcg.mc2techservices.com\">");
		loc1 = loc1 + 25;
		Integer loc2 = DataIn.indexOf("</string>");
		RetVal = DataIn.substring(loc1, loc2);
		return RetVal;
	}

	public static String PMMakeKey(String DataIn) {
		String pKey = DataIn.replaceAll("[^0-9]", "");
		long pKeyA = Long.parseLong(pKey);
		float pKeyB = (pKeyA / .0526F); // This is the key
		String pKeyC = String.valueOf(pKeyB);
		pKey = pKeyC.replaceAll("[^0-9]", "");
		pKey = pKey.substring(0, 3);
		return pKey;
	}

	public static String PMValidateInfo(String pPassword, String pPaswwordVer,
			String pLogin) {
		String retVal = "";
		if (pPassword.equals(pPaswwordVer) == false) {
			retVal = retVal
					+ "The password and verification password didn't match.\n";
		}
		if (pLogin.length() < 4) {
			retVal = retVal + "The username has to be at least 4 characters.\n";
		}
		if (pPassword.length() < 4) {
			retVal = retVal + "The password has to be at least 4 characters.\n";
		}
		if (retVal.length() > 1) {
			retVal = retVal.substring(0, retVal.length() - 1);
		}
		return retVal;
	}

	public static void WriteSharedPreference(Context ctxt, String PrefName,
			String ValToWrite) {
		SharedPreferences prefs = ctxt.getSharedPreferences(
				"com.mc2techservices.gcg", Context.MODE_PRIVATE);
		prefs.edit().putString(PrefName, ValToWrite).commit();
	}

	public static String ReadSharedPreference(Context ctxt, String PrefName) {
		String retVal = "<No Value Defined>";
		SharedPreferences prefs = ctxt.getSharedPreferences(
				"com.mc2techservices.gcg", Context.MODE_PRIVATE);

		retVal = prefs.getString(PrefName, retVal);
		return retVal;
	}

	public static String GetResonseData(String DataIn) {
		String retVal = "";
		String startText = "xmlns=\"gcg.mc2techservices.com\">";
		String endText = "</string>";
		try {
			int startPos = DataIn.indexOf(startText);
			if (startPos > -1) {
				startPos = startPos + startText.length();
			}
			int endPos = DataIn.indexOf(endText, startPos);
			retVal = DataIn.substring(startPos, endPos);
		} catch (Exception e) {
			Log.d("GCG", "e1");
		}
		return retVal;
	}

	public static String NonAsyncWebCall(String pURL, String pParams) {
		String resp;
		String USER_AGENT = "Mozilla/5.0";
		try {
			String url = pURL;
			String urlParameters = pParams;

			URL obj = new URL(url);
			HttpURLConnection con = (HttpURLConnection) obj.openConnection();

			con.setRequestMethod("POST");
			con.setRequestProperty("User-Agent", USER_AGENT);
			con.setRequestProperty("Accept-Language", "en-US,en;q=0.5");
			con.setDoOutput(true);
			DataOutputStream wr = new DataOutputStream(con.getOutputStream());
			wr.writeBytes(urlParameters);
			wr.flush();
			wr.close();

			int responseCode = con.getResponseCode();

			Log.d("WS Call Detail", "\nSending 'POST' request to URL : " + url);
			Log.d("WS Call Detail", "Post parameters : " + urlParameters);
			Log.d("WS Call Detail", "Response Code : " + responseCode);

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
			Log.d("GCG Good Result", response.toString());
			resp = response.toString();
		} catch (Exception e) {
			Log.d("GCG", "e1");
			resp = "CallWebService Exception Occured 1";
		}
		return resp;
	}

}
