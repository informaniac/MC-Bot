using Bot.Utils;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Timers;
namespace Bot
{
    #region Logger
    public class _Log
    {
        public static void Bot(string Message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[{_Config.BotName}] {Message}");
        }
        public static void Custom(string Message, ConsoleColor Color = ConsoleColor.White)
        {
            if (Color != ConsoleColor.White)
            {
                Console.ForegroundColor = Color;
            }
            Console.WriteLine(Message);
        }
        public static void Blacklist(string Message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[Blacklist] {Message}");
        }
        public static void Error(string Message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Error] {Message}");
        }
        public static void GuildJoined(string Message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"[Joined] {Message}");
        }
        public static void GuildLeft(string Message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"[Left] {Message}");
        }
        public static void Warning(string Message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[{_Config.BotName}] {Message}");
        }
        public static void Command(string Message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[Command] {Message}");
        }
    }
    #endregion

    public class _Bot
    {
        #region ConsoleFix
        private class DisableConsoleQuickEdit
        {
            const uint ENABLE_QUICK_EDIT = 0x0040;
            const int STD_INPUT_HANDLE = -10;
            [DllImport("kernel32.dll", SetLastError = true)]
            static extern IntPtr GetStdHandle(int nStdHandle);
            [DllImport("kernel32.dll")]
            static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);
            [DllImport("kernel32.dll")]
            static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
            internal static bool Go()
            {
                IntPtr consoleHandle = GetStdHandle(STD_INPUT_HANDLE);
                uint consoleMode;
                if (!GetConsoleMode(consoleHandle, out consoleMode))
                {
                    return false;
                }
                consoleMode &= ~ENABLE_QUICK_EDIT;
                if (!SetConsoleMode(consoleHandle, consoleMode))
                {
                    return false;
                }
                return true;
            }
        }
        #endregion

        public static DiscordSocketClient _Client;
        public static IServiceProvider _Services;
        public static CommandService _Commands;
        public static Dictionary<ulong, IGuildUser> BotCache = new Dictionary<ulong, IGuildUser>();
        private static string Status = "";
        public static string GetStatus()
        {
            if (Status == "")
            {
                return $"{_Config.Prefix}help [{_Client.Guilds.Count}] www.blaze.ml";
            }
            else
            {
                return Status;
            }
        }
        public static void SetStatus(string Message = "")
        {
            if (Message == "")
            {
                Status = "";
                _Client.SetGameAsync($"{_Config.Prefix}help [{_Client.Guilds.Count}] www.blaze.ml").GetAwaiter();
            }
            else
            {
                Status = Message;
                _Client.SetGameAsync(Message).GetAwaiter();
            }
        }
        public static void Main()
        {
            DisableConsoleQuickEdit.Go();
            Console.Title = _Config.BotName;
            Console.ForegroundColor = ConsoleColor.White;
            if (File.Exists($"{_Config.BotPath}LIVE.txt"))
            {
                _Log.Bot("Loading in LIVE mode");
                _Config.DevMode = false;
            }
            else
            {
                Console.Title = $"[DevMode] {_Config.BotName}";
                _Log.Bot("Loading in DEV mode");
            }
            if (!Directory.Exists(_Config.BotPath))
            {
                Directory.CreateDirectory(_Config.BotPath);
            }
            if (!Directory.Exists(_Config.BlacklistPath))
            {
                Directory.CreateDirectory(_Config.BlacklistPath);
            }
            _Config.CreateTemplate();
            if (!File.Exists($"{_Config.BotPath}Config.json"))
            {
                _Log.Error("Config file not found");
                while (true)
                { }
            }
            _Config.LoadFile();
            if (_Config.Tokens.Discord == "")
            {
                _Log.Error("Discord token not set");
                while (true)
                { }
            }
            Blacklist.Load();
            _Client = new DiscordSocketClient(new DiscordSocketConfig { AlwaysDownloadUsers = true });
            _Commands = new CommandService(new CommandServiceConfig()
            {
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Sync,
            });
            new _Bot().RunBot().GetAwaiter().GetResult();
        }
        public class Blacklist
        {
            #region Blacklist
            public static List<Class> List = new List<Class>();
            public class Class
            {
                public string GuildName = "";
                public ulong GuildID = 0;
                public string Reason = "";
                public string UsersToBots = "";
            }
            public static void Load()
            {
                foreach (var File in Directory.GetFiles(_Config.BlacklistPath))
                {
                    using (StreamReader reader = new StreamReader(File))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        List.Add((Class)serializer.Deserialize(reader, typeof(Class)));
                    }
                }
                Timer Timer = new Timer()
                {
                    Interval = 300000
                };
                Timer.Elapsed += UpdateBlacklist;
                Timer.Start();
            }
            public static void Add(string GuildName = "", ulong ID = 0, string Reason = "", string UsersBots = "")
            {
                Class NewBlacklist = new Class()
                {
                    GuildID = ID,
                    Reason = Reason,
                    GuildName = GuildName,
                    UsersToBots = UsersBots
                };
                List.Add(NewBlacklist);
                using (StreamWriter file = File.CreateText(_Config.BlacklistPath + $"{ID.ToString()}.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, NewBlacklist);
                }
            }
            public static void Remove(ulong ID)
            {
                if (List.Exists(x => x.GuildID == ID))
                {
                    List.RemoveAll(x => x.GuildID == ID);
                }
                if (File.Exists(_Config.BlacklistPath + $"{ID.ToString()}.json"))
                {
                    File.Delete(_Config.BlacklistPath + $"{ID.ToString()}.json");
                }
            }
            public static void UpdateBlacklist(object sender, ElapsedEventArgs e)
            {
                List<Class> BlacklistCache = new List<Class>();
                foreach (var File in Directory.GetFiles(_Config.BlacklistPath))
                {
                    string FileName = File.Replace(_Config.BlacklistPath, "").Replace(".json", "");
                    using (StreamReader reader = new StreamReader(File))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        BlacklistCache.Add((Class)serializer.Deserialize(reader, typeof(Class)));
                    }

                }
                List.Clear();
                List = BlacklistCache;
            }
            #endregion
        }


        public async Task RunBot()
        {
            _Client.Connected += () =>
            {
                _Log.Bot("CONNECTED!");
                if (_Config.DevMode == true)
                {
                    Console.Title = $"[DevMode] {_Config.BotName} - Online";
                }
                else
                {
                    Console.Title = $"{_Config.BotName} - Online";
                }
                return Task.CompletedTask;
            };

            _Client.Disconnected += (e) =>
            {
                _Log.Bot("DISCONNECTED!");
                if (_Config.DevMode == true)
                {
                    Console.Title = $"[DevMode] {_Config.BotName} - Offline";
                }
                else
                {
                    Console.Title = $"{_Config.BotName} - Offline";
                }
                return Task.CompletedTask;
            };

            _Client.Ready += () =>
            {
                _Log.Custom($"Ready in {_Client.Guilds.Count} guilds", ConsoleColor.Green);
                _Config.Ready = true;
                return Task.CompletedTask;
            };

            _Client.JoinedGuild += (g) =>
            {
                BotCache.Add(g.Id, g.CurrentUser);
                if (_Config.DevMode == false)
                {
                    if (g.Id == 110373943822540800 || g.Id == 264445053596991498)
                    {
                    }
                    else
                    {
                        if (g.Owner == null)
                        {
                            _Log.Warning($"Null owner > {g.Name}");
                            g.DefaultChannel.SendMessageAsync("This guild has no owner :( i have to leave. If this is an issue contact xXBuilderBXx#9113").GetAwaiter();
                            g.LeaveAsync().GetAwaiter();
                            return Task.CompletedTask;
                        }

                        int Users = g.Users.Where(x => !x.IsBot).Count();
                        int Bots = g.Users.Where(x => x.IsBot).Count();
                        if (_Bot.Blacklist.List.Exists(x => x.GuildID == g.Id))
                        {
                            _Bot.Blacklist.Class BlacklistItem = _Bot.Blacklist.List.Find(x => x.GuildID == g.Id);
                            _Log.Blacklist($"Removed {g.Name} - {g.Id}" + Environment.NewLine + $"    Reason: {BlacklistItem.Reason}");
                            g.DefaultChannel.SendMessageAsync($"Removed guild > {BlacklistItem.Reason}").ConfigureAwait(false).GetAwaiter();
                            g.LeaveAsync().ConfigureAwait(false).GetAwaiter();
                            return Task.CompletedTask;
                        }
                        else
                        {

                            if (Bots * 100 / g.Users.Count() > 85)
                            {
                                _Log.Blacklist($"Removed {g.Name} - {g.Id}" + Environment.NewLine + $"    Reason: Bot collection guild");
                                g.DefaultChannel.SendMessageAsync($"Removed guild > Bot collection guild").ConfigureAwait(false).GetAwaiter();
                                _Bot.Blacklist.Add(g.Name, g.Id, "Bot collection guild", $"{Users}/{Bots}");
                                g.LeaveAsync().ConfigureAwait(false).GetAwaiter();
                                return Task.CompletedTask;
                            }
                        }
                    }
                }

                _Log.GuildJoined($"[{BotCache.Count()}] {g.Name} - {g.Owner.Username}#{g.Owner.Discriminator} - {g.Users.Where(x => !x.IsBot).Count()}/{g.Users.Where(x => x.IsBot).Count()} Users/Bots");
                return Task.CompletedTask;
            };

            _Client.LeftGuild += (g) =>
            {
                _Log.GuildLeft($"{g.Name}");
                if (BotCache.Keys.Contains(g.Id))
                {
                    BotCache.Remove(g.Id);
                }
                return Task.CompletedTask;
            };

            _Client.GuildAvailable += (g) =>
            {
                if (_Config.DevMode == false)
                {
                    if (g.Id == 110373943822540800 || g.Id == 264445053596991498)
                    { }
                    else
                    {
                        if (g.Owner == null)
                        {
                            _Log.Warning($"Null owner > {g.Name}");
                            g.DefaultChannel.SendMessageAsync("This guild has no owner :( i have to leave. If this is an issue contact xXBuilderBXx#9113").GetAwaiter();
                            g.LeaveAsync().GetAwaiter();
                            return Task.CompletedTask;
                        }
                        int Users = g.Users.Where(x => !x.IsBot).Count();
                        int Bots = g.Users.Where(x => x.IsBot).Count();
                        if (_Bot.Blacklist.List.Exists(x => x.GuildID == g.Id))
                        {
                            _Bot.Blacklist.Class BlacklistItem = _Bot.Blacklist.List.Find(x => x.GuildID == g.Id);
                            _Log.Blacklist($"Removed {g.Name} - {g.Id}" + Environment.NewLine + $"    Reason: {BlacklistItem.Reason}");
                            g.DefaultChannel.SendMessageAsync($"Removed guild > {BlacklistItem.Reason}").ConfigureAwait(false).GetAwaiter();
                            g.LeaveAsync().ConfigureAwait(false).GetAwaiter();
                            return Task.CompletedTask;
                        }
                        else
                        {

                            if (Bots * 100 / g.Users.Count() > 85)
                            {
                                _Log.Blacklist($"Removed {g.Name} - {g.Id}" + Environment.NewLine + $"    Reason: Bot collection guild");
                                g.DefaultChannel.SendMessageAsync($"Removed guild > Bot collection guild").ConfigureAwait(false).GetAwaiter();
                                _Bot.Blacklist.Add(g.Name, g.Id, "Bot collection guild", $"{Users}/{Bots}");
                                return Task.CompletedTask;
                            }
                        }
                    }
                }
                if (!BotCache.Keys.Contains(g.Id))
                {
                    BotCache.Add(g.Id, g.CurrentUser);
                }
                return Task.CompletedTask;
            };

            _Client.GuildUnavailable += (g) =>
            {
                if (BotCache.Keys.Contains(g.Id))
                {
                    BotCache.Remove(g.Id);
                }
                return Task.CompletedTask;
            };

            _Client.JoinedGuild += async (g) =>
            {
                if (!_Config.DevMode)
                {
                    if (!_Bot.Blacklist.List.Exists(x => x.GuildID == g.Id))
                    {
                        await _Client.SetGameAsync(GetStatus());
                    }
                }
            };

            _Client.LeftGuild += async (g) =>
            {
                if (!_Config.DevMode)
                {
                    if (!_Bot.Blacklist.List.Exists(x => x.GuildID == g.Id))
                    {
                        await _Client.SetGameAsync(GetStatus());
                    }
                }
            };

            _Client.Ready += async () =>
            {
                if (!_Config.DevMode)
                {
                    await _Client.SetGameAsync(GetStatus());
                }
            };

            await _Config.ConfigureServices(_Services, _Client, _Commands);
            await _Client.LoginAsync(TokenType.Bot, _Config.Tokens.Discord);
            await _Client.StartAsync();

            await Task.Delay(-1);
        }
    }

    #region CommandService
    public class CommandHandler
    {
        private readonly DiscordSocketClient _Client;
        private readonly CommandService _Commands;
        private IServiceProvider _Services;
        public CommandHandler(DiscordSocketClient Client, CommandService Commands)
        {
            _Client = Client;
            _Commands = Commands;
            StartHandle();
        }
        public void Setup(IServiceProvider services)
        {
            _Services = services;
        }
        public Task StartHandle()
        {

            _Client.MessageReceived += (msg) => { var _ = Task.Run(() => RunCommand(msg)); return Task.CompletedTask; };
            return Task.CompletedTask;
        }
        public async Task RunCommand(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            if (message.Author.IsBot) return;
            int argPos = 0;
            if (_Config.DevMode == false)
            {
                if (!(message.HasStringPrefix(_Config.Prefix, ref argPos) || message.HasMentionPrefix(_Client.CurrentUser, ref argPos))) return;
                var context = new CommandContext(_Client, message);

                var result = await _Commands.ExecuteAsync(context, argPos, _Services);
                if (result.IsSuccess)
                {
                    if (context.Channel is IPrivateChannel)
                    {
                        _Log.Command($"DM");
                        _Log.Custom($"     {context.Message.Author}: {context.Message.Content}");
                    }
                    else
                    {
                        _Log.Command($"{context.Guild.Name} #{context.Channel.Name}");
                        _Log.Custom($"     {context.Message.Author}: {context.Message.Content}");
                    }
                }
                else
                {
                    if (result.ErrorReason.Contains("Bot requires guild permission") || result.ErrorReason.Contains("User requires guild permission") || result.ErrorReason.Contains("Bot requires channel permission") || result.ErrorReason.Contains("User requires channel permission"))
                    {
                        await context.Channel.SendMessageAsync($"`{result.ErrorReason}`");
                    }
                }
            }
            else
            {
                if (!(message.HasStringPrefix(_Config.DevPrefix, ref argPos))) return;
                var context = new CommandContext(_Client, message);

                var result = await _Commands.ExecuteAsync(context, argPos, _Services);
                if (result.IsSuccess)
                {
                    _Log.Command($"Executed > {context.Message.Content}");
                }
                else
                {
                    _Log.Command($"Error > {context.Message.Content}");
                    _Log.Custom($"     Error: {result.ErrorReason}");
                }
            }
        }
    }
    #endregion
}
namespace Bot.Commands
{
    public class _Utils
    {
        public static int BotPercentage(int AllUsers, int BotUsers)
        {
            if (BotUsers == 0)
            {
                return 0;
            }
            else
            {
                return BotUsers * 100 / AllUsers;
            }
        }
        public static IGuild GetGuild(DiscordSocketClient Client, ulong ID)
        {
            IGuild Guild = null;
            try
            {
                Guild = Client.Guilds.ElementAt((int)ID - 1);
            }
            catch
            {
                Guild = Client.GetGuild(ID);
            }

            return Guild;
        }

