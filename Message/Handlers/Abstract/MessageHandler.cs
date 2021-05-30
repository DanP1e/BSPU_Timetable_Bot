using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;
using TimetableBot.Commands;

namespace TimetableBot.Message
{
    public abstract class MessageHandler
    {
        public delegate void CommandExecutedHandler(MessageHandler executedCommand);
        public event CommandExecutedHandler CommandExecuted;
        
        public void Execute() 
        {
            Process();
            CommandExecuted?.Invoke(this);
        }
        public abstract void Process();
                      
    }
}
