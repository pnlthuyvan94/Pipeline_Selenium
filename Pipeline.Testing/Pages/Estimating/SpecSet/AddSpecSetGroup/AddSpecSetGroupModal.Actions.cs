using OpenQA.Selenium;

namespace Pipeline.Testing.Pages.Estimating.SpecSet.AddSpecSetGroup
{
    public partial class AddSpecSetGroupModal
    {
        public AddSpecSetGroupModal AddGroupName(string name)
        {
            GroupName_txt.SetText(name);
            return this;
        }

        public AddSpecSetGroupModal SetUseDefault(string useDefault)
        {
            if ("TRUE".Equals(useDefault))
            {
                UseDefault_chb.SetCheck(true);
            }
            else
            {
                UseDefault_chb.SetCheck(false);
            }

            return this;
        }

        public void Save()
        {
            SpecSetSave_btn.Click();
        }

        public void CloseModal()
        {
            SpecSetClose_btn.Click();
            System.Threading.Thread.Sleep(500);
        }
    }
}
