namespace Pipeline.Testing.Pages.UserMenu.Role
{
    public partial class RolePage
    {
        public bool IsRoleLocked(string userRole)
        {
            if (Role_Grid.IsLockedRoleOnItemInGrid("Admin"))
            {
                return true;
            }
            else
                return false;
        }
    }
}
