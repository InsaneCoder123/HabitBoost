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
        public static UserData User = new UserData();

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

            if (UserInputStream.Key == ConsoleKey.UpArrow && CurrentInterfaceIndexSelectorY != 0)
            {
                UserInputStreamString = "";
                if (CurrentProgramState == ProgramState.Browse)
                { --CurrentInterfaceIndexSelectorY; }
            }
            else if (UserInputStream.Key == ConsoleKey.DownArrow)
            {
                UserInputStreamString = "";
                if (CurrentProgramState == ProgramState.Browse)
                { ++CurrentInterfaceIndexSelectorY; }
            }
            else if (UserInputStream.Key == ConsoleKey.LeftArrow)
            {
                UserInputStreamString = "";
                if (CurrentProgramState == ProgramState.Browse)
                { --CurrentInterfaceIndexSelectorX; }
            }
            else if (UserInputStream.Key == ConsoleKey.RightArrow)
            {
                UserInputStreamString = "";
                if (CurrentProgramState == ProgramState.Browse)
                { ++CurrentInterfaceIndexSelectorX; }
            }
            else if (UserInputStream.Key == ConsoleKey.Enter && CurrentProgramState == ProgramState.Browse)
            {
                UserInputStreamString = "";
                //++CurrentInterfaceLevel;
                //CurrentInterfaceIndexSelector = 0;

                #region Button Non-Invokable Binded to InputField
                // Error spot! Warning! null stuff
                currentGraphicElement = GetCurrentActiveGraphicElement(GetCurrentActiveScene());
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
                            CurrentInterfaceIndexSelectorY = bindedInterface.InterfaceIndexY;
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
                                SwitchScreen(ProgramScreen.Main);
                            }
                        }

                        // Switch Screen Invoke
                        if (ButtonInvokedInformation[0] == '1')
                        {
                            SwitchScreen((ProgramScreen)int.Parse(ButtonInvokedInformation[1..2]));
                        }

                        // Toggle Boost Data List Invoke
                        if (ButtonInvokedInformation[0] == '2')
                        {
                            ToggleSpecificGraphicElement(ButtonInvokedInformation[1..4], ButtonInvokedInformation[4] == '1', 
                                CurrentInterfaceIndexSelectorY.ToString());
                            CurrentInterfaceIndexSelectorY = ButtonInvokedInformation[5] - '0';
                            CurrentInterfaceIndexSelectorX = ButtonInvokedInformation[6] - '0';
                            CurrentInterfaceLevel = ButtonInvokedInformation[7] - '0';
                        }

                        // Habit Operation Invoke
                        if (ButtonInvokedInformation[0] == '3')
                        {
                            // Add Operation
                            if (ButtonInvokedInformation[1] == '1')
                            {
                               ToggleSpecificGraphicElement("002", true,
                                   CurrentInterfaceIndexSelectorY.ToString(), true);
                                CurrentInterfaceIndexSelectorY = 0;
                                CurrentInterfaceIndexSelectorX = 0;
                                CurrentInterfaceLevel = 4;
                            }

                            // Delete Operation
                            if (ButtonInvokedInformation[1] == '2')
                            {
                                User.DeleteHabit(UserFolderPath, ButtonInvokedInformation[2] - '0');
                            }

                            // Edit Operation
                            if (ButtonInvokedInformation[1] == '3')
                            {
                                //User.EditHabit(UserFolderPath, ButtonInvokedInformation[2] - '0');
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
                        CurrentInterfaceIndexSelectorY = currentActiveButton.InterfaceIndexY;
                    }
                }
                else if (CurrentProgramState == ProgramState.Browse)
                {
                    string currentActiveGraphicInfoToken = GetCurrentActiveGraphicElement(GetCurrentActiveScene())?.InfoToken ?? "";
                    bool isEscapeInfoPresent =
                        currentActiveGraphicInfoToken.Length >= 3 &&
                        !char.IsWhiteSpace(currentActiveGraphicInfoToken[1]) &&
                        !char.IsWhiteSpace(currentActiveGraphicInfoToken[2]) &&
                        !char.IsWhiteSpace(currentActiveGraphicInfoToken[3]);
                    if (isEscapeInfoPresent)
                    {
                        ToggleSpecificGraphicElement(currentActiveGraphicInfoToken[1..4], true,
                            CurrentInterfaceIndexSelectorY.ToString(), true);
                        CurrentInterfaceIndexSelectorY = currentActiveGraphicInfoToken[4] - '0';
                        CurrentInterfaceIndexSelectorX = currentActiveGraphicInfoToken[5] - '0';
                        CurrentInterfaceLevel = currentActiveGraphicInfoToken[6] - '0';
                    }
                }
            }
            else if (CurrentProgramState == ProgramState.Edit)
            {
                foreach (GraphicElement graphicElement in GetCurrentActiveScene())
                {
                    if (graphicElement.InputFields != null && graphicElement.IsGraphicElementVisible == true)
                    {
                        foreach (InputField inputField in graphicElement.InputFields)
                        {
                            if (CurrentInterfaceIndexSelectorY == inputField.InterfaceIndexY && CurrentInterfaceLevel == inputField.MenuInterfaceLevel)
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
            else if (UserInputStream.Key == ConsoleKey.W)
            {
                User.ReadData(UserFolderPath + @"\" + User.Username);
            }
            else if (UserInputStream.Key == ConsoleKey.Q)
            {
                User.AddHabit(HabitBoostFolderPath);
                //User.AddJournalEntry();
                //User.AddTask();
            }
            else if (UserInputStream.Key == ConsoleKey.R)
            {
                SwitchScreen(ProgramScreen.Main);
            }
        }
    }
}
