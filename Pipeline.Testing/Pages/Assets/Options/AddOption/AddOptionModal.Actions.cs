using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Assets.Options.AddOption
{
    public partial class AddOptionModal
    {
        public AddOptionModal EnterOptionName(string data)
        {
            if (!string.IsNullOrEmpty(data))
                OptionName_txt.SetText(data);
            return this;
        }

        public AddOptionModal EnterOptionNumber(string data)
        {
            if (!string.IsNullOrEmpty(data))
                OptionNumber_txt.SetText(data);
            return this;
        }

        public AddOptionModal EnterOptionSquareFootage(string data)
        {
            if (!string.IsNullOrEmpty(data))
                SquareFootage_txt.SetText(data);
            return this;
        }

        public AddOptionModal EnterPrice(string data)
        {
            if (!string.IsNullOrEmpty(data))
                Price_txt.SetText(data);
            return this;
        }

        public AddOptionModal EnterOptionDescription(string data)
        {
            if (!string.IsNullOrEmpty(data))
                Description_txt.SetText(data);
            return this;
        }
        public AddOptionModal EnterOptionSaleDescription(string data)
        {
            if (!string.IsNullOrEmpty(data))
                SaleDescription_txt.SetText(data);
            return this;
        }

        public string EnterOptionGroup(string data)
        {
            return OptionGroup_ddl.SelectItemByValueOrIndex(data, 1);

        }

        public AddOptionModal EnterOptionRoom(string data)
        {
            if (!string.IsNullOrEmpty(data))
                OptionRoom_ddl.SelectItem(data, true);
            return this;
        }

        public string EnterCostGroup(string data)
        {
            return CostGroup_ddl.SelectItemByValueOrIndex(data, 0);
        }

        public string EnterOptionType(string data)
        {
            return OptionType_ddl.SelectItemByValueOrIndex(data, 0);
        }

        public AddOptionModal SelectTypeOfOption(bool elevation, bool allowMultiples, bool global)
        {
            Type.AllowMultiples.SetCheck(allowMultiples);
            Type.Elevation.SetCheck(elevation);
            Type.Global.SetCheck(global);
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
        }


        public OptionData AddOption(OptionData option)
        {
            EnterOptionName(option.Name)
            .EnterOptionNumber(option.Number)
            .EnterOptionSquareFootage(option.SquareFootage.ToString())
            .EnterOptionDescription(option.Description)
            .EnterOptionSaleDescription(option.SaleDescription);
            option.OptionGroup = EnterOptionGroup(option.OptionGroup);
                EnterOptionGroup(option.OptionGroup);
                //.EnterOptionRoom(Option.OptionRoom)
                option.CostGroup = EnterCostGroup(option.CostGroup);
                option.OptionType = EnterOptionType(option.OptionType);
                EnterPrice(option.Price.ToString())
                .SelectTypeOfOption(option.Types[0], option.Types[1], option.Types[2])
                .Save();

            WaitGridLoad();
            OptionData newoption = new OptionData(option)
            {
                OptionGroup = option.OptionGroup,
                CostGroup = option.CostGroup,
                OptionType = option.OptionType
            };

            return newoption;
        }
    }
}
