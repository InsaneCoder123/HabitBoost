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
        public int Experience { get; set; }

        public List<Habit> HabitList { get; set; } = [];
        public List<JournalEntry> JournalList { get; set; } = [];
        public List<Task> TaskList { get; set; } = [];
        public List<Achievement> AchievementList { get; set; } = [];

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
                            foreach (string habitFile in Directory.GetFiles(userContentFolder))
                            {
                                string[] habitLines = File.ReadAllLines(habitFile);
                                string habitFileName = Path.GetFileName(habitFile);
                                Habit habit = new()
                                {
                                    ID = habitLines[0],
                                    Name = habitLines[1],
                                    Difficulty = (HabitBoostDifficulty)Enum.Parse(typeof(HabitBoostDifficulty), habitLines[2]),
                                    Experience = Convert.ToInt32(habitLines[3]),
                                    Completed = Convert.ToBoolean(habitLines[4]),
                                    DateCreated = Convert.ToDateTime(habitFileName[..2] + "-" + habitFileName[2..4] + "-" + habitFileName[4..8])
                                };
                                HabitList.Add(habit);
                            }
                        }
                        else if (userContentFolderName == "Journal")
                        {
                            foreach (string journalFile in Directory.GetFiles(userContentFolder))
                            {
                                string[] journalFiles = File.ReadAllLines(journalFile);
                                string journalFileName = Path.GetFileName(journalFile);
                                JournalEntry entry = new()
                                {
                                    ID = journalFiles[0],
                                    Name = journalFiles[1],
                                    Entry = journalFiles[2],
                                    DateCreated = Convert.ToDateTime(journalFileName[..2] + "-" + journalFileName[2..4] + "-" + journalFileName[4..8])
                                };
                                JournalList.Add(entry);
                            }
                        }
                        else if (userContentFolderName == "TODO")
                        {
                            foreach (string taskFile in Directory.GetFiles(userContentFolder)) 
                            {
                                string[] taskFiles = File.ReadAllLines(taskFile);
                                string taskFileName = Path.GetFileName(taskFile);
                                Task task = new()
                                {
                                    ID = taskFiles[0],
                                    Name = taskFiles[1],
                                    Difficulty = (HabitBoostDifficulty)Enum.Parse(typeof(HabitBoostDifficulty), taskFiles[2]),
                                    Experience = Convert.ToInt32(taskFiles[3]),
                                    Completed = Convert.ToBoolean(taskFiles[4]),
                                    DateDue = Convert.ToDateTime(taskFiles[5]),
                                    DateCreated = Convert.ToDateTime(taskFileName[..2] + "-" + taskFileName[2..4] + "-" + taskFileName[4..8])
                                };
                                TaskList.Add(task);
                            }
                        }
                        else if (userContentFolderName == "Achievements")
                        {
                            // Final if there is time
                        }
                    }
                }
                else if (userFolderName == "UserData")
                {
                    // Add error handling later
                    string[] dataLines = File.ReadAllLines(userFolder + @"\datas.txt");
                    Level = Convert.ToInt32(dataLines[0]);
                    Experience = Convert.ToInt32(dataLines[1]);
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
