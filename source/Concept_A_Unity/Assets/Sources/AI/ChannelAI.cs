using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ChannelSystem;
using Information;
using R = ChannelSystem.RadioProtocol;


public class ChannelAI : MonoBehaviour
{
    static public int count = 0;
    int myNum = 0;
    static public bool isDebugMode = false;

    public User channelUser { get; private set; }

    public Questioner<bool> questioner { get; private set; } = new Questioner<bool>();
    Sequence sequence = new Sequence(null);

    bool isIgnoreIfNotAck = false;

    Message messageToSend; 

    Parcel sendingParcel;
    Message sendingMessage;

    public static ChannelAI CreateAndAttach(GameObject parent)
    {
        var newNum = ChannelAI.count;
        ChannelAI.count++;
        return CreateAndAttach(parent, newNum);
    }

    public static ChannelAI CreateAndAttach(GameObject parent, int myNum)
    {
        var newChAI = parent.AddComponent<ChannelAI>();
        newChAI.channelUser = parent.AddComponent<User>();
        newChAI.myNum = myNum;
        return newChAI;
    }

    public bool SendMessage(Message msg)
    {
        Print("Try send message");
        bool readyToSend = (messageToSend == null);
        if(readyToSend) { messageToSend = msg; }
        return readyToSend; 
    }

    void Start()
    {
        channelUser.SetChannel("main");
    }

    void Update()
    {
        if(questioner.isAnswerNeeded) { return; }
        CheckAnswer();

        if (!IsBusy() && HasInformationToSend()) 
        {
            Print("Begin sending");
            isIgnoreIfNotAck = false;
            sendingMessage = messageToSend;
            sequence.SetTransmissionSequence(); 
        }
        var receivedParcel = channelUser.TryGetReceived();

        bool hasReceivedParcel = (receivedParcel != null);
        bool hasParcelToSend = (null != sendingParcel);

        if (hasReceivedParcel)  { HandleReceived(receivedParcel); }
        else if(hasParcelToSend)
        { 
            bool isSended = channelUser.TrySetSending(sendingParcel);
            if(isSended) {
                Print("Parcel sended:" + sendingParcel.PrintDebug());
                sendingParcel = null;
                // End of reception sequence
                if (sequence.IsLastStep())
                {
                    Print("Exchange complete");
                    CompleteSendingAndBeginListen();
                }
                else { sequence.NextStep(); }
            }
        }
        else if(IsTransmissionNeeded())
        {
            sendingParcel = GenerateParcelToSend();
        }
    }

    void HandleReceived(Parcel parcel)
    {
        Print("Parcel received:" + parcel.PrintDebug());

        sequence.SetStimulIfNeeded(parcel.action);

        switch (sequence.step.stimul)
        {
            case R.Action.CALL:
                isIgnoreIfNotAck = true;
                questioner.SetQuestion(parcel.payload);
                break;

            case R.Action.DATA:
                isIgnoreIfNotAck = false;
                questioner.SetQuestion(parcel.payload);
                break;

            case R.Action.ANSWER:
                bool isAck = (bool)parcel.payload;
                if (!isAck) { ResetAndListen(); }
                break;

            default:
                PrintError(myNum + " Inconsistent state");
                ResetAndListen();
                break;
        }
    }

    Parcel GenerateParcelToSend()
    {
        Parcel parcel = null;
        var parcelType = sequence.step.response;

        switch (parcelType)
        {
            case R.Action.ANSWER:
                if (!questioner.isAnswerNeeded )
                {
                    //if(isIgnoreIfNotAck) { return; }
                    parcel = new Parcel(parcelType, questioner.answer);
                }
                break;

            case R.Action.CALL:
                parcel = new Parcel(parcelType, sendingMessage.recepient);
                break;

            case R.Action.DATA:
                parcel = new Parcel(parcelType, sendingMessage);
                break;

            case R.Action.END:
                parcel = new Parcel(parcelType, null);
                break;

            default:
                PrintError(myNum + " Inconsistent state");
                ResetAndListen();
                break;
        }

        return parcel;
    }    

    bool HasInformationToSend()
    {
        return (messageToSend != null);
    }

    bool IsBusy()
    {
        return (!sequence.AtStart() || sendingMessage != null);
    }

    void CheckAnswer()
    {
        bool isAck = questioner.answer;

        if (isIgnoreIfNotAck && !isAck)
        {
            isIgnoreIfNotAck = false;
            Print("Reject to answer. Continue to listening");
            ResetAndListen();
        }
    }

    void CompleteSendingAndBeginListen()
    {
        if (sendingMessage != null)
        {
            Print("Complete sending");
            messageToSend = null;
            sendingMessage = null;
        }
        ResetAndListen();
    }

    void ResetAndListen()
    {
        Print("Reseted and listening");
        sequence.Reset();
    }

    bool IsTransmissionNeeded()
    {
        return sequence.isStimulReceived;
    }

    void Print(string str)
    {
        if (!isDebugMode) { return; }
        Debug.Log(myNum + ": " + str);
    }

    void Print(string str, string str2)
    {
        if (!isDebugMode) { return; }
        Debug.Log(myNum + ": " + str + "  " + str2);
    }

    void PrintError(string str)
    {
        if (!isDebugMode) { return; }
        Debug.LogError(myNum + ": " + str);
    }
}
