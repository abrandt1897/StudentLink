package com.example.studentlink;

import android.content.Context;
import android.util.Log;
import android.widget.Toast;
import androidx.appcompat.app.AppCompatActivity;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.example.studentlink.ui.home.Notification;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.UnsupportedEncodingException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

public class ConnectionClass extends AppCompatActivity {

    private Context context;
    String serverResponse = "";
    ArrayList<String> serverResponseArray = new ArrayList<String>();
    boolean arrayFinished = false;
    private Map<String,String> theHeaders;
    private Map<String,String> theParameters;
    JSONObject objectOfServerResponse;
    private RequestQueue requestQueue;

    public ConnectionClass(){
        theHeaders = new HashMap<String,String>();
        theHeaders.put("bearerToken", Global.bearerToken);
        theParameters = new HashMap<String,String>();
        theParameters.put("StudentID", Global.studentID + "");
    };

    public ConnectionClass(Context c){
        context=c;
        theHeaders = new HashMap<String,String>();
        theHeaders.put("bearerToken",Global.bearerToken);
    };

    public void setContext(Context c){
        context = c;
    }

    //asks for any data coming to app
    public void getRequest(String database)  {
        //Request Queue
        RequestQueue RequestQueue = Volley.newRequestQueue(context);

        String url = "http://coms-309-mc-02.cs.iastate.edu:5000/" + database;

        JsonObjectRequest ObjRequest = new JsonObjectRequest(Request.Method.GET, url, null, new Response.Listener<JSONObject>() {

            @Override
            public void onResponse(JSONObject response) {
                objectOfServerResponse = response;
                serverResponse = response.toString();

                try {
                    JSONArray array = response.getJSONArray("response");
                    serverResponse = array.toString();

                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(context, error.toString(), Toast.LENGTH_SHORT).show();
            }
        }){
            @Override
            public Map<String, String> getHeaders() throws AuthFailureError {
                //headers.put("Content-Type", "application/json");
                return theHeaders;
            }
            @Override
            public Map<String, String> getParams() {
                return theParameters;
            }
        };

        // Add the request to the RequestQueue.
        RequestQueue.add(ObjRequest);
        RequestQueue.start();
    }

    public void getArrayRequest(String database)  {
        //Request Queue
        RequestQueue RequestQueue = Volley.newRequestQueue(context);

        String url = "http://coms-309-mc-02.cs.iastate.edu:5000/" + database;

        JsonArrayRequest arrayRequest = new JsonArrayRequest(Request.Method.GET, url, null, new Response.Listener<JSONArray>() {

            @Override
            public void onResponse(JSONArray response) {
                if(response.length()==0)return;

                Map<String, Object> theMap = new HashMap<String, Object>();

                for(int i = 0; i < response.length(); i++){
                    try {
                        theMap = toMap(response.getJSONObject(i));
                    } catch (JSONException e) {
                        e.printStackTrace();
                    }

                    serverResponseArray.add(theMap.get("chatID").toString());
                    //Log.i("chatIDs", serverResponseArray.toString());
                }

                arrayFinished = true;

            }

        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(context, error.toString(), Toast.LENGTH_SHORT).show();
            }
        });

        // Add the request to the RequestQueue.
        RequestQueue.add(arrayRequest);
        RequestQueue.start();
    }

    public void getArrayRequest1(String database)  {
        //Request Queue
        RequestQueue RequestQueue = Volley.newRequestQueue(context);

        String url = "http://coms-309-mc-02.cs.iastate.edu:5000/" + database;

        JsonArrayRequest arrayRequest = new JsonArrayRequest(Request.Method.GET, url, null, new Response.Listener<JSONArray>() {

            @Override
            public void onResponse(JSONArray response) {
                serverResponse = response.toString();

            }

        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(context, error.toString(), Toast.LENGTH_SHORT).show();
            }
        });

