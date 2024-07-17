using System;

namespace Pipeline.Common.Pages
{
    // Apply Singleton Stuff
    public class NormalPage<TPage> : ToolbarMenuSection
        where TPage : NormalPage<TPage>, new()
    {
        private static readonly Lazy<TPage> _lazyPage = new Lazy<TPage>(() => new TPage());
        public static TPage Instance => _lazyPage.Value;

        protected NormalPage() : base() { }
    }
}
