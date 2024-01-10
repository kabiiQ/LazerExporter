﻿using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Pipes;

namespace BeatmapExporter.Exporters
{
    public class Transcoder
    {
        public bool Available { get; }

        public Transcoder()
        {
            try
            {
                var _ = FFMpeg.GetAudioCodecs();
                Console.WriteLine("FFmpeg successfully loaded! .mp3 export for beatmaps that use other audio formats will be available.");
                Available = true;
            }
            catch (Exception)
            {
                Console.WriteLine("FFmpeg not found. Conversion to .mp3 for beatmap audio export will not be available.");
                Available = false;
            }
        }

        public bool TranscodeMP3(FileStream sourceFile, string destFile)
        {
            if (!Available) throw new InvalidOperationException("Audio transcoder is not available within this runtime.");

            return FFMpegArguments
                .FromPipeInput(new StreamPipeSource(sourceFile))
                .OutputToFile(destFile, overwrite: false, o => o.WithAudioCodec(AudioCodec.LibMp3Lame))
                .ProcessSynchronously();
        }
    }
}
