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

        // use to change to Home page?
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
                    // TODO: check user and password with backend
                    String databaseName = "table";
                    String ResponseForAGoodLogin = "isaUser";
//                    Map<String,String> userLoginData = new HashMap<String,String>();
//                    userLoginData.put("Username",username.getText().toString());
//                    userLoginData.put("Password",password.getText().toString());
//                    cc.putRequest(userLoginData,databaseName);
                    if(cc.getResponse().equals(ResponseForAGoodLogin)){
                        ErrorText.setText("Incorrect username or password");
                        return;
                    }
                    else{
                        MoveToHome(); // TODO: goes to Login page, need to change to Home page
                    }

                }

            }
        });

    }

    private void MoveToHome(){
        Fragment fragment = new HomeFragment();
        FragmentManager fragmentManager = this.getSupportFragmentManager();
        FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
        fragmentTransaction.replace(R.id.home, fragment);
        fragmentTransaction.addToBackStack(null);
        fragmentTransaction.commit();
    }
}