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
            while (ProgramManager.isProgramRunning == true)
            {           
                ScreenRenderer.RenderScreen();
                ScreenRenderer.UserInput();
            }
            return 0;
        }
    }
}