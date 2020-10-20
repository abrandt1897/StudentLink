package com.example.studentlink.ui.home;

public class Notification {

    private String description;
    private String type;

    public Notification(String d, String t){
        description = d;
        type = t;
    }

    public String getType(){
        return type;
    }

    public String getDescription(){
        return description;
    }

}
