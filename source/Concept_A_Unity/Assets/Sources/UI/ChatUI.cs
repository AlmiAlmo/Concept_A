using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatUI : MonoBehaviour
{
    public GameObject message;
    public GameObject content;

    UnityEngine.UI.ScrollRect scrollRect;

    void Awake()
    {
        scrollRect = gameObject.GetComponentInChildren<UnityEngine.UI.ScrollRect>();
    }

    void Start()
    {
        /*for (int i = 0; i < 15; i++)
        {
            AddMessage("txt");
        }*/
    }

    public void AddMessage(string text)
    {
        var msg = Instantiate(message);
        var chatMessageUI = msg.GetComponent<ChatMessageUI>();
        chatMessageUI.SetText(text);
        msg.transform.SetParent(content.transform, false);
        UpdateScrollPosition();
    }

    void UpdateScrollPosition()
    {
        ScrollToBottom();
    }

    void ScrollToTop()
    {
        scrollRect.normalizedPosition = new Vector2(0, 1);
    }
    void ScrollToBottom()
    {
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }

}
