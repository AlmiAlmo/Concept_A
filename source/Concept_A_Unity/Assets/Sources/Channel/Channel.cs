using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChannelSystem 
{
    public class Channel : MonoBehaviour
    {
        public string channelName { get; private set; } = "unnamed";
        public IChannelView view;

        static readonly float ktransitionTime = 2.0f;
        Tools.Decounter transitionTimer = new Tools.Decounter(ktransitionTime);

        List<User> users = new List<User>();
        User currentUser = null;
        bool wasFailed = false;
        Parcel broadcastingParcel = null;

        public static Channel Create(GameObject parent, string name, ChannelView view)
        {
            var newChannel = parent.AddComponent<Channel>();
            newChannel.channelName = name; 
            newChannel.view = view; 
            return newChannel;
        }

        void Update()
        {
            bool isExpired = transitionTimer.UpdateAndResetIfExpired();
            if (isExpired && IsActive())
            {
                BroadcastParcelToUsers();
                wasFailed = false;
                currentUser = null;
                broadcastingParcel = null;
            }
        }

        public bool Connect(User newUser)
        {
            users.Add(newUser);
            return true;
        }

        public void Disconnect(User user)
        {
            users.Remove(user);
        }

        public bool IsActive()
        {
            return currentUser != null;
        }

        public void Transmitte(User initiator, Parcel parcel)
        {
            if (IsActive()){ wasFailed = true; }
 
            currentUser = initiator;
            this.broadcastingParcel = parcel;
            transitionTimer.Reset();
            OnParcelBroadcasting();
        }

        void BroadcastParcelToUsers()
        {
            if (wasFailed)
            {
                broadcastingParcel = new Parcel(RadioProtocol.Action.NONE, null);
            }

            foreach (var user in users)
            {
                if (user == currentUser) { continue; }
                user.PushIncomingParcel(broadcastingParcel);
            }
        }

        void OnParcelBroadcasting()
        {
            if(view == null) { return; }
            view.OnParcelBroadcast(broadcastingParcel, ktransitionTime, wasFailed);
        }
    }
}

