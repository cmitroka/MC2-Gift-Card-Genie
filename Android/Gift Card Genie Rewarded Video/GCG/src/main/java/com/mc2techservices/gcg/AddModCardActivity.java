package com.mc2techservices.gcg;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.text.InputType;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.mc2techservices.ads.AdsSetup;

import java.util.Timer;
import java.util.TimerTask;


public class AddModCardActivity extends Activity {
	int GloUCDID;
	String GloCardType;
	EditText txtGCICardType;
	EditText txtGCIURL;
	EditText txtGCILogin;
	EditText txtGCIPassword;
	EditText txtCDCardNumber;
	EditText txtCDCardPIN;
	EditText txtCDOtherInfo;
	Button cmdLookup;
	WebComm wc2;
	int pAmntOfLookups;
	int wc2cnt;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_add_mod_card);
		String SGloUCDID=(getIntent().getStringExtra("CardID"));
		GloCardType=(getIntent().getStringExtra("CardType"));
		cmdLookup= (Button)findViewById(R.id.cmdLookup);
		pAmntOfLookups=0;
		SetupFields();
		ConfigGeneralCardInfo();
		EnableCardGeneralInfo(false);
		GloUCDID=GeneralFunctions01.Conv.StringToInt(SGloUCDID);
		if (GloUCDID>0)
		{
			ConfigExistingCard();
			if (!ShowHistory()) HideBalanceHistory();
			CheckToEnableCardGeneralInfo();
			GetLookupAmntInfo();
		}
		else if (GloUCDID==0)
		{
			HideBalanceHistory();
			HideLookupAndAdjustBalance();
		}
		else if (GloUCDID==-1)
		{
			EnableCardGeneralInfo(true);
			HideLookupAndAdjustBalance();
			HideBalanceHistory();
		}
	}




	@Override
	public void onResume () {
		super.onResume();
		String testJustWatchedAdDate=GeneralFunctions01.Cfg.ReadSharedPreference(this,"JustWatchedAdDate");
		if (testJustWatchedAdDate.equals("1"))
		{
			GeneralFunctions01.Cfg.WriteSharedPreference(this, "JustWatchedAdDate","");
			GoToManualLookup();
		}
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.add_mod_card, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.mnuSave) {
			SaveCard();
			return true;
		}
		if (id == R.id.mnuDelete) {
			DeleteCard();
			return true;
		}
		else if (id == R.id.mnuCancel) {
			finish();
			return true;
		}
		return super.onOptionsItemSelected(item);
	}

	private void GetLookupAmntInfo()
	{
		String pParams = "pUUID="+AppSpecific.gloUUID;
		wc2=new WebComm(AppSpecific.gloxmlns);
		wc2.ExecuteWebRequest(AppSpecific.gloWebServiceURL + "/GetLookupsRemaining", pParams);
		MonitorGetLookupAmntInfo();
	}
	private void MonitorGetLookupAmntInfo()
	{
		wc2cnt=0;
		final Timer wc2Tmr=new Timer();
		wc2Tmr.scheduleAtFixedRate(new TimerTask() {
			public void run() {
				runOnUiThread(new Runnable() {
					public void run() {
						wc2cnt++;
						Log.d("APP", String.valueOf(wc2Tmr));
						if (wc2.wcWebResponse==null) return;
						if (wc2cnt > 10) {
							//timed out
							wc2Tmr.cancel();
							Log.d("APP", "Timed Out");
						}
						wc2Tmr.cancel();
						cmdLookup.setEnabled(true);
						ReportRemainingLookups();
						pAmntOfLookups=GeneralFunctions01.Conv.StringToInt(wc2.wcWebResponse);
					}
				});
			}
		}, 1, 1000); // 1000 means start delay (1 sec), and the second is the loop delay.
	}
	private void ReportRemainingLookups()
	{
		Toast toast = Toast.makeText(this, wc2.wcWebResponse+ " Lookups Remaining", Toast.LENGTH_SHORT);
		TextView v = (TextView) toast.getView().findViewById(android.R.id.message);
		v.setTextColor(Color.BLACK);
		toast.show();
	}

	private void EnableCardGeneralInfo(boolean TorF)
	{
		txtGCICardType.setEnabled(TorF);
		txtGCIURL.setEnabled(TorF);
	}


	private String ValidateEntries()
	{
		String errMsg="";
		String pGCICardType=txtGCICardType.getText().toString();
		String pGCIURL=txtGCIURL.getText().toString();
		String pCDCardNumber=txtCDCardNumber.getText().toString();
		String pCDOtherInfo=txtCDOtherInfo.getText().toString().toUpperCase();

		if (pCDCardNumber.length()<4)
		{
			errMsg="This card number is too short, it has to be at least 4 characters.";
		}
		DBAdapter myDb  = new DBAdapter(this);
		myDb.open();
		String AllSavedData=myDb.getAllRowsAsString("SELECT * FROM tblUserCardData WHERE UCDCardNumber='"+pCDCardNumber+"'");
		myDb.close();
		if (!AllSavedData.equals(""))
		{
			if (GloUCDID<1) errMsg="This card number is already in the database.";
		}
		if (pGCICardType.length()<1)
		{
			errMsg="You need to specify who the card is for.";
		}
		if (pCDOtherInfo.contains("'"))
		{
			errMsg="Sorry, Other Info cant have the ' character.";
		}
		if (pGCICardType.contains("'"))
		{
			errMsg="Sorry, Issued By cant have the ' character.";
		}
		if (pGCIURL.contains("'"))
		{
			errMsg="Sorry, URL cant have the ' character.";
		}
		return errMsg;
	}
	public void onClickDeleteCard(View arg0)
	{
		DeleteCard();
	}
	public void onClickAdjustBalance(View arg0)
	{
		AdjustBalance();
	}
	public void onClickLookupBalance(View arg0)
	{
		LookupBalance();
	}

	private void LookupBalance()
	{
		String IsOK="";
		if (pAmntOfLookups>3)
		{
			IsOK="1";
		}

		String AdViewDate=GeneralFunctions01.Cfg.ReadSharedPreference(this, "WatchAdDate");
		String currDate1=GeneralFunctions01.Dte.GetCurrentDate();
		if (currDate1.equals(AdViewDate))
		{
			IsOK="1";
		}

		if (IsOK.equals(""))
		{
			InformAboutLookup();
		}
		else
		{
			GoToManualLookup();
		}
	}

	private void GoToManualLookup()
	{
		String pCardNumber=GeneralFunctions01.Text.GetVal(txtCDCardNumber);
		String pCardPIN=GeneralFunctions01.Text.GetVal(txtCDCardPIN);
		String pURL=GeneralFunctions01.Text.GetVal(txtGCIURL);
		String pLogin=GeneralFunctions01.Text.GetVal(txtGCILogin);
		String pPassword=GeneralFunctions01.Text.GetVal(txtGCIPassword);

		String pParams = "pUUID=" + AppSpecific.gloUUID + "&pCardType=" + txtGCICardType.getText();
		new GeneralFunctions01.AsyncWebCall().execute(AppSpecific.gloWebServiceURL + "/LogCardSearch",pParams);


		String AllData=GeneralFunctions01.Comm.NonAsyncWebCall(AppSpecific.gloWebServiceURL + "/GetLookupsRemaining", "pUUID="+AppSpecific.gloUUID);
		int pLookupsRemaining=GeneralFunctions01.Conv.StringToInt(AllData);
		if (pLookupsRemaining==0)
		{
			Intent intent = new Intent(this, PurchaseOptionsActivity.class);
			startActivity(intent);
		}
		else
		{
			new GeneralFunctions01.AsyncWebCall().execute(AppSpecific.gloWebServiceURL + "/LogLookupRequest", "pUUID="+AppSpecific.gloUUID);
			Intent intent = new Intent(this, LookupActivity.class);
			intent.putExtra("URL", pURL);
			intent.putExtra("CardNum", pCardNumber);
			intent.putExtra("CardPIN", pCardPIN);
			intent.putExtra("Login", pLogin);
			intent.putExtra("Password", pPassword);
			startActivity(intent);
		}
		finish();
	}

	private void InformAboutLookup()
	{
		DialogInterface.OnClickListener dialogClickListener = new DialogInterface.OnClickListener() {
			@Override
			public void onClick(DialogInterface dialog, int which) {
				switch (which)
				{
					case DialogInterface.BUTTON_NEUTRAL:
						Log.d("APP", "They've approved");
						GoToWatchAd();
				}
			}
		};
		AlertDialog.Builder builder = new AlertDialog.Builder(this);
		builder.setTitle("Before we help you get your balance...");
		builder.setMessage("You may be shown an ad.  You can check it out or close it and proceed.  You wont be shown an ad if you've clicked on one today, or have purchased the app.");
		builder.setNeutralButton("Got It", dialogClickListener);
		AlertDialog dialog = builder.create();
		dialog.show();
	}

	private void GoToWatchAd()
	{
		Intent intent = new Intent(this, WatchAdActivity.class);
		startActivity(intent);
	}
	private void CheckToEnableCardGeneralInfo()
	{
		DBAdapter myDb  = new DBAdapter(this);
		myDb.open();
		String ItExists=myDb.getSingleValAsString("SELECT CICardType FROM tblCardInfo WHERE CICardType='"+txtGCICardType.getText()+"'");
		myDb.close();
		if (ItExists.length()==0)
		{
			EnableCardGeneralInfo(true);
		}

	}
	private void AdjustBalance()
	{
		AlertDialog.Builder alert = new AlertDialog.Builder(this);
		alert.setTitle("New Balance");
		alert.setMessage("Please adjust the balance or cancel.");

		// Set an EditText view to get user input
		final EditText input = new EditText(this);
		input.setText("");
		input.setInputType(InputType.TYPE_CLASS_NUMBER | InputType.TYPE_NUMBER_FLAG_DECIMAL);
		alert.setView(input);

		alert.setPositiveButton("Done", new DialogInterface.OnClickListener() {
			public void onClick(DialogInterface dialog, int whichButton) {
				String value = input.getText().toString();
				UpdateBalance(value);
			}
		});
		alert.setNegativeButton("Cancel",
				new DialogInterface.OnClickListener() {
					public void onClick(DialogInterface dialog, int whichButton) {
						// Canceled.
					}
				});
		alert.show();
	}
	private void UpdateBalance(String valIn)
	{
		String pCardNumber=GeneralFunctions01.Text.GetVal(txtCDCardNumber);
		String DateLogged=GeneralFunctions01.Dte.GetCurrentDateTime();
		DBAdapter myDb  = new DBAdapter(this);
		myDb.open();
		String OK=myDb.execSQL("INSERT INTO tblBalanceHistory (BHCardNumber, BHLastKnownBalance, BHLastKnownBalanceDate) VALUES ('" + pCardNumber + "','" + valIn + "','"+ DateLogged + "')");
		Log.d("APP", "OK: " + OK);
		myDb.close();
		finish();
	}

	private void DeleteCard()
	{
		DialogInterface.OnClickListener dialogClickListener = new DialogInterface.OnClickListener() {
			@Override
			public void onClick(DialogInterface dialog, int which) {
				switch (which){
					case DialogInterface.BUTTON_POSITIVE:
						if (GloUCDID>0)
						{
							AppSpecific.PMRunQuery("DELETE FROM tblUserCardData WHERE UCDID="+GloUCDID);
						}
						finish();
				}
			}
		};
		AlertDialog.Builder builder = new AlertDialog.Builder(this);
		builder.setMessage("Are you sure?").setPositiveButton("Yes", dialogClickListener)
				.setNegativeButton("No", dialogClickListener).show();
	}
	private boolean ShowHistory() {
		String pCardNumber=GeneralFunctions01.Text.GetVal(txtCDCardNumber);
		Boolean retVal=true;
		LinearLayout BHLL = (LinearLayout) findViewById(R.id.llAMBalHistory);
		BHLL.removeAllViews();
		LinearLayout temprow = BuildBHTableRow("Date", "Balance");
		BHLL.addView(temprow);
		DBAdapter myDb  = new DBAdapter(this);
		myDb.open();
		String AllBHData=myDb.getAllRowsAsString("SELECT BHLastKnownBalance, BHLastKnownBalanceDate FROM tblBalanceHistory WHERE BHCardNumber='" + pCardNumber + "' ORDER BY BHID DESC;");
		myDb.close();
		if (AllBHData.equals("")) return false;
		String[] lines=AllBHData.split("l#d");



		for (int i = 0; i < lines.length; i++) {
			String[] vals=lines[i].split("p#d");
			String pBal=GeneralFunctions01.Text.GetValInArray(vals, 0);
			String pDate=GeneralFunctions01.Text.GetValInArray(vals, 1);
			String dispDate=GeneralFunctions01.Conv.DBDateToStandardDate(pDate, true);
			LinearLayout bhrow = BuildBHTableRow(dispDate, pBal);
			BHLL.addView(bhrow);
		}
		return retVal;
	}
	private LinearLayout BuildBHTableRow(String pDateIn, String pBalIn)
	{
		LinearLayout row = (LinearLayout)LayoutInflater.from(this).inflate(R.layout.cell_bal_history_entry, null);
		((TextView)row.findViewById(R.id.txtCellBHDate)).setText(pDateIn);
		((TextView)row.findViewById(R.id.txtCellBHBal)).setText(pBalIn);
		return row;
	}

	public void onClickSaveCard(View arg0)
	{
		boolean pExit=SaveCard();
		if (pExit)
		{
			AppSpecific.gloFinishIt=true;
			finish();
		}
	}
	public boolean SaveCard()
	{
		String msg=ValidateEntries();
		if (msg.length()>0)
		{
			GeneralFunctions01.Oth.Alert(this, msg);
			return false;
		}
		if (GloUCDID<1)
		{
			SaveCardInfo("I");
		}
		else
		{
			SaveCardInfo("U");
		}
		return true;
	}

	private void SaveCardInfo(String pInsertorUpdate)
	{
		//UCDID, UCDCardType, UCDURL, UCDCardNumber, UCDCardPIN, UCDLogin, UCDPassword, UCDNotes, UCDDateLogged
		String UCDCardType="'"+ GeneralFunctions01.Text.GetVal(txtGCICardType) + "'";
		String UCDURL="'"+ GeneralFunctions01.Text.GetVal(txtGCIURL) + "'";
		String UCDCardNumber="'"+ GeneralFunctions01.Text.GetVal(txtCDCardNumber) + "'";
		String UCDCardPIN="'"+ GeneralFunctions01.Text.GetVal(txtCDCardPIN) + "'";
		String UCDLogin="'"+ GeneralFunctions01.Text.GetVal(txtGCILogin) + "'";
		String UCDPassword="'"+GeneralFunctions01.Text.GetVal(txtGCIPassword)+ "'";
		String UCDNotes="'"+ GeneralFunctions01.Text.GetVal(txtCDOtherInfo) + "'";
		String DateLogged="'" + GeneralFunctions01.Dte.GetCurrentDateTime() + "'";
		String pTemp;
		pTemp=UCDCardType +","+UCDURL+","+UCDCardNumber+","+UCDCardPIN+","+UCDLogin+","+UCDPassword+","+UCDNotes+","+DateLogged;
		//String pAdjusted=GeneralFunctions01.Text.PutValsInSingQuotes(GCIType +","+GCISubType+","+GCIIssuedBy+","+GCIURL+","+CDNumber+","+CDPIN+","+CDCVV+","+CDOther+","+DateLogged);
		DBAdapter myDb  = new DBAdapter(this);
		myDb.open();
		if (pInsertorUpdate.equals("I"))
		{
			String OK=myDb.execSQL("INSERT INTO tblUserCardData (UCDCardType,UCDURL,UCDCardNumber,UCDCardPIN,UCDLogin,UCDPassword,UCDNotes,UCDDateLogged) VALUES " + "("+pTemp+")");
		}
		else
		{
			String OK=myDb.execSQL("UPDATE tblUserCardData SET UCDCardType=" + UCDCardType +",UCDURL=" +UCDURL + ",UCDCardNumber=" + UCDCardNumber + ",UCDCardPIN=" +UCDCardPIN+ ",UCDLogin=" +UCDLogin+ ",UCDPassword="+UCDPassword+",UCDNotes="+UCDNotes+",UCDDateLogged="+DateLogged+" WHERE UCDID="+GloUCDID);
		}
		myDb.close();
		/*
		if (GloUCDID==-1)
		{
			String pParams = "pUUID=" + AppSpecific.gloUUID +"&pCardType=" + GCITypeNA +"&pCardSubType=" + GCISubTypeNA +"&pIssuedBy=" + GCIIssuedByNA +"&pURL=" + GCIURLNA;
			String pURL=AppSpecific.gloWebServiceURL + "/LogCardDataCandidate";
			new GeneralFunctions01.AsyncWebCall().execute(pURL,pParams);
		}
		*/
	}
	private void HideBalanceHistory()
	{
		LinearLayout llAMBalHistory = (LinearLayout)findViewById(R.id.llAMBalHistory);
		llAMBalHistory.setVisibility(View.GONE);
	}
	private void HideLookupAndAdjustBalance()
	{
		LinearLayout llLookupAdjBal = (LinearLayout)findViewById(R.id.llLookupAdjBal);
		llLookupAdjBal.setVisibility(View.GONE);
	}
	private void ConfigGeneralCardInfo() {
		DBAdapter myDb  = new DBAdapter(this);
		myDb.open();
		String AllSavedData=myDb.getAllRowsAsString("SELECT CICardType, CIURL, CIShowLP FROM tblCardInfo WHERE CICardType='"+GloCardType+"'");
		myDb.close();
		if (AllSavedData.equals(""))return;
		String templine="";
		String[] lines=AllSavedData.split("l#d");
		for (int i = 0; i < lines.length; i++) {
			templine = lines[i] + "p#d";
			String[] pVal = GeneralFunctions01.Text.Split(templine, "p#d");
			String pCICardType = GeneralFunctions01.Text.GetValInArray(pVal, 0);
			String pCIURL = GeneralFunctions01.Text.GetValInArray(pVal, 1);
			String pCIShowLP = GeneralFunctions01.Text.GetValInArray(pVal, 2);
			txtGCICardType.setText(pCICardType);
			txtGCIURL.setText(pCIURL);
			if (!pCIShowLP.equals("1")) {
				LinearLayout llLoginPassword = (LinearLayout) findViewById(R.id.llLoginPassword);
				llLoginPassword.setVisibility(View.GONE);
			}
		}
	}
	private void ConfigExistingCard()
	{
		DBAdapter myDb  = new DBAdapter(this);
		myDb.open();
		//GCIType, GCISubType, GCIIssuedBy, CDNumber, CDCVV, qryBalanceHistory.LastKnownBalance, qryBalanceHistory.LastKnownBalanceDate
		String AllSavedData=myDb.getAllRowsAsString("SELECT * FROM tblUserCardData WHERE UCDID="+GloUCDID);
		myDb.close();
		if (AllSavedData.equals(""))return;
		String[] pieces=AllSavedData.split("p#d");
		String pCardType=GeneralFunctions01.Text.GetValInArray(pieces, 1);
		String pURL=GeneralFunctions01.Text.GetValInArray(pieces, 2);
		String pCardNumber=GeneralFunctions01.Text.GetValInArray(pieces, 3);
		String pCardPIN=GeneralFunctions01.Text.GetValInArray(pieces, 4);
		String pLogin=GeneralFunctions01.Text.GetValInArray(pieces, 5);
		String pPassword=GeneralFunctions01.Text.GetValInArray(pieces, 6);
		String pOther=GeneralFunctions01.Text.GetValInArray(pieces,7);
		txtGCICardType.setText(pCardType);
		txtGCIURL.setText(pURL);
		txtGCILogin.setText(pLogin);
		txtGCIPassword.setText(pPassword);
		txtCDCardNumber.setText(pCardNumber);
		txtCDCardPIN.setText(pCardPIN);
		txtCDOtherInfo.setText(pOther);
	}
	private void SetupFields()
	{
		txtGCICardType = (EditText)findViewById(R.id.txtGCICardType);
		txtGCIURL = (EditText)findViewById(R.id.txtGCIURL);
		txtGCILogin = (EditText)findViewById(R.id.txtGCILogin);
		txtGCIPassword = (EditText)findViewById(R.id.txtGCIPassword);
		txtCDCardNumber= (EditText)findViewById(R.id.txtCDCardNumber);
		txtCDCardPIN= (EditText)findViewById(R.id.txtCDCardPIN);
		txtCDOtherInfo= (EditText)findViewById(R.id.txtCDOtherInfo);
	}
}
