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
        private string Password { get; set; }
        public Game UserStats { get; set; }
        public UserDataListType UserDataList { get; set; }

        public UserData() 
        {
            UserStats = new Game();
            UserDataList = new UserDataListType();
        }
        public void WriteData()
        {
            // Writes user data (user stats) to a file in a folder named "UserData" contained within that User's folder
            // Data
            // - UserID
            //          - UserData
            //                      - UserStats.txt
            //                      - Journal
            //                        - 1.txt
            //                        - 2.txt
            //                        - 3.txt
            //                      - Habit
            //                        - 1.txt
            //                        - 2.txt
            //                        - 3.txt
            //                      - Todo
            //                        - 1.txt
            //                        - 2.txt
            //                        - 3.txt
        }
        public void ReadData()
        {
            // Read user data (user stats) to a file in a folder named "UserData" contained within that User's folder
            // Then syncs that data obtained to the UserStats and Userdatalist
            // - UserID
            //          - UserData
            //                      - UserStats.txt
            //                      - Journal
            //                        - 1.txt
            //                        - 2.txt
            //                        - 3.txt
            //                      - Habit
            //                        - 1.txt
            //                        - 2.txt
            //                        - 3.txt
            //                      - Todo
            //                        - 1.txt
            //                        - 2.txt
            //                        - 3.txt
        }
    }
}
