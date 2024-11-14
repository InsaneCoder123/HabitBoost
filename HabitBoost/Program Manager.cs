using Habit_User_Data_Structures;
using System.ComponentModel.Design;
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

        public static void UpdateInformation()
        {
            HabitEditInterfaceGraphics.IsHabitListEmpty = User.HabitList.Count == 0;
            HabitEditInterfaceGraphics.IsJournalListEmpty = User.JournalList.Count == 0;
            HabitEditInterfaceGraphics.IsToDoListEmpty = User.TaskList.Count == 0;
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

        public static void CreateUser(string Username, string Password)
        {
            string[] dummyData = ["1", "10"];
            Directory.CreateDirectory(HabitBoostFolderPath + @"\" + Username + @"\UserData");
            File.WriteAllText(HabitBoostFolderPath + @"\" + Username + @"\UserData\password.txt", Password);
            File.WriteAllLines(HabitBoostFolderPath + @"\" + Username + @"\UserData\datas.txt", dummyData);
        }

        public static void AddBottomMessage(string message, bool isError) 
        {
            // If $ at end, then error
            // if # at end, then message
            if (isError)
            {
                ProgramMessage.Add(message + "$");
            }
            else
            {
                ProgramMessage.Add(message + "#");
            }
            ++ScreenHeight;
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
                CurrentGraphicElement = GetCurrentActiveGraphicElement(GetCurrentActiveScene());
                if (CurrentGraphicElement != null)
                {
                    CurrentActiveButton = CurrentGraphicElement.GetCurrentActiveButton();
                    if (CurrentActiveButton != null)
                    {
                        HabitInterface? bindedInterface = CurrentActiveButton.BindedInterface;
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

                void ClearInputFields()
                {
                    List<GraphicElement> currentScene = GetCurrentActiveScene();
                    foreach (InputField inputField in currentScene[0]?.InputFields ?? Enumerable.Empty<InputField>())
                    {
                        inputField.ClearFieldText();
                    }
                }

                if (CurrentGraphicElement != null)
                {
                    CurrentActiveButton = CurrentGraphicElement.GetCurrentActiveButton();
                    if (CurrentActiveButton != null && CurrentActiveButton.IsInvokable)
                    {
                        ButtonInvokedInformation = CurrentActiveButton.InvokeButton();


                        if (ButtonInvokedInformation[0..2] == "-1")
                        {
                            ToggleSpecificGraphicElement("000", true,
                                    CurrentInterfaceIndexSelectorY.ToString(), true);
                            CurrentInterfaceIndexSelectorY = 0;
                            CurrentInterfaceIndexSelectorX = 0;
                            CurrentInterfaceLevel = 1;
                        }
                        // Login Invoke
                        else if (ButtonInvokedInformation[0] == '0')
                        {
                            string username = ButtonInvokedInformation[1..30].Replace("~", "");
                            string password = ButtonInvokedInformation[31..40].Replace("~", "");

                            if (username == "" || password == "")
                            {
                                if (username == "") { AddBottomMessage("Empty Username!", true); }
                                if (password == "") { AddBottomMessage("Empty Password!", true); }
                                ClearInputFields();
                            }
                            else if (AttemptLogin(ButtonInvokedInformation[1..30].Replace("~", ""),
                                ButtonInvokedInformation[31..40].Replace("~", "")))
                            {
                                User.ReadData(UserFolderPath + @"\" + ButtonInvokedInformation[1..30].Replace("~", ""));
                                SwitchScreen(ProgramScreen.Main);
                            }
                        }

                        // Switch Screen Invoke
                        else if (ButtonInvokedInformation[0] == '1')
                        {
                            SwitchScreen((ProgramScreen)int.Parse(ButtonInvokedInformation[1..2]));
                        }

                        // Toggle Invoke
                        else if (ButtonInvokedInformation[0] == '2')
                        {
                            ToggleSpecificGraphicElement(ButtonInvokedInformation[1..4], ButtonInvokedInformation[4] == '1',
                                CurrentInterfaceIndexSelectorY.ToString(), ButtonInvokedInformation[8] == '1');
                            CurrentInterfaceIndexSelectorY = ButtonInvokedInformation[5] - '0';
                            CurrentInterfaceIndexSelectorX = ButtonInvokedInformation[6] - '0';
                            CurrentInterfaceLevel = ButtonInvokedInformation[7] - '0';
                        }

                        // Habit Operation Invoke
                        else if (ButtonInvokedInformation[0] == '3')
                        {
                            // Add Operation
                            if (ButtonInvokedInformation[1] == '1')
                            {
                                ToggleSpecificGraphicElement("002", true,
                                    CurrentInterfaceIndexSelectorY.ToString(), ButtonInvokedInformation[2] == '1');
                                CurrentInterfaceIndexSelectorY = 0;
                                CurrentInterfaceIndexSelectorX = 0;
                                CurrentInterfaceLevel = 4;
                            }

                            // Delete Operation
                            if (ButtonInvokedInformation[1] == '2')
                            {
                                User.DeleteHabit(UserFolderPath, int.Parse(ButtonInvokedInformation[3..]));
                                UpdateInformation();
                                if (HabitEditInterfaceGraphics.IsHabitListEmpty)
                                {
                                    ToggleSpecificGraphicElement("003", true,
                                        CurrentInterfaceIndexSelectorY.ToString(), ButtonInvokedInformation[2] == '1');
                                    CurrentInterfaceIndexSelectorY = 0;
                                    CurrentInterfaceIndexSelectorX = 0;
                                    CurrentInterfaceLevel = 0;
                                }
                                else
                                {
                                    ToggleSpecificGraphicElement("000", true,
                                        CurrentInterfaceIndexSelectorY.ToString(), ButtonInvokedInformation[2] == '1');
                                    CurrentInterfaceIndexSelectorY = 0;
                                    CurrentInterfaceIndexSelectorX = 0;
                                    CurrentInterfaceLevel = 1;
                                }
                            }

                            // Edit Operation
                            if (ButtonInvokedInformation[1] == '3')
                            {
                                ToggleSpecificGraphicElement("002", true,
                                   CurrentInterfaceIndexSelectorY.ToString() + '2', ButtonInvokedInformation[2] == '1', false);
                                CurrentInterfaceIndexSelectorY = 0;
                                CurrentInterfaceIndexSelectorX = 0;
                                CurrentInterfaceLevel = 4;
                            }

                            // Finish Operation
                            if (ButtonInvokedInformation[1] == '4')
                            {
                                User.EditHabit(UserFolderPath, int.Parse(ButtonInvokedInformation[3..]), true);
                                UpdateInformation();
                                ToggleSpecificGraphicElement("000", true,
                                   CurrentInterfaceIndexSelectorY.ToString(), ButtonInvokedInformation[2] == '1');
                                CurrentInterfaceIndexSelectorY = 0;
                                CurrentInterfaceIndexSelectorX = 0;
                                CurrentInterfaceLevel = 1;
                            }
                        }

                        // Habit Edit Invoke
                        else if (ButtonInvokedInformation[0] == '4')
                        {
                            string difficultyString = ButtonInvokedInformation[41..49].Replace("~", "");
                            string habitName = ButtonInvokedInformation[1..41].Replace("~", "");
                            if (!Enum.TryParse(difficultyString, out HabitBoostDifficulty difficulty) || habitName == "")
                            {
                                if (habitName == "")
                                {
                                    AddBottomMessage("Empty Habit Name!", true);
                                }

                                if (!Enum.TryParse(difficultyString, out HabitBoostDifficulty _))
                                {
                                    AddBottomMessage("Invalid Difficulty! (Easy/Medium/Hard/VeryHard) Only!", true);
                                }
                                ClearInputFields();
                            }
                            else
                            {
                                if (ButtonInvokedInformation[49] == '0')
                                {
                                    User.EditHabit(UserFolderPath, int.Parse(ButtonInvokedInformation[50..]), ButtonInvokedInformation[1..41].Replace("~", ""), difficulty);
                                }
                                else
                                {
                                    User.AddHabit(HabitBoostFolderPath, habitName, difficulty);
                                }
                                ToggleSpecificGraphicElement("000", true,
                                    CurrentInterfaceIndexSelectorY.ToString(), true);
                                CurrentInterfaceIndexSelectorY = 0;
                                CurrentInterfaceIndexSelectorX = 0;
                                CurrentInterfaceLevel = 1;
                            }
                        }

                        // Create User Invoke
                        else if (ButtonInvokedInformation[0] == '5')
                        {
                            string username = ButtonInvokedInformation[1..30].Replace("~", "");
                            string password = ButtonInvokedInformation[31..40].Replace("~", "");
                            if (DoesUserExists(username))
                            {
                                AddBottomMessage("User Already Exists!", true);
                                ClearInputFields();
                            }
                            else if (password == "" || username == "")
                            {
                                if (username == "") { AddBottomMessage("Empty Username!", true); }
                                if (password == "") { AddBottomMessage("Empty Password!", true); }
                                ClearInputFields();
                            }
                            else
                            {
                                CreateUser(username, password);
                                User.ReadData(UserFolderPath + @"\" + username);
                                SwitchScreen(ProgramScreen.Main);
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
                    if (CurrentActiveButton != null)
                    {
                        CurrentInterfaceLevel = CurrentActiveButton.MenuInterfaceLevel;
                        CurrentInterfaceIndexSelectorY = CurrentActiveButton.InterfaceIndexY;
                    }
                }
                else if (CurrentProgramState == ProgramState.Browse)
                {
                    string currentActiveGraphicInfoToken = GetCurrentActiveGraphicElement(GetCurrentActiveScene())?.InfoToken ?? "";
                    bool isEscapeInfoPresent =
                        !char.IsWhiteSpace(currentActiveGraphicInfoToken[2]) &&
                        !char.IsWhiteSpace(currentActiveGraphicInfoToken[3]) &&
                        !char.IsWhiteSpace(currentActiveGraphicInfoToken[4]);
                    if (isEscapeInfoPresent)
                    {
                        ToggleSpecificGraphicElement(currentActiveGraphicInfoToken[2..5], true,
                            CurrentInterfaceIndexSelectorY.ToString(), true);
                        CurrentInterfaceIndexSelectorY = currentActiveGraphicInfoToken[5] - '0';
                        CurrentInterfaceIndexSelectorX = currentActiveGraphicInfoToken[6] - '0';
                        CurrentInterfaceLevel = currentActiveGraphicInfoToken[7] - '0';
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
        }
    }
}
