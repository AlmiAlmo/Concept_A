using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Action = ChannelSystem.RadioProtocol.Action;
using Information;


public class RadioTextFormatter 
{
    struct FormatData
    {
        public Subject emitter;
        public Subject initiator;
        public Subject recepient;
        public string context;

        public FormatData(Message msg)
        {
            emitter = msg.initiator;
            initiator = msg.initiator;
            recepient = msg.recepient;
            context = "";
        }
    }

    public static string FormatParcel(ChannelSystem.Parcel parcel)
    {
        var action = parcel.action;
        var msg = parcel.payload as Information.Message;

        if (msg == null) { return RadioTextFormatter.FormatUnknown(); }
        else switch (action)
            {
                case Action.CALL: return RadioTextFormatter.FormatCall(msg);
                case Action.ANSWER: return RadioTextFormatter.FormatAnswer(msg);
                case Action.DATA: return RadioTextFormatter.FormatInformation(msg);
                case Action.END: return RadioTextFormatter.FormatEnd(msg);
                default: return RadioTextFormatter.FormatUnknown();
            }
    }

    public static string FormatCall(Message msg)
    {
        var variants = RadioTextDatabase.GetCallVariants();
        var variant = ChooseVariant(variants);
        var data = new FormatData(msg);

        return Format(variant, data);
    }

    public static string FormatEnd(Message msg)
    {
        var variants = RadioTextDatabase.GetEndVariants();
        var variant = ChooseVariant(variants);
        var data = new FormatData(msg);

        return Format(variant, data);
    }

    static string FormatOrder(Order order)
    {
        var variants = RadioTextDatabase.GetOrderVariants(order.action);
        var variant = ChooseVariant(variants);
        bool isPrintPosition = Random.Range(0, 1) > 0;
        string orderTarget = "unknown";

        orderTarget = FormatPostition(order.target.pos);

        variant = string.Format(variant, order.recepient.name, orderTarget);
        var emitter = string.Format("\"{0}\": ", order.initiator.name);
        return emitter + variant;
    }


    static string FormatReport(Report report)
    {
        return FormatUnknown();
    }

    public static string FormatAnswer(Message msg)
    {
        var answer = msg as Answer;
        if (answer == null) { return FormatUnknown(); }
        var variants = RadioTextDatabase.GetAnswerVariants(answer);
        var variant = ChooseVariant(variants);
        var data = new FormatData(msg);

        return Format(variant, data);
    }

    public static string FormatInformation(Message msg)
    {
        if (msg is Order) { return FormatOrder(msg as Order); }
        else if (msg is Report) { return FormatReport(msg as Report); }
        else { return FormatUnknown(); }
    }

    public static string FormatUnknown()
    {
        var variants = RadioTextDatabase.GetUnknown();
        var variant = ChooseVariant(variants);
        return variant;
    }

    static string ChooseVariant(string[] variants)
    {
        return variants[Random.Range(0, variants.Length)];
    }

    static string FormatPostition(Vector3 pos)
    {
        string postition = pos.x.ToString();
        return postition;
    }

    static string Format(string variant, FormatData data)
    {
        variant = string.Format(variant, data.initiator.name, data.recepient.name);
        var emitter = string.Format("\"{0}\": ", data.emitter.name);
        return emitter + variant;
    }
}
