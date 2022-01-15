using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Information
{
    public sealed class Order : Message
    {
        public Action action { get; private set; }
        public ITarget target { get; private set; }

        public Order(Subject initiator, Subject receient, Action action, ITarget target)
            : base(initiator, receient)
        {
            this.action = action;
            this.target = target;
        }
    }
}