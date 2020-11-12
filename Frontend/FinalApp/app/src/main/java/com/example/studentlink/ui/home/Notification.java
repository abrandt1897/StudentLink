package com.example.studentlink.ui.home;

import android.content.Context;
import android.widget.Toast;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.Volley;
import com.example.studentlink.Global;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

public class Notification {

    private Context context;
    private String description;
    private String type;
    private int senderID;
    private String senderName;
//    private String data;

    public Notification(Context c, int s, String d, String t){
        senderID = s;
        description = d;
        type = t;
        context = c;
        this.setSenderName();
    }

    public String getType(){
        return type;
    }

    public void setSenderName(){
        String databaseName = "api/Students/" + getSenderID(); // test with 81537
        String url = "http://coms-309-mc-02.cs.iastate.edu:5000/" + databaseName;
        RequestQueue requestQueue;
        requestQueue = Volley.newRequestQueue(context);
        requestQueue.start();

        JsonArrayRequest getRequest = new JsonArrayRequest(Request.Method.GET, url, null, new Response.Listener<JSONArray>()
        {
            @Override
            public void onResponse(JSONArray response) {

                if(response.length()==0)return;

                Map<String, Object> theMap = new HashMap<String, Object>();

                try {
                    theMap = toMap(response.getJSONObject(0));
                } catch (JSONException e) {
                    e.printStackTrace();
                }

                senderName = theMap.get("username").toString();

            }
        },
                new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        error.printStackTrace();
                    }
                }
        ) {
        };
        requestQueue.add(getRequest);
    }


    public String getSender(){
        return senderName;
    }

    public int getSenderID(){ return senderID; }

    public String getDescription(){
        return description;
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

}
