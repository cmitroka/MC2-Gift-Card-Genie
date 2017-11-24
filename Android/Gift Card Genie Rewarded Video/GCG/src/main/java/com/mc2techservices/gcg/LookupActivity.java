package com.mc2techservices.gcg;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.Dialog;
import android.content.Context;
import android.content.DialogInterface;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.text.InputType;
import android.util.Log;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.webkit.WebChromeClient;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class LookupActivity extends Activity {
	final Context context = this;
	String pCardNum;
	String pCardPIN;
	String pLogin;
	String pPassword;
	WebView webView;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_lookup);
		SetupScreen();
	}

	private void SetupScreen()
	{
		String pURL=(getIntent().getStringExtra("URL"));
		pCardNum=(getIntent().getStringExtra("CardNum"));
		pCardPIN=(getIntent().getStringExtra("CardPIN"));
		pLogin=(getIntent().getStringExtra("Login"));
		pPassword=(getIntent().getStringExtra("Password"));
		Button cmdPasteUserInfo = (Button) findViewById(R.id.cmdPasteUserInfo);
		Button cmdUpdateBalance = (Button) findViewById(R.id.cmdUpdateBalance);
		webView = (WebView) findViewById(R.id.webView);
		webView.getSettings().setJavaScriptEnabled(true);
		webView.getSettings().setAllowContentAccess(true);
		webView.getSettings().setAppCacheEnabled(false);
		webView.clearCache(true);
		webView.setWebViewClient(new WebViewClient());
		webView.setWebChromeClient(new WebChromeClient());
		webView.setLayerType(View.LAYER_TYPE_SOFTWARE, null);
		webView.getSettings().setBuiltInZoomControls(false);
		webView.setScrollBarStyle(View.SCROLLBARS_INSIDE_OVERLAY);
		webView.getSettings().setLoadWithOverviewMode(true);
		webView.getSettings().setDomStorageEnabled(true);
		webView.loadUrl(pURL); // URL
		getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_HIDDEN);
	    Toast.makeText(this, "Use the button above to paste the data where it belongs.", Toast.LENGTH_LONG).show();
	}
	public void onClickAdjustBalance(View arg0)
	{
		AdjustBalance();
	}
	public void onClickPasteUserData(View arg0)
	{
		PasteUserData();
	}


	private void AdjustBalance()
	{
		AlertDialog.Builder alert = new AlertDialog.Builder(this);
		alert.setTitle("New Balance");
		alert.setMessage("Please adjust the balance or cancel.");
		final EditText input = new EditText(this);
		input.setInputType(InputType.TYPE_CLASS_NUMBER | InputType.TYPE_NUMBER_FLAG_DECIMAL);
		input.setText("");
		alert.setView(input);

		alert.setPositiveButton("Done", new DialogInterface.OnClickListener() {
			public void onClick(DialogInterface dialog, int whichButton) {
				String value = input.getText().toString();
				String TestNumber = value;
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
		String DateLogged=GeneralFunctions01.Dte.GetCurrentDateTime();
		DBAdapter myDb  = new DBAdapter(this);
		myDb.open();
		String OK=myDb.execSQL("INSERT INTO tblBalanceHistory (BHCardNumber, BHLastKnownBalance, BHLastKnownBalanceDate) VALUES ('" + pCardNum + "','" + valIn + "','"+ DateLogged + "')");
		Log.d("APP", "OK: " + OK);
		myDb.close();
		finish();
	}
	private void ShowPasteInfo(String valIn)
	{
		final Toast tag = Toast.makeText(getBaseContext(), valIn,Toast.LENGTH_SHORT);

		tag.show();

		new CountDownTimer(9000, 1000)
		{
			public void onTick(long millisUntilFinished) {tag.show();}
			public void onFinish() {tag.show();}

		}.start();
	}
	private void PasteUserData()
	{
		final Dialog dialog = new Dialog(context);
		dialog.requestWindowFeature(Window.FEATURE_NO_TITLE);
		//dialog.setTitle("Dialog Title");
		dialog.setContentView(R.layout.activity_paste_user_data);
		Button cmdCardNumber = (Button) dialog.findViewById(R.id.cmdPCardNumber);
		cmdCardNumber.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				dialog.dismiss();
				webView.loadUrl("javascript:document.execCommand('insertHtml', false,'" + pCardNum + "');");
				ShowPasteInfo(pCardNum);
				//Toast.makeText(getApplicationContext(),"Dismissed..!!",Toast.LENGTH_SHORT).show();
			}
		});

		Button cmdPIN = (Button) dialog.findViewById(R.id.cmdPCardPIN);
		cmdPIN.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				dialog.dismiss();
				webView.loadUrl("javascript:document.execCommand('insertHtml', false,'" + pCardPIN + "');");
				ShowPasteInfo(pCardPIN);
			}
		});
		Button cmdLogin = (Button) dialog.findViewById(R.id.cmdPLogin);
		cmdLogin.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				dialog.dismiss();
				webView.loadUrl("javascript:document.execCommand('insertHtml', false,'" + pLogin + "');");
				ShowPasteInfo(pLogin);
			}
		});
		Button cmdPassword = (Button) dialog.findViewById(R.id.cmdPPassword);
		cmdPassword.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				dialog.dismiss();
				webView.loadUrl("javascript:document.execCommand('insertHtml', false,'" + pPassword + "');");
				ShowPasteInfo(pPassword);
			}
		});
		dialog.show();
	}
}
