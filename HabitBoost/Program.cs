using User;
using UserInterface;

namespace Main 
{
    public class MainLoop
    {
        static int Main(string[] args)
        {
            // Login Screen
            // Prompt for a login for user
            // if user has no account, prompt to create a new user
            // else, load user data and proceed to main screen
            UserData user = new UserData();

            //Placeholder (This should be where the program reads the data from a file)
            user.UserStats.Level = 1;
            user.UserStats.Experience = 5;
            user.UserDataList.HabitList.Add(new HabitType { Name = "Exercise", Description = "Exercise for 30 minutes", Level = 1, Experience = 5, Completed = false });
            user.UserDataList.HabitList.Add(new HabitType { Name = "Assignment", Description = "Assignment for 10 minutes", Level = 2, Experience = 10, Completed = false });
            user.UserDataList.HabitList.Add(new HabitType { Name = "Paper", Description = "Practice", Level = 2, Experience = 10, Completed = false });

            ProgramManager.StartProgram();
            ProgramManager.InitiateSubMenu(user.UserDataList);
            while (ProgramManager.isProgramRunning == true)
            {
                // Login Screen
                // - Check if user exists
                // - Check if password is correct
                // - If user does not exist, create a new user
                // - If password is incorrect, prompt user to re-enter password



                // Main screen
                // Display user stats
                // Display display habits, dailies, journals, and todos
                // Prompt user to select an option
                ScreenRenderer.RenderScreen(user, ProgramManager.subMenu);
                ProgramManager.WaitForKeyInput();

                // Habit <--> Daily <--> Journal <--> Todo

                //Habit screen

                //Daily screen

                //Journal screen

                //Todo screen

                // Features

                // - Add new habit
                // - Edit habit
                // - Delete habit
                // - Complete habit
                // - View habit history

                // - Add new daily
                // - Edit daily
                // - Delete daily
                // - Complete daily
                // - View daily history

                // - Add new journal
                // - Edit journal
                // - Delete journal
                // - View journal history

                // - Add new todo
                // - Edit todo
                // - Delete todo
                // - Complete todo
                // - View todo history

                // - View user stats
                // - View user achievements
                // - View user settings
                // - View user statistics

            }
            return 0;
        }
    }
}