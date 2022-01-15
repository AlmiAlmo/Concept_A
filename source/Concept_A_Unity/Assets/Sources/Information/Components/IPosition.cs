using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Information
{
    public interface IPiece
    {

    }

    public interface IPosition : IPiece
    {
        public Vector3 pos { get; }
    }
}

