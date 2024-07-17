namespace Pipeline.Testing.Pages.UserMenu.Role.AddRole
{
    public partial class AddRoleModal
    {
        public AddRoleModal EnterName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                Name_txt.SetText(name);
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
        }

        public void Close()
        {
            Close_btn.Click();
        }
    }
}