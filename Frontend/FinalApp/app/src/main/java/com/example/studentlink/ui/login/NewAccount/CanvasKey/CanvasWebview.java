package com.example.studentlink.ui.login.NewAccount.CanvasKey;

import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
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

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.example.studentlink.Global;
import com.example.studentlink.PageController;
import com.example.studentlink.ui.login.ILogic;
import com.example.studentlink.ui.login.NewAccount.Logic.CanvasLogic;
import com.example.studentlink.R;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;


public class CanvasWebview extends AppCompatActivity {

    private WebView webview;
    private ILogic logic;
    private EditText CanvasToken;
    private Button doneButton;
    private Context c;

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
        c = this;
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
        webview.loadUrl("https://canvas.iastate.edu/profile/settings");

        doneButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(TextUtils.isEmpty(CanvasToken.getText().toString())){
                    CanvasToken.setError("Please enter a Canvas Token.");
                    return;
                }
//                logic.setToken(CanvasToken.getText().toString());
//                String response = logic.checkCredentials(username, password);

                String databaseName = "api/Students/" + CanvasToken.getText().toString();
                Map<String,String> userLoginData = new HashMap<String,String>();
                userLoginData.put("Username",username);
                userLoginData.put("Password",password);
//                userLoginData.put("canvasOAuthToken", CanvasToken.getText().toString());
                String url = "http://coms-309-mc-02.cs.iastate.edu:5000/" + databaseName;
                RequestQueue requestQueue;
                requestQueue = Volley.newRequestQueue(c);
                requestQueue.start();

                StringRequest putRequest = new StringRequest(Request.Method.POST, url, new Response.Listener<String>()
                {
                    @Override
                    public void onResponse(String response) {
                        Toast.makeText(c, "yay connect" + response, Toast.LENGTH_SHORT).show();
                        Toast.makeText(c,"Congrats you made your account!",Toast.LENGTH_LONG).show();
                        String ans[] = response.split(",");
                        String studID = ans[0].split(":")[1];
                        Global.studentID = Integer.parseInt(studID);
//                        String[] answer = response.split(" ");
//                        Global.studentID = Integer.parseInt(answer[0]);
//                        Global.bearerToken = answer[1];
//                        Toast.makeText(c,"Congrats you made your account!  " + Global.studentID + " &  " + Global.bearerToken, Toast.LENGTH_LONG).show();
                        c.startActivity(new Intent(c, PageController.class));
                    }
                },
                        new Response.ErrorListener() {
                            @Override
                            public void onErrorResponse(VolleyError error) {
                                error.printStackTrace();
                                Toast.makeText(c,"Error", Toast.LENGTH_LONG).show();
                            }
                        }
                ) {
//                    @Override
//                    protected Map<String, String> getParams()
//                    {
//                        return userLoginData;
//                    }

                    @Override
                    public byte[] getBody() throws AuthFailureError {
                        JSONObject obj = new JSONObject();
                        try {
                            obj.put("Username", username);
                            obj.put("Password", password);
//                            obj.put("canvasOAuthToken", CanvasToken.getText().toString());
                        } catch (JSONException e) {
                            e.printStackTrace();
                        }

                        byte[] body = new byte[0];
                        try {
                            body = obj.toString().getBytes("UTF-8");
                        } catch (Exception e) {
                            e.printStackTrace();
                        }
                        return body;
                    }

                    @Override
                    public String getBodyContentType() {
                        return "application/json";
                    }

                };
                requestQueue.add(putRequest);
            }
        });

    }

}