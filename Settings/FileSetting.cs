namespace MatchTracker.Settings
{
    public static class FileSetting
    {
        public const string PlayerImagesFolderPath = @"assets\Images\Players";
        public const string TeamImagesFolderPath = @"assets\Images\Teams";
        public const string UserImagesFolderPath = @"assets\Images\Users";
        public const string DefaultImagePath = @"assets\Images\Default.webp";
        public const string AllowedExtensions = ".jpg,.jpeg,.png,.webp";
        public const int MaxImageSizeMB = 2;
        public const int MaxImageSizeBytes = MaxImageSizeMB * 1024 * 1024;
    }
}
