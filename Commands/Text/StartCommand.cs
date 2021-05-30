using System;
using System.Collections.Generic;
using System.Text;
using TimetableBot.DATA.Interfaces;

namespace TimetableBot.Commands
{
    public class StartCommand : Command
    {
        public StartCommand(ITelegramUser commandSender) : base(commandSender)
        {

        }
        public override string Identifier => "/start";

        public override void Action(params object[] args)
        {

            Command infoCommand = new InfoCommand(CommandSender);
           
            infoCommand.Action();
        }
    }
}
