package com.mc2techservices.gcbg;

import java.util.Timer;
import java.util.TimerTask;

import com.mc2techservices.common.GeneralFunctions;
import com.mc2techservices.gcbg.LoginActivity.AsyncWebCallRunner;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.Gravity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TextView;

public class SplashscreenActivity extends Activity {
	Timer myTimer;
	TextView txtSSLabel;
	LinearLayout pll1;
	int position;
	final String AppName = "Realtime Recalls";
	String displayIt;
	String SessionID;
	String CurrCall;
	private Timer t;
	private int TimeCounter = 3;
	TextView tvTimer;
	Button btnStop;
	Context context;
	public static final String URL = "";

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		context = this;
		super.onCreate(savedInstanceState);

		
		//Test
		/*
		String pParams = "pGCGKey=string&pChannel=string";
		String pURL = GlobalClass.gloWebServiceURL + "/GCGLogin";
		WebServiceHandlerASynch runner = new WebServiceHandlerASynch();
		runner.execute("GCGLogUser", pParams); // "pGCGLogin=cjm&pGCGPassword=cjm"
		WebServiceHandlerSynch.DoWSCall("GCGLogUser", pParams);
		*/
		DoOnCreate();
		
	}

	private void DoOnCreate()
	{
		Log.d("SplashscreenActivity", "");

		setContentView(R.layout.activity_splashscreen);
		TextView tvTimer = (TextView) findViewById(R.id.etTimer);
		pll1 = (LinearLayout) findViewById(R.id.ll1);
		tvTimer.setGravity(Gravity.CENTER);
		btnStop = (Button) findViewById(R.id.cmdCancel);
		t = new Timer();
		String AutoStart = AppSpecificFunctions.ReadSharedPreference(context,
				"AutoStart");
		AppSpecificFunctions.WriteSharedPreference(context, "DoAutoStart", "0");
		if (AutoStart.equals("1")) {
			t.scheduleAtFixedRate(new TimerTask() {
				public void run() {
					runOnUiThread(new Runnable() {
						public void run() {
							TextView tvTimer = (TextView) findViewById(R.id.etTimer);
							tvTimer.setText(String.valueOf(TimeCounter));
							TimeCounter--;
							Log.d("Count: ", String.valueOf(TimeCounter));
							if (TimeCounter == 0) {
								t.cancel();
								AppSpecificFunctions.WriteSharedPreference(
										context, "DoAutoStart", "1");
								Init();
							}
						}
					});

				}
			}, 1000, 1000); // 1000 means start from 1 sec, and the second 1000
							// is do the loop each 1 sec.
		} else {
			pll1.setAlpha(0);
			t.scheduleAtFixedRate(new TimerTask() {
				public void run() {
					runOnUiThread(new Runnable() {
						public void run() {
							TimeCounter++;
							Log.d("Count: ", String.valueOf(TimeCounter));
							if (TimeCounter == 6) {
								t.cancel();
								Init();
							}
						}
					});

				}
			}, 500, 500); // 1000 means start from 1 sec, and the second 1000 is
							// do the loop each 1 sec.
		}
	}
	
	
	public void onCancelClick(View arg0) {
		t.cancel();
		AppSpecificFunctions.WriteSharedPreference(context, "DoAutoStart", "0");
		Init();
	}

	private void Init() {
		Intent intent = new Intent(this, InitActivity.class);
		startActivity(intent);
	}

}
