using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Costing.TaxGroup.AddTaxGroup;

namespace Pipeline.Testing.Pages.Costing.TaxGroup
{
    public partial class TaxGroupPage : DashboardContentPage<TaxGroupPage>
    {
        public AddTaxGroupModal AddTaxGroupModal { get; private set; }

        protected IGrid TaxGroup_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgTaxGroups_ctl00']", "");

        protected Button AddTaxGroup_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddTaxGroup']");

        public TaxGroupPage() : base()
        {

        }

    }

}
