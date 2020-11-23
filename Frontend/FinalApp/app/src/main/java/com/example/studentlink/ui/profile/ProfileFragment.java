package com.example.studentlink.ui.profile;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.cardview.widget.CardView;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.studentlink.ConnectionClass;
import com.example.studentlink.Global;
import com.example.studentlink.R;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;

public class ProfileFragment extends Fragment {
    String serverData = "";
    ConnectionClass connection;
    TextView ProfileText;
    TextView studentIdentificationText;
    TextView studentClassesText;
    Thread updateThread;
    CardView cardView;
    private boolean threadRunning = false;
    RecyclerView RecyclerView;
    RecyclerView.Adapter recycleAdapter;
    ArrayList<classObject> classObjects = new ArrayList<classObject>();
    RecyclerView.LayoutManager recycleLayoutMan;


    private static final String seasons[] = {
            "Spring", "Spring", "Spring", "Spring", "Spring", "Summer",
            "Summer", "Fall", "Fall", "Fall", "Fall", "Fall"
    };

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.profile_layout, container, false);
        connection = new ConnectionClass(getContext());

       // ProfileText = root.findViewById(R.id.ProfileText);
        studentIdentificationText = root.findViewById(R.id.studentIdentityText);
        //studentClassesText = root.findViewById(R.id.studentClassesText);
        cardView = root.findViewById(R.id.classCardView);


        RecyclerView = root.findViewById(R.id.recyclerView);
        RecyclerView.setHasFixedSize(true);
        recycleLayoutMan = new LinearLayoutManager(getContext());

        if(Global.studentIdendity == null && Global.Classes == null){
            updateThread = new getServerData();
            updateThread.start();
            connection.getRequest("api/Students/Username/" + Global.username);
            connection.getArrayRequest1("api/Courses/" + Global.studentID);
        }
        else{
            studentIdentificationText.setText(Global.studentIdendity);
            //studentClassesText.setText(Global.Classes);

            for(String s: Global.ClassesArray){
                classObjects.add(new classObject(s));
                //classObjects.add(new classObject("yeet"));
            }
            recycleAdapter = new RecycleAdapter(classObjects);
            RecyclerView.setLayoutManager(recycleLayoutMan);
            RecyclerView.setAdapter(recycleAdapter);
//            classObjects.add(new classObject("yeet"));

        }

        return root;
    }

    //update everything
    class getServerData extends Thread{
        @Override
        public void run() {
            String currentResponse = serverData;
            while (true) {
                serverData = connection.getResponse();
                if(!serverData.equals("")){
                    if(Global.studentIdendity == null)
                        Global.studentIdendity = serverData;
                    else if(serverData != Global.studentIdendity){
                        Global.Classes = serverData;
                        break;
                    }
                }
            }
            connection.runOnUiThread(new Runnable() {
                @Override
                public void run() {
                    String[] name = Global.studentIdendity.split(",");
                    for (String s : name){
                        if(s.contains("fullName") ){
                            Global.studentIdendity = s.substring(12, s.length()-1);
                        }
                    }
                    studentIdentificationText.setText(Global.studentIdendity);

                    String[] totalClasses = Global.Classes.split(",");
                    for (String s : totalClasses) {
                        if (s.contains("name") && s.contains(getSeason() + " " + Calendar.getInstance().get(Calendar.YEAR))) {
                            Global.ClassesArray.add(s.substring(8, s.length() - 1));
                        }
                    }


                    for(String s: Global.ClassesArray){
                        classObjects.add(new classObject(s));
                        //classObjects.add(new classObject("yeet"));
                    }
                    recycleAdapter = new RecycleAdapter(classObjects);
                    RecyclerView.setLayoutManager(recycleLayoutMan);
                    RecyclerView.setAdapter(recycleAdapter);
                }
            });
        }
    }

    public String getSeason() {
        return seasons[ Calendar.getInstance().get(Calendar.MONTH) ];
    }
}

