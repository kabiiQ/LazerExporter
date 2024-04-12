﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using BeatmapExporter.Exporters.Lazer.LazerDB.Schema;
using Realms;

namespace Test.schemas
{
    /// <summary>
    /// A realm model containing metadata for a single score.
    /// </summary>
    [MapTo("Score")]
    public class ScoreInfo : RealmObject
    {
        [PrimaryKey]
        public Guid ID { get; set; }
        
        public Beatmap? BeatmapInfo { get; set; }
        
        public DateTimeOffset Date { get; set; }
        
        [MapTo("User")]
        public RealmUser RealmUser { get; set; } = null!;
        
        //could not find a username attached to score besides this way
        private APIUser? user;

        [Ignored]
        public APIUser User =>
            user ??= new APIUser
            {
                Username = RealmUser.Username
            };

        //GetDisplayTitle() step in (from lazer's ScoreInfoExtensions) to stay consistent with lazer's naming
        //Display methods can be removed and replaced with a realm property if following lazer's naming is not important 
        public string Display() => $"{this.User.Username} playing {this.BeatmapInfo?.Display() ?? "unknown"}";
        
        public string ArchiveFilename()
        {
            string scoreString = this.Display();
            string filename = $"{scoreString} ({this.Date.LocalDateTime:yyyy-MM-dd_HH-mm})";

            return filename;
        }
    }
}
