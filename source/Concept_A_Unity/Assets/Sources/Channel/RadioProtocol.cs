using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 *  Describes radio communication sequences
 */

namespace ChannelSystem
{
    public class RadioProtocol
    {
        public enum Action
        {
            CALL,
            DATA,
            ANSWER,
            END,
            //BAD,
            ANY,
            NONE,
        }

        public struct Step
        {
            public Action stimul;
            public Action response;
        }

        // Start
        static readonly Step callRecepient = new Step() { stimul = Action.NONE, response = Action.CALL };
        static readonly Step waitCall = new Step() { stimul = Action.CALL, response = Action.ANSWER };

        // Data
        static readonly Step sendData = new Step() { stimul = Action.ANSWER, response = Action.DATA };
        static readonly Step receiveData = new Step() { stimul = Action.DATA, response = Action.ANSWER };

        static readonly Step explictEnd = new Step() { stimul = Action.ANSWER, response = Action.END };

        // Service
        //public static readonly Step badParcel = new Step() { stimul = Direction.NONE, type = Action.BAD };


        public static readonly Step[] orderTransmission = new Step[] {
            callRecepient,
            sendData,
            explictEnd
        };

        public static readonly Step[] orderReception = new Step[] {
            waitCall,
            receiveData,
        };
    }

}