using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using TMPro;

public class DisplayLocalIP : MonoBehaviour
{
    public TextMeshProUGUI ipText; 


    void Start()
    {
        string localIP = GetLocalIPAddress();
        ipText.text = "Local IP Address: " + localIP; 

        PlayerPrefs.SetString("LocalIPAddress", localIP);
        PlayerPrefs.Save(); 
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
