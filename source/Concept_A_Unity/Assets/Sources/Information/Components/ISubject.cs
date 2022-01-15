using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Information
{
    public interface ISubject : IPiece
    {
        public int subject { get; }
    }

}