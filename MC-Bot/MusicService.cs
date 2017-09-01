using Discord;
using Discord.Audio;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Services
{
    public class MusicService
    {
        public DiscordSocketClient _Client;
        public MusicService(DiscordSocketClient Client)
        {
            _Client = Client;
            _Client.GuildAvailable += _Client_GuildAvailable;
        }

        private Task _Client_GuildAvailable(SocketGuild arg)
        {
            //Console.WriteLine(arg.Name);
            return Task.CompletedTask;
        }

        private Dictionary<ulong, _MusicPlayer> MusicPlayers = new Dictionary<ulong, _MusicPlayer>();
        public _MusicPlayer GetMusicPlayer(IGuild Guild)
        {
            MusicPlayers.TryGetValue(Guild.Id, out _MusicPlayer MP);
            if (MP == null)
            {
                MP = new _MusicPlayer()
                {
                    _BotUser = Guild.GetCurrentUserAsync().GetAwaiter().GetResult(), _AudioClient = null, _Player = null, _Process = null
                };
                MusicPlayers.Add(Guild.Id, MP);
            }
            return MP;
        }

        public class _Song
        {
            public string Name;
            public string Duration;
            public string Url;
            public string Provider;
        }
        public class _MusicPlayer
        {
            public System.Threading.Thread _Player;
            public IAudioClient _AudioClient;
            public Process _Process;
            //public AudioOutStream _AudioStream;
            //public System.IO.Stream _Stream;
            public IGuildUser _BotUser;
            

            public async Task PlayMusic(string MusicPath, IVoiceChannel Chan, bool Change)
            {
                try
                {
                    if (_AudioClient == null)
                    {
                        _AudioClient = await Chan.ConnectAsync();
                    }
                    else
                    {
                        if (_Process != null)
                        {
                            _Process.Close();
                            _Process.Dispose();
                        }
                        if (Change == true)
                        {
                            await _AudioClient.StopAsync();
                            _AudioClient.Dispose();
                            _AudioClient = await Chan.ConnectAsync();
                        }
                    }
                    
                    
                    
                }catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
                
                
               
                
                CreateProcess(MusicPath);
                var _Stream = _Process.StandardOutput.BaseStream;
                var _AudioStream = _AudioClient.CreatePCMStream(AudioApplication.Music, bufferMillis: 500);
                await _Stream.CopyToAsync(_AudioStream);
                await _AudioStream.FlushAsync();

            }

            public async Task ChangeVoiceChannel(string MusicPath, IVoiceChannel NewChan)
            {
                if (_Process == null)
                {
                    CreateProcess(MusicPath);
                }
                _AudioClient = await NewChan.ConnectAsync();
                var _Stream = _Process.StandardOutput.BaseStream;
                var _AudioStream = _AudioClient.CreatePCMStream(AudioApplication.Music, bufferMillis: 500);
                await _Stream.CopyToAsync(_AudioStream);
                await _AudioStream.FlushAsync();
            }

            public void Stop(bool FullStop = false)
            {
                if (FullStop == true)
                {
                    if (_AudioClient != null)
                    {
                        _AudioClient.StopAsync().GetAwaiter();
                    }
                }
                if (_Process != null)
                {
                    _Process.Dispose();
                }
            }

            private void CreateProcess(string MusicPath)
            {
                var StartProcess = new ProcessStartInfo
                {
                    FileName = "ffmpeg",
                    Arguments = $"-i {MusicPath} -ac 2 -f s16le -ar 48000 pipe:1",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };
                Process GetProcess = Process.Start(StartProcess);
                _Process = GetProcess;
            }
        }
    }

}
