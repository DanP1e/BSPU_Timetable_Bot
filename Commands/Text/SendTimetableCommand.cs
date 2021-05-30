using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;
using TimetableBot.DATA.Interfaces;
using TimetableBot.Parser;

namespace TimetableBot.Commands
{
    public class SendTimetableCommand : Command
    {
        private HtmlNode actualTimeTable;
        public SendTimetableCommand(ITelegramUser commandSender) : base(commandSender)
        {

        }

        public override string Identifier => "/gettf";

        public async override void Action(params object[] args)
        {
            if (args.Length < 2)
            {
                if (args.Length == 1)
                {
                    Command com = new TimetableSelectCommand(CommandSender);
                    com.Action();
                    return;
                }
                Logger.Error($"{nameof(SendTimetableCommand)} Action function get incorrect arguments");
                SendArgumentExсeptionToUser();
            }

            actualTimeTable = Program.BSPUParser.GetActualTimetable((string)args[1]);
            string docPath = "";
            string? inner = actualTimeTable?.InnerText;
            if (inner == null)
            {
                SendExeptionMessageToUser(CommandSender, $"Увы расписания с группой {(string)args[1]} не найдено");
                return;
            }
            docPath = $"{Program.ProgramPath}{inner}_{Program.SheduleName}";
            
            FileDownloader ttDownloader = new FileDownloader(docPath, Program.BSPUParser.GetTimetableLink(actualTimeTable));

            try
            {
                ttDownloader.Download();
            }
            catch 
            {
                try
                {
                    SendExeptionMessageToUser(CommandSender, $"Увы {(string)args[1]} специальности не найдено.");
                }
                catch (Exception e)
                {
                    Logger.Error(CommandSender.ChatID + e.Message);
                }
                return;
            }

            SendFileToChat(CommandSender.ChatID, docPath, $"{actualTimeTable.InnerText} { Program.SheduleName}");           
        }
        
        private async void SendFileToChat(long chatId, string docPath, string loadedFileName) 
        {
            using (var stream = File.Open(docPath, FileMode.Open))
            {
                InputOnlineFile iof = new InputOnlineFile(stream);
                iof.FileName = loadedFileName;

                var send = await Program.TelegramBotClient.SendDocumentAsync(
                    chatId,
                    iof,
                    $"Расписание {actualTimeTable.InnerText}"
                    );
            }
            File.Delete(docPath);
        }
        private async void SendArgumentExсeptionToUser()
        {
            try
            {
                await Program.TelegramBotClient.SendTextMessageAsync(
                  CommandSender.ChatID,
                  $"Вы ввели невенрные аргументы для команды: {Identifier}.\n " +
                  $"Правильный формат: {Identifier} <группа>"
                  );
            }
            catch (Exception e)
            {
                Logger.Error(CommandSender.ChatID + e.Message);
            }
           
            return;
        }
        private async void SendExeptionMessageToUser(ITelegramUser lastUser, string message) 
        {
            await Program.TelegramBotClient.SendTextMessageAsync(lastUser.ChatID, message);
        }
    }
}
