package com.example.studentlink;

import org.junit.Test;

import android.content.Context;
import android.widget.EditText;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatActivity;

import com.example.studentlink.ui.login.NewAccount.CreateAccount;
import com.example.studentlink.ui.login.NewAccount.Logic.CreateAccountLogic;
import com.example.studentlink.ui.login.SignIn.LoginActivity;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.runners.MockitoJUnitRunner;

import java.util.HashMap;
import java.util.Map;

import static org.mockito.Mockito.doNothing;
import static org.mockito.Mockito.doReturn;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.times;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.when;

import static org.junit.Assert.*;

@RunWith(MockitoJUnitRunner.class)
public class LoginTest {

    @Mock
    private ConnectionClass connectionClass;

    @Mock
    private Context context;

    @Mock
    private AppCompatActivity theActivity;

    @Mock
    private HashMap<String,String> data;

    @Test
    public void testNavigateToWebView(){
        CreateAccountLogic createAccountLogic = new CreateAccountLogic(theActivity);
        createAccountLogic.setConnectionClass(connectionClass); //inject mock
//        CreateAccountLogic spy = Mockito.spy(createAccountLogic);
//        Mockito.doNothing().when(spy).navigateWebView();
        when(connectionClass.getResponse()).thenReturn("No");
//        doNothing().when(createAccountLogic,"navigateWebView").thenReturn();

        String response = createAccountLogic.checkCredentials("badUsername","badPassword");

        Assert.assertEquals(response, "");

    }

    @Test
    public void testForCheckServerResponse() {
//        LoginActivity loginActivity = new LoginActivity();
//        loginActivity.setConnectionClass(connectionClass); // injecting the mock
//
//        when(connectionClass.getResponse()).thenReturn("Ok");
//
//        loginActivity.checkServerResponse(); // testing the method
//
//        verify(connectionClass, times(1)).getResponse();
    }

    @Test
    public void test() {
//        LoginActivity loginActivity = new LoginActivity();
//        loginActivity.setConnectionClass(connectionClass);
//
//        when(connectionClass.getResponse()).thenReturn("Ok"); //
//
//        boolean response = loginActivity.checkServerResponse();
//
//        Assert.assertEquals(response, true);
    }

    @Test
    public void testForMakeTheConnection() {
//        LoginActivity loginActivity = new LoginActivity();
//        loginActivity.setConnectionClass(connectionClass); // injecting the mock
//
//        Map<String, String> map = new HashMap<>();
//        String databaseName = "";
//
//        when(username.getText().toString()).thenReturn("hello"); //
//        when(password.getText().toString()).thenReturn("pass");
//
//        when(loginActivity.makeTheConnection(username, password, ErrorText));
//
//        verify(connectionClass, times(1)).putRequest(map, databaseName);
    }

}
