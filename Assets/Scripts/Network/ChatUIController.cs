using System;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Text;

public class ChatUIController : MonoBehaviour
{
    [SerializeField] private ChatNetworkManager _networkManager;
    [SerializeField] private InputField _inputField;
    [SerializeField] private RectTransform _chatContent;
    [SerializeField] private TextMeshProUGUI _messagePrefab;

    private void Start()
    {
        _networkManager.OnMessageReceived.AddListener(AddMessageToUI);
    }

    private void AddMessageToUI(string messageText)
    {
        TextMeshProUGUI newMessage = Instantiate(_messagePrefab, _chatContent);
        newMessage.text = messageText;
    }
    public void SendChatMessage()
    {
        if (!_networkManager.isNetworkActive) return;

        string messageText = _inputField.text.Trim();

        if (string.IsNullOrEmpty(messageText)) return;

        char[] charArray = messageText.ToCharArray();

        ChatMessage message = new ChatMessage { Text = messageText };

        NetworkClient.Send(message);
        Debug.Log($"Sent message: {message.Text}");
        _inputField.text = "";
    }
}
