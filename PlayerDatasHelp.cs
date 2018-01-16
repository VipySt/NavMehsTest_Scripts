using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeekBrains.lesson3
{
    public enum TypeMessagePlayer : byte
    {
        AllIsOk,
        TooLongWay,
        WayNotExist
    }

    public struct PlayerMessages
    {
        public TypeMessagePlayer TypeMessage
        {
            get { return _typeMessage; }
        }

        public string Message
        {
            get { return _message; }
        }


        public PlayerMessages(TypeMessagePlayer type, string message)
        {
            _typeMessage = type;
            _message = message;
        }


        private TypeMessagePlayer _typeMessage;

        private string _message;


    }
}
