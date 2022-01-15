using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChannelSystem
{
    public class Manager : MonoBehaviour
    {
        static List<Channel> channels = new List<Channel>();

        static public Channel TryCreateChannel()
        {
            return TryCreateChannel("main");
        }

        static public Channel TryCreateChannel(string name)
        {
            if (TrySearchChannel(name) != null) { return null; }

            var objToSpawn = new GameObject("ChannelParent");
            var newChannelView = ChannelView.Create(objToSpawn);
            var newChannel = Channel.Create(objToSpawn, name, newChannelView);
            channels.Add(newChannel);
            return newChannel;
        }

        static public Channel TrySearchChannel(string name)
        {
            foreach (var channel in channels)
            {
                if (channel.channelName == name)
                {
                    return channel;
                }
            }
            return null;
        }
    }
}