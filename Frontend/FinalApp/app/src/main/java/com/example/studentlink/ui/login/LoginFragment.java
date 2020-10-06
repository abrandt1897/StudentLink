package com.example.studentlink.ui.login;

import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.webkit.WebView;
import android.widget.Button;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import com.example.studentlink.ConnectionClass;
import com.example.studentlink.R;

public class LoginFragment extends Fragment {

    private Button theCreateAccountButton;
    private Button theLoginButton;

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.login_layout, container, false);

        TextView LoginText = root.findViewById(R.id.LoginText);
        LoginText.setText("Welcome to StudentLink!");

        theCreateAccountButton = (Button) root.findViewById(R.id.CreateButton);
        Intent createIntent = new Intent(this.getContext(), CreateALogin.class);
         theCreateAccountButton.setOnClickListener(new View.OnClickListener() {
             @Override
             public void onClick(View view) {
                 startActivity(createIntent);
             }
         });

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