package com.example.studentlink.ui.search;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.TextView;

import com.android.volley.RequestQueue;
import com.android.volley.toolbox.Volley;
import com.example.studentlink.R;
import com.example.studentlink.ui.home.HomeFragment;
import com.example.studentlink.ui.home.Notification;

import java.util.List;
import java.util.Map;

public class SearchAdapter extends BaseAdapter {

    private static LayoutInflater inflater = null;
    private SearchFragment frag;
    private Map<String, Object> userMap;

    public SearchAdapter(Map<String, Object> foundName, SearchFragment sf){
        userMap = foundName;
        frag = sf;
        inflater = (LayoutInflater) sf.getContext().getSystemService(Context.LAYOUT_INFLATER_SERVICE);
    }


    @Override
    public int getCount() {
        return 1;
    }

    @Override
    public Object getItem(int i) {
        return null;
    }

    @Override
    public long getItemId(int i) {
        return 0;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
//dkooker
        View vi = convertView;
        if (vi == null) {
                vi = inflater.inflate(R.layout.item_searched_name, null);
                TextView text = (TextView) vi.findViewById(R.id.textUserRequest);
                text.setText("Username: " + userMap.get("username"));

                Button bAccept = (Button) vi.findViewById(R.id.bAccept);
                bAccept.setTag(position);


                bAccept.setOnClickListener(v -> {
                    // todo: do a put request to let know that request was sent
                });

        }

        return vi;

    }
}
