using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAI : AI
{
    ChannelAI channelAI;
    Character character;
    Pawn pawn;

    bool isAwaitingOrder = false;

    static float kThinkTime = 2.0f;
    static float thinkTimeout = kThinkTime;

    void Awake()
    {
        character = this.gameObject.GetComponent<Character>();
        pawn = this.gameObject.GetComponent<Pawn>();
        channelAI = ChannelAI.CreateAndAttach(this.gameObject);
    }

    void Start()
    {
        pawn.movingBehaviour.target = this.gameObject.transform.position;
    }

    void Update()
    {
        thinkTimeout -= Time.deltaTime;
        if(thinkTimeout > 0) { return; }
        thinkTimeout = kThinkTime;

        AnswerQuestions();
    }

    public void ReceiveMessage(Information.Message msg)
    {
        if (msg is Information.Order) { ReceiveOrder((Information.Order)msg); }
    }

    void ReceiveOrder(Information.Order order)
    {
        if(order.action == Information.Action.MOVE)
        {
            pawn.movingBehaviour.target = order.target.pos;
        }
    }

    void AnswerQuestions()
    {
        var questioner = channelAI.questioner;
        if (!questioner.isAnswerNeeded) { return; }
        bool isAck = false;

        if(questioner.question is Information.Subject)
        {
            var recepient = questioner.question as Information.Subject;
            isAck = (recepient == character.subject);
            isAwaitingOrder = isAck;
            //Debug.Log("Qustion is recepient? Answer is: " + isAck);
            questioner.AnswerAs(isAck);
        }
        else if(questioner.question is Information.Order)
        {
            isAck = IsOrderCanExecuted(questioner.question as Information.Order);
            //Debug.Log("Can execute this order? Answer is: " + isAck);
            if(isAck && isAwaitingOrder)
            {
                ReceiveOrder(questioner.question as Information.Order);
            }
        }
        questioner.AnswerAs(isAck);
    }

    bool IsOrderCanExecuted(Information.Order order)
    {
        return true;
    }
}
