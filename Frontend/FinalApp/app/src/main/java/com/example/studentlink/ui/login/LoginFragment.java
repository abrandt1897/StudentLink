package com.example.studentlink.ui.login;

import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;

import com.example.studentlink.R;
import com.example.studentlink.ui.login.NewAccount.CreateAccount;
import com.example.studentlink.ui.login.SignIn.LoginActivity;

public class LoginFragment extends Fragment {

    private Button theCreateAccountButton;
    private Button theLoginButton;

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.login_layout, container, false);

        TextView LoginText = root.findViewById(R.id.LoginText);
        LoginText.setText("Welcome to StudentLink! Are you an existing or new user?");

        // For the create account button, go to CreateAccount page when clicked
        theCreateAccountButton = (Button) root.findViewById(R.id.CreateButton);
        Intent createIntent = new Intent(this.getContext(), CreateAccount.class);
         theCreateAccountButton.setOnClickListener(new View.OnClickListener() {
             @Override
             public void onClick(View view) {
                 startActivity(createIntent);
             }
         });

         // For the login button, go to LoginActivity page when clicked
        theLoginButton = (Button) root.findViewById(R.id.LoginButton);
        Intent loginIntent = new Intent(this.getContext(), LoginActivity.class);
        theLoginButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                startActivity(loginIntent);
            }
        });

        return root;

    }

}