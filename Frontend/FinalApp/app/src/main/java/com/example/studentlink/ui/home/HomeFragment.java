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

import java.util.List;

public class HomeFragment extends Fragment {

    private List<Notification> notifications;
    ListView listview;

    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.home_layout, container, false);

        listview = root.findViewById(R.id.listview);
        listview.setAdapter(new someAdapter(this.getContext(), new String[] { "data1",
                "data2" }));

//        RecyclerView rv = root.findViewById(R.id.rv);
//        rv.setHasFixedSize(true);

//        LinearLayoutManager llm = new LinearLayoutManager(this.getContext());
//        rv.setLayoutManager(llm);



        // connect to this web socket for notifications
        // ws://http://coms-309-mc-02.cs.iastate.edu:5000/ws/{StudentID}



        return root;
    }
}