using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Information
{
    public class Subject : ISubject
    {
        public int subject { get; }

        public Subject(int subject)
        {
            this.subject = subject; 
        }
    }
}
