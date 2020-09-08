package com.example.studentlink.ui.chat;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.fragment.app.Fragment;

import androidx.annotation.NonNull;

import com.example.studentlink.R;

public class ChatPageFragment extends Fragment {

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.chatpage_layout, container, false);

        TextView LoginText = root.findViewById(R.id.chatText);

        LoginText.setText("Chat Page Placeholder");

        return root;
    }


}
