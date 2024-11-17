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
            ProgramManager.AddBottomMessage("CONTROLS:", false);
            ProgramManager.AddBottomMessage("ARROW KEYS - MOVE", false);
            ProgramManager.AddBottomMessage("ENTER - SELECT", false);
            ProgramManager.AddBottomMessage("ESC - BACK", false);

            while (ProgramManager.isProgramRunning == true)
            {
                ProgramManager.UpdateInformation();
                ScreenRenderer.RenderScreen(ref ProgramManager.User);
                ProgramManager.UserInput();
            }
            return 0;
        }
    }
}