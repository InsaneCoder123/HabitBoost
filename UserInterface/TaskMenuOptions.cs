using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface
{
    public class AddTask : InterfaceOption
    {
        public override void InvokedAction()
        {
            Console.WriteLine("Add Task");
        }
    }
    public class DoneTask : InterfaceOption
    {
        public override void InvokedAction()
        {
            Console.WriteLine("Task FInished");
        }
    }
    public class UndoneTask : InterfaceOption
    {
        public override void InvokedAction()
        {
            Console.WriteLine("Task Unfinished");
        }
    }
    public class EditTask : InterfaceOption
    {
        public override void InvokedAction()
        {
            Console.WriteLine("Edit Task");
        }
    }
    public class DeleteTask : InterfaceOption
    {
        public override void InvokedAction()
        {
            Console.WriteLine("Delete Task");
        }
    }
}
