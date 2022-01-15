using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class Decounter
    {
        readonly float kInitialTime = 2.0f;
        float decounter = 0.0f;

        public Decounter(float initialTime)
        {
            this.kInitialTime = initialTime;
        }

        public void Update(){ decounter -= Time.deltaTime; }
        public bool IsActive() { return decounter > 0.0f; }
        public bool IsExpired() { return !IsActive(); }
        public void Reset() { decounter = kInitialTime; }

        public bool UpdateAndResetIfExpired()
        {
            Update();
            if (IsExpired()) 
            { 
                Reset();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

