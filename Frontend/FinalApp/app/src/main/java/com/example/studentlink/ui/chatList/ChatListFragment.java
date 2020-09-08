package com.example.studentlink.ui.chatList;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
//import android.support.v7.app.AppCompatActivity;
import android.view.MotionEvent;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.Toast;
import java.util.ArrayList;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.Observer;
import androidx.lifecycle.ViewModelProviders;

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
            }
        });

        return root;
    }
}