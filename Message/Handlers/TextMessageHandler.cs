using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;
using TimetableBot.Commands;
using TimetableBot.DATA.Interfaces;

namespace TimetableBot.Message.Handlers
{
    public class TextMessageHandler : MessageHandler
    {
        private ITelegramUser commandSender;
        public MessageEventArgs MessageEventArgs { get; private set; }
        public TextMessageHandler(ITelegramUser commandSender, MessageEventArgs messageEventArgs)
        {
            this.MessageEventArgs = messageEventArgs;
            this.commandSender = commandSender;
        }
        public override void Process()
        {
            string msg = MessageEventArgs.Message.Text;
            string[] textes = msg.Split('@');
            string[] arguments = textes[0].Split(' ');

            Type comType = Command.GetCommandType(arguments[0]);
            Command com = (Command)Activator.CreateInstance(comType, commandSender);
            com.Action(arguments);
        }
    }
}
