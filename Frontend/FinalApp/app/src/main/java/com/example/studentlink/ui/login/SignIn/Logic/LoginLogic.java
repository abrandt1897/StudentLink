package com.example.studentlink.ui.login.SignIn.Logic;

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
        userLoginData.put("Username",username);
        userLoginData.put("Password",password);
        connectionClass.putRequest(userLoginData,databaseName);

        if(connectionClass.getResponse().equals("bad")){
            return "";
        }
        else{
            // for existing user, we get the student ID and bearer token, which has to be in the header for all calls after that
            Global.studentID = 234234;

            moveToHome();
        }
        return null;
    }
}
