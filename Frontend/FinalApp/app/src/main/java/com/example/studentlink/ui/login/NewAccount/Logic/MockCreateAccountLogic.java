package com.example.studentlink.ui.login.NewAccount.Logic;

import android.content.Context;
import android.content.Intent;

import com.example.studentlink.ui.login.AbstractLogic;
import com.example.studentlink.ui.login.NewAccount.CanvasKey.CanvasWebview;
import com.example.studentlink.ui.login.NewAccount.CreateAccount;


public class MockCreateAccountLogic extends AbstractLogic {

    public MockCreateAccountLogic(CreateAccount activity){
        super(activity);
    }

    private void navigateToWebView(Context context, Class moveToThisClass, String username, String password) {
        Intent intent = new Intent(context, moveToThisClass);
        intent.putExtra("Username", username);
        intent.putExtra("Password", password);
        theActivity.startActivity(intent);
    }

    @Override
    public String checkCredentials(String username, String password) {
        navigateToWebView(theActivity.getApplicationContext(), CanvasWebview.class, username, password);
        return "Ok";
    }

}
