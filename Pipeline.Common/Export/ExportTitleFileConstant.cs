namespace Pipeline.Common.Export
{
    public static class ExportTitleFileConstant
    {
        public const string OPTION_GROUP_TITLE = "Name,Order";
        public const string OPTION_TITLE = "Level (M or G),Number,Name,Description,Sales Description,Elevation,Allow Multiples,Option Group,Option Room,Option Type,Cost Group";
        public const string COMMUNITY_TITLE = "Community Name,Division,Community Code,Community City,City Hyperlink,Community Township,Community County,Community State,Community Zip,Community School District,Community School District Link,Community Info,Community Planner,Community Latitude,Community Longitude,Community Status,Community Slug";
        public const string USE_TITLE = "Name,Description,Sort Order";
        public const string CATEGORY_TITLE = "Name,Parent Name";
        public const string BUILDINGGROUPS_TITLE = "Building Group Code,Building Group Name,Description";
        public const string BOMPHASERULES_TITLE = "Original Parent Building Phase Code,Original Parent Building Phase Name,Original Child Building Phase Code,Original Child Building Phase Name,New Child Building Phase Code,New Child Building Phase Name";
        public const string SPECSETGROUP_TITLE = "Spec Set Group,Default Group,Spec Set Default";
        public const string SPECSETPRODUCTGROUP_TITLE = "Original Building Phase,Original Product Name,Original Product Style,Original Product Code,Original Product Description,Original Use,Spec Set Group,Spec Set,New Building Phase,New Product Name,New Product Style,New Product Code,New Product Description,New Use,Calculation";
        public const string PRODUCT = "Building Phase Code,Product Name,Category Name,Description,SKU,Notes,Manufacturer,Style,Code,Unit,Waste,Rounding Unit,Rounding Direction,Default Building Phase,Tax Status,Supplemental to Bid,Projected Cost,Reverse Product";
        public const string PRODUCT_TO_CATEGORY_TITLE = "Category Name,Product Name";
        public const string JOBBOM_TITLE = "Community Code,Community Name,Plan Number,Plan Name,Job Number,Option Number,Option Description,Building Phase Code,Building Phase Name,Product Code,Product Name,Product Description,Product Style,Waste,Rounding,Accuracy,Product Unit,Product Unit Cost,Product Cost,Product Qty,Waste Qty,Rounding Qty,Total Qty";
        public const string HOUSEQUANTITESWITHPARAMETER_TITLE = "Plan Number,House Name,Community Code,Community Name,Option Name,Option Condition,Product Name,Product Quantity,Use,Product Unit,Building Phase Code,Parameter,Style Name,Manufacturer";
        public const string HOUSEBOMPRODUCT_TITLE = "Community,Community Code,House,Plan No,Option,Option Number,Dependent Condition,Option Description,Building Phase,Product Code,Product,Style,Description,Supplemental,Waste,Round,Accuracy,Unit,Product Qty,Waste Qty,Rounding Qty,Total Qty,Use";
        public const string HOUSEBOMPRODUCTWITHPARAMETER_TITLE = "Community,Community Code,House,Plan No,Option,Option Number,Dependent Condition,Option Description,Building Phase,SWG,Product Code,Product,Style,Description,Supplemental,Waste,Round,Accuracy,Unit,Product Qty,Waste Qty,Rounding Qty,Total Qty,Use";
        public const string TRADE_TO_PHASE_TITLE = "Trade Name,Trade Code,Building Phase Name,Building Phase Code";
        public const string TRADE_TO_VENDOR_TITLE = "Trade Name,Trade Code,Vendor Name,Vendor Code";
        public const string TRADE_TO_SCHEDULING_TITLE = "Trade Name,Trade Code,Scheduling Task Code,Scheduling Task Name";
        public const string FUTUREBIDCOST_OPTION = "Bid Cost Id,Vendor Code,Vendor Name,Option Code,Option Name,Building Phase Code,Building Phase Name,Dependent Condition,Future Bid Cost,Future Bid Effective Date";
        public const string FUTUREBIDCOST_HOUSE = "Bid Cost Id,Vendor Code,Vendor Name,House Plan No,House Name,Option Code,Option Name,Building Phase Code,Building Phase Name,Dependent Condition,Future Bid Cost,Future Bid Effective Date";
        public const string FUTUREBIDCOST_COMMUNITY = "Bid Cost Id,Vendor Code,Vendor Name,Communities Name,Communities Code,Option Code,Option Name,Building Phase Code,Building Phase Name,Dependent Condition,Future Bid Cost,Future Bid Effective Date";
        public const string FUTUREBIDCOST_COMMUNITYOPTIONHOUSE = "Bid Cost Id,Vendor Code,Vendor Name,Communities Name,Communities Code,House Plan No,House Name,Option Code,Option Name,Building Phase Code,Building Phase Name,Dependent Condition,Future Bid Cost,Future Bid Effective Date";
        public const string VENDOR_OPTION_BIDCOST_HOUSE = "Row ID,House Name,House Plan No,Vendor Code,Vendor Name,Option Name,Option Code,Building Phase Code,Building Phase Name,Bid Cost,Dependent Condition";
        public const string VENDOR_BASE_COSTS = "Vendor Code,Vendor Name,Product Name,Product Description,Manufacturer Name,Style Name,Product Code,Material Cost,Labor Cost";
        public const string COST_CODES = "Cost Code Name,Cost Code Description";
        public const string VENDOR = "Vendor Id,Name,Code,Trade,Contact,Email,Address 1,Address 2,Address 3,City,State,Zip,Phone,Phone 2,Mobile,URL,Fax";
        public const string VENDORS_TO_BUILDINGPHASES = "Vendor Code,Vendor Name,Building Phase Code,Building Phase Name";
        public const string VENDORS_TO_Community_Overrides = "Community Code,Community Name,Vendor Code,Vendor Name,Product Name,Product Description,Manufacturer Name,Style Name,Product Code,Material Cost,Labor Cost";

        public const string COMMUNITY_HOUSEBOMPRODUCT_TITLE = "Community,Community Code,House,Plan No,Option,Option Number,Dependent Condition,Option Description,Building Phase,Product,Style,Description,Supplemental,Waste,Round,Accuracy,Unit,Product Qty,Waste Qty,Rounding Qty,Total Qty,Use";
        public const string COMMUNITY_HOUSEBOMPRODUCTWITHPARAMETER_TITLE = "Community,Community Code,House,Plan No,Option,Option Number,Dependent Condition,Option Description,Building Phase,SWG,Product,Style,Description,Supplemental,Waste,Round,Accuracy,Unit,Product Qty,Waste Qty,Rounding Qty,Total Qty,Use";
        public const string VENDORS_TITLE = "Vendor Id,Name,Code,Trade,Contact,Email,Address 1,Address 2,Address 3,City,State,Zip,Phone,Phone 2,Mobile,URL,Fax";
        public const string PRODUCTCONVERSION_TITLE = "Original Building Phase,Original Product Name,Original Product Style,Original Product Code,Original Product Description,Original Use,Spec Set Group,Spec Set,New Building Phase,New Product Name,New Product Style,New Product Code,New Product Description,New Use,Calculation";
        public const string TRADES_TITLE = "Trade Name,Trade Code,Trade Description";
        public const string WORKSHEET_PRODUCTS_TITLE = "Worksheet Name,Worksheet Code,Building Phase Code,Product Name,Product Code,Product Description,Quantity,Use,Unit";

        public const string HOUSE_ESTIMATES_TITLE = "Community Name,Community Code,Plan Name,Plan Number,Option Name,Dependent Condition,Option Description,Building Phase Name,Vendor Name,Vendor Code,Product Name,Supplemental,Product Style,Product Quantity,Product Waste,Product Rounding,Product Total Quantity,Product Unit,Description,Unit Price,Unit Total,Supplement Bid Total,Bid Cost,Phase Total,Option Total";
        public const string JOB_ESTIMATES_TITLE = "Community Name,Community Code,Plan Name,Plan Number,Job Number,Option Name,Option Number,Option Description,Building Phase Name,Building Phase Code,Vendor Name,Vendor Code,Product Code,Product Name,Product Description,Product Style,Product Quantity,Product Waste,Product Rounding,Product Total Quantity,Product Unit,Material Cost,Labor Cost,Total Cost";
        public const string JOB_QUANTITIES_FLAT_TITLE = "Job Code,House Code,House Name,Community Code,Community Name,Lot Code,Option Name,Option Code,Option Description,Product Name,Product Code,Product Description,Product Quantity,Product Use,Product Unit,Product Waste,Style Name,Manufacturer,Building Phase Code,Source Name";
        public const string JOB_QUANTITIES_NORMAL_TITLE = "Job Code,House Code,House Name,Community Code,Community Name,Lot Code";
    }
}
