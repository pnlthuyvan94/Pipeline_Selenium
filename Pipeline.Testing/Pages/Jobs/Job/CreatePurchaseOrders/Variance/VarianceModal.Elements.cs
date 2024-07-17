using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.Job.CreatePurchaseOrders.Variance
{
    public partial class VarianceModal : CreatePurchaseOrdersPage
    {
        protected Label ModalTitle_lbl => new Label(FindType.XPath, "//*[@id='confirm-po-pricechange-modal']/section/header/div[2]/h1/label/span");        
        protected Button ConfirmChanges_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_btnConfirmChanges']");
        protected Button Close_btn => new Button(FindType.XPath, "//*[@id='confirm-po-pricechange-modal']/section/header/a");
    }
}
