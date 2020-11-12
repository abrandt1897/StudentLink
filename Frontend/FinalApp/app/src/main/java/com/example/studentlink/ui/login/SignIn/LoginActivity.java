package com.example.studentlink.ui.login.SignIn;

import androidx.appcompat.app.AppCompatActivity;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.example.studentlink.Global;
import com.example.studentlink.PageController;
import com.example.studentlink.R;
import com.example.studentlink.ui.login.ILogic;
import com.example.studentlink.ui.login.SignIn.Logic.LoginLogic;
import com.example.studentlink.ui.login.SignIn.Logic.MockLoginLogic;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import java.util.HashMap;
import java.util.Map;

public class LoginActivity extends AppCompatActivity {

    private Button LoginUserButton;
    private TextView AboveText;
    private EditText username;
    private EditText password;
    private TextView ErrorText;
    private ILogic logic;
    private Context c;


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
//        logic = new LoginLogic(this); // TODO: Change to LoginLogic for actual put request
        c = this;

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
//                    String message = logic.checkCredentials(username.getText().toString(),password.getText().toString());
//                    if(!message.equals("Ok")){
//                        ErrorText.setText("Incorrect username or password");
//                    }

                    String databaseName = "api/Login";
                    Map<String,String> userLoginData = new HashMap<String,String>();
                    userLoginData.put("Username",username.getText().toString()); //greysonj
                    userLoginData.put("Password",password.getText().toString()); //yeet
                    String url = "http://coms-309-mc-02.cs.iastate.edu:5000/" + databaseName;
                    RequestQueue requestQueue;
                    //start a queue for requests for our api
                    requestQueue = Volley.newRequestQueue(c);
                    requestQueue.start();

                    StringRequest putRequest = new StringRequest(Request.Method.PUT, url, new Response.Listener<String>()
                    {
                        @Override
                        public void onResponse(String response) {
                            Toast.makeText(c, "yay connect" + response, Toast.LENGTH_SHORT).show();
                            String[] answer = response.split(" ");
                            Global.studentID = Integer.parseInt(answer[0]);
                            Global.bearerToken = answer[1];
                            ErrorText.setText("Login Successful!");
                            c.startActivity(new Intent(c, PageController.class));
//                            moveToHome();
                        }
                    },
                            new Response.ErrorListener() {
                                @Override
                                public void onErrorResponse(VolleyError error) {
                                    error.printStackTrace();
                                    ErrorText.setText(error.toString());
                                }
                            }
                    ) {
                        @Override
                        protected Map<String, String> getParams()
                        {
                            return userLoginData;
                        }
                    };
                    requestQueue.add(putRequest);

                }
            }
        });
    }

}