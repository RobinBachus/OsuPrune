using osu_database_reader.BinaryFiles;

namespace OsuPrune
{
    internal static class Common
    {
        // File paths
        public static string BASE_PATH { get; } = @"D:\Scripts\CsharpPractice\OsuPrune\TestFiles";
        public static string BINARY_FILES_PATH { get; } = $@"{BASE_PATH}\TestBinaryFiles";
        public static string OSU_SONG_FILE_PATH { get; } = $@"{BASE_PATH}\TestSongFolder";
        public static string OSU_DOWNLOAD_FILE_PATH { get; } = $@"{BASE_PATH}\TestDownloads";

        /// <summary>
        /// The osu!.db file as an object that holds data about all beatmaps and some other info 
        /// </summary>
        public static OsuDb OsuFile { get; } 
            = OsuDb.Read(BINARY_FILES_PATH + @"\osu!.db");

        /// <summary>
        /// The collection.db file as an object that holds data about collections made by the player
        /// </summary>
        public static CollectionDb CollectionFile { get; } 
            = CollectionDb.Read(BINARY_FILES_PATH + @"\collection.db");

        /// <summary>
        /// The presence.db file as an object that holds data about information about the player
        /// </summary>
        public static PresenceDb PresenceFile { get; }
            = PresenceDb.Read(BINARY_FILES_PATH + @"\presence.db");

        /// <summary>
        /// The scores.db file as an object that holds data about replays and scores
        /// </summary>
        public static ScoresDb ScoresFile { get; }
            = ScoresDb.Read(BINARY_FILES_PATH + @"\scores.db");

        /// <summary>
        /// The date used to determine of a map has been played
        /// </summary>
        public static DateTime CompareDate { get; set; } = new DateTime(2010, 1, 1);

    }
}
