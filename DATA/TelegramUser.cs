using System;
using System.Collections.Generic;
using System.Text;
using TimetableBot.Message;
using TimetableBot.DATA.Interfaces;

namespace TimetableBot
{
    public class TelegramUser : ITelegramUser
    {
        private List<MessageHandler> messageHandlers = new List<MessageHandler>();
        public string Nick { get; }
        public long ChatID { get; }
 
        public TelegramUser(string nick, long chatID)
        {
            Nick = nick;
            ChatID = chatID;           
        }
        public void AddMessageHandler(MessageHandler messageHandler) 
        {
            messageHandler.CommandExecuted += RemoveMessageHandler;
            messageHandlers.Add(messageHandler);
        }
        public async void SendTextMessage(string message) 
        {
            try
            {
                await Program.TelegramBotClient.SendTextMessageAsync(ChatID, message);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
            
        }
        private void RemoveMessageHandler(MessageHandler messageHandler)
        {
            messageHandler.CommandExecuted -= RemoveMessageHandler;
            messageHandlers.Remove(messageHandler);
        }
    }
}
