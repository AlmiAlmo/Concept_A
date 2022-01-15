using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChannelSystem
{
    public class User : MonoBehaviour
    {
        Queue<Parcel> received = new Queue<Parcel>();
        Parcel toSendParcel = null;

        string channelName;
        Channel channel;

        private void Update()
        {
            if (IsConnected() == false)
            {
                this.channel = Manager.TrySearchChannel(channelName);
                if (channel != null) { channel.Connect(this); }
                return;
            }

            if (toSendParcel != null) { TryBeginSending(); }
        }

        void TryBeginSending()
        {
            if (channel.IsActive() == false)
            {
                channel.Transmitte(this, toSendParcel);
                toSendParcel = null;
            }
        }

        public void SetChannel(string channelName)
        {
            this.channelName = channelName;
            if (channel != null) { channel.Disconnect(this); }
        }

        public Parcel TryGetReceived()
        {
            if(received.Count == 0) { return null; }
            return received.Dequeue();
        }

        public bool TrySetSending(Parcel parcel)
        {
            if (IsSendingComplete() == false) { return false; }
            toSendParcel = parcel;
            return true;
        }

        public bool IsSendingComplete()
        {
            return (toSendParcel == null);
        }

        public bool IsConnected()
        {
            return (channel != null);
        }

        public bool HasReceived()
        {
            return (received.Count > 0);
        }

        public void PushIncomingParcel(Parcel parcel)
        {
            received.Enqueue(parcel);
        }
    }
}
