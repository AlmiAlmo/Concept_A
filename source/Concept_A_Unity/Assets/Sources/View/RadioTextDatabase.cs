using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Action = ChannelSystem.RadioProtocol.Action;
using Information;

public class RadioTextDatabase
{

    public static string[] GetCallVariants()
    {
        return callVariants;
    }

    public static string[] GetEndVariants()
    {
        return endVariants;
    }

    public static string[] GetAnswerVariants(Answer answer)
    {
        if (answer.isAnswerToCall)
        {
            if (answer.isAck) { return successCallAnswerVariants; }
            else { return negativeCallAnswerVariants; }
        }
        else
        {
            if (answer.isAck) { return successAnswerVariants; }
            else { return negativeAnswerVariants; }
        }
    }

    public static string[] GetOrderVariants(Information.Action action)
    {
        switch (action)
        {
            case Information.Action.MOVE: return orderMoveVariants;
            default: return GetUnknown();
        }
    }

    public static string[] GetOrder(Order order)
    {
        if (order == null) { return GetUnknown(); }
        else switch(order.action)
        {
            case Information.Action.MOVE: return orderMoveVariants; 
            default: return GetUnknown();
        }
    }

    public static string[] GetReport(Report report)
    {
        if (report == null) { return GetUnknown(); }

        return GetUnknown();
    }

    public static string GetName()
    {
        var random = new System.Random();
        var index = random.Next(0, names.Count - 1);
        var name = names[index];
        names.RemoveAt(index);
        return name;
    }

    public static string[] GetUnknown()
    {
        return unknownVariants;
    }


    static readonly string[] callVariants = new string[]
    {
        "{0} calling {1}",
        "{1} answer to {0}",
    };

    static readonly string[] successAnswerVariants = new string[]
    {
        "Affirmative, {1}",
        "Affirmative.",
    };

    static readonly string[] negativeAnswerVariants = new string[]
    {
        "Negative.",
    };

    static readonly string[] successCallAnswerVariants = new string[]
    {
        "{0} online.",
        "{0} receiving.",
        "{0} listening.",
    };

    static readonly string[] negativeCallAnswerVariants = new string[]
    {
        "{1}, repeat.",
    };

    static readonly string[] orderMoveVariants = new string[]
    {
        "{0}, move to {1} position.",
    };

    static readonly string[] reportVariants = new string[]
    {
        "REPORT.",
    };

    static readonly string[] endVariants = new string[]
    {
        "{0} out.",
        "{0} closing transmission.",
        "{0}, over.",
    };

    static readonly string[] unknownVariants = new string[]
    {
        "unknown"
    };

    static readonly List<string> names = new List<string>
    {
        "Cummer",
        "Perdix",
        "Abu",
        "Cockhole",
        "Swine",
        "Vata",
        "Lahta",
        "Zoomer",
    };
}
