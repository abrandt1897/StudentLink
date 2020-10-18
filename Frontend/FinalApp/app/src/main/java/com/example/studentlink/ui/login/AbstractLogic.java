package com.example.studentlink.ui.login;

import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import com.example.studentlink.ConnectionClass;
import com.example.studentlink.R;
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
        Fragment fragment = new SplashFragment();
        FragmentManager fragmentManager = theActivity.getSupportFragmentManager();
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
    public String checkCredentials(String username, String password) {
        return null;
    }


}
