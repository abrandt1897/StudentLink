package com.example.studentlink.ui.login.NewAccount.Logic;

import android.content.Context;
import android.content.Intent;

import androidx.appcompat.app.AppCompatActivity;

import com.example.studentlink.ui.login.NewAccount.CanvasKey.*;
import com.example.studentlink.ui.login.NewAccount.CreateAccount;

import java.util.HashMap;
import java.util.Map;

import com.example.studentlink.ui.login.AbstractLogic;

public class CreateAccountLogic extends AbstractLogic {

    public CreateAccountLogic(AppCompatActivity activity){
        super(activity);
    }


    public void navigateToWebView(Intent intent, String username, String password) {
//        Intent intent = new Intent(context, moveToThisClass);
        intent.putExtra("Username", username);
        intent.putExtra("Password", password);
        theActivity.startActivity(intent);
    }

    @Override
    public String checkCredentials(String username, String password) {
        String databaseName = "Students";
        Map<String,String> userLoginData = new HashMap<String,String>();
        userLoginData.put("Username", username);
        userLoginData.put("Password", password);
        connectionClass.putRequest(userLoginData,databaseName);
        if(!connectionClass.getResponse().equals("No student under that username")){
            return "";
        }
        else{
            this.navigateToWebView(new Intent(theActivity.getApplicationContext(), CanvasWebview.class), username, password);
            return "Ok";
        }
    }
}
