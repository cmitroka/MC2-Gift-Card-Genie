package com.mc2techservices.gcg;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;

public class TestAreaActivity extends Activity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_test_area);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.test_area, menu);
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
	public void onDoIt(View arg0) {
        Log.d("", "onDoIt.");
        GlobalClass.gloUUID = "CJMTest";
        GlobalClass.gloLoggedInAs="D76B11129C24673";
        
        GlobalClass.gloPurchaseType = "999";
        GlobalClass.gloPurchaseType="override";
		Intent intent = new Intent(this, PurchaseActivity.class);
		startActivity(intent);
    }	
	public void onTestDoOverlay(View arg0) {
		Intent intent = new Intent(this, OverlayActivity.class);
		startActivity(intent);
    }	
}
