package com.example.studentlink.ui.chat;

import android.content.Intent;
import android.os.Bundle;
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

import com.example.studentlink.R;
import com.example.studentlink.api.IApiCommands;
import com.example.studentlink.api.MockCommands;

public class ChatListFragment extends Fragment {

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        View root = inflater.inflate(R.layout.chatlist_layout, container, false);
        final ListView list = root.findViewById(R.id.list);
        IApiCommands listOfChats = new MockCommands();
        ArrayAdapter<String> arrayAdapter = new ArrayAdapter<String>(this.getContext(), android.R.layout.simple_list_item_1, listOfChats.getListOfChats());
        list.setAdapter(arrayAdapter);
        list.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                String clickedItem=(String) list.getItemAtPosition(position);
                Toast.makeText(ChatListFragment.this.getContext(),clickedItem,Toast.LENGTH_LONG).show();
                //openChatPage();
            }
        });

        return root;
    }


//    public void openChatPage(){
//        Intent intent = new Intent(this.getContext(), ChatPageFragment.class);
//        startActivity(intent);
//    }
}