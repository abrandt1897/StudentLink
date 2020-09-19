package com.example.studentlink.ui.profile;

import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;
import com.example.studentlink.ConnectionClass;
import com.example.studentlink.R;

import org.json.JSONObject;

public class ProfileFragment  extends Fragment {
    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.profile_layout, container, false);

        ConnectionClass connection = new ConnectionClass();

        TextView ProfileText = root.findViewById(R.id.ProfileText);
        TextView requestData = root.findViewById(R.id.requestData);
        Button RequestButton = root.findViewById(R.id.RequestButton);

        ProfileText.setText("Profile Page Stuff");

        RequestButton.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                connection.getRequest("name_test");

                requestData.setText(connection.getResponse());


            }
        });
        return root;


    }
}
