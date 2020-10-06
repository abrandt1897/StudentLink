package com.example.studentlink.ui.login;

import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import com.example.studentlink.ConnectionClass;
import com.example.studentlink.PageController;
import com.example.studentlink.R;

import android.content.Intent;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import java.sql.Connection;

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

        ConnectionClass cc = new ConnectionClass(this.getApplicationContext());
        PageController pc = new PageController();
        Intent i = pc.getIntent();

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
                    // check user and password with backend
                    startActivity(i); // goes to Login page, need to change to Home page
                }

            }
        });

    }


    private void MoveToHome(){
//        Fragment fragment = new LoginFragment();
//        FragmentManager fragmentManager = getActivity().getSupportFragmentManager();
//        FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
//        fragmentTransaction.replace(R.id.nav_home, fragment);
//        fragmentTransaction.addToBackStack(null);
//        fragmentTransaction.commit();


    }
}