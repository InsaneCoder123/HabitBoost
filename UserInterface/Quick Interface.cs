using User;

namespace UserInterface
{
    public abstract class SubMenu
    {
        private int _currentIndexSelector = 0;
        public virtual int currentIndexSelector
        {
            get { return _currentIndexSelector; }
            set { _currentIndexSelector = value; }
        }

        public virtual void UpdateData(UserData userData)
        {
            // Placeholder (This should be where the program reads the data from a file)
        }
    }
    public class HabitMenu : SubMenu
    {
        private List<Habit> UserHabits { get; set; }
        private int _currentIndexSelector;
        public HabitMenu (List<Habit> UserHabits)
        {
            this.UserHabits = UserHabits;
        }
        public override int currentIndexSelector
        {
            get { return _currentIndexSelector; }
            set
            {
                if (value < 0)
                {
                    _currentIndexSelector = UserHabits.Count - 1;
                }
                else if (value >= UserHabits.Count)
                {
                    _currentIndexSelector = 0;
                }
                else
                {
                    _currentIndexSelector = value;
                }
            }
        }
        public override void UpdateData(UserData userData)
        {
            // Load habits from userdata to UserHabits
            UserHabits.Clear();
            for (int i = 0; i < userData.UserDataList.Habit.Count; i++)
            {
                UserHabits.Add(userData.UserDataList.Habit[i]);
            
        }
    }
    public class DailyMenu : SubMenu
    {
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
            Console.Write($"User ID: {user.Id}\n");
            Console.Write($"Level: {user.UserStats.Level}\n");
            Console.Write($"Experience: {user.UserStats.Experience}\n\n");

            switch (currentMenu)
            {
                case ProgramManager.CurrentSubMenu.Habit:
                    RenderHabitMenu(user);
                    break;
                case ProgramManager.CurrentSubMenu.Daily:
                    Console.Write("Daily Menu\n");
                    break;
                case ProgramManager.CurrentSubMenu.Journal:
                    Console.Write("Journal Menu\n");
                    break;
                case ProgramManager.CurrentSubMenu.Todo:
                    Console.Write("Todo Menu\n");
                    break;
            }
        }
        public static void RenderHabitMenu(UserData user)
        {
            Console.Write("Habit Menu\n");
            Console.Write("Habits\n");

            foreach (Habit habit in user.UserDataList.Habit)
            {
                Console.Write($"{habit.Name}\n");
            }
        }
    }
    }
}