package com.example.studentlink.ui.home;

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
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.example.studentlink.Global;
import com.example.studentlink.R;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

import javax.xml.transform.ErrorListener;

public class HomeFragment extends Fragment {

    private List<Notification> notifications;
    private ListView listview;
    private HomeLogic logic;
    private HomeAdapter homeAdapter;
    private HomeFragment hf;
    private RequestQueue requestQueue;

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.home_layout, container, false);
        listview = root.findViewById(R.id.listview);
        logic = new HomeLogic(this.getContext());
        notifications = new ArrayList<Notification>();
//        notifications = logic.MockGetNotifications();
        hf = this;

        String databaseName = "api/Notifications/" + Global.studentID;
        String url = "http://coms-309-mc-02.cs.iastate.edu:5000/" + databaseName;
        requestQueue = Volley.newRequestQueue(hf.getContext());
        requestQueue.start();

        Map<String,String> header = new HashMap<String,String>();
        header.put("Authorization","Bearer " + Global.bearerToken);

        JsonArrayRequest getRequest = new JsonArrayRequest(Request.Method.GET, url, null, new Response.Listener<JSONArray>()
        {
            @Override
            public void onResponse(JSONArray response) {
                Toast.makeText(hf.getContext(), "yay stuffs" + response, Toast.LENGTH_SHORT).show();

                if(response.length()==0)return;

                Map<String, Object> theMap = new HashMap<String, Object>();

                for(int i = 0; i < response.length();i++){
                    try {
                        theMap = toMap(response.getJSONObject(i));
                    } catch (JSONException e) {
                        e.printStackTrace();
                    }
                    Notification notification = new Notification(theMap.get("data").toString(), hf.getContext(),Integer.parseInt(theMap.get("studentID").toString()), theMap.get("description").toString(), theMap.get("type").toString());
                    notifications.add(notification);
//                    setSenderName(notification);
                }

                // mocked data
//                notifications.add(new Notification(hf.getContext(),81537,"Hi Hiiiii. Friend meeee","Request"));
//                notifications.add(new Notification(hf.getContext(),81537,"Update soon!","Announce"));
//                notifications.add(new Notification(hf.getContext(),81537,"Nah. Friend me","Request"));

                homeAdapter = new HomeAdapter(hf, hf.getContext(), notifications);
                resetAdapter(homeAdapter);
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

    public void resetAdapter(HomeAdapter h){
        listview.setAdapter(h);
    }

    public void setListView(ListView aListView){
        listview = aListView;
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

    public void setSenderName(Notification notification){
        String databaseName = "api/Students/" + notification.getSenderID();
        String url = "http://coms-309-mc-02.cs.iastate.edu:5000/" + databaseName;
        RequestQueue requestQueue;
        requestQueue = Volley.newRequestQueue(hf.getContext());
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
                notification.setName(theMap.get("username").toString());
                notifications.add(notification);
                homeAdapter = new HomeAdapter(hf, hf.getContext(), notifications);
                resetAdapter(homeAdapter);

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