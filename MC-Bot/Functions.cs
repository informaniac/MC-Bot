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
        /// <summary>
        /// Set ID to 0 for no guild
        /// </summary>
        public static void GetGuild(IGuild ID, out _Guild Guild, out int LangInt)
        {
            if (ID != null)
            {
                Guild = _Config.MCGuilds.Find(x => x.ID == ID.Id);
                if (Guild == null)
                {
                    Guild = null;
                    LangInt = 0;
                }
                else
                {
                    LangInt = (int)Guild.Language;
                }
            }
            else
            {
                Guild = null;
                LangInt = 0;
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
    public enum _EnchantType
    {
        Armor, Weapon, Bow, Tool, FishingRod, All
    }
    public enum _EnchantItem
    {
        None, Sword, Helmet, Boot, Bow, FishingRod
    }
    public enum _OreTier
    {
        Wood, Stone, Iron
    }
    public class _Enchant
    {
        public string Name;
        public int ID;
        public int MaxLevel;
        public string Note;
        public _EnchantType Type;
        public string Version;
        public _EnchantItem Item = _EnchantItem.None;
        public string GetEnchantItem()
        {
                switch (Item)
                {
                    case _EnchantItem.FishingRod:
                        return "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/c/c7/Fishing_Rod.png";
                    case _EnchantItem.Bow:
                        return "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/4/46/PE_Bow.png";
                    case _EnchantItem.Boot:
                        return "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/0/0b/Diamond_Boots.png";
                    case _EnchantItem.Sword:
                        return "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/a/a0/Diamond_Sword.png";
                    case _EnchantItem.Helmet:
                        return "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/1/1d/Diamond_Helmet.png";
                    default:
                        return "";
                }
        }
    }
    public class _Ore
    {
        public string Name;
        public _OreTier Tier;
        public string Icon;
    }
    public enum _PotionBase
    {
        Base1, Base2, Base3
    }
    public class _Potion
    {
        public string Name;
        public string Ingredient;
        public _PotionBase Base;
        public _Potion Level2 = null;
        public _Potion Extended = null;
        public string Duration;
        public string Note;
        public string Image;
        public string GetDuration()
        {
            if (Duration == "Instant")
                return "Instant";
            else
                return $"{Duration} Mins";
        }

        public string GetBase()
        {
            if (Base == _PotionBase.Base1)
            {
                return "<NetherWart + Water Bottle = Awkward(Base1) >";
            }
            else if (Base == _PotionBase.Base2)
            {
                return "< Potion of swiftness = (Base2) >";
            }
            else
            {
                return "< Potion of strength = (Base3) >";
            }
        }
    }
    public class _Item
    {
        public string ID;
        public string Meta;
        public string Name;
        public string Text;
    }
    public class _Mob
    {
        public string ID;
        public string EmojiID;
        public string PicUrl;
        public string Name;
        public string Health;
        public string Height;
        public string Width;
        public string Version;
        public string AttackEasy;
        public string AttackNormal;
        public string AttackHard;
        public _MobType Type;
        public string Note;
        public string WikiLink;
    }
    public class _Quiz
    {
        public string Question;
        public string Answer;
        public string Note;
    }
    public class Cooldown
    {
        public int Count;
        public DateTime Date;
    }
    public enum _MobType
    {
        Passive, Neutral, Hostile, Boss, Tameable, Secret
    }
    public enum _Language
    {
        English = 0, French = 1, Spanish = 2
    }
    public class _Guild
    {
        public ulong ID;
        public string CommunityName = "";
        public string CommunityDescription = "";
        public string Website = "";
        public List<_Server> Servers = new List<_Server>();
        public _Language Language = _Language.English;
    }
}
