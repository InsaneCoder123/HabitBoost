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
        public List<Daily> Daily { get; set; }
        public List<Journal> Journal { get; set; }
        public List<Todo> Todo { get; set; }
        public UserDataList()
        {
            Habit = new List<Habit>();
            Daily = new List<Daily>();
            Journal = new List<Journal>();
            Todo = new List<Todo>();
        }
    }
}
