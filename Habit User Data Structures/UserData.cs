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
        public int tasksCompleted { get; set; }
        public int habitsCompleted { get; set; }

        public List<Habit> HabitList { get; set; } = [];
        public List<JournalEntry> JournalList { get; set; } = [];
        public List<Task> TaskList { get; set; } = [];
        public List<Achievement> AchievementList { get; set; } = [];

        #region File Handling

        public void DoesFolderExist(string FolderName)
        {
            if (Directory.Exists(FolderName))
            {
                return;
            }
            else
            {
                Directory.CreateDirectory(FolderName);
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
                    DoesFolderExist(Path.Combine(userFolder, "UserContents"));
                    string userFolderName = Path.GetFileName(userFolder);
                    if (userFolderName == "UserContents")
                    {
                        DoesFolderExist(Path.Combine(userFolder, "Habit"));
                        DoesFolderExist(Path.Combine(userFolder, "Journal"));
                        DoesFolderExist(Path.Combine(userFolder, "TODO"));

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
                                        Console.ReadKey(intercept: true);
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
                                        Console.ReadKey(intercept: true);
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
                                        Console.ReadKey(intercept: true);
                                    }
                                }
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
                            Console.ReadKey(intercept: true);
                        }
                    }
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"Directory not found: {ex.Message}");
                Console.ReadKey(intercept: true);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Access denied: {ex.Message}");
                Console.ReadKey(intercept: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.ReadKey(intercept: true);
            }
        }

        public void WriteHabitData(string DataFolder, string HabitID)
        {
            if (Username == null) throw new Exception("Username not set.");
            string habitFolder = Path.Combine(DataFolder, Username, "UserContents", "Habit");
            VerifySystemFolder(habitFolder);

            Habit? habit = HabitList.Find(habit => habit.ID == HabitID);
            if (habit == null)
            {
                Console.WriteLine($"Habit with ID '{HabitID}' not found.");
                return;
            }

            string habitFileName = habit.DateCreated.ToString("ddMMyyyy") + habit.ID  + ".txt";
            string habitFilePath = Path.Combine(habitFolder, habitFileName);

            try
            {
                string[] habitData =
                [
                    habit.ID,
                    habit.Name,
                    habit.Difficulty.ToString(),
                    habit.Experience.ToString(),
                    habit.Completed.ToString()
                ];
                File.WriteAllLines(habitFilePath, habitData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing habit file '{habitFilePath}': {ex.Message}");
            }
        }

        public void WriteJournalData(string DataFolder, string JournalID)
        {
            if (Username == null) throw new Exception("Username not set.");
            string journalFolder = Path.Combine(DataFolder, Username, "UserContents", "Journal");
            VerifySystemFolder(journalFolder);

            JournalEntry? journal = JournalList.Find(journal => journal.ID == JournalID);
            if (journal == null)
            {
                Console.WriteLine($"Journal with ID '{JournalID}' not found.");
                return;
            }

            string journalFileName = journal.DateCreated.ToString("ddMMyyyy") + journal.ID + ".txt";
            string journalFilePath = Path.Combine(journalFolder, journalFileName);

            try
            {
                string[] journalData =
                [
                    journal.ID,
                    journal.Name,
                    journal.Entry
                ];
                File.WriteAllLines(journalFilePath, journalData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing journal file '{journalFilePath}': {ex.Message}");
            }
        }

        public void WriteTaskData(string DataFolder, string TaskID)
        {
            if (Username == null) throw new Exception("Username not set.");
            string taskFolder = Path.Combine(DataFolder, Username, "UserContents", "TODO");
            VerifySystemFolder(taskFolder);

            Task? task = TaskList.Find(task => task.ID == TaskID);
            if (task == null)
            {
                Console.WriteLine($"Task with ID '{TaskID}' not found.");
                return;
            }

            string taskFileName = task.DateCreated.ToString("ddMMyyyy") + task.ID + ".txt";
            string taskFilePath = Path.Combine(taskFolder, taskFileName);

            try
            {
                string[] taskData =
                [
                    task.ID,
                    task.Name,
                    task.Difficulty.ToString(),
                    task.Experience.ToString(),
                    task.Completed.ToString(),
                    task.DateDue.ToString()
                ];
                File.WriteAllLines(taskFilePath, taskData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing task file '{taskFilePath}': {ex.Message}");
            }
        }

        public static void DeleteBoostDataFile(string FilePath)
        {
            try
            {
                File.Delete(FilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file '{FilePath}': {ex.Message}");
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

        #region Boost Data Creation and Deletion
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

        #region Add Methods
        public void AddHabit(string DataFolder, string name, HabitBoostDifficulty difficulty)
        {
            Habit habit = new()
            {
                ID = FindFreeID(HabitList.Cast<IIdentifiable>().ToList()),
                Name = name,
                Experience = 0,
                Completed = false,
                Difficulty = difficulty,
                DateCreated = DateTime.Now
            };
            HabitList.Add(habit);
            WriteHabitData(DataFolder, habit.ID);
        }

        public void AddJournalEntry(string DataFolder, string Name, string Entry)
        {
            JournalEntry entry = new()
            {
                ID = FindFreeID(JournalList.Cast<IIdentifiable>().ToList()),
                Name = Name,
                Entry =  Entry,
                DateCreated = DateTime.Now
            };
            JournalList.Add(entry);
            WriteJournalData(DataFolder, entry.ID);
        }

        public void AddTask(string DataFolder, string Name, DateTime DateDue, HabitBoostDifficulty difficulty)
        {
            Task task = new()
            {
                ID = FindFreeID(TaskList.Cast<IIdentifiable>().ToList()),
                Name = Name,
                Experience = 5,
                Completed = false,
                Difficulty = difficulty,
                DateCreated = DateTime.Now,
                DateDue = DateDue
            };
            TaskList.Add(task);
            WriteTaskData(DataFolder, task.ID);
        }
        #endregion

        #region Delete Methods
        public void DeleteHabit(string DataFolder, string habitID)
        {
            Habit? habit = HabitList.Find(habit => habit.ID == habitID);
            if (habit != null)
            {
                HabitList.Remove(habit);
                if (Username != null)
                DeleteBoostDataFile(Path.Combine(DataFolder, Username, "UserContents", "Habit", habit.DateCreated.ToString("ddMMyyyy") + habit.ID + ".txt"));
            }
        }

        public void DeleteHabit(string DataFolder, int index)
        {
            Habit habit = HabitList[index];
            HabitList.RemoveAt(index);
            if (Username != null)
                DeleteBoostDataFile(Path.Combine(DataFolder, Username, "UserContents", "Habit", habit.DateCreated.ToString("ddMMyyyy") + habit.ID + ".txt"));
        }

        public void DeleteJournalEntry(string DataFolder, string journalID)
        {
            JournalEntry? journal = JournalList.Find(journal => journal.ID == journalID);
            if (journal != null)
            {
                JournalList.Remove(journal);
                if (Username != null)
                    DeleteBoostDataFile(Path.Combine(DataFolder, Username, "UserContents", "Journal", journal.DateCreated.ToString("ddMMyyyy") + journal.ID + ".txt"));
            }
        }

        public void DeleteJournalEntry(string DataFolder, int index)
        {
            JournalEntry journal = JournalList[index];
            JournalList.RemoveAt(index);
            if (Username != null)
                DeleteBoostDataFile(Path.Combine(DataFolder, Username, "UserContents", "Journal", journal.DateCreated.ToString("ddMMyyyy") + journal.ID + ".txt"));
        }

        public void DeleteTask(string DataFolder, string taskID)
        {
            Task? task = TaskList.Find(task => task.ID == taskID);
            if (task != null)
            {
                TaskList.Remove(task);
                if (Username != null)
                    DeleteBoostDataFile(Path.Combine(DataFolder, Username, "UserContents", "TODO", task.DateCreated.ToString("ddMMyyyy") + task.ID + ".txt"));
            }
        }

        public void DeleteTask(string DataFolder, int index)
        {
            Task task = TaskList[index];
            TaskList.RemoveAt(index);
            if (Username != null)
                DeleteBoostDataFile(Path.Combine(DataFolder, Username, "UserContents", "TODO", task.DateCreated.ToString("ddMMyyyy") + task.ID + ".txt"));
        }
        #endregion

        #region Edit Methods
        public void EditHabit(string DataFolder, string habitID, string name, HabitBoostDifficulty difficulty)
        {
            Habit? habit = HabitList.Find(habit => habit.ID == habitID);
            if (habit != null)
            {
                habit.Name = name;
                habit.Difficulty = difficulty;
                WriteHabitData(DataFolder, habit.ID);
            }
        }
        public void EditHabit(string DataFolder, int index, string name, HabitBoostDifficulty difficulty)
        {
            Habit habit = HabitList[index];
            if (habit != null)
            {
                habit.Name = name;
                habit.Difficulty = difficulty;
                WriteHabitData(DataFolder, habit.ID);
            }
        }
        public void EditHabit(string DataFolder, int index, bool completed)
        {
            Habit habit = HabitList[index];
            if (habit != null)
            {
                habit.Completed = completed;
                WriteHabitData(DataFolder, habit.ID);
            }
        }

        public void EditJournalEntry(string DataFolder, string journalID, string name, string entry)
        {
            JournalEntry? journal = JournalList.Find(journal => journal.ID == journalID);
            if (journal != null)
            {
                journal.Name = name;
                journal.Entry = entry;
                WriteJournalData(DataFolder, journal.ID);
            }
        }
        public void EditJournalEntry(string DataFolder, int index, string name, string entry)
        {
            JournalEntry journal = JournalList[index];
            if (journal != null)
            {
                journal.Name = name;
                journal.Entry = entry;
                WriteJournalData(DataFolder, journal.ID);
            }
        }

        public void EditTask(string DataFolder, string taskID, string name, HabitBoostDifficulty difficulty, DateTime dateDue)
        {
            Task? task = TaskList.Find(task => task.ID == taskID);
            if (task != null)
            {
                task.Name = name;
                task.Difficulty = difficulty;
                task.Experience = 0;
                task.Completed = false;
                task.DateDue = dateDue;
                WriteTaskData(DataFolder, task.ID);
            }
        }
        public void EditTask(string DataFolder, int index, string name, HabitBoostDifficulty difficulty, DateTime dateDue)
        {
            Task task = TaskList[index];
            if (task != null)
            {
                task.Name = name;
                task.Difficulty = difficulty;
                task.Experience = 0;
                task.Completed = false;
                task.DateDue = dateDue;
                WriteTaskData(DataFolder, task.ID);
            }
        }

        #endregion
        #endregion
    }
}
