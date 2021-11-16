using DSharpPlus.VoiceNext;
using NAudio.CoreAudioApi;
using NAudio.Utils;
using NAudio.Wave;

namespace Discord
{
    public static class Audio
    {
        private static WaveFormat REQUIRED_PCM = new WaveFormat(48000, 16, 2);

        public static void PipeCaptureToChannel(WasapiCapture capture, VoiceTransmitSink sink)
        {
            // I don't necessarily understand how this resampling is working.
            // It was copied from: I can't find the source :(.

            var buffer = new BufferedWaveProvider(capture.WaveFormat);
            var resampler = new MediaFoundationResampler(buffer, Audio.REQUIRED_PCM);

            var sourcePcm = capture.WaveFormat;

            var bpsIn = sourcePcm.BitsPerSample;
            var spsIn = sourcePcm.SampleRate;
            var bpsOut = Audio.REQUIRED_PCM.BitsPerSample;
            var spsOut = Audio.REQUIRED_PCM.SampleRate;

            var byteBuffer = Array.Empty<byte>();
            capture.DataAvailable += (_, args) =>
            {
                buffer.AddSamples(args.Buffer, 0, args.BytesRecorded);
                var outBytesLength = args.BytesRecorded * bpsOut / bpsIn * spsOut / spsIn;

                byteBuffer = BufferHelpers.Ensure(byteBuffer, outBytesLength);
                var bytesRead = resampler.Read(byteBuffer, 0, outBytesLength);

                sink.WriteAsync(byteBuffer, 0, bytesRead);
            };
        }
    }
}