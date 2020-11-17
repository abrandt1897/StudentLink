package com.example.studentlink.ui.friends;

public class Friend {
    private int ID;
    private String name;
    private String username;
    private int percentClass;

    public Friend(int ID){
        this.ID = ID;
    }

    public Friend(String name, String username){
        this.name = name;
        this.username = username;
//        this.percentClass =
    }

    public int getID(){
        return ID;
    }

    public void setID(int ID){
        this.ID=ID;
    }

    public void setName(String name){
        this.name = name;

    }

    public void setUsername(String username){
        this.username= username;
    }

    public String getName(){
        return name;
    }
    public String getUsername(){
        return username;
    }

}
