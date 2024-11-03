using Habit_User_Data_Structures;
using static UserInterface.ScreenRenderer;

namespace UserInterface
{
    public static class ProgramManager
    {
        public static bool DebugMode { get; set; } = false;

        public static bool isProgramRunning { get; set; }
        public static ConsoleKeyInfo keyPressedInfo { get; set; }
        public static DateTime currentDate { get; set; }

        public static string HabitBoostFolderPath { get; set; } = AppContext.BaseDirectory + @"\HabitBoostData";
        public static string UserFolderPath { get; set; } = HabitBoostFolderPath;
        public static UserData User { get; set; } = new UserData();

        public static string ButtonInvokedInformation { get; set; } = "";

        public static void StartProgram()
        {
            isProgramRunning = true;
            currentDate = DateTime.Today;
        }

        public static bool AttemptLogin(string UserName, string Password) 
        {
            if (DoesUserExists(UserName))
            {
                string UserDataFolderPath = HabitBoostFolderPath + @"\" + UserName + @"\UserData";
                try
                {
                    string password = File.ReadAllText(UserDataFolderPath + @"\password.txt");
                    if (password == Password) { return true; }
                    else { return false; }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
            return false;
        }

        public static void CreatePassword()
        {
            // Create password
            // - Check if password is strong
            // - If password is weak, prompt user to re-enter password
            // If password is strong, create a new user
            // Then store user data to a file contained in a folder named "UserData" in the User's folder
        }

        public static bool DoesUserExists(string userName)
        {
            string[] userFolders = Directory.GetDirectories(HabitBoostFolderPath);

            foreach (string userFolder in userFolders)
            {
                string userFolderName = Path.GetFileName(userFolder);
                if (userFolderName == userName)
                {
                    return true;
                }
            }
            return false;
        }

        public static void UserInput()
        {
            UserInputStreamString = "";
            UserInputStream = Console.ReadKey(intercept: true);
            UserInputStreamString = UserInputStream.KeyChar.ToString();

            if (UserInputStream.Key == ConsoleKey.UpArrow && CurrentInterfaceIndexSelector != 0)
            {
                UserInputStreamString = "";
                if (CurrentProgramState == ProgramState.Browse)
                { --CurrentInterfaceIndexSelector; }
            }
            else if (UserInputStream.Key == ConsoleKey.DownArrow)
            {
                UserInputStreamString = "";
                if (CurrentProgramState == ProgramState.Browse)
                { ++CurrentInterfaceIndexSelector; }
            }
            else if (UserInputStream.Key == ConsoleKey.Enter && CurrentProgramState == ProgramState.Browse)
            {
                UserInputStreamString = "";
                //++CurrentInterfaceLevel;
                //CurrentInterfaceIndexSelector = 0;

                #region Button Non-Invokable Binded to InputField
                // Error spot! Warning! null stuff
                currentGraphicElement = GetCurrentActiveGraphicElement();
                if (currentGraphicElement != null)
                {
                    currentActiveButton = currentGraphicElement.GetCurrentActiveButton();
                    if (currentActiveButton != null)
                    {
                        HabitInterface? bindedInterface = currentActiveButton.BindedInterface;
                        if (bindedInterface != null)
                        {
                            CurrentProgramState = ProgramState.Edit;
                            CurrentInterfaceLevel = bindedInterface.MenuInterfaceLevel;
                            CurrentInterfaceIndexSelector = bindedInterface.InterfaceIndex;
                        }
                    }
                }
                #endregion

                #region Button Invokable
                if (currentGraphicElement != null)
                {
                    currentActiveButton = currentGraphicElement.GetCurrentActiveButton();
                    if (currentActiveButton != null && currentActiveButton.IsInvokable)
                    {
                        ButtonInvokedInformation = currentActiveButton.InvokeButton();

                        // Login Invoke
                        if (ButtonInvokedInformation[0] == '0')
                        {
                            if (AttemptLogin(ButtonInvokedInformation[1..30].Replace("~", ""),
                                ButtonInvokedInformation[31..40].Replace("~", "")))
                            {
                                User.ReadData(UserFolderPath + @"\" + ButtonInvokedInformation[1..30].Replace("~", ""));
                            }
                        }

                    }
                }
                #endregion

            }
            else if (UserInputStream.Key == ConsoleKey.Escape)
            {
                if (CurrentProgramState == ProgramState.Edit)
                {
                    CurrentProgramState = ProgramState.Browse;
                    if (currentActiveButton != null)
                    {
                        CurrentInterfaceLevel = currentActiveButton.MenuInterfaceLevel;
                        CurrentInterfaceIndexSelector = currentActiveButton.InterfaceIndex;
                    }
                }
            }
            else if (CurrentProgramState == ProgramState.Edit)
            {
                foreach (GraphicElement graphicElement in ScreenRenderer.GraphicElement)
                {
                    if (graphicElement.InputFields != null && graphicElement.IsGraphicElementActive == true)
                    {
                        foreach (InputField inputField in graphicElement.InputFields)
                        {
                            if (CurrentInterfaceIndexSelector == inputField.InterfaceIndex && CurrentInterfaceLevel == inputField.MenuInterfaceLevel)
                            {
                                if (UserInputStream.Key == ConsoleKey.Backspace)
                                {
                                    if (inputField.FieldText.Length > 0)
                                    {
                                        inputField.RemoveFieldText();
                                    }
                                }
                                else if (UserInputStream.Key == ConsoleKey.Spacebar)
                                {
                                    inputField.AddFieldText(' ');
                                }
                                else if (IsLetterOrValidSymbol(UserInputStream.KeyChar))
                                {
                                    inputField.AddFieldText(UserInputStream.KeyChar);
                                }
                            }
                        }
                    }
                }
            }
            // Later implement to validate if the input are characters or numbers and not enter or space
        }
    }
}
