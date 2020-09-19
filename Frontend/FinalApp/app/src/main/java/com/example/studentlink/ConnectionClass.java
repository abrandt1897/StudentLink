package com.example.studentlink;

import android.app.Activity;
import android.app.DownloadManager;
import android.content.Context;
import android.os.Bundle;
import android.util.Log;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import org.json.JSONObject;
import java.util.HashMap;
import java.util.Map;


public class ConnectionClass extends AppCompatActivity {

    private Context context;
    String serverResponse = "";

    public ConnectionClass(){};


    public ConnectionClass(Context c){
        context=c;

    };

    public void setContext(Context c){
        context = c;
    }

    //asks for any data coming to app
    public void getRequest(String database)  {
        //Request Queue
        RequestQueue RequestQueue = Volley.newRequestQueue(context);
       // String url = "https://b9bd0a6c-31b5-4e2a-8803-c10a85855393.mock.pstmn.io/" + database;

        String url = "https://b9bd0a6c-31b5-4e2a-8803-c10a85855393.mock.pstmn.io/" + database;

        JsonObjectRequest ObjRequest = new JsonObjectRequest(Request.Method.GET, url, null, new Response.Listener<JSONObject>() {

                @Override
                    public void onResponse(JSONObject response) {
                            serverResponse = response.toString();
                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                    Toast.makeText(context, error.toString(), Toast.LENGTH_SHORT).show();
            }
        });

        // Add the request to the RequestQueue.
        RequestQueue.add(ObjRequest);
        RequestQueue.start();
    }

    public void Other_getRequest(String URL)  {

        //Request Queue
        RequestQueue RequestQueue = Volley.newRequestQueue(context);
        // String url = "https://b9bd0a6c-31b5-4e2a-8803-c10a85855393.mock.pstmn.io/" + database;

        String url = URL;

        JsonObjectRequest ObjRequest = new JsonObjectRequest(Request.Method.GET, url, null, new Response.Listener<JSONObject>() {

            @Override
            public void onResponse(JSONObject response) {
                serverResponse = response.toString();
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(context, error.toString(), Toast.LENGTH_SHORT).show();
            }
        });

        // Add the request to the RequestQueue.
        RequestQueue.add(ObjRequest);
        RequestQueue.start();
    }

    //sends data to database
    public void putRequest(Map<String, String> data, String database){

        //our api's urls
        //String url = "https://localhost:5001/test";
        String url = "https://280c9d98-ea4a-443c-a85b-4e365b376def.mock.pstmn.io/" + database;

        //start a queue for requests for our api
        RequestQueue ExampleRequestQueue = Volley.newRequestQueue(context);


        //create a request object for our api asking for a json object to be returned
        StringRequest putRequest = new StringRequest(Request.Method.PUT, url,
                new Response.Listener<String>()
                {
                    @Override
                    public void onResponse(String response) {
                        serverResponse = response;
                    }
                },
                new Response.ErrorListener()
                {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        // error
                        //Log.d("Error.Response", response);
                    }
                }
        ) {

            @Override
            protected Map<String, String> getParams()
            {
                return data;
            }

        };

        ExampleRequestQueue.add(putRequest);
    }

    public void setResponse(String string){
        serverResponse = string;
    }

    public String getResponse(){
        return serverResponse;
    }




    // Grace's stuff
    public void tryCanvasPost() {
        RequestQueue queue = Volley.newRequestQueue(context);

        String serverUrl = "https://iastate.okta.com/api/v1/authn";

        StringRequest stringRequest = new StringRequest(Request.Method.POST, serverUrl,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        Log.d("TAG", "response = "+ response);
                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Log.d("TAG", "Error = "+ error);
            }
        })
        {

            @Override
            public Map<String, String> getHeaders() {
                HashMap<String, String> headers = new HashMap<>();
                headers.put("accept", "application/json");
                headers.put("Cx-okta-user-agent-extended", "okta-signin-widget-4.1.2");
                headers.put("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36");
                headers.put("content-type", "application/json");
                return headers;
            }
        };
        // Add the request to the RequestQueue.
        queue.add(stringRequest);
    }

    // also Grace's stuff
    private void sendStringPostRequest() {
        RequestQueue queue = Volley.newRequestQueue(context);;
        String url = "http://coms-309-mc-02.cs.iastate.edu:8080/hello";
        String postUrl = "http://coms-309-mc-02.cs.iastate.edu:8080/post";
        StringRequest postRequest = new StringRequest(Request.Method.POST, postUrl,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        System.out.println("RESPONSE: " + response);
                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                System.out.println("ERROR: " + error.toString());
            }
        }) {
            @Override
            public Map<String, String> getParams() {
                Map<String, String> params = new HashMap<>();
                params.put("name", "gematera");
                params.put("password", "pass");

                return params;
            }
        };

        queue.add(postRequest);
    }


}
