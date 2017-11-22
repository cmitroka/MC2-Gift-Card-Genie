package com.mc2techservices.gcg;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;

public class ContactActivity extends Activity {
	String pShortUUID;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_contact);
    	pShortUUID=AppSpecific.gloUUID.substring(0, 8);

	}

	public void emailSuggestion(View arg0) 
	{
		Intent email = new Intent(Intent.ACTION_SEND);
		email.setType("message/rfc822");
		email.putExtra(Intent.EXTRA_EMAIL, new String[]{"service@mc2techservices.com"});
		email.putExtra(Intent.EXTRA_SUBJECT, "UGCB Feedback " + pShortUUID);
		email.putExtra(Intent.EXTRA_TEXT, "");

		try {
		// the user can choose the email client
			startActivity(Intent.createChooser(email, "Choose an email client from..."));

		} catch (android.content.ActivityNotFoundException ex) 
		{
		}
	}
	public void emailBug(View arg0) 
	{
		Intent email = new Intent(Intent.ACTION_SEND);
		email.setType("message/rfc822");
		email.putExtra(Intent.EXTRA_EMAIL, new String[]{"service@mc2techservices.com"});
		email.putExtra(Intent.EXTRA_SUBJECT, "UGCB Bug " + pShortUUID);
		email.putExtra(Intent.EXTRA_TEXT, "");

		try {
		// the user can choose the email client
			startActivity(Intent.createChooser(email, "Choose an email client from..."));

		} catch (android.content.ActivityNotFoundException ex) 
		{
		}
	}
	


}
