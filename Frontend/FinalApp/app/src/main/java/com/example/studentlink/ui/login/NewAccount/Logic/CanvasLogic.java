package com.example.studentlink.ui.login.NewAccount.Logic;

import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import com.example.studentlink.ui.login.AbstractLogic;

import java.util.HashMap;
import java.util.Map;

public class CanvasLogic extends AbstractLogic {

    String CanvasToken;

    public CanvasLogic(AppCompatActivity activity) {
        super(activity);
    }

    public void setToken(String c){
        CanvasToken = c;
    }

    @Override
    public String checkCredentials(String username, String password) {
        String databaseName = "api/Students/" + CanvasToken;
        Map<String,String> userLoginData = new HashMap<String,String>();
        userLoginData.put("Username",username);
        userLoginData.put("Password",password);
        connectionClass.putRequest(userLoginData,databaseName);
        if(connectionClass.getResponse().equals("Bad response") || connectionClass.getResponse().equals("")){
            return "";
        }
        else{
            Toast.makeText(theActivity.getApplicationContext(),"Congrats you made your account!", Toast.LENGTH_LONG).show();
            // MoveToHome(); // TODO: Uncomment for real functionality
        }

        return null;

    }
}