        public static ITextChannel GetChannel(DiscordSocketClient Client, List<ITextChannel> Channels, ulong ID)
        {
            ITextChannel Chan = null;
            try
            {
                Chan = Channels.ElementAt((int)ID - 1);
            }
            catch
            {
                Chan = Channels.Where(x => x.Id == ID).First();
            }
            return Chan;
        }
    }
}
namespace Bot.Commands
{
    #region CoreCommands
    public class Core : ModuleBase
    {
        private DiscordSocketClient _Client;
        public Core(DiscordSocketClient Client)
        {
            _Client = Client;
        }

        [Command("ping")]
        public async Task Ping()
        {
            System.Net.NetworkInformation.PingReply PingDiscord = new System.Net.NetworkInformation.Ping().Send("discordapp.com");
            System.Net.NetworkInformation.PingReply PingGoogle = new System.Net.NetworkInformation.Ping().Send("google.com");
            await ReplyAsync($"`PONG > Discord: {PingDiscord.RoundtripTime} ms Google: {PingGoogle.RoundtripTime} ms Gateway: {_Client.Latency} ms`");
        }

        [Command("prefix")]
        public async Task Prefix()
        {
            await ReplyAsync($"My prefix is `{_Config.Prefix}` custom prefix option coming soon");
        }

