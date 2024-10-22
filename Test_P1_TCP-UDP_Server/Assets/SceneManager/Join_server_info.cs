using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Join_server_info : MonoBehaviour
{
    [SerializeField] public InputField Js_name;

    [SerializeField] public InputField Js_IP;

    public void SaveInputTexts()
    {

        PlayerPrefs.SetString("SavedInput1", Js_name.text);

        PlayerPrefs.SetString("SavedInput2", Js_IP.text);

        PlayerPrefs.Save();
    }

}
