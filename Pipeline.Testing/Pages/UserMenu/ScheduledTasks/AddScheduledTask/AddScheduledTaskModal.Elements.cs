using LinqToExcel;
using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Pipeline.Testing.Pages.UserMenu.ScheduledTasks.AddScheduledTask
{
    public partial class AddScheduledTaskModal : ScheduledTaskPage
    {
        public AddScheduledTaskModal() : base()
        {
        }

        private Row _modalTitle
            => ExcelFactory.GetRow(MetaData, 1);
        protected Label ModalTitle_lbl
            => new Label(_modalTitle);

        private Row _task
           => ExcelFactory.GetRow(MetaData, 2);
        protected DropdownList Task_ddl
            => new DropdownList(_task);

        private Row _descriptiption
            => ExcelFactory.GetRow(MetaData, 3);
        protected Label Desription_lbl
            => new Label(_descriptiption);

        private Row _community
            => ExcelFactory.GetRow(MetaData, 4);
        protected DropdownList Community_ddl
            => new DropdownList(_community);

        private Row _house
            => ExcelFactory.GetRow(MetaData, 5);
        protected DropdownList House_ddl
            => new DropdownList(_house);

        private Row _date
           => ExcelFactory.GetRow(MetaData, 6);
        protected Textbox Date_txt
            => new Textbox(_date);

        private Row _time
           => ExcelFactory.GetRow(MetaData, 7);
        protected Textbox Time_txt
            => new Textbox(_time);

        private Row _frequency
            => ExcelFactory.GetRow(MetaData, 8);
        protected DropdownList Frequency_ddl
            => new DropdownList(_frequency);

        private Row _active
            => ExcelFactory.GetRow(MetaData, 9);
        protected CheckBox Active_chk
            => new CheckBox(_active);

        private Row _save
            => ExcelFactory.GetRow(MetaData, 10);
        protected Button Save_btn
            => new Button(_save);

        private Row _close
          => ExcelFactory.GetRow(MetaData, 11);
        protected Button Close_btn
            => new Button(_close);
    }

}
