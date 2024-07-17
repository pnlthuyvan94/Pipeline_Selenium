using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Assets.Options
{
    public class OptionData
    {
        public OptionData()
        {
            Name = string.Empty;
            Number = string.Empty;
            SquareFootage = 0.00;
            Description = string.Empty;
            SaleDescription = string.Empty;
            Price = 0.00;
        }
        public string Name { get; set; }
        public string Number { get; set; }
        public double SquareFootage { get; set; }
        public string Description { get; set; }
        public string SaleDescription { get; set; }

        private string optionGroup;
        public string OptionGroup
        {
            get
            {
                if (string.IsNullOrEmpty(optionGroup))
                    return "NONE";
                return optionGroup;

            }
            set
            {
                optionGroup = value;
            }
        }
        private string optionRoom;
        public string OptionRoom
        {
            get
            {
                if (string.IsNullOrEmpty(optionRoom))
                    return "NONE";
                return optionRoom;
            }
            set
            {
                optionRoom = value;
            }
        }

        private string costGroup;
        public string CostGroup
        {
            get
            {
                if (string.IsNullOrEmpty(costGroup))
                    return "NONE";
                return costGroup;
            }
            set
            {
                costGroup = value;
            }
        }

        private string optionType;
        public string OptionType
        {
            get
            {
                if (string.IsNullOrEmpty(optionType))
                    return "NONE";
                return optionType;
            }
            set
            {
                optionType = value;
            }
        }
        public double Price { get; set; }
        public IList<bool> Types { get; set; }
        public OptionData(OptionData optionData)
        {
            Name = optionData.Name;
            Number = optionData.Number;
            SquareFootage = optionData.SquareFootage;
            Description = optionData.Description;
            SaleDescription = optionData.SaleDescription;
            OptionGroup = optionData.optionGroup;
            OptionRoom = optionData.optionGroup;
            CostGroup = optionData.costGroup;
            OptionType = optionData.optionType;
            Price = optionData.Price;
            Types = optionData.Types;
        }
    }

}