namespace Pipeline.Common.Constants
{
    public class BaseMenuUrls
    {
        //---------------------------- Dashboard menu -----------------------------
        public const string VIEW_HOUSE_URL_FROM_DASHBOARD = "/Builder/Houses/";
        public const string VIEW_COMMUNITY_URL_FROM_DASHBOARD = "/Builder/Communities/";
        public const string VIEW_OPTION_URL_FROM_DASHBOARD = "/Builder/Options/";

        //---------------------------- Asset menu -----------------------------
        public const string VIEW_DIVISION_URL = "/Builder/Divisions/Default.aspx";
        public const string CREATE_NEW_DIVISION_URL = "/Builder/Divisions/Division.aspx?did=0";
        public const string VIEW_COMMUNITY_URL = "/Builder/Communities/Default.aspx";
        public const string CREATE_NEW_COMMUNITY_URL = "/Builder/Communities/Community.aspx?cid=0";
        public const string VIEW_SERIES_URL = "/Builder/Series/Default.aspx";
        public const string VIEW_HOUSE_URL = "/Builder/Houses/Default.aspx";
        public const string CREATE_NEW_HOUSE_URL = "/Builder/Houses/House.aspx?hid=0";
        public const string VIEW_OPTION_URL = "/Builder/Options/Default.aspx";
        public const string CREATE_NEW_OPTION_URL = "/Builder/Options/Default.aspx";
        public const string VIEW_OPTION_GROUP_URL = "/Builder/Options/OptionGroups.aspx";
        public const string VIEW_OPTION_TYPE_URL = "/Builder/Options/OptionTypes.aspx";
        public const string VIEW_CUSTOM_OPTION_URL = "/Builder/Options/CustomOptions.aspx";
        public const string CREATE_NEW_CUSTOM_OPTION_URL = "/Builder/Options/CustomOption.aspx?coid=0";

        //---------------------------- Estimating menu -----------------------------
        public const string VIEW_PRODUCT_URL = "/Products/Products/Default.aspx";
        public const string CREATE_NEW_PRODUCT_URL = "/Products/Products/Product.aspx?pid=0";
        public const string VIEW_BUILDING_GROUP_URL = "/Products/BuildingGroups/Default.aspx";
        public const string VIEW_BUILDING_PHASES_URL = "/Products/BuildingPhases/Default.aspx";
        public const string VIEW_QUANTITY_BUILDING_PHASE_RULES_URL = "/BuilderBom/PhaseRules/Default.aspx";
        public const string VIEW_BOM_BUILDING_PHASE_RULES_URL = "/BuilderBom/BOMPhaseRules/Default.aspx";
        public const string VIEW_BUILDING_PHASE_TYPES_URL = "/Products/BuildingPhases/Types.aspx";
        public const string VIEW_STYLES_URL = "/Products/Styles/Default.aspx";
        public const string CREATE_NEW_STYLE_URL = "/Products/Styles/Style.aspx?sid=0";
        public const string VIEW_STYLES_IMPORT_RULE_URL = "/BuilderBom/Transfers/Imports/StyleImportRules.aspx";
        public const string VIEW_MANUFACTURERS_URL = "/Products/Manufacturers/Default.aspx";
        public const string CREATE_NEW_MANUFACTURER_URL = "/Products/Manufacturers/Manufacturer.aspx?mid=0";
        public const string VIEW_USES_URL = "/Products/Uses/Default.aspx";
        public const string VIEW_UNITS_URL = "/Products/Products/Units.aspx";
        public const string VIEW_CATEGORIES_URL = "/Products/Categories/Default.aspx";
        public const string VIEW_CALCULATION_URL = "/ProductAssemblies/Calculations/Default.aspx";
        public const string VIEW_SPECSETS_URL = "/ProductAssemblies/SpecSets/Default.aspx";
        public const string VIEW_BOM_LOGIC_RULES_URL = "/ProductAssemblies/BOMLogicRules/Default.aspx";
        public const string VIEW_WORKSHEETS_URL = "/BuilderBom/Worksheets/Default.aspx";
        public const string CREATE_NEW_WORKSHEET_URL = "/BuilderBom/Worksheets/Details.aspx?wid=0";


