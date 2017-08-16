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

namespace Bot.Commands
{
    public class Main : ModuleBase
    {
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
            
            var embed = new EmbedBuilder()
            {
                Title = "Commands",
                Description = "mc/ping (IP) | `Ping a minecraft server`" + Environment.NewLine + "mc/skin (Arg) (Name) | `Get a players skin`" + Environment.NewLine + "mc/list | `List of this guilds minecraft servers`" + Environment.NewLine + "mc/info | `Info about minecraft`" + Environment.NewLine + $"mc/names (Name) | `Account name history`" + Environment.NewLine + $"mc/status | `Mojang service status`" + Environment.NewLine + "mc/uuid (Name) | `Uuid of a player`" + Environment.NewLine + "mc/item (Name) | `Lookup a minecraft item/block`" + Environment.NewLine + "mc/colors | `Minecraft color codes`" + Environment.NewLine + "mc/invite | `Invite this bot to your guild`"
            };
            await ReplyAsync("", false, embed);
        }
        [Command("colors")]
        public async Task Colors()
        {
            var embed = new EmbedBuilder()
            {
                Title = "Minecraft Color Codes",
                ImageUrl = "https://lolis.ml/img-1o4ubn88Z474.png"
            };
            await ReplyAsync("", false, embed);
        }

        [Command("item")]
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
                Footer = new EmbedFooterBuilder()
                { Text = $"/give {Context.User.Username} {Item.Text}" }
            };
            await ReplyAsync("", false, embed);
        }


        [Command("uuid")]
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

        [Command("ping"), Priority(0)]
        public async Task Ping(string IP = "", ushort Port = 25565)
        {
            if (IP == "" || IP.Contains("("))
            {
                await ReplyAsync("Enter an IP | `mc/ping my.server.net` | `mc/ping other.server.net:25566` | `mc/ping this.server.net 25567`");
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
                await ReplyAsync("Server is offline");
            }
            
        }

        [Command("list")]
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
                Description = string.Join(Environment.NewLine, Servers)
            };
            await ReplyAsync("", false, embed);
        }

        [Command("info")]
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

        [Command("skin")]
        public async Task SkinArg(string Arg = null, [Remainder] string User = null)
        {
            if (User == null || Arg == null)
            {
                await Context.Channel.SendMessageAsync("mc/skin (Arg) (User) | head | cube | full | steal | `mc/skin full Notch`");
                return;
            }
            UuidAtTimeResponse uuid = new UuidAtTime(User, DateTime.Now).PerformRequest().Result;
            if (!uuid.IsSuccess)
            {
                await ReplyAsync($"Cannot find player `{User}`");
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
                ImageUrl = Url
            };
            await ReplyAsync("", false, embed);
        }

        [Command("names")]
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
                await ReplyAsync($"Cannot find player `{Name}` please use the current player name");
            }
        }

        [Command("status")]
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
                    $"Minecraft.net skins: {status.Skins}" + Environment.NewLine + $"Minecraft.net textures: {status.Textures}"
                };
                await ReplyAsync("", false, embed);
            }
            else
            {
                await ReplyAsync("API error");
            }
        }

        [Command("addserver")]
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

        [Command("delserver")]
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
    }
}