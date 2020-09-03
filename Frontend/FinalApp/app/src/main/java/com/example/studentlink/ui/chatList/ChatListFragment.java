package com.example.studentlink.ui.chatList;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;

import com.example.studentlink.R;

public class ChatListFragment extends Fragment {
    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.chatlist_layout, container, false);

        TextView ChatListText = root.findViewById(R.id.ChatListText);

        ChatListText.setText("ChatList Page Stuff");

        return root;
    }
}
