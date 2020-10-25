package com.example.studentlink.ui.login.SignIn.Logic;

import android.os.Handler;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.example.studentlink.ConnectionClass;
import com.example.studentlink.Global;
import com.example.studentlink.ui.login.AbstractLogic;
import com.example.studentlink.ui.login.SignIn.LoginActivity;

import java.util.HashMap;
import java.util.Map;

public class LoginLogic extends AbstractLogic {

    public LoginLogic(LoginActivity activity){
        super(activity);
    }

    @Override
    public String checkCredentials(String username, String password) {
        String databaseName = "api/Login";
        Map<String,String> userLoginData = new HashMap<String,String>();
        userLoginData.put("Username",username); //greysonj
        userLoginData.put("Password",password); //yeet
        connectionClass.putRequest(userLoginData,databaseName);

        if(connectionClass.getResponse().equals("Bad Response")){
            Toast.makeText(theActivity, connectionClass.getResponse(), Toast.LENGTH_SHORT).show();
            return "";
        }
        else {
            // for existing user, we get the student ID (for Home page notifications) and bearer token (has to be in the header for all calls)
            Toast.makeText(theActivity.getApplicationContext(), connectionClass.getResponse(), Toast.LENGTH_SHORT).show();
            String[] answer = connectionClass.getResponse().split(" ");
            Global.studentID = Integer.parseInt(answer[0]);
            Global.bearerToken = answer[1];
            moveToHome();
        }
        return null;
    }
}
