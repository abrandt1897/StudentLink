package com.example.studentlink.ui.login;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.TextView;

import com.example.studentlink.R;

public class CreateALogin extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_create_a_login);

        TextView LoginText = findViewById(R.id.LoginText);
        LoginText.setText("Welcome to StudentLink! Please create an account.");

    }
}