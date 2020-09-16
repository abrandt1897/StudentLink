package com.example.studentlink.ui.login;

import android.os.Bundle;
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

import com.example.studentlink.R;

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
                // call  method
            }

        });



        return root;
    }




}