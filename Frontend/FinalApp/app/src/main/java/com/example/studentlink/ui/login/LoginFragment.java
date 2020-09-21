package com.example.studentlink.ui.login;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.webkit.WebView;
import android.widget.Button;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;

import com.example.studentlink.ConnectionClass;
import com.example.studentlink.R;

import org.json.JSONException;

import java.util.HashMap;
import java.util.Map;
import java.net.CookieManager;
import java.net.CookiePolicy;

public class LoginFragment extends Fragment {

    private Button theLoginButton;

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.login_layout, container, false);

        WebView myWebView = (WebView) root.findViewById(R.id.CanvasPage);
        myWebView.loadUrl("https://canvas.iastate.edu/");


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
                cc.setContext(getContext());

                Map<String,String> dataMap = new HashMap<String,String>();

                dataMap.put("username",username);
                dataMap.put("password",password);
                cc.putRequest(dataMap, "test");

//                CookieManager CM = new CookieManager();
//                CM.setCookiePolicy(CookiePolicy.ACCEPT_ALL);

//                try {
//                    cc.tryCanvasPost(dataMap);
//                } catch (JSONException e) {
//                    e.printStackTrace();
//                }
                LoginText.setText(cc.getResponse());
            }

        });


        return root;

    }






}