using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScenes : MonoBehaviour
{
    [SerializeField] private TMP_Text displayText1;
    [SerializeField] private TMP_Text displayText2;
    [SerializeField] private TMP_Text displayText3;

    private void Start()
    {
        // Verifica y asigna el texto
        if (PlayerPrefs.HasKey("SavedInput1"))
        {
            displayText1.text = PlayerPrefs.GetString("SavedInput1");
        }
        else
        {
            displayText1.text = "No hay texto guardado";
        }

        if (PlayerPrefs.HasKey("SavedInput2"))
        {
            displayText2.text = PlayerPrefs.GetString("SavedInput2");
        }
        else
        {
            displayText2.text = "No hay texto guardado";
        }
        if (PlayerPrefs.HasKey("LocalIPAddress"))
        {
            displayText3.text = PlayerPrefs.GetString("LocalIPAddress");
        }
        else
        {
            displayText2.text = "No hay texto guardado";
        }
    }
}
