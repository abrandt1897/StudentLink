package com.example.studentlink.ui.profile;

import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;

import com.example.studentlink.ConnectionClass;
import com.example.studentlink.R;

public class ProfileFragment  extends Fragment {
    String serverData = "";
    ConnectionClass connection;
    TextView requestData;

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.profile_layout, container, false);
        connection = new ConnectionClass(getContext());
        new Thread(new getServerData()).start();

        TextView ProfileText = root.findViewById(R.id.ProfileText);
        requestData = root.findViewById(R.id.requestData);
        Button RequestButton = root.findViewById(R.id.RequestButton);

        ProfileText.setText("Profile Page Stuff");

        RequestButton.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                connection.getRequest("Test/6969420");
                //requestData.setText(connection.getResponse());
            }
        });
        return root;


    }

    //update everything
    class getServerData implements Runnable {
        @Override
        public void run() {
            String currentResponse = serverData;
            while (currentResponse.equals(serverData)) {
                serverData = connection.getResponse();
                if(serverData != ""){
                    updateScreen(serverData);
                }
            }
        }
        public void updateScreen(String string){
            connection.runOnUiThread(new Runnable() {
                @Override
                public void run() {
                    requestData.setText(string);
                }
            });
        }
    }

}

