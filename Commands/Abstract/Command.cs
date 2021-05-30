using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TimetableBot.DATA.Interfaces;

namespace TimetableBot.Commands
{
    public abstract class Command
    {
        public ITelegramUser CommandSender { get; private set; }

        protected Command(ITelegramUser commandSender)
        {
            this.CommandSender = commandSender;
        }
       
        public abstract string Identifier { get; }
        public abstract void Action(params object[] args);


        private static Dictionary<Type, string> CommandTypeToIdentifier = new Dictionary<Type, string>();
        private static Dictionary<string, Type> IdentifierToCommandType = new Dictionary<string, Type>();

        public static string GetIdentifier(Type commandType)
        {
            string identifier;
            if (CommandTypeToIdentifier.TryGetValue(commandType, out identifier))
            {
                return identifier;
            }
            else 
            {
                throw new ArgumentException(nameof(commandType), "This type does not inherit from the Command class!");
            }
            
        }
        public static Type GetCommandType(string identifier)
        {
            Type commandType;
            if (IdentifierToCommandType.TryGetValue(identifier, out commandType))
            {
                return commandType;
            }
            else
            {
                throw new ArgumentException(nameof(identifier), "Unknown identifier!");
            }
        }
        public static void Initialize()
        {
            CommandTypeToIdentifier.Clear();
            IdentifierToCommandType.Clear();

            Type ourtype = typeof(Command);
            IEnumerable<Type> instances = Assembly.GetAssembly(ourtype).GetTypes().Where(type => type.IsSubclassOf(ourtype));
            Command[] commands = new Command[instances.Count()];

            for (int comId = 0; comId < commands.Length; comId++)
            {
                commands[comId] = (Command)Activator.CreateInstance(instances.ElementAt(comId), new TelegramUser("initializator", 0));
                CommandTypeToIdentifier.Add(commands[comId].GetType(), commands[comId].Identifier);
                IdentifierToCommandType.Add(commands[comId].Identifier, commands[comId].GetType());
            }
        }
        
    }
}
