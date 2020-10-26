package com.example.studentlink.ui.home;

import android.content.Context;
import android.graphics.Color;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;

import com.example.studentlink.R;

import java.util.List;

public class HomeFragment extends Fragment {

    private List<Notification> notifications;
    private ListView listview;
    private HomeLogic logic;
    private HomeAdapter homeAdapter;

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.home_layout, container, false);
        listview = root.findViewById(R.id.listview);
        logic = new HomeLogic(this.getContext());
        notifications = logic.MockGetNotifications();
        homeAdapter = new HomeAdapter(this, this.getContext(), notifications);
        resetAdapter(homeAdapter);

        return root;
    }

    public void resetAdapter(HomeAdapter h){
        listview.setAdapter(h);
    }

    public void setListView(ListView aListView){
        listview = aListView;
    }


}