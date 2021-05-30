using System;
using System.Collections.Generic;
using System.Text;
using TimetableBot.DATA.Interfaces;

namespace TimetableBot.Commands.Button
{
    public class PreferencesButtonCommand : Command
    {
        public PreferencesButtonCommand(ITelegramUser commandSender) : base(commandSender)
        {
        }

        public override string Identifier => "preferencessButton";

        public override async void Action(params object[] args)
        {
            var lU = CommandSender;
            string message = $"{lU.Nick} - нажал на кнопку в чате {lU.ChatID}";
            try
            {
                await Program.TelegramBotClient.SendTextMessageAsync(lU.ChatID, message);
            }
            catch (Exception e)
            {
                Logger.Error(CommandSender.ChatID + e.Message);
            }
            
           
        }
    }
}
