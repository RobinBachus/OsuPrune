using osu_database_reader.Components.Beatmaps;
using OsuPrune.Beatmaps;
using System.Runtime.CompilerServices;
using static OsuPrune.Common;

[assembly: InternalsVisibleTo("OsuPrune_UnitTest")]

namespace OsuPrune
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SortedSets sortedSets = new();

            List<BeatmapSet> unplayedSets = sortedSets.BeatmapSets.FindAll(set => !set.HasPlayedBeatmaps);
            List<BeatmapEntry> unplayedMaps = new();
            int itr = 0;
            foreach (BeatmapSet set in sortedSets.BeatmapSets) {
                foreach (var map in set.Beatmaps)
                {
                    //Console.WriteLine($"[{map.GameMode}] {map.BeatmapFileName}");
                    //OsuFile.Beatmaps.Remove(map);
                    if (!set.IsPlayed(map)) itr++;
                }
            }
            Console.WriteLine(itr);
        }
    }
}