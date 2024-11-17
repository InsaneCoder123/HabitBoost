namespace Habit_User_Data_Structures
{
    public enum HabitBoostDifficulty 
    { 
        Easy, 
        Medium, 
        Hard, 
        VeryHard 
    }
    public interface IIdentifiable
    {
        string ID { get; set; }
    }

    public abstract class HabitBoostData
    {
        public string ID { get; set; } = "";
        public string Name { get; set; } = "";
        // 00 00 0000 - Date, Month, Year Created
        // 0 - Habit Boost Data Type {0 - Habit, 1 - ToDo, 2 - Journal Entry, 3 - Achievement}
        // 000 - habit ID
        // Example: 010120210001 - Is a Habit created on the 1st of January 2021 with an id of 1
    }
    public class Habit : HabitBoostData, IIdentifiable
    {
        public int Experience { get; set; }
        public bool Completed { get; set; }
        public HabitBoostDifficulty Difficulty { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastCompleted { get; set; }
    }
    public class Task : HabitBoostData, IIdentifiable
    {
        public required double Experience { get; set; }
        public required bool Completed { get; set; }
        public required HabitBoostDifficulty Difficulty { get; set; }
        public required DateTime DateCreated { get; set; }
        public required DateTime DateDue { get; set; }
    }
    public class JournalEntry : HabitBoostData, IIdentifiable
    {
        public required DateTime DateCreated { get; set; }
        public required string Entry { get; set; }
    }
    public class Achievement : HabitBoostData, IIdentifiable
    {
        public required string AchievementDescription { get; set; }
        public int Experience { get; set; } = 10;
        public required bool Completed { get; set; }
        public required HabitBoostDifficulty Difficulty { get; set; }
    }
}
