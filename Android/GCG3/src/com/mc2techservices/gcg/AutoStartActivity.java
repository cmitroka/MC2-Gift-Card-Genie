package com.mc2techservices.gcg;

import java.util.Timer;
import java.util.TimerTask;

import com.mc2techservices.gcg.LoginActivity.AsyncWebCallRunner;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.Gravity;
import android.view.View;
import android.webkit.WebView;
import android.widget.Button;
import android.widget.TextView;
import android.util.Log;

public class AutoStartActivity extends Activity {
	private Timer t;
	private int TimeCounter = 3;
	TextView tvTimer;
    Button btnStop;
	public static final String URL = "";

    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
		final Context context = this;
		super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_autostart);
        TextView tvTimer=(TextView) findViewById(R.id.etTimer);
        tvTimer.setGravity(Gravity.CENTER);
        btnStop=(Button) findViewById(R.id.cmdCancel);
        t = new Timer();
		String AutoStart=AppSpecificFunctions.ReadSharedPreference(context, "AutoStart");
		if (AutoStart.equals("1"))
		{
			AppSpecificFunctions.WriteSharedPreference(context, "DoAutoStart", "0");
	        t.scheduleAtFixedRate(new TimerTask() {
	            public void run() {
	                runOnUiThread(new Runnable() {
	                    public void run() {
	                        TextView tvTimer=(TextView) findViewById(R.id.etTimer);
	                        tvTimer.setText(String.valueOf(TimeCounter)); // you can set it to a textView to show it to the user to see the time passing while he is writing.
	                        TimeCounter--;
	                        if (TimeCounter==0) {
	                        	t.cancel();
	                    		AppSpecificFunctions.WriteSharedPreference(context, "DoAutoStart", "1");
	            				Intent intent = new Intent(context, LoginActivity.class);
	            				startActivity(intent);							
							}
	                    }
	                });

	            }
	        }, 1000, 1000); // 1000 means start from 1 sec, and the second 1000 is do the loop each 1 sec.			
		}
		else {
			AppSpecificFunctions.WriteSharedPreference(context, "DoAutoStart", "0");
			Intent intent = new Intent(context, LoginActivity.class);
			startActivity(intent);			
		}
		btnStop.setOnClickListener(new View.OnClickListener() 		
		{
			public void onClick(View v) {
				t.cancel();
				AppSpecificFunctions.WriteSharedPreference(context, "DoAutoStart", "0");
				Intent intent = new Intent(context, LoginActivity.class);
				startActivity(intent);
			}
		});
    }
}