using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace TimetableBot.Commands
{
    public class InfoCommand : Command
    {
        public InfoCommand(DATA.Interfaces.ITelegramUser commandSender) : base(commandSender)
        {
        }

        public override string Identifier => "/help";

        public async override void Action(params object[] args)
        {           
            string message = "© Данил С.\n" +
                "Команды : \n" +
                $"{Command.GetIdentifier(typeof(InfoCommand))} - показывает информацию обо мне.\n" +
                $"{Command.GetIdentifier(typeof(SendTimetableCommand))} [группа] - скидывает в чат файл расписания\n" +
                "" +
                "Это всё)";
            try
            {
                await Program.TelegramBotClient.SendTextMessageAsync(CommandSender.ChatID, message);
            }
            catch (Exception e)
            {
                Logger.Error(CommandSender.ChatID + e.Message);
            }
        }

        private IReplyMarkup GetMessageButtons()
        {
            return new InlineKeyboardMarkup(new InlineKeyboardButton() { Text = "Настройки", CallbackData = $"preferencessButton" });
        }
    }
}
