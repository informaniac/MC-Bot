using Bot.Classes;
using Bot.Functions;
using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Services
{
    public class GuildCheck
    {
        DiscordSocketClient _Client;
        public GuildCheck(DiscordSocketClient Client)
        {
            _Client = Client;
            _Task.LoadGuilds();
            _Client.GuildAvailable += _Client_GuildAvailable;
            _Client.JoinedGuild += _Client_JoinedGuild;
        }

        private Task _Client_JoinedGuild(SocketGuild g)
        {
            if (_Bot._Blacklist.Check(g.Id))
            {
                var Guild = _Config.MCGuilds.Find(x => x.ID == g.Id);
                if (Guild == null)
                {
                    _Task.NewGuild(g.Id);
                }
            }
            return Task.CompletedTask;
        }

        private Task _Client_GuildAvailable(SocketGuild g)
        {
            if (_Bot._Blacklist.Check(g.Id))
            {
                var Guild = _Config.MCGuilds.Find(x => x.ID == g.Id);
                if (Guild == null)
                {
                    _Task.NewGuild(g.Id);
                }
            }
            return Task.CompletedTask;
        }
    }

}
namespace Bot.Functions
{
    public class _Task
    {
        public static void NewGuild(ulong ID)
        {
            _Guild NewConfig = new _Guild()
            {
                ID = ID, Servers = new List<_Server>(), Website = ""
            };
            using (StreamWriter file = File.CreateText(_Config.BotPath + $"Guilds/{ID}" + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, NewConfig);

            }
            _Config.MCGuilds.Add(NewConfig);
        }
        public static void SaveGuild(ulong ID)
        {
            _Guild Guild = _Config.MCGuilds.Find(x => x.ID == ID);
            if (Guild != null)
            {
                using (StreamWriter file = File.CreateText(_Config.BotPath + $"Guilds/{ID}" + ".json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, Guild);

                }
            }
        }
        public static void LoadGuilds()
        {
            foreach(var i in Directory.GetFiles(_Config.BotPath + "Guilds/"))
            {
                using (StreamReader reader = new StreamReader(i))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    _Config.MCGuilds.Add((_Guild)serializer.Deserialize(reader, typeof(_Guild)));
                }
            }
        }
    }
}
namespace Bot.Apis
{
    public class MineStat
    {
        const ushort dataSize = 512; // this will hopefully suffice since the MotD should be <=59 characters
        const ushort numFields = 6;  // number of values expected from server

        public string Address { get; set; }
        public ushort Port { get; set; }
        public string Motd { get; set; }
        public string Version { get; set; }
        public string CurrentPlayers { get; set; }
        public string MaximumPlayers { get; set; }
        public bool ServerUp { get; set; }
        public long Delay { get; set; }

        public MineStat(string address, ushort port)
        {
            var rawServerData = new byte[dataSize];

            Address = address;
            Port = port;

            try
            {
                // ToDo: Add timeout
                var stopWatch = new Stopwatch();
                var tcpclient = new TcpClient();
                stopWatch.Start();
                tcpclient.Connect(address, port);
                stopWatch.Stop();
                var stream = tcpclient.GetStream();
                var payload = new byte[] { 0xFE, 0x01 };
                stream.Write(payload, 0, payload.Length);
                stream.Read(rawServerData, 0, dataSize);
                tcpclient.Close();
                Delay = stopWatch.ElapsedMilliseconds;
            }
            catch (Exception)
            {
                ServerUp = false;
                return;
            }

            if (rawServerData == null || rawServerData.Length == 0)
            {
                ServerUp = false;
            }
            else
            {
                var serverData = Encoding.Unicode.GetString(rawServerData).Split("\u0000\u0000\u0000".ToCharArray());
                if (serverData != null && serverData.Length >= numFields)
                {
                    ServerUp = true;
                    Version = serverData[2];
                    Motd = serverData[3];
                    CurrentPlayers = serverData[4];
                    MaximumPlayers = serverData[5];
                }
                else
                {
                    ServerUp = false;
                }
            }
        }

        #region Obsolete

        [Obsolete]
        public string GetAddress()
        {
            return Address;
        }

        [Obsolete]
        public void SetAddress(string address)
        {
            Address = address;
        }

        [Obsolete]
        public ushort GetPort()
        {
            return Port;
        }

        [Obsolete]
        public void SetPort(ushort port)
        {
            Port = port;
        }

        [Obsolete]
        public string GetMotd()
        {
            return Motd;
        }

        [Obsolete]
        public void SetMotd(string motd)
        {
            Motd = motd;
        }

        [Obsolete]
        public string GetVersion()
        {
            return Version;
        }

        [Obsolete]
        public void SetVersion(string version)
        {
            Version = version;
        }

        [Obsolete]
        public string GetCurrentPlayers()
        {
            return CurrentPlayers;
        }

        [Obsolete]
        public void SetCurrentPlayers(string currentPlayers)
        {
            CurrentPlayers = currentPlayers;
        }

        [Obsolete]
        public string GetMaximumPlayers()
        {
            return MaximumPlayers;
        }

        [Obsolete]
        public void SetMaximumPlayers(string maximumPlayers)
        {
            MaximumPlayers = maximumPlayers;
        }

        [Obsolete]
        public bool IsServerUp()
        {
            return ServerUp;
        }

        #endregion
    }
}
namespace Bot.Classes
{
    public class _Server
    {
        public string Tag;
        public string Name;
        public string Ip;
        public ushort Port;
    }
    public class _Item
    {
        public string ID;
        public string Meta;
        public string Name;
        public string Text;
    }
    public class _Guild
    {
        public ulong ID;
        public string Website;
        public List<_Server> Servers = new List<_Server>();
    }
}
