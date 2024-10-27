using User;

namespace UserInterface
{
    public static class ProgramManager
    {
        public enum CurrentSubMenu
        {
            Habit,
            Journal,
            Todo
        }
        public static bool isProgramRunning { get; set; }
        public static ConsoleKeyInfo keyPressedInfo { get; set; }
        public static List<SubMenu>? subMenus { get; set; }
        public static CurrentSubMenu subMenu { get; set; }
        public static DateTime currentDate { get; set; }
        public static OptionList optionList { get; set; }


        public static void StartProgram()
        {
            isProgramRunning = true;
            currentDate = DateTime.Today;
            subMenu = CurrentSubMenu.Habit;
        }
        public static void WaitForKeyInput()
        {
            keyPressedInfo = Console.ReadKey(intercept: true);
            UserInputControl();
        }
        public static void InitiateSubMenu(UserDataList userDataList)
        {
            optionList = new OptionList(4);
            subMenus = new List<SubMenu>();
            subMenus.Add(new HabitMenu(userDataList.Habit));
            subMenus.Add(new TodoMenu());
            subMenus.Add(new JournalMenu());
        }
        public static void UserInputControl()
        {
            switch (keyPressedInfo.Key)
            {
                case ConsoleKey.RightArrow:
                    if (optionList.isOptionSelected) { optionList.currentIndexSelector = 0; }
                    if (subMenu == CurrentSubMenu.Todo)
                    {
                        subMenu = CurrentSubMenu.Habit;
                    }
                    else
                    {
                        subMenu++;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (optionList.isOptionSelected) { optionList.currentIndexSelector = 0; }
                    if (subMenu == CurrentSubMenu.Habit)
                    {
                        subMenu = CurrentSubMenu.Todo;
                    }
                    else
                    {
                        subMenu--;
                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (optionList.isOptionSelected) { optionList.currentIndexSelector--; break; }
                    subMenus[(int)subMenu].currentIndexSelector--;
                    break;
                case ConsoleKey.DownArrow:
                    if (optionList.isOptionSelected) { optionList.currentIndexSelector++; break; }
                    subMenus[(int)subMenu].currentIndexSelector++;
                    break;
            }
        }
    }
}
