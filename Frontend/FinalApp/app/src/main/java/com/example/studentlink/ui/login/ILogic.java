package com.example.studentlink.ui.login;

import android.content.Context;
import android.content.Intent;

public interface ILogic {

    public void moveToHome();
    public String checkCredentials(String username, String password);
    public void navigateToNewActivity(Context context, Class moveToThisClass);

}
