using Habit_User_Data_Structures;
using UserInterface;

namespace Main 
{
    public class MainLoop
    {
        static int Main(string[] args)
        {

            ProgramManager.StartProgram();
            ScreenRenderer.InitiateGraphics();
            UserData.VerifySystemFolder(ProgramManager.HabitBoostFolderPath);

            while (ProgramManager.isProgramRunning == true)
            {           
                ScreenRenderer.RenderScreen();
                ProgramManager.UserInput();
            }
            return 0;
        }
    }
}