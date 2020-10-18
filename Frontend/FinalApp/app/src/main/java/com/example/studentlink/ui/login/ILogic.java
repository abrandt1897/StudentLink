package com.example.studentlink.ui.login;

import android.content.Context;
import android.content.Intent;

public interface ILogic {

    void moveToHome();
    String checkCredentials(String username, String password);

}
