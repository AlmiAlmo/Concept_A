using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Information
{
    public class Subject : ISubject
    {
        
        public enum ReservedID
        {
            UNKNOWN = 0,
            PLAYER = 1,
            LAST = 2,
        }

        public static Subject PLAYER 
        { get; } = new Subject(ReservedID.PLAYER);

        public static Subject UNKNOWN
        { get; } = new Subject(ReservedID.UNKNOWN);

        public int subject { get; }
        public string name { get; private set; }

        public Subject(int subject)
        {
            this.subject = subject;
            this.name = RadioTextDatabase.GetName();
        }

        public Subject(ReservedID subject)
        {
            this.subject = ((int)subject);
            this.name = RadioTextDatabase.GetName();
        }
    }
}
