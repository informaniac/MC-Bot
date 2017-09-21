using Bot.Apis;
using Bot.Classes;
using Discord;
using Discord.Commands;
using MojangSharp.Endpoints;
using MojangSharp.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MojangSharp.Endpoints.Statistics;
using static MojangSharp.Responses.NameHistoryResponse;
using System.Net.NetworkInformation;
using Bot.Functions;
using Bot.Utils;
using System.IO;
using Newtonsoft.Json.Linq;
using Discord.Addons.InteractiveCommands;
using Bot.Services;
using Discord.WebSocket;

namespace Bot.Commands
{
    public class Throw
    {
        public static async Task Data(ICommandContext Context)
        {
            await Context.Channel.SendMessageAsync("Use `mc/help`");
            throw new Exception("");
        }
    }
    public class Main : ModuleBase
    {
        public _Trans.Main _TransMain = new _Trans.Main();

        private CommandService _Commands;
        private DiscordSocketClient _Client;
        public Main(DiscordSocketClient Client, CommandService Commands)
        {
            _Client = Client;
            _Commands = Commands;
        }

        public string NewsText = "";

        [Command("help"), Alias("commands"), RequireBotPermission(GuildPermission.EmbedLinks)]
        public async Task Help(string Action = "")
        {
                _Task.GetGuild(Context.Guild, out _Guild Guild);
                if (Context.Guild != null)
                {
                    IGuildUser GU = await Context.Guild.GetUserAsync(Context.Client.CurrentUser.Id);
                    if (!GU.GuildPermissions.EmbedLinks || !GU.GetPermissions(Context.Channel as ITextChannel).EmbedLinks)
                    {
                        await ReplyAsync("```python" + Environment.NewLine + $"{_TransMain.Error_NoEmbedPerms.Get(Guild)}```");
                        return;
                    }
                }
                if (NewsText == "" && File.Exists(_Config.BotPath + "News.txt"))
                {
                    using (StreamReader reader = new StreamReader(_Config.BotPath + "News.txt"))
                    {
                        NewsText = reader.ReadLine();
                    }
                }
                var embed = new EmbedBuilder()
                {
                    Title = $"Bot News > {NewsText}",
                    Description = "",
                    Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                    Footer = new EmbedFooterBuilder()
                    {
                        Text = _TransMain.Help_FooterHiddenCommands.Get(Guild)
                    }
                };
                embed.AddField(_TransMain.Commands.Get(Guild), "```md" + Environment.NewLine + string.Join(Environment.NewLine, _TransMain.HelpCommands[(int)Guild.Language]) + "```");
                embed.AddField(_TransMain.Links.Get(Guild), $"[MultiMC](https://multimc.org/) {_TransMain.MultiMC.Get(Guild)}" + Environment.NewLine + "[Ftb Legacy](http://ftb.cursecdn.com/FTB2/launcher/FTB_Launcher.exe) | [Technic Launcher](https://www.technicpack.net/download) | [AT Launcher](https://www.atlauncher.com/downloads)");
                if (Action == "update" && Context.User.Id == 190590364871032834)
                {
                    ITextChannel TE = await Context.Guild.GetTextChannelAsync(351033810961301506);
                    IUserMessage Update = await TE.GetMessageAsync(351404116527808512) as IUserMessage;
                    await Update.ModifyAsync(x => { x.Embed = embed.Build(); });
                }
                else
                {
                    await ReplyAsync("", false, embed.Build());
                }
        }

        [Command("setnews"), RequireOwner]
        public async Task News([Remainder]string Text)
        {
            NewsText = Text;
            using (StreamWriter file = File.CreateText(_Config.BotPath + $"News" + ".txt"))
            {
                file.WriteLine(Text);
            }
            await ReplyAsync("News has been set");
        }

        [Command("colors"), Alias("color")]
        public async Task Colors()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            var embed = new EmbedBuilder()
            {
                Title = _TransMain.ColorCodes.Get(Guild),
                ImageUrl = "https://i.imgur.com/IqWbUoT.png"
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("uuid")]
        public async Task Uuid(string Player)
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            UuidAtTimeResponse uuid = new UuidAtTime(Player, DateTime.Now).PerformRequest().Result;
            if (uuid.IsSuccess)
            {
                var embed = new EmbedBuilder()
                {
                    Title = $"UUID | {Player}",
                    Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                    Description = uuid.Uuid.Value
                };
                await ReplyAsync("", false, embed.Build());
            }
            else
            {
                await ReplyAsync(_TransMain.Error_PlayerNotFound.Get(Guild).Replace("{0}", $"`{Player}`"));
            }

        }