        [Command("invite")]
        public async Task Invite()
        {
            var embed = new EmbedBuilder()
            {
                Description = $"[Add {_Config.BotName} to your server/guild](https://discordapp.com/oauth2/authorize?&client_id=" + Context.Client.CurrentUser.Id + "&scope=bot&permissions=0)"
            };
            if (Context.Guild == null)
            {
                await Context.Channel.SendMessageAsync("", false, embed);
            }
            else
            {
                _Bot.BotCache.TryGetValue(Context.Guild.Id, out IGuildUser GU);
                if (!GU.GuildPermissions.EmbedLinks)
                {
                    await Context.Channel.SendMessageAsync($"Add {_Config.BotName} to your server/guild" + Environment.NewLine + "https://discordapp.com/oauth2/authorize?&client_id=" + Context.Client.CurrentUser.Id + "&scope=bot&permissions=0");
                }
                else
                {

                    await Context.Channel.SendMessageAsync("", false, embed);
                }
            }
        }

        [Command("website")]
        public async Task Website()
        {
            await ReplyAsync("https://blaze.ml");
        }

        [Command("github")]
        public async Task Github()
        {
            if (_Config.Github == "")
            {
                await ReplyAsync("This bot does not have a github");
                return;
            }
            var embed = new EmbedBuilder()
            {
                Description = $"Please report issues/suggestions to [Github]({_Config.Github})"
            };
            if (Context.Guild == null)
            {
                await Context.Channel.SendMessageAsync("", false, embed);
            }
            else
            {
                _Bot.BotCache.TryGetValue(Context.Guild.Id, out IGuildUser GU);
                if (!GU.GuildPermissions.EmbedLinks)
                {
                    await Context.Channel.SendMessageAsync($"Please report issues/suggestions to {_Config.Github}");
                }
                else
                {

                    await Context.Channel.SendMessageAsync("", false, embed);
                }
            }
        }

