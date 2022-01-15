using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChannelSystem
{
    public class Parcel
    {
        public RadioProtocol.Action action { get; private set; }
        public object payload { get; private set; }
        public System.Type payloadType { get; private set; }

        public Parcel(RadioProtocol.Action action, object payload)
        {
            this.action = action;
            this.payload = payload;
            if (payload is null) { this.payloadType = null; }
            else { this.payloadType = payload.GetType(); }
        }

        public string PrintDebug()
        {
            return (" Type: " + action + " PayloadType: " + payloadType);
        }
    }
}