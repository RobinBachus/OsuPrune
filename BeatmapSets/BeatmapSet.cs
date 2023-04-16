using osu.Shared;
using osu_database_reader.Components.Beatmaps;
using static OsuPrune.Common;
using static OsuPrune.Beatmaps.CommonMethods;

namespace OsuPrune.Beatmaps
{
    internal class BeatmapSet
    {
        public List<BeatmapEntry> Beatmaps { get; }
        public List<BeatmapEntry> PlayedBeatmaps { get; }
        public List<KeyValuePair<BeatmapEntry, double>> Diffs { get; }
        public string FolderName { get; }
        public int ID { get; }
        public string URL { get; }
        public List<GameMode> GameModes { get; }
        public bool HasPlayedBeatmaps { get; }

        public BeatmapSet(List<BeatmapEntry> beatmaps)
        {
            Beatmaps = SortMaps(beatmaps);
            PlayedBeatmaps = Beatmaps.FindAll(m => m.LastPlayed > CompareDate);
            Diffs = GetDiffs(Beatmaps);
            FolderName = Beatmaps[0].FolderName;
            ID = Beatmaps[0].BeatmapSetId;
            URL = GetBeatmapSetURL(ID);
            GameModes = Beatmaps.Select(beatmap => beatmap.GameMode).Distinct().ToList();
            HasPlayedBeatmaps = PlayedBeatmaps.Count > 0;
        }

        public bool IsPlayed(int index)
        {
            if (index > Beatmaps.Count || index < 0) throw new IndexOutOfRangeException($"Index '{index}' is out of range");

            if (!HasPlayedBeatmaps) return false;
            return Beatmaps[index].LastPlayed > CompareDate;
        }

        public bool IsPlayed(BeatmapEntry beatmap)
        {
            if (!Beatmaps.Contains(beatmap)) throw new ArgumentException($"Beatmap {beatmap.BeatmapFileName} is not part of this set");

            if (!HasPlayedBeatmaps) return false;
            return PlayedBeatmaps.Contains(beatmap);
        }

        public double GetDifficulty(int index)
        {
            if (index > Beatmaps.Count || index < 0) throw new IndexOutOfRangeException($"Index '{index}' is out of range");
            return Diffs[index].Value;
        }

        public double GetDifficulty(BeatmapEntry beatmap)
        {
            if (!Beatmaps.Contains(beatmap)) throw new ArgumentException($"Beatmap {beatmap.BeatmapFileName} is not part of this set");
            return Diffs.Find(x => x.Key.Equals(beatmap)).Value;
        }

        private static List<BeatmapEntry> SortMaps(List<BeatmapEntry> beatmaps)
        {
            List<BeatmapEntry> sortedMaps = new();

            List<BeatmapEntry> Stand = beatmaps.FindAll(m => m.GameMode == GameMode.Standard);
            Stand.Sort((x, y) => x.DiffStarRatingStandard[Mods.None].CompareTo(y.DiffStarRatingStandard[Mods.None]));
            sortedMaps.AddRange(Stand);

            List<BeatmapEntry> Taiko = beatmaps.FindAll(m => m.GameMode == GameMode.Taiko);
            Taiko.Sort((x, y) => x.DiffStarRatingTaiko[Mods.None].CompareTo(y.DiffStarRatingTaiko[Mods.None]));
            sortedMaps.AddRange(Taiko);

            List<BeatmapEntry> Catch = beatmaps.FindAll(m => m.GameMode == GameMode.CatchTheBeat);
            Catch.Sort((x, y) => x.DiffStarRatingCtB[Mods.None].CompareTo(y.DiffStarRatingCtB[Mods.None]));
            sortedMaps.AddRange(Catch);

            List<BeatmapEntry> Mania = beatmaps.FindAll(m => m.GameMode == GameMode.Mania);
            Mania.Sort((x, y) => x.DiffStarRatingMania[Mods.None].CompareTo(y.DiffStarRatingMania[Mods.None]));
            sortedMaps.AddRange(Mania);

            return sortedMaps;
        }

        private static List<KeyValuePair<BeatmapEntry, double>> GetDiffs(List<BeatmapEntry> beatmaps)
        {
            List<KeyValuePair<BeatmapEntry, double>> diffs = new();
            foreach (BeatmapEntry beatmap in beatmaps)
            {
                double difficulty = 0;

                switch (beatmap.GameMode)
                {
                    case GameMode.Standard:
                        difficulty = beatmap.DiffStarRatingStandard[Mods.None];
                        break;
                    case GameMode.Taiko:
                        difficulty = beatmap.DiffStarRatingTaiko[Mods.None];
                        break;
                    case GameMode.CatchTheBeat:
                        difficulty = beatmap.DiffStarRatingCtB[Mods.None];
                        break;
                    case GameMode.Mania:
                        difficulty = beatmap.DiffStarRatingMania[Mods.None];
                        break;
                    default:
                        break;
                }

                diffs.Add(new(beatmap, difficulty));
            }

            return diffs;
        }

        public override string ToString()
        {
            string str = "";
            str += FolderName + "\n";
            str += "\t Info:\n";
            str += $"\t\t Set ID: {ID}\n";
            str += $"\t\t Set URL: {URL}\n";
            str += $"\t\t Download URL: {GetDownloadURL(ID)}\n";
            str += $"\t\t Map count: {Beatmaps.Count}\n";
            str += $"\t\t Played: {HasPlayedBeatmaps}\n";
            str += $"\t\t Played maps count: {PlayedBeatmaps.Count}\n";
            str += $"\t\t Game modes: {string.Join(", ", GameModes)}\n";

            str += "\t Maps:\n";
            Beatmaps.ForEach(m => str += $"\t\t [{(IsPlayed(m) ? "x" : " ")}] [{m.GameMode.ToString()[0]}: ⭐{Math.Round(GetDifficulty(m), 2)}] {m.BeatmapId}: {m.BeatmapFileName}\n \t\t\t URL: {GetBeatmapURL(m)}\n");

            return str;
        }
    }
}
