package com.mc2techservices.gcbg;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.Random;
import java.util.UUID;

import javax.crypto.SecretKey;
import javax.crypto.SecretKeyFactory;
import javax.crypto.spec.DESKeySpec;


import android.R.bool;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.Configuration;
import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Matrix;
import android.graphics.BitmapFactory.Options;
import android.graphics.drawable.Drawable;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Environment;
import android.os.StrictMode;
import android.util.DisplayMetrics;
import android.util.Log;
import android.view.View;
import android.widget.TableRow;

public class GeneralFunctions extends Activity{
	public static String GetImageName(String DataIn, String Delimiter) 
    {
		String RetVal="";
    	String[] result = DataIn.split(Delimiter);
    	if (result.length > 0)
    	{
    		int test=result.length-1;
    		RetVal=result[test];
    	}
    	return RetVal;
    }
	public static List<File> GetFileList(File parentDir) {
	    ArrayList<File> inFiles = new ArrayList<File>();
	    File[] files = parentDir.listFiles();
	    for (File file : files) {
	        if (file.isDirectory()) {
	            inFiles.addAll(GetFileList(file));
	        } else {
	            if(file.getName().length()>0){
	                inFiles.add(file);
	            }
	        }
	    }
	    return inFiles;
	}
	public static boolean isAndroidEmulator() {
	    String model = Build.MODEL;
	    String product = Build.PRODUCT;
	    boolean isEmulator = false;
	    if (product != null) {
	        isEmulator = product.equals("sdk") || product.contains("_sdk") || product.contains("sdk_");
	    }
	    Log.d("CJM App", "isEmulator=" + isEmulator);
	    return isEmulator;
	}	

