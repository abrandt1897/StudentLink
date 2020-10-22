package com.example.studentlink.ui.home;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.example.studentlink.R;

import java.util.List;

class HomeAdapter extends BaseAdapter {

    Context context;
    private List<Notification> notifications;
    private static LayoutInflater inflater = null;
    private HomeFragment frag;

    public HomeAdapter(HomeFragment hf, Context context, List<Notification> data) {
        frag = hf;
        this.context = context;
        this.notifications = data;
        inflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
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


    public void removeRow(int position) {
        notifications.remove(position);
        frag.resetAdapter(this);
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View vi = convertView;
        if (vi == null) {
            if (notifications.get(position).getType().equals("Request")) {
                vi = inflater.inflate(R.layout.item_request, null);
                TextView text = (TextView) vi.findViewById(R.id.requestText);
                text.setText(notifications.get(position).getDescription());

                Button acceptButton = (Button) vi.findViewById(R.id.AcceptButton);
                acceptButton.setTag(position);
                acceptButton.setOnClickListener(v -> {
                    removeRow(position);
                });

                Button declineButton = (Button) vi.findViewById(R.id.DeclineButton);
                declineButton.setTag(position);
                declineButton.setOnClickListener(v -> {
                    Toast.makeText(context, "Congrats!" + v.getTag().toString(), Toast.LENGTH_LONG).show();
//                    int pos = (int)v.getTag();
//                    notifications.remove(pos);
//                    HomeAdapter.this.notifyDataSetChanged();
                });

            } else {
                vi = inflater.inflate(R.layout.item_announcement, null);
                TextView text = (TextView) vi.findViewById(R.id.announceText);
                text.setText(notifications.get(position).getDescription());
            }

        }

        return vi;
    }

}
