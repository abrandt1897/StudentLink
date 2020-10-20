package com.example.studentlink.ui.home;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;

import com.example.studentlink.R;

import java.util.List;

public class HomeFragment extends Fragment {

    private List<Notification> notifications;
    private ListView listview;
    private HomeLogic logic;

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.home_layout, container, false);

        listview = root.findViewById(R.id.listview);
        logic = new HomeLogic(this.getContext());
        notifications = logic.MockGetNotifications();

        listview.setAdapter(new HomeAdapter(this.getContext(), notifications));

        return root;
    }
}