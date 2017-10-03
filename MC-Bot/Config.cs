using Bot.Classes;
using Bot.Functions;
using Bot.Services;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
            _Client.GuildAvailable += _Client_GuildAvailable;
            _Client.JoinedGuild += _Client_JoinedGuild;
        }

        private async Task _Client_JoinedGuild(SocketGuild g)
        {
            if (!_Bot._Blacklist.Check(g.Id))
            {
                _Config.MCGuilds.TryGetValue(g.Id, out _Guild Guild);
                if (Guild == null)
                {
                    _Task.NewGuild(g.Id);
                    if (_Config.DevMode == false)
                    {
                        var embed = new EmbedBuilder()
                        {
                            Title = ":wave: Hi im Minecraft Bot",
                            Description = "Im packed full of Minecraft commands and special features such as player skins, name history, quiz and some other secret commands." + Environment.NewLine + "Type **mc/help** for a list of commands",
                            Footer = new EmbedFooterBuilder()
                            {
                                Text = "If you have any issues please contact xXBuilderBXx#9113"
                            },
                            Color = new Color(0, 200, 0)
                        };
                        embed.AddField("Language", $"<@{g.Owner.Id}> Use `mc/lang` to change the language ```md" + Environment.NewLine + "< English | Français | Español | Pусский | Português | Deutsche >```", true);
                        await g.DefaultChannel.SendMessageAsync("", false, embed.Build());
                    }
                }
            }
        }

        private Task _Client_GuildAvailable(SocketGuild g)
        {
            if (!_Bot._Blacklist.Check(g.Id))
            {
                _Config.MCGuilds.TryGetValue(g.Id, out _Guild Guild);
                if (Guild == null)
                {
                    _Task.NewGuild(g.Id);
                }
            }
            return Task.CompletedTask;
        }
    }

}
namespace Bot
{
    public class _Config
    {
        public static string BotName = "Minecraft";
        public static string Prefix = "mc/";
        public static string DevPrefix = "tmc/";
        public static string Github = "https://github.com/xXBuilderBXx/MC-Bot";
        public static string BotPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/MC-Bot/";
        public static bool Ready = false;
        public static string Library = ".net V2-00828";
        public static bool DevMode = true;
        public static Class Tokens = new Class();
        
        public static Dictionary<ulong, _Guild> MCGuilds = new Dictionary<ulong, _Guild>();
        public static List<_Item> MCItems = new List<_Item>();
        public static List<_Mob> MCMobs = new List<_Mob>();
        public static List<_Quiz> MCQuiz = new List<_Quiz>();
        public static List<_Potion> MCPotions = new List<_Potion>();
        public static List<_Enchant> MCEnchantments = new List<_Enchant>();
        public static Dictionary<string, _Version> MC_Versions = new Dictionary<string, _Version>();
        public static Dictionary<ulong, Cooldown> PingCooldown = new Dictionary<ulong, Cooldown>();
        public static int Count = 0;

        public static _Trans.Main _TransMain = new _Trans.Main();
        public static _Trans.Hidden _TransHidden = new _Trans.Hidden();
        public static _Trans.Wiki _TransWiki = new _Trans.Wiki();
        public static _Trans.Admin _TransAdmin = new _Trans.Admin();
        public class Class
        {
            public string Discord = "";
        }

