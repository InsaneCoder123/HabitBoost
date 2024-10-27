using User;

namespace UserInterface
{
    public static class ProgramManager
    {
        public enum CurrentSubMenu
        {
            Habit,
            Daily,
            Journal,
            Todo
        }
        public static bool isProgramRunning { get; set; }
        public static ConsoleKeyInfo keyPressedInfo { get; set; }
        public static List<SubMenu>? subMenus { get; set; }
        public static CurrentSubMenu subMenu { get; set; }


        public static void StartProgram()
        {
            isProgramRunning = true;
            subMenu = CurrentSubMenu.Habit;
        }
        public static void WaitForKeyInput()
        {
            keyPressedInfo = Console.ReadKey(intercept: true);
            UserInputControl();
        }
        public static void InitiateSubMenu(UserDataList userDataList)
        {
            subMenus = new List<SubMenu>();
            subMenus.Add(new HabitMenu(userDataList.Habit));
            //subMenus.Add(new DailyMenu());
            //subMenus.Add(new TodoMenu());
            //subMenus.Add(new JournalMenu());
        }
        public static void UserInputControl()
        {
            if (keyPressedInfo.Key == ConsoleKey.RightArrow)
            {
                if (subMenu == CurrentSubMenu.Todo)
                {
                    subMenu = CurrentSubMenu.Habit;
                }
                else
                {
                    subMenu++;
                }
            }
            else if (keyPressedInfo.Key == ConsoleKey.LeftArrow)
            {
                if (subMenu == CurrentSubMenu.Habit)
                {
                    subMenu = CurrentSubMenu.Todo;
                }
                else
                {
                    subMenu--;
                }
            }
            else if (keyPressedInfo.Key == ConsoleKey.UpArrow)
            {
                subMenus[(int)subMenu].currentIndexSelector--;
            }
            else if (keyPressedInfo.Key == ConsoleKey.DownArrow)
            {
                subMenus[(int)subMenu].currentIndexSelector++;
            }
        }
    }
}
