package com.example.studentlink.ui.login.SignIn;

import androidx.appcompat.app.AppCompatActivity;

import com.example.studentlink.R;
import com.example.studentlink.ui.login.ILogic;
import com.example.studentlink.ui.login.SignIn.Logic.LoginLogic;

import android.os.Bundle;
import android.text.TextUtils;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class LoginActivity extends AppCompatActivity {

    private Button LoginUserButton;
    private TextView AboveText;
    private EditText username;
    private EditText password;
    private TextView ErrorText;
    private ILogic logic;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        AboveText = findViewById(R.id.textView);
        AboveText.setText("Welcome back to StudentLink! Please log in.");
        LoginUserButton = findViewById(R.id.loginUser);
        LoginUserButton.setText("Sign In");
        username = findViewById(R.id.LoginUsername);
        password = findViewById(R.id.LoginPassword);
        ErrorText = findViewById(R.id.ShowErrorText);
        logic = new LoginLogic(this);

        LoginUserButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(TextUtils.isEmpty(username.getText().toString())){
                    username.setError("Please enter username.");
                }
                else if(TextUtils.isEmpty(password.getText().toString())){
                    password.setError("Please enter a password.");
                }
                else{
                    String message = logic.checkCredentials(username.getText().toString(),password.getText().toString());
                    if(!message.equals("Ok")){
                        ErrorText.setText("Incorrect username or password");
                    }

                }
            }
        });
    }

}