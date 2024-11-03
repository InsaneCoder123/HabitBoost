using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habit_User_Data_Structures
{
    interface IWriteData
    {
        void WriteData(string DataFolder);
    }
    interface IReadData
    {
        void ReadData(string DataFolder);
    }

    public class UserData : IWriteData, IReadData
    {
        public string? Username { get; set; }
        public string? Password { get; set; }

        public int Level { get; set; }
        public double Experience { get; set; }

        public List<Habit>? HabitList { get; set; }
        public List<JournalEntry>? JournalList { get; set; }
        public List<Task>? TaskList { get; set; }
        public List<Achievement>? AchievementList { get; set; }

        public void WriteData(string DataFolder)
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

        public void ReadData(string DataFolder)
        {
            string[] userFolders = Directory.GetDirectories(DataFolder);

            foreach (string userFolder in userFolders)
            {
                string userFolderName = Path.GetFileName(userFolder);
                if (userFolderName == "UserContents")
                {
                    foreach (string userContentFolder in Directory.GetDirectories(userFolder))
                    {
                        string userContentFolderName = Path.GetFileName(userContentFolder);
                        if (userContentFolderName == "Habit")
                        {

                        }
                        else if (userContentFolderName == "Journal")
                        {

                        }
                        else if (userContentFolderName == "TODO")
                        {

                        }
                        else if (userContentFolderName == "Achievements")
                        {

                        }
                    }
                }
            }
        }

        public static void VerifySystemFolder(string DataFolder)
        {
            if (Directory.Exists(DataFolder))
            {
                return;
            }
            else
            {
                Directory.CreateDirectory(DataFolder);
            }
        }

    }
}
