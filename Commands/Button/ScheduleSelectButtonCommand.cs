using System;
using System.Collections.Generic;
using System.Text;
using TimetableBot.DATA.Interfaces;

namespace TimetableBot.Commands.Button
{
    public class ScheduleSelectButtonCommand : Command
    {
        public ScheduleSelectButtonCommand(ITelegramUser commandSender) : base(commandSender)
        {
        }

        public override string Identifier => "tablenameIs";

        public override void Action(params object[] args)
        {
            if(args.Length != 2)
            {
                throw new ArgumentException($"Incorrect arguments!", nameof(args));
            }
            Command com = new SendTimetableCommand(CommandSender);

            com.Action(Identifier, args[1]);
        }
    }
}
