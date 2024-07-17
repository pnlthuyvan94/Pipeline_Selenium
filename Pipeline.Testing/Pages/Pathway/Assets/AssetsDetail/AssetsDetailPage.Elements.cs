using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Utils;
using System.Linq;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Pathway.Assets.AssetsDetail
{
    public partial class AssetsDetailPage : DetailsContentPage<AssetsDetailPage>
    {
        private static IQueryable<Row> MetaData;

        public AssetsDetailPage() : base()
        {
            // Sheet contains repository of Dashboard
            MetaData = AssetsPage.Instance.MetaData;
        }

        private Row _name
         => ExcelFactory.GetRow(MetaData, 1);
        protected Textbox Name_txt
            => new Textbox(_name);

        private Row _type
           => ExcelFactory.GetRow(MetaData, 2);
        protected DropdownList AssetType_ddl
            => new DropdownList(_type);

        private Row _assetGroup 
            => ExcelFactory.GetRow(MetaData, 3);
        protected DropdownList AssetGroup_ddl
            => new DropdownList(_assetGroup);

        private Row _length
          => ExcelFactory.GetRow(MetaData, 4);
        protected Textbox Length_txt
            => new Textbox(_length);

        private Row _width 
            => ExcelFactory.GetRow(MetaData, 5);
        protected Textbox Width_txt
            => new Textbox(_width);

        private Row _save 
            => ExcelFactory.GetRow(MetaData, 6);
        protected Button Save_btn 
            => new Button(_save);
    }

}
