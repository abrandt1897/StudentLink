package com.example.studentlink;

import org.junit.Test;

import android.content.Context;

import androidx.appcompat.app.AppCompatActivity;

import com.android.volley.RequestQueue;
import com.android.volley.toolbox.Volley;
import com.example.studentlink.ui.home.HomeAdapter;
import com.example.studentlink.ui.home.HomeFragment;
import com.example.studentlink.ui.home.Notification;
import com.example.studentlink.ui.login.NewAccount.Logic.CreateAccountLogic;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.runners.MockitoJUnitRunner;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import static org.mockito.Mockito.doNothing;
import static org.mockito.Mockito.doReturn;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.times;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.when;

import static org.junit.Assert.*;

@RunWith(MockitoJUnitRunner.class)
public class HomeAdapterTest {

    @Mock
    private Context c;

    @Mock
    private HomeFragment hf;

    @Mock
    RequestQueue requestQueue;

//    @Test
//    public void testRemoveRow(){
//        List<Notification> notifications = new ArrayList<Notification>();
//        notifications.add(new Notification(1, "Hi Hiiiii. Friend meeee","Request"));
//        notifications.add(new Notification(1,"Nah friend me!","Request"));
//        notifications.add(new Notification(1,"Update soon!","Announce"));
//
//        HomeAdapter ha = new HomeAdapter(hf,c,notifications);
//        ha.removeRow(0, true, requestQueue);
//
//        String nextDescription = ((Notification)ha.getItem(0)).getDescription();
//        Assert.assertEquals("Nah friend me!", nextDescription);
//        Assert.assertEquals(2, ha.getCount());
//    }


    // 3 new tests for demo 4 below



    @Test
    public void testCheckPutRequest(){
        List<Notification> notifications = new ArrayList<Notification>();
        notifications.add(new Notification(1,"Hi Hiiiii. Friend meeee","Request"));
        notifications.add(new Notification(1, "Nah friend me!","Request"));
        notifications.add(new Notification(1, "Update soon!","Announce"));

        HomeAdapter ha = new HomeAdapter(hf,c,notifications);
        ha.removeRow(0, true, requestQueue);

        verify(requestQueue, times(1)).start();
        verify(requestQueue, times(1)).add(Mockito.any());
    }

    @Test
    public void testgetCount(){
        List<Notification> notifications = new ArrayList<Notification>();
        notifications.add(new Notification(1,"Hi Hiiiii. Friend meeee","Request"));
        notifications.add(new Notification(1, "Nah friend me!","Request"));
        notifications.add(new Notification(1, "Update soon!","Announce"));
        HomeAdapter ha = new HomeAdapter(hf,c,notifications);

        Assert.assertEquals(3, ha.getCount());
    }


    @Test
    public void testgetItem(){
        List<Notification> notifications = new ArrayList<Notification>();
        notifications.add(new Notification(1,"Hi Hiiiii. Friend meeee","Request"));
        notifications.add(new Notification(1, "Nah friend me!","Request"));
        notifications.add(new Notification(1, "Update soon!","Announce"));
        HomeAdapter ha = new HomeAdapter(hf,c,notifications);

        Notification expected = new Notification(1,"Hi Hiiiii. Friend meeee","Request");
        Notification actual = (Notification)ha.getItem(0);

        Assert.assertEquals(expected.getSenderID(), actual.getSenderID());
        Assert.assertEquals(expected.getDescription(), actual.getDescription());
        Assert.assertEquals(expected.getType(), actual.getType());
    }



}
