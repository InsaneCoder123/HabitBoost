using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    public struct Habit
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
    public struct Journal
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public double Experience { get; set; }
    }
    public struct Todo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public double Experience { get; set; }
        public bool Completed { get; set; }
    }
    public class UserDataList
    {
        public List<Habit> Habit { get; set; }
        public List<Journal> Journal { get; set; }
        public List<Todo> Todo { get; set; }
        public UserDataList()
        {
            Habit = new List<Habit>();
            Journal = new List<Journal>();
            Todo = new List<Todo>();
        }

        public void AddHabit(Habit habit)
        {
            Habit.Add(habit);
        }
        public void DeleteHabit(Habit habit)
        {
            Habit.Remove(habit);
        }
        public void EditHabit(Habit habit, string name, string description, int level, double experience, bool completed)
        {
            habit.Name = name;
            habit.Description = description;
            habit.Level = level;
            habit.Experience = experience;
            habit.Completed = completed;
        }

        public void AddJournal(Journal journal)
        {
            Journal.Add(journal);
        }
        public void DeleteJournal(Journal journal)
        {
            Journal.Remove(journal);
        }
        public void EditJournal(Journal journal, string name, string description, int level, double experience)
        {
            journal.Name = name;
            journal.Description = description;
            journal.Level = level;
            journal.Experience = experience;
        }

        public void AddTodo(Todo todo)
        {
            Todo.Add(todo);
        }
        public void DeleteTodo(Todo todo)
        {
            Todo.Remove(todo);
        }
        public void EditTodo(Todo todo, string name, string description, int level, double experience, bool completed)
        {
            todo.Name = name;
            todo.Description = description;
            todo.Level = level;
            todo.Experience = experience;
            todo.Completed = completed;
        }
    }
}
