using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.UserMenu.Role.RolePermission
{
    public partial class RolePermissionPage
    {
        public bool DisplayCorrectGridBaseOnType(string permissionType)
        {
            return GridTitle_lbl.GetText() == permissionType;
        }
    }
}
