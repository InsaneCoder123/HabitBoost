namespace User
{
    interface IWriteData
    {
        void WriteData();
    }
    interface IReadData
    {
        void ReadData();
    }
    public class Game
    {
        public int Level { get; set; }
        public double Experience { get; set; }
    }
    public class UserData : IWriteData, IReadData
    {
        public int Id { get; set; }
        public Game UserStats { get; set; }
        public UserDataList UserDataList { get; set; }

        public UserData() 
        {
            UserStats = new Game();
            UserDataList = new UserDataList();
        }
        public void WriteData()
        {
        }
        public void ReadData()
        {
        }
    }
}
