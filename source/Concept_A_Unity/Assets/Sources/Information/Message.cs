using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Information
{

    public abstract class Message
    {
        public Subject initiator { get; protected set; }
        public Subject recepient { get; protected set; }

        protected Message(Subject initiator, Subject recepient)
        {
            this.initiator = initiator;
            this.recepient = recepient;
        }
    }

}