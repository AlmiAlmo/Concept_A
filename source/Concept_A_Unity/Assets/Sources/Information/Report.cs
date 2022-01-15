using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Information
{
    public sealed class Report : Message
    {
        public Action action { get; private set; }
        public ITarget target { get; private set; }

        public Report(Subject initiator, Subject receient, Action action, ITarget target)
            : base(initiator, receient)
        {
            this.action = action;
            this.target = target;
        }
    }
}