        //---------------------------- Job menu -----------------------------
        public const string VIEW_JOB_URL = "/Jobs/Default.aspx?jobstatus=all";
        public const string CREATE_NEW_JOB_URL = "/Jobs/Job.aspx?jid=0";
        public const string VIEW_ACTIVE_JOB_URL = "/Jobs/Default.aspx?jobstatus=1";
        public const string VIEW_COMPLETED_JOB_URL = "/Jobs/Default.aspx?jobstatus=0";
        public const string VIEW_TRADE_URL = "/Jobs/JobDocuments/Default.aspx";

        //---------------------------- Costing menu -----------------------------
        public const string VIEW_VENDORS_URL = "/Costing/Vendors/Default.aspx";
        public const string CREATE_NEW_VENDOR_URL = "/Costing/Vendors/Vendor.aspx?vid=0";
        public const string VIEW_COMMUNITY_SALES_TAX_URL = "/Costing/Taxes/CommunityTaxes.aspx";
        public const string VIEW_TAX_GROUP_URL = "/Costing/Taxes/TaxGroups.aspx";
        public const string VIEW_COST_COMPARISON_URL = "/Costing/Products/CostComparisons.aspx";
        public const string VIEW_OPTION_BID_COST_URL = "/Costing/Costs/VendorOptionBidCosts.aspx";
        public const string VIEW_COST_ESTIMATE_URL = "/Costing/CostingEstimates/CostingEstimates.aspx";

        //---------------------------- Purchasing menu -----------------------------
        public const string VIEW_MANAGE_ALL_PO_URL = "/Purchasing/PurchaseOrder/AllPurchaseOrders.aspx";
        public const string VIEW_WORK_COMPLETED_URL = "/Purchasing/PurchaseOrder/ApprovedPurchaseOrderPayment.aspx";
        public const string VIEW_APPROVE_FOR_PAYMENT_URL = "/Purchasing/PurchaseOrder/ApprovedPurchaseOrders.aspx";
        public const string VIEW_TRADES_URL = "/Purchasing/Trades/Default.aspx";
        public const string VIEW_PURCHASING_BUILDING_PHASES_URL = "/Purchasing/BuildingPhases/Default.aspx";
        public const string VIEW_RELEASE_GROUPS_URL = "/Purchasing/ReleaseGroups/ReleaseGroups.aspx";
        public const string VIEW_COST_CODES_URL = "/Purchasing/CostCodes/CostCodes.aspx";
        public const string VIEW_COST_TYPES_URL = "/Purchasing/CostCodes/CostTypes.aspx";
        public const string VIEW_COST_CATEGORIES_URL = "/Purchasing/CostCodes/CostCategories.aspx";

        //---------------------------- Profile -----------------------------
        public const string PROFILE_URL = "/MyProfile.aspx";
        public const string SUBCRIPTIONS_URL = "/Subscriptions.aspx";
        public const string LICENSING_URL = "/UserLimitReached.aspx";
        public const string ADMIN_DASHBOARD_URL = "/Admin/";
        public const string ADMIN_USERS_URL = "/Admin/Users/Users.aspx";
        public const string ADMIN_CREATE_USER_URL = "/Admin/Users/User.aspx";
        public const string ADMIN_ROLES_URL = "/Admin/Users/Roles.aspx";
        public const string ADMIN_SETTINGS_URL = "/Admin/Settings/Default.aspx";
        public const string ADMIN_QUEUE_URL = "/Reports/Queue.aspx";
        public const string ADMIN_GENERATED_REPORTS_URL = "/Reports/GeneratedReports.aspx";
        public const string ADMIN_DOCUMENTS_URL = "/DocumentManager/DocumentTypes.aspx?p=1";
        public const string ADMIN_SCHEDULED_TASKS_URL = "/Tasks/Default.aspx";
        public const string ADMIN_SCHEDULED_TASKS_VIEW_TASK_LOG_URL = "/Tasks/EventLog.aspx";
        public const string PLAYBOOK_TEMPLATE_URL = "/Playbook.aspx";
        public const string VIEW_HELP = "help.strongtietech.com/pipeline/";

