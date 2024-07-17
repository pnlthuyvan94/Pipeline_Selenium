

namespace Pipeline.Testing.Pages.Jobs.Job.JobSpecSets
{
    class JobSpecSetsData
    {
        public string JobSpecSetName { get; set; }
        public string CommunityStandard { get; set; }
        public string JobSetOverride { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public JobSpecSetsData()
        {
            JobSpecSetName = string.Empty;
            CommunityStandard = string.Empty;
            JobSetOverride = string.Empty;
            PageSize = 0;
            PageNumber = 0;
        }
    }
}
