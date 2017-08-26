﻿using Bot.Classes;
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
        public static string Github = "https://github.com/xXBuilderBXx/MC-Bot";
        public static string BotPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/MC-Bot/";
        public static bool Ready = false;
        public static bool DevMode = true;
        public static Class Tokens = new Class();
        public static List<_Guild> MCGuilds = new List<_Guild>();
        public static List<_Item> MCItems = new List<_Item>();
        public static List<_Mob> MCMobs = new List<_Mob>();
        public static List<_Quiz> MCQuiz = new List<_Quiz>();
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
            AddMobs();
            AddQuiz();
        }
        public static void AddMobs()
        {
            MCMobs.Add(new _Mob() { Name = "Bat", EmojiID = "350864494991245313", ID = "65", Health = "3", Height = "0.9", Width = "0.5", Type = _MobType.Passive, Version = "1.4.2", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/0/09/Bat.gif", Note = "Bats are useless in the game" });
            MCMobs.Add(new _Mob() { Name = "ChickenJockey", EmojiID = "350864495108685826", ID = "54", Health = "10", Height = "Rip", Width = "Rip", Type = _MobType.Hostile, Version = "1.7.4", AttackEasy = "1", AttackNormal = "1.5", AttackHard = "2", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/d/d3/Chicken_Jockey.png/150px-Chicken_Jockey.png", Note = "Baby zombie riding a chicken what madness is this!" });
            MCMobs.Add(new _Mob() { Name = "Donkey", EmojiID = "350864495113142274", ID = "31", Health = "7.5", Height = "1.6", Width = "1.4", Type = _MobType.Tameable, Version = "1.6.1", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/9/95/Donkey.png/200px-Donkey.png" });
            MCMobs.Add(new _Mob() { Name = "Creeper", EmojiID = "350864495129919490", ID = "50", Health = "10", Height = "1.7", Width = "0.6", Type = _MobType.Hostile, Version = "Classic 0.24", AttackEasy = "Max Damage", AttackNormal = "24.5", AttackHard = "Charged 48.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/0/0a/Creeper.png/150px-Creeper.png", Note = "Created from a failed pig model :D" });
            MCMobs.Add(new _Mob() { Name = "Endermite", EmojiID = "350864495171731457", ID = "67", Health = "4", Height = "0.3", Width = "0.4", Type = _MobType.Hostile, Version = "1.8", AttackEasy = "1", AttackNormal = "1", AttackHard = "1.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/c/cf/Endermite.png/200px-Endermite.png" });
            MCMobs.Add(new _Mob() { Name = "Chicken", EmojiID = "350864495251423253", ID = "93", Health = "2", Height = "0.7", Width = "0.4", Type = _MobType.Passive, Version = "Alpha 1.0.14", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/a/a3/Chicken.png/150px-Chicken.png", Note = "They are drawn to light sources" });
            MCMobs.Add(new _Mob() { Name = "Ghast", EmojiID = "350864495301623821", ID = "56", Health = "5", Height = "4", Width = "4", Type = _MobType.Hostile, Version = "Alpha 1.2.0", AttackEasy = "4.5", AttackNormal = "8.5", AttackHard = "12.5", PicUrl = "https://minecraft.gamepedia.com/File:Ghast.gif" });
            MCMobs.Add(new _Mob() { Name = "Blaze", EmojiID = "350864495406612480", ID = "61", Health = "10", Height = "1.8", Width = "0.6", Type = _MobType.Hostile, Version = "1.0.0", AttackEasy = "2", AttackNormal = "3", AttackHard = "4.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/b/bd/Blaze.png/150px-Blaze.png" });
            MCMobs.Add(new _Mob() { Name = "Enderman", EmojiID = "350864495415001089", ID = "58", Health = "20", Height = "2.9", Width = "0.6", Type = _MobType.Neutral, Version = "Beta 1.8", AttackEasy = "2", AttackNormal = "3.5", AttackHard = "5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/2/28/Enderman.png/106px-Enderman.png", Note = "Wont attack player if wearing a pumpkin on your head" });
            MCMobs.Add(new _Mob() { Name = "Horse", EmojiID = "350864495423258626", ID = "100", Health = "7.5", Height = "1.6", Width = "1.4", Type = _MobType.Tameable, Version = "1.6.1", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/3/3d/HorseInv.png/200px-HorseInv.png", Note = "They have diffrent stats to one another" });
            MCMobs.Add(new _Mob() { Name = "ElderGuardian", EmojiID = "350864495452880906", ID = "4", Health = "40", Height = "2", Width = "2", Type = _MobType.Hostile, Version = "1.8", AttackEasy = "2.5", AttackNormal = "4", AttackHard = "6", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/a/a4/Elder_Guardian.png/250px-Elder_Guardian.png", Note = "Not considered a boss mob?" });
            MCMobs.Add(new _Mob() { Name = "EnderDragon", EmojiID = "350864495456813057", ID = "63", Health = "100", Height = "Rip", Width = "Rip", Type = _MobType.Boss, Version = "1.0.0", AttackEasy = "3", AttackNormal = "5", AttackHard = "7.5", PicUrl = "https://vignette1.wikia.nocookie.net/minecraft/images/f/fe/Enderdragonboss.png" });
            MCMobs.Add(new _Mob() { Name = "Guardian", EmojiID = "350864495519858699", ID = "68", Health = "15", Height = "0.85", Width = "0.85", Type = _MobType.Hostile, Version = "1.8", AttackEasy = "2", AttackNormal = "3", AttackHard = "4.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/f/fd/Guardian.png/150px-Guardian.png" });
            MCMobs.Add(new _Mob() { Name = "Evoker", EmojiID = "350864495557738506", ID = "34", Health = "12", Height = "2", Width = "0.6", Type = _MobType.Hostile, Version = "1.11", AttackEasy = "3", AttackNormal = "3", AttackHard = "3", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/2/28/Evoker_attacking.png/295px-Evoker_attacking.png" });
            MCMobs.Add(new _Mob() { Name = "CaveSpider", EmojiID = "350864495792357376", ID = "59", Health = "6", Height = "0.5", Width = "0.7", Type = _MobType.Neutral, Version = "Beta 1.8", AttackEasy = "1", AttackNormal = "1", AttackHard = "1.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/6/60/Cave_Spider.png/150px-Cave_Spider.png" });
            MCMobs.Add(new _Mob() { Name = "Pig", EmojiID = "350864495876374531", ID = "90", Health = "5", Height = "0.45", Width = "0.45", Type = _MobType.Passive, Version = "Classic 0.24", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/3/30/Pig.png/150px-Pig.png" });
            MCMobs.Add(new _Mob() { Name = "Llama", EmojiID = "350864495935225868", ID = "103", Health = "15", Height = "1.87", Width = "0.9", Type = _MobType.Tameable, Version = "1.11", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/4/4b/Llama_gray.png/150px-Llama_gray.png" });
            MCMobs.Add(new _Mob() { Name = "MagmaCube", EmojiID = "350864495943352321", ID = "62", Health = "8", Height = "2.04", Width = "2.04", Type = _MobType.Hostile, Version = "1.0.0", AttackEasy = "3", AttackNormal = "3", AttackHard = "3", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/e/ed/Magma_Cube.png/150px-Magma_Cube.png" });
            MCMobs.Add(new _Mob() { Name = "Ocelot", EmojiID = "350864496006397954", ID = "98", Health = "5", Height = "0.7", Width = "0.6", Type = _MobType.Tameable, Version = "1.2.1", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/0/0d/Ocelot.png/200px-Ocelot.png", Note = "Can be tamed and will turn into a cat that sits by beds and ontop of furnaces/chests" });
            MCMobs.Add(new _Mob() { Name = "Parrot", EmojiID = "350864496023306243", ID = "105", Health = "3", Height = "0.9", Width = "0.5", Type = _MobType.Tameable, Version = "1.12", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/b/bf/Parrot_red_blue.png/150px-Parrot_red_blue.png", Note = "Comes in many diffrent colors" });
            MCMobs.Add(new _Mob() { Name = "Rabbit", EmojiID = "350864496098672641", ID = "101", Health = "1.5", Height = "0.5", Width = "0.4", Type = _MobType.Passive, Version = "1.8", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/f/fd/Rabbit.png/150px-Rabbit.png", Note = "Watch out for the killer rabbit..." });
            MCMobs.Add(new _Mob() { Name = "IronGolem", EmojiID = "350864496182427669", ID = "99", Health = "50", Height = "2.7", Width = "1.4", Type = _MobType.Neutral, Version = "1.2.1", AttackEasy = "2-5.5", AttackNormal = "4.5-10.5", AttackHard = "5-15.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/c/c9/Village_Golem.png/150px-Village_Golem.png" });
            MCMobs.Add(new _Mob() { Name = "Spider", EmojiID = "350864496182427672", ID = "52", Health = "8", Height = "0.9", Width = "1.4", Type = _MobType.Neutral, Version = "Classic 0.27", AttackEasy = "1", AttackNormal = "1", AttackHard = "1.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/8/84/Spider.png/150px-Spider.png" });
            MCMobs.Add(new _Mob() { Name = "Mooshroom", EmojiID = "350864496199204864", ID = "96", Health = "5", Height = "0.7", Width = "0.45", Type = _MobType.Passive, Version = "1.0.0", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/b/bb/Mooshroom.png/150px-Mooshroom.png" });
            MCMobs.Add(new _Mob() { Name = "Shulker", EmojiID = "350864496262381569", ID = "69", Health = "15", Height = "1 | Open 1.2", Width = "1", Type = _MobType.Hostile, Version = "1.9", AttackEasy = "2", AttackNormal = "2", AttackHard = "2", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/c/cf/Shulker_Open.png/150px-Shulker_Open.png" });
            MCMobs.Add(new _Mob() { Name = "Sheep", EmojiID = "350864496266444812", ID = "91", Health = "4", Height = "1.3", Width = "0.9", Type = _MobType.Passive, Version = "Classic 0.27", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/6/69/White_Sheep.png/150px-White_Sheep.png" });
            MCMobs.Add(new _Mob() { Name = "Skeleton", EmojiID = "350864496274833409", ID = "51", Health = "10", Height = "2", Width = "0.6", Type = _MobType.Hostile, Version = "Classic 0.24", AttackEasy = "0.5-2", AttackNormal = "0.5-2", AttackHard = "0.5-2.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/2/23/Skeleton.png/200px-Skeleton.png" });
            MCMobs.Add(new _Mob() { Name = "Mule", EmojiID = "350864496279027712", ID = "32", Health = "7.5", Height = "1.6", Width = "1.4", Type = _MobType.Tameable, Version = "1.6.1", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/e/e7/Mule.png/200px-Mule.png" });
            MCMobs.Add(new _Mob() { Name = "PolarBear", EmojiID = "350864496308256768", ID = "102", Health = "15", Height = "1.4", Width = "1.3", Type = _MobType.Neutral, Version = "1.10", AttackEasy = "2", AttackNormal = "3", AttackHard = "4.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/7/78/Polar_Bear.png/280px-Polar_Bear.png", Note = "Stay away from their babies" });
            MCMobs.Add(new _Mob() { Name = "SnowGolem", EmojiID = "350864496312713217", ID = "97", Health = "2", Height = "1.9", Width = "0.7", Type = _MobType.Passive, Version = "1.0.0", AttackEasy = "0", AttackNormal = "0", AttackHard = "1.5 to blaze", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/c/c2/Snow_Golem.png/150px-Snow_Golem.png", Note = "The most random mob in minecraft" });
            MCMobs.Add(new _Mob() { Name = "SpiderJockey", EmojiID = "350864496396468225", ID = "Skel 51/Spid 52", Health = "10", Height = "Rip", Width = "Rip", Type = _MobType.Hostile, Version = "Alpha 1.0.17", AttackEasy = "Skel 2/Spid 1", AttackNormal = "Skel 2/Spid 1", AttackHard = "Skel 2.5/Spid 1.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/e/ee/Spider_Jockey.png/183px-Spider_Jockey.png", Note = "Rare mob that has a 1% chance to spawn" });
            MCMobs.Add(new _Mob() { Name = "Squid", EmojiID = "350864496430153729", ID = "94", Health = "4", Height = "0.8", Width = "0.8", Type = _MobType.Passive, Version = "Beta 1.2", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/8/81/Squid.png/150px-Squid.png" });
            MCMobs.Add(new _Mob() { Name = "Silverfish", EmojiID = "350864496434216960", ID = "60", Health = "4", Height = "0.3", Width = "0.4", Type = _MobType.Hostile, Version = "Beta 1.8", AttackEasy = "0.5", AttackNormal = "0.5", AttackHard = "0.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/b/b9/Silverfish.png/150px-Silverfish.png" });
            MCMobs.Add(new _Mob() { Name = "SkeletonHorse", EmojiID = "350864496484417536", ID = "28", Health = "7.5", Height = "1.6", Width = "1.4", Type = _MobType.Passive, Version = "1.6.1", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/e/e6/Skeletonhorse.png/200px-Skeletonhorse.png" });
            MCMobs.Add(new _Mob() { Name = "Slime", EmojiID = "350864496497131521", ID = "55", Health = "8", Height = "2.04", Width = "2.04", Type = _MobType.Hostile, Version = "Alpha 1.0.11", AttackEasy = "2", AttackNormal = "2", AttackHard = "2", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/3/38/Slime.png/150px-Slime.png", Note = "Small slimes cannot damage you" });
            MCMobs.Add(new _Mob() { Name = "SkeletonRider", EmojiID = "350864496534749184", ID = "No ID", Health = "7.5", Height = "Rip", Width = "Rip", Type = _MobType.Hostile, Version = "1.9", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://vignette2.wikia.nocookie.net/minecraft/images/d/d8/Skeleton_Trap_Horse.png" });
            MCMobs.Add(new _Mob() { Name = "WitherSkeleton", EmojiID = "350864496841064453", ID = "5", Health = "10", Height = "2.4", Width = "0.7", Type = _MobType.Hostile, Version = "1.4.2", AttackEasy = "2", AttackNormal = "3.5", AttackHard = "5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/c/cd/Wither_Skeleton.png/150px-Wither_Skeleton.png" });
            MCMobs.Add(new _Mob() { Name = "Husk", EmojiID = "350864496887070720", ID = "23", Health = "10", Height = "2", Width = "0.6", Type = _MobType.Hostile, Version = "1.10", AttackEasy = "1", AttackNormal = "1.5", AttackHard = "2", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/8/8a/Husk.png/163px-Husk.png" });
            MCMobs.Add(new _Mob() { Name = "Villager", EmojiID = "350864496908042252", ID = "27", Health = "10", Height = "2", Width = "0.6", Type = _MobType.Passive, Version = "1.0.0", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/1/1c/Farmer_Villager.png/136px-Farmer_Villager.png" });
            MCMobs.Add(new _Mob() { Name = "Vindicator", EmojiID = "350864496937533442", ID = "36", Health = "12", Height = "2", Width = "0.6", Type = _MobType.Hostile, Version = "1.11", AttackEasy = "3.5", AttackNormal = "6.5", AttackHard = "9.5", PicUrl = "https://minecraft.gamepedia.com/File:Vindicator.png" });
            MCMobs.Add(new _Mob() { Name = "Zombie", EmojiID = "350864497101242369", ID = "54", Health = "10", Height = "2", Width = "0.6", Type = _MobType.Hostile, Version = "Classic 0.24", AttackEasy = "1", AttackNormal = "1.5", AttackHard = "2", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/c/c3/Zombie.png/163px-Zombie.png" });
            MCMobs.Add(new _Mob() { Name = "Vex", EmojiID = "350864497134796800", ID = "35", Health = "7", Height = "0.8", Width = "0.4", Type = _MobType.Hostile, Version = "1.11", AttackEasy = "2.5", AttackNormal = "4.5", AttackHard = "6.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/b/bc/Vex.png/250px-Vex.png" });
            MCMobs.Add(new _Mob() { Name = "Cow", EmojiID = "350864497168220170", ID = "92", Health = "5", Height = "1.4", Width = "0.9", Type = _MobType.Passive, Version = "Alpha 1.0.8", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/8/84/Cow.png/150px-Cow.png" });
            MCMobs.Add(new _Mob() { Name = "Witch", EmojiID = "350864497168351232", ID = "66", Health = "13", Height = "2", Width = "0.6", Type = _MobType.Hostile, Version = "1.4.2", AttackEasy = "", AttackNormal = "", AttackHard = "", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/e/e6/Witch.png/150px-Witch.png" });
            MCMobs.Add(new _Mob() { Name = "Wither", EmojiID = "350864497189322752", ID = "64", Health = "150", Height = "3.5", Width = "0.9", Type = _MobType.Boss, Version = "1.4.2", AttackEasy = "2.5", AttackNormal = "4", AttackHard = "6", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/a/aa/Wither.png/250px-Wither.png" });
            MCMobs.Add(new _Mob() { Name = "Wolf", EmojiID = "350864497197449225", ID = "95", Health = "4/Tamed 10", Height = "0.85", Width = "0.6", Type = _MobType.Tameable, Version = "Beta 1.4", AttackEasy = "1.5/Tamed 2", AttackNormal = "2/Tamed 2", AttackHard = "3/Tamed 2", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/2/25/Wolf_%28Wild%29.png/150px-Wolf_%28Wild%29.png", Note = "Still neutral mobs and will attack if provoked" });
            MCMobs.Add(new _Mob() { Name = "Stray", EmojiID = "350864497876926484", ID = "6", Health = "10", Height = "2", Width = "0.6", Type = _MobType.Hostile, Version = "1.10", AttackEasy = "0.5-2", AttackNormal = "0.5-2", AttackHard = "0.5-2.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/0/07/Stray.png/200px-Stray.png", Note = "Arrow attacks will cause slowness" });
            MCMobs.Add(new _Mob() { Name = "ZombiePigman", EmojiID = "350864498359533568", ID = "57", Health = "10", Height = "2", Width = "0.6", Type = _MobType.Neutral, Version = "Alpha 1.2.0", AttackEasy = "2.5", AttackNormal = "4.5", AttackHard = "6.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/0/09/Zombie_Pigman.png/150px-Zombie_Pigman.png", Note = "They will swarm you if provoked" });
            MCMobs.Add(new _Mob() { Name = "ZombieVillager", EmojiID = "350864498711724043", ID = "27", Health = "10", Height = "2", Width = "0.6", Type = _MobType.Hostile, Version = "1.4.2", AttackEasy = "1", AttackNormal = "1.5", AttackHard = "2", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/6/6c/Zombie_Villager2.png/160px-Zombie_Villager2.png", Note = "Can be cured using weakness effect and golden apple" });
            MCMobs.Add(new _Mob() { Name = "Giant", EmojiID = "1", ID = "53", Health = "50", Height = "11.7", Width = "3.6", Type = _MobType.Secret, Version = "Classic 0.30", AttackEasy = "13", AttackNormal = "25", AttackHard = "37.5", PicUrl = "https://minecraft.gamepedia.com/media/minecraft.gamepedia.com/thumb/c/c3/Zombie.png/150px-Zombie.png", Note = "Not used in the game but can be spawned in" });
        }
        public static void AddQuiz()
        {
            MCQuiz.Add(new _Quiz() { Question = "How many villager professions/careers are there not including the default villager", Answer = "6 six", Note = "The 6 professions are Farmer, Librarian, Priest, Blacksmith, Butcher and Nitwit"});
            MCQuiz.Add(new _Quiz() { Question = "What item can iron golems spawn with", Answer = "poppy, poppies", Note = "Iron golems can spawn with a poppy in their hand and offer them to villagers" });
            MCQuiz.Add(new _Quiz() { Question = "Does a blaze drop blaze powder?", Answer = "no", Note = "Blaze drop 0-1 blaze rods not blaze powder" });
            MCQuiz.Add(new _Quiz() { Question = "How many colors are there in minecraft?", Answer = "16 sixteen", Note = "Minecraft has 16 colors use > mc/colors" });
            MCQuiz.Add(new _Quiz() { Question = "How many hearts does the wither boss have", Answer = "150", Note = "The wither has 150 hearts/health points" });
            MCQuiz.Add(new _Quiz() { Question = "What item can iron golems spawn with", Answer = "poppy, poppies", Note = "Iron golems can spawn with a poppy in their hand and offer them to villagers" });
            MCQuiz.Add(new _Quiz() { Question = "Can snow golems take fall damage", Answer = "no", Note = "Snow golems are immune to fall damage" });
            MCQuiz.Add(new _Quiz() { Question = "Who created minecraft", Answer = "notch Markus", Note = "Notch aka Markus Persson created Minecraft" });
            MCQuiz.Add(new _Quiz() { Question = "Do endermites take damage on soul sand?", Answer = "yes", Note = "Endermites take damage on soul sound because of their small height" });
            MCQuiz.Add(new _Quiz() { Question = "Can nether wart grow in the overworld", Answer = "no", Note = "Nether wart only grows in the nether" });
            MCQuiz.Add(new _Quiz() { Question = "What dimension is The End called", Answer = "sky", Note = "Press F3 and the dimension for The End is called Sky" });
            MCQuiz.Add(new _Quiz() { Question = "Can skeletons see through glass", Answer = "no", Note = "Skeletons cannot see through glass like zombies and spiders can" });
            MCQuiz.Add(new _Quiz() { Question = "What was Minecraft originally called", Answer = "cavegame", Note = "Notch the creator almost called Minecraft Cavegame" });
        }
    }
}

