package com.example.studentlink;

import android.app.Activity;
import android.app.DownloadManager;
import android.content.Context;
import android.os.Bundle;
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


public class ConnectionClass extends AppCompatActivity {

    private Context context;
    public ConnectionClass(){};
    String serverResponse = "";




    //asks for any data coming to app
    public void getRequest(String database, Context context)  {
        this.context = context;


        //Request Queue
        RequestQueue RequestQueue = Volley.newRequestQueue(context);
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

    //sends data to database
    public void putRequest(String data){

        //our api's url
        String url = "https://localhost:5001/test";

        //start a queue for requests for our api
        RequestQueue ExampleRequestQueue = Volley.newRequestQueue(this);

        //create a request object for our api asking for a json object to be returned
        JsonObjectRequest ExampleRequest = new JsonObjectRequest(Request.Method.GET, url, null, new Response.Listener<JSONObject>() {

            @Override
            public void onResponse(JSONObject response) {
                //when our api responds then we do somthing with the data
                Toast toast = Toast.makeText(getApplicationContext(), response.toString(), Toast.LENGTH_SHORT);
            }

        }, new Response.ErrorListener() { //Create an error listener to handle errors appropriately.
            @Override
            public void onErrorResponse(VolleyError error) {
                //This code is executed if there is an error.
            }
        });
        //add the request object to the queue of requests
        ExampleRequestQueue.add(ExampleRequest);
    }

    public void setResponse(String string){
        serverResponse = string;
    }

    public String getResponse(){
        return serverResponse;
    }

}
