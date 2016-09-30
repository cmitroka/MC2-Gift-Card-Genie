package com.mc2techservices.gcbg;

import com.mc2techservices.gcbg.R;

import android.net.Uri;
import android.os.Bundle;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.AlertDialog;
import android.content.ActivityNotFoundException;
import android.content.DialogInterface;
import android.content.Intent;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import android.webkit.WebChromeClient;
import android.webkit.WebResourceRequest;
import android.webkit.WebResourceResponse;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.ProgressBar;
import android.widget.TextView;

public class WebformActivity extends Activity {
	private WebView webView;
	private static final String badurl = "http://myappname.invalid/";

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_webview);
		ProgressBar pb = (ProgressBar) findViewById(R.id.progressBar1);
		pb.setVisibility(View.VISIBLE);
		pb.animate();
		TextView txtProgress = (TextView)findViewById(R.id.txtProgress);
		txtProgress.setVisibility(View.VISIBLE);

		Log.d("WebformActivity", "");

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
		webView.loadUrl(GlobalClass.gloLaunchURL);
		/*
		webView.setWebViewClient(new WebViewClient() {
			public boolean shouldOverrideUrlLoading(WebView view, String url) {
				String url_new = view.getUrl();
				Log.v("", "Webview URL: " + url);
				return false;
			}
		});
		*/
		webView.setWebViewClient(new MyWebViewClient());
	}

	@Override
	public void onBackPressed() {
		if (webView.canGoBack()) {
			webView.goBack();
		} else {
			super.onBackPressed();
		}
	}

	private void SwitchScreen() {
		Intent intent = new Intent(this, PurchaseOptionsActivity.class);
		startActivity(intent);
	}
	private void SwitchScreenMLA() {
		Intent intent = new Intent(this, ManualLookupActivity.class);
		startActivity(intent);
	}


	private class MyWebViewClient extends WebViewClient {
		@Override
		public void onPageFinished(WebView view, String url) {
			ProgressBar pb = (ProgressBar) findViewById(R.id.progressBar1);
			pb.setVisibility(View.GONE);
			TextView txtProgress = (TextView)findViewById(R.id.txtProgress);
			txtProgress.setVisibility(View.GONE);

			Log.d("GCGX", "onPageFinished");
			if (url.contains("PleasePurchaseGCG")) {
				SwitchScreen();
				finish();
			}
			else if (url.contains("DoManualLookup")) {
				String pParams=WebServiceHandlerSynch.DoWSCall("GetMLParams", "pGCGKey="+GlobalClass.gloLoggedInAs);
				if (pParams.length()>1) {
					SwitchScreenMLA();					
				}
			}
		}
		@Override
		public void onReceivedError(WebView view, int errorCode, String description, String failingUrl)
		{
			Log.d("GCGX", "onReceivedError");
		}
		@Override
		public boolean shouldOverrideUrlLoading(WebView view, String url) {
			Log.d("GCGX", "shouldOverrideUrlLoading");
			webView.loadUrl(url);
			return true;
		}
	}
}
