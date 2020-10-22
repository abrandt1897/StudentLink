package com.example.studentlink.ui.home;

import android.content.Context;

import java.util.ArrayList;
import java.util.List;

public class HomeLogic {
    private List<Notification> notifications;
    private Context context;


    public HomeLogic(Context c){
        context = c;
    }

    public List<Notification> MockGetNotifications(){

        notifications = new ArrayList<Notification>();
        notifications.add(new Notification("Hi Hiiiii. Friend meeee","Request"));
        for (int i = 0; i<7;i++){
            if(i==3){
                notifications.add(new Notification("Nah. Friend me","Request"));
            }
            notifications.add(new Notification("Update soon!","Announce"));
        }

        return notifications;
    }

    public List<Notification> getNotifications(int studentID){
        // connect to websocket
        // ws://http://coms-309-mc-02.cs.iastate.edu:5000/ws/{StudentID}

        notifications = new ArrayList<Notification>();

        // parse data from Greyson JSONParser

        return notifications;
    }



}
