using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ChannelSystem;

public class UserCommander : MonoBehaviour
{
    const int kPlayerID = 0;
    readonly Information.Subject kPlayerSubject = new Information.Subject(kPlayerID);
    List<ChannelSystem.Channel> userChannels = new List<ChannelSystem.Channel> ();

    ChannelAI channelAI;
    ChannelSystem.Channel currentChannel;

    void Awake()
    {
        channelAI = ChannelAI.CreateAndAttach(gameObject, 999);
    }

    void Start()
    {
        var mainChannel = Manager.TryCreateChannel();
        userChannels.Add(currentChannel);
        currentChannel = mainChannel;
    }

    void Update()
    {
        AnswerQuestions();
    }

    public void CreateMoveOrder(Character character, Vector3 pos)
    {
        var mapPos = new Information.MapPosition(pos);
        var order = CreateOrder(character.subject, Information.Action.MOVE, mapPos);
        channelAI.SendMessage(order);
    }

    private Information.Order CreateOrder(
        Information.Subject recepient, 
        Information.Action action, 
        Information.ITarget target)
    {
        var order = new Information.Order(kPlayerSubject, recepient, action, target);
        return order;
    }

    void AnswerQuestions()
    {
        var questioner = channelAI.questioner;
        if (!questioner.isAnswerNeeded) { return; }
        bool isAck = false;

        if (questioner.question is Information.Subject)
        {
            var recepient = questioner.question as Information.Subject;
            isAck = (recepient.subject == 0);
            questioner.AnswerAs(isAck);
        }
        else if (questioner.question is Information.Order)
        {
            Debug.Log("the tail wagging the dog");
        }
        questioner.AnswerAs(isAck);
    }
}