        [Command("ping"), Priority(0)]
        public async Task Ping(string IP = "", ushort Port = 25565)
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            if (IP == "" || IP.Contains("("))
            {
                await ReplyAsync($"{_TransMain.Error_EnterIP.Get(Guild)} | `mc/ping my.server.net` | `mc/ping other.server.net:25566` | `mc/ping this.server.net 25567`");
                return;
            }
            switch (IP)
            {
                case "127.0.0.1":
                    await ReplyAsync(_TransMain.Error_IPMain.Get(Guild));
                    return;
                case "192.168.0.1":
                    await ReplyAsync(_TransMain.Error_IPRouter.Get(Guild));
                    return;
                case "0.0.0.0":
                    await ReplyAsync(_TransMain.Error_IPZero.Get(Guild));
                    return;
                case "google.com":
                    await ReplyAsync(_TransMain.Error_IPGoogle.Get(Guild));
                    return;
                case "youtube.com":
                    await ReplyAsync(_TransMain.Error_IPYoutube.Get(Guild));
                    return;
                case "blazeweb.ml":
                    await ReplyAsync(_TransMain.Error_IPMyWeb.Get(Guild));
                    return;
                case "mc.hypixel.net":
                    await ReplyAsync(_TransMain.Error_IPBlocked.Get(Guild));
                    return;
            }
            if (IP.Contains(":"))
            {
                string[] GetPort = IP.Split(':');
                IP = GetPort.First();
                Port = Convert.ToUInt16(GetPort.Last());

            }
            if (Context.Guild != null)
            {
                if (!IP.Contains("."))
                {
                    if (Guild != null)
                    {
                        _Server Server = Guild.Servers.Find(x => x.Tag.ToLower() == IP.ToLower());
                        if (Server != null)
                        {
                            IP = Server.Ip; Port = Server.Port;
                        }
                        else
                        {
                            var ValidEmbed = new EmbedBuilder()
                            {
                                Description = $"<:error:350172479936921611> {_TransMain.Error_IPInvalid.Get(Guild)}",
                                Color = new Color(200, 0, 0)
                            };
                            await ReplyAsync("", false, ValidEmbed.Build());
                            return;
                        }
                    }
                }
            }
            else
            {
                if (!IP.Contains("."))
                {
                    var ValidEmbed = new EmbedBuilder()
                    {
                        Description = $"<:error:350172479936921611> {_TransMain.Error_IPInvalid.Get(Guild)}",
                        Color = new Color(200, 0, 0)
                    };
                    await ReplyAsync("", false, ValidEmbed.Build());
                    return;
                }
            }
            Cooldown Cooldown = null;
            _Config.PingCooldown.TryGetValue(Context.User.Id, out Cooldown);
            if (Cooldown == null)
            {
                _Config.PingCooldown.Add(Context.User.Id, new Cooldown() { Count = 0, Date = DateTime.Now });
                _Config.PingCooldown.TryGetValue(Context.User.Id, out Cooldown);
            }
            if (Cooldown.Count == 3)
            {
                if (DateTime.Now.Hour == Cooldown.Date.Hour)
                {
                    if ((DateTime.Now - Cooldown.Date).TotalMinutes >= 1)
                    {
                        Cooldown.Date = DateTime.Now;
                        Cooldown.Count = 1;
                    }
                    else
                    {
                        await ReplyAsync(_TransMain.Error_Cooldown.Get(Guild));
                        return;
                    }
                }
                else
                {
                    Cooldown.Date = DateTime.Now;
                    Cooldown.Count = 1;
                }
            }
            else
            {
                Cooldown.Date = DateTime.Now;
                Cooldown.Count++;
            }
            var Info = await ReplyAsync($"{_TransMain.Ping_PleaseWait.Get(Guild)} `{IP}`");
            var ErrorEmbed = new EmbedBuilder()
            {
                Description = $"<:error:350172479936921611> {_TransMain.Error_IPInvalid.Get(Guild)}",
                Color = new Color(200, 0, 0)
            };
            await Task.Run(async () =>
            {
                try
                {
                    Ping PingTest = new Ping();
                    PingReply PingReply = PingTest.Send(IP, 3000);
                    if (PingReply.Status != IPStatus.Success)
                    {
                        await Info.DeleteAsync();
                        await ReplyAsync("", false, ErrorEmbed.Build());
                        return;
                    }

                }
                catch (PingException)
                {
                    await Info.DeleteAsync();
                    await ReplyAsync("", false, ErrorEmbed.Build());
                    return;
                }
                MineStat Ping = new MineStat(IP, Port);
                if (Ping.ServerUp)
                {
                    if (Ping.CurrentPlayers == "")
                    {
                        var embed = new EmbedBuilder()
                        {
                            Title = $"[{Ping.Version}] {IP}:{Port}",
                            Description = _TransMain.Ping_ServerLoading.Get(Guild),
                            Color = new Color(0, 191, 255),
                            Footer = new EmbedFooterBuilder()
                            {
                                Text = "This command will be improved alot soon"
                            }
                        };
                        await Info.DeleteAsync();
                        await ReplyAsync("", false, embed.Build());
                    }
                    else
                    {
                        var embed = new EmbedBuilder()
                        {
                            Title = $"[{Ping.Version}] {IP}:{Port}",
                            Color = new Color(0, 200, 0),
                            Description = $"{_TransMain.Players.Get(Guild)} {Ping.CurrentPlayers}/{Ping.MaximumPlayers}",
                            Footer = new EmbedFooterBuilder()
                            {
                                Text = Ping.Motd.Replace("§a", "").Replace("§1", "").Replace("§2", "").Replace("§3", "").Replace("§4", "").Replace("§5", "").Replace("§6", "").Replace("§7", "").Replace("§8", "").Replace("§9", "").Replace("§b", "").Replace("§c", "").Replace("§d", "").Replace("§e", "").Replace("§f", "").Replace("§l", "").Replace("&k", "").Replace("&r", "")
                            }
                        };
                        if (Ping.Version.Contains("BungeeCord"))
                        {
                            //embed.WithDescription(embed.Description + Environment.NewLine + _TransMain.BungeeCord[LangInt]);
                        }
                        await Info.DeleteAsync();
                        await ReplyAsync("", false, embed.Build());
                    }
                }
                else
                {
                    var embed = new EmbedBuilder()
                    {
                        Description = $"<:warning:350172481757118478> {_TransMain.Ping_ServerOffline.Get(Guild)}",
                        Color = new Color(255, 165, 0)
                    };
                    await Info.DeleteAsync();
                    await ReplyAsync("", false, embed.Build());
                }
            });
        }

        [Command("list"),Alias("servers"), RequireContext(ContextType.Guild)]
        public async Task List()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            if (Guild.Servers.Count == 0)
            {
                await ReplyAsync($"{_TransMain.List_NoServers.Get(Guild)} :(" + Environment.NewLine + $"{_TransMain.List_GuildAdmin.Get(Guild)} `mc/admin`");
                return;
            }
            List<string> Servers = new List<string>();
            foreach (var i in Guild.Servers)
            {
                Servers.Add($"<{i.Tag} {i.Ip} = {i.Name}>");
            }
            string Name = Context.Guild.Name;
            if (Guild.CommunityName != "")
            {
                Name = Guild.CommunityName;
            }
            var embed = new EmbedBuilder()
            {
                Author = new EmbedAuthorBuilder()
                {
                    Name = $"{Name} {_TransMain.Servers.Get(Guild)}",
                    IconUrl = Context.Guild.IconUrl
                },
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                Description = "```md" + Environment.NewLine + string.Join(Environment.NewLine, Servers) + "```"
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("info")]
        public async Task Info()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            StatisticsResponse stats = await new Statistics(Item.MinecraftAccountsSold).PerformRequest();
            if (stats.IsSuccess)
            {
                var embed = new EmbedBuilder()
                {
                    Author = new EmbedAuthorBuilder()
                    {
                        Name = _TransMain.Info_MCSales.Get(Guild),
                        Url = _TransMain.Info_MCSalesUrl.Get(Guild)
                    },
                    Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                    Description = $"Total: {stats.Total}" + Environment.NewLine + $"24 Hours: {stats.Last24h}" + Environment.NewLine + $"Average Per Second: {stats.SaleVelocity}",
                    Footer = new EmbedFooterBuilder()
                    {
                        Text = _TransMain.Info_SalesError.Get(Guild)
                    }
                };
                await ReplyAsync("", false, embed.Build());
            }
            else
            {
                await ReplyAsync(_TransMain.Error_Api.Get(Guild));
            }
        }

        [Command("skin")]
        public async Task SkinArg(string Arg = "", string Player = "")
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            if (Arg == "")
            {
                await Context.Channel.SendMessageAsync($"mc/skin (Arg) {_TransMain.Skin_Args.Get(Guild)} | `mc/skin Notch` or `mc/skin cube Notch`");
                return;
            }
            if (Player == "")
            {
                Player = Arg;
                Arg = "full";
            }
            UuidAtTimeResponse uuid = new UuidAtTime(Player, DateTime.Now).PerformRequest().Result;
            if (!uuid.IsSuccess)
            {
                await ReplyAsync(_TransMain.Error_PlayerNotFound.Get(Guild).Replace("{0}", $"`{Player}`"));
                return;
            }
            string Url = "";
            switch (Arg.ToLower())
            {
                case "head":
                    Url = "https://visage.surgeplay.com/face/100/" + Player + ".png";
                    break;
                case "cube":
                    Url = "https://visage.surgeplay.com/head/100/" + Player + ".png";
                    break;
                case "full":
                    Url = "https://visage.surgeplay.com/full/200/" + Player + ".png";
                    break;
                case "steal":
                    Url = "steal";
                    await ReplyAsync($"{Context.User.Username} {_TransMain.Skin_Stole.Get(Guild)} :o <https://minotar.net/download/" + Player + ">");
                    return;
                default:
                    await Context.Channel.SendMessageAsync($"{_TransMain.Error_UnknownArg.Get(Guild)} mc/skin");
                    return;
            }
            var embed = new EmbedBuilder()
            {
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                ImageUrl = Url
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("name"), Alias("names")]
        public async Task Names(string Player = "")
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            if (Player == "")
            {
                await Context.Channel.SendMessageAsync($"mc/names ({_TransMain.Player.Get(Guild)}) | `mc/names Notch`");
                return;
            }
            UuidAtTimeResponse uuid = new UuidAtTime(Player, DateTime.Now).PerformRequest().Result;
            if (uuid.IsSuccess)
            {
                NameHistoryResponse names = new NameHistory(uuid.Uuid.Value).PerformRequest().Result;
                if (names.IsSuccess)
                {
                    List<string> Names = new List<string>();
                    if (names.NameHistory.Count == 1)
                    {
                        await ReplyAsync(_TransMain.Name_OneOnly.Get(Guild).Replace("{0}", $"`{Player}`"));
                        return;
                    }
                    foreach (NameHistoryEntry entry in names.NameHistory)
                    {
                        if (entry.ChangedToAt.HasValue)
                        {
                            Names.Add($"[ {entry.Name} ][ {entry.ChangedToAt.Value.Day}/{entry.ChangedToAt.Value.Month}/{entry.ChangedToAt.Value.Year} ]");
                        }
                        else
                        {
                            Names.Add($"[ {entry.Name} ]( {_TransMain.First.Get(Guild)} )");
                        }
                    }

                    await ReplyAsync("```" + Environment.NewLine + string.Join(Environment.NewLine, Names) + "```");
                }
                else
                {
                    await ReplyAsync(_TransMain.Error_Api.Get(Guild));
                }
            }
            else
            {
                await ReplyAsync(_TransMain.Name_PlayerNotFoundNames.Get(Guild).Replace("{0}", $"`{Player}`"));
            }
        }

        [Command("status")]
        public async Task Status()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            ApiStatusResponse status = new ApiStatus().PerformRequest().Result;
            if (status.IsSuccess)
            {
                var embed = new EmbedBuilder()
                {
                    Title = _TransMain.Status_Mojang.Get(Guild),
                    Description = $"Mojang: {status.Mojang}" + Environment.NewLine + $"Minecraft.net: {status.Minecraft}" + Environment.NewLine +
                    $"{_TransMain.Status_MojangAccounts.Get(Guild)}: {status.MojangAccounts}" + Environment.NewLine + $"Mojang API: {status.MojangApi}" + Environment.NewLine +
                    $"{_TransMain.Status_MojangAuthServers.Get(Guild)}: {status.MojangAutenticationServers}" + Environment.NewLine + $"{_TransMain.Status_MojangAuthService.Get(Guild)}: {status.MojangAuthenticationService}" + Environment.NewLine +
                    $"{_TransMain.Status_MojangSessions.Get(Guild)}: {status.MojangSessionsServer}" + Environment.NewLine + $"{_TransMain.Status_MinecraftSessions.Get(Guild)}: {status.Sessions}" + Environment.NewLine +
                    $"{_TransMain.Status_MinecraftSkins.Get(Guild)}: {status.Skins}" + Environment.NewLine + $"{_TransMain.Status_MinecraftTextures.Get(Guild)}: {status.Textures}",
                    Color = new Color(0, 200, 0)
                };
                if (status.Mojang != ApiStatusResponse.Status.Available || status.Minecraft != ApiStatusResponse.Status.Available || status.MojangAccounts != ApiStatusResponse.Status.Available || status.MojangApi != ApiStatusResponse.Status.Available || status.MojangAutenticationServers != ApiStatusResponse.Status.Available || status.MojangAuthenticationService != ApiStatusResponse.Status.Available || status.MojangSessionsServer != ApiStatusResponse.Status.Available || status.Sessions != ApiStatusResponse.Status.Available || status.Skins != ApiStatusResponse.Status.Available || status.Textures != ApiStatusResponse.Status.Available)
                {
                    embed.Color = new Color(255, 165, 0);
                }
                await ReplyAsync("", false, embed.Build());
            }
            else
            {
                await ReplyAsync(_TransMain.Error_Api.Get(Guild));
            }
        }

        [Command("music")]
        public async Task Music()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            var embed = new EmbedBuilder()
            {
            };
            embed.AddField("Commands", "```md" + Environment.NewLine + "[ mc/music play (ID) ][ Play a song by ID ]" + Environment.NewLine + "[ mc/music stop ][ Stop the current playing music ]" + Environment.NewLine + "[ mc/music leave ][ Stop current song and leave the voice channel ]```");
            embed.AddField("Playlist", "```md" + Environment.NewLine + "<1 Mine Diamonds>" + Environment.NewLine + "<2 Screw the nether - Yogscast>" + Environment.NewLine + "<3 Mincraft Style - Captain Sparklez>```");
            //await ReplyAsync("", false, embed.Build());
            await ReplyAsync("Coming Soon");
        }

        [Command("playing")]
        public async Task Playing()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            int CountOther = 0;
            int Count1710 = 0;
            int Count18 = 0;
            int Count19 = 0;
            int Count110 = 0;
            int Count111 = 0;
            int Count112 = 0;
            foreach(var i in _Client.Guilds)
            {
                foreach(var u in i.Users)
                {
                    string Game = u.Game.ToString().ToLower();
                    if (Game.Contains("minecraft"))
                    {
                        if (Game.Contains("1.7"))
                        {
                            Count1710++;
                        }
                        else if (Game.Contains("1.8"))
                        {
                            Count18++;
                        }
                        else if (Game.Contains("1.9"))
                        {
                            Count19++;
                        }
                        else if (Game.Contains("1.10"))
                        {
                            Count110++;
                        }
                        else if (Game.Contains("1.11"))
                        {
                            Count111++;
                        }
                        else if (Game.Contains("1.12"))
                        {
                            Count112++;
                        }
                        else if (Game == "minecraft")
                        {
                            CountOther++;
                        }
                        
                    }
                }
            }
            var embed = new EmbedBuilder()
            {
                Title = $"{CountOther + Count1710 + Count18 + Count19 + Count110 + Count111 + Count112} {_TransMain.PeoplePlayingMinecraft.Get(Guild)}",
                Color = _Utils_Discord.GetRoleColor(Context.Channel),
                Description = $"**1.7.10:** {Count1710}" + Environment.NewLine + $"**1.8:** {Count18}" + Environment.NewLine + $"**1.9:** {Count19}" + Environment.NewLine + $"**1.10:** {Count110}" + Environment.NewLine + $"**1.11:** {Count111}" + Environment.NewLine + $"**1.12:** {Count112}"
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("get")]
        public async Task Get([Remainder]string Text = "")
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            if (Text == "")
            {
                await ReplyAsync($"mc/get ({_TransMain.Text.Get(Guild)}) | `mc/get {_TransMain.Hi.Get(Guild)}`");
                return;
            }
            if (Text.Length > 22)
            {
                await ReplyAsync(_TransMain.Get_ErrorLimit.Get(Guild));
                return;
            }
            Random.Org.Random Rng = new Random.Org.Random();
            int Number = Rng.Next(1, 39);
            UriBuilder uriBuilder = null;
            try
            {
                uriBuilder = new UriBuilder("https://www.minecraftskinstealer.com/achievement/a.php?i=" + Number.ToString() + "&h=Achievement+Get!&t=" + Text.Replace(" ", "+"));

                var embed = new EmbedBuilder()
                {
                    Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                    ImageUrl = uriBuilder.Uri.AbsoluteUri
                };
                await ReplyAsync("", false, embed.Build());
            }
            catch
            {
                _Log.Warning(uriBuilder.Uri.OriginalString);
                await ReplyAsync($"`{_TransMain.Error_Api.Get(Guild)}`");
            }
        }

        [Command("skinedit")]
        public async Task Misc()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            var embed = new EmbedBuilder()
            {
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                Description = $"[{_TransMain.OnlineSkinEditor.Get(Guild)}](https://www.minecraftskinstealer.com/skineditor.php)"
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("minime")]
        public async Task Minime(string Player = "")
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            if (Player == "")
            {
                await Context.Channel.SendMessageAsync($"mc/minime ({_TransMain.Player.Get(Guild)}) | `mc/minime Notch`");
                return;
            }
            UuidAtTimeResponse uuid = new UuidAtTime(Player, DateTime.Now).PerformRequest().Result;
            if (uuid.IsSuccess)
            {
                var embed = new EmbedBuilder()
                {
                    Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                    ImageUrl = "https://avatar.yourminecraftservers.com/avatar/trnsp/not_found/tall/128/" + Player + ".png"
                };
                await ReplyAsync("", false, embed.Build());
            }
            else
            {
                await ReplyAsync(_TransMain.Error_PlayerNotFound.Get(Guild).Replace("{0}", $"`{Player}`"));
            }
        }

        [Command("bot")]
        public async Task Bot()
        {
            int FrenchCount = 0;
            int SpanishCount = 0;
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            foreach(var i in _Config.MCGuilds.Where(x => x.Language == _Language.French))
            {
                FrenchCount++;
            }
            foreach (var i in _Config.MCGuilds.Where(x => x.Language == _Language.Spanish))
            {
                SpanishCount++;
            }
            int Count = 0;
            foreach(var i in _Commands.Commands.Where(x => x.Module.Name != "o" && x.Module.Name != "whitelist" && x.Module.Name != "blacklist" && x.Module.Name != "Music"))
            {
                Count++;
            }
            string Uptime = "";
            if (_Config.Uptime.Elapsed.Hours == 0)
            {
                Uptime = $"{_Config.Uptime.Elapsed.Minutes}m {_Config.Uptime.Elapsed.Seconds}s";
            }
            else if (_Config.Uptime.Elapsed.Days == 0)
            {
                Uptime = $"{_Config.Uptime.Elapsed.Hours}h {_Config.Uptime.Elapsed.Minutes}m";
            }
            else
            {
                Uptime = $"{_Config.Uptime.Elapsed.Days}d {_Config.Uptime.Elapsed.Hours}h";
            }
            var embed = new EmbedBuilder()
            {
                Title = "",
                Description = _TransMain.Bot_Desc.Get(Guild),
                Footer = new EmbedFooterBuilder()
                {
                    Text = _TransMain.Bot_Footer.Get(Guild)
                },
                Color = _Utils_Discord.GetRoleColor(Context.Channel)
            };
            embed.AddField($"<:info:350172480645758976> Info", $"**{_TransMain.Bot_Owner.Get(Guild)}**" + Environment.NewLine + "xXBuilderBXx#9113" + Environment.NewLine + "<@190590364871032834>" + Environment.NewLine + Environment.NewLine + $"**{_TransMain.Language.Get(Guild)}:** C#" + Environment.NewLine + $"**{_TransMain.Bot_Lib.Get(Guild)}:** {_Config.Library}", true);
            embed.AddField($"<:stats:350172481157464065> {_TransMain.Stats.Get(Guild)}", $"**{_TransMain.Guilds.Get(Guild)}:** {_Client.Guilds.Count()}" + Environment.NewLine + $"**{_TransMain.Commands.Get(Guild)}:** {Count}" + Environment.NewLine + $"**{_TransMain.Uptime.Get(Guild)}:** {Uptime}" + Environment.NewLine + Environment.NewLine + $"**français:** {FrenchCount}" + Environment.NewLine + $"**Español:** {SpanishCount}", true);
            embed.AddField($"<:world:350172484038950912> {_TransMain.Links.Get(Guild)}", $"[{_TransMain.Bot_Invite.Get(Guild)}](https://discordapp.com/oauth2/authorize?&client_id=" + Context.Client.CurrentUser.Id + "&scope=bot&permissions=0)" + Environment.NewLine + $"[Website](https://blazeweb.ml)" + Environment.NewLine + "[Github](https://github.com/xXBuilderBXx/MC-Bot)" + Environment.NewLine + Environment.NewLine + $"**{_TransMain.Bot_ListGuilds.Get(Guild)}**" + Environment.NewLine + "[Dbots](https://bots.discord.pw/bots/346346285953056770)" + Environment.NewLine + "[DBL](https://discordbots.org/bot/346346285953056770)" + Environment.NewLine + "[Novo](https://novo.archbox.pro/)", true);
            await ReplyAsync("", false, embed.Build());
        }
    }

    public class Hidden : ModuleBase
    {
        _Trans.Hidden _TransHidden = new _Trans.Hidden();

        [Command("classic")]
        public async Task Classic()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            var embed = new EmbedBuilder()
            {
                Title = "Minecraft Classic",
                Description = $"{_TransHidden.MinecraftClassic.Get(Guild)} [Wiki](https://minecraft.gamepedia.com/Classic)",
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                Footer = new EmbedFooterBuilder()
                {
                    Text = _TransHidden.FoundSecretCommand.Get(Guild)
                }
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("storymode")]
        public async Task Storymode()
        {

        }

        [Command("forgecraft")]
        public async Task Forge()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            var embed = new EmbedBuilder()
            {
                Description = _TransHidden.Forgecraft.Get(Guild) + Environment.NewLine +
                $"[{_TransHidden.ForgecraftWallpaper.Get(Guild)}](http://feed-the-beast.wikia.com/wiki/Forgecraft) | [{_TransHidden.Wallpaper.Get(Guild)}](http://www.minecraftforum.net/forums/show-your-creation/fan-art/other-fan-art/1582624-forgecraft-wallpaper)",
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                Footer = new EmbedFooterBuilder()
                {
                    Text = _TransHidden.FoundSecretCommand.Get(Guild)
                }
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("forgecraftwallpaper")]
        public async Task ForgecraftWallpaper()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            var embed = new EmbedBuilder()
            {
                Author = new EmbedAuthorBuilder()
                {
                    Name = _TransHidden.ForgecraftWallpaper.Get(Guild),
                    Url = "http://www.minecraftforum.net/forums/show-your-creation/fan-art/other-fan-art/1582624-forgecraft-wallpaper"
                },
                ImageUrl = "https://dl.dropbox.com/u/25591134/ForgeCraft/ForgeCraft-480x270.jpg",
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                Footer = new EmbedFooterBuilder()
                {
                    Text = _TransHidden.FoundSecretCommand.Get(Guild)
                }
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("bukkit")]
        public async Task Bukkit()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            var embed = new EmbedBuilder()
            {
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                Description = $"{_TransHidden.Bukkit.Get(Guild)} [{_TransHidden.BukkitNews.Get(Guild)}](https://bukkit.org/threads/bukkit-its-time-to-say.305106/)",
                Footer = new EmbedFooterBuilder()
                {
                    Text = _TransHidden.FoundSecretCommand.Get(Guild)
                }
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("direwolf20")]
        public async Task DW()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            var embed = new EmbedBuilder()
            {
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                Description = _TransHidden.Direwolf20.Get(Guild) + Environment.NewLine + "[Youtube](https://www.youtube.com/channel/UC_ViSsVg_3JUDyLS3E2Un5g) | [Twitch](https://www.twitch.tv/direwolf20) | [Twitter](https://twitter.com/Direwolf20) | [Reddit](https://www.reddit.com/r/DW20/) | [Discord](https://discordapp.com/invite/SQ6wjHg)",
                Footer = new EmbedFooterBuilder()
                {
                    Text = _TransHidden.FoundSecretCommand.Get(Guild)
                }
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("herobrine")]
        public async Task Herobrine()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            var embedh = new EmbedBuilder()
            {
                Title = "Herobrine",
                Description = $"{_TransHidden.Herobrine.Get(Guild)} [Wiki](http://minecraftcreepypasta.wikia.com/wiki/Herobrine)",
                ThumbnailUrl = "https://lh3.googleusercontent.com/AQ5S9Xj1z6LBbNis2BdUHM-mQbDrkvbrrlx5rTIxCPc-SwdITwjkJP370gZxNpjG92ND8wImuMuLyKnKi7te7w",
                Footer = new EmbedFooterBuilder()
                {
                    Text = _TransHidden.FoundSecretCommand.Get(Guild)
                },
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel)
            };
            await ReplyAsync("", false, embedh.Build());
        }

        [Command("entity303")]
        public async Task Entity303()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            var embedh = new EmbedBuilder()
            {
                Title = "Entity 303",
                Description = $"{_TransHidden.Entity303.Get(Guild)} [Wiki](http://minecraftcreepypasta.wikia.com/wiki/Entity_303)",
                ThumbnailUrl = "https://vignette3.wikia.nocookie.net/minecraftcreepypasta/images/4/49/Entity_303.png",
                Footer = new EmbedFooterBuilder()
                {
                    Text = _TransHidden.FoundSecretCommand.Get(Guild)
                },
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel)
            };
            await ReplyAsync("", false, embedh.Build());
        }

        [Command("israphel")]
        public async Task IS()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            var embed = new EmbedBuilder()
            {
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                Description = $"{_TransHidden.Israphel.Get(Guild)} [Youtube](https://www.youtube.com/playlist?list=PLF60520313D07F366)",
                Footer = new EmbedFooterBuilder()
                {
                    Text = _TransHidden.FoundSecretCommand.Get(Guild)
                }
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("notch")]
        public async Task Notch()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            var embed = new EmbedBuilder()
            {
                Description = $"{_TransHidden.Notch.Get(Guild)} [Wiki](https://en.wikipedia.org/wiki/Markus_Persson)",
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                Footer = new EmbedFooterBuilder()
                {
                    Text = _TransHidden.FoundSecretCommand.Get(Guild)
                }
            };
            await ReplyAsync("", false, embed.Build());
        }
    }

    public class GuildAdmin : ModuleBase
    {
        public _Trans.Admin _TransAdmin = new _Trans.Admin();

        [Command("admin"), RequireContext(ContextType.Guild)]
        public async Task Admin()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            IGuildUser GU = await Context.Guild.GetUserAsync(Context.User.Id);
            if (!GU.GuildPermissions.Administrator)
            {
                await ReplyAsync($"<:error:350172479936921611> {_TransAdmin.AdminOnly.Get(Guild)}");
                return;
            }

            var embed = new EmbedBuilder()
            {
                Title = _TransAdmin.AdminCommands.Get(Guild),
                Description = "```md" + Environment.NewLine + $"{string.Join(Environment.NewLine, _TransAdmin.Commands[(int)Guild.Language])}" + Environment.NewLine + $"< {_TransAdmin.UseList.Get(Guild)} >```",
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                Footer = new EmbedFooterBuilder()
                { Text = "" }

            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("addserver"), RequireContext(ContextType.Guild)]
        public async Task Addserver(string Tag = "", string IP = "", [Remainder]string Name = "")
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            IGuildUser GU = await Context.Guild.GetUserAsync(Context.User.Id);
            if (!GU.GuildPermissions.Administrator)
            {
                await ReplyAsync($"<:error:350172479936921611> {_TransAdmin.AdminOnly.Get(Guild)}");
                return;
            }

            if (Tag == "" || IP == "" || Name == "")
            {
                await ReplyAsync($"{_TransAdmin.AddServer.Get(Guild)} | `mc/addserver (Tag) (IP) (Name)` | `mc/addserver sf sky.minecraft.net Skyfactory 2`");
                return;
            }

            _Server Server = Guild.Servers.Find(x => x.Tag.ToLower() == Tag.ToLower());
            if (Server != null)
            {
                await ReplyAsync(_TransAdmin.AddServer_Already.Get(Guild));
                return;
            }
            ushort Port = 25565;
            if (IP.Contains(":"))
            {
                string[] GetPort = IP.Split(':');
                IP = GetPort.First();
                Port = Convert.ToUInt16(GetPort.Last());

            }
            _Server NewServer = new _Server()
            {
                Tag = Tag,
                Ip = IP,
                Name = Name,
                Port = Port
            };
            Guild.Servers.Add(NewServer);
            _Task.SaveGuild(Guild);
            await ReplyAsync($"{_TransAdmin.AddServer_Added.Get(Guild)} {Name} {_TransAdmin.AddServer_AddedList.Get(Guild)} | `mc/list`");

        }

        [Command("delserver"), RequireContext(ContextType.Guild)]
        public async Task Delserver(string Tag = "")
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            IGuildUser GU = await Context.Guild.GetUserAsync(Context.User.Id);
            if (!GU.GuildPermissions.Administrator)
            {
                await ReplyAsync($"<:error:350172479936921611> {_TransAdmin.AdminOnly.Get(Guild)}");
                return;
            }

            if (Tag == "")
            {
                await ReplyAsync($"{_TransAdmin.DelServer_Enter.Get(Guild)} | `mc/delserver (Tag)` | `mc/delserver sf`");
                return;
            }
            _Server Server = Guild.Servers.Find(x => x.Tag.ToLower() == Tag.ToLower());
            if (Server == null)
            {
                await ReplyAsync(_TransAdmin.DelServer_None.Get(Guild));
                return;
            }
            Guild.Servers.Remove(Server);
            _Task.SaveGuild(Guild);
            await ReplyAsync($"{_TransAdmin.DelServer_Deleted.Get(Guild)} {Server.Name} {_TransAdmin.DelServer_List.Get(Guild)} | `mc/list`");

        }

        [Command("lang"), RequireContext(ContextType.Guild)]
        public async Task Language(int ID = -1)
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild);
            IGuildUser GU = await Context.Guild.GetUserAsync(Context.User.Id);
            if (Context.User.Id != 190590364871032834)
            {
                if (!GU.GuildPermissions.Administrator)
                {
                    await ReplyAsync($"<:error:350172479936921611> {_TransAdmin.AdminOnly.Get(Guild)}");
                    return;
                }
            }
            if (ID == -1)
            {
                var embed = new EmbedBuilder()
                {
                    Title = _TransAdmin.ChangeLang.Get(Guild),
                    Description = "```md" + Environment.NewLine + "<0 English> mc/lang 0" + Environment.NewLine + "<1 français> mc/lang 1" + Environment.NewLine + "<2 Español> mc/lang 2" + Environment.NewLine + "<3 русский> mc/lang 3```",
                    Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                    Footer = new EmbedFooterBuilder()
                    {
                        Text = $"{_TransAdmin.LanguageTranslate.Get(Guild)} xXBuilderBXx#9113"
                    }
                };
                await ReplyAsync("", false, embed.Build());
            }
            else
            {
                switch (ID)
                {
                    case 0:
                        Guild.Language = _Language.English;
                        await ReplyAsync("Community language set to english");
                        _Task.SaveGuild(Guild);
                        break;
                    case 1:
                        Guild.Language = _Language.French;
                        await ReplyAsync("Langue de la communauté définie en français");
                        _Task.SaveGuild(Guild);
                        break;
                    case 2:
                        Guild.Language = _Language.Spanish;
                        await ReplyAsync("Langue de la communauté définie en espagnol");
                        _Task.SaveGuild(Guild);
                        break;
                    case 3:
                        Guild.Language = _Language.Russian;
                        await ReplyAsync("Язык общения на русском языке");
                        _Task.SaveGuild(Guild);
                        break;
                }
            }
        }
    }

    public class Wiki : ModuleBase
    {
        [Command("wiki")]
        public async Task WikiHelp()
        {
            var embed = new EmbedBuilder()
            {
                Title = "Wiki Commands",
                Description = "[Wiki Website](https://minecraft.gamepedia.com/Minecraft_Wiki)" + Environment.NewLine + "```md" + Environment.NewLine + "[ mc/item (Name/ID) ]( Block/Item info )" + Environment.NewLine + "[ mc/item 35:5 ]( Block/Item info )" + Environment.NewLine + "[ mc/mob (Name) ]( Mob/Entity info )" + Environment.NewLine + "[ mc/potion (Name) ]( Potion recipe )" + Environment.NewLine + "[ mc/enchant (Name) ]( Enchant info )```",
                Color = _Utils_Discord.GetRoleColor(Context.Channel)
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("item"), Remarks("mc/item"), Alias("block", "items", "blocks")]
        public async Task Items(string ID = "0", string Meta = "0")
        {
            if (_Config.MCItems.Count == 0)
            {
                using (StreamReader reader = new StreamReader(_Config.BotPath + "Items.json"))
                {
                    string json = reader.ReadToEnd();
                    JArray a = JArray.Parse(json);
                    foreach (JObject o in a.Children<JObject>())
                    {
                        string GetID = "";
                        string GetMeta = "";
                        string GetName = "";
                        string GetText = "";
                        foreach (JProperty p in o.Properties())
                        {
                            if (p.Name == "type")
                            {
                                GetID = (string)p.Value;
                            }
                            if (p.Name == "meta")
                            {
                                GetMeta = (string)p.Value;
                            }
                            if (p.Name == "name")
                            {
                                GetName = (string)p.Value;
                            }
                            if (p.Name == "text_type")
                            {
                                GetText = (string)p.Value;
                            }
                        }
                        _Item ThisItem = new _Item()
                        {
                            ID = GetID,
                            Meta = GetMeta,
                            Name = GetName,
                            Text = GetText
                        };
                        _Config.MCItems.Add(ThisItem);
                    }
                }
            }
            if (ID.Contains(":"))
            {
                string[] Split = ID.Split(':');
                ID = Split.First();
                Meta = Split.Last();
            }
            _Item Item = _Config.MCItems.Find(x => x.ID == ID & x.Meta == Meta);
            if (Item == null)
            {
                Item = _Config.MCItems.Find(x => x.Name.ToLower() == ID.ToLower());
            }
            if (Item == null)
            {
                await ReplyAsync("Cannot find item ID");
                return;
            }
            var embed = new EmbedBuilder()
            {
                Description = $"{Item.ID}:{Item.Meta} | {Item.Name}",
                ThumbnailUrl = "https://blazeweb.ml/lolis/mcitems/" + ID + "-" + Meta + ".png",
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                Footer = new EmbedFooterBuilder()
                { Text = $"/give {Context.User.Username} {Item.Text}" }
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("mob"), Remarks("mc/mob"), Alias("mobs", "entity", "entitys")]
        public async Task Mob([Remainder]string Name = "")
        {
            if (Name != "")
            {
                if (Name.ToLower() == "herobrine")
                {
                    var embedh = new EmbedBuilder()
                    {
                        Title = "Herobrine",
                        Description = "[Wiki](http://minecraftcreepypasta.wikia.com/wiki/Herobrine) Always watching you...",
                        ThumbnailUrl = "https://lh3.googleusercontent.com/AQ5S9Xj1z6LBbNis2BdUHM-mQbDrkvbrrlx5rTIxCPc-SwdITwjkJP370gZxNpjG92ND8wImuMuLyKnKi7te7w",
                        Footer = new EmbedFooterBuilder()
                        {
                            Text = "Hey you found a secret command :D"
                        },
                        Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel)
                    };
                    await ReplyAsync("", false, embedh.Build());
                    return;
                }
                if (Name.ToLower() == "entity303" || Name.ToLower() == "entity 303")
                {
                    var embedh = new EmbedBuilder()
                    {
                        Title = "Entity 303",
                        Description = "[Wiki](http://minecraftcreepypasta.wikia.com/wiki/Entity_303) A minecraft creepy pasta of a former Mojang employee who was fired by Notch and now want revenge",
                        ThumbnailUrl = "https://vignette3.wikia.nocookie.net/minecraftcreepypasta/images/4/49/Entity_303.png",
                        Footer = new EmbedFooterBuilder()
                        {
                            Text = "Hey you found a secret command :D"
                        },
                        Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel)
                    };
                    await ReplyAsync("", false, embedh.Build());
                    return;
                }
                _Mob Mob = _Config.MCMobs.Find(x => x.Name.ToLower() == Name.ToLower().Replace(" ", ""));
                if (Mob != null)
                {
                    var embed = new EmbedBuilder()
                    {
                        Title = $"[{Mob.ID}] {Mob.Name}",
                        Description = $"[Wiki]({Mob.WikiLink}) {Mob.Note}",
                        ThumbnailUrl = Mob.PicUrl,
                        Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel)
                    };
                    if (Mob.Type == _MobType.Secret)
                    {
                        embed.WithFooter(new EmbedFooterBuilder() { Text = "Hey you found a secret command :D" });
                    }
                    string Height = Mob.Height + " blocks";
                    string Width = Mob.Width + " blocks";
                    if (Mob.Height == "Rip")
                    {
                        Height = "Unknown";
                    }
                    if (Mob.Width == "Rip")
                    {
                        Width = "Unknown";
                    }
                    if (Mob.AttackEasy == "")
                    {

                        string PlayerText = "";
                        if (Mob.Name == "Player")
                        {
                            embed.WithTitle("Player");
                            PlayerText = Environment.NewLine + "**Fist Attack:** 0.5 :heart:";
                        }
                        embed.AddField("Stats", $"**Health:** {Mob.Health} :heart:" + Environment.NewLine + $"**Type:** {Mob.Type}" + PlayerText, true);
                        embed.AddField("Info", $"**Height:** {Height}" + Environment.NewLine + $"**Width:** {Width}" + Environment.NewLine + $"**Version:** {Mob.Version}", true);
                    }
                    else
                    {
                        embed.AddField("Stats", $"**Health:** {Mob.Health} :heart:" + Environment.NewLine + "**Attack** :crossed_swords:" + Environment.NewLine + $"**Easy:** {Mob.AttackEasy}" + Environment.NewLine + $"**Normal:** {Mob.AttackNormal}" + Environment.NewLine + $"**Hard:** {Mob.AttackHard}", true);
                        embed.AddField("Info", $"**Height:** {Height}" + Environment.NewLine + $"**Width:** {Width}" + Environment.NewLine + $"**Version:** {Mob.Version}" + Environment.NewLine + $"**Type:** {Mob.Type}", true);
                    }
                    await ReplyAsync("", false, embed.Build());
                }
                else
                {
                    await ReplyAsync("Could not find mob");
                }
            }
            else
            {
                List<string> Passive = new List<string>();
                List<string> Tameable = new List<string>();
                List<string> Neutral = new List<string>();
                List<string> Hostile = new List<string>();
                List<string> Boss = new List<string>();
                foreach (var i in _Config.MCMobs)
                {
                    if (i.EmojiID != "")
                    {
                        if (i.Type == _MobType.Passive)
                        {
                            Passive.Add($"<:{i.Name}:{i.EmojiID}>");
                        }
                        else if (i.Type == _MobType.Tameable)
                        {
                            Tameable.Add($"<:{i.Name}:{i.EmojiID}>");
                        }
                        else if (i.Type == _MobType.Neutral)
                        {
                            Neutral.Add($"<:{i.Name}:{i.EmojiID}>");
                        }
                        else if (i.Type == _MobType.Hostile)
                        {
                            Hostile.Add($"<:{i.Name}:{i.EmojiID}>");
                        }
                        else if (i.Type == _MobType.Boss)
                        {
                            Boss.Add($"<:{i.Name}:{i.EmojiID}>");
                        }
                    }
                }

                var embed = new EmbedBuilder()
                {
                    Description = "Get more info with mc/mob (Mob Name) | mc/mob Bat"
                };
                embed.AddField("Passive", string.Join(" ", Passive), true);
                embed.AddField("Tameable", string.Join(" ", Tameable), true);
                embed.AddField("Neutral", string.Join(" ", Neutral), true);
                embed.AddField("Hostile", string.Join(" ", Hostile), true);
                embed.AddField("Boss", string.Join(" ", Boss), true);

                await ReplyAsync("", false, embed.Build());
            }
        }

        [Command("potion"), Remarks("mc/potion"), Alias("potions")]
        public async Task Potion([Remainder]string Name = "")
        {
            if (Name == "")
            {
                var embed = new EmbedBuilder()
                {
                    Title = "Minecraft Potions",
                    Description = "Get a single potion using `mc/potion Instant Health`" + Environment.NewLine + "Or list them all `mc/potion all`",
                    Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel)
                };
                await ReplyAsync("", false, embed.Build());
            }
            else if (Name.ToLower() == "all")
            {
                List<string> Potions = new List<string>();
                foreach (_Potion Potion in _Config.MCPotions)
                {
                    if (Potion.Extended != null && Potion.Level2 != null)
                    {
                        Potions.Add($"<{Potion.Base} + {Potion.Ingredient} = {Potion.Name.Replace(" ", "")}> II ⏳");
                    }
                    else if (Potion.Level2 != null)
                    {
                        Potions.Add($"<{Potion.Base} + {Potion.Ingredient} = {Potion.Name.Replace(" ", "")}> II");
                    }
                    else
                    {
                        Potions.Add($"<{Potion.Base} + {Potion.Ingredient} = {Potion.Name.Replace(" ", "")}> ⏳");
                    }
                }
                var embed = new EmbedBuilder()
                {
                    Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                    Footer = new EmbedFooterBuilder()
                    {
                        Text = "II Has level 2 | ⏳ Can strengthen"
                    }
                };
                embed.AddField("Base Potions", "```md" + Environment.NewLine + "<NetherWart + Water Bottle = Awkward(Base1) >" + Environment.NewLine + "< Potion of swiftness = (Base2) >" + Environment.NewLine + "< Potion of strength = (Base3) >```");
                embed.AddField("Potions", "```md" + Environment.NewLine + string.Join(Environment.NewLine, Potions) + "```");
                await ReplyAsync("", false, embed.Build());
            }
            else
            {
                _Potion Potion = _Config.MCPotions.Find(x => x.Name.ToLower().Replace(" ", "").Contains(Name.ToLower().Replace(" ", "")));
                if (Potion == null)
                {
                    await ReplyAsync("Could not find potion");
                }
                else
                {
                    var embed = new EmbedBuilder()
                    {
                        Title = $"Potion of {Potion.Name}",
                        Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                        Description = "```md" + Environment.NewLine + $"Base: {Potion.GetBase()}```" + "```md" + Environment.NewLine + $"Recipe: <{Potion.Base} + {Potion.Ingredient} = {Potion.Name.Replace(" ", "")}>" + Environment.NewLine + $"<Duration {Potion.GetDuration()}> <Note {Potion.Note}```",
                        ThumbnailUrl = Potion.Image
                    };
                    if (Potion.Extended != null)
                    {
                        embed.AddField("Extended - Add redstone", "```md" + Environment.NewLine + $"<Duration {Potion.Extended.GetDuration()}>```");
                    }
                    if (Potion.Level2 != null)
                    {
                        embed.AddField("Level 2 - Add glowstone", "```md" + Environment.NewLine + $"<Duration {Potion.Level2.GetDuration()}> <Note {Potion.Level2.Note}>```");
                    }
                    await ReplyAsync("", false, embed.Build());
                }
            }
        }

        [Command("enchant"), Remarks("mc/enchant"), Alias("enchants")]
        public async Task Enchant([Remainder]string Name = "")
        {
            if (Name == "")
            {
                var embed = new EmbedBuilder()
                {
                    Description = "Get enchantment info using `mc/enchant (Name)` or `mc/enchant protection`",
                    Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel)
                };
                embed.AddField("Armor", "Protection | FireProt | Feather Falling | Blast Prot | Projectile Prot | Respiration | Aqua Affinity | Thorns | Depth Strider | Frost Walker");
                embed.AddField("Weapons", "Sharpness | Smite | Arthropods | Knockback | Fire Aspect | Looting");
                embed.AddField("Bows", "Power | Punch | Infinity | Flame");
                embed.AddField("Tools", "Efficiency | Silk Touch | Fortune");
                embed.AddField("Fishing Rod", "Luck Of The Sea | Lure");
                embed.AddField("All", "Unbreaking | Mending");
                await ReplyAsync("", false, embed.Build());
            }
            else
            {
                _Enchant Enchant = _Config.MCEnchantments.Find(x => x.Name.ToLower().Replace(" ", "").Contains(Name.ToLower().Replace(" ", "")));
                var embed = new EmbedBuilder()
                {
                    Title = $"[{Enchant.ID}] {Enchant.Name}",
                    Description = "```md" + Environment.NewLine + $"<Version {Enchant.Version}> <Type {Enchant.Type}> <MaxLevel {Enchant.MaxLevel}>" + Environment.NewLine + $"<Note {Enchant.Note}>```",
                    ThumbnailUrl = Enchant.GetEnchantItem(),
                    Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel)
                };
                await ReplyAsync("", false, embed.Build());
            }
        }
        
    }

    public class Quiz : InteractiveModuleBase
    {
        public _Trans.Main _TransMain = new _Trans.Main();
        [Command("quiz")]
        public async Task QuizCom(string Accept = "")
        {
            if (Accept == "start")
            {
                    Random.Org.Random Rng = new Random.Org.Random();
                    int Num = Rng.Next(1, _Config.MCQuiz.Count);
                    _Quiz Quiz = _Config.MCQuiz[Num - 1];
                    string User = $"<@{Context.User.Id}>";
                    var qembed = new EmbedBuilder()
                    {
                        Title = $"Question - {Context.User.Username}",
                        Description = Quiz.Question,
                        Color = new Color(135, 206, 235)
                    };
                    await ReplyAsync("", false, qembed.Build());
                    var response = await WaitForMessage(Context.Message.Author, Context.Channel, new TimeSpan(0, 0, 10));
                    if (response == null)
                    {
                        var embed = new EmbedBuilder()
                        {
                            Title = "Minecraft Quiz",
                            Description = $"{User} you ran out of time :(",
                            Color = new Color(200, 0, 0),
                            Footer = new EmbedFooterBuilder()
                            {
                                Text = "Question: " + Quiz.Question
                            }
                        };
                        await ReplyAsync("", false, embed.Build());
                    }
                    else
                    {
                        if (Quiz.Answer.Contains(response.Content.ToLower()))
                        {
                            var embed = new EmbedBuilder()
                            {
                                Title = "Minecraft Quiz",
                                Description = $"<:success:350172481186955267> Correct! {User}" + Environment.NewLine + Quiz.Note,
                                Color = new Color(0, 200, 0),
                                Footer = new EmbedFooterBuilder()
                                {
                                    Text = "Question: " + Quiz.Question
                                }
                            };
                            await ReplyAsync("", false, embed.Build());
                        }
                        else
                        {
                            var embed = new EmbedBuilder()
                            {
                                Title = "Minecraft Quiz",
                                Description = $"<:error:350172479936921611> Incorrect {User}",
                                Color = new Color(200, 0, 0),
                                Footer = new EmbedFooterBuilder()
                                {
                                    Text = "Question: " + Quiz.Question
                                }
                            };
                            await ReplyAsync("", false, embed.Build());
                        }

                    }
            }
            else
            {

                var embed = new EmbedBuilder()
                {
                    Title = "Minecraft Quiz",
                    Description = "Think you know your minecraft knowledge?" + Environment.NewLine + "Type **mc/quiz start** to play you have 10 seconds to answer",
                    Footer = new EmbedFooterBuilder()
                    {
                        Text = $"Questions Available {_Config.MCQuiz.Count()}"
                    },
                    Color = new Color(135, 206, 235)
                };
                await ReplyAsync("", false, embed.Build());
            }
        }
    }
}
