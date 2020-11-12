package com.example.studentlink.ui.home;

public class Notification {

    private String description;
    private String type;
    private int senderID;
//    private String data;

    public Notification(int s, String d, String t){
        senderID = s;
        description = d;
        type = t;
    }

    public String getType(){
        return type;
    }

    public String getSender(){
        // do getRequest to get student sender name
        return null;
    }

    public int getSenderID(){
        return senderID;

    }



    public String getDescription(){
        return description;
    }

}