        [Group("o")]
        [Alias("owner")]
        public class Owner : ModuleBase
        {
            private CommandService _Commands;
            private DiscordSocketClient _Client;
            public Owner(CommandService Commands, DiscordSocketClient CLient)
            {
                _Client = CLient;
                _Commands = Commands;
            }
            [Command]
            public async Task O()
            {
                if (Context.User.Id != 190590364871032834)
                {
                    await ReplyAsync("The owner is xXBuilderBXx#9113 - 190590364871032834" + Environment.NewLine + $"For more info about the bot do {_Config.Prefix}help");
                }
                else
                {
                    List<string> OwnerCommands = new List<string>();
                    foreach (var CMD in _Commands.Commands.Where(x => x.Module.Name == "o"))
                    {
                        if (CMD.Remarks != null)
                        {
                            try
                            {
                                CMD.Remarks.Trim();
                                OwnerCommands.Add(CMD.Remarks);
                            }
                            catch { }
                        }
                    }
                    await ReplyAsync($"`{string.Join(" | ", OwnerCommands)} | blacklist`").ConfigureAwait(false);
                }
            }

            [Command("invite"), Remarks("invite (GID/NUM)")]
            [RequireOwner]
            public async Task Invite(ulong ID)
            {
                if (ID == 0)
                {
                    ID = Context.Guild.Id;
                }
                IGuild Guild = _Utils.GetGuild(_Client, ID);
                if (Guild == null)
                {
                    await ReplyAsync($"`Cannot find guild {ID}`");
                    return;
                }
                var Invites = await Guild.GetInvitesAsync();
                if (Invites.Count != 0)
                {
                    await ReplyAsync(Invites.First().Code);
                    return;
                }
                IGuildChannel Chan = await Guild.GetDefaultChannelAsync();
                var Invite = await Chan.CreateInviteAsync();
                if (Invite != null)
                {
                    await ReplyAsync(Invite.Code);
                }
                else
                {
                    await ReplyAsync($"`Could not create invite for guild {ID}`");
                    return;
                }
            }

