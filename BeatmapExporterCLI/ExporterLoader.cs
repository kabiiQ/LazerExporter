﻿using BeatmapExporterCLI.Data;
using BeatmapExporterCLI.Interface;

namespace BeatmapExporterCLI
{
    internal class ExporterLoader
    {
        const string Version = "1.3.11";
        static async Task Main(string[] args) 
        {
            // check application version 
            try
            {
                var client = new HttpClient()
                {
                    Timeout = TimeSpan.FromSeconds(2),
                };
                var latest = await client.GetStringAsync("https://raw.githubusercontent.com/kabiiQ/BeatmapExporter/main/VERSION");
                if(latest != Version)
                {
                    Console.WriteLine($"UPDATE AVAILABLE for BeatmapExporter: ({Version} -> {latest})\nhttps://github.com/kabiiQ/BeatmapExporter/releases/latest\n");
                }
            }
            catch (Exception) { } // unable to load version from github. not critical error, dont bother user

            // currently only load lazer, can add interface for selecting osu stable here later
            ExporterApp exporter = LazerLoader.Load(args.FirstOrDefault());

            exporter.StartApplicationLoop();

            ExporterApp.Exit();
        }
    }
}