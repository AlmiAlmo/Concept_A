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
        Information.Message answer = new Information.Answer(character.subject, false);

        if(questioner.question is Information.Order)
        {
            var order = (questioner.question as Information.Order);
            bool isAck = IsOrderCanExecuted(order);
            //Debug.Log("Can execute this order? Answer is: " + isAck);
            if(isAck && isAwaitingOrder)
            {
                ReceiveOrder(questioner.question as Information.Order);
            }
            answer = new Information.Answer(character.subject, order.initiator, isAck, false);
        }
        else if (questioner.question is Information.Message)
        {
            var msg = questioner.question as Information.Message;
            isAwaitingOrder = (msg.recepient == character.subject);
            //Debug.Log("Qustion is recepient? Answer is: " + isAck);
            if (isAwaitingOrder)
            {
                answer = new Information.Answer(character.subject, msg.initiator, true, true);
            }
        }
        questioner.AnswerAs(answer);
    }

    bool IsOrderCanExecuted(Information.Order order)
    {
        return true;
    }
}
