using System;
using System.Collections.Generic;
using System.Text;
using TimetableBot.DATA.Interfaces;

namespace TimetableBot.Commands
{
    public class InvalidСommand : Command
    {
        public InvalidСommand(ITelegramUser commandSender) : base(commandSender)
        {

        }

        public override string Identifier => "/invalidCommand";

        public override async void Action(params object[] args)
        {
            
            string message = $"Неверная команда!\n" +
                $"{Command.GetIdentifier(typeof(InfoCommand))} - для полного списка команд";
            try
            {
                await Program.TelegramBotClient.SendTextMessageAsync(CommandSender.ChatID, message);
            }
            catch (Exception e)
            {
                Logger.Error(CommandSender.ChatID + e.Message);
            }
            
        }
    }
}
