namespace OsuPrune.WebUtils
{
    internal class DownloadUtils
    {
        public static async Task Download(string url, string localPath)
        {
            using FileStream fileStream = new(localPath, FileMode.Create, FileAccess.Write);
            using HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                using Stream stream = await response.Content.ReadAsStreamAsync();
                await stream.CopyToAsync(fileStream);
            }
            else
            {
                throw new HttpRequestException($"Failed to download file. Status code: {response.StatusCode}", null, response.StatusCode);
            }
        }
    }
}
