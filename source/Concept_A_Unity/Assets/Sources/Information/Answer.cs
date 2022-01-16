using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Information
{
    public sealed class Answer : Message
    {
        public bool isAck { get; private set; }
        public bool isAnswerToCall { get; private set; } = false;

        public Answer(Subject initiator, Subject receient, bool isAck, bool isAnswerToCall)
            : base(initiator, receient)
        {
            this.isAck = isAck;
            this.isAnswerToCall = isAnswerToCall;
        }

        public Answer(Subject initiator, bool isAck)
            : base(initiator, Subject.UNKNOWN)
        {
            this.isAck = isAck;
        }

        public override Message ToMessage()
        {
            return new Message(initiator, recepient);
        }
    }
}
