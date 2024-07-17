namespace Pipeline.Testing.Pages.Assets.OptionRooms.AddOptionRoom
{
    public partial class AddOptionRoomModal
    {
        public AddOptionRoomModal EnterOptionRoomName(string data)
        {
            if (!string.IsNullOrEmpty(data))
                OptionRoomName_txt.SetText(data);
            return this;
        }

        public AddOptionRoomModal EnterSortOrder(string data)
        {
            if (!string.IsNullOrEmpty(data))
                SortOrder_txt.SetText(data);
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
            WaitGridLoad();
        }

        public void AddOptionRoom(OptionRoomData OptionRoom)
        {
            EnterOptionRoomName(OptionRoom.Name)
                .EnterSortOrder(OptionRoom.SortOrder)
                .Save();
        }
    }
}
