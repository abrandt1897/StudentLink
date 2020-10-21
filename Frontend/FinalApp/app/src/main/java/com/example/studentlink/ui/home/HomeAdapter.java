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

    public HomeAdapter(Context context, List<Notification> data) {

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
        if (vi == null)
            if(notifications.get(position).getType().equals("Request")){
                vi = inflater.inflate(R.layout.item_request,null);
                TextView text = (TextView) vi.findViewById(R.id.requestText);
                text.setText(notifications.get(position).getDescription());
                Button acceptButton = (Button) vi.findViewById(R.id.AcceptButton);
                acceptButton.setTag(position);
                acceptButton.setOnClickListener(v -> {
                    Toast.makeText(context,"Congrats!" + v.getTag().toString(), Toast.LENGTH_LONG).show();
//                    int pos = (int)v.getTag();
//                    notifications.remove(pos);
//                    HomeAdapter.this.notifyDataSetChanged();
                });
            }
        else{
                vi = inflater.inflate(R.layout.item_announcement, null);
                TextView text = (TextView) vi.findViewById(R.id.announceText);
                text.setText(notifications.get(position).getDescription());


            }




        return vi;
    }
}

//    public void AcceptClickHandler(View v)
//    {
//        //get the row the clicked button is in
//        LinearLayout vwParentRow = (LinearLayout) v.getParent();
//
//        TextView child = (TextView)vwParentRow.getChildAt(0);
//        Button btnChild = (Button)vwParentRow.getChildAt(1);
//        btnChild.setText(child.getText());
//        btnChild.setText("I've been clicked!");
//
//        int c = Color.CYAN;
//        vwParentRow.setBackgroundColor(c);
//        vwParentRow.refreshDrawableState();
//    }