package com.example.studentlink.ui.login;

import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProviders;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.example.studentlink.ConnectionClass;
import com.example.studentlink.R;

import java.util.HashMap;
import java.util.Map;

public class LoginFragment extends Fragment {

    private Button theLoginButton;

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.login_layout, container, false);

        TextView LoginText = root.findViewById(R.id.LoginText);

        LoginText.setText("Login Page Stuff");

        theLoginButton = root.findViewById(R.id.LoginButton);
        theLoginButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                TextView usernameText = root.findViewById(R.id.textUserName);
                String username = usernameText.getText().toString();
                TextView passwordText = root.findViewById(R.id.textPassword);
                String password = passwordText.getText().toString();

                ConnectionClass cc = new ConnectionClass();
                //cc.tryCanvasPost(getContext());
                //cc.putRequest(usernameText.getText().toString());
                //cc.putRequest(passwordText.getText().toString());
                cc.Other_getRequest("https://iastate.okta.com/api/v1/authn", getContext());
                //cc.sendStringPostRequest();
                LoginText.setText(cc.getResponse());
            }

        });


        return root;
    }






}