            [Command("info"), Remarks("info (GID/NUM)")]
            [RequireOwner]
            public async Task Oinfo(ulong ID = 0)
            {
                if (ID == 0)
                {
                    ID = Context.Guild.Id;
                }
                IGuild Guild = _Utils.GetGuild(_Client, ID);
                if (Guild == null)
                {
                    await ReplyAsync($"`Cannot find guild {ID}`");
                    return;
                }
                string Owner = "NO OWNER";
                var Users = await Guild.GetUsersAsync();
                IGuildUser ThisOwner = await Guild.GetOwnerAsync().ConfigureAwait(false);
                if (ThisOwner != null)
                {
                    Owner = $"{ThisOwner.Username}#{ThisOwner.Discriminator} - {ThisOwner.Id}";
                }
                var embed = new EmbedBuilder()
                {
                    Author = new EmbedAuthorBuilder()
                    {
                        Name = $"{Guild.Name}",
                        IconUrl = Guild.IconUrl
                    },
                    Description = "```md" + Environment.NewLine + $"<ID {Guild.Id}>" + Environment.NewLine + $"<Owner {Owner}>" + Environment.NewLine + $"<Users {Users.Where(x => !x.IsBot).Count()}> <Bots {Users.Where(x => x.IsBot).Count()}> <BotPercentage {_Utils.BotPercentage(Users.Count(), Users.Where(x => x.IsBot).Count())}>```",
                    Footer = new EmbedFooterBuilder()
                    {
                        Text = $"Created {Guild.CreatedAt.Day}/{Guild.CreatedAt.Month}/{Guild.CreatedAt.Year}"
                    },
                    Color = DiscordUtils.GetRoleColor(Context.Channel as ITextChannel)
                };
                await ReplyAsync("", false, embed);
            }

            [Command("leavehere"), Remarks("leavehere")]
            [RequireOwner]
            public async Task Leavehere()
            {
                await Context.Guild.LeaveAsync();
            }

            [Command("clearcon"), Remarks("clearcon")]
            [RequireOwner]
            public async Task Clear()
            {
                await Context.Message.DeleteAsync().ConfigureAwait(false);
                Console.Clear();
                _Log.Custom("Console cleared", ConsoleColor.Cyan);
                await Context.Channel.SendMessageAsync("`Console cleared`").ConfigureAwait(false);
            }

            [Command("botcol"), Remarks("botcol (*int)")]
            [RequireOwner]
            public async Task Botcol(int Number = 100)
            {
                var Guilds = await Context.Client.GetGuildsAsync().ConfigureAwait(false);
                List<string> GuildList = new List<string>();
                foreach (var Guild in Guilds)
                {
                    if (Guild.Id != 110373943822540800 & Guild.Id != 264445053596991498)
                    {
                        var Users = await Guild.GetUsersAsync();
                        IGuildUser Owner = await Guild.GetOwnerAsync() ?? null;
                        if (Owner != null)
                        {

                            if (Users.Where(x => x.IsBot).Count() * 100 / Users.Count() > 85)
                            {
                                GuildList.Add($"{Guild.Name} ({Guild.Id}) - Owner: {Owner.Username} ({Owner.Id}) - {Users.Where(x => !x.IsBot).Count()}/{Users.Where(x => x.IsBot).Count()}");
                            }
                        }
                        else
                        {
                            GuildList.Add($"{Guild.Name} ({Guild.Id}) - NO OWNER! - {Users.Where(x => !x.IsBot).Count()}/{Users.Where(x => x.IsBot).Count()}");
                        }
                    }

                }
                string AllGuilds = string.Join(Environment.NewLine, GuildList.ToArray());
                IDMChannel DM = await Context.User.GetOrCreateDMChannelAsync().ConfigureAwait(false);
                await DM.SendMessageAsync("```" + Environment.NewLine + AllGuilds + "```");
            }

