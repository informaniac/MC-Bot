using Bot.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot
{
    public class _Trans
    {
        /// <summary>
        ///  LEAVE THIS ALONE
        /// </summary>
        public class TString
        {
            public string EN = "";
            public string FR = "";
            public string SP = "";
            public string RU = "";
            public string Get(_Guild Guild)
            {
                if (Guild == null)
                {
                    return EN;
                }
                else
                {
                    switch ((int)Guild.Language)
                    {
                        case 0:
                            return EN;
                        case 1:
                            return FR;
                        case 2:
                            return SP;
                        case 3:
                            return RU;
                        default:
                            return EN;
                    }
                }
            }
        }
        /// <summary>
        ///  END
        /// </summary>
        /// 

        public class Main
        {
            public List<string[]> HelpCommands = new List<string[]>()
                {
                    // English
                    new string[]
                    {
                        "[ mc/bot ]( Bot Invite/Info/Stats/Links )",
                        "[ mc/quiz ]( Minecraft quiz :D )",
                        "[ mc/colors ]( MC color codes )",
                        "[ mc/uuid (Player) ]( Player UUID )",
                        "[ mc/ping (IP) ]( Ping a server )",
                        "[ mc/list ]( List guild MC servers )",
                        "[ mc/wiki ]( Wiki for Items/Mobs/Enchants/Potions )",
                        "[ mc/info ]( MC sales info )",
                        "[ mc/skin (Player) ]( Player skin )",
                        "[ mc/names (Player) ]( MC account name history )",
                        "[ mc/status ]( Mojang status )",
                        "[ mc/get (Text) ]( Get an achievement )",
                        "[ mc/skinedit ]( Online skin editor )",
                        "[ mc/minime (Player) ]( Minify player skin )",
                        "[ mc/playing ]( People playing Minecraft )",
                        "[ mc/admin ]( Guild admin commands )",
                        "[ mc/invite ]( Get the bot invite )"
                    },
                    // French
                    new string[]
                    {
                        "[ mc/bot ]( Bot Inviter/Info/Stats/Liens )",
                        "[ mc/quiz ]( Essai de Minecraft :D )",
                        "[ mc/colors ]( Codes de couleur MC )",
                        "[ mc/uuid (Joueur) ]( Joueur UUID )",
                        "[ mc/ping (IP) ]( Faire un ping sur un serveur )",
                        "[ mc/list ]( Liste des serveurs MC de la guilde )",
                        "[ mc/wiki ]( Wiki for Items/Mobs/Enchants/Potions )",
                        "[ mc/info ]( Informations sur les ventes MC )",
                        "[ mc/skin (Joueur) ]( Skin du joueur )",
                        "[ mc/names (Joueur) ]( Historique des nom de compte MC )",
                        "[ mc/status ]( Status des serveur de Mojange )",
                        //"[ mc/music ]( Jouer de la musique )",
                        "[ mc/get (Texte) ]( Obtenir une réussite )",
                        "[ mc/skinedit ]( Éditeur de skin en ligne )",
                        "[ mc/minime (Joueur) ]( Minimiser la skin du joueur )",
                        "[ mc/playing ]( Les gens qui jouent à Minecraft )",
                        "[ mc/admin ]( Commandes d'administration de la guilde )",
                        "[ mc/invite ]( Obtenir l'invitation de bot )"
                    },

                    // Spanish
                    new string[]
                    {
                        "[ mc/bot ]( Bot Invitación/Información/Estadísticas/Campo de golf )",
                        "[ mc/quiz ]( Minecraft quiz :D )",
                        "[ mc/colors ]( MC color codes )",
                        "[ mc/uuid (Player) ]( Jugador UUID )",
                        "[ mc/ping (IP) ]( Hacer ping a un servidor )",
                        "[ mc/list ]( Lista de servidores de MC del clan )",
                        "[ mc/wiki ]( Wiki de Artículos/Mobs/Enchants/Pociones )",
                        "[ mc/info ]( Información de ventas MC )",
                        "[ mc/skin (Player) ]( Piel del jugador )",
                        "[ mc/names (Player) ]( Historia del nombre de cuenta de MC )",
                        "[ mc/status ]( Estado de Mojang )",
                        //"[ mc/music ]( Play some music )",
                        "[ mc/get (Text) ]( Obtener un logro )",
                        "[ mc/skinedit ]( Editor de piel en línea )",
                        "[ mc/minime (Player) ]( Minify la piel del reproductor )",
                        "[ mc/playing ]( Les gens qui jouent à Minecraft )",
                        "[ mc/admin ]( Guild comandos de administración )",
                        "[ mc/invite ]( Obtener la invitación bot )"
                    },

                    // Russian
                     new string[]
                    {
                        "[ mc/bot ](  )",
                        "[ mc/quiz ](  )",
                        "[ mc/colors ](  )",
                        "[ mc/uuid (Player) ](  )",
                        "[ mc/ping (IP) ](  )",
                        "[ mc/list ](  )",
                        "[ mc/wiki ](  )",
                        "[ mc/info ](  )",
                        "[ mc/skin (Player) ](  )",
                        "[ mc/names (Player) ]( )",
                        "[ mc/status ](  )",
                        "[ mc/get (Text) ]( )",
                        "[ mc/skinedit ](  )",
                        "[ mc/minime (Player) ](  )",
                        "[ mc/playing ](  )",
                        "[ mc/admin ]( )",
                        "[ mc/invite ]( )"
                    }
                
            };

            public TString Test = new TString()
            {
                EN = "English",
                FR = "French",
                SP = "Spanish",
                RU = "Russian"
            };

            public TString Error_NoEmbedPerms = new TString()
            {
                EN = "Bot requires permission \" Embed Links \"",
                FR = "Bot nécessite l'autorisatio \" Liens incrémentés \"",
                SP = "Bot requiere permiso \" Enlazar Enlaces \"",
                RU = ""
            };

            public TString Help_FooterHiddenCommands = new TString()
            {
                EN = "There are some hidden commands aswell ;)",
                FR = "Il y a aussi des commandes cachées ;)",
                SP = "Hay algunos comandos ocultos también ;)",
                RU = ""
            };

            public TString Commands = new TString()
            {
                EN = "Commands",
                FR = "Commandes",
                SP = "Comandos",
                RU = ""
            };

            public TString Links = new TString()
            {
                EN = "Links",
                FR = "Des liens",
                SP = "Campo de golf",
                RU = ""
            };

            public TString MultiMC = new TString()
            {
                EN = "MultiMC allows you to manage and launch multiple versions with easy forge/mods installation",
                FR = "MultiMC vous permet de gérer et de lancer plusieurs versions avec une installation facile de forge / mods",
                SP = "MultiMC le permite administrar y lanzar varias versiones con una instalación fácil de forge / mods",
                RU = ""
            };

            public TString ColorCodes = new TString()
            {
                EN = "Minecraft Color Codes",
                FR = "Codes de couleur Minecraft",
                SP = "Códigos de color Minecraft",
                RU = ""
            };

            public TString Error_PlayerNotFound = new TString()
            {
                EN = "Player {0} not found",
                FR = "Joueur {0} pas trouvé",
                SP = "Jugador {0} extraviado",
                RU = ""
            };

            #region PingCommand
            public TString Error_EnterIP = new TString()
            {
                EN = "Enter an IP",
                FR = "Entrez une adresse IP",
                SP = "Introduzca un IP",
                RU = ""
            };
            public TString Error_IPMain = new TString()
            {
                EN = "You really think that would work?",
                FR = "Vous pensez vraiment que cela fonctionnerait?",
                SP = "¿De verdad crees que funcionaría?",
                RU = ""
            };
            public TString Error_IPRouter = new TString()
            {
                EN = "Minecraft servers dont run on routers DUH",
                FR = "Les serveurs Minecraft ne fonctionnent pas sur les routeurs DUH",
                SP = "Los servidores de Minecraft no funcionan con routers DUH",
                RU = ""
            };
            public TString Error_IPZero = new TString()
            {
                EN = "Not enough zeros?",
                FR = "Pas assez de zéros?",
                SP = "¿No hay suficientes ceros?",
                RU = ""
            };
            public TString Error_IPGoogle = new TString()
            {
                EN = "This is for minecraft servers not google :(",
                FR = "Ceci est pour les serveurs minecraft pas Google :(",
                SP = "Esto es para servidores de minecraft no google :(",
                RU = ""
            };
            public TString Error_IPYoutube = new TString()
            {
                EN = "This is for minecraft servers not youtube :(",
                FR = "Ceci est pour les serveurs Minecraft, pas youtube :(",
                SP = "Esto es para servidores de minecraft no youtube :(",
                RU = ""
            };
            public TString Error_IPMyWeb = new TString()
            {
                EN = "Trying to ping my own website :D",
                FR = "Essayer de faire un ping sur mon propre site web :D",
                SP = "Tratando de hacer ping a mi propio sitio web: D",
                RU = ""
            };
            public TString Error_IPBlocked = new TString()
            {
                EN = "Minecraft server has blocked the ping",
                FR = "Le serveur Minecraft a bloqué le ping",
                SP = "El servidor de Minecraft ha bloqueado el ping",
                RU = ""
            };
            public TString Error_EnableQuery = new TString()
            {
                EN = "Minecraft server does not have enable-query set in server.properties",
                FR = "Le serveur Minecraft n'a pas activé la requête dans server.properties",
                SP = "El servidor de Minecraft no tiene enable-query establecido en server.properties",
                RU = ""
            };
            public TString Error_IPInvalid = new TString()
            {
                EN = "This is not a valid ip",
                FR = "Ce n'est pas un ip valide",
                SP = "Esto no es un IP válido",
                RU = ""
            };
            public TString Error_Cooldown = new TString()
            {
                EN = "You are on cooldown for 1 mins!",
                FR = "Vous êtes en pause pendant 1 minutes!",
                SP = "Estás en cooldown por 1 minutos!",
                RU = ""
            };
            public TString Ping_PleaseWait = new TString()
            {
                EN = "Please wait while i ping",
                FR = "Patientez pendant que je ping",
                SP = "Por favor, espere mientras hago ping",
                RU = ""
            };
            public TString Ping_ServerLoading = new TString()
            {
                EN = "Server is loading!",
                FR = "Le serveur est en cours de chargement!",
                SP = "¡El servidor está cargando!",
                RU = ""
            };
            public TString Players = new TString()
            {
                EN = "Players",
                FR = "Joueurs",
                SP = "Jugadores",
                RU = ""
            };
            public TString Ping_ServerOffline = new TString()
            {
                EN = "Server is offline",
                FR = "Le serveur est hors-ligne",
                SP = "Server está desconectado",
                RU = ""
            };
            #endregion

            public TString List_NoServers = new TString()
            {
                EN = "This community has no servers listed",
                FR = "Cette communauté n'a aucun serveur répertorié",
                SP = "Esta comunidad no tiene servidores",
                RU = ""
            };
            public TString List_GuildAdmin = new TString()
            {
                EN = "Guild administrators should use",
                FR = "Les administrateurs de guilde devraient utiliser",
                SP = "Los administradores del gremio deben usar",
                RU = ""
            };
            public TString Servers = new TString()
            {
                EN = "Servers",
                FR = "Les serveurs",
                SP = "Servidores",
                RU = ""
            };

            #region InfoCommand
            public TString Info_MCSales = new TString()
            {
                EN = "Minecraft Account Sales",
                FR = "Ventes de compte Minecraft",
                SP = "Ventas de la cuenta de Minecraft",
                RU = ""
            };
            public TString Info_MCSalesUrl = new TString()
            {
                EN = "https://minecraft.net/en-us/stats/",
                FR = "https://minecraft.net/fr-fr/stats/",
                SP = "https://minecraft.net/es-es/stats/",
                RU = "https://minecraft.net/en-us/stats/"
            };
            public TString Info_SalesError = new TString()
            {
                EN = "Stats may be slightly off due to caching",
                FR = "Les statistiques peuvent être légèrement désactivées en raison de la mise en cache",
                SP = "Estadísticas pueden estar ligeramente fuera debido al almacenamiento en caché",
                RU = ""
            };
            public TString Error_Api = new TString()
            {
                EN = "API Error",
                FR = "Erreur API",
                SP = "Error de API",
                RU = ""
            };
            #endregion

            #region SkinCommand
            public TString Skin_Args = new TString()
            {
                EN = "(Player) | head | cube | full | steal",
                FR = "(Joueur) | head | cube | full | steal",
                SP = "(Jugador) | cabeza | cubo | completo | robar",
                RU = ""
            };
            public TString Skin_Stole = new TString()
            {
                EN = "Stole a skin",
                FR = "Roule une skin",
                SP = "Robó una piel",
                RU = ""
            };
            public TString Error_UnknownArg = new TString()
            {
                EN = "Unknown argument do",
                FR = "Argument inconnu",
                SP = "Argumento desconocido",
                RU = ""
            };
            #endregion

            #region NameCommand
            public TString Player = new TString()
            {
                EN = "Player",
                FR = "Joueur",
                SP = "Jugador",
                RU = ""
            };
            public TString Name_OneOnly = new TString()
            {
                EN = "Unknown argument do",
                FR = "Argument inconnu",
                SP = "Argumento desconocido",
                RU = ""
            };
            public TString First = new TString()
            {
                EN = "First",
                FR = "Premier",
                SP = "primero",
                RU = ""
            };
            public TString Name_PlayerNotFoundNames = new TString()
            {
                EN = "Player {0} not found, please use the current players name",
                FR = "Joueur {0} pas trouvé, utilisez le nom actuel des joueurs",
                SP = "Jugador {0} no encontrado, por favor use el nombre actual de los jugadores",
                RU = ""
            };
            #endregion

            #region StatusCommand
            public TString Status_Mojang = new TString()
            {
                EN = "Mojang Status",
                FR = "État des serveurs Mojang",
                SP = "Estado de Mojang",
                RU = ""
            };
            public TString Status_MojangAccounts = new TString()
            {
                EN = "Mojang accounts",
                FR = "Comptes Mojang",
                SP = "Cuentas de Mojang",
                RU = ""
            };
            public TString Status_MojangAuthServers = new TString()
            {
                EN = "Mojang auth servers",
                FR = "Serveurs d'authentification Mojang",
                SP = "Mojang servidores de autenticación",
                RU = ""
            };
            public TString Status_MojangAuthService = new TString()
            {
                EN = "Mojang auth service",
                FR = "Service d'authentification Mojang",
                SP = "Servicio Mojang auth",
                RU = ""
            };
            public TString Status_MojangSessions = new TString()
            {
                EN = "Mojang sessions",
                FR = "Séances de Mojang",
                SP = "Sesiones de Mojang",
                RU = ""
            };
            public TString Status_MinecraftSessions = new TString()
            {
                EN = "Minecraft.net sessions",
                FR = "Séances de Minecraft.net",
                SP = "Sesiones de Minecraft.net",
                RU = ""
            };
            public TString Status_MinecraftSkins = new TString()
            {
                EN = "Minecraft.net skins",
                FR = "Peaux Minecraft.net",
                SP = "Pieles de Minecraft.net",
                RU = ""
            };
            public TString Status_MinecraftTextures = new TString()
            {
                EN = "Minecraft.net textures",
                FR = "Les textures de Minecraft.net",
                SP = "Minecraft.net texturas",
                RU = ""
            };
            #endregion

            public TString PeoplePlayingMinecraft = new TString()
            {
                EN = "People playing Minecraft",
                FR = "Les gens qui jouent à Minecraft",
                SP = "Personas jugando Minecraft",
                RU = ""
            };
            public TString Text = new TString()
            {
                EN = "Text",
                FR = "Texte",
                SP = "Texto",
                RU = ""
            };
            public TString Hi = new TString()
            {
                EN = "Hi",
                FR = "salut",
                SP = "Hola",
                RU = ""
            };
            public TString Get_ErrorLimit = new TString()
            {
                EN = "Text cannot be more than 22 letters/numbers",
                FR = "Le texte ne peut pas dépasser 22 lettres/nombres",
                SP = "El texto no puede tener más de 22 letras / números",
                RU = ""
            };
            public TString OnlineSkinEditor = new TString()
            {
                EN = "Online Skin Editor",
                FR = "Éditeur de peau en ligne",
                SP = "Online Skin Editor",
                RU = ""
            };

#region BotCommand
            public TString Bot_Desc = new TString()
            {
                EN = "If you have any issue, suggestions or language translations please contact me",
                FR = "Si vous avez un problème, des suggestions ou des traductions linguistiques, contactez-moi",
                SP = "Si tiene algún problema, sugerencias o traducciones de idiomas, póngase en contacto conmigo",
                RU = ""
            };
            public TString Bot_Footer = new TString()
            {
                EN = "Bot owner commands do not count to the command count above also some of them are secret ones you have to find",
                FR = "Les commandes du propriétaire du robot ne comptent pas sur le compte de commande ci-dessus, mais certains d'entre eux sont secrets que vous devez trouver",
                SP = "Bot comandos de propietario no cuentan para el comando de contar por encima también algunos de ellos son secretos que tiene que encontrar",
                RU = ""
            };
            public TString Bot_Owner = new TString()
            {
                EN = "Bot Owner",
                FR = "Propriétaire du robot",
                SP = "Bot Owner",
                RU = ""
            };
            public TString Language = new TString()
            {
                EN = "Language",
                FR = "La langue",
                SP = "Idioma",
                RU = ""
            };
            public TString Bot_Lib = new TString()
            {
                EN = "Library",
                FR = "Bibliothèque",
                SP = "Biblioteca",
                RU = ""
            };
            public TString Stats = new TString()
            {
                EN = "Stats",
                FR = "Stats",
                SP = "Estadísticas",
                RU = ""
            };
            public TString Guilds = new TString()
            {
                EN = "Guilds",
                FR = "Guildes",
                SP = "Gremios",
                RU = ""
            };
            
            public TString Uptime = new TString()
            {
                EN = "Uptime",
                FR = "Temps de disponibilité",
                SP = "Tiempo de actividad",
                RU = ""
            };
            
            public TString Bot_Invite = new TString()
            {
                EN = "Bot Invite",
                FR = "Invité Bot",
                SP = "Bot Invita",
                RU = ""
            };
            
            public TString Bot_ListGuilds = new TString()
            {
                EN = "Bot List Guilds",
                FR = "Liste alphabétique",
                SP = "Bot List Guilds",
                RU = ""
            };
            #endregion
        }

        public class Hidden
        {
            public TString Wallpaper = new TString()
            {
                EN = "Wallpaper",
                FR = "Fond d'écran",
                SP = "Papel pintado",
                RU = ""
            };
            public TString ForgecraftWiki = new TString()
            {
                EN = "Wiki And Forgecraft Users",
                FR = "Utilisateurs Wiki et Forgecraft",
                SP = "Usuarios de Wiki y Forgecraft",
                RU = ""
            };
            public TString BukkitNews = new TString()
            {
                EN = "Bukkit News",
                FR = "Nouvelles de Bukkit",
                SP = "Noticias de Bukkit",
                RU = ""
            };
            public TString FoundSecretCommand = new TString()
            {
                EN = "Hey you found a secret command :D",
                FR = "Hé, vous avez trouvé une commande secrète :D",
                SP = "Hola, has encontrado un comando secreto :D",
                RU = ""
            };
            public TString MinecraftClassic = new TString()
            {
                EN = "Minecraft classic was the second phase of developent in 2009 that allowed players to play in the browser using java on the minecraft.net website which was primarly used to build things",
                FR = "Minecraft classic a été la deuxième phase de développement en 2009 qui a permis aux joueurs de jouer dans le navigateur en utilisant java sur le site web minecraft.net qui a été principalement utilisé pour construire des choses",
                SP = "Minecraft clásico fue la segunda fase de desarrollo en 2009 que permitió a los jugadores jugar en el navegador usando java en el sitio web minecraft.net que fue utilizado principalmente para construir cosas",
                RU = ""
            };
            public TString Forgecraft = new TString()
            {
                EN = "Forgecraft is the set of private whitelisted servers for mod developers to gather and beta-test their mods in a private environment. Many YouTubers and live-streamers also gather on the server to interact with the mod developers, help play-test the mods, and create videos to let the general public know what the mod developers are doing.",
                FR = "Forgecraft est l'ensemble des serveurs privés de liste blanche pour les développeurs mod pour rassembler et bêta-tester leurs mods dans un environnement privé. De nombreux YouTubers et live-streamers se rassemblent également sur le serveur pour interagir avec les développeurs de mod, aident à tester les mods et à créer des vidéos pour permettre au grand public de savoir ce que font les développeurs de mod.",
                SP = "Forgecraft es el conjunto de servidores privados de listas blancas para que los desarrolladores de mod puedan recopilar y probar sus mods en un entorno privado. Muchos YouTubers y live-streamers también se reúnen en el servidor para interactuar con los desarrolladores de mod, ayudar a jugar a probar los mods y crear videos para que el público en general sepa lo que están haciendo los desarrolladores de mod.",
                RU = ""
            };
            public TString ForgecraftWallpaper = new TString()
            {
                EN = "Forgecraft Wallpaper",
                FR = "Fond d'écran Forgecraft",
                SP = "Fondo de Pantalla de Forgecraft",
                RU = ""
            };
            public TString Bukkit = new TString()
            {
                EN = "RIP Bukkit you will be missed along with other server solutions...",
                FR = "RIP Bukkit vous manquera avec d'autres solutions serveur...",
                SP = "RIP Bukkit te faltará junto con otras soluciones de servidor ...",
                RU = ""
            };
            public TString Direwolf20 = new TString()
            {
                EN = "Direwolf20 is a popular youtuber known for his lets plays and mod tutorials on modded minecraft. He also plays on a private server called Forgecraft with a bunch of mod developers and other youtubers with his friends Soaryn and Pahimar",
                FR = "Direwolf20 est un populaire youtuber connu pour ses jeux de joueurs et tutoriels mod sur minecraft modded. Il joue également sur un serveur privé appelé Forgecraft avec un tas de développeurs de mod et d'autres youtubers avec ses amis Soaryn et Pahimar",
                SP = "Direwolf20 es un popular youtuber conocido por su permite juegos y tutoriales de mod en minecraft modded. También juega en un servidor privado llamado Forgecraft con un montón de desarrolladores de mod y otros youtubers con sus amigos Soaryn y Pahimar",
                RU = ""
            };
            public TString Herobrine = new TString()
            {
                EN = "Always watching you...",
                FR = "Te regarde toujours...",
                SP = "Siempre observándote ...",
                RU = ""
            };
            public TString Entity303 = new TString()
            {
                EN = "A minecraft creepy pasta of a former Mojang employee who was fired by Notch and now want revenge",
                FR = "Une creppy pasta de minecraft d'un ancien employé de Mojang qui a été renvoyé par Notch et veut maintenant se venger",
                SP = "Un minecraft espeluznante pasta de un ex empleado de Mojang que fue despedido por Notch y ahora quieren venganza",
                RU = ""
            };
            public TString Israphel = new TString()
            {
                EN = "The best youtube minecraft series that will never die in our hearts... 2010 - 2012 RIP Yogscast",
                FR = "La meilleure série de Youtube Minecraft qui ne mourra jamais dans nos coeurs ... 2010 - 2012 RIP Yogscast",
                SP = "La mejor serie de minecraft de youtube que nunca morirá en nuestros corazones ... 2010 - 2012 RIP Yogscast",
                RU = ""
            };
            public TString Notch = new TString()
            {
                EN = "Minecraft was created by Notch aka Markus Persson",
                FR = "Minecraft a été créé par Notch aka Markus Persson",
                SP = "Minecraft fue creado por Notch aka Markus Persson",
                RU = ""
            };
        }

        public class Admin
        {
            public List<string[]> Commands = new List<string[]>()
                {
                    // English
                    new string[]
                    {
                        "[ mc/lang ]( Set the community language )",
                        "[ mc/addserver ]( Add a MC server to this guild list )",
                        "[ mc/delserver ]( Remove a MC server from this guild list )",
                    },

                    // French
                    new string[]
                    {
                        "[ mc/lang ]( Définir la langue de la communauté )",
                        "[ mc/addserver ]( Ajouter un serveur MC à cette liste de guilde )",
                        "[ mc/delserver ]( Supprimez un serveur MC de cette liste de guilde )",

                    },

                    // Spanish
                    new string[]
                    {
                        "[ mc/lang ]( Définir la langue de la communauté )",
                        "[ mc/addserver ]( Ajouter un serveur MC à cette liste de guilde )",
                        "[ mc/delserver ]( Supprimez un serveur MC de cette liste de guilde )",
                    },

                    // Russian
                    new string[]
                    {
                        "[ mc/lang ]( )",
                        "[ mc/addserver ]( )",
                        "[ mc/delserver ]( )"
                    }
                };
            public TString LanguageTranslate = new TString()
            {
                EN = "Want a language translates? Contact",
                FR = "Vous voulez une langue traduire? Contact",
                SP = "¿Quiere traducir un idioma? Contacto",
                RU = ""
            };
            public TString NoData = new TString()
            {
                EN = "Could not find guild data contact xXBuilderBXx#9113",
                FR = "Impossible de trouver les informations de la guilde contact xXBuilderBXx#9113",
                SP = "No se pudieron encontrar los datos del clan xXBuilderBXx#9113",
                RU = ""
            };
            public TString AdminCommands = new TString()
            {
                EN = "Guild Admin Commands",
                FR = "Commandes d'administration de la guilde",
                SP = "Comandos de Guild Admin",
                RU = ""
            };
            public TString AdminOnly = new TString()
            {
                EN = "You are not a guild admin",
                FR = "Vous n'êtes pas un administrateur",
                SP = "No eres un administrador del gremio",
                RU = ""
            };
            public TString UseList = new TString()
            {
                EN = "Use mc/list for a list of this guilds minecraft servers",
                FR = "Utilisez mc/list pour une liste de ces serveurs minecraft de guilde",
                SP = "Utilice mc/list para obtener una lista de estos servidores de los minecraft de los gremios",
                RU = ""
            };
            public TString AddServer = new TString()
            {
                EN = "Enter a tag, ip and name",
                FR = "Entrez une étiquette, un ip et un nom",
                SP = "Introduzca una etiqueta, ip y nombre",
                RU = ""
            };
            public TString AddServer_Already = new TString()
            {
                EN = "This server is already on the list",
                FR = "Ce serveur est déjà sur la liste",
                SP = "Este servidor ya está en la lista",
                RU = ""
            };
            public TString AddServer_Added = new TString()
            {
                EN = "Added server",
                FR = "Serveur ajouté",
                SP = "Añadido servidor",
                RU = ""
            };
            public TString AddServer_AddedList = new TString()
            {
                EN = "to the guild list",
                FR = "à la liste de guilde",
                SP = "a la lista de los gremios",
                RU = ""
            };
            public TString DelServer_Enter = new TString()
            {
                EN = "Added server",
                FR = "Serveur ajouté",
                SP = "Añadido servidor",
                RU = ""
            };
            public TString DelServer_None = new TString()
            {
                EN = "This server is not on the list",
                FR = "Ce serveur n'est pas sur la liste",
                SP = "Este servidor no está en la lista",
                RU = ""
            };
            public TString DelServer_Deleted = new TString()
            {
                EN = "Removed server",
                FR = "Serveur supprimé",
                SP = "Servidor eliminado",
                RU = ""
            };
            public TString DelServer_List = new TString()
            {
                EN = "from the guild list",
                FR = "de la liste de guilde",
                SP = "de la lista de gremios",
                RU = ""
            };
            public TString ChangeLang = new TString()
            {
                EN = "Change Community Language",
                FR = "Changer la langue de la communauté",
                SP = "Cambiar idioma de la comunidad",
                RU = ""
            };
        }
    }
}
