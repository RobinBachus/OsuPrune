using osu_database_reader.BinaryFiles;

namespace OsuPrune
{
    internal static class Common
    {
        // File paths
        public static string BASE_PATH { get; } = @"D:\Scripts\CsharpPractice\Osu_Test\TestFiles";
        public static string OSU_FILE_PATH { get; } = $@"{BASE_PATH}\TestBinaryFiles\osu!.db";
        public static string OSU_SONG_FILE_PATH { get; } = $@"{BASE_PATH}\TestSongFolder";
        public static string OSU_DOWNLOAD_FILE_PATH { get; } = $@"{BASE_PATH}\TestDownloads";

        /// <summary>
        /// The osu!.db file as an object that holds data about all beatmaps and some other info 
        /// </summary>
        public static OsuDb OsuFile { get; } = OsuDb.Read(OSU_FILE_PATH);

        /// <summary>
        /// The date used to determine of a map has been played
        /// </summary>
        public static DateTime CompareDate { get; set; } = new DateTime(2020, 1, 1);

    }
}
