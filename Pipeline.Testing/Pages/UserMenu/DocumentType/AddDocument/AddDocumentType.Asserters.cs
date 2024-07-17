using System;

namespace Pipeline.Testing.Pages.UserMenu.DocumentType.AddDocument
{
    public partial class AddDocumentType
    {
        public bool IsModalDisplayed
        {
            get
            {
                int iTimeOut = 0;
                while (Name_txt == null || Name_txt.IsDisplayed() == false)
                {
                    System.Threading.Thread.Sleep(500);
                    iTimeOut++;
                    if (iTimeOut == 10)
                    {
                        throw new TimeoutException("Add Document Type modal is not displayed.");
                    }
                }
                return (CategoryModal_lbl.GetText() == "Categories" && RoleVisibilityModal_lbl.GetText() == "Role Visibility");
            }
        }

    }
}
