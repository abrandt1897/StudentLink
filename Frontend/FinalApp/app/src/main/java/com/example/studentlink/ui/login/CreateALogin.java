package com.example.studentlink.ui.login;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.example.studentlink.PageController;
import com.example.studentlink.R;

public class CreateALogin extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_create_a_login);
        Button nextButton = findViewById(R.id.NextButton);
        EditText username = findViewById(R.id.textUserName);
        EditText password = findViewById(R.id.textPassword);
        TextView LoginText = findViewById(R.id.LoginText);
        LoginText.setText("Welcome to StudentLink! Please create an account.");

        // use to change to home page
        PageController pc = new PageController();
        Intent i = pc.getIntent();

        nextButton.setOnClickListener(new View.OnClickListener() {
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
}