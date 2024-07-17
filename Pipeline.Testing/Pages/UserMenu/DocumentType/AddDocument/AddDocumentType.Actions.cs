using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using System;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.UserMenu.DocumentType.AddDocument
{
    public partial class AddDocumentType
    {
        public AddDocumentType InsertName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                Name_txt.SetText(name);
            return this;
        }

        public string[] SelectCategory (string[] listCategory, int[] listindex)
        {

            if (listCategory.Length != 0 && AvailableCategory_lst.IsItemExisted(GridFilterOperator.EqualTo, listCategory))
            {
                AvailableCategory_lst.Select(listCategory);
                return listCategory;
            }
            else
            {
                List<string> getlistCategory = new List<string>();
                string[] newlistCategory = new string[] { };
                foreach (var index in listindex)
                {
                    string listXpath = $"//*[@id='ctl00_CPH_Content_rlbCategoriesAvailable']//ul[@class='rlbList']/li[{index}]/span";
                    Textbox Item = new Textbox(FindType.XPath, listXpath);
                    string Itemvalue = Item.GetText().ToString();
                    getlistCategory.Add(Itemvalue);
                    newlistCategory = getlistCategory.ToArray();
                }
                AvailableCategory_lst.Select(newlistCategory);
                return newlistCategory;
            }
        }

        public bool IsExistingCategory(string[] listCategory)
        {
            return SelectedCategory_lst.IsItemExisted(GridFilterOperator.EqualTo, listCategory);
        }

        public void CategoryToLeft()
        {
            CategoryToLeft_btn.Click();
        }

        public void RoleToLeft()
        {
            RoleToLeft_btn.Click();
        }

        public string[] SelectRole( string[] listRole, int[] listindex)
        {
            if (listRole.Length != 0)
                AvailableRole_lst.Select(listRole);
            if (listRole.Length != 0 && AvailableRole_lst.IsItemExisted(GridFilterOperator.EqualTo, listRole))
            {
                AvailableRole_lst.Select(listRole);
                return listRole;
            }
            else
            {
                List<string>  getlistRole = new List<string> { };
                string[] newlistMyarray = new string[] { };
                foreach (var index in listindex)
                {
                    string listXpath = $"//*[@id='ctl00_CPH_Content_rlbRolesAvailable']//ul[@class='rlbList']/li[{index}]/span";
                    Textbox Item = new Textbox(FindType.XPath, listXpath);
                    string Itemvalue = Item.GetText().ToString();
                    //Array.Resize(ref listRole, listindex.Length + 1);
                    getlistRole.Add(Itemvalue);
                    newlistMyarray = getlistRole.ToArray();
                }
                AvailableRole_lst.Select(newlistMyarray);
                return newlistMyarray;
            }
        }

        public bool IsExistingRole(string[] listRole)
        {
            return SelectedRole_lst.IsItemExisted(GridFilterOperator.EqualTo, listRole);
        }

        public void Insert()
        {
            Insert_btn.Click();
            DocumentType_Grid.WaitGridLoad();
        }

        public void Cancel()
        {
            Cancel_btn.Click();
        }
    }
}