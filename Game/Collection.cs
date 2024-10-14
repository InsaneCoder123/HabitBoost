using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection
{
    interface IWriteData
    {
        void WriteData();
    }
    interface IReadData
    {
        void ReadData();
    }
    public class Game
    {
        public int Level { get; set; }
        public double Experience { get; set; }
    }
    public class UserData : IWriteData, IReadData
    {
        public int Id { get; set; }
        public Game UserStats { get; set; }

        public UserData() 
        {
            UserStats = new Game();
        }
        public void WriteData()
        {
        }
        public void ReadData()
        {
        }
    }
}
