package com.example.studentlink;

import org.junit.Test;

import android.content.Context;
import android.content.Intent;

import androidx.appcompat.app.AppCompatActivity;

import com.example.studentlink.ui.login.NewAccount.CanvasKey.CanvasWebview;
import com.example.studentlink.ui.login.NewAccount.CreateAccount;
import com.example.studentlink.ui.login.NewAccount.Logic.CreateAccountLogic;
import com.example.studentlink.ui.login.SignIn.LoginActivity;

import static org.powermock.api.mockito.PowerMockito.when;
import static org.powermock.api.mockito.PowerMockito.doNothing;
import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.runners.MockitoJUnitRunner;
import org.powermock.api.mockito.PowerMockito;
import org.powermock.api.mockito.verification.PrivateMethodVerification;
import org.powermock.core.classloader.annotations.PrepareForTest;
import org.powermock.modules.junit4.PowerMockRunner;
import org.powermock.reflect.Whitebox;

import java.util.HashMap;
import java.util.Map;

import static org.mockito.Mockito.doNothing;
import static org.mockito.Mockito.doReturn;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.times;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.when;

import static org.junit.Assert.*;


@RunWith(PowerMockRunner.class)
@PrepareForTest(CreateAccountLogic.class)
public class NavWebViewTest {

    @Mock
    private ConnectionClass connectionClass;

    @Mock
    private AppCompatActivity theActivity;

//    @Test
//    public void testNavigateToWebView() throws Exception{
//        CreateAccountLogic createAccountLogic = new CreateAccountLogic(theActivity);
//        createAccountLogic.setConnectionClass(connectionClass); //inject mock
//        createAccountLogic = PowerMockito.spy(createAccountLogic);
//
//        PowerMockito.doNothing().when(createAccountLogic, PowerMockito.method(CreateAccountLogic.class, "navigateToWebView")).withArguments(context, CanvasWebview.class, "uniqueUsername", "goodPassword");
//        Mockito.when(connectionClass.getResponse()).thenReturn("No student under that username");
//        createAccountLogic.checkCredentials("uniqueUsername","goodPassword");
//        PrivateMethodVerification privateMethodInvocation = PowerMockito.verifyPrivate(createAccountLogic);
//
//        privateMethodInvocation.invoke("navigateToWebView", context, CanvasWebview.class, "uniqueUsername","goodPassword");
//
//    }

    @Test
    public void test2() throws Exception {
        CreateAccountLogic createAccountLogic = new CreateAccountLogic(theActivity);
        createAccountLogic.setConnectionClass(connectionClass); //inject mock
        CreateAccountLogic theSpy = PowerMockito.spy(createAccountLogic);
        Intent intent = new Intent(theActivity.getApplicationContext(),CanvasWebview.class);

        Mockito.when(connectionClass.getResponse()).thenReturn("No student under that username");
//        createAccountLogic.checkCredentials("uniqueUsername","goodPassword");
//        Mockito.when(intent.putExtra("Username","unique")).thenReturn(intent);
//        Mockito.when(intent.putExtra("Password","goodPassword")).thenReturn(intent);
//            PowerMockito.when(theSpy,"putExtra").thenReturn(intent);
        Whitebox.invokeMethod(theSpy, "checkCredentials","uniqueUser","password");
        PowerMockito.verifyPrivate(theSpy, times(1)).invoke("navigateToWebView",intent, "uniqueUser","password");
//        final SomeClass someClass = new SomeClass();
//        final SomeClass spy = PowerMockito.spy(someClass);

//        PowerMockito.doReturn("someValue", spy, "privateMethod1");
//        final String response = Whitebox.invokeMethod(spy, "anotherPrivateMethod");
//        assert(response)
//        verifyPrivate(spy, times(1)).invoke("anotherPrivateMethod", "xyz");

    }


}
