using Collection;
using DataList;
using System;

namespace QuickInterface 
{
    public class SubMenu { 
        public static UserDataList data = new UserDataList();

        public void LoadData(){
            // Placeholder (This should be where the program reads the data from a file)
        }
    }
    public class HabitMenu : SubMenu
    {
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
            SwitchSubMenu();
        }
        public static void InitiateSubMenu()
        {
            subMenus = new List<SubMenu>();
            subMenus.Add(new HabitMenu());
            subMenus.Add(new DailyMenu());
            subMenus.Add(new TodoMenu());
            subMenus.Add(new JournalMenu());
        }
        public static void SwitchSubMenu()
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
        }
    }

    public static class ScreenRenderer {
        public static void RenderScreen(UserData user, ProgramManager.CurrentSubMenu currentMenu)
        {
            Console.Write($"User ID: {user.Id}\n");
            Console.Write($"Level: {user.UserStats.Level}\n");
            Console.Write($"Experience: {user.UserStats.Experience}\n");

            switch (currentMenu) {
                case ProgramManager.CurrentSubMenu.Habit:
                    Console.Write("Habit Menu\n");
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
    }
}