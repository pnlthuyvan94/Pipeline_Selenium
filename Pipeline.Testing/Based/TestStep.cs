namespace LBM.Testing.Based
{
    public class TestStep
    {
        public int step = 0;
        public string testSectionName = string.Empty;
        public string testCaseName = string.Empty;

        public string name = string.Empty;
        public string title = string.Empty;
        public string description = string.Empty;

        public TestStep(string name, string title, string description)
        {
            this.name = name;
            this.title = title;
            this.description = description;
        }
    }
}
