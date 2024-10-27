using User;

namespace UserInterface
{
    public abstract class SubMenu
    {
        private int _currentIndexSelector = 0;
        public int totalItems { get; set; }
        public virtual int currentIndexSelector
        {
            get { return _currentIndexSelector; }
            set { _currentIndexSelector = value; }
        }
    }
    public class HabitMenu : SubMenu
    {
        private int _currentIndexSelector;
        public override int currentIndexSelector
        {
            get { return _currentIndexSelector; }
            set
            {
                if (value < 0)
                {
                    _currentIndexSelector = totalItems - 1;
                }
                else if (value >= totalItems)
                {
                    _currentIndexSelector = 0;
                }
                else
                {
                    _currentIndexSelector = value;
                }
            }
        }
        public HabitMenu(List<Habit> habitsList)
        {
            totalItems = habitsList.Count;
        }
    }
    public class JournalMenu : SubMenu
    {
    }
    public class TodoMenu : SubMenu
    {
    }
    public static class ScreenRenderer
    {
        public static void RenderScreen(UserData user, ProgramManager.CurrentSubMenu currentMenu)
        {
            Console.Clear();
            Console.Write($"{ProgramManager.currentDate:d}\n");
            Console.Write($"User ID: {user.Id}\n");
            Console.Write($"Level: {user.UserStats.Level}\n");
            Console.Write($"Experience: {user.UserStats.Experience}\n\n");

            switch (currentMenu)
            {
                case ProgramManager.CurrentSubMenu.Habit:
                    Console.Write("HabitMenu\n");
                    RenderHabitMenu(user);
                    break;
                case ProgramManager.CurrentSubMenu.Journal:
                    Console.Write("Journal Menu\n");
                    break;
                case ProgramManager.CurrentSubMenu.Todo:
                    Console.Write("Todo Menu\n");
                    break;
            }
        }
        public static void RenderOptions(UserData user) { 
        }
        public static void RenderHabitMenu(UserData user)
        {
            Console.Write("Habit Menu\n");
            Console.Write("Habits:\n");

            if (ProgramManager.subMenus != null && ProgramManager.subMenus.Count > 0)
            {
                foreach (Habit habit in user.UserDataList.Habit)
                {
                    int currentIndex = user.UserDataList.Habit.IndexOf(habit);
                    if (currentIndex == ProgramManager.subMenus[0].currentIndexSelector)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.Write($"{habit.Name}\n");
                    Console.ResetColor();
                }
            }
        }
    }
}