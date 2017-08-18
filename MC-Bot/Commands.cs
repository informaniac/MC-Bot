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

namespace Bot.Commands
{
    public class Main : ModuleBase
    {
        private CommandService _Commands;
        public Main(CommandService Commands)
        {
            _Commands = Commands;
        }

        [Command("help")]
        public async Task Help()
        {
            if (Context.Guild != null)
            {
                IGuildUser GU = await Context.Guild.GetUserAsync(Context.Client.CurrentUser.Id);
                if (!GU.GuildPermissions.EmbedLinks)
                {
                    await ReplyAsync("This bot requires permission `Embed Links`");
                    return;
                }
            }
            List<string> Commands = new List<string>();
            foreach(var I in _Commands.Commands)
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
                Title = "Commands",
                Description = "```md" + Environment.NewLine + string.Join(Environment.NewLine, Commands) + "```",
                Color = Bot.Utils.DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
                Footer = new EmbedFooterBuilder()
                {
                    Text = "There are some hidden commands aswell ;)"
                }
            };
            embed.AddField("Launchers", "[MultiMC](https://multimc.org/) Manage and launch multiple versions/instances and easy forge/mods install" + Environment.NewLine + "[Ftb Legacy](http://ftb.cursecdn.com/FTB2/launcher/FTB_Launcher.exe) | [TechnicPack](https://www.technicpack.net/download) | [AT](https://www.atlauncher.com/downloads)");
            await ReplyAsync("", false, embed);
        }

        [Command("colors"), Remarks("colors"), Summary("Minecraft color codes")]
        public async Task Colors()
        {
            var embed = new EmbedBuilder()
            {
                Title = "Minecraft Color Codes",
                ImageUrl = "https://lolis.ml/img-1o4ubn88Z474.png"
            };
            await ReplyAsync("", false, embed);
        }

