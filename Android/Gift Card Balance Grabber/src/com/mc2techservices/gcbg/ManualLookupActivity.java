package com.mc2techservices.gcbg;

import java.util.ArrayList;
import java.util.List;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.webkit.WebChromeClient;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class ManualLookupActivity extends Activity {
	private WebView webView;
	String RUCardDataID;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_manual_lookup);
		String pParams = WebServiceHandlerSynch.DoWSCall("GetMLParams",
				"pGCGKey=" + GlobalClass.gloLoggedInAs);
		if (pParams.equals("X")) {
			finish();
			return;
		}
		WebServiceHandlerSynch.DoWSCall("SetMLParams", "pGCGKey="
				+ GlobalClass.gloLoggedInAs + "&pParams=X");
		String[] pParamArr = pParams.split("~_~");
		// https://wbiprod.storedvalue.com/WBI/clientPages/apple_en_Lookup.jsp?host=applebees.com~_~1234567890123456789~_~
		TextView txtCardNum = (TextView) findViewById(R.id.txtCardNum);
		TextView txtCardPIN = (TextView) findViewById(R.id.txtCardPIN);
		RUCardDataID = pParamArr[1]; // RUCardDataID
		txtCardNum.setText(pParamArr[2]); // CardNum
		txtCardPIN.setText(pParamArr[3]); // CardPIN
		webView = (WebView) findViewById(R.id.webView1);
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
		webView.loadUrl(pParamArr[0]); // URL

	}

	public void onUpdateBalanceClick(View arg0) {
		PromptForInput();
	}

	private void PromptForInput() {
		AlertDialog.Builder alert = new AlertDialog.Builder(this);

		alert.setTitle("New Balance");
		alert.setMessage("Please adjust the balance or cancel.");

		// Set an EditText view to get user input
		final EditText input = new EditText(this);
		input.setText("");
		alert.setView(input);

		alert.setPositiveButton("Done", new DialogInterface.OnClickListener() {
			public void onClick(DialogInterface dialog, int whichButton) {
				String value = input.getText().toString();
				String TestNumber = value;
				String Result = WebServiceHandlerSynch
						.DoWSCall(
								"RUCardDataMod",
								"pGCGKey="
										+ GlobalClass.gloLoggedInAs
										+ "&CardID="
										+ RUCardDataID
										+ "&CardType=X&CardNumber=X&CardPIN=X&LastKnownBalance="
										+ TestNumber
										+ "&LastKnownBalanceDate=X&pAction=UpdateBalance");
				if (Result.equals("1")) {
					DoAlert();
				}
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

	private void DoAlert()
	{
		AlertDialog.Builder bld = new AlertDialog.Builder(this);
        bld.setMessage("Your balance has been updated.");
        bld.setNeutralButton("OK", null);
        bld.create().show();

	}
	
	@Override
	public void onBackPressed() {
		if (webView.canGoBack()) {
			webView.goBack();
		} else {
			super.onBackPressed();
		}
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.manual_lookup, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		// Handle action bar item clicks here. The action bar will
		// automatically handle clicks on the Home/Up button, so long
		// as you specify a parent activity in AndroidManifest.xml.
		int id = item.getItemId();
		if (id == R.id.action_settings) {
			return true;
		}
		return super.onOptionsItemSelected(item);
	}
}
