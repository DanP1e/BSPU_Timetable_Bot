using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using HtmlAgilityPack;
using System.Linq;
using System.Net;
using TimetableBot.Commands;
using TimetableBot.Message;
using TimetableBot.DATA.Interfaces;
using TimetableBot.DATA;
using Telegram.Bot.Types.Enums;
namespace TimetableBot
{
    class Program
    {

        public static Action OnUpdate;

        public static string[] GroupTags = { "ФМ", "КТ", "ТО" };
        public static BSPUParser BSPUParser { get; private set; }
        public static TelegramBotClient TelegramBotClient { get; private set; }
        public static TelegramInputHandler TelegramInputHandler { get; private set; }
        public static IDataTable<ITelegramUser> UsersDataTable { get; private set; }
        public static string ActualTimetableLink { get; private set; }
        public static string ProgramPath { get; private set;}

        public const string SheduleName = "timetable.docx";
        private const string token = "1658114458:AAFVjC8I4YuB3QP2zBmoR-p1vZNoDHhvRsU";
        
        static void Main(string[] args)
        {
            BSPUParser = new BSPUParser();
            ProgramPath = Environment.CurrentDirectory += "\\";
            UsersDataTable = new UsersTable("TelegramUsers");
            TelegramInputHandler = new TelegramInputHandler(UsersDataTable);

            TelegramBotClient = new TelegramBotClient(token);
            Command.Initialize();
            TelegramBotClient.OnUpdate += TelegramInputHandler.HandleUpdateEvent;

            TelegramBotClient.StartReceiving(new UpdateType[] { UpdateType.Message, UpdateType.CallbackQuery });

            Logger.Success("Bot online!");

            StartProgramCycle();

            while (true)
            {
                Console.ReadLine();
            }

        }
        
        private static async void StartProgramCycle()
        {
            while (true)
            {
                await Task.Delay(5000);

                OnUpdate?.Invoke();
            }
        }
        
    }
}
