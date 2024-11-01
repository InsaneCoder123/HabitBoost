using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habit_User_Data_Structures
{
    interface IWriteData
    {
        void WriteData();
    }
    interface IReadData
    {
        void ReadData();
    }

    public class UserData : IWriteData, IReadData
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }

        public int Level { get; set; }
        public double Experience { get; set; }

        public required List<Habit> HabitList { get; set; }
        public required List<JournalEntry> JournalList { get; set; }
        public required List<Task> TaskList { get; set; }
        public List<Achievement>? AchievementList { get; set; }

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
        }
    }
}
