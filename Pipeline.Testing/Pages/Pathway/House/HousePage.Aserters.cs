using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Pathway.House
{
    public partial class HousePage
    {
        protected PathwayHouseProperty GetHouseProperty(string houseName)
        {
            PathwayHouseProperty houseProperty = new PathwayHouseProperty();

            CheckBox FloorPlan_Chk = new CheckBox(FindType.XPath, string.Format(_floorPlan, houseName));
            CheckBox ExteriorDesigner_Chk = new CheckBox(FindType.XPath, string.Format(_exterior, houseName));
            CheckBox InteriorDesigner_Chk = new CheckBox(FindType.XPath, string.Format(_interior, houseName));
            CheckBox MediaCenter_Chk = new CheckBox(FindType.XPath, string.Format(_media, houseName));

            houseProperty.FloorPlan = FloorPlan_Chk.IsChecked;
            houseProperty.Exterior = ExteriorDesigner_Chk.IsChecked;
            houseProperty.Interior = InteriorDesigner_Chk.IsChecked;
            houseProperty.Media = MediaCenter_Chk.IsChecked;

            return houseProperty;
        }

        public bool IsCorrectHouseProperty(string houseName, PathwayHouseProperty expectedProperty)
        {
            PathwayHouseProperty actual = GetHouseProperty(houseName);
            if (actual.FloorPlan != expectedProperty.FloorPlan)
                return false;
            if (actual.Exterior != expectedProperty.Exterior)
                return false;
            if (actual.Interior != expectedProperty.Interior)
                return false;
            if (actual.Media != expectedProperty.Media)
                return false;

            return true;
        }

    }

    public class PathwayHouseProperty
    {
        public bool FloorPlan { get; set; }
        public bool Exterior { get; set; }
        public bool Interior { get; set; }
        public bool Media { get; set; }
    }

}
