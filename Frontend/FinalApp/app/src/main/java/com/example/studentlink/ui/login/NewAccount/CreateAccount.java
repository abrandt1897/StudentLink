package com.example.studentlink.ui.login.NewAccount;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import com.example.studentlink.ConnectionClass;
import com.example.studentlink.R;
import com.example.studentlink.ui.login.ILogic;
import com.example.studentlink.ui.login.NewAccount.CanvasKey.CanvasWebview;
import com.example.studentlink.ui.login.NewAccount.Logic.CreateAccountLogic;
import com.example.studentlink.ui.login.NewAccount.Logic.MockCreateAccountLogic;

public class CreateAccount extends AppCompatActivity {

    private Button nextButton;
    private EditText username;
    private EditText password;
    private TextView LoginText;
    private TextView ErrorText;
    private ILogic logic;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_create_a_login);

        nextButton = findViewById(R.id.NextButton);
        username = findViewById(R.id.textUserName);
        password = findViewById(R.id.textPassword);
        LoginText = findViewById(R.id.LoginText);
        LoginText.setText("Welcome to StudentLink! Please create an account.");
        ErrorText = findViewById(R.id.CreateAccountErrorText);
        logic = new MockCreateAccountLogic(this); // TODO: Remove Mock


        Context c = this;
        ConnectionClass connection = new ConnectionClass(this.getApplicationContext());
        Intent nextIntent = new Intent(this, CanvasWebview.class);

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
                    String message = logic.checkCredentials(username.getText().toString(),password.getText().toString());
                    if(!message.equals("Ok")){
                        ErrorText.setText("This username is taken. Please enter a different username.");
                    }

                }

            }
        });

    }
}