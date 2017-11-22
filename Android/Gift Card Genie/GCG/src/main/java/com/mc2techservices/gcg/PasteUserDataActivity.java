package com.mc2techservices.gcg;

import android.os.Bundle;
import android.app.Activity;
import android.view.Window;

public class PasteUserDataActivity extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.activity_paste_user_data);
    }
    /*
    public void onClickCardPIN(View arg0)
    {
        Button cmdPCardNumber = (Button)findViewById(R.id.cmdPCardNumber);
        cmdPCardNumber.setText("abc");
    }
    */
}
