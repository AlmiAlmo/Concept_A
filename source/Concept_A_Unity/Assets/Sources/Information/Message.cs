using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Information
{

    public class Message
    {
        public Subject initiator { get; protected set; }
        public Subject recepient { get; protected set; }

        public Message(Subject initiator, Subject recepient)
        {
            this.initiator = initiator;
            this.recepient = recepient;
        }

        public virtual Message ToMessage()
        {
            return this;
        }
    }

}