    public static long DaysBetweenDate(String StartDate, String EndDate)
    {
    	if (StartDate==null)
    	{
    		String date = new SimpleDateFormat("yyyy-MM-dd").format(new Date());
    		Calendar c = Calendar.getInstance();
    		System.out.println("Current time => " + c.getTime());
    		SimpleDateFormat df = new SimpleDateFormat("yyyy-MM-dd");
    		StartDate = df.format(c.getTime());
    	}
    	Date d1=StringToDate(StartDate);
    	Date d2=StringToDate(EndDate);
    	long diff = d2.getTime() - d1.getTime();
    	long diffDays = diff / (24 * 60 * 60 * 1000);
    	return diffDays;
    }
    public static String ModifyDays(String DateIn, int DaysToChange)
    {
        String dt = DateIn; //"2012-01-04";  // Start date
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
        Calendar c = Calendar.getInstance();
        try {
            c.setTime(sdf.parse(dt));
        } catch (Exception e) {
            e.printStackTrace();
        }
        c.add(Calendar.DATE, DaysToChange);  // number of days to add, can also use Calendar.DAY_OF_MONTH in place of Calendar.DATE
        SimpleDateFormat sdf1 = new SimpleDateFormat("yyyy-MM-dd");
        String output = sdf1.format(c.getTime()); 
        return output;
    }
    public static String GetCurrentDateTime()
    {
    	   SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
		   Date date = new Date(); 
		   return sdf.format(date); 
    }
    public static long SecondsBetweenDateTimes(String dateStart, String dateStop)
    {
    	SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");  
    	long diffSeconds=0;
    	Date d1 = null;
    	Date d2 = null;
    	try {
    	    d1 = format.parse(dateStart);
    	    d2 = format.parse(dateStop);

    	// Get msec from each, and subtract.
    	long diff = d2.getTime() - d1.getTime();
    	diffSeconds = diff / 1000;         
    	System.out.println("Time in seconds: " + diffSeconds + " seconds.");
    	} catch (Exception e) {
    	    e.getMessage();
    	}    
    	return diffSeconds;
    }
    public static String TimeBetweenDateTimes(String dateStart, String dateStop)
    {
    	String retVal="";
		Date d1 = null;
		Date d2 = null;
		SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
		try {
			d1 = format.parse(dateStart);
			d2 = format.parse(dateStop);

			//in milliseconds
			long diff = d2.getTime() - d1.getTime();

			long diffSeconds = diff / 1000 % 60;
			long diffMinutes = diff / (60 * 1000) % 60;
			long diffHours = diff / (60 * 60 * 1000) % 24;
			long diffDays = diff / (24 * 60 * 60 * 1000);

			retVal = diffDays + " days, " + diffHours + " hours, " + diffMinutes + " minutes, " + diffSeconds + " seconds";
			System.out.print(diffDays + " days, ");
			System.out.print(diffHours + " hours, ");
			System.out.print(diffMinutes + " minutes, ");
			System.out.print(diffSeconds + " seconds.");

		} catch (Exception e) {
			e.printStackTrace();
		}
    	return retVal;
    }
    public static String GetCurrentDate()
    {
		   DateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd");
		   Date date = new Date(); 
		   return dateFormat.format(date); 
    }
    public static String DateToString(Date DateIn)
    {
		   DateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd");
		   return dateFormat.format(DateIn); 
    }
    public static Date StringToDate(String StringDateIn)
    {
    	Date date=null;
    	String dtStart = "2010-10-15T09:27:37Z";  
    	dtStart = StringDateIn;
    	//dtStart = "2010-10-15T09:27:37Z";  
    	
    	SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd");  //SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'"); 
    	try {  
    	    date = format.parse(dtStart);  
    	    System.out.println(date);  
    	} catch (Exception e) {  
    	    // TODO Auto-generated catch block  
    	    e.printStackTrace();  
    	}
    	return date;
    }
    private static final String ALLOWED_CHARACTERS ="0123456789qwertyuiopasdfghjklzxcvbnm";

    public static String PMGetRandomString(final int sizeOfRandomString)
      {
      final Random random=new Random();
      final StringBuilder sb=new StringBuilder(sizeOfRandomString);
      for(int i=0;i<sizeOfRandomString;++i)
        sb.append(ALLOWED_CHARACTERS.charAt(random.nextInt(ALLOWED_CHARACTERS.length())));
      return sb.toString();
      }
	public static String PMPurchaseOverride(String ValIn) {
		int total=0;
		for (int i = 0; i < ValIn.length(); i++) {
			char mychar = ValIn.charAt(i); //char="a"
			int ascii = (int)mychar;
			int addval=GetAdjustedValFromASCII(ascii);
			total=total+addval;
		}
		return String.valueOf(total);
	}
    private static int GetAdjustedValFromASCII(int ASCIIin)
    {
        //0=48...9=57
        //A=65...H=72
        //a=97...h=104
        int ASCIItoRet = 0;
        if (ASCIIin == 48) { ASCIItoRet = ASCIIin + 6; }
        else if (ASCIIin == 49) { ASCIItoRet = ASCIIin + 63; }
        else if (ASCIIin == 50) { ASCIItoRet = ASCIIin + 21; }
        else if (ASCIIin == 51) { ASCIItoRet = ASCIIin + 97; }
        else if (ASCIIin == 52) { ASCIItoRet = ASCIIin + 43; }
        else if (ASCIIin == 53) { ASCIItoRet = ASCIIin + 31; }
        else if (ASCIIin == 54) { ASCIItoRet = ASCIIin + 88; }
        else if (ASCIIin == 55) { ASCIItoRet = ASCIIin + 55; }
        else if (ASCIIin == 56) { ASCIItoRet = ASCIIin + 14; }
        else if (ASCIIin == 57) { ASCIItoRet = ASCIIin + 3; }
        else if (ASCIIin == 65) { ASCIItoRet = ASCIIin + 70; }
        else if (ASCIIin == 66) { ASCIItoRet = ASCIIin + 19; }
        else if (ASCIIin == 67) { ASCIItoRet = ASCIIin + 76; }
        else if (ASCIIin == 68) { ASCIItoRet = ASCIIin + 43; }
        else if (ASCIIin == 69) { ASCIItoRet = ASCIIin + 32; }
        else if (ASCIIin == 70) { ASCIItoRet = ASCIIin + 93; }
        else if (ASCIIin == 71) { ASCIItoRet = ASCIIin + 82; }
        else if (ASCIIin == 72) { ASCIItoRet = ASCIIin + 65; }
        else if (ASCIIin == 97) { ASCIItoRet = ASCIIin + 14; }
        else if (ASCIIin == 97) { ASCIItoRet = ASCIIin + 73; }
        else if (ASCIIin == 97) { ASCIItoRet = ASCIIin + 92; }
        else if (ASCIIin == 100) { ASCIItoRet = ASCIIin + 32; }
        else if (ASCIIin == 101) { ASCIItoRet = ASCIIin + 53; }
        else if (ASCIIin == 102) { ASCIItoRet = ASCIIin + 44; }
        else if (ASCIIin == 103) { ASCIItoRet = ASCIIin + 73; }
        else if (ASCIIin == 104) { ASCIItoRet = ASCIIin + 69; }
        return ASCIItoRet;
    }

	public void testMethodView(View view) {
	    System.out.println("clicked");
	}
	
	public static String RunningStatusReporter(String oldText, String newText)
	{
		int amnt=oldText.length();
		if (amnt>500)
		{
			amnt=500;
		}
		String finalText=oldText.substring(0,amnt);
		finalText=newText + System.getProperty("line.separator") + finalText;
//		Log.d("YMCA_Check_In", newText);
		return finalText;
	}
	public static Drawable loadImageFromFilesystem(Context context, String filename, int originalDensity) {
	    Drawable drawable = null;
	    InputStream is = null;

	    // set options to resize the image
	    Options opts = new BitmapFactory.Options();
	    opts.inDensity = originalDensity;

	    try {
	    	FileInputStream fis = new FileInputStream (new File(filename));
	    	drawable = Drawable.createFromResourceStream(context.getResources(), null, fis, filename, opts);         
	    } catch (Exception e) {
			Log.d("YMCA_Check_InErr", e.getMessage());
		} finally {
	      if (is != null) {
	        try {
	          is.close();
	        } catch (Throwable e1) {
	          // ingore
	        }
	      }
	    }
	    return drawable;
	  }
	
	public static String getVersion(Context pContext) {
	    String vn = "";
	    int vc = 0;
	    try {
	        vn = pContext.getPackageManager().getPackageInfo(pContext.getPackageName(), 0).versionName;
	        vc = pContext.getPackageManager().getPackageInfo(pContext.getPackageName(), 0).versionCode;
	    } catch (Exception e) {
	        // Huh? Really?
	    }
	    //String temp=vn + "." + String.valueOf(vc);
	    String temp=vn;
	    return temp;
	}
	public static int getVersionCode(Context pContext) {
	    int vc = 0;
	    try {
	        vc = pContext.getPackageManager().getPackageInfo(pContext.getPackageName(), 0).versionCode;
	    } catch (Exception e) {
	        // Huh? Really?
	    }
	    //String temp=vn + "." + String.valueOf(vc);
	    return vc;
	}
    public static void PMAlert(Context pContext, String pMessage) 
    {
    	AlertDialog.Builder builder = new AlertDialog.Builder(pContext);
    	builder.setTitle("Message")
    	    .setMessage(pMessage)
    	    .setNeutralButton("OK", null);
    	AlertDialog dialog = builder.create();
    	dialog.show();
    }
    public static void WriteSharedPreference(Context ctxt, String PrefName, String ValToWrite)
	{
    	SharedPreferences prefs = ctxt.getSharedPreferences(
			      "com.mc2techservices.YMCA_Check_In", Context.MODE_PRIVATE);
		prefs.edit().putString(PrefName, ValToWrite).commit();
	}
    public static String ReadSharedPreference(Context ctxt, String PrefName)
	{
    	String retVal="<No Value Defined>";
		SharedPreferences prefs = ctxt.getSharedPreferences(
			      "com.mc2techservices.YMCA_Check_In", Context.MODE_PRIVATE);

		retVal = prefs.getString(PrefName, retVal);
		return retVal;
	}
    public static void DeleteSharedPreference(Context ctxt, String PrefName)
    {
    	SharedPreferences preferences = ctxt.getSharedPreferences("com.mc2techservices.YMCA_Check_In", 0);
    	preferences.edit().remove(PrefName).commit();
    }
    public static String ReadSharedPreference(Context ctxt, String PrefName,boolean RetEmptyVals)
	{
    	String retVal="<No Value Defined>";
		SharedPreferences prefs = ctxt.getSharedPreferences(
			      "com.mc2techservices.YMCA_Check_In", Context.MODE_PRIVATE);

		retVal = prefs.getString(PrefName, retVal);
		if (RetEmptyVals==true)
		{
			if (retVal=="<No Value Defined>") retVal="";
		}
		return retVal;
	}
    public static String GetResonseData(String DataIn)
	{
    	String retVal="";
    	String startText="xmlns=\"webservice.mc2techservices.com\">";
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
			Log.d("YMCA_Check_InErr", e.getMessage());
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
    
    
    
    public static Boolean FileExists(String pProposedFile)    
    {
        File file = new File(pProposedFile);
        if(file.exists())      
        {
        	return true;
        }
        else
        {
        	return false;
        }
    }
    
    public static Boolean DirectoryExists(String pProposedDirectory)
    {
        File f = new File(pProposedDirectory);
        if(f.isDirectory()) {
			Log.d("GeneralFunctions", "DirectoryExists: True");
        	return true;
        }
        else
        {
			Log.d("GeneralFunctions", "DirectoryExists: False");
        	return false;
        }
    }

    
    public static void DirectoryHelper(String pProposedDirectory)
    {
        try {
	    	File folder = new File(pProposedDirectory);
	    	boolean success = true;
			//Log.d("YMCA_Check_In", "Checking for: "+fileLoc);
	    	if (!folder.exists()) 
	    	{
	    	    success = folder.mkdirs();
	    	}
	    	else
	    	{
	    		Log.d("YMCA_Check_In", "DirectoryHelper Existed");	    		
	    	}
	    	if (success) {
	    		Log.d("YMCA_Check_In", "DirectoryHelper OK");
	    	} else {
	    		Log.d("YMCA_Check_In", "DirectoryHelper Fail");
	    	}
        }
		catch (Exception e)
		{
			Log.d("YMCA_Check_InErr", e.getMessage());
		}

    }
    
    public static void ProfileWrite(String PathAndNameOfFile, String WhatToWrite)
	{
		try {
			BufferedWriter bufferedWriter = new BufferedWriter(new FileWriter(new 
                    File(PathAndNameOfFile)));
			bufferedWriter.write("l1");
			bufferedWriter.write("l2");
			bufferedWriter.write("l3");
			bufferedWriter.close();
			bufferedWriter = new BufferedWriter(new FileWriter(new 
                    File(PathAndNameOfFile + "b")));
			bufferedWriter.write("l1");
			bufferedWriter.close();
			} catch (Exception e) {
			Log.d("YMCA_Check_InErr", e.getMessage());
		}
	}

    public static String CreateUUID()
    {
        UUID uuid = UUID.randomUUID();
        String randomUUIDString = uuid.toString();
        return randomUUIDString;
    }
    
	public static String[] ProfileRead(String PathAndNameOfFile)
	{
		String[] RetVal=null;
		try {
			 BufferedReader bufferedReader = new BufferedReader(new FileReader(new 
                     File(PathAndNameOfFile)));
				String read;
				StringBuilder builder = new StringBuilder("");
				while((read = bufferedReader.readLine()) != null){
				  builder.append(read);
			    	RetVal = read.split("Undefined");
				}
			Log.d("YMCA_Check_In", builder.toString());
			bufferedReader.close();		
		} catch (Exception e) {
			Log.d("YMCA_Check_InErr", e.getMessage());
		}
		return RetVal;
	}

    public static void DownloadImageFromURL(String pURL)
    {
    	DownloadImageFromURL(pURL,null);
    }
    public static void DownloadImageFromURL(String pURL, Context ctx)
    {
        try {
        	String imageName=GeneralFunctions.GetImageName(pURL, "/");
            String fileLoc=GlobalClass.gloFileLoc+"/"+ imageName;

        	InputStream input;
            URL url = new URL (pURL);
            input = url.openStream();
            byte[] buffer = new byte[1500];

            Log.d("YMCA_Check_In", "Writing: "+fileLoc);
			OutputStream output = new FileOutputStream (fileLoc);
            try 
            {     
                int bytesRead = 0;
                while ((bytesRead = input.read(buffer, 0, buffer.length)) >= 0) 
                {
                    output.write(buffer, 0, bytesRead);
                }
            }
	        catch (Exception e1) {
	            // TODO Auto-generated catch block
	        	Log.d("YMCA_Check_In", e1.getMessage());
	        }
            finally 
            {
                output.close();
                buffer=null;
            }
        } 
        catch(Exception e) 
        {
        	Log.d("YMCA_Check_In", e.getMessage());        	
			Log.d("YMCA_Check_InErr", e.toString());
        }
    }
    public static int GetHighestID()
    {
    	int RetVal=0;
    	for (int i = 9; i>0; i--) 
	    {
	    	File f = new File(GlobalClass.gloFileLoc+ "/Profiles/"+i);
            if(f.exists())
            {
            	RetVal=i;
            	break;
	    	}
	    }
    	return RetVal;
    }
    

	

    public static void AllowNetworkOnMainThreadException()
	{
		if (android.os.Build.VERSION.SDK_INT > 9) {
			StrictMode.ThreadPolicy policy = 
			        new StrictMode.ThreadPolicy.Builder().permitAll().build();
			StrictMode.setThreadPolicy(policy);
			}
	}
    
    public static String NonAsyncWebCall(String pURL, String pParams)
	{
		String resp="";
		String USER_AGENT = "Mozilla/5.0";
		if (android.os.Build.VERSION.SDK_INT > 9) {
			StrictMode.ThreadPolicy policy = 
			        new StrictMode.ThreadPolicy.Builder().permitAll().build();
			StrictMode.setThreadPolicy(policy);
			}
		
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

				Log.d("YMCA_Check_In", "\nSending 'POST' request to URL : " + url);
				Log.d("YMCA_Check_In","Post parameters : " + urlParameters);
				Log.d("YMCA_Check_In","Response Code : " + responseCode);
		 
				BufferedReader in = new BufferedReader(
				        new InputStreamReader(con.getInputStream()));
				String inputLine;
				StringBuffer response = new StringBuffer();
		 
				while ((inputLine = in.readLine()) != null) {
					response.append(inputLine);
				}
				in.close();
		 
				//print result
				System.out.println(response.toString());
				Log.d("YMCA_Check_In", response.toString());
				resp=response.toString();
		}
		catch (Exception e)
		{
			Log.d("YMCA_Check_InErr", e.getMessage());
		}
		return resp;
	}   

    public class AsyncWebCallRunner extends AsyncTask<String, String, String> {

		private String resp;
		private String USER_AGENT = "Mozilla/5.0";

		@Override
		protected String doInBackground(String... params) {
			publishProgress("Sleeping..."); // Calls onProgressUpdate()
			try {
				String url = params[0];
				String urlParameters = params[1];
				URL obj = new URL(url);
				// Authenticator.setDefault(new ProxyAuthenticator("chmitro",
				// "@Fiserv026"));
				// System.setProperty("http.proxyHost", "10.141.105.1");
				// System.setProperty("http.proxyPort", "9090");

				HttpURLConnection con = (HttpURLConnection) obj
						.openConnection();
				// HttpsURLConnection con = (HttpsURLConnection)
				// obj.openConnection();

				con.setRequestMethod("POST");
				con.setRequestProperty("User-Agent", USER_AGENT);
				con.setRequestProperty("Accept-Language", "en-US,en;q=0.5");
				con.setDoOutput(true);
				Log.d("YMCA_Check_In", "Ready to send...: " + url);

				try {
					DataOutputStream wr = new DataOutputStream(
							con.getOutputStream());
					wr.writeBytes(urlParameters);
					wr.flush();
					wr.close();
				} catch (Exception e) {
					Log.d("YMCA_Check_In", "Error 001");
					Log.d("YMCA_Check_In", e.getMessage());
					resp = "CallWebService Exception Occured 1";
				}

				int responseCode = con.getResponseCode();

				Log.d("YMCA_Check_In",
						"\nSending 'POST' request to URL : " + url);
				Log.d("YMCA_Check_In", "Post parameters : "
						+ urlParameters);
				Log.d("YMCA_Check_In", "Response Code : " + responseCode);

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
				Log.d("YMCA_Check_In", response.toString());
				resp = response.toString();
			} catch (Exception e) {
				Log.d("YMCA_Check_In", "Error 002");
				Log.d("YMCA_Check_In", e.getMessage());
				resp = "CallWebService Exception Occured 2";
			}
			return resp;
		}

		@Override
		protected void onPostExecute(String result) {
			Log.d("YMCA_Check_In", "onPostExecute");
			Log.d("YMCA_Check_In", result);
			String Response = GeneralFunctions.GetResonseData(result); // "http://YMCA_Check_In.mc2techservices.com/Barcodes/9876543321.jpg";
																		// //
			Log.d("YMCA_Check_In", "onPostExecute Response 1:" + Response);
			GeneralFunctions.DownloadImageFromURL(Response, null);
			Log.d("YMCA_Check_In", "onPostExecute Response 2:" + Response);
		}

		@Override
		protected void onPreExecute() {
			Log.d("YMCA_Check_In", "onPreExecute");
			// Things to be done before execution of long running operation. For
			// example showing ProgessDialog
		}

		@Override
		protected void onProgressUpdate(String... text) {
			Log.d("YMCA_Check_In", "onProgressUpdate");
			// finalResult.setText(text[0]);
			// Things to be done while execution of long running operation is in
			// progress. For example updating ProgessDialog
		}
	}
    public class DownloadFilesTask extends AsyncTask<URL, Integer, Long> {
        protected Long doInBackground(URL... urls) {
            int count = urls.length;
            long totalSize = 0;
			Log.d("DownloadFilesTask", "Start");
            for (int i = 0; i < count; i++) {
                //totalSize += Downloader.downloadFile(urls[i]);
                publishProgress((int) ((i / (float) count) * 100));
                // Escape early if cancel() is called
                if (isCancelled()) break;
            }
            return totalSize;
        }

        protected void onProgressUpdate(Integer... progress) {
			Log.d("DownloadFilesTask", "onProgressUpdate");
            //setProgressPercent(progress[0]);
        }

        protected void onPostExecute(Long result) {
			Log.d("DownloadFilesTask", "onPostExecute");
            //showDialog("Downloaded " + result + " bytes");
        }
    }
}