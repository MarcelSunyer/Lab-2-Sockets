using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Save_Texts : MonoBehaviour
{
    [SerializeField] public InputField inputField1;

    [SerializeField] public InputField inputField2;

    public void SaveInputTexts()
    {

        PlayerPrefs.SetString("SavedInput1", inputField1.text);
        PlayerPrefs.SetString("SavedInput2", inputField2.text);
        PlayerPrefs.Save();
        Debug.Log(inputField1.text);
        Debug.Log(inputField2.text);
    }

}