            [Command("leave"), Remarks("leave (GID/NUM)")]
            [RequireOwner]
            public async Task Leave(ulong ID)
            {
                try
                {
                    await Context.Message.DeleteAsync();
                }
                catch { }
                IGuild Guild = _Utils.GetGuild(_Client, ID);
                if (Guild == null)
                {
                    await ReplyAsync($"`Could not find guild by id {ID}`");
                    return;
                }
                try
                {
                    IGuildUser Owner = await Guild.GetOwnerAsync();
                    await Guild.LeaveAsync();
                    await ReplyAsync($"`Left guild {Guild.Name} - {Guild.Id} | Owned by {Owner.Username}#{Owner.Discriminator}`");
                }
                catch
                {
                    await Guild.LeaveAsync();
                    await ReplyAsync($"`Left guild {Guild.Name} - {Guild.Id}`");
                }
            }

            [Group("blacklist")]
            public class BlacklistGroup : ModuleBase
            {
                private DiscordSocketClient _Client;
                public BlacklistGroup(CommandService Commands, DiscordSocketClient CLient)
                {
                    _Client = CLient;
                }
                [Command]
                [RequireOwner]
                public async Task Blacklist()
                {

                    await ReplyAsync("`Blacklist > add (ID) | reload | remove (ID) | list | info (ID)`");
                }

                [Command("add")]
                [RequireOwner]
                public async Task BlacklistAdd(ulong ID, [Remainder] string Reason = "")
                {
                    if (_Bot.Blacklist.List.Exists(x => x.GuildID == ID))
                    {
                        await Context.Channel.SendMessageAsync($"`{ID} is already in the blacklist`").ConfigureAwait(false);
                    }
                    else
                    {
                        try
                        {
                            IGuild Guild = _Client.GetGuild(ID);
                            var Users = await Guild.GetUsersAsync();
                            _Bot.Blacklist.Add(Guild.Name, ID, Reason, $"{Users.Where(x => !x.IsBot).Count()}/{Users.Where(x => x.IsBot).Count()}");
                            await ReplyAsync($"`Adding {Guild.Name} - {ID} to blacklist`");
                            await Guild.LeaveAsync();
                        }
                        catch
                        {
                            _Bot.Blacklist.Add("", ID, Reason);
                            await ReplyAsync($"`Adding {ID} to blacklist`");
                        }

                    }
                }

                [Command("reload")]
                [RequireOwner]
                public async Task BlacklistReload()
                {
                    List<_Bot.Blacklist.Class> BlacklistCache = new List<_Bot.Blacklist.Class>();
                    foreach (var File in Directory.GetFiles(_Config.BlacklistPath))
                    {
                        using (StreamReader reader = new StreamReader(File))
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            BlacklistCache.Add((_Bot.Blacklist.Class)serializer.Deserialize(reader, typeof(_Bot.Blacklist.Class)));
                        }

                    }
                    _Bot.Blacklist.List.Clear();
                    _Bot.Blacklist.List = BlacklistCache;
                    await ReplyAsync("`Blacklist reloaded`").ConfigureAwait(false);
                }

                [Command("remove")]
                [RequireOwner]
                public async Task BlacklistRemove(ulong ID)
                {
                    _Bot.Blacklist.Class Item = _Bot.Blacklist.List.Find(x => x.GuildID == ID);
                    if (Item == null)
                    {
                        await ReplyAsync("`Could not find guild ID`").ConfigureAwait(false);
                    }
                    else
                    {
                        _Bot.Blacklist.List.Remove(Item);
                        if (File.Exists(_Config.BlacklistPath + $"{Item.GuildID.ToString()}.json"))
                        {
                            File.Delete(_Config.BlacklistPath + $"{Item.GuildID.ToString()}.json");
                        }
                        await ReplyAsync($"`Remove {Item.GuildName} - {Item.GuildID} from blacklist`").ConfigureAwait(false);
                    }
                }

                [Command("list")]
                [RequireOwner]
                public async Task BlacklistList()
                {
                    if (_Bot.Blacklist.List.Count == 0)
                    {
                        await ReplyAsync("`Blacklist is empty`").ConfigureAwait(false);
                    }
                    else
                    {
                        List<string> BlacklistItems = new List<string>();
                        foreach (var i in _Bot.Blacklist.List)
                        {
                            BlacklistItems.Add($"{i.GuildName} - {i.GuildID} ({i.UsersToBots})");
                        }
                        await ReplyAsync("```" + Environment.NewLine + string.Join(Environment.NewLine, BlacklistItems) + "```").ConfigureAwait(false);
                    }
                }

                [Command("info")]
                [RequireOwner]
                public async Task BlacklistInfo(ulong ID)
                {
                    _Bot.Blacklist.Class Item = _Bot.Blacklist.List.Find(x => x.GuildID == ID);
                    if (Item == null)
                    {
                        await ReplyAsync("`Could not find guild ID`").ConfigureAwait(false);
                    }
                    else
                    {
                        await ReplyAsync($"{Item.GuildName} - {Item.GuildID} ({Item.UsersToBots})" + Environment.NewLine + Item.Reason).ConfigureAwait(false);
                    }
                }
            }

