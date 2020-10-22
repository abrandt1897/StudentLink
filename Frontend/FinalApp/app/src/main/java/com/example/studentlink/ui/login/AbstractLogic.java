package com.example.studentlink.ui.login;

import android.content.Intent;

import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import com.example.studentlink.ConnectionClass;
import com.example.studentlink.PageController;
import com.example.studentlink.R;
import com.example.studentlink.ui.home.HomeFragment;
import com.example.studentlink.ui.splash.SplashFragment;

public class AbstractLogic implements ILogic{

    protected AppCompatActivity theActivity;
    protected ConnectionClass connectionClass;

    public AbstractLogic(AppCompatActivity activity){
        theActivity = activity;
        connectionClass = new ConnectionClass(activity.getApplicationContext());
    }

    @Override
    public void moveToHome() {
//        Fragment fragment = new HomeFragment();
//        FragmentManager fragmentManager = theActivity.getSupportFragmentManager();
//        FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
//        fragmentTransaction.replace(R.id.nav_view, fragment); // drawer_layout
//        fragmentTransaction.addToBackStack(null);
//        fragmentTransaction.commit();
//        setContentView(R.layout.controller_layout);
//        PageController pc = new PageController();
//        pc.startActivity(new HomeFragment().getActivity().getIntent());
//        pc.navigateUpTo(new HomeFragment().getActivity().getIntent());
//        Intent i = pc.getIntent();
//        theActivity.startActivity(i);
        theActivity.startActivity(new Intent(theActivity, PageController.class));
//        pc.navigateUpTo(new HomeFragment().getActivity().getIntent());
//        startActivity(i);
    }

    @Override
    public String checkCredentials(String username, String password) {
        return null;
    }

    @Override
    public void setToken(String token) {
    }

}
