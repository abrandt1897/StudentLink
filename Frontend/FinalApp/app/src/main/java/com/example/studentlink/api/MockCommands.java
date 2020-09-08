package com.example.studentlink.api;

import java.util.ArrayList;

public class MockCommands implements IApiCommands {


    public ArrayList<String> getListOfChats() {
        ArrayList<String> chats = new ArrayList<>();
        chats.add("Chat 1");
        chats.add("Chat 2");
        chats.add("Chat 3");
        chats.add("Chat 4");
        chats.add("Chat 5");
        chats.add("Chat 6");

        return chats;
    }
}