        // Add the request to the RequestQueue.
        RequestQueue.add(arrayRequest);
        RequestQueue.start();
    }

    public static Map<String, Object> toMap(JSONObject jsonobj)  throws JSONException {
        Map<String, Object> map = new HashMap<String, Object>();
        Iterator<String> keys = jsonobj.keys();
        while(keys.hasNext()) {
            String key = keys.next();
            Object value = jsonobj.get(key);
            if (value instanceof JSONArray) {
                value = toList((JSONArray) value);
            } else if (value instanceof JSONObject) {
                value = toMap((JSONObject) value);
            }
            map.put(key, value);
        }   return map;
    }

    public static List<Object> toList(JSONArray array) throws JSONException {
        List<Object> list = new ArrayList<Object>();
        for(int i = 0; i < array.length(); i++) {
            Object value = array.get(i);
            if (value instanceof JSONArray) {
                value = toList((JSONArray) value);
            }
            else if (value instanceof JSONObject) {
                value = toMap((JSONObject) value);
            }
            list.add(value);
        }   return list;
    }



    //sends data to database
    public void putRequest(Map<String, String> data, String database){
        //our api's urls
        String url = "http://coms-309-mc-02.cs.iastate.edu:5000/" + database;

        //start a queue for requests for our api
        requestQueue = Volley.newRequestQueue(context);
        requestQueue.start();

        //create a request object for our api asking for a json object to be returned
        StringRequest putRequest = new StringRequest(Request.Method.PUT, url, new Response.Listener<String>()
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
                        error.printStackTrace();
                        Toast.makeText(context, error.toString(), Toast.LENGTH_SHORT).show();
                        serverResponse = "Bad Response";
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

        requestQueue.add(putRequest);
    }

    //sends data to database
    public void postRequest(Map<String, String> data, String database){
        //our api's urls
        String url = "http://coms-309-mc-02.cs.iastate.edu:5000/" + database;

        //start a queue for requests for our api
        requestQueue = Volley.newRequestQueue(context);
        requestQueue.start();

        //create a request object for our api asking for a json object to be returned
        StringRequest putRequest = new StringRequest(Request.Method.POST, url, new Response.Listener<String>()
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
                        error.printStackTrace();
                        Toast.makeText(context, error.toString(), Toast.LENGTH_SHORT).show();
                        serverResponse = "Bad Response";
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

        requestQueue.add(putRequest);
    }

    public String getResponse(){ return serverResponse; }

    public ArrayList<String> getResponseArray(){
        while(true){
            if(arrayFinished) break;
        }
        return serverResponseArray;
    }

    public JSONObject getObjectOfServerResponse(){
        return objectOfServerResponse;
    }




    // Broken due to MFA currently
    public void tryCanvasPost(Map<String, String> data) throws JSONException {
//        CookieStore cookieStore = new MyCookieStore();
//        CookieManager manager = new CookieManager( cookieStore, CookiePolicy.ACCEPT_ALL );
//        CookieHandler.setDefault( manager  );

        JSONObject jsonBody = new JSONObject();
        //HttpRequest.BodyPublishers.ofString("{\"password\":\"wolf2Link\",\"username\":\"gematera@iastate.edu\",\"options\":{\"warnBeforePasswordExpired\":true,\"multiOptionalFactorEnroll\":true}}"))
        jsonBody.put("password", data.get("password"));
        jsonBody.put("username", data.get("username"));

        JSONObject jsonBodyOptions = new JSONObject();
        jsonBodyOptions.put("warnBeforePasswordExpired", "true");
        jsonBodyOptions.put("multiOptionalFactorEnroll", "true");
        jsonBody.put("options", jsonBodyOptions);
        final String mRequestBody = jsonBody.toString();

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

            @Override
            public String getBodyContentType() {
                return "application/json; charset=utf-8";
            }
            @Override
            public byte[] getBody() {
                try {
                    return mRequestBody == null ? null : mRequestBody.getBytes("utf-8");
                } catch (UnsupportedEncodingException e) {
                    e.printStackTrace();
                    return null;
                }
            }

        };
        // Add the request to the RequestQueue.
        queue.add(stringRequest);
    }


}

