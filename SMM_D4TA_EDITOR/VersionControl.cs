namespace SMM_D4TA_EDITOR
{
    public static class VersionControl
    {
        public const int MajorUpdate = 0;
        public const int MinorUpdate = 2;
        public const int PatchUpdate = 0;

        public static string GetString()
        {
            return $"{MajorUpdate}.{MinorUpdate}.{PatchUpdate}";
        }
    }
}