package com.example.studentlink.ui.login;

import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
import android.app.ProgressDialog;
import android.content.Context;
import android.os.Bundle;
import android.view.View;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.studentlink.ConnectionClass;
import com.example.studentlink.R;

import java.util.HashMap;
import java.util.Map;

public class CanvasWebview extends AppCompatActivity {

    private WebView webview;

    private ProgressDialog progDailog;

    @SuppressLint("NewApi")
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_canvas_webview);

        Button doneButton = findViewById(R.id.doneButton);
        EditText CanvasToken = findViewById(R.id.CanvasToken);
        ConnectionClass connectionClass = new ConnectionClass(this.getApplicationContext());
        Context context = this;
        String username = getIntent().getExtras().getString("Username");
        String password = getIntent().getExtras().getString("Password");

        progDailog = ProgressDialog.show(this, "Loading","Please wait...", true);
        progDailog.setCancelable(false);

        webview = (WebView) findViewById(R.id.CanvasPage);
        webview.setWebViewClient(new CanvasWebviewClient());
        webview.getSettings().setJavaScriptEnabled(true);
        webview.getSettings().setLoadWithOverviewMode(true);
        webview.getSettings().setUseWideViewPort(true);

        webview.setWebViewClient(new WebViewClient(){

            @Override
            public boolean shouldOverrideUrlLoading(WebView view, String url) {
                progDailog.show();
                view.loadUrl(url);

                return true;
            }
            @Override
            public void onPageFinished(WebView view, final String url) {
                progDailog.dismiss();
            }
        });
        webview.loadUrl("https://canvas.iastate.edu/");

        doneButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                // TODO: Need to error check for an incorrect CanvasToken or empty
                String databaseName = "api/Students/" + CanvasToken.getText().toString();
                String ResponseForAGoodLogin = "Ok";
                Map<String,String> userLoginData = new HashMap<String,String>();
                userLoginData.put("Username",username);
                userLoginData.put("Password",password);
                connectionClass.putRequest(userLoginData,databaseName);
                if(!connectionClass.getResponse().equals(ResponseForAGoodLogin)){
                    CanvasToken.setError("Your Canvas Token is incorrect.");
                    return;
                }
                else{
                    Toast.makeText(context,"Congrats you made your account!", Toast.LENGTH_LONG).show();
                    // MoveToHome();
                }
            }
        });

    }

}