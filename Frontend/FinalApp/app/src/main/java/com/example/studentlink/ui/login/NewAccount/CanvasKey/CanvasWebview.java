package com.example.studentlink.ui.login.NewAccount.CanvasKey;

import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
import android.app.ProgressDialog;
import android.content.Context;
import android.graphics.Canvas;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.View;
import android.webkit.CookieManager;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.studentlink.ui.login.ILogic;
import com.example.studentlink.ui.login.NewAccount.Logic.CanvasLogic;
import com.example.studentlink.R;


public class CanvasWebview extends AppCompatActivity {

    private WebView webview;
    private ILogic logic;
    private EditText CanvasToken;
    private Button doneButton;

    private ProgressDialog progDailog;

    @SuppressLint("NewApi")
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_canvas_webview);

        doneButton = findViewById(R.id.doneButton);
        CanvasToken = findViewById(R.id.CanvasToken);
        String username = getIntent().getExtras().getString("Username");
        String password = getIntent().getExtras().getString("Password");
        logic = new CanvasLogic(this);

        progDailog = ProgressDialog.show(this, "Loading","Please wait...", true);
        progDailog.setCancelable(false);

        webview = (WebView) findViewById(R.id.CanvasPage);
        webview.setWebViewClient(new CanvasWebviewClient());
        webview.getSettings().setJavaScriptEnabled(true);
        webview.getSettings().setLoadWithOverviewMode(true);
        webview.getSettings().setUseWideViewPort(true);

        // TODO: look into Jsoup and CookieManager.getInstance().setAcceptCookie(true); and WebViewClient

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
                if(TextUtils.isEmpty(CanvasToken.getText().toString())){
                    CanvasToken.setError("Please enter a Canvas Token.");
                    return;
                }
                logic.setToken(CanvasToken.getText().toString());
                logic.checkCredentials(username, password);
                // error checking???

            }
        });

    }

}