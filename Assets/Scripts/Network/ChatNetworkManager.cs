using System;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

public class MessageReceivedEvent : UnityEvent<string> { }
public class ChatNetworkManager : NetworkManager
{
    public override void Awake()
    {
        base.Awake();
        NetworkServer.RegisterHandler<ChatMessage>(OnReceiveMessage);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        Debug.Log($"Client Connected");
    }

    public MessageReceivedEvent OnMessageReceived = new MessageReceivedEvent();


    private void OnReceiveMessage(NetworkConnection conn, ChatMessage message)
    {
        Debug.Log($"Received message: {message.Text}");

        // OnMessageReceived.Invoke(message.Text);

    }

}
