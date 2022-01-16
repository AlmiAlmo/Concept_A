using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Action = ChannelSystem.RadioProtocol.Action;
using Information;

public class ChannelView : MonoBehaviour, ChannelSystem.IChannelView
{
    public static ChannelView Create(GameObject parent)
    {
        return parent.AddComponent<ChannelView>();
    }

    public void OnParcelBroadcast(ChannelSystem.Parcel parcel, float printTime, bool isNonsense)
    {
        string toPrint = "unknown";
        if(isNonsense) { toPrint = RandomString(); }
        else
        {
            var textParcel = RadioTextFormatter.FormatParcel(parcel);
            Print(textParcel);
        }
    }

    void Print(string str)
    {
        Debug.Log(str);
    }


    static string RandomString()
    {
        int stringLengt = Random.Range(8, 16);
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[stringLengt];
        
        for (int i = 0; i < stringChars.Length; i++)
        {
            int random = Random.Range(0, chars.Length);
            stringChars[i] = chars[random];
        }

        return new string(stringChars);
    }
}
