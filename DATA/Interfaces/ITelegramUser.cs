using System;
using System.Collections.Generic;
using System.Text;
using TimetableBot.Message;


namespace TimetableBot.DATA.Interfaces
{
    public interface ITelegramUser
    {
        string Nick { get; }
        long ChatID { get; }
        void AddMessageHandler(MessageHandler messageHandler);
    }
}
