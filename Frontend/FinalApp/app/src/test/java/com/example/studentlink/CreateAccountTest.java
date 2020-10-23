package com.example.studentlink;

import org.junit.Test;

import android.content.Context;

import androidx.appcompat.app.AppCompatActivity;

import com.example.studentlink.ui.login.NewAccount.CanvasKey.CanvasWebview;
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
public class CreateAccountTest {

    @Mock
    private ConnectionClass connectionClass;

    @Mock
    private Context context;

    @Mock
    private AppCompatActivity theActivity;

    @Mock
    private HashMap<String,String> data;

//    @Mock
//    private CreateAccountLogic createAccount;


    @Test
    public void testBadUsernameLogin(){
        CreateAccountLogic createAccountLogic = new CreateAccountLogic(theActivity);
        createAccountLogic.setConnectionClass(connectionClass); //inject mock
//        CreateAccountLogic spy = Mockito.spy(createAccountLogic);
//        Mockito.doNothing().when(spy).navigateWebView();
        when(connectionClass.getResponse()).thenReturn("No");
//        doNothing().when(createAccountLogic,"navigateWebView").thenReturn();

        String response = createAccountLogic.checkCredentials("badUsername","badPassword");

        Assert.assertEquals( "", response);

    }

    @Test
    public void testNavigateToWebView(){
        CreateAccountLogic createAccount = new CreateAccountLogic(theActivity);
        createAccount.setConnectionClass(connectionClass); //inject mock

        when(connectionClass.getResponse()).thenReturn("No student under that username");
//        doNothing().when(createAccount).navigateToWebView(null, "uniqueUser", "password");

        String response = createAccount.checkCredentials("uniqueUser","password");
        Assert.assertEquals("Ok", response);
//        verify(createAccount, times(1)).navigateToWebView(null, "uniqueUser", "password");
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