        //---------------------------- Reports -----------------------------
        public const string DASHBOARD_USER_ACTIVITY_LOG_URL = "/Admin/Reports/Activity.aspx";
        public const string DASHBOARD_USER_ACTIVITY_LOG_V3_URL = "/Admin/Reports/Activity_v3.aspx";
        public const string BUILDER_COMMUNITIES_BY_HOUSE_URL = "/Builder/Reports/CommunitiesByHouse.aspx";
        public const string BUILDER_HOUSES_BY_COMMUNITY_URL = "/Builder/Reports/HousesByCommunity.aspx";
        public const string PRODUCTS_ITEMS_BY_BUILDING_PHASE_URL = "/Products/Reports/BuildingPhaseProducts.aspx";
        public const string BOM_PRODUCTS_BY_HOUSE_AND_OPTION_URL = "/BuilderBom/Reports/OptionReport.aspx";
        public const string BOM_PRODUCT_QTYS_BY_HOUSES_AND_OPT_URL = "/BuilderBom/HouseBom/ProductQtysByOptionAndHouses.aspx";
        public const string BOM_OPTION_PRICE_MARGIN_ANALYSIS_URL = "/builderbom/Reports/OptionPriceMarginAnalysis.aspx";
        public const string BOM_HOUSE_BOM_STATUS_REPORT_URL = "/builderbom/Reports/HouseGenerationStats.aspx";
        public const string BOM_PL_WMS_BOM_ASSEMBLY_COMPARISON_URL = "/BuilderBom.Integrations.BuilderMT/Utilities/ViewPipelineToWMSBOMAssemblyComparison.aspx";
        public const string BOM_PL_WMS_BOM_ASSEMBLY_COMPARISON_AND_SYNC_URL = "/BuilderBom.Integrations.BuilderMT/Utilities/HouseBOMReportInSync.aspx";
        public const string BOM_TRACE_QTY_DOWN_URL = "/BuilderBom/Tracing/QuantityOnDown.aspx";
        public const string COSTING_CONTRACT_URL = "/Costing/Reports/ContractCosting.aspx";
        public const string COSTING_COST_BY_ELEVATION_URL = "/Costing/Reports/CostBySetNumber.aspx";
        public const string COSTING_COST_COMPARISON_BY_ELEVATION_URL = "/Costing/Reports/CostComparisonSetup.aspx";
        public const string COSTING_HOUSE_COST_AND_PRICE_BY_ELEVATION_URL = "/Costing/Reports/CostAndPriceByElevation.aspx";
        public const string COSTING_OPTION_COST_AND_PRICE_BY_HOUSE_URL = "/Costing/Reports/OptionCostAndPrice.aspx";
        public const string WMS_COSTING_OPTION_COST_URL = "/BuilderBom.Integrations.BuilderMT/Reports/ReportOptionCostData.aspx";
        public const string WMS_COSTING_OPTION_COST_BY_PHASE_URL = "/BuilderBom/Reports/OptionCostByPhase.aspx";
        public const string WMS_COSTING_OPTION_COST_COMPARISON_URL = "/BuilderBom/Reports/OptionCostComparison.aspx";
        public const string view = "/Purchasing/Reports/BudgetsReport.aspx";
        public const string HOMEFRONT_HOUSE_OPTIONS_URL = "/Integrations.HomeFront/Reports/HouseOptions.aspx";

        //---------------------------- Scheduling -----------------------------
        public const string ACTIVITY_ASSIGNMENTS_URL = "/Scheduling/Activity/PhaseAssignments.aspx";

        //---------------------------- Sales Pricing -----------------------------
        public const string OPTION_GROUP_RULES_URL = "/SalesPricing/OptionGroupRules.aspx";
        public const string OPTION_RULES_URL = "/SalesPricing/OptionRules.aspx";
        public const string OPTION_PRICING_URL = "/SalesPricing/OptionsPricing.aspx";
        public const string PENDING_FUTURE_PRICES_URL = "/SalesPricing/PendingFuturePrices.aspx";

