﻿using Bot.Classes;
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
        ///  Translation string
        /// </summary>
        public class TString // LEAVE THIS ALONE
        {
            /// <summary>
            /// English language
            /// </summary>
            public string EN = "";
            /// <summary>
            /// French language
            /// </summary>
            public string FR = "";
            /// <summary>
            /// Spanish language
            /// </summary>
            public string SP = "";
            /// <summary>
            /// Russian language
            /// </summary>
            public string RU = "";
            /// <summary>
            /// Portuguese language
            /// </summary>
            public string PO = "";
            /// <summary>
            /// Germnan language
            /// </summary>
            public string GR = "";
            /// <summary>
            /// Dutch language
            /// </summary>
            public string DU = "";
            /// <summary>
            /// Italian language
            /// </summary>
            public string IT = "";

            /// <summary>
            /// Get the language translation
            /// </summary>
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
                        case 4:
                            return PO;
                        case 5:
                            return GR;
                        case 6:
                            return DU;
                        case 7:
                            return IT;
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
            
            public HashSet<string[]> HelpCommands = new HashSet<string[]>()
                {
                    // English
                    new string[]
                    {
                        "[ mc/bot ]( Bot Invite/Info/Stats/Links )",
                        "[ mc/quiz ]( Minecraft quiz :D )",
                        "[ mc/colors ]( MC color codes )",
                        "[ mc/uuid (Player) ]( Player UUID )",
                        "[ mc/ping (IP) ]( Ping a server )",
                        "[ mc/list ]( List community servers )",
                        "[ mc/wiki ]( Wiki for Items/Mobs/Enchants/Potions )",
                        "[ mc/version ]( Minecraft version info )",
                        "[ mc/skin (Player) ]( Player skin )",
                        "[ mc/names (Player) ]( MC account name history )",
                        "[ mc/status ]( Mojang status )",
                        "[ mc/get (Text) ]( Get an achievement )",
                        "[ mc/minime (Player) ]( Minify player skin )",
                        "[ mc/playing ]( People playing Minecraft )",
                        "[ mc/admin ]( Community admin commands )",
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
                        "[ mc/list ]( Liste des serveurs communautaires )",
                        "[ mc/wiki ]( Wiki for Items/Mobs/Enchants/Potions )",
                        "[ mc/version ]( Minecraft version info )",
                        "[ mc/skin (Joueur) ]( Skin du joueur )",
                        "[ mc/names (Joueur) ]( Historique des nom de compte MC )",
                        "[ mc/status ]( Status des serveur de Mojange )",
                        "[ mc/get (Texte) ]( Obtenir une réussite )",
                        "[ mc/minime (Joueur) ]( Minimiser la skin du joueur )",
                        "[ mc/playing ]( Les gens qui jouent à Minecraft )",
                        "[ mc/admin ]( Commandes d'administration de la communauté )",
                        "[ mc/invite ]( Obtenir l'invitation de bot )"
                    },

                    // Spanish
                    new string[]
                    {
                        "[ mc/bot ]( Bot Invitación/Información/Estadísticas/Campo de golf )",
                        "[ mc/quiz ]( Minecraft quiz :D )",
                        "[ mc/colors ]( MC color codes )",
                        "[ mc/uuid (Jugador) ]( Jugador UUID )",
                        "[ mc/ping (IP) ]( Hacer ping a un servidor )",
                        "[ mc/list ]( Listar servidores comunitarios )",
                        "[ mc/wiki ]( Wiki de Artículos/Mobs/Enchants/Pociones )",
                        "[ mc/version ]( Minecraft version info )",
                        "[ mc/skin (Jugador) ]( Piel del jugador )",
                        "[ mc/names (Jugador) ]( Historia del nombre de cuenta de MC )",
                        "[ mc/status ]( Estado de Mojang )",
                        "[ mc/get (Texto) ]( Obtener un logro )",
                        "[ mc/minime (Jugador) ]( Minify la piel del reproductor )",
                        "[ mc/playing ]( Les gens qui jouent à Minecraft )",
                        "[ mc/admin ]( Comandos de administración de la comunidadn )",
                        "[ mc/invite ]( Obtener la invitación bot )"
                    },

                    // Russian
                     new string[]
                    {
                        "[ mc/bot ]( Основная информация о боте: приглашение/статистика/ссылки )",
                        "[ mc/quiz ]( Викторина :D )",
                        "[ mc/colors ]( Коды цветов )",
                        "[ mc/uuid (игрок) ]( UUID игрока )",
                        "[ mc/ping (IP) ]( Пинг сервера )",
                        "[ mc/list ]( Список серверов сообщества )",
                        "[ mc/wiki ]( Вики: информация о мобах/предметах/зачарованиях и т.д. )",
                        "[ mc/version ]( Minecraft version info )",
                        "[ mc/skin (игрок) ]( Скин игрока )",
                        "[ mc/names (игрок) ]( История имён аккаунта )",
                        "[ mc/status ]( Статус Mojang )",
                        "[ mc/get (текст) ]( Генератор достижений )",
                        "[ mc/minime (игрок) ]( Минимизировать скин игрока )",
                        "[ mc/playing ]( Сколько людей играют в Minecraft на данный момент )",
                        "[ mc/admin ]( Команды администратора сообщества )",
                        "[ mc/invite ]( Ссылка на добавление бота на свой сервер )"
                    },

                     // Portugese
                    new string[]
                    {
                        "[ mc/bot ]( Bot Convite/Info/Status/Links )",
                        "[ mc/quiz ]( Minecraft Quiz :D )",
                        "[ mc/colors ]( Códigos das Cores MC )",
                        "[ mc/uuid (Jogador) ]( UUID do Jogador )",
                        "[ mc/ping (IP) ]( Ping do Servidor )",
                        "[ mc/list ]( Lista de servidores da comunidade )",
                        "[ mc/wiki ]( Wiki de Items/Mobs/Enchants/Potions )",
                        "[ mc/version ]( Minecraft version info )",
                        "[ mc/skin (Jogador) ]( Skin do Jogador )",
                        "[ mc/names (Jogador) ]( Histórico de Nomes MC )",
                        "[ mc/status ]( Mojang Status )",
                        "[ mc/get (Texto) ]( Obter uma Conquista )",
                        "[ mc/minime (Jogador) ]( MiniEu de uma Skin )",
                        "[ mc/playing ]( Pessoas a Jogar Minecraft )",
                        "[ mc/admin ]( Comandos de administração da comunidade )",
                        "[ mc/invite ]( Convite do BOT )"
                    },

                    // German
                    new string[]
                    {
                        "[ mc/bot ]( Bot Einladen/Info/Stats/Links )",
                        "[ mc/quiz ]( Minecraft quiz :D )",
                        "[ mc/colors ]( MC-Farbcodes )",
                        "[ mc/uuid (Spieler) ]( Spieler UUID )",
                        "[ mc/ping (IP) ]( Ping ein Server )",
                        "[ mc/list ]( Liste der Community-Server )",
                        "[ mc/wiki ]( Wiki für Items/Mobs/Enchants/Tränke )",
                        "[ mc/version ]( Minecraft version info )",
                        "[ mc/skin (Spieler) ]( Spielerhaut )",
                        "[ mc/names (Spieler) ]( MC-Kontoname Historie )",
                        "[ mc/status ]( Mojang-Status )",
                        "[ mc/get (Text) ]( Holen Sie sich einen Erfolg )",
                        "[ mc/minime (Spieler) ]( Vergrößern der Spielerhaut )",
                        "[ mc/playing ]( Leute spielen Minecraft )",
                        "[ mc/admin ]( Community-Admin-Befehle )",
                        "[ mc/invite ]( Holen Sie sich die Bot-Einladung )"
                    },
                    
                    // Dutch
                    new string[]
                    {
                        "[ mc/bot ]( Bot Invite/Info/Stats/Links )",
                        "[ mc/quiz ]( Minecraft quiz :D )",
                        "[ mc/colors ]( MC color codes )",
                        "[ mc/uuid (Player) ]( Player UUID )",
                        "[ mc/ping (IP) ]( Ping a server )",
                        "[ mc/list ]( List community servers )",
                        "[ mc/wiki ]( Wiki voor Items/Mobs/Enchants/Potions )",
                        "[ mc/version ]( Minecraft versie informatie )",
                        "[ mc/skin (Player) ]( Player skin )",
                        "[ mc/names (Player) ]( MC account naam historie )",
                        "[ mc/status ]( Mojang status )",
                        "[ mc/get (Text) ]( Krijg een achievement )",
                        "[ mc/minime (Player) ]( Mini player skin )",
                        "[ mc/playing ]( Members die Minecraft spelers )",
                        "[ mc/admin ]( Community admin commands )",
                        "[ mc/invite ]( Krijg een bot invite )"
                    },

                    // Italian
                    new string[]
                    {
                        "[ mc/bot ]( Invito del bot/Info/Statistiche/Links )",
                        "[ mc/quiz ]( Minecraft quiz :D )",
                        "[ mc/colors ]( Codici dei Colori MC )",
                        "[ mc/uuid (Player) ]( Player UUID )",
                        "[ mc/ping (IP) ]( Pinga un server )",
                        "[ mc/list ]( Lista dei server della community )",
                        "[ mc/wiki ]( Wiki per Oggetti/Mobs/Incantamenti/Pozioni )",
                        "[ mc/version ]( Info sulla versione di MC )",
                        "[ mc/skin (Player) ]( Skin del Giocatore )",
                        "[ mc/names (Player) ]( Storia del nome dell' account MC )",
                        "[ mc/status ]( Stato della Mojang )",
                        "[ mc/get (Text) ]( Ottieni un achievement )",
                        "[ mc/minime (Player) ]( Minimizza la skin del giocatore )",
                        "[ mc/playing ]( Persone che stanno giocando ad MC )",
                        "[ mc/admin ]( Comandi dell' admin della community )",
                        "[ mc/invite ]( Ottieni l' invito del bot )"
                    }
            };

            public TString Error_NoEmbedPerms = new TString()
            {
                EN = "Bot requires permission \" Embed Links \"",
                FR = "Bot nécessite l'autorisatio \" Liens incrémentés \"",
                SP = "Bot requiere permiso \" Enlazar Enlaces \"",
                RU = "Требуются права \" Embed Links \"",
                PO = "O BOT precisa da permissão \" Inserir Links \"",
                GR = "Bot benötigt die Erlaubnis \" Einbetten von Links \"",
                DU = "Bot heeft permissions nodig \" Embed Links \"",
                IT = "Il bot richiede dei permessi \" Embed Links \""
            };

            public TString Help_FooterHiddenCommands = new TString()
            {
                EN = "There are some hidden commands aswell ;)",
                FR = "Il y a aussi des commandes cachées ;)",
                SP = "Hay algunos comandos ocultos también ;)",
                RU = "Также здесь есть несколько секретных команд ;)",
                PO = "Também há alguns comandos ocultos ;)",
                GR = "Es gibt auch einige versteckte Befehle ;)",
                DU = "Er zijn ook een aantal verstopte commands ;)",
                IT = "Ci sono dei pure dei comandi segreti ;)"
            };

            public TString Commands = new TString()
            {
                EN = "Commands",
                FR = "Commandes",
                SP = "Comandos",
                RU = "Команды",
                PO = "Comandos",
                GR = "Befehle",
                DU = "Commands",
                IT = "Comandi"
            };

            public TString Links = new TString()
            {
                EN = "Links",
                FR = "Des liens",
                SP = "Campo de golf",
                RU = "Ссылки",
                PO = "Links",
                GR = "Links",
                DU = "Links",
                IT = "Links"
            };

            public TString MultiMC = new TString()
            {
                EN = "MultiMC allows you to manage and launch multiple versions with easy forge/mods installation",
                FR = "MultiMC vous permet de gérer et de lancer plusieurs versions avec une installation facile de forge / mods",
                SP = "MultiMC le permite administrar y lanzar varias versiones con una instalación fácil de forge / mods",
                RU = "MultiMC позволяет запускать любые версии игры, также можно легко установить Forge и моды",
                PO = "MultiMC permite administrar e lançar várias versões com instação fácil de Forge/Mods",
                GR = "MultiMC ermöglicht es Ihnen, mehrere Versionen mit einfacher Schmiede / Mods-Installation zu verwalten und zu starten",
                DU = "MultiMC Geeft jou de optie om meerdere versies te spelen met een forge/mods installatie",
                IT = "MultiMC ti permette di gestire e avviare più versioni di minecraft Con facile installazione di forge/mods"
            };

            public TString ColorCodes = new TString()
            {
                EN = "Minecraft Color Codes",
                FR = "Codes de couleur Minecraft",
                SP = "Códigos de color Minecraft",
                RU = "Коды цветов в Minecraft",
                PO = "Códigos das Cores do Minecraft",
                GR = "Minecraft Farbcodes",
                DU = "Minecraft Kleuren Codes",
                IT = "Codici dei colori di minecraft"
            };

            public TString Error_PlayerNotFound = new TString()
            {
                EN = "Player {0} not found",
                FR = "Joueur {0} pas trouvé",
                SP = "Jugador {0} extraviado",
                RU = "Игрок {0} не найден",
                PO = "Jogador {0} não encontrado",
                GR = "Spieler {0} nicht gefunden",
                DU = "Player {0} niet gevonden",
                IT = "Giocatore {0} Non trovato"
            };

            #region PingCommand
            public TString Error_EnterIP = new TString()
            {
                EN = "Enter an IP",
                FR = "Entrez une adresse IP",
                SP = "Introduzca un IP",
                RU = "Введите IP",
                PO = "Insere um IP",
                GR = "Geben Sie eine IP ein",
                DU = "Geef een IP op",
                IT = "Inserisci l' IP"
            };
            public TString Error_IPMain = new TString()
            {
                EN = "You really think that would work?",
                FR = "Vous pensez vraiment que cela fonctionnerait?",
                SP = "¿De verdad crees que funcionaría?",
                RU = "Ты думаешь, что это сработает?",
                PO = "Achas realmente que isso funcionaria?",
                GR = "Du denkst wirklich, das würde funktionieren?",
                DU = "Dacht je echt dat dat zou werken?",
                IT = "Pensi davvero che possa funzionare?"
            };
            public TString Error_IPRouter = new TString()
            {
                EN = "Minecraft servers don't run on routers DUH",
                FR = "Les serveurs Minecraft ne fonctionnent pas sur les routeurs DUH",
                SP = "Los servidores de Minecraft no funcionan con routers DUH",
                RU = "Сервера Minecraft не работают на DUH",
                PO = "Os Servidores de Minecraft não são executados em routers DUH!",
                GR = "Minecraft-Server laufen nicht auf Routern DUH",
                DU = "Minecraft servers runnen niet op routers",
                IT = "I server di minecraft non funzionano sui router"
            };
            public TString Error_IPZero = new TString()
            {
                EN = "Not enough zeros?",
                FR = "Pas assez de zéros?",
                SP = "¿No hay suficientes ceros?",
                RU = "Не хватает нулей?",
                PO = "Não há zeros suficientes?",
                GR = "Nicht genügend Nullen?",
                DU = "Niet genoeg nullen",
                IT = "Non ci sono abbastanza 0?"
            };
            public TString Error_IPGoogle = new TString()
            {
                EN = "This is for minecraft servers not google :(",
                FR = "Ceci est pour les serveurs minecraft pas Google :(",
                SP = "Esto es para servidores de minecraft no google :(",
                RU = "Это для серверов Minecraft, это не гугл :(",
                PO = "Isto é para Servidores de Minecraft, não para o Google :(",
                GR = "Dies ist für Minecraft Server nicht google :(",
                DU = "Dit is voor Minecraft servers, niet Google :(",
                IT = "Questo è per i server minecraft, non google :("
            };
            public TString Error_IPYoutube = new TString()
            {
                EN = "This is for minecraft servers not youtube :(",
                FR = "Ceci est pour les serveurs Minecraft, pas youtube :(",
                SP = "Esto es para servidores de minecraft no youtube :(",
                RU = "Это для серверов Minecraft, а не ютуб :(",
                PO = "Isto é para Servidores de Minecraft, não para o YouTube :(",
                GR = "Dies ist für Minecraft Server nicht youtube :(",
                DU = "Dit is voor Minecraft Servers, niet YouTube :(",
                IT = "Questo è per i server minecraft, non youtube :("
            };
            public TString Error_IPMyWeb = new TString()
            {
                EN = "Trying to ping my own website :D",
                FR = "Essayer de faire un ping sur mon propre site web :D",
                SP = "Tratando de hacer ping a mi propio sitio web: D",
                RU = "Попытаюсь пингануть мой собственный сайт :D",
                PO = "Tentando descobrir o Ping do meu WebSite :D",
                GR = "Ich versuche, meine eigene Website zu pingen: D",
                DU = "Ik probeer mijn eigen Website te pingen :D",
                IT = "Stai provando a pingare il mio sito :D"
            };
            public TString Error_IPBlocked = new TString()
            {
                EN = "Minecraft server has blocked the ping",
                FR = "Le serveur Minecraft a bloqué le ping",
                SP = "El servidor de Minecraft ha bloqueado el ping",
                RU = "Сервер заблокировал пинг",
                PO = "Este Servidor de Minecraft bloqueou o seu Ping",
                GR = "Minecraft-Server hat das Ping blockiert",
                DU = "Minecraft server heeft de ping geblokkeerd",
                IT = "Il server di minecraft ha bloccato il ping"
            };
            public TString Error_EnableQuery = new TString()
            {
                EN = "Minecraft server does not have enable-query set in server.properties",
                FR = "Le serveur Minecraft n'a pas activé la requête dans server.properties",
                SP = "El servidor de Minecraft no tiene enable-query establecido en server.properties",
                RU = "Этот сервер не имеет определённой отметки 'enable-query' в 'server.properties'",
                PO = "Este Servidor de Minecraft tem de ativar o enable-query em server.properties",
                GR = "Minecraft-Server hat keine Freigabe-Abfrage in server.properties gesetzt",
                DU = "Minecraft server heeft geen enable-query aangezet in server.properties",
                IT = "Il server minecraft non ha abilita-query settata nel server.properties"
            };
            public TString Error_IPInvalid = new TString()
            {
                EN = "This is not a valid ip",
                FR = "Ce n'est pas un ip valide",
                SP = "Esto no es un IP válido",
                RU = "Неверно введён IP",
                PO = "Este IP é inválido",
                GR = "Dies ist kein gültiges ip",
                DU = "Dat is geen juist IP",
                IT = "Questo non è un IP valido"
            };
            public TString Error_Cooldown = new TString()
            {
                EN = "You are on cooldown for 1 mins!",
                FR = "Vous êtes en pause pendant 1 minutes!",
                SP = "Estás en cooldown por 1 minutos!",
                RU = "Вы на кулдауне на 1 минуту!",
                PO = "Estás em cooldown por 1 minuto!",
                GR = "Sie sind auf Abklingzeit für 1 Minute!",
                DU = "Wacht op de cooldown van 1 minuut",
                IT = "Tu sei in cooldown per 1 minuto!"
            };
            public TString Ping_PleaseWait = new TString()
            {
                EN = "Please wait while I ping",
                FR = "Patientez pendant que je ping",
                SP = "Por favor, espere mientras hago ping",
                RU = "Подождите, пожалуйста...",
                PO = "Espera até eu conseguir o Ping",
                GR = "Bitte warten Sie, während ich ping",
                DU = "Wacht even terwijl er wordt gepingt",
                IT = "Perfavore aspetta mentre sto pingando"
            };
            public TString Ping_ServerLoading = new TString()
            {
                EN = "Server is loading!",
                FR = "Le serveur est en cours de chargement!",
                SP = "¡El servidor está cargando!",
                RU = "Сервер загружается!",
                PO = "O Servidor está a carregar!",
                GR = "Server wird geladen!",
                DU = "Server is aan het laden!",
                IT = "Il server sta caricando!"
            };
            public TString Players = new TString()
            {
                EN = "Players",
                FR = "Joueurs",
                SP = "Jugadores",
                RU = "Игроки",
                PO = "Jogadores",
                GR = "Spieler",
                DU = "Spelers",
                IT = "Giocatori"
            };
            public TString Ping_ServerOffline = new TString()
            {
                EN = "Server is offline",
                FR = "Le serveur est hors-ligne",
                SP = "Server está desconectado",
                RU = "Сервер оффлайн",
                PO = "O Servidor está Offline",
                GR = "Server ist offline",
                DU = "Server is offline",
                IT = "Il server è offline"
            };
            #endregion

            public TString List_NoServers = new TString()
            {
                EN = "This community has no servers listed",
                FR = "Cette communauté n'a aucun serveur répertorié",
                SP = "Esta comunidad no tiene servidores",
                RU = "Это сообщество не имеет серверов",
                PO = "Esta Comunidade não tem Servidores listados",
                GR = "Diese Community hat keine Server aufgeführt",
                DU = "Deze community heeft geen opgeslagen servers",
                IT = "Questa community non ha un server"
            };
            public TString List_GuildAdmin = new TString()
            {
                EN = "Community admin should use",
                FR = "L'administrateur communautaire devrait utiliser",
                SP = "El administrador de la comunidad debería usar",
                RU = "Администратор сообщества должен использовать",
                PO = "O administrador da comunidade deve usar",
                GR = "Community-Admin sollte verwenden",
                DU = "Dit kan alleen een Admin",
                IT = "Dovrebbe usarlo l' admin della community"
            };
            public TString Servers = new TString()
            {
                EN = "Servers",
                FR = "Les serveurs",
                SP = "Servidores",
                RU = "Сервера",
                PO = "Servidores",
                GR = "Servers",
                DU = "Servers",
                IT = "Servers"
            };

            #region InfoCommand
            public TString Info_MCSales = new TString()
            {
                EN = "Minecraft Account Sales",
                FR = "Ventes de compte Minecraft",
                SP = "Ventas de la cuenta de Minecraft",
                RU = "Продажи Minecraft",
                PO = "Vendas de Contas de Minecraft",
                GR = "Minecraft Account Sales",
                DU = "Minecraft Account Sales",
                IT = "Saldi dell' account minecraft"
            };
            public TString Info_MCSalesUrl = new TString()
            {
                EN = "https://minecraft.net/en-us/stats/",
                FR = "https://minecraft.net/fr-fr/stats/",
                SP = "https://minecraft.net/es-es/stats/",
                RU = "https://minecraft.net/ru-ru/stats/",
                PO = "https://minecraft.net/pt-pt/",
                GR = "https://minecraft.net/en-us/stats/",
                DU = "https://minecraft.net/en-us/stats/",
                IT = "https://minecraft.net/it-it/stats/"
            };
            public TString Info_SalesError = new TString()
            {
                EN = "Stats may be slightly off due to caching",
                FR = "Les statistiques peuvent être légèrement désactivées en raison de la mise en cache",
                SP = "Estadísticas pueden estar ligeramente fuera debido al almacenamiento en caché",
                RU = "Статистика может быть немного неверной",
                PO = "Vendas de Contas de Minecraft",
                GR = "Stats können aufgrund des Cachings leicht ausgeschaltet werden",
                DU = "Stats kunnen niet compleet zijn door caching",
                IT = "Le statistiche possono essere sbagliate a causa della memorizzazione nella cache"
            };
            public TString Error_Api = new TString()
            {
                EN = "API Error",
                FR = "Erreur API",
                SP = "Error de API",
                RU = "Ошибка API",
                PO = "API Error",
                GR = "API-Fehler",
                DU = "API Error",
                IT = "Errore dell' API"
            };
            #endregion

            #region SkinCommand
            public TString Skin_Args = new TString()
            {
                EN = "(Player) | head | cube | full | steal",
                FR = "(Joueur) | head | cube | full | steal",
                SP = "(Jugador) | cabeza | cubo | completo | robar",
                RU = "(игрок) | голова | куб | полный | украсть",
                PO = "(Jogador) | cabeça | cubo | cheio | roubar",
                GR = "(Spieler) | Kopf | Würfel | voll | stehlen",
                DU = "(Player) | head | cube | full | steal",
                IT = "(Giocatore) | testa | cubo | pieno | ruba"
            };
            public TString Skin_Stole = new TString()
            {
                EN = "Stole a skin",
                FR = "Roule une skin",
                SP = "Robó una piel",
                RU = "Украден скин",
                PO = "Roubar uma Skin",
                GR = "Stola eine Haut",
                DU = "Steel een skin",
                IT = "Ruba una skin"
            };
            public TString Error_UnknownArg = new TString()
            {
                EN = "Unknown argument do",
                FR = "Argument inconnu",
                SP = "Argumento desconocido",
                RU = "Неизвестный аргумент",
                PO = "Argumento desconhecido",
                GR = "Unbekanntes Argument",
                DU = "Onbekend Argument",
                IT = "argomento sconosciuto"
            };
            #endregion

            #region NameCommand
            public TString Player = new TString()
            {
                EN = "Player",
                FR = "Joueur",
                SP = "Jugador",
                RU = "Игрок",
                PO = "Jogador",
                GR = "Spieler",
                DU = "Player",
                IT = "Giocatore"
            };
            public TString Name_OneOnly = new TString()
            {
                EN = "Player {0} only has 1 name on records",
                FR = "Le joueur {0} n'a qu'un nom sur les enregistrements",
                SP = "El jugador {0} sólo tiene 1 nombre en los registros",
                RU = "Игрок {0} имеет только 1 имя в записях",
                PO = "O jogador {0} possui apenas 1 nome nos registros",
                GR = "Spieler {0} hat nur 1 Namen auf Datensätzen",
                DU = "Player {0} heeft maar 1 name",
                IT = "Giocatore {0} ha un solo nome registrato"
            };
            public TString First = new TString()
            {
                EN = "First",
                FR = "Premier",
                SP = "primero",
                RU = "Первый",
                PO = "Primeiro",
                GR = "Zuerst",
                DU = "First",
                IT = "Primo"
            };
            public TString Name_PlayerNotFoundNames = new TString()
            {
                EN = "Player {0} not found, please use the current players name",
                FR = "Joueur {0} pas trouvé, utilisez le nom actuel des joueurs",
                SP = "Jugador {0} no encontrado, por favor use el nombre actual de los jugadores",
                RU = "Игрок {0} не найден, пожалуйста, используйте имя реального игрока",
                PO = "Jogador {0} não encontrado, usa um nickname existente",
                GR = "Spieler {0} nicht gefunden, bitte benutze den aktuellen Spielernamen",
                DU = "Player {0} niet gevonden, gebruik de naam die de speler op dit moment heeft",
                IT = "Giocatore {0} non trovato, perfavore usa il nome corrente del giocatore"
            };
            #endregion

            #region StatusCommand
            public TString Status_Mojang = new TString()
            {
                EN = "Mojang Status",
                FR = "État des serveurs Mojang",
                SP = "Estado de Mojang",
                RU = "Статус Mojang",
                PO = "Status de Mojangs",
                GR = "Mojang Status",
                DU = "Mojang Status",
                IT = "Stato della Mojang"
            };
            public TString Status_MojangAccounts = new TString()
            {
                EN = "Mojang accounts",
                FR = "Comptes Mojang",
                SP = "Cuentas de Mojang",
                RU = "Аккаунты Mojang",
                PO = "Contas de Mojang",
                GR = "Mojang-Konten",
                DU = "Mojang accounts",
                IT = "Account della Mojang"
            };
            public TString Status_MojangAuthServers = new TString()
            {
                EN = "Mojang auth servers",
                FR = "Serveurs d'authentification Mojang",
                SP = "Mojang servidores de autenticación",
                RU = "Сервера авторизации Mojang",
                PO = "Servidores de autenticação de Mojang",
                GR = "Mojang Auth Server",
                DU = "Mojang auth servers",
                IT = "Mojang Servers automatici"
            };
            public TString Status_MojangAuthService = new TString()
            {
                EN = "Mojang auth service",
                FR = "Service d'authentification Mojang",
                SP = "Servicio Mojang auth",
                RU = "Сервис авторизации Mojang",
                PO = "Serviço de autenticação de Mojang",
                GR = "Mojang auth Service",
                DU = "Mojang auth service",
                IT = "Mojang servizio automatico"
            };
            public TString Status_MojangSessions = new TString()
            {
                EN = "Mojang sessions",
                FR = "Séances de Mojang",
                SP = "Sesiones de Mojang",
                RU = "Сессии Mojang",
                PO = "Sessões de Mojang",
                GR = "Mojang-Sessions",
                DU = "Mojang sessions",
                IT = "Sessione della Mojang"
            };
            public TString Status_MinecraftSessions = new TString()
            {
                EN = "Minecraft.net sessions",
                FR = "Séances de Minecraft.net",
                SP = "Sesiones de Minecraft.net",
                RU = "Сессии Minecraft.net",
                PO = "Sessões do Minecraft.net",
                GR = "Minecraft.net Sessions",
                DU = "Minecraft.net sessions",
                IT = "Sessioni di minecraft.net"
            };
            public TString Status_MinecraftSkins = new TString()
            {
                EN = "Minecraft.net skins",
                FR = "Peaux Minecraft.net",
                SP = "Pieles de Minecraft.net",
                RU = "Скины Minecraft.net",
                PO = "Minecraft.net skins",
                GR = "Minecraft.net Skins",
                DU = "Minecraft.net skins",
                IT = "Skins di minecraft.net"
            };
            public TString Status_MinecraftTextures = new TString()
            {
                EN = "Minecraft.net textures",
                FR = "Les textures de Minecraft.net",
                SP = "Minecraft.net texturas",
                RU = "Текстуры Minecraft.net",
                PO = "Texturas Minecraft.net",
                GR = "Minecraft.net Texturen",
                DU = "Minecraft.net textures",
                IT = "Textures di minecraf.net"
            };
            #endregion

            public TString PeoplePlayingMinecraft = new TString()
            {
                EN = "People playing Minecraft [Discord]",
                FR = "Les gens qui jouent à Minecraft [Discord]",
                SP = "Personas jugando Minecraft [Discord]",
                RU = "Игроки в Minecraft [Discord]",
                PO = "Pessoas a Jogar Minecraft [Discord]",
                GR = "Leute spielen Minecraft [Discord]",
                DU = "Members die Minecraft spelen [Discord]",
                IT = "Persone che stanno giocando a minecraft [Discord]"
            };
            public TString Text = new TString()
            {
                EN = "Text",
                FR = "Texte",
                SP = "Texto",
                RU = "Текст",
                PO = "Texto",
                GR = "Text",
                DU = "Text",
                IT = "Scritta"
            };
            public TString Hi = new TString()
            {
                EN = "Hi",
                FR = "salut",
                SP = "Hola",
                RU = "Привет",
                PO = "Olá",
                GR = "Hallo",
                DU = "Hi",
                IT = "Ciao"
            };
            public TString Get_ErrorLimit = new TString()
            {
                EN = "Text cannot be more than 22 letters/numbers",
                FR = "Le texte ne peut pas dépasser 22 lettres/nombres",
                SP = "El texto no puede tener más de 22 letras / números",
                RU = "Текст не может содержать более 22 символов",
                PO = "O texto não pode ter mais que 22 caracteres",
                GR = "Text kann nicht mehr als 22 Buchstaben/Zahlen sein",
                DU = "De tekst mag niet meer dan 22 letters/nummers bevatten",
                IT = "Le scritte non posso avere più di 22 caratteri/numberi"
            };
            public TString OnlineSkinEditor = new TString()
            {
                EN = "Online Skin Editor",
                FR = "Éditeur de peau en ligne",
                SP = "Online Skin Editor",
                RU = "Онлайн редактор скинов",
                PO = "Editor de pele on-line",
                GR = "Online-Skin-Editor",
                DU = "Online Skin Editor",
                IT = "Editore di skin Online"
            };

            #region BotCommand
            public TString Bot_Desc = new TString()
            {
                EN = "If you have any issue, suggestions or language translations please contact me",
                FR = "Si vous avez un problème, des suggestions ou des traductions linguistiques, contactez-moi",
                SP = "Si tiene algún problema, sugerencias o traducciones de idiomas, póngase en contacto conmigo",
                RU = "Если вы нашли неполадки или у вас есть предложения, обращайтесь ко мне",
                PO = "Se tiveres algum problema, sugestão ou tradução, contacta-me",
                GR = "Wenn Sie irgendwelche Fragen, Vorschläge oder Sprachübersetzungen haben, kontaktieren Sie mich bitte",
                DU = "Als je problemen hebt, suggesties of kunt vertalen, neem dan contact met me op",
                IT = "Se tu hai qualche problema, suggerimenti o traduzioni perfavore contattami"
            };
            public TString Bot_Footer = new TString()
            {
                EN = "Bot owner commands do not count to the command count above also some of them are secret ones you have to find",
                FR = "Les commandes du propriétaire du robot ne comptent pas sur le compte de commande ci-dessus, mais certains d'entre eux sont secrets que vous devez trouver",
                SP = "Bot comandos de propietario no cuentan para el comando de contar por encima también algunos de ellos son secretos que tiene que encontrar",
                RU = "Команды владельца бота не отображены сверху, поэтому вы должны найти их сами",
                PO = "Os comandos do proprietário do botão não contam para a contagem de comandos acima, também alguns deles são secretos que você precisa encontrar",
                GR = "Bot Besitzer Befehle zählen nicht auf die Befehlszählung oben auch einige von ihnen sind geheimen, die Sie zu finden haben",
                DU = "Bot eigenaar commands tellen niet bij de normale commands, er zijn ook geheime commands die je kan vinden",
                IT = "I comandi dell' owner non entrano le conto dei comandi, ci sono anche comandi segreti"
            };
            public TString Bot_Owner = new TString()
            {
                EN = "Bot Owner",
                FR = "Propriétaire du robot",
                SP = "Bot Owner",
                RU = "Владелец бота",
                PO = "Bot Owner",
                GR = "Bot Besitzer",
                DU = "Bot Owner",
                IT = "Propietario del bot"
            };
            public TString Language = new TString()
            {
                EN = "Language",
                FR = "La langue",
                SP = "Idioma",
                RU = "Язык",
                PO = "Língua",
                GR = "Sprache",
                DU = "Language",
                IT = "Linguaggio"
            };
            public TString Bot_Lib = new TString()
            {
                EN = "Library",
                FR = "Bibliothèque",
                SP = "Biblioteca",
                RU = "Библиотека",
                PO = "Biblioteca",
                GR = "Bibliothek",
                DU = "Library",
                IT = "Libreria"
            };
            public TString Stats = new TString()
            {
                EN = "Stats",
                FR = "Stats",
                SP = "Estadísticas",
                RU = "Статистика",
                PO = "Status",
                GR = "Stats",
                DU = "Stats",
                IT = "Statistiche"
            };
            public TString Guilds = new TString()
            {
                EN = "Guilds",
                FR = "Guildes",
                SP = "Gremios",
                RU = "Сервера",
                PO = "Servidores",
                GR = "Gilden",
                DU = "Guilds",
                IT = "Gilde"
            };

            public TString Uptime = new TString()
            {
                EN = "Uptime",
                FR = "Temps de disponibilité",
                SP = "Tiempo de actividad",
                RU = "Время после последнего перезапуска",
                PO = "Tempo de atividade",
                GR = "Betriebszeit",
                DU = "Uptime",
                IT = "Uptime"
            };

            public TString Bot_Invite = new TString()
            {
                EN = "Bot Invite",
                FR = "Invité Bot",
                SP = "Bot Invita",
                RU = "Ссылка на бота",
                PO = "Convidar o BOT",
                GR = "Bot einladen",
                DU = "Bot Invite",
                IT = "Invito del bot"
            };

            public TString Bot_ListGuilds = new TString()
            {
                EN = "Bot List Guilds",
                FR = "Liste alphabétique",
                SP = "Bot List Guilds",
                RU = "Список серверов",
                PO = "Lista de BOTs",
                GR = "Bot-Liste Gilden",
                DU = "Bot List Guilds",
                IT = "Lista delle gilde del bot"
            };
            #endregion

            public TString Info = new TString()
            {
                EN = "Info",
                FR = "Info",
                SP = "Información",
                RU = "Информация",
                PO = "Info",
                GR = "Info",
                DU = "Info",
                IT = "Info"
            };
            public TString Unknown = new TString()
            {
                EN = "Unknown",
                FR = "Inconnu",
                SP = "Desconocido",
                RU = "неизвестный",
                PO = "Desconhecido",
                GR = "Unbekannt",
                DU = "Unknown",
                IT = "Sconosciuto"
            };
            public TString Health = new TString()
            {
                EN = "Health",
                FR = "Santé",
                SP = "Salud",
                RU = "Здоровье",
                PO = "Vida",
                GR = "Gesundheit",
                DU = "Health",
                IT = "Vita"
            };
            public TString Height = new TString()
            {
                EN = "Height",
                FR = "la taille",
                SP = "Altura",
                RU = "Высота",
                PO = "Altura",
                GR = "Höhe",
                DU = "Height",
                IT = "Altezza"
            };
            public TString Width = new TString()
            {
                EN = "Width",
                FR = "Largeur",
                SP = "Anchura",
                RU = "Ширина",
                PO = "Largura",
                GR = "Breite",
                DU = "Width",
                IT = "Peso"
            };
            public TString Type = new TString()
            {
                EN = "Type",
                FR = "Type",
                SP = "Tipo",
                RU = "Тип",
                PO = "Tipo",
                GR = "Art",
                DU = "Type",
                IT = "Tipo"
            };
            public TString Version = new TString()
            {
                EN = "Version",
                FR = "Version",
                SP = "Versión",
                RU = "Версия",
                PO = "Versão",
                GR = "Version",
                DU = "Version",
                IT = "Versione"
            };
            public TString Attack = new TString()
            {
                EN = "Attack",
                FR = "Attaque",
                SP = "Ataque",
                RU = "Атака",
                PO = "Atacar",
                GR = "Attacke",
                DU = "Attack",
                IT = "Attacco"
            };
            public TString Easy = new TString()
            {
                EN = "Easy",
                FR = "Facile",
                SP = "Fácil",
                RU = "Легко",
                PO = "Fácil",
                GR = "Einfach",
                DU = "Easy",
                IT = "Facile"
            };
            public TString Normal = new TString()
            {
                EN = "Normal",
                FR = "Normal",
                SP = "Normal",
                RU = "Нормальный",
                PO = "Normal",
                GR = "Normal",
                DU = "Normal",
                IT = "Normale"
            };
            public TString Hard = new TString()
            {
                EN = "Hard",
                FR = "Difficile",
                SP = "Difícil",
                RU = "Жесткий",
                PO = "Díficil",
                GR = "Hart",
                DU = "Hard",
                IT = "Difficile"
            };
        }
       public class Wiki
        {
            public TString Error_UnknownItemID = new TString()
            {
                EN = "Cannot find item name or ID",
                FR = "Impossible de trouver le nom de l'élément ou l'ID",
                SP = "No se puede encontrar el nombre o el ID del elemento",
                RU = "Не удается найти имя или идентификатор элемента",
                PO = "Não foi possível encontrar o nome do item ou ID",
                GR = "Artikelname oder ID kann nicht gefunden werden",
                DU = "Geen item gevonden met die naam/ID",
                IT = "Non posso trovare il nome dell' item o l' ID"
            };
            public TString Error_UnknownMob = new TString()
            {
                EN = "Cannot find mob name",
                FR = "Vous ne pouvez pas trouver le nom de la foule",
                SP = "No se puede encontrar el nombre de la mafia",
                RU = "Не удалось найти имя моба",
                PO = "Não foi possível encontrar o nome do mob",
                GR = "Ich kann keinen Namen finden",
                DU = "Geen mob gevonden met de opgegeven naam",
                IT = "Non posso trovare il nome del mob"
            };
            public TString blocks = new TString()
            {
                EN = "blocks",
                FR = "blocs",
                SP = "bloques",
                RU = "блоки",
                PO = "blocos",
                GR = "Blöcke",
                DU = "blocks",
                IT = "blocchi"
            };
            public TString Fist_Attack = new TString()
            {
                EN = "Fist Attack",
                FR = "Attaque de poing",
                SP = "Ataque de puño",
                RU = "Кулачная атака",
                PO = "Primeiro Ataque",
                GR = "Faustangriff",
                DU = "Fist Attack",
                IT = "Attaco a pugni"
            };
        }
        public class Hidden
        {
            public TString Wallpaper = new TString()
            {
                EN = "Wallpaper",
                FR = "Fond d'écran",
                SP = "Papel pintado",
                RU = "Фон",
                PO = "Wallpaper",
                GR = "Tapete",
                DU = "Wallpaper",
                IT = "Carta da parati"
            };
            public TString ForgecraftWiki = new TString()
            {
                EN = "Wiki And Forgecraft Users",
                FR = "Utilisateurs Wiki et Forgecraft",
                SP = "Usuarios de Wiki y Forgecraft",
                RU = "Вики и пользователи Forgecraft",
                PO = "Membros da Wiki e Forgecraft",
                GR = "Wiki und Forgecraft Benutzer",
                DU = "Wiki And Forgecraft Users",
                IT = "Wiki e Forgecraft utenti"
            };
            public TString BukkitNews = new TString()
            {
                EN = "Bukkit News",
                FR = "Nouvelles de Bukkit",
                SP = "Noticias de Bukkit",
                RU = "Новости о Bukkit",
                PO = "Bukkit News",
                GR = "Bukkit Nachrichten",
                DU = "Bukkit News",
                IT = "Bukkit News"
            };
            public TString FoundSecretCommand = new TString()
            {
                EN = "Hey, you found a secret command :D",
                FR = "Hé, vous avez trouvé une commande secrète :D",
                SP = "Hola, has encontrado un comando secreto :D",
                RU = "Эй, ты нашёл секретную команду :D",
                PO = "Hey, encontraste um comando secreto :D",
                GR = "Hey, du hast einen geheimen Befehl gefunden :D",
                DU = "Hey! Je hebt een geheime command gevonden! :D",
                IT = "hey, Hai trovato un comando segreto"
            };
            public TString MinecraftClassic = new TString()
            {
                EN = "Minecraft classic was the second phase of developent in 2009 that allowed players to play in the browser using java on the minecraft.net website which was primarly used to build things",
                FR = "Minecraft classic a été la deuxième phase de développement en 2009 qui a permis aux joueurs de jouer dans le navigateur en utilisant java sur le site web minecraft.net qui a été principalement utilisé pour construire des choses",
                SP = "Minecraft clásico fue la segunda fase de desarrollo en 2009 que permitió a los jugadores jugar en el navegador usando java en el sitio web minecraft.net que fue utilizado principalmente para construir cosas",
                RU = "Minecraft Classic был второй фазой разработки игры в 2009, которая позволила играть в браузере, используя Java, на сайте minecraft.net",
                PO = "O Minecraft classic foi a segunda fase do desenvolvimento em 2009, que permitiu que os jogadores jogassem no navegador usando o java no site minecraft.net, que foi usado principalmente para construir coisas",
                GR = "Minecraft-Klassiker war die zweite Phase der Entwicklung im Jahr 2009, die es Spielern ermöglicht, im Browser mit Java auf der Minecraft.net-Website zu spielen, die primär verwendet wurde, um Dinge zu bauen",
                DU = "Minecraft Classic was de 2e fase van de ontwikkelaars in 2009, waardoor spelers in de browser konden spelen met behulp van Java op de minecraft.net site, die voornamelijk werd gebruikt om dingen te bouwen",
                IT = "Minecraft classic fu la 2° fase di sviluppo nel 2009 che ha permesso ai giocatori di giocare nel browser usando java su minecraft.net che fu usato inizialmente per costruire cose"
            };
            public TString Forgecraft = new TString()
            {
                EN = "Forgecraft is the set of private whitelisted servers for mod developers to gather and beta-test their mods in a private environment. Many YouTubers and live-streamers also gather on the server to interact with the mod developers, help play-test the mods, and create videos to let the general public know what the mod developers are doing.",
                FR = "Forgecraft est l'ensemble des serveurs privés de liste blanche pour les développeurs mod pour rassembler et bêta-tester leurs mods dans un environnement privé. De nombreux YouTubers et live-streamers se rassemblent également sur le serveur pour interagir avec les développeurs de mod, aident à tester les mods et à créer des vidéos pour permettre au grand public de savoir ce que font les développeurs de mod.",
                SP = "Forgecraft es el conjunto de servidores privados de listas blancas para que los desarrolladores de mod puedan recopilar y probar sus mods en un entorno privado. Muchos YouTubers y live-streamers también se reúnen en el servidor para interactuar con los desarrolladores de mod, ayudar a jugar a probar los mods y crear videos para que el público en general sepa lo que están haciendo los desarrolladores de mod.",
                RU = "Forgecraft - это набор приватных серверов для создателей модов, предназначенный для проверки модов. Ютуберы и стримеры также могут попасть на такие сервера и помочь разработчикам модов в тестировании, и показать другим игрокам, что создатели модов делают.",
                PO = "Forgecraft é o conjunto de servidores privados com whitelist para desenvolvedores de modificações para coletar e testar seus mods beta em um ambiente privado. Muitos YouTubers e live-streamers também se reúnem no servidor para interagir com os desenvolvedores de mod, ajudar a jogar-testar os mods e criar vídeos para que o público em geral saiba o que os desenvolvedores de modelos estão fazendo.",
                GR = "Forgecraft ist die Menge der privaten Whitelist-Server für Mod-Entwickler zu sammeln und beta-Test ihre Mods in einer privaten Umgebung. Viele YouTubers und Live-Streamer sammeln auch auf dem Server, um mit den Mod-Entwicklern zu interagieren, zu helfen, die Mods zu testen und Videos zu erstellen, um die allgemeine Öffentlichkeit zu informieren, was die Mod-Entwickler tun.",
                DU = "ForgeCraft zijn een aantal whitelisted servers speciaal voor developers om hun eigen mods en beta-tests uit te voeren. Veel YouTubers en live-streamers hadden ook toegang tot deze servers, om te overleggen met developpers, en ze te helpen met testen, en video's te maken om alle spelers op te hoogte te brengen van de nieuwe updates.",
                IT = "Forgecraft è il set di server whitelistati per i creatori delle mod e guadagnare soldi e beta-testers per le loro mod in un ambiente privato . Many YouTubers and live-streamers also gather on the server to interact with the mod developers, help play-test the mods, and create videos to let the general public know what the mod developers are doing."
            };
            public TString ForgecraftWallpaper = new TString()
            {
                EN = "Forgecraft Wallpaper",
                FR = "Fond d'écran Forgecraft",
                SP = "Fondo de Pantalla de Forgecraft",
                RU = "Фон Forgecraft",
                PO = "Forgecraft Wallpaper",
                GR = "Forgecraft Hintergrund",
                DU = "Forgecraft Wallpaper",
                IT = "Forgecraft Wallpaper"
            };
            public TString Bukkit = new TString()
            {
                EN = "RIP Bukkit you will be missed along with other server solutions...",
                FR = "RIP Bukkit vous manquera avec d'autres solutions serveur...",
                SP = "RIP Bukkit te faltará junto con otras soluciones de servidor ...",
                RU = "RIP Bukkit, нам тебя будет не хватать...",
                PO = "RIP Bukkit vais perder com outras soluções para os Servidores...",
                GR = "RIP Bukkit werden Sie mit anderen Serverlösungen verpasst ...",
                DU = "RIP Bukkit. Je wordt gemist.",
                IT = "RIP Bukkit tu verrai dimenticato insieme ad altre soluzioni per il server..."
            };
            public TString Direwolf20 = new TString()
            {
                EN = "Direwolf20 is a popular youtuber known for his lets plays and mod tutorials on modded minecraft. He also plays on a private server called Forgecraft with a bunch of mod developers and other youtubers with his friends Soaryn and Pahimar",
                FR = "Direwolf20 est un populaire youtuber connu pour ses jeux de joueurs et tutoriels mod sur minecraft modded. Il joue également sur un serveur privé appelé Forgecraft avec un tas de développeurs de mod et d'autres youtubers avec ses amis Soaryn et Pahimar",
                SP = "Direwolf20 es un popular youtuber conocido por su permite juegos y tutoriales de mod en minecraft modded. También juega en un servidor privado llamado Forgecraft con un montón de desarrolladores de mod y otros youtubers con sus amigos Soaryn y Pahimar",
                RU = "Direwolf20 - это популярный ютубер, который известен за его летс-плеи и туториалы по модам. Он также играет на приватном сервере Forgecraft, с несколькими разработчиками модов, с его друзьями Soaryn и Pahimar",
                PO = "Direwolf20 é um popular YouTuber conhecido por seus jogadinhos e tutoriais mod em Minecraft modificado. Ele também joga em um servidor privado chamado Forgecraft com um monte de desenvolvedores de mod e outros youtubers com seus amigos Soaryn e Pahimar",
                GR = "Direwolf20 ist ein populärer Youtuber, der für seine Letspiele und Mod Tutorials auf modded Minecraft bekannt ist. Er spielt auch auf einem privaten Server namens Forgecraft mit einem Haufen Mod-Entwickler und anderen Youtubers mit seinen Freunden Soaryn und Pahimar",
                DU = "Direwolf20 is een populaire YouTuber bekend om zijn lets plays en mod tutorials op modded Minecraft. Hij speelt ook op een private server genaamd ForgeCraft met een aantal mod developers en andere YouTubers met zijn vrienden Soaryn en Pahimar.",
                IT = "Direwolf20 è un youtuber popolare noto per i suoi giochi e tutorial mod sul mini mestiere modded. Gioca anche su un server privato chiamato Forgecraft con un gruppo di sviluppatori di mod e altri youtubers con i suoi amici Soaryn e Pahimar"
            };
            public TString Herobrine = new TString()
            {
                EN = "Always watching you...",
                FR = "Te regarde toujours...",
                SP = "Siempre observándote...",
                RU = "Всегда следим за тобой...",
                PO = "Sempre observando-te...",
                GR = "Immer beobachte dich...",
                DU = "Houd je altijd in de gaten...",
                IT = "Ti sto guardando..."
            };
            public TString Entity303 = new TString()
            {
                EN = "A minecraft creepy pasta of a former Mojang employee who was fired by Notch and now want revenge",
                FR = "Une creppy pasta de minecraft d'un ancien employé de Mojang qui a été renvoyé par Notch et veut maintenant se venger",
                SP = "Un minecraft espeluznante pasta de un ex empleado de Mojang que fue despedido por Notch y ahora quieren venganza",
                RU = "Крипипаста майнкрафта, которая была замечена Нотчем, но хочет мести",
                PO = "Um Minecraft macarrão assustador de um ex-funcionário de Mojang que foi demitido por Notch e agora quer vingança",
                GR = "Ein minecraft gruselige Nudeln eines ehemaligen Mojang-Angestellten, der von Notch gefeuert wurde und nun Rache will",
                DU = "Een Minecraft voormalig Mojang medewerker die door Notch werd ontslagen en nu wraak wilt nemen",
                IT = "Una creepy pasta del minecraft di un ex dipendente di Mojang che fu licenziato da Notch e ora vuole vendicarsi"
            };
            public TString Israphel = new TString()
            {
                EN = "The best youtube minecraft series that will never die in our hearts... 2010 - 2012 RIP Yogscast",
                FR = "La meilleure série de Youtube Minecraft qui ne mourra jamais dans nos coeurs ... 2010 - 2012 RIP Yogscast",
                SP = "La mejor serie de minecraft de youtube que nunca morirá en nuestros corazones ... 2010 - 2012 RIP Yogscast",
                RU = "Лучшие серии майнкрафта, которые никогда не умрут в наших сердцах... 2010 - 2012 RIP Yogscast",
                PO = "O melhor YouTuber de séries de Minecraft que nunca morrerá nos nossos corações... 2010 - 2012 RIP Yogscast",
                GR = "Die besten Youtube Minecraft Serie, die niemals in unseren Herzen sterben wird ... 2010 - 2012 RIP Yogscast",
                DU = "De beste YouTube Minecraft serie die we nooit zullen vergeten... 2010-2012 RIP Yogscast",
                IT = "La migliore serie di minecraft di YouTube che non morirà mai nei nostri cuori ... 2010 - 2012 RIP Yogscast"
            };
            public TString Notch = new TString()
            {
                EN = "Minecraft was created by Notch aka Markus Persson",
                FR = "Minecraft a été créé par Notch aka Markus Persson",
                SP = "Minecraft fue creado por Notch aka Markus Persson",
                RU = "Minecraft был создан Нотчем aka Маркусом Перссоном",
                PO = "O Minecraft foi criado pelo Notch aka Markus Persson",
                GR = "Minecraft wurde von Notch aka Markus Persson erstellt",
                DU = "Minecraft is gemaakt door Notch aka Markus Persson",
                IT = "Minecraft è stato creato da Notch alias Markus Persson"
            };
        }

        public class Admin
        {
            public HashSet<string[]> Commands = new HashSet<string[]>()
                {
                    // English
                    new string[]
                    {
                        "[ mc/lang ]( Set the community language )",
                        "[ mc/addserver ]( Add a MC server to this guild list )",
                        "[ mc/delserver ]( Remove a MC server from this guild list )",
                        "[ mc/setprefix ]( Set Custom Prefix )",
                        "[ mc/resetprefix ]( Remove Custom Prefix )"
                    },

                    // French
                    new string[]
                    {
                        "[ mc/lang ]( Définir la langue de la communauté )",
                        "[ mc/addserver ]( Ajouter un serveur MC à cette liste de guilde )",
                        "[ mc/delserver ]( Supprimez un serveur MC de cette liste de guilde )",
                        "[ mc/setprefix ]( Set Custom Prefix )",
                        "[ mc/resetprefix ]( Remove Custom Prefix )"

                    },

                    // Spanish
                    new string[]
                    {
                        "[ mc/lang ]( Définir la langue de la communauté )",
                        "[ mc/addserver ]( Ajouter un serveur MC à cette liste de guilde )",
                        "[ mc/delserver ]( Supprimez un serveur MC de cette liste de guilde )",
                        "[ mc/setprefix ]( Set Custom Prefix )",
                        "[ mc/resetprefix ]( Remove Custom Prefix )"
                    },

                    // Russian
                    new string[]
                    {
                        "[ mc/lang ]( Установить язык сервера )",
                        "[ mc/addserver ]( Добавить MC сервер в список серверов )",
                        "[ mc/delserver ]( Удалить MC сервер из списка)",
                        "[ mc/setprefix ]( Set Custom Prefix )",
                        "[ mc/resetprefix ]( Remove Custom Prefix )"
                    },

                    // Portugese
                    new string[]
                    {
                        "[ mc/lang ]( Muda a linguagem do BOT )",
                        "[ mc/addserver ]( Adicionar um Servidor de Minecraft á lista )",
                        "[ mc/delserver ]( Remove um Servidor de Minecraft da lista )",
                        "[ mc/setprefix ]( Set Custom Prefix )",
                        "[ mc/resetprefix ]( Remove Custom Prefix )"
                    },

                    // German
                    new string[]
                    {
                        "[ mc/lang ]( Stellen Sie die Gemeinschaftssprache eine )",
                        "[ mc/addserver ]( Füge dieser Gildenliste einen MC-Server hinzu )",
                        "[ mc/delserver ]( Entfernen Sie einen MC-Server aus dieser Gildenliste )",
                        "[ mc/setprefix ]( Set Custom Prefix )",
                        "[ mc/resetprefix ]( Remove Custom Prefix )"
                    },

                    // Dutch
                    new string[]
                    {
                        "[ mc/lang ]( Stel de taal in )",
                        "[ mc/addserver ]( Voegt een Minecraft server toe aan de lijst )",
                        "[ mc/delserver ]( Verwijderd een Minecraft server van de lijst )",
                        "[ mc/setprefix ]( Zet een eigen prefix )",
                        "[ mc/resetprefix ]( Verwijderd de eigen prefix )"
                    },

                    // Italian
                    new string[]
                    {
                        "[ mc/lang ]( Setta il linguaggio della community )",
                        "[ mc/addserver ]( Aggiungi un server MC a la lista di questa gildad )",
                        "[ mc/delserver ]( Rimuovi un server MC da questo gilda )",
                        "[ mc/setprefix ]( Setta il Custom Prefix )",
                        "[ mc/resetprefix ]( Rimuovi il Custom Prefix )"
                    }
                };
            public TString LanguageTranslate = new TString()
            {
                EN = "Want a language translates? Contact",
                FR = "Vous voulez une langue traduire? Contact",
                SP = "¿Quiere traducir un idioma? Contacto",
                RU = "Хотите перевести бота на другой язык? Соединитесь с нами",
                PO = "Pretendes uma tradução? Contacta",
                GR = "Wollen Sie eine Sprache übersetzen? Kontakt",
                DU = "Wil je een bepaalde taal vertalen? Neem contact met me op",
                IT = "Vuoi tradurre un linguaggio? Contattami"
            };
            public TString Error_NoData = new TString()
            {
                EN = "Could not find guild data contact xXBuilderBXx#9113",
                FR = "Impossible de trouver les informations de la guilde contact xXBuilderBXx#9113",
                SP = "No se pudieron encontrar los datos del clan xXBuilderBXx#9113",
                RU = "Не найден контакт с xXBuilderBXx#9113",
                PO = "Não foi possível encontrar o contacto para xXBuilderBXx#9113",
                GR = "Gildendaten konnte nicht gefunden werden Kontakt xXBuilderBXx#9113",
                DU = "Kon geen data vinden, neem contact op met xXBuilderBXx#9113",
                IT = "Non posso trovare i dati della gilda, contatta xXBuilderBXx#9113"
            };
            public TString AdminCommands = new TString()
            {
                EN = "Guild Admin Commands",
                FR = "Commandes d'administration de la guilde",
                SP = "Comandos de Guild Admin",
                RU = "Список админских команд",
                PO = "Comandos Administrativos",
                GR = "Gilden-Admin-Befehle",
                DU = "Guild Admin Commands",
                IT = "Comandi dell' admin della gilda"
            };
            public TString AdminOnly = new TString()
            {
                EN = "You are not a guild admin",
                FR = "Vous n'êtes pas un administrateur",
                SP = "No eres un administrador del gremio",
                RU = "Вы не администратор",
                PO = "Não és um admin da Comunidade",
                GR = "Du bist keine Gilde admin",
                DU = "Je bent geen Admin",
                IT = "Tu non sei l' admin della gilda"
            };
            public TString UseList = new TString()
            {
                EN = "Use mc/list for a list of this guilds minecraft servers",
                FR = "Utilisez mc/list pour une liste de ces serveurs minecraft de guilde",
                SP = "Utilice mc/list para obtener una lista de estos servidores de los minecraft de los gremios",
                RU = "Используйте mc/list для списка серверов в майнкрафте для данной гильдии",
                PO = "Usa mc/list para veres a lista de Servidores de Minecraft",
                GR = "Verwenden Sie mc / list für eine Liste dieser Gilden Minecraft Server",
                DU = "Gebruik mc/list om de opgeslagen servers te bekijken",
                IT = "usa mc/list per una lista dei server MC di questa gilda"
            };
            public TString AddServer = new TString()
            {
                EN = "Enter a tag, ip and name",
                FR = "Entrez une étiquette, un ip et un nom",
                SP = "Introduzca una etiqueta, ip y nombre",
                RU = "Введите тэг, IP и название",
                PO = "Coloca uma tag, ip e nome",
                GR = "Geben Sie ein Tag, ip und name ein",
                DU = "Geeft een Tag, IP en naam op",
                IT = "Inserisci la tag, IP e nome"
            };
            public TString AddServer_Already = new TString()
            {
                EN = "This server is already on the list",
                FR = "Ce serveur est déjà sur la liste",
                SP = "Este servidor ya está en la lista",
                RU = "Этот сервер уже добавлен",
                PO = "Este Servidor já está na lista",
                GR = "Dieser Server befindet sich bereits auf der Liste",
                DU = "Deze server staat al op de lijst",
                IT = "Questo server è già nella lista"
            };
            public TString AddServer_Added = new TString()
            {
                EN = "Added server {0} to the guild list",
                FR = "Serveur ajouté {0} à la liste de guilde",
                SP = "Añadido servidor {0} a la lista de los gremios",
                RU = "Добавлен сервер {0} в список серверов",
                PO = "Servidor adicionado {0} á lista de Servidores",
                GR = "Server hinzugefügt {0} zur Gildenliste",
                DU = "server {0} toegevoegd aan de lijst",
                IT = "Server aggiunto {0} alla lista della gilda"
            };
            
            public TString DelServer_Enter = new TString()
            {
                EN = "Delete a server with",
                FR = "Supprimer un serveur avec",
                SP = "Eliminar un servidor con",
                RU = "Удалить сервер с",
                PO = "Eliminar um Servidor com",
                GR = "Löschen Sie einen Server mit",
                DU = "Verwijder een server",
                IT = "Elimina un server con"
            };
            public TString DelServer_None = new TString()
            {
                EN = "This server is not on the list",
                FR = "Ce serveur n'est pas sur la liste",
                SP = "Este servidor no está en la lista",
                RU = "Сервера нет в списке",
                PO = "Este Servidor não está na lista",
                GR = "Dieser Server ist nicht auf der Liste",
                DU = "Deze server staat niet op de lijst",
                IT = "Questo server non è sulla lista"
            };
            public TString DelServer_Deleted = new TString()
            {
                EN = "Removed server {0} from the guild list",
                FR = "Serveur supprimé {0} de la liste de guilde",
                SP = "Servidor eliminado {0} de la lista de gremios",
                RU = "Удалён сервер {0} из списка серверов данной гильдии",
                PO = "Servidor removido {0} da lista de Servidores",
                GR = "Entfernter Server {0} aus der Gildenliste",
                DU = "Server {0} is verwijderd van de lijst",
                IT = "Server rimosso {0} dalla lista della gilda"
            };
            public TString ChangeLang = new TString()
            {
                EN = "Change Community Language",
                FR = "Changer la langue de la communauté",
                SP = "Cambiar idioma de la comunidad",
                RU = "Изменить язык сервера",
                PO = "Mudar a Linguagem do BOT",
                GR = "Sprache ändern",
                DU = "Verander de taal van de bot",
                IT = "Cambia il linguaggio della communuty"
            };
        }
    }
}
