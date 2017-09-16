using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot
{
    public class _Translate
    {
        public class Commands
        {
            public class Main
            {
                public List<string[]> Commands = new List<string[]>()
                {
                    // English
                    new string[]
                    {
                        "[ mc/quiz ]( Minecraft quiz :D )",
                        "[ mc/colors ]( MC color codes )",
                        "[ mc/uuid (Player) ]( Player UUID )",
                        "[ mc/ping (IP) ]( Ping a server )",
                        "[ mc/list ]( List guild MC servers )",
                        "[ mc/info ]( MC sales info )",
                        "[ mc/skin (Player) ]( Player skin )",
                        "[ mc/names (Player) ]( MC account name history )",
                        "[ mc/status ]( Mojang status )",
                        //"[ mc/music ]( Play some music )",
                        "[ mc/get (Text) ]( Get an achievement )",
                        "[ mc/skinedit ]( Online skin editor )",
                        "[ mc/minime (Player) ]( Minify player skin )",
                        "[ mc/admin ]( Guild admin commands )",
                        "[ mc/invite ]( Get the bot invite )"
                    },
                    // French
                    new string[]
                    {
                        "[ mc/quiz ]( Essai de Minecraft :D )",
                        "[ mc/colors ]( Codes de couleur MC )",
                        "[ mc/uuid (Joueur) ]( Joueur UUID )",
                        "[ mc/ping (IP) ]( Faire un ping sur un serveur )",
                        "[ mc/list ]( Liste des serveurs MC de la guilde )",
                        "[ mc/info ]( Informations sur les ventes MC )",
                        "[ mc/skin (Joueur) ]( Skin du joueur )",
                        "[ mc/names (Joueur) ]( Historique des nom de compte MC )",
                        "[ mc/status ]( Status des serveur de Mojange )",
                        //"[ mc/music ]( Jouer de la musique )",
                        "[ mc/get (Texte) ]( Obtenir une réussite )",
                        "[ mc/skinedit ]( Éditeur de skin en ligne )",
                        "[ mc/minime (Joueur) ]( Minimiser la skin du joueur )",
                        "[ mc/admin ]( Commandes d'administration de la guilde )",
                        "[ mc/invite ]( Obtenir l'invitation de bot )"
                    },
                    // Spanish
                    new string[]
                    {
                        "[ mc/quiz ]( Minecraft quiz :D )",
                        "[ mc/colors ]( MC color codes )",
                        "[ mc/uuid (Player) ]( Jugador UUID )",
                        "[ mc/ping (IP) ]( Hacer ping a un servidor )",
                        "[ mc/list ]( Lista de servidores de MC del clan )",
                        "[ mc/info ]( Información de ventas MC )",
                        "[ mc/skin (Player) ]( Piel del jugador )",
                        "[ mc/names (Player) ]( Historia del nombre de cuenta de MC )",
                        "[ mc/status ]( Estado de Mojang )",
                        //"[ mc/music ]( Play some music )",
                        "[ mc/get (Text) ]( Obtener un logro )",
                        "[ mc/skinedit ]( Editor de piel en línea )",
                        "[ mc/minime (Player) ]( Minify la piel del reproductor )",
                        "[ mc/admin ]( Guild comandos de administración )",
                        "[ mc/invite ]( Obtener la invitación bot )"
                    },
                };


                public string[] NoEmbedPerm = new string[]
                {
                    "Bot requires permission \" Embed Links \"",
                    "Bot nécessite l'autorisatio \" Liens incrémentés \"",
                    "Bot requiere permiso \" Enlazar Enlaces \""
                };
                public string[] HelpFooterHiddenCommands = new string[]
                    {
                        "There are some hidden commands aswell ;)",
                        "Il y a aussi des commandes cachées",
                        "Hay algunos comandos ocultos también ;)"
                    };
                public string[] ThisCommunity = new string[]
                {
                    "This Community",
                    "Cette communauté",
                    "Esta comunidad"
                };
                public string[] CommunityError = new string[]
                {
                    "This guild is not registered as a community contact the guild owner to set it up",
                    "Cette guilde n'est pas enregistrée en tant que communauté contactez le propriétaire de la guilde pour la mettre en place",
                    "Este gremio no está registrado como una comunidad en contacto con el propietario del gremio para configurarlo"
                };
                public string[] MultiMC = new string[]
                {
                    "MultiMC allows you to manage and launch multiple versions with easy forge/mods installation",
                    "MultiMC vous permet de gérer et de lancer plusieurs versions avec une installation facile de forge / mods",
                    "MultiMC le permite administrar y lanzar varias versiones con una instalación fácil de forge / mods"
                };
                public string[] GetAchievementError = new string[]
                {
                    "Text cannot be more than 20 letters/numbers",
                    "Le texte ne peut pas dépasser 20 lettres/nombres",
                    "El texto no puede tener más de 20 letras / números"
                };
                public string[] PlayerNotFound = new string[]
                {
                    "Player {0} not found",
                    "Joueur {0} pas trouvé",
                    "Jugador {0} extraviado"
                };
                public string[] PlayerNotFoundNames = new string[]
                {
                    "Player {0} not found, please use the current players name",
                    "Joueur {0} pas trouvé, utilisez le nom actuel des joueurs",
                    "Jugador {0} no encontrado, por favor use el nombre actual de los jugadores"
                };
                public string[] First = new string[]
                {
                    "First",
                    "Premier",
                    "primero"
                };
                public string[] MojangStatus = new string[]
                {
                    "Mojang Status",
                    "État des serveurs Mojang",
                    "Estado de Mojang"
                };
                public string[] ApiError = new string[]
                {
                    "API Error",
                    "Erreur API",
                    "Error de API"
                };
                public string[] UsernamesOnlyOne = new string[]
                {
                    "Account {0} only has 1 username recorded",
                    "Compte {0} seul le nom d'utilisateur est enregistré",
                    "Cuenta {0} sólo tiene 1 nombre de usuario registrado"
                };
                public string[] StoleASkin = new string[]
                {
                    "Stole a skin",
                    "Roule une skin",
                    "Robó una piel"
                };
                public string[] UnknownArg = new string[]
                {
                    "Unknown argument do",
                    "Argument inconnu",
                    "Argumento desconocido"
                };
                public string[] SkinArgs = new string[]
                {
                    "(Player) | head | cube | full | steal",
                    "(Joueur) | head | cube | full | steal",
                    "(Jugador) | cabeza | cubo | completo | robar"
                };
                public string[] MinecraftSales = new string[]
                {
                    "Minecraft Account Sales",
                    "Ventes de compte Minecraft",
                    "Ventas de la cuenta de Minecraft"
                };
                public string[] MinecraftSalesError = new string[]
                {
                    "Stats may be slightly off due to caching",
                    "Les statistiques peuvent être légèrement désactivées en raison de la mise en cache",
                    "Estadísticas pueden estar ligeramente fuera debido al almacenamiento en caché"
                };
                public string[] MinecraftSalesUrl = new string[]
                {
                    "https://minecraft.net/en-us/stats/",
                    "https://minecraft.net/fr-fr/stats/",
                    "https://minecraft.net/es-es/stats/"
                };
                public string[] MojangAccounts = new string[]
                {
                    "Mojang accounts",
                    "Comptes Mojang",
                    "Cuentas de Mojang"
                };
                public string[] MojangAuthServers = new string[]
                {
                    "Mojang auth servers",
                    "Serveurs d'authentification Mojang",
                    "Mojang servidores de autenticación"
                };
                public string[] NoGuildServers = new string[]
                {
                    "This guild has no servers listed",
                    "Cette guilde n'a pas de serveurs listés",
                    "Este gremio no tiene servidores"
                };
                public string[] Servers = new string[]
                {
                    "Servers",
                    "Les serveurs",
                    "Servidores"
                };
                public string[] EnterIP = new string[]
                {
                    "Enter an IP",
                    "Entrez une adresse IP",
                    "Introduzca un IP"
                };
                public string[] IPErrorMain = new string[]
                {
                    "You really think that would work?",
                    "Vous pensez vraiment que cela fonctionnerait?",
                    "¿De verdad crees que funcionaría?"
                };
                public string[] IPErrorRouter = new string[]
                {
                    "Minecraft servers dont run on routers DUH",
                    "Les serveurs Minecraft ne fonctionnent pas sur les routeurs DUH",
                    "Los servidores de Minecraft no funcionan con routers DUH"
                };
                public string[] IPErrorZero = new string[]
                {
                    "Not enough zeros?",
                    "Pas assez de zéros?",
                    "¿No hay suficientes ceros?"
                };
                public string[] IPErrorGoogle = new string[]
                {
                    "This is for minecraft servers not google :(",
                    "Ceci est pour les serveurs minecraft pas Google :(",
                    "Esto es para servidores de minecraft no google :("
                };
                public string[] IPErrorYoutube = new string[]
                {
                    "This is for minecraft servers not youtube :(",
                    "Ceci est pour les serveurs Minecraft, pas youtube :(",
                    "Esto es para servidores de minecraft no youtube :("
                };
                public string[] IPErrorMyWeb = new string[]
                {
                    "Trying to ping my own website :D",
                    "Essayer de faire un ping sur mon propre site web :D",
                    "Tratando de hacer ping a mi propio sitio web: D"
                };
                public string[] IPErrorHypixel = new string[]
                {
                    "Hypixel network has blocked the ping sorry :(",
                    "Le réseau Hypixel a bloqué le ping désolé :(",
                    "Hypixel red ha bloqueado el ping lo siento :("
                };
                public string[] InvalidIP = new string[]
                {
                    "This is not a valid ip",
                    "Ce n'est pas un ip valide",
                    "Esto no es un IP válido"
                };
                public string[] Cooldown = new string[]
                {
                    "You are on cooldown for 2 mins!",
                    "Vous êtes en pause pendant 2 minutes!",
                    "Estás en cooldown por 2 minutos!"
                };
                public string[] PleaseWait = new string[]
                {
                    "Please wait while i ping",
                    "Patientez pendant que je ping",
                    "Por favor, espere mientras hago ping"
                };
                public string[] ServerLoading = new string[]
                {
                    "Server is loading!",
                    "Le serveur est en cours de chargement!",
                    "¡El servidor está cargando!"
                };
                public string[] Players = new string[]
                {
                    "Servers running with BungeeCord will not get the right player count for a single server",
                    "Les serveurs qui fonctionnent avec BungeeCord n'obtiennent pas le bon nombre de joueurs pour un seul serveur",
                    "Los servidores que funcionan con BungeeCord no obtendrán el número de jugadores adecuado para un solo servidor"
                };
                public string[] ServerOffline = new string[]
                {
                    "Server is offline",
                    "Le serveur est hors-ligne",
                    "Server está desconectado"
                };
                public string[] BungeeCord = new string[]
                {
                    "BungeeCord",
                    "Cordon élastique",
                    "Cuerda elástica"
                };
                public string[] HelpLinks = new string[]
                    {
                        "Links",
                        "Des liens",
                        "Campo de golf"
                    };
                public string[] HelpCommands = new string[]
                {
                    "Commands",
                    "Commandes",
                    "Comandos"
                };
                public string[] ColorCodes = new string[]
                {
                    "Minecraft Color Codes",
                    "Codes de couleur Minecraft",
                    "Códigos de color Minecraft"
                };
                public string[] NoGuildServersAdmin = new string[]
                {
                    "Guild administrators should use",
                    "Les administrateurs de guilde devraient utiliser",
                    "Los administradores del gremio deben usar"
                };
                public string[] MojangSessions = new string[]
                {
                    "Mojang sessions",
                    "Séances de Mojang",
                    "Sesiones de Mojang"
                };
                public string[] MinecraftSkins = new string[]
                {
                    "Minecraft.net skins",
                    "Peaux Minecraft.net",
                    "Pieles de Minecraft.net"
                };
                public string[] MojangAuthService = new string[]
                {
                    "Mojang auth service",
                    "Service d'authentification Mojang",
                    "Servicio Mojang auth"
                };
                public string[] MinecraftSessions = new string[]
                {
                    "Minecraft.net sessions",
                    "Séances de Minecraft.net",
                    "Sesiones de Minecraft.net"
                };
                public string[] MinecraftTextures = new string[]
                {
                    "Minecraft.net textures",
                    "Les textures de Minecraft.net",
                    "Minecraft.net texturas"
                };
                public string[] OnlineSkinEditor = new string[]
                {
                    "Online Skin Editor",
                    "Éditeur de peau en ligne",
                    "Online Skin Editor"
                };
            }
            public class Music
            {

            }
            public class Hidden
            {
                public string[] Wallpaper = new string[]
                {
                    "Wallpaper",
                    "Fond d'écran",
                    "Papel pintado"
                };
                public string[] ForgecraftWiki = new string[]
                {
                    "Wiki And Forgecraft Users",
                    "Utilisateurs Wiki et Forgecraft",
                    "Usuarios de Wiki y Forgecraft"
                };
                public string[] BukkitNews = new string[]
                {
                    "Bukkit News",
                    "Nouvelles de Bukkit",
                    "Noticias de Bukkit"
                };
                public string[] FoundSecretCommand = new string[]
                {
                    "Hey you found a secret command :D",
                    "Hé, vous avez trouvé une commande secrète :D",
                    "Hola, has encontrado un comando secreto :D"
                };
                public string[] MinecraftClassic = new string[]
                {
                    "Minecraft classic was the second phase of developent in 2009 that allowed players to play in the browser using java on the minecraft.net website which was primarly used to build things",
                    "Minecraft classic a été la deuxième phase de développement en 2009 qui a permis aux joueurs de jouer dans le navigateur en utilisant java sur le site web minecraft.net qui a été principalement utilisé pour construire des choses",
                    "Minecraft clásico fue la segunda fase de desarrollo en 2009 que permitió a los jugadores jugar en el navegador usando java en el sitio web minecraft.net que fue utilizado principalmente para construir cosas"
                };
                public string[] Forgecraft = new string[]
                {
                    "Forgecraft is the set of private whitelisted servers for mod developers to gather and beta-test their mods in a private environment. Many YouTubers and live-streamers also gather on the server to interact with the mod developers, help play-test the mods, and create videos to let the general public know what the mod developers are doing.",
                    "Forgecraft est l'ensemble des serveurs privés de liste blanche pour les développeurs mod pour rassembler et bêta-tester leurs mods dans un environnement privé. De nombreux YouTubers et live-streamers se rassemblent également sur le serveur pour interagir avec les développeurs de mod, aident à tester les mods et à créer des vidéos pour permettre au grand public de savoir ce que font les développeurs de mod.",
                    "Forgecraft es el conjunto de servidores privados de listas blancas para que los desarrolladores de mod puedan recopilar y probar sus mods en un entorno privado. Muchos YouTubers y live-streamers también se reúnen en el servidor para interactuar con los desarrolladores de mod, ayudar a jugar a probar los mods y crear videos para que el público en general sepa lo que están haciendo los desarrolladores de mod."
                };
                public string[] ForgecraftWallpaper = new string[]
                {
                    "Forgecraft Wallpaper",
                    "Fond d'écran Forgecraft",
                    "Fondo de Pantalla de Forgecraft"
                };
                public string[] Bukkit = new string[]
                {
                    "RIP Bukkit you will be missed along with other server solutions...",
                    "RIP Bukkit vous manquera avec d'autres solutions serveur...",
                    "RIP Bukkit te faltará junto con otras soluciones de servidor ..."
                };
                public string[] Direwolf20 = new string[]
                {
                    "Direwolf20 is a popular youtuber known for his lets plays and mod tutorials on modded minecraft. He also plays on a private server called Forgecraft with a bunch of mod developers and other youtubers with his friends Soaryn and Pahimar",
                    "Direwolf20 est un populaire youtuber connu pour ses jeux de joueurs et tutoriels mod sur minecraft modded. Il joue également sur un serveur privé appelé Forgecraft avec un tas de développeurs de mod et d'autres youtubers avec ses amis Soaryn et Pahimar",
                    "Direwolf20 es un popular youtuber conocido por su permite juegos y tutoriales de mod en minecraft modded. También juega en un servidor privado llamado Forgecraft con un montón de desarrolladores de mod y otros youtubers con sus amigos Soaryn y Pahimar"
                };
                public string[] Herobrine = new string[]
                {
                    "Always watching you...",
                    "Te regarde toujours...",
                    "Siempre observándote ..."
                };
                public string[] Entity303 = new string[]
                {
                    "A minecraft creepy pasta of a former Mojang employee who was fired by Notch and now want revenge",
                    "Une creppy pasta de minecraft d'un ancien employé de Mojang qui a été renvoyé par Notch et veut maintenant se venger",
                    "Un minecraft espeluznante pasta de un ex empleado de Mojang que fue despedido por Notch y ahora quieren venganza"
                };
                public string[] Israphel = new string[]
                {
                    "The best youtube minecraft series that will never die in our hearts... 2010 - 2012 RIP Yogscast",
                    "La meilleure série de Youtube Minecraft qui ne mourra jamais dans nos coeurs ... 2010 - 2012 RIP Yogscast",
                    "La mejor serie de minecraft de youtube que nunca morirá en nuestros corazones ... 2010 - 2012 RIP Yogscast"
                };
                public string[] Notch = new string[]
                {
                    "Minecraft was created by Notch aka Markus Persson",
                    "Minecraft a été créé par Notch aka Markus Persson",
                    "Minecraft fue creado por Notch aka Markus Persson"
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
                        "[ mc/setname ]( Set the community name )",
                        "[ mc/setdesc ]( Set the community description )",
                        "[ mc/setlink ]( Set the community link )"
                    },

                    // French
                    new string[]
                    {
                        "[ mc/lang ]( Définir la langue de la communauté )",
                        "[ mc/addserver ]( Ajouter un serveur MC à cette liste de guilde )",
                        "[ mc/delserver ]( Supprimez un serveur MC de cette liste de guilde )",
                        "[ mc/setname ]( Définir le nom de la communauté )",
                        "[ mc/setdesc ]( Définir la description de la communauté )",
                        "[ mc/setlink ]( Définir le lien de la communauté )"
                        
                    },

                    // Spanish
                    new string[]
                    {
                        "[ mc/lang ]( Définir la langue de la communauté )",
                        "[ mc/addserver ]( Ajouter un serveur MC à cette liste de guilde )",
                        "[ mc/delserver ]( Supprimez un serveur MC de cette liste de guilde )",
                        "[ mc/setname ]( Définir le nom de la communauté )",
                        "[ mc/setdesc ]( Définir la description de la communauté )",
                        "[ mc/setlink ]( Définir le lien de la communauté )"
                    },
                };
                public string[] LanguageTranslate = new string[]
                {
                    "Want a language translates? Contact",
                    "Vous voulez une langue traduire? Contact",
                    "¿Quiere traducir un idioma? Contacto"
                };
                public string[] NoData = new string[]
                {
                    "Could not find guild data contact xXBuilderBXx#9113",
                    "Impossible de trouver les informations de la guilde contact xXBuilderBXx#9113",
                    "No se pudieron encontrar los datos del clan xXBuilderBXx#9113"
                };

                public string[] GuildCommand = new string[]
                {
                    "Guild Admin Commands",
                    "Commandes d'administration de la guilde",
                    "Comandos de Guild Admin"
                };
                public string[] GuildAdminOnly = new string[]
                {
                    "You are not a guild admin",
                    "Vous n'êtes pas un administrateur",
                    "No eres un administrador del gremio"
                };

                public string[] UseList = new string[]
                {
                    "Use mc/list for a list of this guilds minecraft servers",
                    "Utilisez mc/list pour une liste de ces serveurs minecraft de guilde",
                    "Utilice mc / list para obtener una lista de estos servidores de los minecraft de los gremios"
                };
                public string[] AddServer = new string[]
                {
                    "Enter a tag, ip and name",
                    "Entrez une étiquette, un ip et un nom",
                    "Introduzca una etiqueta, ip y nombre"
                };
                public string[] AddServerAlready = new string[]
                {
                    "This server is already on the list",
                    "Ce serveur est déjà sur la liste",
                    "Este servidor ya está en la lista"
                };
                public string[] AddedServer = new string[]
                {
                    "Added server",
                    "Serveur ajouté",
                    "Añadido servidor"
                };
                public string[] AddedServerList = new string[]
                {
                    "to the guild list",
                    "à la liste de guilde",
                    "a la lista de los gremios"
                };
                public string[] DeleteEnterTag = new string[]
                {
                    "Enter the tag of a server to delete from the list",
                    "Entrez la balise d'un serveur à supprimer de la liste",
                    "Introduzca la etiqueta de un servidor para eliminar de la lista"
                };
                public string[] DeleteNoServer = new string[]
                {
                    "This server is not on the list",
                    "Ce serveur n'est pas sur la liste",
                    "Este servidor no está en la lista"
                };
                public string[] DeletedServer = new string[]
                {
                    "Removed server",
                    "Serveur supprimé",
                    "Servidor eliminado"
                };
                public string[] DeletedServerList = new string[]
                {
                    "from the guild list",
                    "de la liste de guilde",
                    "de la lista de gremios"
                };
                public string[] EnterName = new string[]
                {
                    "You need to enter a name",
                    "Vous devez entrer un nom",
                    "Necesitas ingresar un nombre"
                };
                public string[] Name = new string[]
                {
                    "Name",
                    "prénom",
                    "Nombre"
                };
                public string[] CommunityNameSet = new string[]
                {
                    "Community name has been set",
                    "Le nom de la communauté a été défini",
                    "El nombre de la comunidad se ha establecido"
                };
                public string[] EnterDesc = new string[]
                {
                    "You need to enter a description",
                    "Vous devez entrer une description",
                    "Debe introducir una descripción"
                };

                public string[] CommunityDescSet = new string[]
                {
                    "Community description has been set",
                    "La description de la communauté a été définie",
                    "Se ha establecido la descripción de la comunidad"
                };

                public string[] EnterLink = new string[]
                {
                    "You need to enter a link",
                    "Vous devez entrer un lien",
                    "Necesitas ingresar un enlace"
                };
                public string[] LinkInvalid = new string[]
                {
                    "This link is not valid",
                    "Ce lien n'est pas valide",
                    "Este enlace no es válido"
                };
                public string[] CommunityLinkSet = new string[]
                {
                    "Community link has been set",
                    "Le lien de communauté a été défini",
                    "Se ha establecido un vínculo comunitario"
                };
                public string[] Text = new string[]
                {
                    "Text",
                    "Texte",
                    "Texto"
                };
                public string[] DescPlaceholder = new string[]
                {
                    "Best minecraft community",
                    "La meilleure communauté minecraft",
                    "Mejor comunidad minecraft"
                };
                public string[] ChangeLang = new string[]
                {
                    "Change Community Language",
                    "Changer la langue de la communauté",
                    "Cambiar idioma de la comunidad"
                };
            }

            public class Quiz
            {
                //public string[] RanOutOfTime = new string[]
                //{
                    //"EngTest", "FrenchTest"
                //};
            }
        }
    
    }
}
