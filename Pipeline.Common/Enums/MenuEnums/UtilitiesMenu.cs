namespace Pipeline.Common.Enums.MenuEnums
{
    public static class UtilitiesMenu
    {
        public static string Import { get { return "Import"; } }

        public static string GetExportItemWithType(string sectionname, string exportType)
        {
            return "Export " + sectionname  + exportType;
        }
    }
}
