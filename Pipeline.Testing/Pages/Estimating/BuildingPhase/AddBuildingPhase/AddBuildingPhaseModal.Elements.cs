using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Linq;

namespace Pipeline.Testing.Pages.Estimating.BuildingPhase.AddBuildingPhase
{
    public partial class AddBuildingPhaseModal : BuildingPhasePage
    {
        public AddBuildingPhaseModal() : base()
        {
        }

        private Row _modalTitle
            => ExcelFactory.GetRow(MetaData, 1);
        protected Label ModalTitle_lbl
            => new Label(_modalTitle);

        private Row _code
            => ExcelFactory.GetRow(MetaData, 2);
        protected Textbox PhaseCode_txt
            => new Textbox(_code);

        private Row _name
            => ExcelFactory.GetRow(MetaData, 3);
        protected Textbox PhaseName_txt
            => new Textbox(_name);

        private Row _abbName
            => ExcelFactory.GetRow(MetaData, 4);
        protected Textbox PhaseAbbName_txt
            => new Textbox(_abbName);

        private Row _description
            => ExcelFactory.GetRow(MetaData, 5);
        protected Textbox PhaseDescription_txt
            => new Textbox(_description);

        private Row _buildingGroup
            => ExcelFactory.GetRow(MetaData, 6);
        protected DropdownList PhaseBuildingGroup_ddl
            => new DropdownList(_buildingGroup);

        private Row _type
            => ExcelFactory.GetRow(MetaData, 7);
        protected DropdownList PhaseType_ddl
            => new DropdownList(_type);

        private Row _parents
            => ExcelFactory.GetRow(MetaData, 8);
        protected DropdownList PhaseParents_ddl
            => new DropdownList(_parents);

        private Row _percentBill
            => ExcelFactory.GetRow(MetaData, 9);
        protected Textbox PhasePercentBill_txt
            => new Textbox(_percentBill);

        private Row _taxable
            => ExcelFactory.GetRow(MetaData, 10);
        protected CheckBox PhaseTaxable_chk
            => new CheckBox(_taxable);
        private Row _save
            => ExcelFactory.GetRow(MetaData, 11);
        protected Button PhaseSave_btn
            => new Button(_save);

        private Row _close
            => ExcelFactory.GetRow(MetaData, 12);
        protected Button PhaseClose_btn
           => new Button(_close);

        private Row _taskForPayment => ExcelFactory.GetRow(MetaData, 16);
        protected DropdownList SchedulingTaskForPayment_ddl => new DropdownList(_taskForPayment);

        private Row _poDisplay => ExcelFactory.GetRow(MetaData, 17);
        protected DropdownList TaskForPODisplay_ddl => new DropdownList(_poDisplay);

        private Row _taxableYes => ExcelFactory.GetRow(MetaData, 18);
        protected RadioButton TaxableYes_rbtn => new RadioButton(_taxableYes);

        private Row _taxableNo => ExcelFactory.GetRow(MetaData, 19);
        protected RadioButton TaxableNo_rbtn => new RadioButton(_taxableNo);
    }

}
