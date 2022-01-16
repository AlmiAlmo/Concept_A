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

        public Information.Subject initiator;
        public Information.Subject recepient;

        public Parcel(RadioProtocol.Action action, object payload)
        {
            this.action = action;
            this.payload = payload;
            if (payload is null) { this.payloadType = null; }
            else { this.payloadType = payload.GetType(); }
            ExtractInitiator();
            ExtractRecepient();
        }

        void ExtractInitiator()
        {
            if(payload is null) { initiator = Information.Subject.UNKNOWN; }
            else if (payload is Information.Message)
            {
                var msg = payload as Information.Message;
                initiator = msg.initiator;
            }
        }

        void ExtractRecepient()
        {
            if (payload is null) { recepient = Information.Subject.UNKNOWN; }
            else if (payload is Information.Message)
            {
                var msg = payload as Information.Message;
                initiator = msg.initiator;
            }
        }

        public string PrintDebug()
        {
            return (" Type: " + action + " PayloadType: " + payloadType);
        }
    }
}