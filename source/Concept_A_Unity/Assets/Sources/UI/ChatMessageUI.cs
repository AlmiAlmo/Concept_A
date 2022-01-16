using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatMessageUI : MonoBehaviour
{
    UnityEngine.UI.Text text;

    void Awake()
    {
        text = gameObject.GetComponentInChildren<UnityEngine.UI.Text>();
    }

    public void SetText(string newText)
    {
        text.text = newText;
    }

}
