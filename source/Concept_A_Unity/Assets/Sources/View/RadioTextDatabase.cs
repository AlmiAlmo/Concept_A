using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Action = ChannelSystem.RadioProtocol.Action;

public class RadioTextDatabase
{
    static public string[] GetVariant(ChannelSystem.Parcel parcel)
    {
        var action = parcel.action;
        var isAck = false;
        if (action == Action.ANSWER)
        {
            isAck = (bool)parcel.payload;
        }

        switch(action)
        {
            case Action.CALL: return callVariants;
            case Action.ANSWER:
                if(isAck) { return successAnswerVariants; }
                else { return negativeAnswerVariants; }
            case Action.DATA: return dataVariants;
            case Action.END: return endVariants;
            default: return unknownVariants;
        }
    }

    static readonly string[] callVariants = new string[]
    {
        "*i calling *r",
        "*r answer to *i"
    };

    static readonly string[] successAnswerVariants = new string[]
    {
        "*r ok",
        "*r ack"
    };

    static readonly string[] negativeAnswerVariants = new string[]
    {
        "*r negative",
        "*r nack"
    };

    static readonly string[] dataVariants = new string[]
    {
        "here some data",
        "listen next order"
    };

    static readonly string[] endVariants = new string[]
    {
        "*i out",
        "*i closing transmission"
    };

    static readonly string[] unknownVariants = new string[]
    {
        "unknown"
    };
}