        [Command("item"), Remarks("item (ID/Name)"), Summary("Minecraft item/block info")]
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
            await ReplyAsync("", false, embed);
        }


        [Command("uuid"), Remarks("uuid (Player)"), Summary("Get a players UUID")]
        public async Task Uuid([Remainder]string Name)
        {
            UuidAtTimeResponse uuid = new UuidAtTime(Name, DateTime.Now).PerformRequest().Result;
            if (uuid.IsSuccess)
            {
                await ReplyAsync($"UUID for `{Name}` > `{uuid.Uuid.Value}`");
            }
            else
            {
                await ReplyAsync($"Cannot find player `{Name}`");
            }

        }

        [Command("ping"), Priority(0), Remarks("ping (IP)"), Summary("Ping a minecraft server")]
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
                    }
                }
            }
            try
            {
                Ping PingTest = new Ping();
                PingReply PingReply = PingTest.Send(IP);
                if (PingReply.Status != IPStatus.Success)
                {
                    await ReplyAsync("IP is invalid");
                    return;
                }
            }
            catch (PingException)
            {
                await ReplyAsync("IP is invalid");
                return;
            }
            MineStat Ping = new MineStat(IP, Port);
            if (Ping.ServerUp)
            {
                var embed = new EmbedBuilder()
                {
                    Title = $"[{Ping.Version}] {IP}:{Port}",
                    Color = new Color(0,200, 0),
                    Description = $"Players {Ping.CurrentPlayers}/{Ping.MaximumPlayers}",
                    Footer = new EmbedFooterBuilder()
                    {
                        Text = Ping.Motd
                    }
                };
                await ReplyAsync("", false, embed);
            }
            else
            {
                var embed = new EmbedBuilder()
                {
                    Description = "Server is offline",
                    Color = new Color(200, 0, 0)
                };
                await ReplyAsync("", false, embed);
            }
            
        }

        [Command("list"), Remarks("list"), Summary("List this guilds minecraft servers")]
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
                await ReplyAsync("This guild has no servers listed :(" + Environment.NewLine + "Guild owner should use `mc/addserver`");
                return;
            }
            List<string> Servers = new List<string>();
            foreach (var i in Guild.Servers)
            {
                Servers.Add($"{i.Tag} | `{i.Ip}` | {i.Name}");
            }
            var embed = new EmbedBuilder()
            {
                Author = new EmbedAuthorBuilder()
                {
                    Name = $"{Context.Guild.Name} Servers",
                    IconUrl = Context.Guild.IconUrl
                },
                Color = Bot.Utils.DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
                Description = string.Join(Environment.NewLine, Servers)
            };
            await ReplyAsync("", false, embed);
        }

        [Command("info"), Remarks("info"), Summary("Minecraft sales info")]
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
                await ReplyAsync("", false, embed);
            }
            else
            {
                await ReplyAsync("API Error");
            }
        }

        [Command("skin"), Remarks("skin (Player)"), Summary("Show a players minecraft skin")]
        public async Task SkinArg(string Arg = null, [Remainder] string User = null)
        {
            if (Arg == null)
            {
                await Context.Channel.SendMessageAsync("mc/skin (Arg) (User) | head | cube | full | steal | `mc/skin full Notch`");
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
                    await Context.Channel.SendMessageAsync("Unknown argument do /skin");
                    break;
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
            await ReplyAsync("", false, embed);
        }

        [Command("names"), Remarks("names (Player)"), Summary("Minecraft account name history")]
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
                            Names.Add($"{entry.Name} ({entry.ChangedToAt})");
                        }
                        else
                        {
                            Names.Add($"{entry.Name}");
                        }
                    }

                    await ReplyAsync(string.Join(Environment.NewLine, Names));
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

        [Command("status"), Remarks("status"), Summary("Mojang status e.g. auth server")]
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
                await ReplyAsync("", false, embed);
            }
            else
            {
                await ReplyAsync("API error");
            }
        }

        [Command("addserver"), Remarks("addserver"), Summary("Add a minecraft server to this guild list")]
        public async Task Addserver(string Tag = "", string IP = "", [Remainder]string Name = "")
        {
            if (Context.Guild == null)
            {
                await ReplyAsync("This command can only be used in a guild");
                return;
            }
            if (Context.User.Id != Context.Guild.OwnerId)
            {
                await ReplyAsync("Guild owner only command");
                return;
            }
            if (Tag == "" || IP == "" || Name == "")
            {
                await ReplyAsync("Enter a tag, ip and name | `mc/addserver (Tag) (IP) (Name)` | `mc/addserver sf sky.minecraft.net Skyfactory 2`");
                return;
            }
            _Guild Guild = _Config.MCGuilds.Find(x => x.ID == Context.Guild.Id);
            if (Guild != null)
            {
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
        }

        [Command("delserver"), Remarks("delserver"), Summary("Remove a minecraft server from the guild list")]
        public async Task Delserver(string Tag = "")
        {
            if (Context.Guild == null)
            {
                await ReplyAsync("This command can only be used in a guild");
                return;
            }
            if (Context.User.Id != Context.Guild.OwnerId)
            {
                await ReplyAsync("Guild owner only command");
                return;
            }
            if (Tag == "")
            {
                await ReplyAsync("Enter the tag of a server to delete from the list | `mc/delserver (Tag)` | `mc/delserver sf`");
                return;
            }
            _Guild Guild = _Config.MCGuilds.Find(x => x.ID == Context.Guild.Id);
            if (Guild != null)
            {
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
            await ReplyAsync("", false, embed);
        }

        [Command("get"), Remarks("get (Text)"), Summary("Get an achievement")]
        public async Task Get([Remainder]string Text)
        {
            Random RNG = new Random();
            int Number = RNG.Next(1, 39);
            var embed = new EmbedBuilder()
            {
                Color = Bot.Utils.DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
                ImageUrl = "https://www.minecraftskinstealer.com/achievement/a.php?i=" + Number.ToString() + "&h=Achievement+Get!&t=" + Text.Replace(" ", "+")
            };
            await ReplyAsync("", false, embed);
        }

        [Command("misc"), Remarks("misc"), Summary("Misc links for minecraft")]
        public async Task Misc()
        {
            var embed = new EmbedBuilder()
            {
                Color = Bot.Utils.DiscordUtils.GetRoleColor(Context.Channel as ITextChannel),
                Description = "[Online Skin Editor](https://www.minecraftskinstealer.com/skineditor.php)"
            };
            await ReplyAsync("", false, embed);
        }

        [Command("minime"), Remarks("minime (Player)"), Summary("Minify yourself")]
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
                await ReplyAsync("", false, embed);
            }
            else
            {
                await ReplyAsync($"Player `{Player}` not found");
            }
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
            await ReplyAsync("", false, embed);
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
            await ReplyAsync("", false, embed);
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
            await ReplyAsync("", false, embed);
        }
    }
}