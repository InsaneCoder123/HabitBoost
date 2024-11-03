namespace Habit_User_Data_Structures
{
    public enum HabitBoostDifficulty 
    { 
        Easy, 
        Medium, 
        Hard, 
        VeryHard 
    }
    public abstract class HabitBoostData
    {
        public required string Name { get; set; }
        public required string ID { get; set; }
        public required string Description { get; set; }
        // 00 00 0000 - Date, Month, Year Created
        // 0 - Habit Boost Data Type {0 - Habit, 1 - ToDo, 2 - Journal Entry, 3 - Achievement}
        // 000 - habit ID
        // Example: 010120210001 - Is a Habit created on the 1st of January 2021 with an id of 1
    }
    public class Habit : HabitBoostData
    {
        public required double Experience { get; set; }
        public required bool Completed { get; set; }
        public required HabitBoostDifficulty Difficulty { get; set; }
        public required DateTime DateCreated { get; set; }
    }
    public class Task : HabitBoostData
    {
        public required double Experience { get; set; }
        public required bool Completed { get; set; }
        public required HabitBoostDifficulty Difficulty { get; set; }
        public required DateTime DateCreated { get; set; }
        public required DateTime DateDue { get; set; }
    }
    public class JournalEntry : HabitBoostData
    {
        public required DateTime DateCreated { get; set; }
    }
    public class Achievement : HabitBoostData
    {
        public required double Experience { get; set; }
        public required bool Completed { get; set; }
        public required HabitBoostDifficulty Difficulty { get; set; }
    }
}
