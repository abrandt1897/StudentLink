package com.example.studentlink.ui.chat;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;

import androidx.annotation.NonNull;

import com.example.studentlink.ConnectionClass;
import com.example.studentlink.Global;
import com.example.studentlink.R;
import android.text.method.ScrollingMovementMethod;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import org.java_websocket.client.WebSocketClient;
import org.java_websocket.handshake.ServerHandshake;

import java.net.URI;
import java.net.URISyntaxException;
import java.util.HashMap;
import java.util.Map;


public class ChatPageActivity extends AppCompatActivity {

    private WebSocketClient mWebSocketClient;

    private Button bSend;
    private TextView msgOutput;
    private EditText msgInput;
    ConnectionClass connection;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.chatpage_layout);

        connection = new ConnectionClass(this);
        connectWebSocket();

        msgOutput = findViewById(R.id.chatText);
        bSend = findViewById(R.id.bSend);
        msgInput = findViewById(R.id.msgInput);

        // Add scrolling
        msgOutput.setMovementMethod(new ScrollingMovementMethod());
        msgOutput.setHint("Chat Page Placeholder");
        Toast.makeText(this, "ws://coms-309-mc-02.cs.iastate.edu:5000/ws/" + Global.selectedChatID + "/" + Global.studentID, Toast.LENGTH_SHORT).show();

        bSend.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                // Get the message from the input
                String message = msgInput.getText().toString();

                // If the message is not empty, send the message
                if(message != null && message.length() > 0){
                    mWebSocketClient.send(message);
                    msgInput.setText("");
                }
            }
        });

    }


    private void connectWebSocket() {
        URI uri;
        try {
            // uri = new URI("wss://echo.websocket.org" );
            uri = new URI("ws://coms-309-mc-02.cs.iastate.edu:5000/ws/" + Global.selectedChatID + "/" + Global.studentID );
            Toast.makeText(this, uri.toString(), Toast.LENGTH_LONG).show();
            //uri = ws://coms-309-mc-02.cs.iastate.edu:5000/ws/{chatID}/{StudentID};
        } catch (URISyntaxException e) {
            e.printStackTrace();
            return;
        }

        mWebSocketClient = new WebSocketClient(uri) {

            @Override
            public void onOpen(ServerHandshake serverHandshake) {
                Log.i("Websocket", "Opened");
            }

            @Override
            public void onMessage(String msg) {
                Log.i("Websocket", "Message Received");
                // Appends the message received to the previous messages
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        msgOutput.append("\n" + msg);
                        // Stuff that updates the UI
                    }
                });

            }

            @Override
            public void onClose(int errorCode, String reason, boolean remote) {
                Log.i("Websocket", "Closed " + reason);
            }

            @Override
            public void onError(Exception e) {
                Log.i("Websocket", "Error " + e.getMessage());
            }
        };
        mWebSocketClient.connect();
    }



}
