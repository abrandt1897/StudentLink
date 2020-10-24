package com.example.studentlink.ui.login.SignIn.Logic;

import com.example.studentlink.ui.login.AbstractLogic;
import com.example.studentlink.ui.login.SignIn.LoginActivity;

public class MockLoginLogic extends AbstractLogic {

    public MockLoginLogic(LoginActivity activity){
        super(activity);
    }

    @Override
    public String checkCredentials(String username, String password) {
        moveToHome();
        return "Ok";
    }
}
