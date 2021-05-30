using System;
using System.Collections.Generic;
using System.Text;
using TimetableBot.Message.Handlers;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Args;
using TimetableBot.DATA.Interfaces;


namespace TimetableBot.Message
{
    public class TelegramInputHandler
    {
        
        private IDataTable<ITelegramUser> usersTable;
        public TelegramInputHandler(IDataTable<ITelegramUser> usersTable) 
        {
            this.usersTable = usersTable;
        }
        public void HandleUpdateEvent(object s, UpdateEventArgs e)
        {
            TelegramUser sender = null;
            MessageHandler messageHandler = null;

            switch (e.Update.Type)
            {
                case UpdateType.Message:
                    sender = (TelegramUser)AddUserToDataIfHeUnique(new TelegramUser(
                        nick: e.Update.Message.From.Username,
                        chatID: e.Update.Message.Chat.Id
                        ));
                    messageHandler = ProcessMessage(sender, e);
                    break;

                case UpdateType.CallbackQuery:
                    sender = (TelegramUser)AddUserToDataIfHeUnique(new TelegramUser(
                        nick: e.Update.CallbackQuery.From.Username,
                        chatID: e.Update.CallbackQuery.Message.Chat.Id
                        ));
                    messageHandler = new CallbackQueryHandler(sender, e);
                    break;
            }

            if (messageHandler != null && sender != null)
            {
                sender.AddMessageHandler(messageHandler);
                usersTable.AddItem(sender);
                messageHandler.Process();
            }
        }

        private MessageHandler ProcessMessage(TelegramUser sender, UpdateEventArgs e)
        {
            MessageHandler messageHandler = null;
            switch (e.Update.Message.Type)
            {
                case MessageType.Text:
                    messageHandler = new TextMessageHandler(sender, e);
                    break;
            }

            return messageHandler;
        }

        private ITelegramUser AddUserToDataIfHeUnique(ITelegramUser telegramUser)
        {
            int id = 0;
            if (usersTable.ItemBeingInTable(telegramUser, out id))
            {
                return usersTable.GetItem(id - 1);
            }
            else 
            {
                usersTable.AddItem(telegramUser);
            }
            return telegramUser;
        }




    }
}
