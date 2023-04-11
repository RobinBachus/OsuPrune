using osu.Shared;
using osu_database_reader.Components.Beatmaps;
using static OsuPrune.Common;

namespace OsuPrune.Beatmaps
{
    internal class SortedSets
    {
        public List<BeatmapSet> BeatmapSets { get; set; }

        public List<BeatmapSet> Standard { get; set; }
        public List<BeatmapSet> Taiko { get; set; }
        public List<BeatmapSet> CatchTheBeat { get; set; }
        public List<BeatmapSet> Mania { get; set; }
        public List<BeatmapSet> MultipleModes { get; set; }

        public List<BeatmapSet> PlayedSets { get; set; }

        public SortedSets()
        {
            List<BeatmapEntry> beatmaps = OsuFile.Beatmaps;

            List<BeatmapSet> beatmapSets = new();

            // Sort maps into sets based on folder name
            beatmaps.GroupBy(m => m.FolderName)
                    .ToList()
                    .ForEach(set => beatmapSets.Add(new(set.ToList())));
            BeatmapSets = beatmapSets;

            Standard = BeatmapSets.FindAll(beatmapSet => beatmapSet.GameModes.All(gm => gm == GameMode.Standard));
            Taiko = BeatmapSets.FindAll(beatmapSet => beatmapSet.GameModes.All(gm => gm == GameMode.Taiko));
            CatchTheBeat = BeatmapSets.FindAll(beatmapSet => beatmapSet.GameModes.All(gm => gm == GameMode.CatchTheBeat));
            Mania = BeatmapSets.FindAll(beatmapSet => beatmapSet.GameModes.All(gm => gm == GameMode.Mania));

            MultipleModes = BeatmapSets.FindAll(beatmapSet => beatmapSet.GameModes.Count > 1);

            PlayedSets = beatmapSets.FindAll(beatmapSet => beatmapSet.HasPlayedBeatmaps);
        }
    }
}
