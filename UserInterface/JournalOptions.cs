using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface
{
    public class AddJournal : InterfaceOption
    {
        public override void InvokedAction()
        {
            Console.WriteLine("Add Journal");
        }
    }
    public class ViewJournal : InterfaceOption
    {
        public override void InvokedAction()
        {
            Console.WriteLine("View Journal");
        }
    }
    public class EditJournal : InterfaceOption
    {
        public override void InvokedAction()
        {
            Console.WriteLine("Edit Journal");
        }
    }
    public class DeleteJournal : InterfaceOption
    {
        public override void InvokedAction()
        {
            Console.WriteLine("Delete Journal");
        }
    }
}
