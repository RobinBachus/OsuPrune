using osu_database_reader.Components.Beatmaps;

namespace OsuPrune.Beatmaps
{
    internal static class CommonMethods
    {
        public static string BasueUrl { get; } = "https://osu.ppy.sh/beatmapsets";
        public static string BaseDownloadURL { get; } = "https://osu.direct/api/d/";

        public static string GetBeatmapSetURL(int id)
        {
            return $"{BasueUrl}/{id}";
        }

        public static string GetBeatmapURL(BeatmapEntry beatmap)
        {
            return $"{GetBeatmapSetURL(beatmap.BeatmapSetId)}#{beatmap.GameMode}/{beatmap.BeatmapId}";
        }

        public static string GetDownloadURL(BeatmapEntry beatmap)
        {
            return BaseDownloadURL + beatmap.BeatmapSetId;
        }

        public static string GetDownloadURL(BeatmapSet set)
        {
            return BaseDownloadURL + set.SetID;
        }

        public static string GetDownloadURL(int id)
        {
            return BaseDownloadURL + id;
        }
    }
}
