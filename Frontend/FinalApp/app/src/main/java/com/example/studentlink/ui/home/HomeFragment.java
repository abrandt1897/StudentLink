package com.example.studentlink.ui.home;

import android.content.Context;
import android.graphics.Color;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.example.studentlink.Global;
import com.example.studentlink.R;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class HomeFragment extends Fragment {

    private List<Notification> notifications;
    private ListView listview;
    private HomeLogic logic;
    private HomeAdapter homeAdapter;
    private Context c;

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.home_layout, container, false);
        listview = root.findViewById(R.id.listview);
        logic = new HomeLogic(this.getContext());
//        notifications = logic.MockGetNotifications();
        c = this.getContext();


        //here

        String databaseName = "api/Notifications/" + Global.studentID;
        String url = "http://coms-309-mc-02.cs.iastate.edu:5000/" + databaseName;
        RequestQueue requestQueue;
        //start a queue for requests for our api
        requestQueue = Volley.newRequestQueue(c);
        requestQueue.start();

        Map<String,String> header = new HashMap<String,String>();
        header.put("Authorization","Bearer " + Global.bearerToken);

        StringRequest getRequest = new StringRequest(Request.Method.GET, url, new Response.Listener<String>()
        {
            @Override
            public void onResponse(String response) {
                Toast.makeText(c, "yay stuffs" + response, Toast.LENGTH_SHORT).show();
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


        //here

//        homeAdapter = new HomeAdapter(this, this.getContext(), notifications);
//        resetAdapter(homeAdapter);

        return root;
    }

    public void resetAdapter(HomeAdapter h){
        listview.setAdapter(h);
    }

    public void setListView(ListView aListView){
        listview = aListView;
    }


}