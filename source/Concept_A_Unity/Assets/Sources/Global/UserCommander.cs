using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ChannelSystem;

public class UserCommander : MonoBehaviour
{
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
        var playerSubject = Information.Subject.PLAYER;
        var order = new Information.Order(playerSubject, recepient, action, target);
        return order;
    }

    void AnswerQuestions()
    {
        var questioner = channelAI.questioner;
        if (!questioner.isAnswerNeeded) { return; }
        Information.Message answer = null;

        if (questioner.question is Information.Message)
        {
            var player = Information.Subject.PLAYER;
            var msg = questioner.question as Information.Message;
            bool isAck = (msg.recepient.subject == player.subject);
            answer = new Information.Message(player, msg.recepient);
        }
        else if (questioner.question is Information.Order)
        {
            Debug.Log("the tail wagging the dog");
        }
        questioner.AnswerAs(answer);
    }
}
