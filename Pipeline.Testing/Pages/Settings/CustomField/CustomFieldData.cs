using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Settings.CustomField
{
    public class CustomFieldData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public string FieldType { get; set; }
        public bool Default { get; set; }
        public string Note { get; set; }
        public string Value { get; set; }
        public CustomFieldData()
        {
            Title = string.Empty;
            Description = string.Empty;
            Tag = string.Empty;
            FieldType = string.Empty;
            Default = true;
            Note = string.Empty;
            Value = string.Empty;
        }
    }
}
