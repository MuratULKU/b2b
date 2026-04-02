namespace CoreUI.Data
{
    public class UpdateCheckResponse
    {
        public bool HasUpdate { get; set; }

        public string LatestVersion { get; set; }

        public string DownloadUrl { get; set; }

        public bool Mandatory { get; set; }
        public string Version { get; set; }
    }
}
