using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface
{
    public class AddHabit : InterfaceOption
    {
        public override void InvokedAction()
        {
            Console.WriteLine("Add Habit");
        }
    }

    public class ReinforceHabit : InterfaceOption
    {
        public override void InvokedAction()
        {
            Console.WriteLine("Reinforce Habit");
        }
    }

    public class WeakenHabit : InterfaceOption
    {
        public override void InvokedAction()
        {
            Console.WriteLine("Weaken Habit");
        }
    }

    public class DeleteHabit : InterfaceOption
    {
        public override void InvokedAction()
        {
            Console.WriteLine("Delete Habit");
        }
    }
}
