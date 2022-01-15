using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChannelSystem
{
    public interface IChannelView
    {
        public void OnParcelBroadcast(Parcel parcel, float printTime, bool isNonsense);
    }

}

