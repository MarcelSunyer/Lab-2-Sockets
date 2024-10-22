using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Create_Server_Info : MonoBehaviour
{
    [SerializeField] public InputField Cs_Name;

    [SerializeField] public InputField Cs_Server;

    public void SaveInputTexts()
    {

        PlayerPrefs.SetString("SavedInput1", Cs_Name.text);

        PlayerPrefs.SetString("SavedInput2", Cs_Server.text);

        PlayerPrefs.Save();
    }

}
