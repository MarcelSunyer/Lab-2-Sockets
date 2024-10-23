using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using TMPro;
using System.Text;
using System.Collections.Generic;

public class ServerTCP : MonoBehaviour
{
    public int maxMessages = 60;

    public GameObject chatPanel;
    public GameObject textObject;

    public InputField chatBox; // Campo de entrada para el mensaje usando TMP_InputField

    [SerializeField]
    List<Message> messageList = new List<Message>();

    private Socket serverSocket;
    private List<User> connectedUsers = new List<User>();
    private Thread mainThread = null;

    public GameObject UItextObj;
    private TextMeshProUGUI UItext;
    private string serverText;

    public struct User
    {
        public string name;
        public Socket socket;
    }

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

    // Método para enviar un mensaje al servidor y a todos los clientes conectados
    public void SendMessageToServer(string text)
    {
        // Mostrar el mensaje en el chat local
        SendMessageToChat(text);

        // Enviar el mensaje a todos los clientes conectados
        foreach (User user in connectedUsers)
        {
            if (user.socket.Connected) // Asegurarse de que el socket del usuario esté conectado
            {
                byte[] data = Encoding.ASCII.GetBytes(text);
                user.socket.Send(data); // Enviar el mensaje a cada clienteç

            }
        }
        serverText += $"\nSent: {text}";
    }

    // Método para mostrar el mensaje en el chat local
    public void SendMessageToChat(string text)
    {
        if (messageList.Count > maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message newMessage = new Message();
        newMessage.text = text;
        GameObject newText = Instantiate(textObject, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;
        messageList.Add(newMessage);
    }

    public void startServer()
    {

        SendMessageToServer("Starting TDP Server...");

        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 9050);
        serverSocket.Bind(localEndPoint);
        serverSocket.Listen(10);

        mainThread = new Thread(CheckNewConnections);
        mainThread.Start();
    }

    void CheckNewConnections()
    {
        while (true)
        {
            User newUser = new User();
            newUser.name = "";
            newUser.socket = serverSocket.Accept(); 
            connectedUsers.Add(newUser);

            IPEndPoint clientEndPoint = (IPEndPoint)newUser.socket.RemoteEndPoint;
            serverText += $"\nConnected with {clientEndPoint.Address} at port {clientEndPoint.Port}";

            Thread newConnection = new Thread(() => Receive(newUser));
            newConnection.Start();
        }
    }

    void Receive(User user)
    {
        byte[] data = new byte[1024];
        int recv = 0;

        while (true)
        {
            try
            {
                recv = user.socket.Receive(data);
                if (recv == 0)
                    break;
                else
                {
                    string receivedMessage = Encoding.ASCII.GetString(data, 0, recv);
                    serverText += $"\nReceived: {receivedMessage}";

                    // Mostrar mensaje recibido en el chat local
                    SendMessageToChat(receivedMessage);
                }
            }
            catch (System.Exception ex)
            {
                Debug.Log($"Exception: {ex.Message}");
                break;
            }
        }

        user.socket.Close();
        connectedUsers.Remove(user);
        serverText += "\nUser disconnected";
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
public class Message
{
    public string text;
    public Text textObject;
}