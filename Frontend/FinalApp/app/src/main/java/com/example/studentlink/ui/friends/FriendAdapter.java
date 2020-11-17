package com.example.studentlink.ui.friends;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.example.studentlink.Global;
import com.example.studentlink.PageController;
import com.example.studentlink.R;
import com.example.studentlink.ui.home.HomeFragment;
import com.example.studentlink.ui.home.Notification;
import com.example.studentlink.ui.search.SearchFragment;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class FriendAdapter extends BaseAdapter {

    private static LayoutInflater inflater = null;
    private FriendsFragment frag;
    private ArrayList<Friend> friends;

    public FriendAdapter(FriendsFragment ff, ArrayList<Friend>  friends) {
        frag = ff;
        this.friends = friends;
        inflater = (LayoutInflater) ff.getContext().getSystemService(Context.LAYOUT_INFLATER_SERVICE);
    }

    @Override
    public int getCount() {
        return friends.size();
    }

    @Override
    public Object getItem(int position) {
        return friends.get(position);
    }

    @Override
    public long getItemId(int position) {
        return 0;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        View vi = convertView;
        if (vi == null) {
            vi = inflater.inflate(R.layout.item_friend, null);
            TextView text = (TextView) vi.findViewById(R.id.friendText);
            text.setText("Name: "+ friends.get(position).getName() + "\nUsername: " + friends.get(position).getUsername());

        }

        return vi;
    }

}