            [Command("say"), Remarks("say (Text)")]
            [RequireOwner]
            public async Task Say([Remainder]string Message)
            {
                _Bot.BotCache.TryGetValue(Context.Guild.Id, out IGuildUser GU);
                if (GU.GuildPermissions.ManageMessages)
                {
                    await Context.Message.DeleteAsync();
                }
                if (GU.GuildPermissions.EmbedLinks)
                {
                    var embed = new EmbedBuilder()
                    {
                        Description = Message,
                        Color = DiscordUtils.GetRoleColor(Context.Channel as ITextChannel)
                    };
                    await ReplyAsync("", false, embed);
                }
                else
                {
                    await ReplyAsync(Message);
                }
            }

            [Command("csay"), Remarks("csay (CID) (Text)")]
            [RequireOwner]
            public async Task SayChannel(ulong CID, [Remainder]string Message)
            {
                _Bot.BotCache.TryGetValue(Context.Guild.Id, out IGuildUser GU);
                if (GU.GuildPermissions.ManageMessages)
                {
                    await Context.Message.DeleteAsync();
                }
                try
                {
                    ITextChannel Chan = (ITextChannel)_Utils.GetChannel(_Client, (await Context.Guild.GetTextChannelsAsync()).ToList(), CID);
                    if (Chan == null)
                    {
                        await ReplyAsync($"Could not find channel `{CID}`");
                        return;
                    }
                    if (GU.GuildPermissions.EmbedLinks)
                    {
                        var embed = new EmbedBuilder()
                        {
                            Description = Message,
                            Color = DiscordUtils.GetRoleColor(Chan as ITextChannel)
                        };
                        await Chan.SendMessageAsync("", false, embed);
                    }
                    else
                    {
                        await Chan.SendMessageAsync(Message);
                    }
                }
                catch (Exception ex)
                {
                    await ReplyAsync(ex.Message);
                }
            }

            [Command("gsay"), Remarks("gsay (GID/NUM) (CID) (Text)")]
            [RequireOwner]
            public async Task SayGuild(ulong GID, ulong CID, [Remainder]string Message)
            {
                IGuild Guild = _Utils.GetGuild(_Client, GID);

                if (Guild == null)
                {
                    await ReplyAsync($"Could not find guild `{GID}`");
                    return;
                }
                ITextChannel Chan = (ITextChannel)_Utils.GetChannel(_Client, (await Guild.GetTextChannelsAsync()).ToList(), CID);
                if (Chan == null)
                {
                    await ReplyAsync($"Could not find channel `{CID}`");
                    return;
                }
                _Bot.BotCache.TryGetValue(Guild.Id, out IGuildUser GU);
                if (GU.GuildPermissions.ManageMessages)
                {
                    await Context.Message.DeleteAsync();
                }
                try
                {
                    if (GU.GuildPermissions.EmbedLinks)
                    {
                        var embed = new EmbedBuilder()
                        {
                            Description = Message,
                            Color = DiscordUtils.GetRoleColor(Chan as ITextChannel)
                        };
                        await Chan.SendMessageAsync("", false, embed);
                    }
                    else
                    {
                        await Chan.SendMessageAsync(Message);
                    }
                }
                catch (Exception ex)
                {
                    await ReplyAsync(ex.Message);
                }
            }

            [Command("findguild"), Remarks("findguild (Name)")]
            [RequireOwner]
            public async Task FindGuild([Remainder]string Name)
            {
                IGuild Guild = null;
                try
                {
                    Guild = _Client.Guilds.Where(x => x.Name.ToLower().Contains(Name.ToLower())).First();
                }
                catch
                {
                    await ReplyAsync($"Could not find guild with name `{Name}`");
                    return;
                }

                string Owner = "NO OWNER";
                var Users = await Guild.GetUsersAsync();
                IGuildUser ThisOwner = await Guild.GetOwnerAsync().ConfigureAwait(false) ?? null;
                if (ThisOwner != null)
                {
                    Owner = $"{ThisOwner.Username}#{ThisOwner.Discriminator} - {ThisOwner.Id}";
                }
                var embed = new EmbedBuilder()
                {
                    Author = new EmbedAuthorBuilder()
                    {
                        Name = $"{Guild.Name}",
                        IconUrl = Guild.IconUrl
                    },
                    Description = "```md" + Environment.NewLine + $"<ID {Guild.Id}>" + Environment.NewLine + $"<Owner {Owner}>" + Environment.NewLine + $"<Users {Users.Where(x => !x.IsBot).Count()}> <Bots {Users.Where(x => x.IsBot).Count()}> <BotPercentage {_Utils.BotPercentage(Users.Count(), Users.Where(x => x.IsBot).Count())}>```",
                    Footer = new EmbedFooterBuilder()
                    {
                        Text = $"Created {Guild.CreatedAt.Day}/{Guild.CreatedAt.Month}/{Guild.CreatedAt.Year}"
                    },
                    Color = DiscordUtils.GetRoleColor(Context.Channel as ITextChannel)
                };
                await ReplyAsync("", false, embed).ConfigureAwait(false);

            }

