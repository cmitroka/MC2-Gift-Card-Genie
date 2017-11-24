package com.mc2techservices.gcg;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.view.View;
import android.widget.TextView;

public class AboutActivity extends Activity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_about);
		SetupScreen();
	}
	private void SetupScreen()
	{
		TextView tvVersionToDisp = (TextView)findViewById(R.id.tvVersionToDisp);
		TextView tvUsage = (TextView)findViewById(R.id.tvUsage);
		String vToDisp=GeneralFunctions01.Sys.GetVersion(this);
		String pVersion="V" + vToDisp;
		tvVersionToDisp.setText(pVersion);
		String AllData=GeneralFunctions01.Comm.NonAsyncWebCall(AppSpecific.gloWebServiceURL + "/GetLookupsRemaining", "pUUID="+AppSpecific.gloUUID);
		String txtMsg="You have " + AllData + " lookups remaining.";
		tvUsage.setText(txtMsg);
	}
	public void onClickVisitWebsite(View arg0)
	{
		VisitWebsite();
	}
	public void VisitWebsite()
	{
		String url="http://gcg.mc2techservices.com/Default.aspx";
        Uri uriUrl = Uri.parse(url);
        Intent launchBrowser = new Intent(Intent.ACTION_VIEW, uriUrl);
        startActivity(launchBrowser);
	}
	
}
