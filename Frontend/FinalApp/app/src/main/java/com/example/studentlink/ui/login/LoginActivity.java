package com.example.studentlink.ui.login;

import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import com.example.studentlink.ConnectionClass;
import com.example.studentlink.PageController;
import com.example.studentlink.R;
import com.example.studentlink.ui.home.HomeFragment;

import android.content.Intent;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import java.util.HashMap;
import java.util.Map;

public class LoginActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        TextView AboveText = findViewById(R.id.textView);
        AboveText.setText("Welcome back to StudentLink! Please log in.");
        Button LoginUserButton = findViewById(R.id.loginUser);
        LoginUserButton.setText("Sign In");
        EditText username = findViewById(R.id.LoginUsername);
        EditText password = findViewById(R.id.LoginPassword);
        TextView ErrorText = findViewById(R.id.ShowErrorText);

        PageController pc = new PageController();
//        pc.startActivity(new HomeFragment().getActivity().getIntent());
//        pc.navigateUpTo(new HomeFragment().getActivity().getIntent());
        Intent i = pc.getIntent();

        ConnectionClass connectionClass = new ConnectionClass(this.getApplicationContext());

        LoginUserButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(TextUtils.isEmpty(username.getText().toString())){
                    username.setError("Please enter username.");
                    return;
                }
                else if(TextUtils.isEmpty(password.getText().toString())){
                    password.setError("Please enter a password.");
                    return;
                }
                else{
                    String databaseName = "api/Login";
                    String ResponseForAGoodLogin = "Ok";
                    Map<String,String> userLoginData = new HashMap<String,String>();
                    userLoginData.put("Username",username.getText().toString());
                    userLoginData.put("Password",password.getText().toString());
//                    connectionClass.putRequest(userLoginData,databaseName);
                    if(connectionClass.getResponse().equals(ResponseForAGoodLogin)){ //TODO: Add ! for real functionality
                        ErrorText.setText("Incorrect username or password");
                        return;
                    }
                    else{
                        pc.startActivity(new HomeFragment().getActivity().getIntent());
//                        pc.navigateUpTo(new HomeFragment().getActivity().getIntent());
//                        startActivity(i);
//                        MoveToHome(); // TODO: Broken: goes to Login page, needs to go to Home page
                    }
                }
            }
        });
    }

    /**
     * Method to navigate to the Home Fragment
     */
    private void MoveToHome(){
        // Tenson, thanks for looking at this code!
        Fragment fragment = new HomeFragment();
        FragmentManager fragmentManager = this.getSupportFragmentManager();
        FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.drawer_layout, fragment);
        fragmentTransaction.addToBackStack(null);
        setContentView(R.layout.controller_layout);
        fragmentTransaction.commit();
    }
}