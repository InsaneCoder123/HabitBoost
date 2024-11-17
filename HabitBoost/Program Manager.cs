using Habit_User_Data_Structures;
using System.ComponentModel.Design;
using System.Xml;
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
            string[] dummyData = ["1", "0"];
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
                UserData.ProgramMessage.Add(message + "$");
            }
            else
            {
                UserData.ProgramMessage.Add(message + "#");
            }
            SyncScreenHeightToProgramMessage();
        }

        public static void SyncScreenHeightToProgramMessage()
        {
            if (UserData.ProgramMessage.Count != OriginalScreenHeight - ScreenHeight)
            {
                ScreenHeight = OriginalScreenHeight + UserData.ProgramMessage.Count;
            }
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
            UpdateInformation();
            UserInputStreamString = "";
            UserInputStream = Console.ReadKey(intercept: true);
            UserInputStreamString = UserInputStream.KeyChar.ToString();          
            GraphicElement CurrentActiveGraphic = GetCurrentActiveGraphicElement(GetCurrentActiveScene())!;

            static int[] GetMaxInterfaceIndex(GraphicElement graphicElement, int InterfaceLevel)
            {
                return [graphicElement.MaxIndexPerInterface![InterfaceLevel][0], graphicElement.MaxIndexPerInterface[InterfaceLevel][1]];
            }

            if (UserInputStream.Key == ConsoleKey.UpArrow && CurrentInterfaceIndexSelectorY != 0)
            {
                UserInputStreamString = "";
                if (CurrentProgramState == ProgramState.Browse)
                { --CurrentInterfaceIndexSelectorY; }
            }
            else if (UserInputStream.Key == ConsoleKey.UpArrow && CurrentInterfaceIndexSelectorY == 0)
            {
                HabitEditInterfaceGraphics.StartingIndex = Math.Max(0, HabitEditInterfaceGraphics.StartingIndex - 1);
            }
            else if (UserInputStream.Key == ConsoleKey.DownArrow &&
                CurrentInterfaceIndexSelectorY != GetMaxInterfaceIndex(CurrentActiveGraphic, CurrentInterfaceLevel)[1])
            {
                UserInputStreamString = "";
                if (CurrentProgramState == ProgramState.Browse)
                { ++CurrentInterfaceIndexSelectorY; }
            }
            else 
            if (UserInputStream.Key == ConsoleKey.DownArrow &&
                CurrentInterfaceIndexSelectorY == GetMaxInterfaceIndex(CurrentActiveGraphic, CurrentInterfaceLevel)[1])
            {
                HabitEditInterfaceGraphics.StartingIndex = Math.Min(9, HabitEditInterfaceGraphics.StartingIndex + 1);
            }
            else if (UserInputStream.Key == ConsoleKey.LeftArrow && CurrentInterfaceIndexSelectorX != 0)
            {
                UserInputStreamString = "";
                if (CurrentProgramState == ProgramState.Browse)
                { --CurrentInterfaceIndexSelectorX; }
            }
            else if (UserInputStream.Key == ConsoleKey.RightArrow &&
                CurrentInterfaceIndexSelectorX != GetMaxInterfaceIndex(CurrentActiveGraphic, CurrentInterfaceLevel)[0])
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

                static void ClearInputFields()
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
                            ToggleSpecificGraphicElement(ButtonInvokedInformation[2..5], true,
                                    CurrentInterfaceIndexSelectorY.ToString(), true);
                            CurrentInterfaceIndexSelectorY = 0;
                            CurrentInterfaceIndexSelectorX = 0;
                            CurrentInterfaceLevel = 1;
                        }
                        // Login Invoke
                        else if (ButtonInvokedInformation[0] == '0')
                        {
                            User = new UserData();
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
                            else
                            {
                                AddBottomMessage("Invalid Username or Password!", true);
                                ClearInputFields();
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
                                CurrentInterfaceIndexSelectorY.ToString(), ButtonInvokedInformation[8] == '1', ButtonInvokedInformation[9] == '1',
                                ButtonInvokedInformation[10] == '1', ButtonInvokedInformation[11] == '1');
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
                                        CurrentInterfaceIndexSelectorY.ToString(), ["000", "001"]);
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
                                if (!User.HabitList[int.Parse(ButtonInvokedInformation[3..])].Completed)
                                {
                                    User.RewardExperience(User.HabitList[int.Parse(ButtonInvokedInformation[3..])].Difficulty, UserFolderPath + @"\" + User.Username);
                                    User.EditHabit(UserFolderPath, int.Parse(ButtonInvokedInformation[3..]), true, DateTime.Now);
                                    UpdateInformation();
                                    ToggleSpecificGraphicElement("000", true,
                                       CurrentInterfaceIndexSelectorY.ToString(), ButtonInvokedInformation[2] == '1');
                                    CurrentInterfaceIndexSelectorY = 0;
                                    CurrentInterfaceIndexSelectorX = 0;
                                    CurrentInterfaceLevel = 1;
                                }
                                else
                                {
                                    AddBottomMessage("Habit already finished!", true);
                                }
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
                                User.AddAction(UserFolderPath + @"\" + User.Username, DateTime.Now, 1);
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

                        // Journal Operation Invoke
                        else if (ButtonInvokedInformation[0] == '6')
                        {
                            // Add Operation
                            if (ButtonInvokedInformation[1] == '1')
                            {
                                ToggleSpecificGraphicElement("006", true,
                                    CurrentInterfaceIndexSelectorY.ToString(), ["004", "005"]);
                                CurrentInterfaceIndexSelectorY = 0;
                                CurrentInterfaceIndexSelectorX = 0;
                                CurrentInterfaceLevel = 4;

                            }

                            // Delete Operation
                            if (ButtonInvokedInformation[1] == '2')
                            {
                                User.DeleteJournalEntry(UserFolderPath, int.Parse(ButtonInvokedInformation[3..]));
                                UpdateInformation();
                                if (HabitEditInterfaceGraphics.IsJournalListEmpty)
                                {
                                    ToggleSpecificGraphicElement("003", true,
                                        CurrentInterfaceIndexSelectorY.ToString(), ButtonInvokedInformation[2] == '1');
                                    CurrentInterfaceIndexSelectorY = 0;
                                    CurrentInterfaceIndexSelectorX = 1;
                                    CurrentInterfaceLevel = 0;
                                }
                                else
                                {
                                    ToggleSpecificGraphicElement("004", true,
                                        CurrentInterfaceIndexSelectorY.ToString(), ButtonInvokedInformation[2] == '1');
                                    CurrentInterfaceIndexSelectorY = 0;
                                    CurrentInterfaceIndexSelectorX = 0;
                                    CurrentInterfaceLevel = 1;
                                }
                            }

                            // Edit Operation
                            if (ButtonInvokedInformation[1] == '3')
                            {
                                ToggleSpecificGraphicElement("006", true,
                                   CurrentInterfaceIndexSelectorY.ToString() + "2", ["004", "005"], false);
                                CurrentInterfaceIndexSelectorY = 0;
                                CurrentInterfaceIndexSelectorX = 0;
                                CurrentInterfaceLevel = 4;
                            }

                            // View Operation
                            if (ButtonInvokedInformation[1] == '4')
                            {
                                ToggleSpecificGraphicElement("007", true,
                                   CurrentInterfaceIndexSelectorY.ToString() + "0", ["004", "005"], ButtonInvokedInformation[2] == '1');
                                CurrentInterfaceIndexSelectorY = 0;
                                CurrentInterfaceIndexSelectorX = 0;
                                CurrentInterfaceLevel = 4;
                            }
                        }

                        // Journal Edit Invoke
                        else if (ButtonInvokedInformation[0] == '7')
                        {
                            string journalEntry = ButtonInvokedInformation[31..531].Replace("~", "");
                            string journalTitle = ButtonInvokedInformation[1..31].Replace("~", "");
                            if (journalTitle == "" || journalEntry == "")
                            {
                                if (journalTitle == "")
                                {
                                    AddBottomMessage("Empty Journal Title!", true);
                                }
                                if (journalEntry == "")
                                {
                                    AddBottomMessage("Empty Habit Name!", true);
                                }
                                ClearInputFields();
                            }
                            else
                            {
                                if (ButtonInvokedInformation[531] == '0')
                                {
                                    User.EditJournalEntry(UserFolderPath, int.Parse(ButtonInvokedInformation[532..]), ButtonInvokedInformation[1..31].Replace("~", ""), ButtonInvokedInformation[31..501].Replace("~", ""));
                                }
                                else
                                {
                                    User.AddJournalEntry(HabitBoostFolderPath, journalTitle, journalEntry);
                                    User.RewardExperience(HabitBoostDifficulty.Easy, UserFolderPath + @"\" + User.Username);
                                }
                                ToggleSpecificGraphicElement("004", true,
                                CurrentInterfaceIndexSelectorY.ToString(), true);
                                CurrentInterfaceIndexSelectorY = 0;
                                CurrentInterfaceIndexSelectorX = 0;
                                CurrentInterfaceLevel = 1;
                            }

                        }

                        // Task Operation Invoke
                        else if (ButtonInvokedInformation[0] == '8')
                        {
                            // Add Operation
                            if (ButtonInvokedInformation[1] == '1')
                            {
                                ToggleSpecificGraphicElement("010", true,
                                    CurrentInterfaceIndexSelectorY.ToString(), ["009", "008"]);
                                CurrentInterfaceIndexSelectorY = 0;
                                CurrentInterfaceIndexSelectorX = 0;
                                CurrentInterfaceLevel = 4;
                            }

                            // Finish Then Delete Operation
                            if (ButtonInvokedInformation[1] == '2')
                            {
                                User.RewardExperience(User.TaskList[int.Parse(ButtonInvokedInformation[3..])].Difficulty, UserFolderPath + @"\" + User.Username);
                                User.DeleteTask(UserFolderPath, int.Parse(ButtonInvokedInformation[3..]));
                                UpdateInformation();
                                if (HabitEditInterfaceGraphics.IsToDoListEmpty)
                                {
                                    ToggleSpecificGraphicElement("003", true,
                                        CurrentInterfaceIndexSelectorY.ToString(), ["008", "009"]);
                                    CurrentInterfaceIndexSelectorY = 0;
                                    CurrentInterfaceIndexSelectorX = 2;
                                    CurrentInterfaceLevel = 0;
                                }
                                else
                                {
                                    ToggleSpecificGraphicElement("008", true,
                                        CurrentInterfaceIndexSelectorY.ToString(), ButtonInvokedInformation[2] == '1');
                                    CurrentInterfaceIndexSelectorY = 0;
                                    CurrentInterfaceIndexSelectorX = 0;
                                    CurrentInterfaceLevel = 1;
                                }
                            }

                            // Edit Operation
                            if (ButtonInvokedInformation[1] == '3')
                            {
                                ToggleSpecificGraphicElement("010", true,
                                   CurrentInterfaceIndexSelectorY.ToString() + "2", ["009", "008"], false);
                                CurrentInterfaceIndexSelectorY = 0;
                                CurrentInterfaceIndexSelectorX = 0;
                                CurrentInterfaceLevel = 4;
                            }
                        }

                        // Task Edit Invoke
                        else if (ButtonInvokedInformation[0] == '9')
                        {
                            string taskName = ButtonInvokedInformation[1..21].Replace("~", "");
                            string taskDueDate = ButtonInvokedInformation[21..31].Replace("~", "");
                            string taskDifficultyString = ButtonInvokedInformation[31..39].Replace("~", "");
                            string dateFormat = "dd/MM/yyyy";
                            if (taskName == "" || !DateTime.TryParseExact(taskDueDate, dateFormat, null, System.Globalization.DateTimeStyles.None, out DateTime dateValue) ||
                                !Enum.TryParse(taskDifficultyString, out HabitBoostDifficulty difficulty))
                            {
                                if (taskName == "")
                                {
                                    AddBottomMessage("Empty Task Title!", true);
                                }
                                if (!DateTime.TryParseExact(taskDueDate, dateFormat, null, System.Globalization.DateTimeStyles.None, out _))
                                {
                                    AddBottomMessage("Enter Due Date in Valid Format! dd/MM/yyyy", true);
                                }
                                if (!Enum.TryParse(taskDifficultyString, out HabitBoostDifficulty _))
                                {
                                    AddBottomMessage("Invalid Difficulty! (Easy/Medium/Hard/VeryHard) Only!", true);
                                }
                                ClearInputFields();
                            }
                            else
                            {
                                if (ButtonInvokedInformation[39] == '0')
                                {
                                    User.EditTask(UserFolderPath, int.Parse(ButtonInvokedInformation[39..]), taskName, difficulty, dateValue);
                                }
                                else
                                {
                                    User.AddTask(HabitBoostFolderPath, taskName, dateValue, difficulty);
                                }
                                ToggleSpecificGraphicElement("008", true,
                                CurrentInterfaceIndexSelectorY.ToString(), true);
                                CurrentInterfaceIndexSelectorY = 0;
                                CurrentInterfaceIndexSelectorX = 0;
                                CurrentInterfaceLevel = 1;
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
                    GraphicElement? currentActiveGraphicElement = GetCurrentActiveGraphicElement(GetCurrentActiveScene());
                    string currentActiveGraphicInfoToken = currentActiveGraphicElement?.InfoToken ?? "";
                    bool isEscapeInfoPresent =
                        !char.IsWhiteSpace(currentActiveGraphicInfoToken[2]) &&
                        !char.IsWhiteSpace(currentActiveGraphicInfoToken[3]) &&
                        !char.IsWhiteSpace(currentActiveGraphicInfoToken[4]);
                    if (isEscapeInfoPresent)
                    {
                        GraphicElement currentActiveGraphic = GetCurrentActiveGraphicElement(GetCurrentActiveScene())!;
                        if (HabitEditInterfaceGraphics.IsToDoListEmpty && currentActiveGraphic.ID == "010")
                        {
                            ToggleSpecificGraphicElement("003", true,
                            CurrentInterfaceIndexSelectorY.ToString(), true);
                            CurrentInterfaceIndexSelectorY = 0;
                            CurrentInterfaceIndexSelectorX = 2;
                            CurrentInterfaceLevel = 0;
                        }
                        else if (HabitEditInterfaceGraphics.IsHabitListEmpty && currentActiveGraphic.ID == "001")
                        {
                            ToggleSpecificGraphicElement("003", true,
                            CurrentInterfaceIndexSelectorY.ToString(), true);
                            CurrentInterfaceIndexSelectorY = 0;
                            CurrentInterfaceIndexSelectorX = 0;
                            CurrentInterfaceLevel = 0;
                        }
                        else if (HabitEditInterfaceGraphics.IsJournalListEmpty && currentActiveGraphic.ID == "007")
                        {
                            ToggleSpecificGraphicElement("003", true,
                            CurrentInterfaceIndexSelectorY.ToString(), true);
                            CurrentInterfaceIndexSelectorY = 0;
                            CurrentInterfaceIndexSelectorX = 1;
                            CurrentInterfaceLevel = 0;
                        }
                        else
                        {
                            ToggleSpecificGraphicElement(currentActiveGraphicInfoToken[2..5], true,
                                CurrentInterfaceIndexSelectorY.ToString(), true);
                            CurrentInterfaceIndexSelectorY = currentActiveGraphicInfoToken[0] - '0';
                            CurrentInterfaceIndexSelectorX = currentActiveGraphicInfoToken[6] - '0';
                            CurrentInterfaceLevel = currentActiveGraphicInfoToken[7] - '0';
                        }
                    }

                    if (currentActiveGraphicElement?.PreviousScene != ProgramScreen.None)
                    {
                        if (currentActiveGraphicElement?.PreviousScene != null)
                        {
                            SwitchScreen(currentActiveGraphicElement.PreviousScene);
                        }
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
            SyncScreenHeightToProgramMessage();
        }
    }
}
