using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using TMPro; // Asegúrate de incluir esto si usas TextMeshPro

public class DisplayLocalIP : MonoBehaviour
{
    public TextMeshProUGUI ipText; // Si usas TextMeshPro
    // public UnityEngine.UI.Text ipText; // Si usas UI Text

    void Start()
    {
        string localIP = GetLocalIPAddress();
        ipText.text = "Local IP Address: " + localIP; // Actualiza el texto
    }

    string GetLocalIPAddress()
    {
        string localIP = "";

        try
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }

            if (string.IsNullOrEmpty(localIP))
            {
                Debug.LogError("Local IP Address not found!");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error retrieving local IP address: " + ex.Message);
        }

        return localIP;
    }
}
