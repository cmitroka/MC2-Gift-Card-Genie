package com.mc2techservices.gcg;

import android.content.Intent;
import android.os.Bundle;
import android.app.Activity;
import android.view.View;
import android.widget.Button;
import android.widget.ProgressBar;

public class PreCardTypeSelectActivity extends Activity {
    Button cmdDining;
    Button cmdRetail;
    Button cmdFood;
    Button cmdOther;
    Button cmdCancel;
    Button cmdContinue;
    String pCategory="";
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_pre_card_type_select);
        cmdDining=(Button)findViewById(R.id.cmdDining);
        cmdRetail=(Button)findViewById(R.id.cmdRetail);
        cmdFood=(Button)findViewById(R.id.cmdFood);
        cmdOther=(Button)findViewById(R.id.cmdOther);
        cmdCancel=(Button)findViewById(R.id.cmdCancel);
        cmdContinue=(Button)findViewById(R.id.cmdContinue);
    }
    @Override
    public void onResume () {
        super.onResume();
        if (AppSpecific.gloFinishIt==true)
        {
            AppSpecific.gloFinishIt=false;
            finish();
        }
        ProgressBar pLoading=(ProgressBar)findViewById(R.id.ivb00);
        pLoading.setVisibility(View.INVISIBLE);
    }

    public void ToggleCardTypesOn() {
        cmdFood.setAlpha((float)1.0);
        cmdRetail.setAlpha((float)1.0);
        cmdDining.setAlpha((float)1.0);
        cmdOther.setAlpha((float)1.0);
    }
    public void ToggleCardSubTypesOn() {
        cmdCancel.setAlpha((float)1.0);
        cmdContinue.setAlpha((float)1.0);
    }

    public void onClickSelectFood(View arg0) {
        ToggleCardTypesOn();
        if (pCategory.equals("Food"))
        {
            pCategory="";
            cmdFood.setAlpha((float)1);
        }
        else
        {
            pCategory="Food";
            //cmdFood.setAlpha((float).5);
            GoToNextScreen();
        }
    }
    public void onClickSelectRetail(View arg0) {
        ToggleCardTypesOn();
        if (pCategory.equals("Retail"))
        {
            pCategory="";
            cmdRetail.setAlpha((float)1);
        }
        else
        {
            pCategory="Retail";
            //cmdRetail.setAlpha((float).5);
            GoToNextScreen();
        }
    }
    public void onClickSelectDining(View arg0) {
        ToggleCardTypesOn();
        if (pCategory.equals("Dining"))
        {
            pCategory="";
            cmdDining.setAlpha((float)1);
        }
        else
        {
            pCategory="Dining";
            //cmdDining.setAlpha((float).5);
            GoToNextScreen();
        }
    }
    public void onClickSelectOther(View arg0) {
        ToggleCardTypesOn();
        if (pCategory.equals("Other"))
        {
            pCategory="";
            cmdOther.setAlpha((float)1);
        }
        else
        {
            pCategory="Other";
            //cmdOther.setAlpha((float).5);
            GoToNextScreen();
        }
    }
    private void GoToNextScreen()
    {
        ToggleCardTypesOn();
        ProgressBar pLoading=(ProgressBar)findViewById(R.id.ivb00);
        pLoading.setVisibility(View.VISIBLE);
        Intent intent = new Intent(this, CardTypeSelectActivity.class);  //CardTypeSelectActivity
        intent.putExtra("Category", pCategory);
        startActivity(intent);
        //finish();
    }
    public void onClickNextScreen(View arg0)
    {
        pCategory="";
        GoToNextScreen();
    }

    public void onClickPrevScreen(View arg0)
    {
        finish();
    }

    //map out the enable/disable; filter next section; translate old types to new types

}
