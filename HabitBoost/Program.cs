using Habit_User_Data_Structures;
using UserInterface;

namespace Main 
{
    public class MainLoop
    {
        static int Main(string[] args)
        {

            ProgramManager.StartProgram();
            ScreenRenderer.InitiateGraphics(ref ProgramManager.User);
            UserData.VerifySystemFolder(ProgramManager.HabitBoostFolderPath);

            while (ProgramManager.isProgramRunning == true)
            {           
                ScreenRenderer.RenderScreen(ref ProgramManager.User);
                ProgramManager.UserInput();
            }
            return 0;
        }
    }
}