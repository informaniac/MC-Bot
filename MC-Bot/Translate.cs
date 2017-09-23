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
        ///  LEAVE THIS ALONE
        /// </summary>
        public class TString
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
            /// Get the language translation
            /// </summary>
            /// <param name="Guild"></param>
            /// <returns></returns>
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
                        "[ mc/skin (Player) ]( Player skin )",
                        "[ mc/names (Player) ]( MC account name history )",
                        "[ mc/status ]( Mojang status )",
                        "[ mc/get (Text) ]( Get an achievement )",
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
                        "[ mc/skin (Joueur) ]( Skin du joueur )",
                        "[ mc/names (Joueur) ]( Historique des nom de compte MC )",
                        "[ mc/status ]( Status des serveur de Mojange )",
                        "[ mc/get (Texte) ]( Obtenir une réussite )",
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
                        "[ mc/uuid (Jugador) ]( Jugador UUID )",
                        "[ mc/ping (IP) ]( Hacer ping a un servidor )",
                        "[ mc/list ]( Lista de servidores de MC del clan )",
                        "[ mc/wiki ]( Wiki de Artículos/Mobs/Enchants/Pociones )",
                        "[ mc/skin (Jugador) ]( Piel del jugador )",
                        "[ mc/names (Jugador) ]( Historia del nombre de cuenta de MC )",
                        "[ mc/status ]( Estado de Mojang )",
                        "[ mc/get (Texto) ]( Obtener un logro )",
                        "[ mc/minime (Jugador) ]( Minify la piel del reproductor )",
                        "[ mc/playing ]( Les gens qui jouent à Minecraft )",
                        "[ mc/admin ]( Guild comandos de administración )",
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
                        "[ mc/list ]( Список серверов )",
                        "[ mc/wiki ]( Вики: информация о мобах/предметах/зачарованиях и т.д. )",
                        "[ mc/skin (игрок) ]( Скин игрока )",
                        "[ mc/names (игрок) ]( История имён аккаунта )",
                        "[ mc/status ]( Статус Mojang )",
                        "[ mc/get (текст) ]( Генератор достижений )",
                        "[ mc/minime (игрок) ]( Минимизировать скин игрока )",
                        "[ mc/playing ]( Сколько людей играют в Minecraft на данный момент )",
                        "[ mc/admin ]( Команды администраторов сервера )",
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
                        "[ mc/list ]( Lista de Servidores )",
                        "[ mc/wiki ]( Wiki de Items/Mobs/Enchants/Potions )",
                        "[ mc/skin (Jogador) ]( Skin do Jogador )",
                        "[ mc/names (Jogador) ]( Histórico de Nomes MC )",
                        "[ mc/status ]( Mojang Status )",
                        "[ mc/get (Texto) ]( Obter uma Conquista )",
                        "[ mc/minime (Jogador) ]( MiniEu de uma Skin )",
                        "[ mc/playing ]( Pessoas a Jogar Minecraft )",
                        "[ mc/admin ]( Comandos Administrativos )",
                        "[ mc/invite ]( Convite do BOT )"
                    }

            };

            public TString Error_NoEmbedPerms = new TString()
            {
                EN = "Bot requires permission \" Embed Links \"",
                FR = "Bot nécessite l'autorisatio \" Liens incrémentés \"",
                SP = "Bot requiere permiso \" Enlazar Enlaces \"",
                RU = "Требуются права \" Embed Links \"",
                PO = "O BOT precisa da permissão \" Inserir Links \""
            };

            public TString Help_FooterHiddenCommands = new TString()
            {
                EN = "There are some hidden commands aswell ;)",
                FR = "Il y a aussi des commandes cachées ;)",
                SP = "Hay algunos comandos ocultos también ;)",
                RU = "Также здесь есть несколько секретных команд ;)",
                PO = "Também há alguns comandos ocultos ;)"
            };

            public TString Commands = new TString()
            {
                EN = "Commands",
                FR = "Commandes",
                SP = "Comandos",
                RU = "Команды",
                PO = "Comandos"
            };

            public TString Links = new TString()
            {
                EN = "Links",
                FR = "Des liens",
                SP = "Campo de golf",
                RU = "Ссылки",
                PO = "Links"
            };

            public TString MultiMC = new TString()
            {
                EN = "MultiMC allows you to manage and launch multiple versions with easy forge/mods installation",
                FR = "MultiMC vous permet de gérer et de lancer plusieurs versions avec une installation facile de forge / mods",
                SP = "MultiMC le permite administrar y lanzar varias versiones con una instalación fácil de forge / mods",
                RU = "MultiMC позволяет запускать любые версии игры, также можно легко установить Forge и моды",
                PO = "permite administrar e lançar várias versões com instação fácil de Forge/Mods"
            };

            public TString ColorCodes = new TString()
            {
                EN = "Minecraft Color Codes",
                FR = "Codes de couleur Minecraft",
                SP = "Códigos de color Minecraft",
                RU = "Коды цветов в Minecraft",
                PO = "Códigos das Cores do Minecraft"
            };

            public TString Error_PlayerNotFound = new TString()
            {
                EN = "Player {0} not found",
                FR = "Joueur {0} pas trouvé",
                SP = "Jugador {0} extraviado",
                RU = "Игрок {0} не найден",
                PO = "Jogador {0} não encontrado"
            };

            #region PingCommand
            public TString Error_EnterIP = new TString()
            {
                EN = "Enter an IP",
                FR = "Entrez une adresse IP",
                SP = "Introduzca un IP",
                RU = "Введите IP",
                PO = "Insere um IP"
            };
            public TString Error_IPMain = new TString()
            {
                EN = "You really think that would work?",
                FR = "Vous pensez vraiment que cela fonctionnerait?",
                SP = "¿De verdad crees que funcionaría?",
                RU = "Ты думаешь, что это сработает?",
                PO = "Achas realmente que isso funcionaria?"
            };
            public TString Error_IPRouter = new TString()
            {
                EN = "Minecraft servers don't run on routers DUH",
                FR = "Les serveurs Minecraft ne fonctionnent pas sur les routeurs DUH",
                SP = "Los servidores de Minecraft no funcionan con routers DUH",
                RU = "Сервера Minecraft не работают на DUH",
                PO = "Os Servidores de Minecraft não são executados em routers DUH!"
            };
            public TString Error_IPZero = new TString()
            {
                EN = "Not enough zeros?",
                FR = "Pas assez de zéros?",
                SP = "¿No hay suficientes ceros?",
                RU = "Не хватает нулей?",
                PO = "Não há zeros suficientes?"
            };
            public TString Error_IPGoogle = new TString()
            {
                EN = "This is for minecraft servers not google :(",
                FR = "Ceci est pour les serveurs minecraft pas Google :(",
                SP = "Esto es para servidores de minecraft no google :(",
                RU = "Это для серверов Minecraft, это не гугл :(",
                PO = "Isto é para Servidores de Minecraft, não para o Google :("
            };
            public TString Error_IPYoutube = new TString()
            {
                EN = "This is for minecraft servers not youtube :(",
                FR = "Ceci est pour les serveurs Minecraft, pas youtube :(",
                SP = "Esto es para servidores de minecraft no youtube :(",
                RU = "Это для серверов Minecraft, а не ютуб :(",
                PO = "Isto é para Servidores de Minecraft, não para o YouTube :("
            };
            public TString Error_IPMyWeb = new TString()
            {
                EN = "Trying to ping my own website :D",
                FR = "Essayer de faire un ping sur mon propre site web :D",
                SP = "Tratando de hacer ping a mi propio sitio web: D",
                RU = "Попытаюсь пингануть мой собственный сайт :D",
                PO = "Tentando descobrir o Ping do meu WebSite :D"
            };
            public TString Error_IPBlocked = new TString()
            {
                EN = "Minecraft server has blocked the ping",
                FR = "Le serveur Minecraft a bloqué le ping",
                SP = "El servidor de Minecraft ha bloqueado el ping",
                RU = "Сервер заблокировал пинг",
                PO = "Este Servidor de Minecraft bloqueou o seu Ping"
            };
            public TString Error_EnableQuery = new TString()
            {
                EN = "Minecraft server does not have enable-query set in server.properties",
                FR = "Le serveur Minecraft n'a pas activé la requête dans server.properties",
                SP = "El servidor de Minecraft no tiene enable-query establecido en server.properties",
                RU = "Этот сервер не имеет определённой отметки 'enable-query' в 'server.properties'",
                PO = "Este Servidor de Minecraft tem de ativar o enable-query em server.properties"
            };
            public TString Error_IPInvalid = new TString()
            {
                EN = "This is not a valid ip",
                FR = "Ce n'est pas un ip valide",
                SP = "Esto no es un IP válido",
                RU = "Неверно введён IP",
                PO = "Este IP é inválido"
            };
            public TString Error_Cooldown = new TString()
            {
                EN = "You are on cooldown for 1 mins!",
                FR = "Vous êtes en pause pendant 1 minutes!",
                SP = "Estás en cooldown por 1 minutos!",
                RU = "Вы на кулдауне на 1 минуту!",
                PO = "Estás em cooldown por 1 minuto!"
            };
            public TString Ping_PleaseWait = new TString()
            {
                EN = "Please wait while I ping",
                FR = "Patientez pendant que je ping",
                SP = "Por favor, espere mientras hago ping",
                RU = "Подождите, пожалуйста...",
                PO = "Espera até eu conseguir o Ping"
            };
            public TString Ping_ServerLoading = new TString()
            {
                EN = "Server is loading!",
                FR = "Le serveur est en cours de chargement!",
                SP = "¡El servidor está cargando!",
                RU = "Сервер загружается!",
                PO = "O Servidor está a carregar!"
            };
            public TString Players = new TString()
            {
                EN = "Players",
                FR = "Joueurs",
                SP = "Jugadores",
                RU = "Игроки",
                PO = "Jogadores"
            };
            public TString Ping_ServerOffline = new TString()
            {
                EN = "Server is offline",
                FR = "Le serveur est hors-ligne",
                SP = "Server está desconectado",
                RU = "Сервер оффлайн",
                PO = "O Servidor está Offline"
            };
            #endregion

            public TString List_NoServers = new TString()
            {
                EN = "This community has no servers listed",
                FR = "Cette communauté n'a aucun serveur répertorié",
                SP = "Esta comunidad no tiene servidores",
                RU = "Это сообщество не имеет серверов",
                PO = "Esta Comunidade não tem Servidores listados"
            };
            public TString List_GuildAdmin = new TString()
            {
                EN = "Guild administrators should use",
                FR = "Les administrateurs de guilde devraient utiliser",
                SP = "Los administradores del gremio deben usar",
                RU = "Только для администраторов",
                PO = "Os Administradores devem usar"
            };
            public TString Servers = new TString()
            {
                EN = "Servers",
                FR = "Les serveurs",
                SP = "Servidores",
                RU = "Сервера",
                PO = "Servidores"
            };

            #region InfoCommand
            public TString Info_MCSales = new TString()
            {
                EN = "Minecraft Account Sales",
                FR = "Ventes de compte Minecraft",
                SP = "Ventas de la cuenta de Minecraft",
                RU = "Продажи Minecraft",
                PO = "Vendas de Contas de Minecraft"
            };
            public TString Info_MCSalesUrl = new TString()
            {
                EN = "https://minecraft.net/en-us/stats/",
                FR = "https://minecraft.net/fr-fr/stats/",
                SP = "https://minecraft.net/es-es/stats/",
                RU = "https://minecraft.net/ru-ru/stats/",
                PO = "https://minecraft.net/pt-pt/"
            };
            public TString Info_SalesError = new TString()
            {
                EN = "Stats may be slightly off due to caching",
                FR = "Les statistiques peuvent être légèrement désactivées en raison de la mise en cache",
                SP = "Estadísticas pueden estar ligeramente fuera debido al almacenamiento en caché",
                RU = "Статистика может быть немного неверной",
                PO = "Vendas de Contas de Minecraft"
            };
            public TString Error_Api = new TString()
            {
                EN = "API Error",
                FR = "Erreur API",
                SP = "Error de API",
                RU = "Ошибка API",
                PO = "API Error"
            };
            #endregion

            #region SkinCommand
            public TString Skin_Args = new TString()
            {
                EN = "(Player) | head | cube | full | steal",
                FR = "(Joueur) | head | cube | full | steal",
                SP = "(Jugador) | cabeza | cubo | completo | robar",
                RU = "(игрок) | голова | куб | полный | украсть",
                PO = "(Jogador) | cabeça | cubo | cheio | roubar"
            };
            public TString Skin_Stole = new TString()
            {
                EN = "Stole a skin",
                FR = "Roule une skin",
                SP = "Robó una piel",
                RU = "Украден скин",
                PO = "Roubar uma Skin"
            };
            public TString Error_UnknownArg = new TString()
            {
                EN = "Unknown argument do",
                FR = "Argument inconnu",
                SP = "Argumento desconocido",
                RU = "Неизвестный аргумент",
                PO = "Argumento desconhecido"
            };
            #endregion

            #region NameCommand
            public TString Player = new TString()
            {
                EN = "Player",
                FR = "Joueur",
                SP = "Jugador",
                RU = "Игрок",
                PO = "Jogador"
            };
            public TString Name_OneOnly = new TString()
            {
                EN = "Unknown argument do",
                FR = "Argument inconnu",
                SP = "Argumento desconocido",
                RU = "Неизвестный аргумент",
                PO = "Argumento desconhecido"
            };
            public TString First = new TString()
            {
                EN = "First",
                FR = "Premier",
                SP = "primero",
                RU = "Первый",
                PO = "Primeiro"
            };
            public TString Name_PlayerNotFoundNames = new TString()
            {
                EN = "Player {0} not found, please use the current players name",
                FR = "Joueur {0} pas trouvé, utilisez le nom actuel des joueurs",
                SP = "Jugador {0} no encontrado, por favor use el nombre actual de los jugadores",
                RU = "Игрок {0} не найден, пожалуйста, используйте имя реального игрока",
                PO = "Jogador {0} não encontrado, usa um nickname existente"
            };
            #endregion

            #region StatusCommand
            public TString Status_Mojang = new TString()
            {
                EN = "Mojang Status",
                FR = "État des serveurs Mojang",
                SP = "Estado de Mojang",
                RU = "Статус Mojang",
                PO = "Status de Mojangs"
            };
            public TString Status_MojangAccounts = new TString()
            {
                EN = "Mojang accounts",
                FR = "Comptes Mojang",
                SP = "Cuentas de Mojang",
                RU = "Аккаунты Mojang",
                PO = "Contas de Mojang"
            };
            public TString Status_MojangAuthServers = new TString()
            {
                EN = "Mojang auth servers",
                FR = "Serveurs d'authentification Mojang",
                SP = "Mojang servidores de autenticación",
                RU = "Сервера авторизации Mojang",
                PO = "Servidores de autenticação de Mojang"
            };
            public TString Status_MojangAuthService = new TString()
            {
                EN = "Mojang auth service",
                FR = "Service d'authentification Mojang",
                SP = "Servicio Mojang auth",
                RU = "Сервис авторизации Mojang",
                PO = "Serviço de autenticação de Mojang"
            };
            public TString Status_MojangSessions = new TString()
            {
                EN = "Mojang sessions",
                FR = "Séances de Mojang",
                SP = "Sesiones de Mojang",
                RU = "Сессии Mojang",
                PO = "Sessões de Mojang"
            };
            public TString Status_MinecraftSessions = new TString()
            {
                EN = "Minecraft.net sessions",
                FR = "Séances de Minecraft.net",
                SP = "Sesiones de Minecraft.net",
                RU = "Сессии Minecraft.net",
                PO = "Sessões do Minecraft.net"
            };
            public TString Status_MinecraftSkins = new TString()
            {
                EN = "Minecraft.net skins",
                FR = "Peaux Minecraft.net",
                SP = "Pieles de Minecraft.net",
                RU = "Скины Minecraft.net",
                PO = "Minecraft.net skins"
            };
            public TString Status_MinecraftTextures = new TString()
            {
                EN = "Minecraft.net textures",
                FR = "Les textures de Minecraft.net",
                SP = "Minecraft.net texturas",
                RU = "Текстуры Minecraft.net",
                PO = "Texturas Minecraft.net"
            };
            #endregion

            public TString PeoplePlayingMinecraft = new TString()
            {
                EN = "People playing Minecraft",
                FR = "Les gens qui jouent à Minecraft",
                SP = "Personas jugando Minecraft",
                RU = "Игроки в Minecraft",
                PO = "Pessoas a Jogar Minecraft"
            };
            public TString Text = new TString()
            {
                EN = "Text",
                FR = "Texte",
                SP = "Texto",
                RU = "Текст",
                PO = "Texto"
            };
            public TString Hi = new TString()
            {
                EN = "Hi",
                FR = "salut",
                SP = "Hola",
                RU = "Привет",
                PO = "Olá"
            };
            public TString Get_ErrorLimit = new TString()
            {
                EN = "Text cannot be more than 22 letters/numbers",
                FR = "Le texte ne peut pas dépasser 22 lettres/nombres",
                SP = "El texto no puede tener más de 22 letras / números",
                RU = "Текст не может содержать более 22 символов",
                PO = "O texto não pode ter mais que 22 caracteres"
            };
            public TString OnlineSkinEditor = new TString()
            {
                EN = "Online Skin Editor",
                FR = "Éditeur de peau en ligne",
                SP = "Online Skin Editor",
                RU = "Онлайн редактор скинов",
                PO = "Editor de pele on-line"
            };

            #region BotCommand
            public TString Bot_Desc = new TString()
            {
                EN = "If you have any issue, suggestions or language translations please contact me",
                FR = "Si vous avez un problème, des suggestions ou des traductions linguistiques, contactez-moi",
                SP = "Si tiene algún problema, sugerencias o traducciones de idiomas, póngase en contacto conmigo",
                RU = "Если вы нашли неполадки или у вас есть предложения, обращайтесь ко мне",
                PO = "Se tiveres algum problema, sugestão ou tradução, contacta-me"
            };
            public TString Bot_Footer = new TString()
            {
                EN = "Bot owner commands do not count to the command count above also some of them are secret ones you have to find",
                FR = "Les commandes du propriétaire du robot ne comptent pas sur le compte de commande ci-dessus, mais certains d'entre eux sont secrets que vous devez trouver",
                SP = "Bot comandos de propietario no cuentan para el comando de contar por encima también algunos de ellos son secretos que tiene que encontrar",
                RU = "Команды владельца бота не отображены сверху, поэтому вы должны найти их сами",
                PO = "Os comandos do proprietário do botão não contam para a contagem de comandos acima, também alguns deles são secretos que você precisa encontrar"
            };
            public TString Bot_Owner = new TString()
            {
                EN = "Bot Owner",
                FR = "Propriétaire du robot",
                SP = "Bot Owner",
                RU = "Владелец бота",
                PO = "Bot Owner"
            };
            public TString Language = new TString()
            {
                EN = "Language",
                FR = "La langue",
                SP = "Idioma",
                RU = "Язык",
                PO = "Língua"
            };
            public TString Bot_Lib = new TString()
            {
                EN = "Library",
                FR = "Bibliothèque",
                SP = "Biblioteca",
                RU = "Библиотека",
                PO = "Biblioteca"
            };
            public TString Stats = new TString()
            {
                EN = "Stats",
                FR = "Stats",
                SP = "Estadísticas",
                RU = "Статистика",
                PO = "Status"
            };
            public TString Guilds = new TString()
            {
                EN = "Guilds",
                FR = "Guildes",
                SP = "Gremios",
                RU = "Сервера",
                PO = "Servidores"
            };

            public TString Uptime = new TString()
            {
                EN = "Uptime",
                FR = "Temps de disponibilité",
                SP = "Tiempo de actividad",
                RU = "Время после последнего перезапуска",
                PO = "Tempo de atividade"
            };

            public TString Bot_Invite = new TString()
            {
                EN = "Bot Invite",
                FR = "Invité Bot",
                SP = "Bot Invita",
                RU = "Ссылка на бота",
                PO = "Convidar o BOT"
            };

            public TString Bot_ListGuilds = new TString()
            {
                EN = "Bot List Guilds",
                FR = "Liste alphabétique",
                SP = "Bot List Guilds",
                RU = "Список серверов",
                PO = "Lista de BOTs"
            };
            #endregion

            public TString Info = new TString()
            {
                EN = "Info",
                FR = "Info",
                SP = "Información",
                RU = "Информация",
                PO = "Info"
            };
            public TString Unknown = new TString()
            {
                EN = "Unknown",
                FR = "Inconnu",
                SP = "Desconocido",
                RU = "неизвестный",
                PO = "Desconhecido"
            };
            public TString Health = new TString()
            {
                EN = "Health",
                FR = "Santé",
                SP = "Salud",
                RU = "Здоровье",
                PO = "Vida"
            };
            public TString Height = new TString()
            {
                EN = "Height",
                FR = "la taille",
                SP = "Altura",
                RU = "Высота",
                PO = "Altura"
            };
            public TString Width = new TString()
            {
                EN = "Width",
                FR = "Largeur",
                SP = "Anchura",
                RU = "Ширина",
                PO = "Largura"
            };
            public TString Type = new TString()
            {
                EN = "Type",
                FR = "Type",
                SP = "Tipo",
                RU = "Тип",
                PO = "Tipo"
            };
            public TString Version = new TString()
            {
                EN = "Version",
                FR = "Version",
                SP = "Versión",
                RU = "Версия",
                PO = "Versão"
            };
            public TString Attack = new TString()
            {
                EN = "Attack",
                FR = "Attaque",
                SP = "Ataque",
                RU = "Атака",
                PO = "Atacar"
            };
            public TString Easy = new TString()
            {
                EN = "Easy",
                FR = "Facile",
                SP = "Fácil",
                RU = "Легко",
                PO = "Fácil"
            };
            public TString Normal = new TString()
            {
                EN = "Normal",
                FR = "Normal",
                SP = "Normal",
                RU = "Нормальный",
                PO = "Normal"
            };
            public TString Hard = new TString()
            {
                EN = "Hard",
                FR = "Difficile",
                SP = "Difícil",
                RU = "Жесткий",
                PO = "Díficil"
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
                PO = "Não foi possível encontrar o nome do item ou ID"
            };
            public TString Error_UnknownMob = new TString()
            {
                EN = "Cannot find mob name",
                FR = "Vous ne pouvez pas trouver le nom de la foule",
                SP = "No se puede encontrar el nombre de la mafia",
                RU = "Не удалось найти имя моба",
                PO = "Não foi possível encontrar o nome do mob"
            };
            public TString blocks = new TString()
            {
                EN = "blocks",
                FR = "blocs",
                SP = "bloques",
                RU = "блоки",
                PO = "blocos"
            };
            public TString Fist_Attack = new TString()
            {
                EN = "Fist Attack",
                FR = "Attaque de poing",
                SP = "Ataque de puño",
                RU = "Кулачная атака",
                PO = "Primeiro Ataque"
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
                PO = "Wallpaper"
            };
            public TString ForgecraftWiki = new TString()
            {
                EN = "Wiki And Forgecraft Users",
                FR = "Utilisateurs Wiki et Forgecraft",
                SP = "Usuarios de Wiki y Forgecraft",
                RU = "Вики и пользователи Forgecraft",
                PO = "Membros da Wiki e Forgecraft"
            };
            public TString BukkitNews = new TString()
            {
                EN = "Bukkit News",
                FR = "Nouvelles de Bukkit",
                SP = "Noticias de Bukkit",
                RU = "Новости о Bukkit",
                PO = "Bukkit News"
            };
            public TString FoundSecretCommand = new TString()
            {
                EN = "Hey, you found a secret command :D",
                FR = "Hé, vous avez trouvé une commande secrète :D",
                SP = "Hola, has encontrado un comando secreto :D",
                RU = "Эй, ты нашёл секретную команду :D",
                PO = "Hey, encontraste um comando secreto :D"
            };
            public TString MinecraftClassic = new TString()
            {
                EN = "Minecraft classic was the second phase of developent in 2009 that allowed players to play in the browser using java on the minecraft.net website which was primarly used to build things",
                FR = "Minecraft classic a été la deuxième phase de développement en 2009 qui a permis aux joueurs de jouer dans le navigateur en utilisant java sur le site web minecraft.net qui a été principalement utilisé pour construire des choses",
                SP = "Minecraft clásico fue la segunda fase de desarrollo en 2009 que permitió a los jugadores jugar en el navegador usando java en el sitio web minecraft.net que fue utilizado principalmente para construir cosas",
                RU = "Minecraft Classic был второй фазой разработки игры в 2009, которая позволила играть в браузере, используя Java, на сайте minecraft.net",
                PO = "O Minecraft classic foi a segunda fase do desenvolvimento em 2009, que permitiu que os jogadores jogassem no navegador usando o java no site minecraft.net, que foi usado principalmente para construir coisas"
            };
            public TString Forgecraft = new TString()
            {
                EN = "Forgecraft is the set of private whitelisted servers for mod developers to gather and beta-test their mods in a private environment. Many YouTubers and live-streamers also gather on the server to interact with the mod developers, help play-test the mods, and create videos to let the general public know what the mod developers are doing.",
                FR = "Forgecraft est l'ensemble des serveurs privés de liste blanche pour les développeurs mod pour rassembler et bêta-tester leurs mods dans un environnement privé. De nombreux YouTubers et live-streamers se rassemblent également sur le serveur pour interagir avec les développeurs de mod, aident à tester les mods et à créer des vidéos pour permettre au grand public de savoir ce que font les développeurs de mod.",
                SP = "Forgecraft es el conjunto de servidores privados de listas blancas para que los desarrolladores de mod puedan recopilar y probar sus mods en un entorno privado. Muchos YouTubers y live-streamers también se reúnen en el servidor para interactuar con los desarrolladores de mod, ayudar a jugar a probar los mods y crear videos para que el público en general sepa lo que están haciendo los desarrolladores de mod.",
                RU = "Forgecraft - это набор приватных серверов для создателей модов, предназначенный для проверки модов. Ютуберы и стримеры также могут попасть на такие сервера и помочь разработчикам модов в тестировании, и показать другим игрокам, что создатели модов делают.",
                PO = "Forgecraft é o conjunto de servidores privados com whitelist para desenvolvedores de modificações para coletar e testar seus mods beta em um ambiente privado. Muitos YouTubers e live-streamers também se reúnem no servidor para interagir com os desenvolvedores de mod, ajudar a jogar-testar os mods e criar vídeos para que o público em geral saiba o que os desenvolvedores de modelos estão fazendo."
            };
            public TString ForgecraftWallpaper = new TString()
            {
                EN = "Forgecraft Wallpaper",
                FR = "Fond d'écran Forgecraft",
                SP = "Fondo de Pantalla de Forgecraft",
                RU = "Фон Forgecraft",
                PO = "Forgecraft Wallpaper"
            };
            public TString Bukkit = new TString()
            {
                EN = "RIP Bukkit you will be missed along with other server solutions...",
                FR = "RIP Bukkit vous manquera avec d'autres solutions serveur...",
                SP = "RIP Bukkit te faltará junto con otras soluciones de servidor ...",
                RU = "RIP Bukkit, нам тебя будет не хватать...",
                PO = "RIP Bukkit vais perder com outras soluções para os Servidores..."
            };
            public TString Direwolf20 = new TString()
            {
                EN = "Direwolf20 is a popular youtuber known for his lets plays and mod tutorials on modded minecraft. He also plays on a private server called Forgecraft with a bunch of mod developers and other youtubers with his friends Soaryn and Pahimar",
                FR = "Direwolf20 est un populaire youtuber connu pour ses jeux de joueurs et tutoriels mod sur minecraft modded. Il joue également sur un serveur privé appelé Forgecraft avec un tas de développeurs de mod et d'autres youtubers avec ses amis Soaryn et Pahimar",
                SP = "Direwolf20 es un popular youtuber conocido por su permite juegos y tutoriales de mod en minecraft modded. También juega en un servidor privado llamado Forgecraft con un montón de desarrolladores de mod y otros youtubers con sus amigos Soaryn y Pahimar",
                RU = "Direwolf20 - это популярный ютубер, который известен за его летс-плеи и туториалы по модам. Он также играет на приватном сервере Forgecraft, с несколькими разработчиками модов, с его друзьями Soaryn и Pahimar",
                PO = "Direwolf20 é um popular YouTuber conhecido por seus jogadinhos e tutoriais mod em Minecraft modificado. Ele também joga em um servidor privado chamado Forgecraft com um monte de desenvolvedores de mod e outros youtubers com seus amigos Soaryn e Pahimar"
            };
            public TString Herobrine = new TString()
            {
                EN = "Always watching you...",
                FR = "Te regarde toujours...",
                SP = "Siempre observándote ...",
                RU = "Всегда следим за тобой...",
                PO = "Sempre observando-te..."
            };
            public TString Entity303 = new TString()
            {
                EN = "A minecraft creepy pasta of a former Mojang employee who was fired by Notch and now want revenge",
                FR = "Une creppy pasta de minecraft d'un ancien employé de Mojang qui a été renvoyé par Notch et veut maintenant se venger",
                SP = "Un minecraft espeluznante pasta de un ex empleado de Mojang que fue despedido por Notch y ahora quieren venganza",
                RU = "Крипипаста майнкрафта, которая была замечена Нотчем, но хочет мести",
                PO = "Um Minecraft macarrão assustador de um ex-funcionário de Mojang que foi demitido por Notch e agora quer vingança"
            };
            public TString Israphel = new TString()
            {
                EN = "The best youtube minecraft series that will never die in our hearts... 2010 - 2012 RIP Yogscast",
                FR = "La meilleure série de Youtube Minecraft qui ne mourra jamais dans nos coeurs ... 2010 - 2012 RIP Yogscast",
                SP = "La mejor serie de minecraft de youtube que nunca morirá en nuestros corazones ... 2010 - 2012 RIP Yogscast",
                RU = "Лучшие серии майнкрафта, которые никогда не умрут в наших сердцах... 2010 - 2012 RIP Yogscast",
                PO = "O melhor YouTuber de séries de Minecraft que nunca morrerá nos nossos corações... 2010 - 2012 RIP Yogscast"
            };
            public TString Notch = new TString()
            {
                EN = "Minecraft was created by Notch aka Markus Persson",
                FR = "Minecraft a été créé par Notch aka Markus Persson",
                SP = "Minecraft fue creado por Notch aka Markus Persson",
                RU = "Minecraft был создан Нотчем aka Маркусом Перссоном",
                PO = "O Minecraft foi criado pelo Notch aka Markus Persson"
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
                        "[ mc/lang ]( Установить язык сервера )",
                        "[ mc/addserver ]( Добавить MC сервер в список серверов )",
                        "[ mc/delserver ]( Удалить MC сервер из списка)"
                    },

                    // Portugese
                    new string[]
                    {
                        "[ mc/lang ]( Muda a linguagem do BOT )",
                        "[ mc/addserver ]( Adicionar um Servidor de Minecraft á lista )",
                        "[ mc/delserver ]( Remove um Servidor de Minecraft da lista )",
                    }
                };
            public TString LanguageTranslate = new TString()
            {
                EN = "Want a language translates? Contact",
                FR = "Vous voulez une langue traduire? Contact",
                SP = "¿Quiere traducir un idioma? Contacto",
                RU = "Хотите перевести бота на другой язык? Соединитесь с нами",
                PO = "Pretendes uma tradução? Contacta"
            };
            public TString NoData = new TString()
            {
                EN = "Could not find guild data contact xXBuilderBXx#9113",
                FR = "Impossible de trouver les informations de la guilde contact xXBuilderBXx#9113",
                SP = "No se pudieron encontrar los datos del clan xXBuilderBXx#9113",
                RU = "Не найден контакт с xXBuilderBXx#9113",
                PO = "Não foi possível encontrar o contacto para xXBuilderBXx#9113"
            };
            public TString AdminCommands = new TString()
            {
                EN = "Guild Admin Commands",
                FR = "Commandes d'administration de la guilde",
                SP = "Comandos de Guild Admin",
                RU = "Список админских команд",
                PO = "Comandos Administrativos"
            };
            public TString AdminOnly = new TString()
            {
                EN = "You are not a guild admin",
                FR = "Vous n'êtes pas un administrateur",
                SP = "No eres un administrador del gremio",
                RU = "Вы не администратор",
                PO = "Não és um admin da Comunidade"
            };
            public TString UseList = new TString()
            {
                EN = "Use mc/list for a list of this guilds minecraft servers",
                FR = "Utilisez mc/list pour une liste de ces serveurs minecraft de guilde",
                SP = "Utilice mc/list para obtener una lista de estos servidores de los minecraft de los gremios",
                RU = "Используйте mc/list для списка серверов в майнкрафте для данной гильдии",
                PO = "Usa mc/list para veres a lista de Servidores de Minecraft"
            };
            public TString AddServer = new TString()
            {
                EN = "Enter a tag, ip and name",
                FR = "Entrez une étiquette, un ip et un nom",
                SP = "Introduzca una etiqueta, ip y nombre",
                RU = "Введите тэг, IP и название",
                PO = "Coloca uma tag, ip e nome"
            };
            public TString AddServer_Already = new TString()
            {
                EN = "This server is already on the list",
                FR = "Ce serveur est déjà sur la liste",
                SP = "Este servidor ya está en la lista",
                RU = "Этот сервер уже добавлен",
                PO = "Este Servidor já está na lista"
            };
            public TString AddServer_Added = new TString()
            {
                EN = "Added server",
                FR = "Serveur ajouté",
                SP = "Añadido servidor",
                RU = "Добавлен сервер",
                PO = "Servidor adicionado"
            };
            public TString AddServer_AddedList = new TString()
            {
                EN = "to the guild list",
                FR = "à la liste de guilde",
                SP = "a la lista de los gremios",
                RU = "в список серверов",
                PO = "á lista de Servidores"
            };
            public TString DelServer_Enter = new TString()
            {
                EN = "Delete a server with",
                FR = "Supprimer un serveur avec",
                SP = "Eliminar un servidor con",
                RU = "Удалить сервер с",
                PO = "Eliminar um Servidor com"
            };
            public TString DelServer_None = new TString()
            {
                EN = "This server is not on the list",
                FR = "Ce serveur n'est pas sur la liste",
                SP = "Este servidor no está en la lista",
                RU = "Сервера нет в списке",
                PO = "Este Servidor não está na lista"
            };
            public TString DelServer_Deleted = new TString()
            {
                EN = "Removed server",
                FR = "Serveur supprimé",
                SP = "Servidor eliminado",
                RU = "Удалён сервер",
                PO = "Servidor removido"
            };
            public TString DelServer_List = new TString()
            {
                EN = "from the guild list",
                FR = "de la liste de guilde",
                SP = "de la lista de gremios",
                RU = "из списка серверов данной гильдии",
                PO = "da lista de Servidores"
            };
            public TString ChangeLang = new TString()
            {
                EN = "Change Community Language",
                FR = "Changer la langue de la communauté",
                SP = "Cambiar idioma de la comunidad",
                RU = "Изменить язык сервера",
                PO = "Mudar a Linguagem do BOT"
            };
        }
    }
}
