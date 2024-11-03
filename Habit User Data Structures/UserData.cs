using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habit_User_Data_Structures
{

    public class UserData
    {
        public string? Username { get; set; }
        public string? Password { get; set; }

        public int Level { get; set; }
        public int Experience { get; set; }

        public List<Habit> HabitList { get; set; } = [];
        public List<JournalEntry> JournalList { get; set; } = [];
        public List<Task> TaskList { get; set; } = [];
        public List<Achievement> AchievementList { get; set; } = [];

        #region File Handling
        public void WriteData(string DataFolder)
        {
            try
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
                                foreach (Habit habit in HabitList)
                                {
                                    try
                                    {
                                        string[] habitData =
                                        {
                                    habit.ID,
                                    habit.Name,
                                    habit.Difficulty.ToString(),
                                    habit.Experience.ToString(),
                                    habit.Completed.ToString()
                                };

                                        string habitFileName = habit.DateCreated.ToString("ddMMyyyy") + habit.ID.PadLeft(3, '0') + ".txt";
                                        string habitFilePath = Path.Combine(userContentFolder, habitFileName);

                                        File.WriteAllLines(habitFilePath, habitData);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error writing habit file for ID {habit.ID}: {ex.Message}");
                                    }
                                }
                            }
                            else if (userContentFolderName == "Journal")
                            {
                                foreach (JournalEntry entry in JournalList)
                                {
                                    try
                                    {
                                        string[] journalData = { entry.ID, entry.Name, entry.Entry };
                                        string journalFileName = entry.DateCreated.ToString("ddMMyyyy") + entry.ID.PadLeft(3, '0') + ".txt";
                                        string journalFilePath = Path.Combine(userContentFolder, journalFileName);

                                        File.WriteAllLines(journalFilePath, journalData);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error writing journal file for ID {entry.ID}: {ex.Message}");
                                    }
                                }
                            }
                            else if (userContentFolderName == "TODO")
                            {
                                foreach (Task task in TaskList)
                                {
                                    try
                                    {
                                        string[] taskData =
                                        {
                                            task.ID,
                                            task.Name,
                                            task.Difficulty.ToString(),
                                            task.Experience.ToString(),
                                            task.Completed.ToString(),
                                            task.DateDue.ToString()
                                        };

                                        string taskFileName = task.DateCreated.ToString("ddMMyyyy") + task.ID.PadLeft(3, '0') + ".txt";
                                        string taskFilePath = Path.Combine(userContentFolder, taskFileName);

                                        File.WriteAllLines(taskFilePath, taskData);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error writing task file for ID {task.ID}: {ex.Message}");
                                    }
                                }
                            }
                            else if (userContentFolderName == "Achievements")
                            {
                                // Handle "Achievements" folder when implemented (if there is time)
                            }
                        }
                    }
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"Directory not found: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Access denied: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        public void ReadData(string DataFolder)
        {
            try
            {
                Username = Path.GetFileName(DataFolder);
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
                                    try
                                    {
                                        string[] habitLines = File.ReadAllLines(habitFile);
                                        if (habitLines.Length < 5) throw new Exception("Habit file has insufficient data.");

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
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error reading habit file '{habitFile}': {ex.Message}");
                                    }
                                }
                            }
                            else if (userContentFolderName == "Journal")
                            {
                                foreach (string journalFile in Directory.GetFiles(userContentFolder))
                                {
                                    try
                                    {
                                        string[] journalFiles = File.ReadAllLines(journalFile);
                                        if (journalFiles.Length < 3) throw new Exception("Journal file has insufficient data.");

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
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error reading journal file '{journalFile}': {ex.Message}");
                                    }
                                }
                            }
                            else if (userContentFolderName == "TODO")
                            {
                                foreach (string taskFile in Directory.GetFiles(userContentFolder))
                                {
                                    try
                                    {
                                        string[] taskFiles = File.ReadAllLines(taskFile);
                                        if (taskFiles.Length < 6) throw new Exception("Task file has insufficient data.");

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
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Error reading task file '{taskFile}': {ex.Message}");
                                    }
                                }
                            }
                            else if (userContentFolderName == "Achievements")
                            {
                                // Handle "Achievements" folder when implemented
                            }
                        }
                    }
                    else if (userFolderName == "UserData")
                    {
                        try
                        {
                            string[] dataLines = File.ReadAllLines(Path.Combine(userFolder, "datas.txt"));
                            if (dataLines.Length < 2) throw new Exception("UserData file has insufficient data.");

                            Level = Convert.ToInt32(dataLines[0]);
                            Experience = Convert.ToInt32(dataLines[1]);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error reading UserData file in '{userFolder}': {ex.Message}");
                        }
                    }
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"Directory not found: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Access denied: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
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
        #endregion

        public static string FindFreeID(List<IIdentifiable> targetData)
        {
            for (int number = 1; number < 1000; number++)
            {
                string currentNumber = number.ToString().PadLeft(3, '0');
                bool idExists = targetData.Any(data => data.ID == currentNumber);

                if (!idExists)
                {
                    return currentNumber;
                }
            }
            return "ERROR";
        }

        public void AddHabit()
        {
            Habit habit = new()
            {
                ID = FindFreeID(HabitList.Cast<IIdentifiable>().ToList()),
                Name = "New Habit",
                Experience = 3,
                Completed = false,
                Difficulty = HabitBoostDifficulty.Easy,
                DateCreated = DateTime.Now
            };
            HabitList.Add(habit);
        }

        public void AddJournalEntry()
        {
            JournalEntry entry = new()
            {
                ID = FindFreeID(JournalList.Cast<IIdentifiable>().ToList()),
                Name = "New Journal Entry",
                Entry = "Oh yeah test test test test",
                DateCreated = DateTime.Now
            };
            JournalList.Add(entry);
        }

        public void AddTask()
        {
            Task task = new()
            {
                ID = FindFreeID(TaskList.Cast<IIdentifiable>().ToList()),
                Name = "New Task",
                Experience = 5,
                Completed = false,
                Difficulty = HabitBoostDifficulty.Medium,
                DateCreated = DateTime.Now,
                DateDue = DateTime.Now.AddDays(7)
            };
            TaskList.Add(task);
        }

    }
}
