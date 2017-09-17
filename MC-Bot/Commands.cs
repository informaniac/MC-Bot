﻿using Bot.Apis;
using Bot.Classes;
using Discord;
using Discord.Commands;
using MojangSharp.Endpoints;
using MojangSharp.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MojangSharp.Endpoints.Statistics;
using static MojangSharp.Responses.NameHistoryResponse;
using System.Net.NetworkInformation;
using Bot.Functions;
using Bot.Utils;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Discord.Addons.InteractiveCommands;
using jsimple.util.function;
using Bot.Services;
using System.Text.RegularExpressions;
using Discord.WebSocket;

namespace Bot.Commands
{

    public class Main : ModuleBase
    {
        public _Translate.Commands.Main _TransMain = new _Translate.Commands.Main();

        private CommandService _Commands;
        private DiscordSocketClient _Client;
        public Main(DiscordSocketClient Client, CommandService Commands)
        {
            _Client = Client;
            _Commands = Commands;
        }
        public string NewsText = "";

        [Command("help"), Alias("commands")]
        public async Task Help(string Action = "")
        {
            try
            {
                _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
                if (Context.Guild != null && Guild == null)
                {
                    var NewGuildEmbed = new EmbedBuilder()
                    {
                        Title = ":wave: Hi im Minecraft Bot",
                        Description = "Im packed full of Minecraft commands and special features such as player skins, name history, quiz and some other secret commands",
                        Footer = new EmbedFooterBuilder()
                        {
                            Text = "If you have any issues please contact xXBuilderBXx#9113"
                        },
                        Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel)
                    };
                    IGuildUser Owner = await Context.Guild.GetOwnerAsync();
                    NewGuildEmbed.AddField("Language", $"<@{Owner.Id}> Please User" + Environment.NewLine + "`mc/lang 0` English **Default**" + Environment.NewLine + "`mc/lang 1` français", true);
                    NewGuildEmbed.AddField("Commands", "`mc/help` Commands" + Environment.NewLine + "`mc/admin` Guild Admin" + Environment.NewLine + "`mc/ping` Ping A Server" + Environment.NewLine + "`mc/invite` Bot Invite", true);
                    await ReplyAsync("", false, NewGuildEmbed.Build());
                    _Task.NewGuild(Context.Guild.Id);
                    return;
                }
                if (Context.Guild != null)
                {
                    IGuildUser GU = await Context.Guild.GetUserAsync(Context.Client.CurrentUser.Id);
                    if (!GU.GuildPermissions.EmbedLinks || !GU.GetPermissions(Context.Channel as ITextChannel).EmbedLinks)
                    {
                        await ReplyAsync("```python" + Environment.NewLine + $"{_TransMain.NoEmbedPerm[LangInt]}```");
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
                List<string> WikiCommands = new List<string>();

                foreach (var i in _Commands.Commands.Where(x => x.Module.Name == "Wiki"))
                {
                    WikiCommands.Add($"{i.Remarks}");
                }
                string CommunityName = "";
                string CommunityDesc = "";
                string CommunityLink = "";
                int Servers = 0;
                if (Context.Guild != null)
                {
                    if (Guild != null)
                    {
                        if (Guild.CommunityName != "")
                        {
                            CommunityName = Guild.CommunityName;
                            CommunityDesc = Guild.CommunityDescription;
                            CommunityLink = Guild.Website;
                            Servers = Guild.Servers.Count();
                        }
                    }
                }
                var embed = new EmbedBuilder()
                {
                    Title = $"Bot News > {NewsText}",
                    Description = "",
                    Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                    Footer = new EmbedFooterBuilder()
                    {
                        Text = _TransMain.HelpFooterHiddenCommands[LangInt]
                    }
                };
                if (CommunityName == "")
                {
                    embed.AddField(_TransMain.ThisCommunity[LangInt], _TransMain.CommunityError[LangInt]);
                }
                else
                {
                    if (CommunityLink == "")
                    {
                        embed.AddField(_TransMain.ThisCommunity[LangInt], CommunityName + Environment.NewLine + CommunityDesc);
                    }
                    else
                    {
                        embed.AddField($"{_TransMain.ThisCommunity[LangInt]} - {CommunityName}", $"{_TransMain.Servers[LangInt]} {Servers} [Website]({CommunityLink})" + Environment.NewLine + CommunityDesc);
                    }
                }
                embed.AddField("Wiki", string.Join(" | ", WikiCommands));
                embed.AddField(_TransMain.Commands[LangInt], "```md" + Environment.NewLine + string.Join(Environment.NewLine, _TransMain.HelpCommands[LangInt]) + "```");
                embed.AddField(_TransMain.HelpLinks[LangInt], $"[MultiMC](https://multimc.org/) {_TransMain.MultiMC[LangInt]}" + Environment.NewLine + "[Ftb Legacy](http://ftb.cursecdn.com/FTB2/launcher/FTB_Launcher.exe) | [Technic Launcher](https://www.technicpack.net/download) | [AT Launcher](https://www.atlauncher.com/downloads)");
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
            catch(Exception ex)
            {
                Console.WriteLine(ex);
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
        }

        [Command("colors"), Alias("color")]
        public async Task Colors()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            var embed = new EmbedBuilder()
            {
                Title = _TransMain.ColorCodes[LangInt],
                ImageUrl = "https://lolis.ml/img-1o4ubn88Z474.png"
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("uuid")]
        public async Task Uuid([Remainder]string Player)
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
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
                await ReplyAsync(_TransMain.PlayerNotFound[LangInt].Replace("{0}", $"`{Player}`"));
            }

        }

        [Command("ping"), Priority(0)]
        public async Task Ping(string IP = "", ushort Port = 25565)
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            if (IP == "" || IP.Contains("("))
            {
                await ReplyAsync($"{_TransMain.EnterIP[LangInt]} | `mc/ping my.server.net` | `mc/ping other.server.net:25566` | `mc/ping this.server.net 25567`");
                return;
            }
            switch (IP)
            {
                case "127.0.0.1":
                    await ReplyAsync(_TransMain.IPErrorMain[LangInt]);
                    return;
                case "192.168.0.1":
                    await ReplyAsync(_TransMain.IPErrorRouter[LangInt]);
                    return;
                case "0.0.0.0":
                    await ReplyAsync(_TransMain.IPErrorZero[LangInt]);
                    return;
                case "google.com":
                    await ReplyAsync(_TransMain.IPErrorGoogle[LangInt]);
                    return;
                case "youtube.com":
                    await ReplyAsync(_TransMain.IPErrorYoutube[LangInt]);
                    return;
                case "blazeweb.ml":
                    await ReplyAsync(_TransMain.IPErrorMyWeb[LangInt]);
                    return;
                case "mc.hypixel.net":
                    await ReplyAsync(_TransMain.IPErrorHypixel[LangInt]);
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
                                Description = $"<:error:350172479936921611> {_TransMain.InvalidIP[LangInt]}",
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
                        Description = $"<:error:350172479936921611> {_TransMain.InvalidIP[LangInt]}",
                        Color = new Color(200, 0, 0)
                    };
                    await ReplyAsync("", false, ValidEmbed.Build());
                    return;
                }
            }
            Cooldown Cooldown;
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
                    if ((DateTime.Now - Cooldown.Date).TotalMinutes >= 3)
                    {
                        Cooldown.Date = DateTime.Now;
                        Cooldown.Count = 1;
                    }
                    else
                    {
                        await ReplyAsync(_TransMain.Cooldown[LangInt]);
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
            Console.WriteLine(Cooldown.Date);
            Console.WriteLine(DateTime.Now);
            var Info = await ReplyAsync($"P{_TransMain.PleaseWait[LangInt]} `{IP}`");
            var ErrorEmbed = new EmbedBuilder()
            {
                Description = $"<:error:350172479936921611> {_TransMain.InvalidIP[LangInt]}",
                Color = new Color(200, 0, 0)
            };
            await Task.Run(async () =>
            {
                try
                {
                    Ping PingTest = new Ping();
                    PingReply PingReply = PingTest.Send(IP);
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
                            Description = _TransMain.ServerLoading[LangInt],
                            Color = new Color(0, 191, 255),

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
                            Description = $"{_TransMain.Players[LangInt]} {Ping.CurrentPlayers}/{Ping.MaximumPlayers}",
                            Footer = new EmbedFooterBuilder()
                            {
                                Text = Ping.Motd.Replace("§a", "").Replace("§1", "").Replace("§2", "").Replace("§3", "").Replace("§4", "").Replace("§5", "").Replace("§6", "").Replace("§7", "").Replace("§8", "").Replace("§9", "").Replace("§b", "").Replace("§c", "").Replace("§d", "").Replace("§e", "").Replace("§f", "").Replace("§l", "")
                            }
                        };
                        if (Ping.Version.Contains("BungeeCord"))
                        {
                            embed.WithDescription(embed.Description + Environment.NewLine + _TransMain.BungeeCord[LangInt]);
                        }
                        await Info.DeleteAsync();
                        await ReplyAsync("", false, embed.Build());
                    }
                }
                else
                {
                    var embed = new EmbedBuilder()
                    {
                        Description = $"<:warning:350172481757118478> {_TransMain.ServerOffline[LangInt]}",
                        Color = new Color(255, 165, 0)
                    };
                    await Info.DeleteAsync();
                    await ReplyAsync("", false, embed.Build());
                }
            });
        }

        [Command("list")]
        [Alias("servers")]
        public async Task List()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            if (Context.Guild == null)
            {
                await ReplyAsync("This command can only be used in a guild");
                return;
            }
            if (Guild.Servers.Count == 0)
            {
                await ReplyAsync($"{_TransMain.NoGuildServers[LangInt]} :(" + Environment.NewLine + $"{_TransMain.NoGuildServersAdmin[LangInt]} `mc/admin`");
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
                    Name = $"{Name} {_TransMain.Servers[LangInt]}",
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
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            StatisticsResponse stats = await new Statistics(Item.MinecraftAccountsSold).PerformRequest();
            if (stats.IsSuccess)
            {
                var embed = new EmbedBuilder()
                {
                    Author = new EmbedAuthorBuilder()
                    {
                        Name = _TransMain.MinecraftSales[LangInt],
                        Url = _TransMain.MinecraftSalesUrl[LangInt]
                    },
                    Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                    Description = $"Total: {stats.Total}" + Environment.NewLine + $"24 Hours: {stats.Last24h}" + Environment.NewLine + $"Average Per Second: {stats.SaleVelocity}",
                    Footer = new EmbedFooterBuilder()
                    {
                        Text = _TransMain.MinecraftSalesError[LangInt]
                    }
                };
                await ReplyAsync("", false, embed.Build());
            }
            else
            {
                await ReplyAsync(_TransMain.ApiError[LangInt]);
            }
        }

        [Command("skin")]
        public async Task SkinArg(string Arg = null, [Remainder] string Player = "")
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            if (Arg == null)
            {
                await Context.Channel.SendMessageAsync($"mc/skin (Arg) {_TransMain.SkinArgs[LangInt]} | `mc/skin Notch` or `mc/skin cube Notch`");
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
                await ReplyAsync(_TransMain.PlayerNotFound[LangInt].Replace("{0}", $"`{Player}`"));
                return;
            }
            string Url = "";
            switch (Arg.ToLower())
            {
                case "head":
                    Url = "https://visage.surgeplay.com/face/100/" + Player;
                    break;
                case "cube":
                    Url = "https://visage.surgeplay.com/head/100/" + Player;
                    break;
                case "full":
                    Url = "https://visage.surgeplay.com/full/200/" + Player;
                    break;
                case "steal":
                    Url = "steal";
                    await ReplyAsync($"{Context.User.Username} {_TransMain.StoleASkin[LangInt]} :o <https://minotar.net/download/" + Player + ">");
                    return;
                default:
                    await Context.Channel.SendMessageAsync($"{_TransMain.UnknownArg[LangInt]} mc/skin");
                    return;
            }
            var embed = new EmbedBuilder()
            {
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                ImageUrl = Url
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("names")]
        public async Task Names([Remainder]string Name)
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            UuidAtTimeResponse uuid = new UuidAtTime(Name, DateTime.Now).PerformRequest().Result;
            if (uuid.IsSuccess)
            {
                NameHistoryResponse names = new NameHistory(uuid.Uuid.Value).PerformRequest().Result;
                if (names.IsSuccess)
                {
                    List<string> Names = new List<string>();
                    if (names.NameHistory.Count == 1)
                    {
                        await ReplyAsync(_TransMain.UsernamesOnlyOne[LangInt].Replace("{0}", $"`{Name}`"));
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
                            Names.Add($"[ {entry.Name} ]( {_TransMain.First[LangInt]} )");
                        }
                    }

                    await ReplyAsync("```" + Environment.NewLine + string.Join(Environment.NewLine, Names) + "```");
                }
                else
                {
                    await ReplyAsync(_TransMain.ApiError[LangInt]);
                }
            }
            else
            {
                await ReplyAsync(_TransMain.PlayerNotFoundNames[LangInt].Replace("{0}", $"`{Name}`"));
            }
        }

        [Command("status")]
        public async Task Status()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            ApiStatusResponse status = new ApiStatus().PerformRequest().Result;
            if (status.IsSuccess)
            {
                var embed = new EmbedBuilder()
                {
                    Title = _TransMain.MojangStatus[LangInt],
                    Description = $"Mojang: {status.Mojang}" + Environment.NewLine + $"Minecraft.net: {status.Minecraft}" + Environment.NewLine +
                    $"{_TransMain.MojangAccounts[LangInt]}: {status.MojangAccounts}" + Environment.NewLine + $"Mojang API: {status.MojangApi}" + Environment.NewLine +
                    $"{_TransMain.MojangAuthServers[LangInt]}: {status.MojangAutenticationServers}" + Environment.NewLine + $"{_TransMain.MojangAuthService[LangInt]}: {status.MojangAuthenticationService}" + Environment.NewLine +
                    $"{_TransMain.MojangSessions[LangInt]}: {status.MojangSessionsServer}" + Environment.NewLine + $"{_TransMain.MinecraftSessions[LangInt]}: {status.Sessions}" + Environment.NewLine +
                    $"{_TransMain.MinecraftSkins[LangInt]}: {status.Skins}" + Environment.NewLine + $"{_TransMain.MinecraftTextures[LangInt]}: {status.Textures}",
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
                await ReplyAsync(_TransMain.ApiError[LangInt]);
            }
        }

        [Command("music")]
        public async Task Music()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            var embed = new EmbedBuilder()
            {
            };
            embed.AddField("Commands", "```md" + Environment.NewLine + "[ mc/music play (ID) ][ Play a song by ID ]" + Environment.NewLine + "[ mc/music stop ][ Stop the current playing music ]" + Environment.NewLine + "[ mc/music leave ][ Stop current song and leave the voice channel ]```");
            embed.AddField("Playlist", "```md" + Environment.NewLine + "<1 Mine Diamonds>" + Environment.NewLine + "<2 Screw the nether - Yogscast>" + Environment.NewLine + "<3 Mincraft Style - Captain Sparklez>```");
            //await ReplyAsync("", false, embed.Build());
            await ReplyAsync("Coming Soon");
        }

        [Command("get")]
        public async Task Get([Remainder]string Text)
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            if (Text.Length > 20)
            {
                await ReplyAsync(_TransMain.GetAchievementError[LangInt]);
                return;
            }
            Random RNG = new Random();
            int Number = RNG.Next(1, 39);
            UriBuilder uriBuilder = new UriBuilder("https://www.minecraftskinstealer.com/achievement/a.php?i=" + Number.ToString() + "&h=Achievement+Get!&t=" + Text.Replace(" ", "+"));
            var embed = new EmbedBuilder()
            {
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                ImageUrl = uriBuilder.Uri.AbsoluteUri
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("skinedit")]
        public async Task Misc()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            var embed = new EmbedBuilder()
            {
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                Description = $"[{_TransMain.OnlineSkinEditor[LangInt]}](https://www.minecraftskinstealer.com/skineditor.php)"
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("minime")]
        public async Task Minime(string Player)
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
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
                await ReplyAsync(_TransMain.PlayerNotFound[LangInt].Replace("{0}", $"`{Player}`"));
            }
        }

        [Command("bot")]
        public async Task Bot()
        {
            int FrenchCount = 0;
            int SpanishCount = 0;
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
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
                Description = _TransMain.Bot_Desc[LangInt],
                Footer = new EmbedFooterBuilder()
                {
                    Text = _TransMain.Bot_Footer[LangInt]
                },
                Color = _Utils_Discord.GetRoleColor(Context.Channel)
            };
            embed.AddField($"<:info:350172480645758976> {_TransMain.Info[LangInt]}", $"**{_TransMain.Bot_BotOwner[LangInt]}**" + Environment.NewLine + "xXBuilderBXx#9113" + Environment.NewLine + "<@190590364871032834>" + Environment.NewLine + Environment.NewLine + $"**{_TransMain.Language[LangInt]}:** C#" + Environment.NewLine + $"**{_TransMain.Library[LangInt]}:** {_Config.Library}", true);
            embed.AddField($"<:stats:350172481157464065> {_TransMain.Stats[LangInt]}", $"**{_TransMain.Guilds[LangInt]}:** {_Client.Guilds.Count()}" + Environment.NewLine + $"**{_TransMain.Commands[LangInt]}:** {Count}" + Environment.NewLine + $"**{_TransMain.Uptime[LangInt]}:** {Uptime}" + Environment.NewLine + Environment.NewLine + $"**français:** {FrenchCount}" + Environment.NewLine + $"**Español:** {SpanishCount}", true);
            embed.AddField($"<:world:350172484038950912> {_TransMain.Links[LangInt]}", $"[{_TransMain.Links[LangInt]}](https://discordapp.com/oauth2/authorize?&client_id=" + Context.Client.CurrentUser.Id + "&scope=bot&permissions=0)" + Environment.NewLine + $"[{_TransMain.Website[LangInt]}](https://blazeweb.ml)" + Environment.NewLine + "[Github](https://github.com/xXBuilderBXx/MC-Bot)" + Environment.NewLine + Environment.NewLine + $"**{_TransMain.BotListGuilds[LangInt]}**" + Environment.NewLine + "[Dbots](https://bots.discord.pw/bots/346346285953056770)" + Environment.NewLine + "[DBL](https://discordbots.org/bot/346346285953056770)" + Environment.NewLine + "[Novo](https://novo.archbox.pro/)", true);
            await ReplyAsync("", false, embed.Build());
        }
    }
    public class Music : ModuleBase
    {
        private MusicService _MusicService;
        public Music(MusicService MusicService)
        {
            _MusicService = MusicService;
        }
        [Command("music play")]
        public async Task MusicPlay([Remainder]string Song)
        {
            if (Context.User.Id != 190590364871032834)
            {
                return;
            }
            IGuildUser GU = (IGuildUser)Context.User;
            if (GU.VoiceChannel == null)
            {
                await ReplyAsync("You are not in a voice channel");
                return;
            }
            MusicService._MusicPlayer MP = _MusicService.GetMusicPlayer(Context.Guild);


            string SongPath = "";
            switch (Song)
            {
                case "1":
                    SongPath = "C:/music1.mp3";
                    break;
                case "2":
                    SongPath = "C:/music2.mp3";
                    break;
                case "3":
                    SongPath = "C:/music3.mp3";
                    break;
                default:
                    await ReplyAsync("You need to choose a song ID > mc/music");
                    return;
            }
            if (MP._BotUser.VoiceChannel == null)
            {
                await MP.PlayMusic(SongPath, GU.VoiceChannel, false);
            }
            else
            {
                if (MP._BotUser.VoiceChannel.Id == GU.VoiceChannel.Id)
                {
                    MP._AudioClient = await GU.VoiceChannel.ConnectAsync();
                    await MP.PlayMusic(SongPath, GU.VoiceChannel, false);
                }
                else
                {
                    IEnumerable<IGuildUser> Users = await MP._BotUser.VoiceChannel.GetUsersAsync().Flatten();
                    if (Users.Where(x => !x.IsBot).Count() == 1)
                    {
                        if (Users.First().Id == Context.User.Id)
                        {
                            await MP.PlayMusic(SongPath, GU.VoiceChannel, true);
                        }
                        else
                        {
                            await ReplyAsync("Cannot switch channels while someone is listening to music");
                        }
                    }
                    else if (Users.Where(x => !x.IsBot).Count() > 0)
                    {
                        await ReplyAsync("Cannot switch channels while someone is listening to music");
                    }
                    else
                    {
                        await MP.PlayMusic(SongPath, GU.VoiceChannel, true);
                    }
                }
            }
        }

        [Command("music stop")]
        public async Task MusicStop()
        {
            if (Context.User.Id != 190590364871032834)
            {
                return;
            }
            IGuildUser GU = (IGuildUser)Context.User;
            if (GU.VoiceChannel == null)
            {
                await ReplyAsync("You are not in a voice channel");
                return;
            }
            MusicService._MusicPlayer MP = _MusicService.GetMusicPlayer(Context.Guild);
            if (MP._BotUser.VoiceChannel == null)
            {
                await ReplyAsync("I am not in a voice channel");
                return;
            }
            MP._Process.Close();
        }

        [Command("music leave")]
        public async Task MusicLeave()
        {
            if (Context.User.Id != 190590364871032834)
            {
                return;
            }
            MusicService._MusicPlayer MP = _MusicService.GetMusicPlayer(Context.Guild);
            if (MP._BotUser.VoiceChannel == null)
            {
                await ReplyAsync("I am not in a voice channel");
            }
            else
            {
                if (MP._AudioClient == null)
                {
                    var AC = await MP._BotUser.VoiceChannel.ConnectAsync();
                    await AC.StopAsync();
                }
                else
                {
                    MP.Stop(true);
                }
            }
        }

    }

    public class Hidden : ModuleBase
    {
        _Translate.Commands.Hidden _TransHidden = new _Translate.Commands.Hidden();

        [Command("classic")]
        public async Task Classic()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            var embed = new EmbedBuilder()
            {
                Title = "Minecraft Classic",
                Description = $"{_TransHidden.MinecraftClassic[LangInt]} [Wiki](https://minecraft.gamepedia.com/Classic)",
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                Footer = new EmbedFooterBuilder()
                {
                    Text = _TransHidden.FoundSecretCommand[LangInt]
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
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            var embed = new EmbedBuilder()
            {
                Description = _TransHidden.Forgecraft[LangInt] + Environment.NewLine +
                $"[{_TransHidden.ForgecraftWallpaper[LangInt]}](http://feed-the-beast.wikia.com/wiki/Forgecraft) | [{_TransHidden.Wallpaper[LangInt]}](http://www.minecraftforum.net/forums/show-your-creation/fan-art/other-fan-art/1582624-forgecraft-wallpaper)",
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                Footer = new EmbedFooterBuilder()
                {
                    Text = _TransHidden.FoundSecretCommand[LangInt]
                }
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("forgecraftwallpaper")]
        public async Task ForgecraftWallpaper()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            var embed = new EmbedBuilder()
            {
                Author = new EmbedAuthorBuilder()
                {
                    Name = _TransHidden.ForgecraftWallpaper[LangInt],
                    Url = "http://www.minecraftforum.net/forums/show-your-creation/fan-art/other-fan-art/1582624-forgecraft-wallpaper"
                },
                ImageUrl = "https://dl.dropbox.com/u/25591134/ForgeCraft/ForgeCraft-480x270.jpg",
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                Footer = new EmbedFooterBuilder()
                {
                    Text = _TransHidden.FoundSecretCommand[LangInt]
                }
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("bukkit")]
        public async Task Bukkit()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            var embed = new EmbedBuilder()
            {
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                Description = $"{_TransHidden.Bukkit[LangInt]} [{_TransHidden.BukkitNews[LangInt]}](https://bukkit.org/threads/bukkit-its-time-to-say.305106/)",
                Footer = new EmbedFooterBuilder()
                {
                    Text = _TransHidden.FoundSecretCommand[LangInt]
                }
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("direwolf20")]
        public async Task DW()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            var embed = new EmbedBuilder()
            {
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                Description = _TransHidden.Direwolf20[LangInt] + Environment.NewLine + "[Youtube](https://www.youtube.com/channel/UC_ViSsVg_3JUDyLS3E2Un5g) | [Twitch](https://www.twitch.tv/direwolf20) | [Twitter](https://twitter.com/Direwolf20) | [Reddit](https://www.reddit.com/r/DW20/) | [Discord](https://discordapp.com/invite/SQ6wjHg)",
                Footer = new EmbedFooterBuilder()
                {
                    Text = _TransHidden.FoundSecretCommand[LangInt]
                }
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("herobrine")]
        public async Task Herobrine()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            var embedh = new EmbedBuilder()
            {
                Title = "Herobrine",
                Description = $"{_TransHidden.Herobrine[LangInt]} [Wiki](http://minecraftcreepypasta.wikia.com/wiki/Herobrine)",
                ThumbnailUrl = "https://lh3.googleusercontent.com/AQ5S9Xj1z6LBbNis2BdUHM-mQbDrkvbrrlx5rTIxCPc-SwdITwjkJP370gZxNpjG92ND8wImuMuLyKnKi7te7w",
                Footer = new EmbedFooterBuilder()
                {
                    Text = _TransHidden.FoundSecretCommand[LangInt]
                },
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel)
            };
            await ReplyAsync("", false, embedh.Build());
        }

        [Command("entity303")]
        public async Task Entity303()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            var embedh = new EmbedBuilder()
            {
                Title = "Entity 303",
                Description = $"{_TransHidden.Entity303[LangInt]} [Wiki](http://minecraftcreepypasta.wikia.com/wiki/Entity_303)",
                ThumbnailUrl = "https://vignette3.wikia.nocookie.net/minecraftcreepypasta/images/4/49/Entity_303.png",
                Footer = new EmbedFooterBuilder()
                {
                    Text = _TransHidden.FoundSecretCommand[LangInt]
                },
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel)
            };
            await ReplyAsync("", false, embedh.Build());
        }

        [Command("israphel")]
        public async Task IS()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            var embed = new EmbedBuilder()
            {
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                Description = $"{_TransHidden.Israphel[LangInt]} [Youtube](https://www.youtube.com/playlist?list=PLF60520313D07F366)",
                Footer = new EmbedFooterBuilder()
                {
                    Text = _TransHidden.FoundSecretCommand[LangInt]
                }
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("notch")]
        public async Task Notch()
        {
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            var embed = new EmbedBuilder()
            {
                Description = $"{_TransHidden.Notch[LangInt]} [Wiki](https://en.wikipedia.org/wiki/Markus_Persson)",
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                Footer = new EmbedFooterBuilder()
                {
                    Text = _TransHidden.FoundSecretCommand[LangInt]
                }
            };
            await ReplyAsync("", false, embed.Build());
        }
    }

    public class GuildAdmin : ModuleBase
    {
        public _Translate.Commands.Admin _TransAdmin = new _Translate.Commands.Admin();

        [Command("admin")]
        public async Task Admin()
        {
            if (Context.Guild == null)
            {
                await ReplyAsync("<:error:350172479936921611> Guild command only");
                return;
            }
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            IGuildUser GU = await Context.Guild.GetUserAsync(Context.User.Id);
            if (!GU.GuildPermissions.Administrator)
            {
                await ReplyAsync($"<:error:350172479936921611> {_TransAdmin.GuildAdminOnly[LangInt]}");
                return;
            }

            var embed = new EmbedBuilder()
            {
                Title = _TransAdmin.GuildCommand[LangInt],
                Description = "```md" + Environment.NewLine + $"{string.Join(Environment.NewLine, _TransAdmin.Commands[LangInt])}" + Environment.NewLine + $"< {_TransAdmin.UseList[LangInt]} >```",
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                Footer = new EmbedFooterBuilder()
                { Text = "" }

            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("addserver")]
        public async Task Addserver(string Tag = "", string IP = "", [Remainder]string Name = "")
        {
            if (Context.Guild == null)
            {
                await ReplyAsync("<:error:350172479936921611> Guild command only");
                return;
            }
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            IGuildUser GU = await Context.Guild.GetUserAsync(Context.User.Id);
            if (!GU.GuildPermissions.Administrator)
            {
                await ReplyAsync($"<:error:350172479936921611> {_TransAdmin.GuildAdminOnly[LangInt]}");
                return;
            }

            if (Tag == "" || IP == "" || Name == "")
            {
                await ReplyAsync($"{_TransAdmin.AddServer[LangInt]} | `mc/addserver (Tag) (IP) (Name)` | `mc/addserver sf sky.minecraft.net Skyfactory 2`");
                return;
            }

            _Server Server = Guild.Servers.Find(x => x.Tag.ToLower() == Tag.ToLower());
            if (Server != null)
            {
                await ReplyAsync(_TransAdmin.AddServerAlready[LangInt]);
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
            _Task.SaveGuild(Context.Guild.Id);
            await ReplyAsync($"{_TransAdmin.AddedServer[LangInt]} {Name} {_TransAdmin.AddedServerList[LangInt]} | `mc/list`");

        }

        [Command("delserver")]
        public async Task Delserver(string Tag = "")
        {
            if (Context.Guild == null)
            {
                await ReplyAsync("<:error:350172479936921611> Guild command only");
                return;
            }
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            IGuildUser GU = await Context.Guild.GetUserAsync(Context.User.Id);
            if (!GU.GuildPermissions.Administrator)
            {
                await ReplyAsync($"<:error:350172479936921611> {_TransAdmin.GuildAdminOnly[LangInt]}");
                return;
            }

            if (Tag == "")
            {
                await ReplyAsync($"{_TransAdmin.DeleteEnterTag[LangInt]} | `mc/delserver (Tag)` | `mc/delserver sf`");
                return;
            }
            _Server Server = Guild.Servers.Find(x => x.Tag.ToLower() == Tag.ToLower());
            if (Server == null)
            {
                await ReplyAsync(_TransAdmin.DeleteNoServer[LangInt]);
                return;
            }
            Guild.Servers.Remove(Server);
            _Task.SaveGuild(Context.Guild.Id);
            await ReplyAsync($"{_TransAdmin.DeletedServer[LangInt]} {Server.Name} {_TransAdmin.DeletedServerList[LangInt]} | `mc/list`");

        }

        [Command("setname")]
        public async Task SetName([Remainder]string Name = "")
        {
            if (Context.Guild == null)
            {
                await ReplyAsync("<:error:350172479936921611> Guild command only");
                return;
            }
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            IGuildUser GU = await Context.Guild.GetUserAsync(Context.User.Id);
            if (!GU.GuildPermissions.Administrator)
            {
                await ReplyAsync($"<:error:350172479936921611> {_TransAdmin.GuildAdminOnly[LangInt]}");
                return;
            }

            if (Name == "")
            {
                await ReplyAsync($"<:error:350172479936921611> {_TransAdmin.EnterName[LangInt]} mc/setname ({_TransAdmin.Name[LangInt]}) | mc/setname Mineplex");
                return;
            }
            Guild.CommunityName = Name;
            _Task.SaveGuild(Context.Guild.Id);
            await ReplyAsync($"<:success:350172481186955267> {_TransAdmin.CommunityNameSet[LangInt]}");
        }

        [Command("setdesc")]
        public async Task SetDesc([Remainder]string Desc = "")
        {
            if (Context.Guild == null)
            {
                await ReplyAsync("<:error:350172479936921611> Guild command only");
                return;
            }
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            IGuildUser GU = await Context.Guild.GetUserAsync(Context.User.Id);
            if (!GU.GuildPermissions.Administrator)
            {
                await ReplyAsync($"<:error:350172479936921611> {_TransAdmin.GuildAdminOnly[LangInt]}");
                return;
            }
            if (Desc == "")
            {
                await ReplyAsync($"<:error:350172479936921611> {_TransAdmin.EnterDesc[LangInt]} mc/setdesc ({_TransAdmin.Text[LangInt]}) | mc/setname {_TransAdmin.DescPlaceholder[LangInt]}");
                return;
            }
            Guild.CommunityDescription = Desc;
            _Task.SaveGuild(Context.Guild.Id);
            await ReplyAsync($"<:success:350172481186955267> Community {_TransAdmin.CommunityDescSet[LangInt]}");
        }

        [Command("setlink")]
        public async Task SetLink(string Link = "")
        {
            if (Context.Guild == null)
            {
                await ReplyAsync("<:error:350172479936921611> Guild command only");
                return;
            }
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            if (Context.Guild != null & Guild == null)
            {
                await ReplyAsync("Please use mc/help");
                return;
            }
            IGuildUser GU = await Context.Guild.GetUserAsync(Context.User.Id);
            if (!GU.GuildPermissions.Administrator)
            {
                await ReplyAsync($"<:error:350172479936921611> {_TransAdmin.GuildAdminOnly[LangInt]}");
                return;
            }

            if (Link == "")
            {
                await ReplyAsync($"<:error:350172479936921611> {_TransAdmin.EnterLink[LangInt]} mc/setlink (Link) | mc/setname https://minecraftpro.com");
                return;
            }
            Guild.Website = Link;
            _Task.SaveGuild(Context.Guild.Id);
            await ReplyAsync($"<:success:350172481186955267> {_TransAdmin.CommunityLinkSet[LangInt]}");
        }

        [Command("lang")]
        public async Task Language(int ID = -1)
        {
            if (Context.Guild == null)
            {
                await ReplyAsync("<:error:350172479936921611> Guild command only");
                return;
            }
            _Task.GetGuild(Context.Guild, out _Guild Guild, out int LangInt);
            IGuildUser GU = await Context.Guild.GetUserAsync(Context.User.Id);
            if (Context.User.Id != 190590364871032834)
            {
                if (!GU.GuildPermissions.Administrator)
                {
                    await ReplyAsync($"<:error:350172479936921611> {_TransAdmin.GuildAdminOnly[LangInt]}");
                    return;
                }
            }
            if (ID == -1)
            {
                var embed = new EmbedBuilder()
                {
                    Title = _TransAdmin.ChangeLang[LangInt],
                    Description = "```md" + Environment.NewLine + "<0 English> mc/lang 0" + Environment.NewLine + "<1 français> mc/lang 1" + Environment.NewLine + "<2 Español> mc/lang 2```",
                    Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                    Footer = new EmbedFooterBuilder()
                    {
                        Text = $"{_TransAdmin.LanguageTranslate[LangInt]} xXBuilderBXx#9113"
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
                        await ReplyAsync("Guild language set to english");
                        _Task.SaveGuild(Context.Guild.Id);
                        break;
                    case 1:
                        Guild.Language = _Language.French;
                        await ReplyAsync("Langue de la guilde réglée en français");
                        _Task.SaveGuild(Context.Guild.Id);
                        break;
                    case 2:
                        Guild.Language = _Language.Spanish;
                        await ReplyAsync("Idioma del gremio ajustado al español");
                        _Task.SaveGuild(Context.Guild.Id);
                        break;
                }
            }
        }
    }

    public class Wiki : ModuleBase
    {
        [Command("item"), Remarks("mc/item")]
        [Alias("block")]
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
                ThumbnailUrl = "https://lolis.ml/mcitems/" + ID + "-" + Meta + ".png",
                Color = _Utils_Discord.GetRoleColor(Context.Channel as ITextChannel),
                Footer = new EmbedFooterBuilder()
                { Text = $"/give {Context.User.Username} {Item.Text}" }
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("mob"), Remarks("mc/mob")]
        [Alias("mobs")]
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

        [Command("enchant"), Remarks("mc/enchant")]
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

        [Command("biome"), Remarks("~~mc/biome~~")]
        public async Task Biome()
        {
            await ReplyAsync("Command coming soon please wait");
        }
    }

    public class Quiz : InteractiveModuleBase
    {
        [Command("quiz")]
        public async Task QuizCom(string Accept = "")
        {
            if (Accept == "start")
            {
                var Ran = new Random((int)DateTime.Now.Ticks);
                int Num = Ran.Next(1, _Config.MCQuiz.Count);
                _Quiz Quiz = _Config.MCQuiz[Num - 1];
                string User = $"{Context.User.Username}";
                var qembed = new EmbedBuilder()
                {
                    Title = $"Question - {User}",
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
                        Color = new Color(200, 0, 0)
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
                            Color = new Color(0, 200, 0)
                        };
                        await ReplyAsync("", false, embed.Build());
                    }
                    else
                    {
                        var embed = new EmbedBuilder()
                        {
                            Title = "Minecraft Quiz",
                            Description = $"<:error:350172479936921611> Incorrect {User}",
                            Color = new Color(200, 0, 0)
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
