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
        public static void InitiateSubMenu(UserDataListType userDataList)
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

        public static void AttemptLogin() 
        {
            // Login
            // - Check if user exists
            // - Check if password is correct
            // - If user does not exist, create a new user
            // - If password is incorrect, prompt user to re-enter password
        }

        public static void CreatePassword()
        {
            // Create password
            // - Check if password is strong
            // - If password is weak, prompt user to re-enter password
            // If password is strong, create a new user
            // Then store user data to a file contained in a folder named "UserData" in the User's folder
        }
    }
}
