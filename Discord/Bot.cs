using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.VoiceNext;

namespace Discord
{
    public class Bot
    {
        private DiscordClient client;

        /// <summary>
        ///     Send a direct <c>msg</c> to a given <c>member</c>.
        /// </summary>
        public static async Task SendDMToMember(DiscordMember member, string msg)
        {
            var channel = await member.CreateDmChannelAsync();
            await channel.SendMessageAsync(msg);
        }

        /// <summary>
        ///     Create the DJ bot.
        ///     This requires the <c>DISCORS_CLIENT_TOKEN</c> environment variable to be
        ///     present.
        /// </summary>
        public Bot(Configuration cfg)
        {
            client = createClient(cfg);

            client.UseVoiceNext();

            var cmds = useCommandsNext();
            cmds.RegisterCommands<MusicCommandModule>();
        }
        /// <summary>
        ///     Connects the discord client and runts the program indefinitely
        ///     by calling <c>Task.Delay(-1)</c>.
        /// </summary>
        public async Task Run()
        {
            await client.ConnectAsync();
            await Task.Delay(-1);
        }

        private DiscordClient createClient(Configuration cfg)
        {
            return new DSharpPlus.DiscordClient(new DSharpPlus.DiscordConfiguration
            {
                Token = cfg.DISCORD_CLIENT_TOKEN,
                TokenType = DSharpPlus.TokenType.Bot,
                Intents = DSharpPlus.DiscordIntents.AllUnprivileged,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
            });
        }
        private CommandsNextExtension useCommandsNext()
        {
            var cmdsNextConf = new CommandsNextConfiguration
            {
                StringPrefixes = new[] { "!" },
                EnableDms = true,
                DmHelp = true,
            };

            return client.UseCommandsNext(cmdsNextConf);
        }
    }
}