        //---------------------------- Sales menu-----------------------------
        public const string VIEW_PROSPECTS_URL = "/Sales/Prospects/Default.aspx";
        public const string VIEW_SALE_CUSTOMER_URL = "/Sales/Customers/Default.aspx";
        public const string VIEW_SALE_JOBS_URL = "/Sales/Jobs/Default.aspx";
        public const string VIEW_SS_SALES_PRICES_URL = "/Sales.Integrations.SalesSimplicity/UpdateSalesPrices.aspx";
        public const string VIEW_CONTRACT_DOCUMENTS_URL = "/Sales/Contracts/Default.aspx";
        public const string CREATE_NEW_CONTRACT_URL = "/Sales/Contracts/Contract.aspx?ctrid=0";

        //---------------------------- Marketing -----------------------------
        public const string VIEW_ALL_NOTI = "/eHome/Houses/Default.aspx";

        //---------------------------- Pathway menu -----------------------------
        public const string VIEW_PATHWAY_HOUSES_RULES_URL = "/eHome/Houses/Default.aspx";
        public const string VIEW_PATHWAY_ASSETS_RULES_URL = "/eHome/Assets/Default.aspx";
        public const string CREATE_PATHWAY_ASSETS_URL = "/eHome/Assets/Assets.aspx?aid=0";
        public const string VIEW_PATHWAY_ASSET_GROUPS_RULES_URL = "/eHome/AssetGroups/Default.aspx";
        public const string VIEW_PATHWAY_DESIGNER_VIEWS_RULES_URL = "/eHome/Designer/Views.aspx";
        public const string VIEW_PATHWAY_DESIGNER_ELEMENTS_RULES_URL = "/eHome/Designer/Elements.aspx";

        //---------------------------- Import -----------------------------
        public const string COMMUNITY_IMPORT_URL = "/Builder/transfers/imports/builder.aspx?view=Community";
        public const string PRODUCTS_IMPORT_URL_VIEW_ATTRIBUTES = "/Products/Transfers/Imports/Products.aspx?view=ProductAttributes";
        public const string PRODUCTS_IMPORT_URL_VIEW_PRODUCT = "/Products/transfers/imports/products.aspx?view=Products";
        public const string PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE = "/Products/transfers/imports/products.aspx?bpid=0?view=BuildingGroupsAndPhases";
        public const string PRODUCTS_IMPORT_URL_VIEW_SPEC_SET = "/products/transfers/imports/products.aspx?view=SpecSets";


        public const string BUILDER_IMPORT_URL_VIEW_COMMUNITY = "/Builder/transfers/imports/builder.aspx?view=Community";
        public const string BUILDER_IMPORT_URL_VIEW_LOT = "/Builder/transfers/imports/builder.aspx?view=Lot";
        public const string BUILDER_IMPORT_URL_VIEW_HOUSE = "/Builder/transfers/imports/builder.aspx?view=House";
        public const string BUILDER_IMPORT_URL_VIEW_OPTION = "/Builder/transfers/imports/builder.aspx?oid=0&view=Option";
        public const string TRADES_IMPORT_URL = "/Purchasing/transfers/imports/purchasing.aspx?viewtype=trades";
        public const string COSTING_IMPORT_URL = "/Costing/transfers/imports/costing.aspx";
        public const string COSTING_IMPORT_URL_VIEW_IMPORTVENDORS = "/Costing/transfers/imports/importvendors.aspx";
        public const string BUILDERBOM_IMPORT_URL_WORKSHEET = "/BuilderBom/transfers/imports/worksheets.aspx";
        public const string BUILDERBOM_IMPORT_URL_CUSTOMOPTION_PRODUCT = "/BuilderBom/transfers/imports/customoptionproducts.aspx";

        public const string COSTING_IMPORT_URL_VIEW_VENDOR = "/Costing/transfers/imports/importvendors.aspx";

        public const string PURCHASING_IMPORT_URL = "/Purchasing/transfers/imports/purchasing.aspx";

    }
}
