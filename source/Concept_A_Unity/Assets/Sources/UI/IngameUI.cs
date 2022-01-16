using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameUI : MonoBehaviour
{
    Canvas canvas;
    public GameObject channelChat;

    ChatUI chatUI;

    void Awake()
    {
        canvas = this.gameObject.GetComponentInChildren<Canvas>();
        chatUI = Instantiate(channelChat, canvas.gameObject.transform).GetComponentInChildren<ChatUI>();
    }
}
