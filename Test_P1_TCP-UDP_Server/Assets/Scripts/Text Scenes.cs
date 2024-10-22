using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScenes : MonoBehaviour
{
    [SerializeField] private Text displayText1;
    [SerializeField] private Text displayText2;

    private void Start()
    {
        if (PlayerPrefs.HasKey("SavedInput1"))
        {
            displayText1.text = PlayerPrefs.GetString("SavedInput1");
        }
        if (PlayerPrefs.HasKey("SavedInput2"))
        {
            displayText2.text = PlayerPrefs.GetString("SavedInput2");
        }
    }
}
