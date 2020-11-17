package com.example.studentlink.ui.friends;

import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;
import com.example.studentlink.Global;
import com.example.studentlink.R;
import com.example.studentlink.ui.home.HomeAdapter;
import com.example.studentlink.ui.home.Notification;
import com.example.studentlink.ui.search.SearchAdapter;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

public class FriendsFragment extends Fragment {

    private RequestQueue requestQueue;
    private ListView listview;
    private FriendAdapter friendAdapter;
    private FriendsFragment ff;
    private ArrayList<Friend> friends = new ArrayList<Friend>();

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.friends_list_layout, container, false);
        listview = root.findViewById(R.id.friendView);
        ff=this;
        String databaseName = "api/Students/Friends/" + Global.studentID;
        String url = "http://coms-309-mc-02.cs.iastate.edu:5000/" + databaseName;
        requestQueue = Volley.newRequestQueue(ff.getContext());
        requestQueue.start();

        Map<String,String> header = new HashMap<String,String>();
        header.put("Authorization","Bearer " + Global.bearerToken);

        JsonArrayRequest getRequest = new JsonArrayRequest(Request.Method.GET, url, null, new Response.Listener<JSONArray>()
        {
            @Override
            public void onResponse(JSONArray response) {
//                Toast.makeText(hf.getContext(), "yay stuffs" + response, Toast.LENGTH_SHORT).show();

                if(response.length()==0)return;

                Map<String, Object> theMap = new HashMap<String, Object>();

                for(int i = 0; i < response.length();i++){
                    try {
                        theMap = toMap(response.getJSONObject(i));
                    } catch (JSONException e) {
                        e.printStackTrace();
                    }
                    Friend friend = new Friend(Integer.parseInt(theMap.get("friendID").toString()));
                    setFriendName(friend, Volley.newRequestQueue(ff.getContext()));


                }


            }
        },
                new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        error.printStackTrace();
                    }
                }
        ) {
            @Override
            public Map<String, String> getHeaders() throws AuthFailureError {
                //headers.put("Content-Type", "application/json");
                return header;
            }
        };
        requestQueue.add(getRequest);

        return root;
    }


    public void resetAdapter(FriendAdapter f){
        listview.setAdapter(f);
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

    public void setFriendName(Friend friend, RequestQueue requestQueue){
        String databaseName = "api/Students/" + friend.getID();
        String url = "http://coms-309-mc-02.cs.iastate.edu:5000/" + databaseName;
        requestQueue.start();

        JsonObjectRequest getRequest = new JsonObjectRequest(Request.Method.GET, url, null, new Response.Listener<JSONObject>()
        {
            @Override
            public void onResponse(JSONObject response) {

                if(response.length()==0)return;

                Map<String, Object> theMap = new HashMap<String, Object>();

                try {
                    theMap = toMap(response);
                } catch (JSONException e) {
                    e.printStackTrace();
                }
                friend.setUsername(theMap.get("username").toString());
                friend.setName(theMap.get("fullName").toString());
                friends.add(friend);
                friendAdapter = new FriendAdapter(ff, friends);
                resetAdapter(friendAdapter);

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


}
