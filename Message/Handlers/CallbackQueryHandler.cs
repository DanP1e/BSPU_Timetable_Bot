using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;
using TimetableBot.Commands;
using TimetableBot.DATA.Interfaces;

namespace TimetableBot.Message.Handlers
{
    public class CallbackQueryHandler : MessageHandler
    {
        private ITelegramUser commandSender;
        public CallbackQueryEventArgs CallbackQueryEventArgs { get; private set; }
        public CallbackQueryHandler(ITelegramUser commandSender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            this.CallbackQueryEventArgs = callbackQueryEventArgs;
            this.commandSender = commandSender;
        }
        public override void Process()
        {
            string data = CallbackQueryEventArgs.CallbackQuery.Data;
            string[] args = data.Split(' ');
            Type comType = Command.GetCommandType(args[0]);
            Command com = (Command)Activator.CreateInstance(comType, commandSender);          
            com.Action(args);
        }
    }
}