            [Command("channels"), Remarks("channels (GID)")]
            [RequireOwner]
            public async Task Channels(ulong ID = 0)
            {
                if (ID == 0)
                {
                    ID = Context.Guild.Id;
                }
                List<string> Channels = new List<string>();
                IGuild Guild = _Utils.GetGuild(_Client, ID);

                if (Guild == null)
                {
                    await ReplyAsync($"`Could not find guild by id {ID}`");
                    return;
                }
                int Count = 1;
                foreach (var Chan in await Guild.GetTextChannelsAsync())
                {
                    Channels.Add($"<[{Count}]{Chan.Name} {Chan.Id}>");
                    Count++;
                }
                var embed = new EmbedBuilder()
                {
                    Description = "```md" + Environment.NewLine + string.Join(Environment.NewLine, Channels) + Environment.NewLine + "```",
                    Color = DiscordUtils.GetRoleColor(Context.Channel as ITextChannel)
                };
                _Bot.BotCache.TryGetValue(Guild.Id, out IGuildUser GU);
                if (!GU.GuildPermissions.EmbedLinks)
                {
                    await Context.Channel.SendMessageAsync("```md" + Environment.NewLine + string.Join(Environment.NewLine, Channels) + Environment.NewLine + "```");
                }
                else
                {

                    await Context.Channel.SendMessageAsync("", false, embed);
                }

            }
        }
    }
    #endregion
}

namespace Bot.Utils
{
    #region HttpRequest
    public class HttpRequest
    {
        public static string GetString(string Url)
        {
            string Response = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            request.Proxy = null;
            request.Method = WebRequestMethods.Http.Get;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        Response = reader.ReadToEnd();
                    }
                }
            }
            return Response;
        }
        public static dynamic GetJsonObject(string Url, string Auth = "", string OtherHeader = "", string OtherValue = "")
        {
            dynamic Response = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                request.Proxy = null;
                request.Method = WebRequestMethods.Http.Get;
                request.Accept = "application/json";
                if (Auth != "")
                {
                    request.Headers.Add("Authorization", Auth);
                }
                if (OtherHeader != "")
                {
                    request.Headers.Add(OtherHeader, OtherValue);
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream stream = response.GetResponseStream())
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string ResponseText = reader.ReadToEnd();
                            Response = Newtonsoft.Json.Linq.JObject.Parse(ResponseText);
                        }

                    }
                }
            }
            catch { }
            return Response;
        }
        public static dynamic GetJsonArray(string Url, string Auth = "", string OtherHeader = "", string OtherValue = "")
        {
            dynamic Response = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                request.Proxy = null;
                request.Method = WebRequestMethods.Http.Get;
                request.Accept = "application/json";
                if (Auth != "")
                {
                    request.Headers.Add("Authorization", Auth);
                }
                if (OtherHeader != "")
                {
                    request.Headers.Add(OtherHeader, OtherValue);
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream stream = response.GetResponseStream())
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string ResponseText = reader.ReadToEnd();
                            Response = Newtonsoft.Json.Linq.JArray.Parse(ResponseText);
                        }
                    }
                }
            }
            catch { }
            return Response;
        }

    }
    #endregion

    #region DiscordUtils
    public class DiscordUtils
    {
        public static async Task<IGuildUser> StringToUser(IGuild Guild, string Username)
        {
            IGuildUser GuildUser = null;
            if (Username.StartsWith("<@"))
            {
                string RealUsername = Username;
                RealUsername = RealUsername.Replace("<@", "").Replace(">", "");
                if (RealUsername.Contains("!"))
                {
                    RealUsername = RealUsername.Replace("!", "");
                }
                GuildUser = await Guild.GetUserAsync(Convert.ToUInt64(RealUsername));
            }
            else
            {
                GuildUser = await Guild.GetUserAsync(Convert.ToUInt64(Username));
            }
            return GuildUser;
        }

        public static string StringToUserID(string User)
        {
            if (User.StartsWith("("))
            {
                User = User.Replace("(", "");
            }
            if (User.EndsWith(")"))
            {
                User = User.Replace(")", "");
            }
            if (User.StartsWith("<@"))
            {
                User = User.Replace("<@", "").Replace(">", "");
                if (User.Contains("!"))
                {
                    User = User.Replace("!", "");
                }
            }
            return User;
        }

        public static Color GetRoleColor(ITextChannel Channel)
        {
            Color RoleColor = new Discord.Color(30, 0, 200);
            IGuildUser BotUser = null;
            if (Channel != null)
            {
                _Bot.BotCache.TryGetValue(Channel.Guild.Id, out BotUser);
                if (BotUser.GetPermissions(Channel).EmbedLinks)
                {
                    if (BotUser != null)
                    {
                        if (BotUser.RoleIds.Count != 0)
                        {
                            foreach (var Role in BotUser.Guild.Roles.OrderBy(x => x.Position))
                            {
                                if (BotUser.RoleIds.Contains(Role.Id))
                                {
                                    RoleColor = Role.Color;
                                }
                            }
                        }
                    }
                }
            }
            return RoleColor;
        }
    }
    #endregion

    #region OtherUtils
    public class OtherUtils
    {
        public static DateTime UnixToDateTime(long Unix)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(Unix).ToLocalTime();
            return dtDateTime;
        }
    }
    #endregion
}