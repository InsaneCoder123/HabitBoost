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
    public struct Daily
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
        public List<HabitType> Habit { get; set; }
        public List<JournalType> Journal { get; set; }
        public List<TodoType> Todo { get; set; }
        public UserDataListType()
        {
            Habit = new List<HabitType>();
            Journal = new List<JournalType>();
            Todo = new List<TodoType>();
        }

        public void AddHabit(HabitType habit)
        {
            Habit.Add(habit);
        }
        public void DeleteHabit(HabitType habit)
        {
            Habit.Remove(habit);
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
            Journal.Add(journal);
        }
        public void DeleteJournal(JournalType journal)
        {
            Journal.Remove(journal);
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
            Todo.Add(todo);
        }
        public void DeleteTodo(TodoType todo)
        {
            Todo.Remove(todo);
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
