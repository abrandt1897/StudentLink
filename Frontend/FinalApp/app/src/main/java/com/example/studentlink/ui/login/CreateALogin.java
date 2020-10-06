package com.example.studentlink.ui.login;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.example.studentlink.ConnectionClass;
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
        TextView ErrorText = findViewById(R.id.CreateAccountErrorText);


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
                    // check account with backend
                    String databaseName = "table";
                    String ResponseForAGoodLogin = "";
//                    Map<String,String> userLoginData = new HashMap<String,String>();
//                    userLoginData.put("Username",username.getText().toString());
//                    userLoginData.put("Password",password.getText().toString());
//                    cc.putRequest(userLoginData,databaseName);
                    if(!connection.getResponse().equals(ResponseForAGoodLogin)){
                        ErrorText.setText("This username is taken. Please enter a different username.");
                        return;
                    }
                    else{
                        startActivity(nextIntent); // TODO: go to WebView
                    }

                }

            }
        });


    }
}