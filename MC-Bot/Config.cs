using Bot.Classes;
using Bot.Services;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace Bot
{
    public class _Config
    {
        public static string BotName = "Minecraft";
        public static string Prefix = "mc/";
        public static string DevPrefix = "tmc/";
        public static string ClientID = "";
        public static string BotPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/MC-Bot/";
        public static string BlacklistPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Blacklist/";
        public static bool Ready = false;
        public static bool DevMode = true;
        public static Class Tokens = new Class();
        public static List<_Guild> MCGuilds = new List<_Guild>();
        public static List<_Item> MCItems = new List<_Item>();
        public class Class
        {
            public string Discord = "";
        }
        public static async Task ConfigureServices(IServiceProvider Services, DiscordSocketClient Client, CommandService Commands)
        {
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly());
            Services = new ServiceCollection()
                .AddSingleton(Client)
                .AddSingleton(Commands)
                .AddSingleton(new GuildCheck(Client))
                .AddSingleton<CommandHandler>(new CommandHandler(Client, Commands)).BuildServiceProvider();
            
            var commandHandler = Services.GetService<CommandHandler>();
            commandHandler.Setup(Services);
        }

        #region Functions
        public static void CreateTemplate()
        {
            Class NewConfig = new Class();
            using (StreamWriter file = File.CreateText(BotPath + "Config-Example" + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, NewConfig);

            }
        }
        public static void LoadFile()
        {
            using (StreamReader reader = new StreamReader(BotPath + "Config.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                Tokens = (Class)serializer.Deserialize(reader, typeof(Class));
            }
        }
        #endregion
    }
}

