using Domain.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    public interface IMessagesRepository
    {
        #region Входящие сообщения
        IEnumerable<IncomingMessage> GetIncomingMessages();
        IEnumerable<IncomingMessage> GetIncomingMessagesByUserId(int userId);
        void SaveIncomingMessage(IncomingMessage message);
        void DeleteIncomingMessage(IncomingMessage message);
        #endregion

        #region Исходящие сообщения
        IEnumerable<OutgoingMessage> GetOutgoingMessages();
        IEnumerable<OutgoingMessage> GetOutgoingMessagesByUserId(int userId);
        void SaveOutgoingMessage(OutgoingMessage message);
        void DeleteOutgoingMessage(OutgoingMessage message);
        #endregion
    }
}
