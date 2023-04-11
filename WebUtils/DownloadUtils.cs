namespace OsuPrune.WebUtils
{
    internal class DownloadUtils
    {
        public static async Task Download(string url, string localPath)
        {

            using HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                using Stream stream = await response.Content.ReadAsStreamAsync();
                using FileStream fileStream = new(localPath, FileMode.Create, FileAccess.Write);
                await stream.CopyToAsync(fileStream);
            }
            else
            {
                Console.WriteLine($"Failed to download file. Status code: {response.StatusCode}");
            }

        }
    }
}
