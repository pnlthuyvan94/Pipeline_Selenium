using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Assets.Communities.MoveInReadyHomes.MoveInReadyResource;

namespace Pipeline.Testing.Pages.Assets.Communities.MoveInReadyHomes
{
    public partial class MoveInReadyHomesPage
    {
        public MoveInReadyHomesPage PressAddNewMoveInReadyHome()
        {
            AddMoveInReadyHomes_btn.Click();
            return this;
        }

        public MoveInReadyHomesPage SelectHouse(string house)
        {
            if (!string.IsNullOrEmpty(house))
                HouseBeforeSaving_ddl.SelectItem(house, true);
            return this;
        }

        public MoveInReadyHomesPage SelecLot(string lot)
        {
            if (!string.IsNullOrEmpty(lot))
                Lot_ddl.SelectItem(lot, true);
            return this;
        }

        public MoveInReadyHomesPage SelectStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
                Status_ddl.SelectItem(status, true);
            return this;
        }

        public MoveInReadyHomesPage EnterPrice(string price)
        {
            if (!string.IsNullOrEmpty(price))
                Price_txt.SetText(price);
            return this;
        }

        public MoveInReadyHomesPage EnterAddress(string address)
        {
            if (!string.IsNullOrEmpty(address))
                Address_txt.SetText(address);
            return this;
        }

        public MoveInReadyHomesPage EnterBasement(string basement)
        {
            if (!string.IsNullOrEmpty(basement))
                Basement_txt.SetText(basement);
            return this;
        }

        public MoveInReadyHomesPage EnterFirstFloor(string firstFloor)
        {
            if (!string.IsNullOrEmpty(firstFloor))
                FistFloor_txt.SetText(firstFloor);
            return this;
        }

        public MoveInReadyHomesPage EnterSecondFloor(string secondFloor)
        {
            if (!string.IsNullOrEmpty(secondFloor))
                SecondFloor_txt.SetText(secondFloor);
            return this;
        }

        public MoveInReadyHomesPage EnterHeat(string heat)
        {
            if (!string.IsNullOrEmpty(heat))
                Heated_txt.SetText(heat);
            return this;
        }

        public MoveInReadyHomesPage EnterTotal(string total)
        {
            if (!string.IsNullOrEmpty(total))
                Total_txt.SetText(total);
            return this;
        }

        public MoveInReadyHomesPage SelectStyle(string style)
        {
            if (!string.IsNullOrEmpty(style))
                Style_ddl.SelectItem(style, true);
            return this;
        }

        public MoveInReadyHomesPage SelectStory(string story)
        {
            if (!string.IsNullOrEmpty(story))
                Story_ddl.SelectItem(story, true);
            return this;
        }

        public MoveInReadyHomesPage SelectBathroom(string bathroom)
        {
            if (!string.IsNullOrEmpty(bathroom))
                Bathroom_ddl.SelectItem(bathroom, true);
            return this;
        }

        public MoveInReadyHomesPage SelectBedroom(string bedroom)
        {
            if (!string.IsNullOrEmpty(bedroom))
                Bedroom_ddl.SelectItem(bedroom, true);
            return this;
        }

        public MoveInReadyHomesPage SelectGarage(string garage)
        {
            if (!string.IsNullOrEmpty(garage))
                Garage_ddl.SelectItem(garage, true);
            return this;
        }

        public MoveInReadyHomesPage EnterNote(string note)
        {
            if (!string.IsNullOrEmpty(note))
                Note_txt.SetText(note);
            return this;
        }

        public MoveInReadyHomesPage IsModalHome(string isSelect)
        {
            if (isSelect.ToLower() == "true")
                IsModalHome_btn.Click();
            return this;
        }

        public void Save()
        {
            SaveMoveInReadyHomes_btn.Click();
        }

        public MoveInReadyHomesPage AddNewMoveInReadyHome(MoveInReadyHomesData data)
        {
            SelectHouse(data.House).SelecLot(data.Lot).SelectStatus(data.Status).EnterPrice(data.Price)
                .EnterAddress(data.Address).EnterBasement(data.Basement).EnterFirstFloor(data.FirstFloor).EnterSecondFloor(data.SecondFloor)
                .EnterHeat(data.Heated).EnterTotal(data.Total).SelectStyle(data.Style).SelectStory(data.Story).SelectBedroom(data.Bedroom).SelectBathroom(data.Bathroom).SelectGarage(data.Garage)
                .EnterNote(data.Note).IsModalHome(data.IsModalHome)
                .Save();
            MoveInReadyResource = new MoveInReadyResourceGrid();

            // Loading grid
            MoveInReadyHomePage_Grid.WaitGridLoad();

            // Reloading page after save data
            PageLoad();
            return this;
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            MoveInReadyHomePage_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            MoveInReadyHomePage_Grid.WaitGridLoad();
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            MoveInReadyHomePage_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            MoveInReadyHomePage_Grid.WaitGridLoad();
        }

        public void OpenItemInGrid(string columnName, string value)
        {
            MoveInReadyHomePage_Grid.ClickItemInGridWithTextContains(columnName, value);
            PageLoad();
        }

        public void WaitGridLoad()
        {
            MoveInReadyHomePage_Grid.WaitGridLoad();
        }
    }
}
