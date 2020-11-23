package com.example.studentlink.ui.chat;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
//import android.support.v7.app.AppCompatActivity;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;

import com.example.studentlink.ConnectionClass;
import com.example.studentlink.Global;
import com.example.studentlink.R;
import com.example.studentlink.api.IApiCommands;
import com.example.studentlink.api.MockCommands;
import com.example.studentlink.ui.profile.ProfileFragment;

import java.util.ArrayList;

public class ChatListFragment extends Fragment implements IApiCommands {

    ArrayList<String> serverData;
    ConnectionClass connection;
    Thread updateThread;
    ListView list = null;
    ArrayAdapter<String> arrayAdapter;

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        View root = inflater.inflate(R.layout.chatlist_layout, container, false);
        list = root.findViewById(R.id.list);
        arrayAdapter = new ArrayAdapter<String>(this.getContext(), android.R.layout.simple_list_item_1, getListOfChats());
        list.setAdapter(arrayAdapter);

        connection = new ConnectionClass(getContext());

        // if(Global.chatIDs.isEmpty()){
        updateThread = new getServerData();
        updateThread.start();
        connection.getArrayRequest("api/Chats/Group/" + Global.studentID); //+ Global.studentID);
        // }

        list.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                String clickedItem = (String) list.getItemAtPosition(position);
                Global.selectedChatID = Global.chatIDs.get(position);
                Toast.makeText(ChatListFragment.this.getContext(), clickedItem, Toast.LENGTH_LONG).show();
                openChatPage();
            }
        });


        return root;
    }


    public void openChatPage(){
        Intent intent = new Intent(getActivity(), ChatPageActivity.class);
        startActivity(intent);
    }

    @Override
    public ArrayList<String> getListOfChats() {
        ArrayList<String> chatNames = new ArrayList<String>();
        for(String s : Global.chatIDs){
            chatNames.add("Chat " + s);
        }
        return chatNames;
    }

    //update everything
    class getServerData extends Thread{
        @Override
        public void run() {
            while (true) {
                serverData = connection.getResponseArray();
                if(!serverData.isEmpty()){
                    if(Global.chatIDs.isEmpty()){
                        Log.i("chatIDs", serverData.toString());
                        for (String s : serverData){
                            Global.chatIDs.add(s);
                        }
                        break;
                    }
                }
            }
            connection.runOnUiThread(new Runnable() {
                @Override
                public void run() {
                    if(!Global.chatIDs.isEmpty()) {
                        arrayAdapter = new ArrayAdapter<String>(getContext(), android.R.layout.simple_list_item_1, getListOfChats());
                        list.setAdapter(arrayAdapter);
                    }
                }
            });
        }

    }
}