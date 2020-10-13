package com.example.studentlink.ui.login.SignIn.Logic;

import android.content.Context;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import com.example.studentlink.ConnectionClass;
import com.example.studentlink.R;
import com.example.studentlink.ui.login.ILogic;
import com.example.studentlink.ui.login.SignIn.LoginActivity;
import com.example.studentlink.ui.splash.SplashFragment;

import java.util.HashMap;
import java.util.Map;

public class LoginLogic implements ILogic {

    private LoginActivity loginClass;
    private ConnectionClass connectionClass;

    public LoginLogic(LoginActivity la){
        loginClass = la;
        connectionClass = new ConnectionClass(la.getApplicationContext());
    }

    @Override
    public void moveToHome() {
        Fragment fragment = new SplashFragment();
        FragmentManager fragmentManager = loginClass.getSupportFragmentManager();
        FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.nav_view, fragment); // drawer_layout
        fragmentTransaction.addToBackStack(null);
//        setContentView(R.layout.controller_layout);
        fragmentTransaction.commit();
//        PageController pc = new PageController();
//        pc.startActivity(new HomeFragment().getActivity().getIntent());
//        pc.navigateUpTo(new HomeFragment().getActivity().getIntent());
//        Intent i = pc.getIntent();
//        pc.startActivity(new HomeFragment().getActivity().getIntent());
//        pc.navigateUpTo(new HomeFragment().getActivity().getIntent());
//        startActivity(i);
    }

    @Override
    public String logInUser(String username, String password) {
        String databaseName = "api/Login";
        String ResponseForAGoodLogin = "Ok";

        Map<String,String> userLoginData = new HashMap<String,String>();
        userLoginData.put("Username",username);
        userLoginData.put("Password",password);
        connectionClass.putRequest(userLoginData,databaseName);

        if(!connectionClass.getResponse().equals("Ok")){
            return "";
        }
        else{
            moveToHome();
        }

        return null;
    }
}
