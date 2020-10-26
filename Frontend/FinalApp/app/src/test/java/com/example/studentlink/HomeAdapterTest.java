package com.example.studentlink;

import org.junit.Test;

import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ListView;

import androidx.appcompat.app.AppCompatActivity;

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

    @Test
    public void testRemoveRow(){
        List<Notification> notifications = new ArrayList<Notification>();
        notifications.add(new Notification("Hi Hiiiii. Friend meeee","Request"));
        notifications.add(new Notification("Nah friend me!","Request"));
        notifications.add(new Notification("Update soon!","Announce"));

        HomeAdapter ha = new HomeAdapter(hf,c,notifications);
        ha.removeRow(0);

        String nextDescription = ((Notification)ha.getItem(0)).getDescription();
        Assert.assertEquals("Nah friend me!", nextDescription);
        Assert.assertEquals(2, ha.getCount());
    }
}
