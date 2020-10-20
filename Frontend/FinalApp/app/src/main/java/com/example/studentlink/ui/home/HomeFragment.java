package com.example.studentlink.ui.home;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.studentlink.R;

import java.util.ArrayList;
import java.util.List;

public class HomeFragment extends Fragment {

    private List<Notification> notifications;
    private ListView listview;
    private HomeLogic logic;

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.home_layout, container, false);

        listview = root.findViewById(R.id.listview);
        logic = new HomeLogic(this.getContext());
        notifications = logic.MockgetNotifications();

        listview.setAdapter(new someAdapter(this.getContext(), notifications));

        return root;
    }
}