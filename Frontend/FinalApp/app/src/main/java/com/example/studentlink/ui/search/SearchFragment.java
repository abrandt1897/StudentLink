package com.example.studentlink.ui.search;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.SearchView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProviders;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.example.studentlink.Global;
import com.example.studentlink.R;
import com.example.studentlink.ui.home.HomeAdapter;
import com.example.studentlink.ui.home.Notification;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

public class SearchFragment extends Fragment {

    private SearchAdapter searchAdapter;
    private SearchFragment sf;
    private TextView textUserNotExist;
    private EditText searchText;
    private Button bAccept;
    private TextView textUserRequest;
    private Map<String, Object> theMap;

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.search_layout, container, false);
        bAccept = root.findViewById(R.id.bAccept);
        textUserRequest = root.findViewById(R.id.textUserRequest);

        textUserRequest.setVisibility(View.GONE);
        bAccept.setVisibility(View.GONE);

        searchText = root.findViewById(R.id.SearchText);
        Button searchButton = root.findViewById(R.id.searchButton);
        textUserNotExist = root.findViewById(R.id.textUserNotExist);
        sf = this;

        bAccept.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String databaseName = "api/Notifications/" + theMap.get("studentID");
                String url = "http://coms-309-mc-02.cs.iastate.edu:5000/" + databaseName;
                RequestQueue requestQueue = Volley.newRequestQueue(sf.getContext());
                requestQueue.start();

                Notification notification = new Notification(sf.getContext(),Global.studentID,"You've got a friend request","Request");

                // todo: post request for sending request
                JsonObjectRequest postRequest = new JsonObjectRequest(Request.Method.POST,url,null, new Response.Listener<JSONObject>() {
                    @Override
                    public void onResponse(JSONObject response) {
                        Toast.makeText(sf.getContext(), "Request Sent Successfully", Toast.LENGTH_SHORT).show();
                        bAccept.setEnabled(false);
                    }
                }, new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        error.printStackTrace();
                    }

                }){
                    @Override
                    public byte[] getBody() {
                        JSONObject obj = new JSONObject();
                    try {
                        obj.put("studentID", theMap.get("studentID"));
                        obj.put("data", theMap.get("data"));
                        obj.put("type", "Request");
                        obj.put("description",theMap.get("description"));
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

            }
        });


        searchButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String databaseName = "api/Students/Username/" + searchText.getText();
                String url = "http://coms-309-mc-02.cs.iastate.edu:5000/" + databaseName;
                RequestQueue requestQueue = Volley.newRequestQueue(sf.getContext());
                requestQueue.start();

                Map<String,String> header = new HashMap<String,String>();
                header.put("Authorization","Bearer " + Global.bearerToken);


                JsonObjectRequest getRequest = new JsonObjectRequest(Request.Method.GET, url, null, new Response.Listener<JSONObject>()
                {
                    @Override
                    public void onResponse(JSONObject response) {
                        theMap = new HashMap<String, Object>();

                        try {
                            theMap = toMap(response);
                        } catch (JSONException e) {
                            e.printStackTrace();
                        }


                        textUserRequest.setVisibility(View.VISIBLE);
                        bAccept.setVisibility(View.VISIBLE);
                        bAccept.setEnabled(false);

                        textUserRequest.setText("Username: " + theMap.get("username") + "\nFull Name: " + theMap.get("fullName"));


                        String databaseName = "api/Students/Friends/" + Global.studentID + "/" + theMap.get("studentID");
                        String url = "http://coms-309-mc-02.cs.iastate.edu:5000/" + databaseName;
                        RequestQueue ifFriendsrequestQueue = Volley.newRequestQueue(sf.getContext());
                        ifFriendsrequestQueue.start();

                        Map<String,String> header = new HashMap<String,String>();
                        header.put("Authorization","Bearer " + Global.bearerToken);

                        StringRequest getIfFriendsRequest = new StringRequest(Request.Method.GET, url, new Response.Listener<String>() {
                            @Override
                            public void onResponse(String response) {
                                if(!Boolean.parseBoolean(response)) {
                                    bAccept.setEnabled(true);

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

                        ifFriendsrequestQueue.add(getIfFriendsRequest);


//                        searchAdapter = new SearchAdapter(theMap, sf);
//                        resetAdapter(searchAdapter);

                    }
                },
                        new Response.ErrorListener() {
                            @Override
                            public void onErrorResponse(VolleyError error) {
                                textUserNotExist.setText("This username does not correspond to a user.");
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
            }
        });

        return root;
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






