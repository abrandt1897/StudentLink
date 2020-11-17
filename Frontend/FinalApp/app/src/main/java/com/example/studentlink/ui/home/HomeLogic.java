package com.example.studentlink.ui.home;

import android.content.Context;

import com.example.studentlink.ConnectionClass;
import com.example.studentlink.Global;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.StringReader;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

public class HomeLogic {
    private List<Notification> notifications;
    private Context context;
    private ConnectionClass connectionClass;

    public HomeLogic(Context c)
    {
        context = c;
        connectionClass = new ConnectionClass(c);
    }

    public List<Notification> MockGetNotifications(){
        notifications = new ArrayList<Notification>();
        notifications.add(new Notification("",context,1, "Hi Hiiiii. Friend meeee","Request"));
        for (int i = 0; i<7;i++){
            if(i==3){
                notifications.add(new Notification("",context, 1, "Nah. Friend me","Request"));
            }
            notifications.add(new Notification("",context,1,"Update soon!","Announce"));
        }
        return notifications;
    }

    public List<Notification> getNotifications(int studentID) throws JSONException {
        // TODO: Connect to websocket    ws://http://coms-309-mc-02.cs.iastate.edu:5000/ws/{StudentID}
        // remember to send the static bearer token in the header
        String databaseName = "api/Notifications/" + Global.studentID;
        connectionClass.getRequest(databaseName);

        notifications = new ArrayList<Notification>();
        JSONObject jo = connectionClass.getObjectOfServerResponse();
        Map<String,Object> studentInfo = toMap(jo);


        //TODO: Parse data (list of hashmaps)

        jo.get("Data");
        jo.get("Type");

        // parse data from Greyson - JSONParser

        return notifications;
    }

    public static Map<String, Object> toMap(JSONObject jsonobj)  throws JSONException {
        Map<String, Object> map = new HashMap<String, Object>();
        Iterator<String> keys = jsonobj.keys();
        while(keys.hasNext()) {
            String key = keys.next();
            Object value = jsonobj.get(key);
            if (value instanceof JSONArray) {
                value = toList((JSONArray) value);
            } else if (value instanceof JSONObject) {
                value = toMap((JSONObject) value);
            }
            map.put(key, value);
        }   return map;
    }

    public static List<Object> toList(JSONArray array) throws JSONException {
        List<Object> list = new ArrayList<Object>();
        for(int i = 0; i < array.length(); i++) {
            Object value = array.get(i);
            if (value instanceof JSONArray) {
                value = toList((JSONArray) value);
            }
            else if (value instanceof JSONObject) {
                value = toMap((JSONObject) value);
            }
            list.add(value);
        }   return list;
    }

}