        public static IServiceProvider AddServices(_Bot ThisBot, DiscordSocketClient Client, CommandService CommandService)
        {
            _Task.LoadGuilds();
            AddMobs();
            AddQuiz();
            AddPotions();
            AddEnchants();
            AddItems();
            AddVersions();
            return new ServiceCollection()
                .AddSingleton(Client)
                .AddSingleton<_Bot>(ThisBot)
                .AddSingleton(new GuildCheck(Client))
                .AddSingleton(CommandService)
                .AddSingleton<CommandHandler>(new CommandHandler(ThisBot, Client))
                .BuildServiceProvider();
            
        }
        internal static void AddItems()
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
                    _Config.MCItems.Add(new _Item()
                    {
                        ID = GetID,
                        Meta = GetMeta,
                        Name = GetName,
                        Text = GetText
                    });
                }
            }
        }
        internal static void AddEnchants()
        {
            MCEnchantments.Add(new _Enchant() { Name = "Protection", ID = 0, Type = _EnchantType.Armor, Version = "0", Note = "Reduces all damage except for void and hunger damage.", MaxLevel = 4});
            MCEnchantments.Add(new _Enchant() { Name = "Fire Protection", ID = 1, Type = _EnchantType.Armor, Version = "0", Note = "Makes you immune to fire damage.", MaxLevel = 4 });
            MCEnchantments.Add(new _Enchant() { Name = "Feather Falling", Item = _EnchantItem.Boot, ID = 2, Type = _EnchantType.Armor, Version = "0", Note = "Reduces fall damage and ender pearl teleportation damage.", MaxLevel = 4 });
            MCEnchantments.Add(new _Enchant() { Name = "Blast Protection", ID = 3, Type = _EnchantType.Armor, Version = "0", Note = "Reduces explosion damage (15 * level)%. Does not stack with more peices.", MaxLevel = 4 });
            MCEnchantments.Add(new _Enchant() { Name = "Projectile Protection", ID = 4, Type = _EnchantType.Armor, Version = "0", Note = "Reduces projectile damage from arrows, ghast and blaze.", MaxLevel = 4 });
            MCEnchantments.Add(new _Enchant() { Name = "Respiration", Item = _EnchantItem.Helmet, ID = 5, Type = _EnchantType.Armor, Version = "0", Note = "Increases underwater breathing time by +15 seconds * level and improves underwater vision.", MaxLevel = 3 });
            MCEnchantments.Add(new _Enchant() { Name = "Aqua Affinity", Item = _EnchantItem.Helmet, ID = 6, Type = _EnchantType.Armor, Version = "0", Note = "Breaking blocks underwater is allowed at regular speed when not floating.", MaxLevel = 1 });
            MCEnchantments.Add(new _Enchant() { Name = "Thorns", ID = 7, Type = _EnchantType.Armor, Version = "0", Note = "Attacker are damaged when they attack the user. (level * 15)% chance to inflict 0.5-2 hearts of damage and reduces durability.", MaxLevel = 3 });
            MCEnchantments.Add(new _Enchant() { Name = "Depth Strider", Item = _EnchantItem.Boot, ID = 8, Type = _EnchantType.Armor, Version = "1.8", Note = "Increases underwater movement speed and flowing water. Level 3 will make you swim as fast as you walk on land.", MaxLevel = 3 });
            MCEnchantments.Add(new _Enchant() { Name = "Frost Walker", Item = _EnchantItem.Boot, ID = 9, Type = _EnchantType.Armor, Version = "1.9", Note = "Creates ice blocks under you to walk over water and protection from magma blocks.", MaxLevel = 2 });

            MCEnchantments.Add(new _Enchant() { Name = "Sharpness", Item = _EnchantItem.Sword, ID = 16, Type = _EnchantType.Weapon, Version = "0", Note = "Increases melee damage. Level 1 half a heart of damage, Additional levels add 1/4 of a heart of damage.", MaxLevel = 5 });
            MCEnchantments.Add(new _Enchant() { Name = "Smite", ID = 17, Item = _EnchantItem.Sword, Type = _EnchantType.Weapon, Version = "0", Note = "Increases damage to undead mobs such as skeletons, zombies, wither, wither skeletons and zombie pigmen. 1.25 hearts * level of damage added.", MaxLevel = 15 });
            MCEnchantments.Add(new _Enchant() { Name = "Bane Of Arthropods", Item = _EnchantItem.Sword, ID = 18, Type = _EnchantType.Weapon, Version = "0", Note = "Increases damage to spides, cave spiders, silverfish and endermites. It also gives them slowness.", MaxLevel = 15 });
            MCEnchantments.Add(new _Enchant() { Name = "Knockback", Item = _EnchantItem.Sword, ID = 19, Type = _EnchantType.Weapon, Version = "0", Note = "level * 3 = block distance for knockback", MaxLevel = 2 });
            MCEnchantments.Add(new _Enchant() { Name = "Fire Aspect", Item = _EnchantItem.Sword, ID = 20, Type = _EnchantType.Weapon, Version = "0", Note = "Sets the target on fire for 4 seconds.", MaxLevel = 2 });
            MCEnchantments.Add(new _Enchant() { Name = "Looting", Item = _EnchantItem.Sword, ID = 21, Type = _EnchantType.Weapon, Version = "0", Note = "Increases loot of mobs you kill.", MaxLevel = 3 });

            MCEnchantments.Add(new _Enchant() { Name = "Efficiency", ID = 32, Type = _EnchantType.Tool, Version = "0", Note = "Increases mining speed. https://minecraft.gamepedia.com/Breaking#Speed", MaxLevel = 5 });
            MCEnchantments.Add(new _Enchant() { Name = "Silk Touch", ID = 33, Type = _EnchantType.Tool, Version = "0", Note = "Drops blocks instead of items for use on ore blocks, glass, mushroom blocks, ice, mycelium, podzol, ender chests and bookshelfs", MaxLevel = 1 });
            MCEnchantments.Add(new _Enchant() { Name = "Fortune", ID = 35, Type = _EnchantType.Tool, Version = "0", Note = "Increases block drop of coal, diamond, emerald, nether quartz and lapis. Level 1 33% chance, Level 2 25% 2-3 each, Level 20% 2-3-4 each.", MaxLevel = 3 });

            MCEnchantments.Add(new _Enchant() { Name = "Power", Item = _EnchantItem.Bow, ID = 48, Type = _EnchantType.Bow, Version = "0", Note = "Increases arrow damage (25% * level + 1)", MaxLevel = 5 });
            MCEnchantments.Add(new _Enchant() { Name = "Punch", Item = _EnchantItem.Bow, ID = 49, Type = _EnchantType.Bow, Version = "0", Note = "Increases knockback of arrows", MaxLevel = 2 });
            MCEnchantments.Add(new _Enchant() { Name = "Flame", Item = _EnchantItem.Bow, ID = 50, Type = _EnchantType.Bow, Version = "0", Note = "Deals 2 heart of damage over 5 seconds to a target.", MaxLevel = 1 });
            MCEnchantments.Add(new _Enchant() { Name = "Infinity", Item = _EnchantItem.Bow, ID = 51, Type = _EnchantType.Bow, Version = "0", Note = "Allows user to shoot unlimited ammount of arrows if you have 1 arrow in your inventory. Does not work for tipped or spectral arrows.", MaxLevel = 1 });

            MCEnchantments.Add(new _Enchant() { Name = "Luck Of The Sea", Item = _EnchantItem.FishingRod, ID = 61, Type = _EnchantType.FishingRod, Version = "1.8", Note = "Increases fishing loot but lowering junk and increasing treasure catches.", MaxLevel = 3 });
            MCEnchantments.Add(new _Enchant() { Name = "Lure", ID = 62, Item = _EnchantItem.FishingRod, Type = _EnchantType.FishingRod, Version = "1.8", Note = "Decreases waiting time for catching fish by 5 * level.", MaxLevel = 3 });

            MCEnchantments.Add(new _Enchant() { Name = "Unbreaking", ID = 34, Type = _EnchantType.All, Version = "0", Note = "Increases durability of items.", MaxLevel = 3 });
            MCEnchantments.Add(new _Enchant() { Name = "Mending", ID = 70, Type = _EnchantType.All, Version = "0", Note = "Repairs durability of held item with experience. Rate is 2 durability per XP.", MaxLevel = 1 });
        }
        internal static void AddPotions()
        {
            MCPotions.Add(new _Potion() { Name = "Regeneration", Image = "https://vignette2.wikia.nocookie.net/minecraft/images/f/f0/Potion-of-regeneration.png", Base = _PotionBase.Base1, Ingredient = "Ghast Tear", Duration = "0:45", Note = "Restores 9 heart of health over time > half a heart every 2.5 seconds", Extended = new _Potion() { Duration = "1:30", Note = "" }, Level2 = new _Potion() { Duration = "0:22", Note = "Restores 9 heart of health over time > half a heart every 1.25 seconds" } });
            MCPotions.Add(new _Potion() { Name = "Swiftness", Image = "https://vignette2.wikia.nocookie.net/minecraft/images/4/49/Potion-of-swiftness.png", Base = _PotionBase.Base1, Ingredient = "Sugar", Duration = "3:00", Note = "Increases speed by 20%", Extended = new _Potion() { Duration = "8:00", Note = "" }, Level2 = new _Potion() { Duration = "1:30", Note = "Increases speed by 40%" } });
            MCPotions.Add(new _Potion() { Name = "Fire Resistance", Image = "https://vignette3.wikia.nocookie.net/minecraft/images/9/97/Potion-of-fire-resistance.png", Base = _PotionBase.Base1, Ingredient = "Magma Cream", Duration = "3:00", Note = "Immune to fire and lava damage", Extended = new _Potion() { Duration = "8:00", Note = "" } });
            MCPotions.Add(new _Potion() { Name = "Instant Health", Image = "https://vignette2.wikia.nocookie.net/minecraft/images/a/a0/Potion-of-healing.png", Base = _PotionBase.Base1, Ingredient = "Glistering Melon", Duration = "Instant", Note = "Restores 2 hearts of health", Level2 = new _Potion() { Duration = "Instant", Note = "Restores 4 hearts of health" } });
            MCPotions.Add(new _Potion() { Name = "Night Vision", Image = "https://vignette4.wikia.nocookie.net/minecraft/images/4/48/Potion-of-night-vision.png", Base = _PotionBase.Base1, Ingredient = "Golden Carrot", Duration = "3:00", Note = "Allows you to see in the dark",Extended = new _Potion() { Duration = "8:00", Note = "" } });
            MCPotions.Add(new _Potion() { Name = "Strength", Image = "https://vignette1.wikia.nocookie.net/minecraft/images/4/44/Potion-of-strength.png", Base = _PotionBase.Base1, Ingredient = "Blaze Powder", Duration = "3:00", Note = "Increases melee damage by 1.5 hearts", Extended = new _Potion() { Duration = "8:00", Note = "" }, Level2 = new _Potion() { Duration = "1:30", Note = "" } });
            MCPotions.Add(new _Potion() { Name = "Leaping", Image = "https://vignette1.wikia.nocookie.net/minecraft/images/b/b5/Potion-of-leaping.png", Base = _PotionBase.Base1, Ingredient = "Rabbit Foot", Duration = "3:00", Note = "Increases jump height and reduces fall damage",Extended = new _Potion() { Duration = "8:00", Note = "Increases jump height even further" }, Level2 = new _Potion() { Duration = "1:30", Note = "" } });
            MCPotions.Add(new _Potion() { Name = "Water Breathing", Image = "https://vignette1.wikia.nocookie.net/minecraft/images/c/c1/Potion-of-water-breating.png", Base = _PotionBase.Base1, Ingredient = "Pufferfish", Duration = "3:00", Note = "Lets you breath and see better underwater", Extended = new _Potion() { Duration = "8:00", Note = "" } });
            MCPotions.Add(new _Potion() { Name = "Invisibility", Image = "https://vignette3.wikia.nocookie.net/minecraft/images/b/be/Potion-of-invisibility.png", Base = _PotionBase.Base1, Ingredient = "Golden Carrot + Fermented Spider Eye", Duration = "3:00", Note = "Makes player/mob disappear, mobs will act neutral unless player is wearing armor which will still be visible", Extended = new _Potion() { Duration = "8:00", Note = "" } });
            MCPotions.Add(new _Potion() { Name = "Poison", Image = "https://vignette2.wikia.nocookie.net/minecraft/images/d/da/Potion-of-poison.png", Base = _PotionBase.Base1, Ingredient = "Spider Eye", Duration = "0:45", Note = "Deals 18 hearts of damage over time but wont cause death", Extended = new _Potion() { Duration = "1:30", Note = "" }, Level2 = new _Potion() { Duration = "0:22", Note = "" } });
            MCPotions.Add(new _Potion() { Name = "Weakness", Image = "https://vignette4.wikia.nocookie.net/minecraft/images/d/d9/Potion-of-weakness.png", Base = _PotionBase.Base3, Ingredient = "Fermented Spider Eye", Duration = "1:30", Note = "Reduces melee damage by 2 hearts", Extended = new _Potion() { Duration = "4:00", Note = "" } });
            MCPotions.Add(new _Potion() { Name = "Slowness", Image = "https://vignette1.wikia.nocookie.net/minecraft/images/5/55/Potion-of-slowness.png", Base = _PotionBase.Base2, Ingredient = "Sugar + Fermented Spider Eye", Duration = "1:30", Note = "Slows players/mobs by 15%", Extended = new _Potion() { Duration = "4:00", Note = "" } });
            MCPotions.Add(new _Potion() { Name = "Harming", Image = "https://vignette1.wikia.nocookie.net/minecraft/images/9/92/Potion-of-harming.png", Base = _PotionBase.Base1, Ingredient = "Spider Eye + Fermented Spider Eye", Duration = "Instant", Note = "Deals 3 hearts of damage", Level2 = new _Potion() { Duration = "Instant", Note = "Deals 6 hearts of damage to player" } });
        }
        internal static void AddMobs()
        {
            MCMobs.Add(new _Mob() { Name = "Player", EmojiID = "352238802900615178", ID = "0", Health = "10", Height = "1.8", Width = "0.6", Type = _MobType.Passive, Version = "0", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/f/f3/Steve.png/166px-Steve.png", Note = "Main character in the game called Steve" + Environment.NewLine + "or in newer versions Alex", AttackEasy = "", WikiLink = "https://minecraft.gamepedia.com/The_Player" });
            MCMobs.Add(new _Mob() { Name = "Bat", EmojiID = "350864494991245313", ID = "65", Health = "3", Height = "0.9", Width = "0.5", Type = _MobType.Passive, Version = "1.4.2", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/0/09/Bat.gif", Note = "Bats are useless in the game", WikiLink = "https://minecraft.gamepedia.com/Bat" });
            MCMobs.Add(new _Mob() { Name = "ChickenJockey", EmojiID = "350864495108685826", ID = "54", Health = "10", Height = "Rip", Width = "Rip", Type = _MobType.Hostile, Version = "1.7.4", AttackEasy = "1", AttackNormal = "1.5", AttackHard = "2", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/d/d3/Chicken_Jockey.png/150px-Chicken_Jockey.png", Note = "Baby zombie riding a chicken what madness is this!", WikiLink = "https://minecraft.gamepedia.com/Chicken_Jockey" });
            MCMobs.Add(new _Mob() { Name = "Donkey", EmojiID = "350864495113142274", ID = "31", Health = "7.5", Height = "1.6", Width = "1.4", Type = _MobType.Tameable, Version = "1.6.1", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/9/95/Donkey.png/200px-Donkey.png", WikiLink = "https://minecraft.gamepedia.com/Horse", Note = "Can be equiped with a chest to store items" });
            MCMobs.Add(new _Mob() { Name = "Creeper", EmojiID = "350864495129919490", ID = "50", Health = "10", Height = "1.7", Width = "0.6", Type = _MobType.Hostile, Version = "Classic 0.24", AttackEasy = "Max Damage", AttackNormal = "24.5", AttackHard = "Charged 48.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/0/0a/Creeper.png/150px-Creeper.png", Note = "Created from a failed pig model :D", WikiLink = "https://minecraft.gamepedia.com/Creeper" });
            MCMobs.Add(new _Mob() { Name = "Endermite", EmojiID = "350864495171731457", ID = "67", Health = "4", Height = "0.3", Width = "0.4", Type = _MobType.Hostile, Version = "1.8", AttackEasy = "1", AttackNormal = "1", AttackHard = "1.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/c/cf/Endermite.png/200px-Endermite.png", Note = "5% chance to spawn when using an ender pearl", WikiLink = "https://minecraft.gamepedia.com/Endermite" });
            MCMobs.Add(new _Mob() { Name = "Chicken", EmojiID = "350864495251423253", ID = "93", Health = "2", Height = "0.7", Width = "0.4", Type = _MobType.Passive, Version = "Alpha 1.0.14", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/a/a3/Chicken.png/150px-Chicken.png", Note = "They are drawn to light sources", WikiLink = "https://minecraft.gamepedia.com/Chicken" });
            MCMobs.Add(new _Mob() { Name = "Ghast", EmojiID = "350864495301623821", ID = "56", Health = "5", Height = "4", Width = "4", Type = _MobType.Hostile, Version = "Alpha 1.2.0", AttackEasy = "4.5", AttackNormal = "8.5", AttackHard = "12.5", PicUrl = "https://minecraft.gamepedia.com/File:Ghast.gif", Note = "Ping pong that ghast fireball!", WikiLink = "https://minecraft.gamepedia.com/Ghast" });
            MCMobs.Add(new _Mob() { Name = "Blaze", EmojiID = "350864495406612480", ID = "61", Health = "10", Height = "1.8", Width = "0.6", Type = _MobType.Hostile, Version = "1.0.0", AttackEasy = "2", AttackNormal = "3", AttackHard = "4.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/b/bd/Blaze.png/150px-Blaze.png", Note = "Can be damaged with snowballs", WikiLink = "https://minecraft.gamepedia.com/Blaze" });
            MCMobs.Add(new _Mob() { Name = "Enderman", EmojiID = "350864495415001089", ID = "58", Health = "20", Height = "2.9", Width = "0.6", Type = _MobType.Neutral, Version = "Beta 1.8", AttackEasy = "2", AttackNormal = "3.5", AttackHard = "5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/2/28/Enderman.png/106px-Enderman.png", Note = "Wont attack player if wearing a pumpkin on your head", WikiLink = "https://minecraft.gamepedia.com/Enderman" });
            MCMobs.Add(new _Mob() { Name = "Horse", EmojiID = "350864495423258626", ID = "100", Health = "7.5", Height = "1.6", Width = "1.4", Type = _MobType.Tameable, Version = "1.6.1", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/3/3d/HorseInv.png/200px-HorseInv.png", Note = "They have diffrent stats to one another", WikiLink = "https://minecraft.gamepedia.com/Horse" });
            MCMobs.Add(new _Mob() { Name = "ElderGuardian", EmojiID = "350864495452880906", ID = "4", Health = "40", Height = "2", Width = "2", Type = _MobType.Hostile, Version = "1.8", AttackEasy = "2.5", AttackNormal = "4", AttackHard = "6", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/a/a4/Elder_Guardian.png/250px-Elder_Guardian.png", Note = "Not considered a boss mob?", WikiLink = "https://minecraft.gamepedia.com/Elder_Guardian" });
            MCMobs.Add(new _Mob() { Name = "EnderDragon", EmojiID = "350864495456813057", ID = "63", Health = "100", Height = "Rip", Width = "Rip", Type = _MobType.Boss, Version = "1.0.0", AttackEasy = "3", AttackNormal = "5", AttackHard = "7.5", PicUrl = "https://vignette1.wikia.nocookie.net/minecraft/images/f/fe/Enderdragonboss.png", Note = "Free the end!", WikiLink = "https://minecraft.gamepedia.com/Ender_Dragon" });
            MCMobs.Add(new _Mob() { Name = "Guardian", EmojiID = "350864495519858699", ID = "68", Health = "15", Height = "0.85", Width = "0.85", Type = _MobType.Hostile, Version = "1.8", AttackEasy = "2", AttackNormal = "3", AttackHard = "4.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/f/fd/Guardian.png/150px-Guardian.png", Note = "Pew pew laser beams!", WikiLink = "https://minecraft.gamepedia.com/Guardian" });
            MCMobs.Add(new _Mob() { Name = "Evoker", EmojiID = "350864495557738506", ID = "34", Health = "12", Height = "2", Width = "0.6", Type = _MobType.Hostile, Version = "1.11", AttackEasy = "3", AttackNormal = "3", AttackHard = "3", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/2/28/Evoker_attacking.png/295px-Evoker_attacking.png", Note = "Loot: Totem of undying", WikiLink = "https://minecraft.gamepedia.com/Evoker" });
            MCMobs.Add(new _Mob() { Name = "CaveSpider", EmojiID = "350864495792357376", ID = "59", Health = "6", Height = "0.5", Width = "0.7", Type = _MobType.Neutral, Version = "Beta 1.8", AttackEasy = "1", AttackNormal = "1", AttackHard = "1.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/6/60/Cave_Spider.png/150px-Cave_Spider.png", Note = "Neutral in daylight unless provoked", WikiLink = "https://minecraft.gamepedia.com/Cave_Spider" });
            MCMobs.Add(new _Mob() { Name = "Pig", EmojiID = "350864495876374531", ID = "90", Health = "5", Height = "0.45", Width = "0.45", Type = _MobType.Passive, Version = "Classic 0.24", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/3/30/Pig.png/150px-Pig.png", Note = "When Pigs Fly!", WikiLink = "https://minecraft.gamepedia.com/Pig" });
            MCMobs.Add(new _Mob() { Name = "Llama", EmojiID = "350864495935225868", ID = "103", Health = "15", Height = "1.87", Width = "0.9", Type = _MobType.Tameable, Version = "1.11", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/4/4b/Llama_gray.png/150px-Llama_gray.png", Note = "Wool colors depend on their biome", WikiLink = "https://minecraft.gamepedia.com/Llama" });
            MCMobs.Add(new _Mob() { Name = "MagmaCube", EmojiID = "350864495943352321", ID = "62", Health = "8", Height = "2.04", Width = "2.04", Type = _MobType.Hostile, Version = "1.0.0", AttackEasy = "3", AttackNormal = "3", AttackHard = "3", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/e/ed/Magma_Cube.png/150px-Magma_Cube.png", Note = "Deals two attacks per second unlike other mobs", WikiLink = "https://minecraft.gamepedia.com/Magma_Cube" });
            MCMobs.Add(new _Mob() { Name = "Ocelot", EmojiID = "350864496006397954", ID = "98", Health = "5", Height = "0.7", Width = "0.6", Type = _MobType.Tameable, Version = "1.2.1", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/0/0d/Ocelot.png/200px-Ocelot.png", Note = "Can be tamed and will turn into a cat that sits by beds and ontop of furnaces/chests", WikiLink = "https://minecraft.gamepedia.com/Ocelot" });
            MCMobs.Add(new _Mob() { Name = "Cat", EmojiID = "", ID = "98", Health = "5", Height = "0.7", Width = "0.6", Type = _MobType.Passive, Version = "1.2.1", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/6/67/Cat_tabby.png/200px-Cat_tabby.png", Note = "Cats will sit ontop of beds, furnaces and chests", WikiLink = "https://minecraft.gamepedia.com/Ocelot" });
            MCMobs.Add(new _Mob() { Name = "Parrot", EmojiID = "350864496023306243", ID = "105", Health = "3", Height = "0.9", Width = "0.5", Type = _MobType.Tameable, Version = "1.12", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/b/bf/Parrot_red_blue.png/150px-Parrot_red_blue.png", Note = "Comes in many diffrent colors", WikiLink = "https://minecraft.gamepedia.com/Parrot" });
            MCMobs.Add(new _Mob() { Name = "Rabbit", EmojiID = "350864496098672641", ID = "101", Health = "1.5", Height = "0.5", Width = "0.4", Type = _MobType.Passive, Version = "1.8", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/f/fd/Rabbit.png/150px-Rabbit.png", Note = "Watch out for the killer rabbit...", WikiLink = "https://minecraft.gamepedia.com/Rabbit" });
            MCMobs.Add(new _Mob() { Name = "IronGolem", EmojiID = "350864496182427669", ID = "99", Health = "50", Height = "2.7", Width = "1.4", Type = _MobType.Neutral, Version = "1.2.1", AttackEasy = "2-5.5", AttackNormal = "4.5-10.5", AttackHard = "5-15.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/c/c9/Village_Golem.png/150px-Village_Golem.png", Note = "Will attack the player if provoked", WikiLink = "https://minecraft.gamepedia.com/Iron_Golem" });
            MCMobs.Add(new _Mob() { Name = "Spider", EmojiID = "350864496182427672", ID = "52", Health = "8", Height = "0.9", Width = "1.4", Type = _MobType.Neutral, Version = "Classic 0.27", AttackEasy = "1", AttackNormal = "1", AttackHard = "1.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/8/84/Spider.png/150px-Spider.png", Note = "Neutral in daylight unless provoked", WikiLink = "https://minecraft.gamepedia.com/Spider" });
            MCMobs.Add(new _Mob() { Name = "Mooshroom", EmojiID = "350864496199204864", ID = "96", Health = "5", Height = "0.7", Width = "0.45", Type = _MobType.Passive, Version = "1.0.0", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/b/bb/Mooshroom.png/150px-Mooshroom.png", Note = "Can be milked with a bowl for mushroom stew", WikiLink = "https://minecraft.gamepedia.com/Mooshroom" });
            MCMobs.Add(new _Mob() { Name = "Shulker", EmojiID = "350864496262381569", ID = "69", Health = "15", Height = "1 | Open 1.2", Width = "1", Type = _MobType.Hostile, Version = "1.9", AttackEasy = "2", AttackNormal = "2", AttackHard = "2", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/c/cf/Shulker_Open.png/150px-Shulker_Open.png", Note = "Shoots projectiles that will follow and damage the player", WikiLink = "https://minecraft.gamepedia.com/Shulker" });
            MCMobs.Add(new _Mob() { Name = "Sheep", EmojiID = "350864496266444812", ID = "91", Health = "4", Height = "1.3", Width = "0.9", Type = _MobType.Passive, Version = "Classic 0.27", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/6/69/White_Sheep.png/150px-White_Sheep.png", Note = "Pink sheep have a 0.164% chance to spawn", WikiLink = "https://minecraft.gamepedia.com/Sheep" });
            MCMobs.Add(new _Mob() { Name = "Skeleton", EmojiID = "350864496274833409", ID = "51", Health = "10", Height = "2", Width = "0.6", Type = _MobType.Hostile, Version = "Classic 0.24", AttackEasy = "0.5-2", AttackNormal = "0.5-2", AttackHard = "0.5-2.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/2/23/Skeleton.png/200px-Skeleton.png", Note = "Bow accuracy is based on the difficulty", WikiLink = "https://minecraft.gamepedia.com/Skeleton" });
            MCMobs.Add(new _Mob() { Name = "Mule", EmojiID = "350864496279027712", ID = "32", Health = "7.5", Height = "1.6", Width = "1.4", Type = _MobType.Tameable, Version = "1.6.1", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/e/e7/Mule.png/200px-Mule.png", Note = "The lesser version of a donkey", WikiLink = "https://minecraft.gamepedia.com/Horse" });
            MCMobs.Add(new _Mob() { Name = "PolarBear", EmojiID = "350864496308256768", ID = "102", Health = "15", Height = "1.4", Width = "1.3", Type = _MobType.Neutral, Version = "1.10", AttackEasy = "2", AttackNormal = "3", AttackHard = "4.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/7/78/Polar_Bear.png/280px-Polar_Bear.png", Note = "Stay away from their babies", WikiLink = "https://minecraft.gamepedia.com/Polar_Bear" });
            MCMobs.Add(new _Mob() { Name = "SnowGolem", EmojiID = "350864496312713217", ID = "97", Health = "2", Height = "1.9", Width = "0.7", Type = _MobType.Passive, Version = "1.0.0", AttackEasy = "0", AttackNormal = "0", AttackHard = "1.5 to blaze", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/c/c2/Snow_Golem.png/150px-Snow_Golem.png", Note = "The most random mob in minecraft", WikiLink = "https://minecraft.gamepedia.com/Snow_Golem" });
            MCMobs.Add(new _Mob() { Name = "SpiderJockey", EmojiID = "350864496396468225", ID = "Skel 51/Spid 52", Health = "10", Height = "Rip", Width = "Rip", Type = _MobType.Hostile, Version = "Alpha 1.0.17", AttackEasy = "Skel 2/Spid 1", AttackNormal = "Skel 2/Spid 1", AttackHard = "Skel 2.5/Spid 1.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/e/ee/Spider_Jockey.png/183px-Spider_Jockey.png", Note = "Rare mob that has a 1% chance to spawn", WikiLink = "https://minecraft.gamepedia.com/Spider_Jockey" });
            MCMobs.Add(new _Mob() { Name = "Squid", EmojiID = "350864496430153729", ID = "94", Health = "4", Height = "0.8", Width = "0.8", Type = _MobType.Passive, Version = "Beta 1.2", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/8/81/Squid.png/150px-Squid.png", Note = "Derpy Squid", WikiLink = "https://minecraft.gamepedia.com/Squid" });
            MCMobs.Add(new _Mob() { Name = "Silverfish", EmojiID = "350864496434216960", ID = "60", Health = "4", Height = "0.3", Width = "0.4", Type = _MobType.Hostile, Version = "Beta 1.8", AttackEasy = "0.5", AttackNormal = "0.5", AttackHard = "0.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/b/b9/Silverfish.png/150px-Silverfish.png", Note = "Will call other silverfish in the area when attack", WikiLink = "https://minecraft.gamepedia.com/Silverfish" });
            MCMobs.Add(new _Mob() { Name = "SkeletonHorse", EmojiID = "350864496484417536", ID = "28", Health = "7.5", Height = "1.6", Width = "1.4", Type = _MobType.Passive, Version = "1.6.1", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/e/e6/Skeletonhorse.png/200px-Skeletonhorse.png", Note = "Cannot be tamed", WikiLink = "https://minecraft.gamepedia.com/Horse#Zombie_and_Skeleton_Horses" });
            MCMobs.Add(new _Mob() { Name = "ZombieHorse", EmojiID = "", ID = "28", Health = "7.5", Height = "1.6", Width = "1.4", Type = _MobType.Secret, Version = "1.6.1", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/4/46/Undeadhorse.png/200px-Undeadhorse.png", Note = "Cannot be tamed", WikiLink = "https://minecraft.gamepedia.com/Horse#Zombie_and_Skeleton_Horses" });
            MCMobs.Add(new _Mob() { Name = "Slime", EmojiID = "350864496497131521", ID = "55", Health = "8", Height = "2.04", Width = "2.04", Type = _MobType.Hostile, Version = "Alpha 1.0.11", AttackEasy = "2", AttackNormal = "2", AttackHard = "2", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/3/38/Slime.png/150px-Slime.png", Note = "Small slimes cannot damage you", WikiLink = "https://minecraft.gamepedia.com/Slime" });
            MCMobs.Add(new _Mob() { Name = "SkeletonTrap", EmojiID = "", ID = "No ID", Health = "7.5", Height = "Rip", Width = "Rip", Type = _MobType.Neutral, Version = "1.9", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://vignette2.wikia.nocookie.net/minecraft/images/d/d8/Skeleton_Trap_Horse.png", Note = "0.75–1.5% chance on Easy, 1.5–4% on Normal, and 2.8125–6.75% on Hard", WikiLink = "https://minecraft.gamepedia.com/Horse#Skeleton_trap" });
            MCMobs.Add(new _Mob() { Name = "WitherSkeleton", EmojiID = "350864496841064453", ID = "5", Health = "10", Height = "2.4", Width = "0.7", Type = _MobType.Hostile, Version = "1.4.2", AttackEasy = "2", AttackNormal = "3.5", AttackHard = "5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/c/cd/Wither_Skeleton.png/150px-Wither_Skeleton.png", Note = "Much larger model than any other mob in the game except for enderman", WikiLink = "https://minecraft.gamepedia.com/Wither_Skeleton" });
            MCMobs.Add(new _Mob() { Name = "Husk", EmojiID = "350864496887070720", ID = "23", Health = "10", Height = "2", Width = "0.6", Type = _MobType.Hostile, Version = "1.10", AttackEasy = "1", AttackNormal = "1.5", AttackHard = "2", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/8/8a/Husk.png/163px-Husk.png", Note = "Shoots tipped arrows of slowness", WikiLink = "https://minecraft.gamepedia.com/Skeleton#Stray" });
            MCMobs.Add(new _Mob() { Name = "Villager", EmojiID = "350864496908042252", ID = "27", Health = "10", Height = "2", Width = "0.6", Type = _MobType.Passive, Version = "1.0.0", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/1/1c/Farmer_Villager.png/136px-Farmer_Villager.png", Note = "They always have bad trade deals :3", WikiLink = "https://minecraft.gamepedia.com/Villager" });
            MCMobs.Add(new _Mob() { Name = "Vindicator", EmojiID = "350864496937533442", ID = "36", Health = "12", Height = "2", Width = "0.6", Type = _MobType.Hostile, Version = "1.11", AttackEasy = "3.5", AttackNormal = "6.5", AttackHard = "9.5", PicUrl = "https://minecraft.gamepedia.com/File:Vindicator.png", Note = "Johnny the woodland chopper", WikiLink = "https://minecraft.gamepedia.com/Vindicator" });
            MCMobs.Add(new _Mob() { Name = "Zombie", EmojiID = "350864497101242369", ID = "54", Health = "10", Height = "2", Width = "0.6", Type = _MobType.Hostile, Version = "Classic 0.24", AttackEasy = "1", AttackNormal = "1.5", AttackHard = "2", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/c/c3/Zombie.png/163px-Zombie.png", Note = "Can be harmed by healing potions", WikiLink = "https://minecraft.gamepedia.com/Zombie" });
            MCMobs.Add(new _Mob() { Name = "Vex", EmojiID = "350864497134796800", ID = "35", Health = "7", Height = "0.8", Width = "0.4", Type = _MobType.Hostile, Version = "1.11", AttackEasy = "2.5", AttackNormal = "4.5", AttackHard = "6.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/b/bc/Vex.png/250px-Vex.png", Note = "Can pass through any blocks", WikiLink = "https://minecraft.gamepedia.com/Vex" });
            MCMobs.Add(new _Mob() { Name = "Cow", EmojiID = "350864497168220170", ID = "92", Health = "5", Height = "1.4", Width = "0.9", Type = _MobType.Passive, Version = "Alpha 1.0.8", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/8/84/Cow.png/150px-Cow.png", Note = "Can be milked", WikiLink = "https://minecraft.gamepedia.com/Cow" });
            MCMobs.Add(new _Mob() { Name = "Witch", EmojiID = "350864497168351232", ID = "66", Health = "13", Height = "2", Width = "0.6", Type = _MobType.Hostile, Version = "1.4.2", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/e/e6/Witch.png/150px-Witch.png", Note = "Attacks you with splash potions of harming, slowness and poison", WikiLink = "https://minecraft.gamepedia.com/Witch" });
            MCMobs.Add(new _Mob() { Name = "Wither", EmojiID = "350864497189322752", ID = "64", Health = "150", Height = "3.5", Width = "0.9", Type = _MobType.Boss, Version = "1.4.2", AttackEasy = "2.5", AttackNormal = "4", AttackHard = "6", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/a/aa/Wither.png/250px-Wither.png", Note = "Will attack any non-undead mobs", WikiLink = "https://minecraft.gamepedia.com/Wither" });
            MCMobs.Add(new _Mob() { Name = "Wolf", EmojiID = "350864497197449225", ID = "95", Health = "4/Tamed 10", Height = "0.85", Width = "0.6", Type = _MobType.Tameable, Version = "Beta 1.4", AttackEasy = "1.5/Tamed 2", AttackNormal = "2/Tamed 2", AttackHard = "3/Tamed 2", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/2/25/Wolf_%28Wild%29.png/150px-Wolf_%28Wild%29.png", Note = "Still neutral mobs and will attack if provoked", WikiLink = "https://minecraft.gamepedia.com/Wolf" });
            MCMobs.Add(new _Mob() { Name = "Stray", EmojiID = "350864497876926484", ID = "6", Health = "10", Height = "2", Width = "0.6", Type = _MobType.Hostile, Version = "1.10", AttackEasy = "0.5-2", AttackNormal = "0.5-2", AttackHard = "0.5-2.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/0/07/Stray.png/200px-Stray.png", Note = "Arrow attacks will cause slowness", WikiLink = "https://minecraft.gamepedia.com/Stray" });
            MCMobs.Add(new _Mob() { Name = "ZombiePigman", EmojiID = "350864498359533568", ID = "57", Health = "10", Height = "2", Width = "0.6", Type = _MobType.Neutral, Version = "Alpha 1.2.0", AttackEasy = "2.5", AttackNormal = "4.5", AttackHard = "6.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/0/09/Zombie_Pigman.png/150px-Zombie_Pigman.png", Note = "They will swarm you if provoked", WikiLink = "https://minecraft.gamepedia.com/Zombie_Pigman" });
            MCMobs.Add(new _Mob() { Name = "ZombieVillager", EmojiID = "350864498711724043", ID = "27", Health = "10", Height = "2", Width = "0.6", Type = _MobType.Hostile, Version = "1.4.2", AttackEasy = "1", AttackNormal = "1.5", AttackHard = "2", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/6/6c/Zombie_Villager2.png/160px-Zombie_Villager2.png", Note = "Can be cured using weakness effect and golden apple", WikiLink = "https://minecraft.gamepedia.com/Zombie_Villager" });
            MCMobs.Add(new _Mob() { Name = "Giant", EmojiID = "", ID = "53", Health = "50", Height = "11.7", Width = "3.6", Type = _MobType.Secret, Version = "Classic 0.30", AttackEasy = "13", AttackNormal = "25", AttackHard = "37.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/c/c3/Zombie.png/150px-Zombie.png", Note = "Not used in the game but can be spawned in", WikiLink = "https://minecraft.gamepedia.com/Giant" });
            MCMobs.Add(new _Mob() { Name = "KillerRabbit", EmojiID = "", ID = "104", Health = "1.5", Height = "0.5", Width = "0.4", Type = _MobType.Secret, Version = "1.8", AttackEasy = "2.5", AttackNormal = "4", AttackHard = "6", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/e/e6/White_Rabbit.png/150px-White_Rabbit.png", Note = "Mystery killer rabbits", WikiLink = "https://minecraft.gamepedia.com/Rabbit#The_Killer_Bunny" });
            MCMobs.Add(new _Mob() { Name = "Illusioner", EmojiID = "", ID = "37", Health = "16", Height = "2", Width = "0.6", Type = _MobType.Secret, Version = "1.12", AttackEasy = "0.5-2", AttackNormal = "0.5-2", AttackHard = "0.5-2.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/f/f8/Illusioner_attacking.png/295px-Illusioner_attacking.png", Note = "Coming soon to 1.12", WikiLink = "https://minecraft.gamepedia.com/Illusioner" });
        }
        internal static void AddQuiz()
        {
            MCQuiz.Add(new _Quiz() { Question = "How many villager professions/careers are there not including the default villager", Answer = "6 six", Note = "The 6 professions are Farmer, Librarian, Priest, Blacksmith, Butcher and Nitwit"});
            MCQuiz.Add(new _Quiz() { Question = "What item can iron golems spawn with", Answer = "poppy, poppies", Note = "Iron golems can spawn with a poppy in their hand and offer them to villagers" });
            MCQuiz.Add(new _Quiz() { Question = "Does a blaze drop blaze powder?", Answer = "no", Note = "Blaze drop 0-1 blaze rods not blaze powder" });
            MCQuiz.Add(new _Quiz() { Question = "How many colors are there in minecraft?", Answer = "16 sixteen", Note = "Minecraft has 16 colors use > mc/colors" });
            MCQuiz.Add(new _Quiz() { Question = "How many hearts does the wither boss have", Answer = "150", Note = "The wither has 150 hearts/health points" });
            MCQuiz.Add(new _Quiz() { Question = "What item can iron golems spawn with", Answer = "poppy, poppies", Note = "Iron golems can spawn with a poppy in their hand and offer them to villagers" });
            MCQuiz.Add(new _Quiz() { Question = "Can snow golems take fall damage", Answer = "no", Note = "Snow golems are immune to fall damage" });
            MCQuiz.Add(new _Quiz() { Question = "Who created minecraft", Answer = "notch Markus", Note = "Notch aka Markus Persson created Minecraft" });
            MCQuiz.Add(new _Quiz() { Question = "Do endermites take damage on soul sand?", Answer = "yes", Note = "Endermites take damage on soul sand because of their small height" });
            MCQuiz.Add(new _Quiz() { Question = "Can nether wart grow in the overworld", Answer = "no", Note = "Nether wart only grows in the nether" });
            MCQuiz.Add(new _Quiz() { Question = "What dimension is The End called", Answer = "sky", Note = "Press F3 and the dimension for The End is called Sky" });
            MCQuiz.Add(new _Quiz() { Question = "Can skeletons see through glass", Answer = "no", Note = "Skeletons cannot see through glass like zombies and spiders can" });
            MCQuiz.Add(new _Quiz() { Question = "What was Minecraft originally called", Answer = "cavegame", Note = "Notch the creator almost called Minecraft Cavegame" });
            MCQuiz.Add(new _Quiz() { Question = "Which version was world borders added", Answer = "1.8", Note = "World borders were officially added in 1.8 aside from plugins" });
            MCQuiz.Add(new _Quiz() { Question = "Which mob drops string", Answer = "spider", Note = "Spiders can drop string and spider eyes" });
            MCQuiz.Add(new _Quiz() { Question = "What is the snow biome called", Answer = "taiga", Note = "The snow biomes are called taiga there is also ice plains" });
            MCQuiz.Add(new _Quiz() { Question = "What biome does hardened clay generate in", Answer = "mesa", Note = "The mesa is a landscape of red sand and hardened clay hills" });
            MCQuiz.Add(new _Quiz() { Question = "How many tree types are there", Answer = "4 four", Note = "There are four tree types in the game oak, birch, spruce and jungle" });
            MCQuiz.Add(new _Quiz() { Question = "What biome can hostile mobs not spawn in", Answer = "mushroom", Note = "Hostile mobs cannot spawn in the mushroom biome or in the caves either" });
        }
        internal static void AddVersions()
        {
            MC_Versions.Add("1.12", new _Version { AllVersions = "[1.12](https://minecraft.gamepedia.com/1.12) | [1.12.1](https://minecraft.gamepedia.com/1.12.1) | [1.12.2](https://minecraft.gamepedia.com/1.12.2)", Name = "World of color update", Released = "June 7 | 2017" });
            MC_Versions.Add("1.11", new _Version { AllVersions = "[1.11](https://minecraft.gamepedia.com/1.11) | [1.11.1](https://minecraft.gamepedia.com/1.11.1) | [1.11.2](https://minecraft.gamepedia.com/1.11.2)", Name = "Exploration update", Released = "November 14 | 2016" });
            MC_Versions.Add("1.10", new _Version { AllVersions = "[1.10](https://minecraft.gamepedia.com/1.10) | [1.10.1](https://minecraft.gamepedia.com/1.10.1) | [1.10.2](https://minecraft.gamepedia.com/1.10.2)", Name = "Frostburn update", Released = "June 8 | 2016" });
            MC_Versions.Add("1.9", new _Version { AllVersions = "[1.9](https://minecraft.gamepedia.com/1.9) | [1.9.1](https://minecraft.gamepedia.com/1.9.1) | [1.9.2](https://minecraft.gamepedia.com/1.9.2) | [1.9.3](https://minecraft.gamepedia.com/1.9.3) | [1.9.4](https://minecraft.gamepedia.com/1.9.4)", Name = "Combat update", Released = "Febuary 29 | 2016" });
            MC_Versions.Add("1.8", new _Version { AllVersions = "[1.8](https://minecraft.gamepedia.com/1.8) | [1.8.1](https://minecraft.gamepedia.com/1.8.1) | [1.8.2](https://minecraft.gamepedia.com/1.8.2) | [1.8.3](https://minecraft.gamepedia.com/1.8.3) | [1.8.4](https://minecraft.gamepedia.com/1.8.4) | [1.8.5](https://minecraft.gamepedia.com/1.8.5) | [1.8.6](https://minecraft.gamepedia.com/1.8.6) | [1.8.7](https://minecraft.gamepedia.com/1.8.7) | [1.8.8](https://minecraft.gamepedia.com/1.8.8) | [1.8.9](https://minecraft.gamepedia.com/1.8.9)", Name = "Bountiful update", Released = "September 2 | 2014" });
            MC_Versions.Add("1.7", new _Version { AllVersions = "[1.7.2](https://minecraft.gamepedia.com/1.7.2) | [1.7.4](https://minecraft.gamepedia.com/1.7.4) | [1.7.5](https://minecraft.gamepedia.com/1.7.5) | [1.7.6](https://minecraft.gamepedia.com/1.7.6) | [1.7.7](https://minecraft.gamepedia.com/1.7.7) | [1.7.8](https://minecraft.gamepedia.com/1.7.8) | [1.7.9](https://minecraft.gamepedia.com/1.7.9) | [1.7.10](https://minecraft.gamepedia.com/1.7.10)", Name = "The update that changed the world", Released = "October 25 | 2013" });
            MC_Versions.Add("1.6", new _Version { AllVersions = "[1.6.1](https://minecraft.gamepedia.com/1.6.1) | [1.6.2](https://minecraft.gamepedia.com/1.6.2) | [1.6.4](https://minecraft.gamepedia.com/1.6.4)", Name = "Horse update", Released = "July 1 | 2013" });
            MC_Versions.Add("1.5", new _Version { AllVersions = "[1.5](https://minecraft.gamepedia.com/1.5) | [1.5.1](https://minecraft.gamepedia.com/1.5.1) | [1.5.2](https://minecraft.gamepedia.com/1.5.2)", Name = "Redstone update", Released = "March 13 | 2013" });
            MC_Versions.Add("1.4", new _Version { AllVersions = "[1.4.2](https://minecraft.gamepedia.com/1.4.2) | [1.4.4](https://minecraft.gamepedia.com/1.4.4) | [1.4.5](https://minecraft.gamepedia.com/1.4.5) | [1.4.6](https://minecraft.gamepedia.com/1.4.6) | [1.4.7](https://minecraft.gamepedia.com/1.4.7)", Name = "Pretty scary update", Released = "October 25 | 2012" });
            MC_Versions.Add("1.3", new _Version { AllVersions = "[1.3.1](https://minecraft.gamepedia.com/1.3.1) | [1.3.2](https://minecraft.gamepedia.com/1.3.2)", Name = "No Name", Released = "August 1 | 2012" });
            MC_Versions.Add("1.2", new _Version { AllVersions = "[1.2.1](https://minecraft.gamepedia.com/1.2.1) | [1.2.2](https://minecraft.gamepedia.com/1.2.2) | [1.2.3](https://minecraft.gamepedia.com/1.2.3) | [1.2.4](https://minecraft.gamepedia.com/1.2.4) | [1.2.5](https://minecraft.gamepedia.com/1.2.5)", Name = "No Name", Released = "March 1 | 2012" });
            MC_Versions.Add("1.1", new _Version { AllVersions = "[1.1](https://minecraft.gamepedia.com/1.1)", Name = "No Name", Released = "January 12 | 2012" });
            MC_Versions.Add("1.0", new _Version { AllVersions = "[1.0.0](https://minecraft.gamepedia.com/1.0.0)", Name = "No Name", Released = "November 18 | 2011" });
        }
    }
}

