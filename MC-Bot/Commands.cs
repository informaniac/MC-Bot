using Bot.Apis;
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

namespace Bot.Commands
{
    
    public class Main : ModuleBase
    {
        

        private CommandService _Commands;
        public Main(CommandService Commands)
        {
            _Commands = Commands;
        }
        public string NewsText = "";

        [Command("help"), Alias("commands")]
        public async Task Help()
        {
            if (Context.Guild != null)
            {
                IGuildUser GU = await Context.Guild.GetUserAsync(Context.Client.CurrentUser.Id);
                if (!GU.GuildPermissions.EmbedLinks || !GU.GetPermissions(Context.Channel as ITextChannel).EmbedLinks)
                {
                    await ReplyAsync("```python" + Environment.NewLine + "Bot requires permission \" Embed Links \"```");
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
            List<string> Commands = new List<string>();
            List<string> WikiCommands = new List<string>();
            foreach (var I in _Commands.Commands.Where(x => x.Module.Name == "Main"))
            {
                if (I.Summary == null || I.Summary == "") continue;
                try
                {
                        I.Summary.Trim();
                        Commands.Add($"[ mc/{I.Remarks} ]( {I.Summary} )");
                }
                catch { }
            }
            foreach(var i in _Commands.Commands.Where(x => x.Module.Name == "Wiki"))
            {
                WikiCommands.Add($"{i.Remarks}");
            }
            string CommunityName = "";
            string CommunityDesc = "";
            string CommunityLink = "";
            int Servers = 0;
            if (Context.Guild != null)
            {
                _Guild Guild = _Config.MCGuilds.Find(x => x.ID == Context.Guild.Id);
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
                Color = Bot.Utils.DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
                Footer = new EmbedFooterBuilder()
                {
                    Text = "There are some hidden commands aswell ;)"
                }
            };
            if (CommunityName == "")
            {
                embed.AddField("This Community", "This guild is not registered as a community contact the guild owner to set it up");
            }
            else
            {
                if (CommunityLink == "")
                {
                    embed.AddField("This Community", CommunityName + Environment.NewLine + CommunityDesc);
                }
                else
                {
                    embed.AddField($"This Community - {CommunityName}", $"Servers {Servers} [Website]({CommunityLink})" + Environment.NewLine + CommunityDesc);
                }
            }
            embed.AddField("Wiki", string.Join(" | ", WikiCommands));
            embed.AddField("Commands", "```md" + Environment.NewLine + string.Join(Environment.NewLine, Commands) + "```");
            embed.AddField("Links", "[MultiMC](https://multimc.org/) MultiMC allows you to manage and launch multiple versions with easy forge/mods installation" + Environment.NewLine + "[Ftb Legacy](http://ftb.cursecdn.com/FTB2/launcher/FTB_Launcher.exe) | [Technic Launcher](https://www.technicpack.net/download) | [AT Launcher](https://www.atlauncher.com/downloads)");

            //ITextChannel TE = await Context.Guild.GetTextChannelAsync(351033810961301506);
            //var Delete = await TE.GetMessageAsync(352287809949794317);
            //IUserMessage Update = await TE.GetMessageAsync(351404116527808512) as IUserMessage;
            //await Delete.DeleteAsync();
            //await Update.ModifyAsync(x => { x.Embed = embed.Build(); });
            await ReplyAsync("", false, embed.Build());
        }

        [Command("quiztestblahlol"),Remarks("quiz"), Summary("Minecraft quiz :D")]
        public async Task Quiztestblah()
        {
            
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

        [Command("colors"), Remarks("colors"), Summary("MC color codes"), Alias("color")]
        public async Task Colors()
        {
            var embed = new EmbedBuilder()
            {
                Title = "Minecraft Color Codes",
                ImageUrl = "https://lolis.ml/img-1o4ubn88Z474.png"
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("uuid"), Remarks("uuid (Player)"), Summary("Player UUID")]
        public async Task Uuid([Remainder]string Name)
        {
            UuidAtTimeResponse uuid = new UuidAtTime(Name, DateTime.Now).PerformRequest().Result;
            if (uuid.IsSuccess)
            {
                var embed = new EmbedBuilder()
                {
                    Title = $"UUID | {Name}",
                    Color = DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
                    Description = uuid.Uuid.Value
                };
                await ReplyAsync("", false, embed.Build());
            }
            else
            {
                await ReplyAsync($"Cannot find player `{Name}`");
            }

        }

        [Command("ping"), Priority(0), Remarks("ping (IP)"), Summary("Ping a server")]
        public async Task Ping(string IP = "", ushort Port = 25565)
        {

            
            if (IP == "" || IP.Contains("("))
            {
                await ReplyAsync("Enter an IP | `mc/ping my.server.net` | `mc/ping other.server.net:25566` | `mc/ping this.server.net 25567`");
                return;
            }
            switch(IP)
            {
                case "127.0.0.1":
                    await ReplyAsync("You really think that would work?");
                    return;
                case "192.168.0.1":
                    await ReplyAsync("Minecraft servers dont run on routers DUH");
                    return;
                case "0.0.0.0":
                    await ReplyAsync("Not enough zeros?");
                    return;
                case "google.com":
                    await ReplyAsync("This is for minecraft servers not google :(");
                    return;
                case "youtube.com":
                    await ReplyAsync("This is for minecraft servers not youtube :(");
                    return;
                case "blazeweb.ml":
                    await ReplyAsync("Trying to ping my own website :D");
                    return;
                case "mc.hypixel.net":
                    await ReplyAsync("Hypixel network has blocked the ping sorry :(");
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
                    _Guild Guild = _Config.MCGuilds.Find(x => x.ID == Context.Guild.Id);
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
                                Description = "<:error:350172479936921611> This is not a valid ip",
                                Color = new Color(200, 0,0)
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
                        Description = "<:error:350172479936921611> This is not a valid ip",
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
                        await ReplyAsync("You are on cooldown for 2 mins!");
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
            var Info = await ReplyAsync($"Please wait while i ping `{IP}`");
            var ErrorEmbed = new EmbedBuilder()
            {
                Description = "<:error:350172479936921611> IP is invalid",
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
                            Description = "Server is loading!",
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
                            Description = $"Players {Ping.CurrentPlayers}/{Ping.MaximumPlayers}",
                            Footer = new EmbedFooterBuilder()
                            {
                                Text = Ping.Motd.Replace("§a", "").Replace("§1", "").Replace("§2", "").Replace("§3", "").Replace("§4", "").Replace("§5", "").Replace("§6", "").Replace("§7", "").Replace("§8", "").Replace("§9", "").Replace("§b", "").Replace("§c", "").Replace("§d", "").Replace("§e", "").Replace("§f", "").Replace("§l", "")
                            }
                        };
                        if (Ping.Version.Contains("BungeeCord"))
                        {
                            embed.WithDescription(embed.Description + Environment.NewLine + "Servers running with BungeeCord will not get the right player count for a single server");
                        }
                        await Info.DeleteAsync();
                        await ReplyAsync("", false, embed.Build());
                    }
                }
                else
                {
                    var embed = new EmbedBuilder()
                    {
                        Description = "<:warning:350172481757118478> Server is offline",
                        Color = new Color(255, 165, 0)
                    };
                    await Info.DeleteAsync();
                    await ReplyAsync("", false, embed.Build());
                }
            });
        }

        [Command("list"), Remarks("list"), Summary("List guild MC servers")]
        [Alias("servers")]
        public async Task List()
        {
            if (Context.Guild == null)
            {
                await ReplyAsync("This command can only be used in a guild");
                return;
            }
            _Guild Guild = _Config.MCGuilds.Find(x => x.ID == Context.Guild.Id);
            if (Guild == null)
            {
                await ReplyAsync("Could not find guild");
                return;
            }
            if (Guild.Servers.Count == 0)
            {
                await ReplyAsync("This guild has no servers listed :(" + Environment.NewLine + "Guild administrators should use `mc/admin`");
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
                    Name = $"{Name} Servers",
                    IconUrl = Context.Guild.IconUrl
                },
                Color = Bot.Utils.DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
                Description = "```md" + Environment.NewLine + string.Join(Environment.NewLine, Servers) + "```"
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("info"), Remarks("info"), Summary("MC sales info")]
        public async Task Info()
        {
            StatisticsResponse stats = await new Statistics(Item.MinecraftAccountsSold).PerformRequest();
            if (stats.IsSuccess)
            {
                var embed = new EmbedBuilder()
                {
                    Author = new EmbedAuthorBuilder()
                    {
                        Name = "Minecraft Account Sales",
                        Url = "https://minecraft.net/en-us/stats/"
                    },
                    Color = Bot.Utils.DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
                    Description = $"Total: {stats.Total}" + Environment.NewLine + $"24 Hours: {stats.Last24h}" + Environment.NewLine + $"Average Per Second: {stats.SaleVelocity}",
                    Footer = new EmbedFooterBuilder()
                    {
                        Text = "Stats may be slightly off due to caching"
                    }
                };
                await ReplyAsync("", false, embed.Build());
            }
            else
            {
                await ReplyAsync("API Error");
            }
        }

        [Command("skin"), Remarks("skin (Player)"), Summary("Player skin")]
        public async Task SkinArg(string Arg = null, [Remainder] string User = null)
        {
            if (Arg == null)
            {
                await Context.Channel.SendMessageAsync("mc/skin (Arg) (User) | head | cube | full | steal | `mc/skin Notch` or `mc/skin cube Notch`");
                return;
            }
            if (User == null)
            {
                User = Arg;
                Arg = "full";
            }
            UuidAtTimeResponse uuid = new UuidAtTime(User, DateTime.Now).PerformRequest().Result;
            if (!uuid.IsSuccess)
            {
                await ReplyAsync($"Player `{User}` not found");
                return;
            }
            string Url = "";
                switch (Arg.ToLower())
            {
                case "head":
                    Url = "https://visage.surgeplay.com/face/100/" + User;
                    break;
                case "cube":
                    Url = "https://visage.surgeplay.com/head/100/" + User;
                    break;
                case "full":
                    Url = "https://visage.surgeplay.com/full/200/" + User;
                    break;
                case "steal":
                    Url = "steal";
                    await ReplyAsync($"{Context.User.Username} Stole a skin :o <https://minotar.net/download/" + User + ">");
                    break;
                default:
                    await Context.Channel.SendMessageAsync("Unknown argument do mc/skin");
                    return;
            }
            if (Url == "steal")
            {
                return;
            }
            if (Url == "")
            {
                await Context.Channel.SendMessageAsync("Unknown argument do mc/skin");
                return;
            }
            var embed = new EmbedBuilder()
            {
                Color = Bot.Utils.DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
                ImageUrl = Url
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("names"), Remarks("names (Player)"), Summary("MC account name history")]
        public async Task Names([Remainder]string Name)
        {
            UuidAtTimeResponse uuid = new UuidAtTime(Name, DateTime.Now).PerformRequest().Result;
            if (uuid.IsSuccess)
            {
                NameHistoryResponse names = new NameHistory(uuid.Uuid.Value).PerformRequest().Result;
                if (names.IsSuccess)
                {
                    List<string> Names = new List<string>();
                    if (names.NameHistory.Count == 1)
                    {
                        await ReplyAsync($"Account `{Name}` only has 1 username recorded");
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
                            Names.Add($"[ {entry.Name} ]( First Name )");
                        }
                    }

                    await ReplyAsync("```" + Environment.NewLine + string.Join(Environment.NewLine, Names) + "```");
                }
                else
                {
                    await ReplyAsync("API error");
                }
            }
            else
            {
                await ReplyAsync($"Player `{Name}` not found, please use the current players name");
            }
        }

        [Command("status"), Remarks("status"), Summary("Mojang status")]
        public async Task Status()
        {
            ApiStatusResponse status = new ApiStatus().PerformRequest().Result;
            if (status.IsSuccess)
            {
                var embed = new EmbedBuilder()
                {
                    Title = "Mojang Status",
                    Description = $"Mojang: {status.Mojang}" + Environment.NewLine + $"Minecraft.net: {status.Minecraft}" + Environment.NewLine +
                    $"Mojang accounts: {status.MojangAccounts}" + Environment.NewLine + $"Mojang API: {status.MojangApi}" + Environment.NewLine +
                    $"Mojang auth. servers: {status.MojangAutenticationServers}" + Environment.NewLine + $"Mojang auth. service: {status.MojangAuthenticationService}" + Environment.NewLine +
                    $"Mojang sessions: {status.MojangSessionsServer}" + Environment.NewLine + $"Minecraft.net sessions: {status.Sessions}" + Environment.NewLine +
                    $"Minecraft.net skins: {status.Skins}" + Environment.NewLine + $"Minecraft.net textures: {status.Textures}",
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
                await ReplyAsync("API error");
            }
        }

        [Command("music"), Remarks("music"), Summary("Play some music")]
        public async Task Music()
        {
            
            var embed = new EmbedBuilder()
            {
            };
            embed.AddField("Commands", "```md" + Environment.NewLine + "[ mc/music play (ID) ][ Play a song by ID ]" + Environment.NewLine + "[ mc/music stop ][ Stop the current playing music ]" + Environment.NewLine + "[ mc/music leave ][ Stop current song and leave the voice channel ]```");
            embed.AddField("Playlist", "```md" + Environment.NewLine + "<1 Mine Diamonds>" + Environment.NewLine + "<2 Screw the nether - Yogscast>" + Environment.NewLine + "<3 Mincraft Style - Captain Sparklez>```");
            //await ReplyAsync("", false, embed.Build());
            await ReplyAsync("Coming Soon");
        }

        [Command("get"), Remarks("get (Text)"), Summary("Get an achievement")]
        public async Task Get([Remainder]string Text)
        {
            if (Text.Length > 20)
            {
                await ReplyAsync("Text cannot be more than 20 letters/numbers");
                return;
            }

            Random RNG = new Random();
            int Number = RNG.Next(1, 39);
            var embed = new EmbedBuilder()
            {
                Color = Bot.Utils.DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
                ImageUrl = "https://www.minecraftskinstealer.com/achievement/a.php?i=" + Number.ToString() + "&h=Achievement+Get!&t=" + Text.Replace(" ", "+")
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("skinedit"), Remarks("skinedit"), Summary("Online skin editor")]
        public async Task Misc()
        {
            var embed = new EmbedBuilder()
            {
                Color = Bot.Utils.DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
                Description = "[Online Skin Editor](https://www.minecraftskinstealer.com/skineditor.php)"
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("minime"), Remarks("minime (Player)"), Summary("Minify player skin")]
        public async Task Minime(string Player)
        {
            UuidAtTimeResponse uuid = new UuidAtTime(Player, DateTime.Now).PerformRequest().Result;
            if (uuid.IsSuccess)
            {
                var embed = new EmbedBuilder()
                {
                    Color = Bot.Utils.DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
                    ImageUrl = "https://avatar.yourminecraftservers.com/avatar/trnsp/not_found/tall/128/" + Player + ".png"
                };
                await ReplyAsync("", false, embed.Build());
            }
            else
            {
                await ReplyAsync($"Player `{Player}` not found");
            }
        }

        

        [Command("admin"), Remarks("admin"), Summary("Guild admin commands")]
        public async Task Admin()
        {
            if (Context.Guild == null)
            {
                await ReplyAsync("<:error:350172479936921611> Guild command only");
                return;
            }
            IGuildUser GU = await Context.Guild.GetUserAsync(Context.User.Id);
            if (!GU.GuildPermissions.Administrator)
            {
                await ReplyAsync("<:error:350172479936921611> You are not a guild admin");
                return;
            }
            List<string> Commands = new List<string>();
            foreach (var I in _Commands.Commands.Where(x => x.Module.Name == "GuildAdmin"))
            {
                if (I.Summary == null || I.Summary == "") continue;
                try
                {
                    I.Summary.Trim();
                    Commands.Add($"[ mc/{I.Remarks} ]( {I.Summary} )");
                }
                catch { }
            }
            var embed = new EmbedBuilder()
            {
                Title = "Guild Admin Commands",
                Description = "```md" + Environment.NewLine + $"{string.Join(Environment.NewLine, Commands)}" + Environment.NewLine + "< Use mc/list for a list of this guilds minecraft servers >```",
                Color = DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
                Footer = new EmbedFooterBuilder()
                { Text = ""}
                
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("inviteblahblahlol"), Remarks("invite"), Summary("Get the bot invite")]
        public async Task InvitePlaceholder()
        {

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
            IGuildUser GU = (IGuildUser)Context.User;
            if (GU.VoiceChannel == null)
            {
                await ReplyAsync("You are not in a voice channel");
                return;
            }
            MusicService._MusicPlayer MP = _MusicService.GetMusicPlayer(Context.Guild);
            
            
            string SongPath = "";
            switch(Song)
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
                    }else if (Users.Where(x => !x.IsBot).Count() > 0)
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
        [Command("classic")]
        public async Task Classic()
        {
            var embed = new EmbedBuilder()
            {
                Title = "Minecraft Classic",
                Description = "[Wiki](https://minecraft.gamepedia.com/Classic) Minecraft classic was the second phase of developent in 2009 that allowed players to play in the browser using java on the minecraft.net website which was primarly used to build things",
                Color = DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
                Footer = new EmbedFooterBuilder()
                {
                    Text = "Hey you found a secret command :D"
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
            var embed = new EmbedBuilder()
            {
                Description = "Forgecraft is the set of private whitelisted servers for mod developers to gather and beta-test their mods in a private environment. Many YouTubers and live-streamers also gather on the server to interact with the mod developers, help play-test the mods, and create videos to let the general public know what the mod developers are doing." + Environment.NewLine +
                "[Wiki And Forgecraft Users](http://feed-the-beast.wikia.com/wiki/Forgecraft) | [Wallpaper](http://www.minecraftforum.net/forums/show-your-creation/fan-art/other-fan-art/1582624-forgecraft-wallpaper)",
                Color = Bot.Utils.DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
                Footer = new EmbedFooterBuilder()
                {
                    Text = "Hey you found a secret command :D"
                }
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("forgecraftwallpaper")]
        public async Task ForgecraftWallpaper()
        {
            var embed = new EmbedBuilder()
            {
                Author = new EmbedAuthorBuilder()
                {
                    Name = "Forgecraft Wallpaper",
                    Url = "http://www.minecraftforum.net/forums/show-your-creation/fan-art/other-fan-art/1582624-forgecraft-wallpaper"
                },
                ImageUrl = "https://dl.dropbox.com/u/25591134/ForgeCraft/ForgeCraft-480x270.jpg",
                Color = DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
                Footer = new EmbedFooterBuilder()
                {
                    Text = "Hey you found a secret command :D"
                }
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("bukkit")]
        public async Task Bukkit()
        {
            var embed = new EmbedBuilder()
            {
                Color = Bot.Utils.DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
                Description = "RIP Bukkit you will be missed along with other server solutions.... [Bukkit News](https://bukkit.org/threads/bukkit-its-time-to-say.305106/)",
                Footer = new EmbedFooterBuilder()
                {
                    Text = "Hey you found a secret command :D"
                }
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("direwolf20")]
        public async Task DW()
        {
            var embed = new EmbedBuilder()
            {
                Color = Bot.Utils.DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
                Description = "Direwolf20 is a popular youtuber known for his lets plays and mod tutorials on modded minecraft. He also plays on a private server called Forgecraft with a bunch of mod developers and other youtubers with his friends Soaryn and Pahimar" + Environment.NewLine + "[Youtube](https://www.youtube.com/channel/UC_ViSsVg_3JUDyLS3E2Un5g) | [Twitch](https://www.twitch.tv/direwolf20) | [Twitter](https://twitter.com/Direwolf20) | [Reddit](https://www.reddit.com/r/DW20/) | [Discord](https://discordapp.com/invite/SQ6wjHg)",
                Footer = new EmbedFooterBuilder()
                {
                    Text = "Hey you found a secret command :D"
                }
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("herobrine")]
        public async Task Herobrine()
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
                Color = DiscordUtils.GetRoleColor(Context.Channel as ITextChannel)
            };
            await ReplyAsync("", false, embedh.Build());
        }

        [Command("entity303")]
        public async Task Entity303()
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
                Color = DiscordUtils.GetRoleColor(Context.Channel as ITextChannel)
            };
            await ReplyAsync("", false, embedh.Build());
        }

        [Command("israphel")]
        public async Task IS()
        {
            var embed = new EmbedBuilder()
            {
                Color = Bot.Utils.DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
                Description = "The best youtube minecraft series that will never die in our hearts... 2010 - 2012 RIP [Youtube](https://www.youtube.com/playlist?list=PLF60520313D07F366)",
                Footer = new EmbedFooterBuilder()
                {
                    Text = "Hey you found a secret command :D"
                }
            };
            await ReplyAsync("", false, embed.Build());
        }

        [Command("notch")]
        public async Task Notch()
        {
            var embed = new EmbedBuilder()
            {
                Description = "Minecraft was created by Notch aka Markus Persson" + Environment.NewLine + "https://en.wikipedia.org/wiki/Markus_Persson",
                Color = DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
                Footer = new EmbedFooterBuilder()
                {
                    Text = "Hey you found a secret command :D"
                }
            };
            await ReplyAsync("", false, embed.Build());
        }
    }

    public class GuildAdmin : ModuleBase
    {
        [Command("addserver"), Remarks("addserver"), Summary("Add a MC server to this guild list")]
        public async Task Addserver(string Tag = "", string IP = "", [Remainder]string Name = "")
        {
            if (Context.Guild == null)
            {
                await ReplyAsync("<:error:350172479936921611> Guild command only");
                return;
            }
            IGuildUser GU = await Context.Guild.GetUserAsync(Context.User.Id);
            if (!GU.GuildPermissions.Administrator)
            {
                await ReplyAsync("<:error:350172479936921611> You are not a guild admin");
                return;
            }
            if (Tag == "" || IP == "" || Name == "")
            {
                await ReplyAsync("Enter a tag, ip and name | `mc/addserver (Tag) (IP) (Name)` | `mc/addserver sf sky.minecraft.net Skyfactory 2`");
                return;
            }
            _Guild Guild = _Config.MCGuilds.Find(x => x.ID == Context.Guild.Id);
            if (Guild == null)
            {
                await ReplyAsync("Could not find guild data contact xXBuilderBXx#9113");
                return;
            }
            _Server Server = Guild.Servers.Find(x => x.Tag.ToLower() == Tag.ToLower());
                if (Server != null)
                {
                    await ReplyAsync("This server already exists on the list");
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
                await ReplyAsync($"Added server {Name} to the guild list | `mc/list`");
            
        }

        [Command("delserver"), Remarks("delserver"), Summary("Remove a MC server from this guild list")]
        public async Task Delserver(string Tag = "")
        {
            if (Context.Guild == null)
            {
                await ReplyAsync("<:error:350172479936921611> Guild command only");
                return;
            }
            IGuildUser GU = await Context.Guild.GetUserAsync(Context.User.Id);
            if (!GU.GuildPermissions.Administrator)
            {
                await ReplyAsync("<:error:350172479936921611> You are not a guild admin");
                return;
            }
            if (Tag == "")
            {
                await ReplyAsync("Enter the tag of a server to delete from the list | `mc/delserver (Tag)` | `mc/delserver sf`");
                return;
            }
            _Guild Guild = _Config.MCGuilds.Find(x => x.ID == Context.Guild.Id);
            if (Guild == null)
            {
                await ReplyAsync("Could not find guild data contact xXBuilderBXx#9113");
                return;
            }
                _Server Server = Guild.Servers.Find(x => x.Tag.ToLower() == Tag.ToLower());
                if (Server == null)
                {
                    await ReplyAsync("This server is not on the list");
                    return;
                }
                Guild.Servers.Remove(Server);
                _Task.SaveGuild(Context.Guild.Id);
                await ReplyAsync($"Removed server {Server.Name} from the guild list | `mc/list`");
            
        }

        [Command("setname"), Remarks("setname (Name)"), Summary("Set the community name")]
        public async Task SetName([Remainder]string Name = "")
        {
            if (Context.Guild == null)
            {
                await ReplyAsync("<:error:350172479936921611> Guild command only");
                return;
            }
            IGuildUser GU = await Context.Guild.GetUserAsync(Context.User.Id);
            if (!GU.GuildPermissions.Administrator)
            {
                await ReplyAsync("<:error:350172479936921611> You are not a guild admin");
                return;
            }
            _Guild Guild = _Config.MCGuilds.Find(x => x.ID == Context.Guild.Id);
            if (Guild == null)
            {
                await ReplyAsync("Could not find guild data contact xXBuilderBXx#9113");
                return;
            }
            if (Name == "")
            {
                await ReplyAsync("<:error:350172479936921611> You need to enter a name mc/setname (Name) | mc/setname Mineplex");
                return;
            }
            Guild.CommunityName = Name;
            _Task.SaveGuild(Context.Guild.Id);
            await ReplyAsync("<:success:350172481186955267> Community name has been set");
        }

        [Command("setdesc"), Remarks("setdesc (Text)"), Summary("Set the community description")]
        public async Task SetDesc([Remainder]string Desc = "")
        {
            if (Context.Guild == null)
            {
                await ReplyAsync("<:error:350172479936921611> Guild command only");
                return;
            }
            IGuildUser GU = await Context.Guild.GetUserAsync(Context.User.Id);
            if (!GU.GuildPermissions.Administrator)
            {
                await ReplyAsync("<:error:350172479936921611> You are not a guild admin");
                return;
            }
            _Guild Guild = _Config.MCGuilds.Find(x => x.ID == Context.Guild.Id);
            if (Guild == null)
            {
                await ReplyAsync("Could not find guild data contact xXBuilderBXx#9113");
                return;
            }
            if (Guild.CommunityName == "")
            {
                await ReplyAsync("<:error:350172479936921611> Community name has not been set | mc/setname (Name)");
                return;
            }
            if (Desc == "")
            {
                await ReplyAsync("<:error:350172479936921611> You need to enter a description mc/setname (Text) | mc/setname Best minecraft community");
                return;
            }
            Guild.CommunityDescription = Desc;
            _Task.SaveGuild(Context.Guild.Id);
            await ReplyAsync("<:success:350172481186955267> Community description has been set");
        }

        [Command("setlink"), Remarks("setlink (Link)"), Summary("Set the community link")]
        public async Task SetLink(string Link = "")
        {
            if (Context.Guild == null)
            {
                await ReplyAsync("<:error:350172479936921611> Guild command only");
                return;
            }
            IGuildUser GU = await Context.Guild.GetUserAsync(Context.User.Id);
            if (!GU.GuildPermissions.Administrator)
            {
                await ReplyAsync("<:error:350172479936921611> You are not a guild admin");
                return;
            }
            _Guild Guild = _Config.MCGuilds.Find(x => x.ID == Context.Guild.Id);
            if (Guild == null)
            {
                await ReplyAsync("Could not find guild data contact xXBuilderBXx#9113");
                return;
            }
            if (Guild.CommunityName == "")
            {
                await ReplyAsync("<:error:350172479936921611> Community name has not been set | mc/setname (Name)");
                return;
            }
            if (Link == "")
            {
                await ReplyAsync("<:error:350172479936921611> You need to enter a link mc/setlink (Link) | mc/setname https://minecraftpro.com");
                return;
            }
            Guild.Website = Link;
            _Task.SaveGuild(Context.Guild.Id);
            await ReplyAsync("<:success:350172481186955267> Community link has been set");
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
                ThumbnailUrl = "http://lolis.ml/mcitems/" + ID + "-" + Meta + ".png",
                Color = Bot.Utils.DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
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
                        Color = DiscordUtils.GetRoleColor(Context.Channel as ITextChannel)
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
                        Color = DiscordUtils.GetRoleColor(Context.Channel as ITextChannel)
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
                        Color = DiscordUtils.GetRoleColor(Context.Channel as ITextChannel)
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
                embed.AddField("Passive", string.Join(" ", Passive),true);
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
                    Color = DiscordUtils.GetRoleColor(Context.Channel as ITextChannel)
                };
                await ReplyAsync("", false, embed.Build());
            }
            else if (Name.ToLower() == "all")
            {
                List<string> Potions = new List<string>();
                foreach(_Potion Potion in _Config.MCPotions)
                {
                    if (Potion.Extended != null && Potion.Level2 != null)
                    {
                        Potions.Add($"<{Potion.Base} + {Potion.Ingredient} = {Potion.Name.Replace(" ", "")}> II ⏳");
                    }
                    else if (Potion.Level2 != null)
                    {
                        Potions.Add($"<{Potion.Base} + {Potion.Ingredient} = {Potion.Name.Replace(" ", "")}> II");
                    }else
                    {
                        Potions.Add($"<{Potion.Base} + {Potion.Ingredient} = {Potion.Name.Replace(" ", "")}> ⏳");
                    }
                }
                var embed = new EmbedBuilder()
                {
                    Color = DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
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
                        Color = DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
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
                    Color = DiscordUtils.GetRoleColor(Context.Channel as ITextChannel)
                };
                embed.AddField("Armor", "Protection | FireProt | Feather Falling | Blast Prot | Projectile Prot | Respiration | Aqua Affinity | Thorns | Depth Strider | Frost Walker");
                embed.AddField("Weapons", "Sharpness | Smite | Arthropods | Knockback | Fire Aspect | Looting");
                embed.AddField("Bows", "Power | Punch | Infinity | Flame");
                embed.AddField("Tools", "Efficiency | Silk Touch | Fortune");
                embed.AddField("Fishing Rod", "Luck Of The Sea | Lure");
                embed.AddField("All", "Unbreaking Mending");
                await ReplyAsync("", false, embed.Build());
            }
            else
            {
                _Enchant Enchant = _Config.MCEnchantments.Find(x => x.Name.ToLower().Replace(" ", "").Contains(Name.ToLower().Replace(" ", "")));
                var embed = new EmbedBuilder()
                {
                    Title = $"[{Enchant.ID}] {Enchant.Name}",
                    Description = "```md" +Environment.NewLine + $"<Version {Enchant.Version}> <Type {Enchant.Type}> <MaxLevel {Enchant.MaxLevel}>" + Environment.NewLine + $"<Note {Enchant.Note}>```",
                    ThumbnailUrl = Enchant.GetEnchantItem(),
                    Color = DiscordUtils.GetRoleColor(Context.Channel as ITextChannel)
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
                string User = $"{Context.User.Username}#{Context.User.Discriminator}";
                var qembed = new EmbedBuilder()
                {
                    Title = $"Question - {User}",
                    Description = Quiz.Question,
                    Color = new Color(135, 206, 235)
                };
                await ReplyAsync("", false, qembed);
                var response = await WaitForMessage(Context.Message.Author, Context.Channel, new TimeSpan(0, 0, 10));
                if (response == null)
                {
                    var embed = new EmbedBuilder()
                    {
                        Title = "Minecraft Quiz",
                        Description = $"{User} you ran out of time :(",
                        Color = new Color(200, 0, 0)
                    };
                    await ReplyAsync("", false, embed);
                }
                else
                {
                    if (Quiz.Answer.Contains(response.Content.ToLower()))
                    {
                        var embed = new EmbedBuilder()
                        {
                            Title = "Minecraft Quiz",
                            Description = $"<:success:350172481186955267> Correct! {User}" + Environment.NewLine + Quiz.Note,
                            Color = new Color(0,200,0)
                        };
                        await ReplyAsync("", false, embed);
                    }
                    else
                    {
                        var embed = new EmbedBuilder()
                        {
                            Title = "Minecraft Quiz",
                            Description = $"<:error:350172479936921611> Incorrect {User}",
                            Color = new Color(200, 0, 0)
                        };
                        await ReplyAsync("", false, embed);
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
                await ReplyAsync("", false, embed);
            }
        }
    }
}