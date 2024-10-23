using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class ServerUDP : MonoBehaviour
{
    public int maxMessages = 60;

    public GameObject chatPanel;
    public GameObject textObject;

    public InputField chatBox; // Campo de entrada para el mensaje usando TMP_InputField

    [SerializeField]
    List<Message_Udp> messageList = new List<Message_Udp>();

    private Socket socket;
    private Thread receiveThread = null;

    public GameObject UItextObj;
    private TextMeshProUGUI UItext;
    private string serverText;

    private IPEndPoint remoteEndPoint;

    void Start()
    {
        UItext = UItextObj.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Enviar mensaje al servidor cuando el InputField tiene texto y se presiona Enter
        if (chatBox.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // Enviar el mensaje a todos los clientes
                SendMessageToServer(PlayerPrefs.GetString("Name_Player") + ": " + chatBox.text);
                chatBox.text = ""; // Limpiar el InputField después de enviar el mensaje
            }
        }
        else
        {
            // Activar el InputField si está vacío y Enter es presionado
            if (!chatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                chatBox.ActivateInputField();
            }
        }

        // Actualizar el texto del UI con los mensajes del servidor
        UItext.text = serverText;
    }

    public void startServer()
    {
        serverText = "Starting UDP Server...";

        // Crear y vincular el socket
        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 9050);
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        socket.Bind(localEndPoint);

        // Iniciar el hilo para recibir mensajes
        receiveThread = new Thread(Receive);
        receiveThread.Start();
    }

    void Receive()
    {
        int recv;
        byte[] data = new byte[1024];

        serverText += "\nWaiting for new Client...";

        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        EndPoint remoteSender = (EndPoint)sender;

        while (true)
        {
            // Recibir mensaje
            recv = socket.ReceiveFrom(data, ref remoteSender);
            string receivedMessage = Encoding.ASCII.GetString(data, 0, recv);

            serverText += $"\nMessage received from {remoteSender.ToString()}: {receivedMessage}";

            // Mostrar mensaje recibido en el chat local
            SendMessageToChat(receivedMessage);

            // Enviar respuesta al cliente (eco)
            Thread sendThread = new Thread(() => Send(receivedMessage, remoteSender));
            sendThread.Start();
        }
    }

    void Send(string message, EndPoint remoteSender)
    {
        byte[] data = Encoding.ASCII.GetBytes(message);

        // Enviar el mensaje al cliente
        socket.SendTo(data, data.Length, SocketFlags.None, remoteSender);
        serverText += $"\nSent to {remoteSender.ToString()}: {message}";
    }

    // Método para enviar un mensaje al servidor y a todos los clientes conectados
    public void SendMessageToServer(string text)
    {
        // Mostrar el mensaje en el chat local
        SendMessageToChat(text);
    }

    // Método para mostrar el mensaje en el chat local
    public void SendMessageToChat(string text)
    {
        if (messageList.Count > maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message_Udp newMessage = new Message_Udp();
        newMessage.text = text;
        GameObject newText = Instantiate(textObject, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;
        messageList.Add(newMessage);
    }

    // Método llamado al hacer clic en el botón de enviar
    public void OnSendButtonClicked()
    {
        if (!string.IsNullOrEmpty(chatBox.text))
        {
            SendMessageToServer(PlayerPrefs.GetString("Name_Player") + ": " + chatBox.text);
            chatBox.text = ""; // Limpiar el InputField después de enviar el mensaje
        }
    }
}

[System.Serializable]
public class Message_Udp
{
    public string text;
    public Text textObject;
}