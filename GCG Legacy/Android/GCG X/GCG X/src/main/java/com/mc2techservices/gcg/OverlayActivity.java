package com.mc2techservices.gcg;

import java.util.Timer;
import java.util.TimerTask;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;

public class OverlayActivity extends Activity {
	Timer t;
	int TimeCounter;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_overlay);
		t = new Timer();
	    t.scheduleAtFixedRate(new TimerTask() {
	        @Override
	        public void run() {
	            runOnUiThread(new Runnable() {
	                public void run() {
	                    TimeCounter++;
	                    if (TimeCounter>10) { //Ready==true
	                    	t.cancel();
	                    	GlobalClass.gloLoggedInAs="TimedOut";
						}
	                    if (!GlobalClass.gloLoggedInAs.equals("")) {
							if (GlobalClass.gloLoggedInAs.equals("TimedOut"))
							{
		                    	t.cancel();
								finish();
							}
							else
							{
		                    	t.cancel();
								ProceedToPurchaseScreen();
							}
						}
	                }
	            });

	        }
	    }, 1000, 1000);

	}

	private void ProceedToPurchaseScreen()
	{
		Intent intent = new Intent(this, PurchaseOldActivity.class);
		startActivity(intent);
	}
}
