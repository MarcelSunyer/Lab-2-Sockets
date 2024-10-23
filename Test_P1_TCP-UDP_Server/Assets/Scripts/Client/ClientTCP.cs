﻿using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using TMPro;
using System.Text;

public class ClientTCP : MonoBehaviour
{
    public GameObject UItextObj;
    private TextMeshProUGUI UItext;
    private string clientText;
    private Socket server;

    void Start()
    {
        UItext = UItextObj.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        UItext.text = clientText;
    }

    public void StartClient()
    {
        Thread connect = new Thread(Connect);
        connect.Start();
    }

    void Connect()
    {
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(PlayerPrefs.GetString("Join_Server_IP")), 9050);
        server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        server.Connect(ipep);

        Thread sendThread = new Thread(Send);
        sendThread.Start();

        Thread receiveThread = new Thread(Receive);
        receiveThread.Start();
    }

    void Send()
    {
        string message = "Hello, Server!";
        byte[] data = Encoding.ASCII.GetBytes(message);

        server.Send(data);
        clientText += "\nSent: " + message;
    }

    void Receive()
    {
        byte[] data = new byte[1024];

        while (server != null && server.Connected)
        {
            try
            {
                int recv = server.Receive(data);
                if (recv > 0)
                {
                    string receivedMessage = Encoding.ASCII.GetString(data, 0, recv);
                    clientText += "\n" + receivedMessage;
                }
            }
            catch (SocketException ex)
            {
                Debug.Log($"Error receiving data: {ex.Message}");
                break; // Salir del bucle si hay un error grave
            }
        }

        server.Close(); // Cerrar la conexión cuando el bucle termine
    }
}