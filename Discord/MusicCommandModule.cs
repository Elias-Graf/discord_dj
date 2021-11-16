using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.VoiceNext;

using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace Discord
{
    public class MusicCommandModule : BaseCommandModule
    {
        private WasapiCapture? capture;
        private VoiceTransmitSink? sink;

        [Command("join")]
        public async Task Join(CommandContext ctx)
        {
            if (ctx.Member.VoiceState == null)
            {
                await Discord.Bot.SendDMToMember(ctx.Member, "You are not in a voice channel");
                return;
            }

            var channel = ctx.Member.VoiceState.Channel;
            var connection = await channel.ConnectAsync();

            sink = connection.GetTransmitSink();
        }
        [Command("setCaptureDevice")]
        public Task SetCaptureDevice(CommandContext ctx)
        {
            if (capture != null)
            {
                capture.StopRecording();
                capture.Dispose();
            }

            var device = SourceSelector.PromptUserToSelectDevice();
            capture = new WasapiCapture(device);

            return Task.CompletedTask;
        }
        [Command("startPlayback")]
        public async Task StartPlayback(CommandContext ctx)
        {
            if (capture == null)
            {
                await Discord.Bot.SendDMToMember(ctx.Member, "Please select a capture device before starting");
                return;
            }

            if (sink == null)
            {
                await Discord.Bot.SendDMToMember(ctx.Member, "Please join a channel before starting");
                return;
            }

            var buffer = new BufferedWaveProvider(capture.WaveFormat);

            capture.StartRecording();
            Discord.Audio.PipeCaptureToChannel(capture, sink);
        }
    }
}