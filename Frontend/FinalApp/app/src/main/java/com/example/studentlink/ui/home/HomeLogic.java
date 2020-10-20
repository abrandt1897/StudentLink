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

    public List<Notification> MockgetNotifications(){

        notifications = new ArrayList<Notification>();
        notifications.add(new Notification("Hi Hiiiii. Friend meeee","Request"));
        for (int i = 0; i<7;i++){
            notifications.add(new Notification("Update soon!","Announce"));
        }

        return notifications;
    }

    public List<Notification> getNotifications(){
        // connect to websocket
        // ws://http://coms-309-mc-02.cs.iastate.edu:5000/ws/{StudentID}

        notifications = new ArrayList<Notification>();

        // parse data from Greyson

        return notifications;
    }



}
