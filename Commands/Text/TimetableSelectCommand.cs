using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;
using System.Linq;
using TimetableBot.DATA.Interfaces;

namespace TimetableBot.Commands
{
    public class TimetableSelectCommand : Command
    {
        public TimetableSelectCommand(ITelegramUser commandSender) : base(commandSender)
        {

        }

        public override string Identifier => "/table";

        public override async void Action(params object[] args)
        {
            string message = "Выбери группу :";

            long chatId = CommandSender.ChatID;
            try
            {
                await Program.TelegramBotClient.SendTextMessageAsync(chatId, message, replyMarkup: GetButtons());
            }
            catch (Exception e)
            {
                Logger.Error(CommandSender.ChatID + e.Message);
            }
           
                
        }
        private IReplyMarkup GetButtons()
        {
            string[] groups = { "ФМ", "КТ", "ТО" };
            List<List<InlineKeyboardButton>> buttons = CreateButtonsMurkup(groups, "tablenameIs", 3);
            return new InlineKeyboardMarkup(buttons);
        }
        private List<List<InlineKeyboardButton>> CreateButtonsMurkup(string[] buttonNames, string buttonCallback, int columns)
        {            
            int layers = (int)Math.Ceiling((decimal)buttonNames.Length / (decimal)columns);
            int lastLayerLength = buttonNames.Length - (layers - 1) * columns;

            List<List<InlineKeyboardButton>> buttons = new List<List<InlineKeyboardButton>>();

            for (int layerID = 0; layerID < layers; layerID++)
            {
                List<InlineKeyboardButton> layer = new List<InlineKeyboardButton>();
                int buttonsInLayer = columns;

                if (layerID == layers - 1) 
                {
                    buttonsInLayer = lastLayerLength; 
                }
                for (int columnID = 0; columnID < buttonsInLayer; columnID++)
                {
                    string buttonText = buttonNames[layerID * columns + columnID];
                    layer.Add(new InlineKeyboardButton{ 
                        Text = buttonText, 
                        CallbackData = buttonCallback + " " + buttonText
                    });
                }
                buttons.Add(layer);
            }
            return buttons;
        }
    }
}
