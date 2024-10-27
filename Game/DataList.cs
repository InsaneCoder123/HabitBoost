using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    public struct HabitType
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public double Experience { get; set; }
        public bool Completed { get; set; }
    }
    public struct JournalType
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public double Experience { get; set; }
    }
    public struct TodoType
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public double Experience { get; set; }
        public bool Completed { get; set; }
    }
    public class UserDataListType
    {
        public List<HabitType> HabitList { get; set; }
        public List<JournalType> JournalList { get; set; }
        public List<TodoType> TodoList { get; set; }
        public UserDataListType()
        {
            HabitList = new List<HabitType>();
            JournalList = new List<JournalType>();
            TodoList = new List<TodoType>();
        }

        public void AddHabit(HabitType habit)
        {
            HabitList.Add(habit);
        }
        public void DeleteHabit(HabitType habit)
        {
            HabitList.Remove(habit);
        }
        public void EditHabit(HabitType habit, string name, string description, int level, double experience, bool completed)
        {
            habit.Name = name;
            habit.Description = description;
            habit.Level = level;
            habit.Experience = experience;
            habit.Completed = completed;
        }

        public void AddJournal(JournalType journal)
        {
            JournalList.Add(journal);
        }
        public void DeleteJournal(JournalType journal)
        {
            JournalList.Remove(journal);
        }
        public void EditJournal(JournalType journal, string name, string description, int level, double experience)
        {
            journal.Name = name;
            journal.Description = description;
            journal.Level = level;
            journal.Experience = experience;
        }

        public void AddTodo(TodoType todo)
        {
            TodoList.Add(todo);
        }
        public void DeleteTodo(TodoType todo)
        {
            TodoList.Remove(todo);
        }
        public void EditTodo(TodoType todo, string name, string description, int level, double experience, bool completed)
        {
            todo.Name = name;
            todo.Description = description;
            todo.Level = level;
            todo.Experience = experience;
            todo.Completed = completed;
        }
    }
}
