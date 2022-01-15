using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Information
{
    public class MapPosition : ITarget
    {
        public Vector3 pos { get; }
        public int subject { get; }

        public MapPosition(Vector3 position)
        {
            pos = position;
            subject = 0;
        }
    }
}
