using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace N2.Messaging
{
    public static class msgFilter
    {
        //Прочтенные сообщения.
        public static Message[] GetReadMsg(Message[] filtMessages)
        {
            return (from child in filtMessages
                    where (child.isRead)
                    select child).ToArray();
        }
        
        //Непрочтенные сообщения.
        public static Message[] GetNotReadMsg(Message[] filtMessages)
        {
            return (from child in filtMessages
                    where (!child.isRead)
                    select child).ToArray();
        }

        //Невыполненные задания.
        public static Task[] GetNotDoneTsk(Task[] filtMessages)
        {
            return (from child in filtMessages
                    where (!child.isDone)
                    select child).ToArray();
        }

        //Входящие сообщения текущего пользователя.
        public static Message[] GetIncomingMsg(Message[] filtMessages, string userName)
        {
            return (from child in filtMessages
                    where (string.Equals(child.To, userName, StringComparison.OrdinalIgnoreCase)
                            )
                    select child).ToArray();
        }

        //Исходящие сообщения текущего пользователя.
        public static Message[] GetSentMsg(Message[] filtMessages, string userName)
        {
            return (from child in filtMessages
                        where (string.Equals(child.From, userName, StringComparison.OrdinalIgnoreCase)
                                )
                        select child).ToArray();
        }
    }
}
