using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace N2.Messaging
{
    public static class msgFilter
    {
        //Прочтенные сообщения.
        public static IEnumerable<Message> GetReadMsg(this IEnumerable<Message> filtMessages)
        {
            return filtMessages.Where(child => child.isRead);
        }
        
        //Непрочтенные сообщения.
        public static IEnumerable<Message> GetNotReadMsg(this IEnumerable<Message> filtMessages)
        {
            return filtMessages.Where(child => !child.isRead);
        }

        //Невыполненные задания.
        public static IEnumerable<Task> GetNotDoneTsk(this IEnumerable<Task> filtMessages)
        {
			return filtMessages.Where(child => !child.isDone);
        }

        //Входящие сообщения текущего пользователя.
        public static IEnumerable<Message> GetIncomingMsg(this IEnumerable<Message> filtMessages, string userName)
        {
            return from child in filtMessages
                    where (string.Equals(child.To, userName, StringComparison.OrdinalIgnoreCase)
                            )
                    select child;
        }

        //Исходящие сообщения текущего пользователя.
        public static IEnumerable<Message> GetSentMsg(this IEnumerable<Message> filtMessages, string userName)
        {
            return from child in filtMessages
                        where (string.Equals(child.From, userName, StringComparison.OrdinalIgnoreCase)
                                )
                        select child;
        }
    }
}
