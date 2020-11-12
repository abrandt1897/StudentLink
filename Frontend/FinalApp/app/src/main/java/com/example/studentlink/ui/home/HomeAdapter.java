package com.example.studentlink.ui.home;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.TextView;
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
import com.example.studentlink.R;

import org.json.JSONObject;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class HomeAdapter extends BaseAdapter {

    Context context;
    private List<Notification> notifications;
    private static LayoutInflater inflater = null;
    private HomeFragment frag;

    public HomeAdapter(HomeFragment hf, Context context, List<Notification> data) {
        frag = hf;
        this.context = context;
        this.notifications = data;
        inflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);

    }

    @Override
    public int getCount() {
        return notifications.size();
    }

    @Override
    public Object getItem(int position) {
        return notifications.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }


    public void removeRow(int position, boolean accepted, RequestQueue requestQueue) {
        // Use Put Request to inform the backend of the decision
        String databaseName = "api/Students/" + Global.studentID;
        String url = "http://coms-309-mc-02.cs.iastate.edu:5000/" + databaseName;
        Map<String,String> notificationData = new HashMap<String,String>();
        notificationData.put("SenderID", notifications.get(position).getSenderID() + "");
        notificationData.put("data", accepted + "");

        HomeAdapter ha = this;

        Map<String,String> header = new HashMap<String,String>();
        header.put("Authorization","Bearer " + Global.bearerToken);

//        RequestQueue requestQueue;
//        requestQueue = Volley.newRequestQueue(context);
        requestQueue.start();

        StringRequest putRequest = new StringRequest(Request.Method.PUT, url,
//                listenerResponse
                new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                notifications.remove(position);
                frag.resetAdapter(ha);

                if(accepted){
                    Toast.makeText(context, "Request Accepted   " + response, Toast.LENGTH_SHORT).show();
                }
                else{
                    Toast.makeText(context, "Request Declined   " + response, Toast.LENGTH_SHORT).show();
                }
            }
        }
        ,
                new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                error.printStackTrace();
            }
        }
        ) {
            @Override
            protected Map<String, String> getParams()
            {
                return notificationData;
            }
            @Override
            public Map<String, String> getHeaders() throws AuthFailureError {
                //headers.put("Content-Type", "application/json");
                return header;
            }
        }
        ;

//        Volley.newRequestQueue(frag.getContext()).add(putRequest);
        requestQueue.add(putRequest);

    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View vi = convertView;
        if (vi == null) {
            if (notifications.get(position).getType().equals("Request")) {
                vi = inflater.inflate(R.layout.item_request, null);
                TextView text = (TextView) vi.findViewById(R.id.requestText);
                text.setText(notifications.get(position).getDescription());

                Button acceptButton = (Button) vi.findViewById(R.id.AcceptButton);
                acceptButton.setTag(position);
                acceptButton.setOnClickListener(v -> {
                    RequestQueue requestQueue = Volley.newRequestQueue(context);
                    removeRow(position, true, requestQueue);

                });

                Button declineButton = (Button) vi.findViewById(R.id.DeclineButton);
                declineButton.setTag(position);
                declineButton.setOnClickListener(v -> {
                    RequestQueue requestQueue = Volley.newRequestQueue(context);
                    removeRow(position, false, requestQueue);
                });

            } else {
                vi = inflater.inflate(R.layout.item_announcement, null);
                TextView text = (TextView) vi.findViewById(R.id.announceText);
                text.setText(notifications.get(position).getDescription());
            }

        }

        return vi;
    }

}
