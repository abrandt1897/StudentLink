package com.example.studentlink.ui.home;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.example.studentlink.R;

import java.util.List;

class someAdapter extends BaseAdapter {

    Context context;
    private List<Notification> notifications;
    private static LayoutInflater inflater = null;

    public someAdapter(Context context, List<Notification> data) {

        this.context = context;
        this.notifications = data;
        inflater = (LayoutInflater) context
                .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
    }

    @Override
    public int getCount() {

        return notifications.size();
    }

    @Override
    public Object getItem(int position) {

        return notifications.get(position);
    }

    @Override
    public long getItemId(int position) {

        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View vi = convertView;
        TextView text = null;
        if (vi == null)
            if(notifications.get(position).getType().equals("Request")){
                vi = inflater.inflate(R.layout.item_request,null);
                text = (TextView) vi.findViewById(R.id.requestText);
            }
        else{
                vi = inflater.inflate(R.layout.item_announcement, null);
                text = (TextView) vi.findViewById(R.id.announceText);
            }

        text.setText(notifications.get(position).getDescription());
        return vi;
    }
}
