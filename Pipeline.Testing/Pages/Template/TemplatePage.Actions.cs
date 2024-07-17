

namespace Pipeline.Testing.Pages.Template
{
    public partial class TemplatePage
    {

        public string DoSomething_1()
        {
            return Template_lbl.GetText();
        }

        public TemplatePage DoSomething_2(string pwd)
        {
            Template_txt.SetText(pwd);
            return this;
        }

        public TemplatePage DoSomething_3()
        {
            Template_btn.Click();
            return this;
        }
    }
}
