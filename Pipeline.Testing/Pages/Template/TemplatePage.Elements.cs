using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Template
{
    public partial class TemplatePage : NormalPage<TemplatePage>
    {
        public TemplatePage() : base()
        {
        }

        public Label Template_lbl => new Label(FindType.XPath, "");
        public Button Template_btn => new Button(FindType.XPath, "");
        public Textbox Template_txt => new Textbox(FindType.XPath, "");
    }
}
