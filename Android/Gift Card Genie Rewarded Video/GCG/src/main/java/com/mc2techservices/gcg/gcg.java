package com.mc2techservices.gcg;

import android.app.Application;
import android.content.Context;
import android.support.multidex.MultiDex;

public class gcg extends Application {

    private static Context context;

    @Override
    public void onCreate() {
        super.onCreate();
        gcg.context = getApplicationContext();
    }
    @Override
    protected void attachBaseContext(Context base) {
        super.attachBaseContext(base);
        MultiDex.install(this);
    }
    public static Context getAppContext() {
        return gcg.context;
    }
}