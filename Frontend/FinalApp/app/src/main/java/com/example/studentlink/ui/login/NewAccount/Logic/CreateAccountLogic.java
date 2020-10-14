package com.example.studentlink.ui.login.NewAccount.Logic;

import android.content.Context;
import android.content.Intent;

import com.example.studentlink.ui.login.NewAccount.CanvasKey.*;
import com.example.studentlink.ui.login.NewAccount.CreateAccount;

import java.util.HashMap;
import java.util.Map;

import com.example.studentlink.ui.login.AbstractLogic;

public class CreateAccountLogic extends AbstractLogic {


    public CreateAccountLogic(CreateAccount activity){
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
        String databaseName = "Login";
        Map<String,String> userLoginData = new HashMap<String,String>();
        userLoginData.put("Username", username);
        userLoginData.put("Password", password);
//        connectionClass.putRequest(userLoginData,databaseName); // TODO: Uncomment for real functionality
//        if(!connectionClass.getResponse().equals("Ok")){
//            return "";
//        }
//        else{
            navigateToWebView(theActivity.getApplicationContext(), CanvasWebview.class, username, password);
//        }
        return null;
    }
}
