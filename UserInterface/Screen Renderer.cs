using Habit_User_Data_Structures;

namespace UserInterface
{
    public enum ProgramScreen
    {
        None=0,
        MainMenu,
        Login,
        CreateUser,
        Main
    }
    public static class ScreenRenderer
    {
        public enum ProgramState
        {
            Browse,
            Edit,
            View
        }


        public static int ScreenWidth { get; set; } = 101;
        public static int ScreenHeight { get; set; } = 26;
        public static int OriginalScreenHeight { get; set; } = 26;

        public static int CurrentInterfaceLevel { get; set; } = 0;
        public static int CurrentInterfaceIndexSelectorY { get; set; } = 0;
        public static int CurrentInterfaceIndexSelectorX { get; set; } = 0;
        public static string UserInputStreamString { get; set; } = "";

        public static ProgramState CurrentProgramState { get; set; } = ProgramState.Browse;
        public static ProgramScreen CurrentProgramScreen { get; set; } = ProgramScreen.MainMenu;

        public static ConsoleKeyInfo UserInputStream { get; set; }


        public static GraphicElement? CurrentGraphicElement { get; set; }
        public static Button? CurrentActiveButton { get; set; }

        public static List<GraphicElement> MainMenuScene  = [];
        public static List<GraphicElement> LoginScene = [];
        public static List<GraphicElement> CreateUserScene = [];
        public static List<GraphicElement> MainScene = [];

        public static GraphicElement? GetCurrentActiveGraphicElement(List<GraphicElement> Scenes) 
        {
            foreach (GraphicElement scene in Scenes)
            {
                if (scene.IsGraphicElementActive) { return scene; }
            }
            return null;
        }

        public static ref List<GraphicElement> GetCurrentActiveScene()
        {
            switch (CurrentProgramScreen)
            {
                case ProgramScreen.MainMenu:
                    return ref MainMenuScene;
                case ProgramScreen.Login:
                    return ref LoginScene;
                case ProgramScreen.CreateUser:
                    return ref CreateUserScene;
                case ProgramScreen.Main:
                    return ref MainScene;
                default:
                    return ref MainMenuScene;
            }
        }

        public static bool IsLetterOrValidSymbol(char input)
        {
            string validSymbols = "!@#$%^&*()-_=+,.[];<>?/\\\'\"";
            if (!char.IsLetter(input) && !validSymbols.Contains(input) && !char.IsDigit(input)) { return false; }
            return true;
        }

        public static void InitiateGraphics(ref UserData user)
        {
            Console.CursorVisible = false;

            #region Main Menu Graphics
            MainMenuGraphic MainMenuGraphic = new()
            {
                AbsolutePositionX = 35,
                AbsolutePositionY = 10,
                IsGraphicElementActive = true,
                IsGraphicElementVisible = true
            };
            TitleGraphic TitleGraphic = new()
            {
                AbsolutePositionX = 9,
                AbsolutePositionY = 2,
                IsGraphicElementVisible = true
            };
            StatisticsGraphic StatisticsGraphic = new()
            {
                AbsolutePositionX = 12,
                AbsolutePositionY = 1,
                IsGraphicElementVisible = false,
                IsDynamic = true
            };
            MainMenuScene.Add(MainMenuGraphic);
            MainMenuScene.Add(TitleGraphic);
            MainMenuScene.Add(StatisticsGraphic);
            #endregion

            #region Login Graphics
            LoginGraphic LoginGraphic = new()
            {
                AbsolutePositionX = 35,
                AbsolutePositionY = 10,
                IsGraphicElementVisible = false
            };
            LoginScene.Add(LoginGraphic);
            LoginScene.Add(TitleGraphic);
            #endregion

            #region Create User Graphics
            CreateAccountGraphic CreateAccountGraphic = new()
            {
                AbsolutePositionX = 35,
                AbsolutePositionY = 10,
                IsGraphicElementVisible = false
            };
            CreateUserScene.Add(CreateAccountGraphic);
            CreateUserScene.Add(TitleGraphic);
            #endregion

            #region Main Graphics
            #region Habit
            TopBarGraphics TopBarGraphics = new()
            {
                AbsolutePositionX = 1,
                AbsolutePositionY = 1,
                IsGraphicElementVisible = false
            };

            HabitListGraphics HabitListGraphics = new()
            {
                AbsolutePositionX = 1,
                AbsolutePositionY = 4,
                IsDynamic = true
            };
            HabitListGraphics.AdjustVariableData(ref user);

            HabitEditInterfaceGraphics HabitEditInterfaceGraphics = new()
            {
                AbsolutePositionX = 70,
                AbsolutePositionY = 4,
                IsGraphicElementVisible = false
            };

            HabitOptionGraphics HabitOptionGraphics = new()
            {
                AbsolutePositionX = 75,
                AbsolutePositionY = 4
            };

            #endregion
            #region Journal
            JournalListGraphic JournalListGraphic = new()
            {
                AbsolutePositionX = 1,
                AbsolutePositionY = 4,
                IsDynamic = true
            };

            JournalOperations JournalOperations = new()
            {
                AbsolutePositionX = 65,
                AbsolutePositionY = 10,
                IsGraphicElementVisible = false
            };

            AddJournalInterfaceGraphics AddJournalInterfaceGraphics = new()
            {
                AbsolutePositionX = 2,
                AbsolutePositionY = 4,
                IsGraphicElementVisible = false
            };

            ViewJournalEntry ViewJournalEntry = new()
            {
                AbsolutePositionX = 2,
                AbsolutePositionY = 4,
                IsGraphicElementVisible = false,
                IsDynamic = true
            };
            #endregion
            #region Task
            TaskListGraphics TaskListGraphics = new()
            {
                AbsolutePositionX = 1,
                AbsolutePositionY = 4,
                IsDynamic = true
            };
            TaskOptionGraphics TaskOptionGraphics = new()
            {
                AbsolutePositionX = 79,
                AbsolutePositionY = 4
            };

            TaskEditInterface TaskEditInterface = new()
            {
                AbsolutePositionX = 40,
                AbsolutePositionY = 4,
                IsGraphicElementVisible = false
            };
            #endregion

            MainScene.Add(TopBarGraphics);
            MainScene.Add(HabitListGraphics);
            MainScene.Add(HabitOptionGraphics);
            MainScene.Add(HabitEditInterfaceGraphics);
            MainScene.Add(JournalListGraphic);
            MainScene.Add(JournalOperations);
            MainScene.Add(AddJournalInterfaceGraphics);
            MainScene.Add(ViewJournalEntry);
            MainScene.Add(TaskListGraphics);
            MainScene.Add(TaskOptionGraphics);
            MainScene.Add(TaskEditInterface);
            #endregion

        }

        public static void UpdateDynamicGraphics(ref UserData user)
        {
            foreach (GraphicElement graphicElement in GetCurrentActiveScene())
            {
                if (graphicElement.IsDynamic)
                {
                    graphicElement.AdjustVariableData(ref user);
                }
            }
        }

        public static void ToggleAllGraphicElements(ref List<GraphicElement> graphicElements, bool toggle) 
        {
            foreach (GraphicElement graphicElement in graphicElements)
            {
                if (graphicElement.IsGraphicElementVisibleDefault)
                graphicElement.IsGraphicElementVisible = toggle;
                if (!toggle) { graphicElement.IsGraphicElementActive = false; }
            }
        }

        public static void ToggleSpecificGraphicElement(string ID, bool toggle, string token, bool disableCurrentElement = false, 
            bool overwritePreviousInterfaceToken = true, bool activateAtSameXCoordinateAsActiveInterface = false, bool activateAtSameYCoordinateAsActiveInterface = false)
        {
            var nextGraphicElement = GetCurrentActiveScene().Find(x => x.ID == ID);
            var currentActiveGraphicElement = GetCurrentActiveGraphicElement(GetCurrentActiveScene());
            if (currentActiveGraphicElement != null)
            {
                var scene = GetCurrentActiveScene()?.Find(x => x.ID == currentActiveGraphicElement.ID);
                if (nextGraphicElement != null && scene != null)
                {
                    if (currentActiveGraphicElement != null)
                    {
                        scene.IsGraphicElementActive = false;
                        scene.IsGraphicElementVisible = !disableCurrentElement;

                        nextGraphicElement.IsGraphicElementActive = toggle;
                        nextGraphicElement.IsGraphicElementVisible = toggle;

                        Button? activeButton = currentActiveGraphicElement.GetCurrentActiveButton();
                        InputField? activeInputField = currentActiveGraphicElement.GetCurrentActiveInputField();


                        if ( activateAtSameXCoordinateAsActiveInterface)
                        {
                            if (activeButton != null)
                                nextGraphicElement.AbsolutePositionX = activeButton.XPosition + currentActiveGraphicElement.AbsolutePositionX - 1;
                            else if (activeInputField != null)
                                nextGraphicElement.AbsolutePositionX = activeInputField.XPosition + currentActiveGraphicElement.AbsolutePositionX - 1;
                        }
                        if (activateAtSameYCoordinateAsActiveInterface)
                        {
                            if (activeButton != null)
                                nextGraphicElement.AbsolutePositionY = activeButton.YPosition + currentActiveGraphicElement.AbsolutePositionY - 1;
                            else if (activeInputField != null)
                                nextGraphicElement.AbsolutePositionY = activeInputField.YPosition + currentActiveGraphicElement.AbsolutePositionY - 1;
                        }


                        string infoToken = nextGraphicElement.InfoToken;
                        if (overwritePreviousInterfaceToken)
                        {
                            nextGraphicElement.InfoToken = token.PadRight(2, ' ') + infoToken[2..];
                        }
                        else
                        {
                            nextGraphicElement.InfoToken = currentActiveGraphicElement.InfoToken[0].ToString() + token[1].ToString() + infoToken[2..];
                        }
                    }
                }
            }
        }

        public static void ToggleSpecificGraphicElement(string ID, bool toggle, string token, string[] graphicToBeDisabledID,
            bool overwritePreviousInterfaceToken = true, bool activateAtSameXCoordinateAsActiveInterface = false, bool activateAtSameYCoordinateAsActiveInterface = false)
        {
            var nextGraphicElement = GetCurrentActiveScene().Find(x => x.ID == ID);
            var currentActiveGraphicElement = GetCurrentActiveGraphicElement(GetCurrentActiveScene());
            if (currentActiveGraphicElement != null)
            {
                foreach (string graphicID in graphicToBeDisabledID)
                {
                    var scene = GetCurrentActiveScene()?.Find(x => x.ID == graphicID);
                    if (scene != null)
                    {
                        scene.IsGraphicElementActive = false;
                        scene.IsGraphicElementVisible = false;
                    }
                    currentActiveGraphicElement.IsGraphicElementActive = false;
                }
                if (nextGraphicElement != null)
                {
                    if (currentActiveGraphicElement != null)
                    {
                        nextGraphicElement.IsGraphicElementActive = toggle;
                        nextGraphicElement.IsGraphicElementVisible = toggle;

                        Button? activeButton = currentActiveGraphicElement.GetCurrentActiveButton();
                        InputField? activeInputField = currentActiveGraphicElement.GetCurrentActiveInputField();


                        if (activateAtSameXCoordinateAsActiveInterface)
                        {
                            if (activeButton != null)
                                nextGraphicElement.AbsolutePositionX = activeButton.XPosition + currentActiveGraphicElement.AbsolutePositionX - 1;
                            else if (activeInputField != null)
                                nextGraphicElement.AbsolutePositionX = activeInputField.XPosition + currentActiveGraphicElement.AbsolutePositionX - 1;
                        }
                        if (activateAtSameYCoordinateAsActiveInterface)
                        {
                            if (activeButton != null)
                                nextGraphicElement.AbsolutePositionY = activeButton.YPosition + currentActiveGraphicElement.AbsolutePositionY - 1;
                            else if (activeInputField != null)
                                nextGraphicElement.AbsolutePositionY = activeInputField.YPosition + currentActiveGraphicElement.AbsolutePositionY - 1;
                        }


                        string infoToken = nextGraphicElement.InfoToken;
                        if (overwritePreviousInterfaceToken)
                        {
                            nextGraphicElement.InfoToken = token.PadRight(2, ' ') + infoToken[2..];
                        }
                        else
                        {
                            nextGraphicElement.InfoToken = currentActiveGraphicElement.InfoToken[0].ToString() + token[1].ToString() + infoToken[2..];
                        }
                    }
                }
            }
        }

        public static void SwitchScreen(ProgramScreen programScreen)
        {
            // Set all scenes to false first
            List<GraphicElement> mainMenuScene = MainMenuScene;
            List<GraphicElement> loginScene = LoginScene;
            List<GraphicElement> createUserScene = CreateUserScene;
            List<GraphicElement> mainScene = MainScene;

            ToggleAllGraphicElements(ref mainMenuScene, false);
            ToggleAllGraphicElements(ref loginScene, false);
            ToggleAllGraphicElements(ref createUserScene, false);
            ToggleAllGraphicElements(ref mainScene, false);

            CurrentInterfaceIndexSelectorX = 0;
            CurrentInterfaceIndexSelectorY = 0;
            CurrentInterfaceLevel = 0;

            // Then set the selected scene to true
            switch (programScreen)
            {
                case ProgramScreen.MainMenu:
                    ToggleAllGraphicElements(ref mainMenuScene, true);
                    CurrentProgramScreen = ProgramScreen.MainMenu; // Update the property if modified
                    mainMenuScene[0].IsGraphicElementActive = true;
                    MainMenuScene = mainMenuScene; // Update the property if modified
                    break;
                case ProgramScreen.Login:
                    ToggleAllGraphicElements(ref loginScene, true);
                    CurrentProgramScreen = ProgramScreen.Login;
                    loginScene[0].IsGraphicElementActive = true;
                    LoginScene = loginScene;
                    break;
                case ProgramScreen.CreateUser:
                    ToggleAllGraphicElements(ref createUserScene, true);
                    CurrentProgramScreen = ProgramScreen.CreateUser;
                    createUserScene[0].IsGraphicElementActive = true;
                    CreateUserScene = createUserScene;
                    break;
                case ProgramScreen.Main:
                    ToggleAllGraphicElements(ref mainScene, true);
                    CurrentProgramScreen = ProgramScreen.Main;
                    mainScene[0].IsGraphicElementActive = true;
                    MainScene = mainScene;
                    break;
                default:
                    ToggleAllGraphicElements(ref mainMenuScene, true);
                    CurrentProgramScreen = ProgramScreen.MainMenu;
                    mainMenuScene[0].IsGraphicElementActive = true;
                    MainMenuScene = mainMenuScene;
                    break;
            }
        }

        public static void RenderScreen(ref UserData user)
        {
            Console.Clear();
            UpdateDynamicGraphics(ref user);
            int modifier = 1;
            for (int y = 0; y < ScreenHeight; y++)
            {
                for (int x = 0; x < ScreenWidth; x++)
                {

                    if (x == 0 && y == 0)
                    {
                        CustomDisplay.DisplayColoredText("┌", ConsoleColor.Green);
                        continue;
                    }

                    else if (x == ScreenWidth - 1 && y == 0)
                    {
                        CustomDisplay.DisplayColoredText("┐", ConsoleColor.Green);
                        continue;
                    }
                    else if (x == 0 && y == ScreenHeight - 1)
                    {
                        CustomDisplay.DisplayColoredText("└", ConsoleColor.Green);
                        continue;
                    }
                    else if (x == ScreenWidth - 1 && y == ScreenHeight - 1)
                    {
                        CustomDisplay.DisplayColoredText("┘", ConsoleColor.Green);
                        continue;
                    }
                    else if (x == 0 || x == ScreenWidth - 1)
                    {
                        CustomDisplay.DisplayColoredText("│", ConsoleColor.Green);
                        continue;
                    }
                    else if (y == 0 || y == ScreenHeight - 1)
                    {
                        CustomDisplay.DisplayColoredText("─", ConsoleColor.Green);
                        continue;
                    }
                    else if (AtGraphicElement(x, y, GetCurrentActiveScene()))
                    {
                        continue;
                    }                   
                    else if (x == 2 && y == ScreenHeight - ((ScreenHeight - OriginalScreenHeight) + modifier))
                    {
                        if (UserData.ProgramMessage.Count != 0)
                        {
                            if (UserData.ProgramMessage[0][^1] == '$')
                            {
                                CustomDisplay.DisplayColoredText(UserData.ProgramMessage[0][..^1], ConsoleColor.Red);
                            }
                            else if (UserData.ProgramMessage[0][^1] == '#')
                            {
                                CustomDisplay.DisplayColoredText(UserData.ProgramMessage[0][..^1], ConsoleColor.White);
                            }
                            x += UserData.ProgramMessage[0].Length - 2;
                            UserData.ProgramMessage.RemoveAt(0);
                            --modifier;
                            continue;
                        }
                        Console.Write(" ");
                    }
                    else 
                    { 
                        Console.Write(" "); 
                    }
                }
                Console.WriteLine();
            }
            ScreenHeight = OriginalScreenHeight;
        }


        public static bool AtGraphicElement(int x, int y, List<GraphicElement> CurrentActiveScene)
        {
            foreach (GraphicElement graphicElement in CurrentActiveScene)
            {
                if (x == graphicElement.AbsolutePositionX + graphicElement.RenderPointerX &&
                    y == graphicElement.AbsolutePositionY + graphicElement.RenderPointerY && graphicElement.IsGraphicElementVisible == true)
                {
                    int graphicIndex = (graphicElement.RenderPointerY * graphicElement.MaxWidth) + graphicElement.RenderPointerX;

                    if (graphicElement.Graphic[graphicIndex] == '@')
                    { CustomDisplay.DisplayColoredText("+", ConsoleColor.White); }
                    else if (graphicElement.Graphic[graphicIndex] == '%') 
                    {
                        if (graphicElement.InputFields != null)
                        {
                            foreach (InputField inputField in graphicElement.InputFields)
                            {
                                if (inputField.InterfaceIndexY == CurrentInterfaceIndexSelectorY && inputField.MenuInterfaceLevel == CurrentInterfaceLevel &&
                                    inputField.InterfaceIndexX == CurrentInterfaceIndexSelectorX)
                                { inputField.IsInterfaceSelected = true; }
                                else { inputField.IsInterfaceSelected = false; }
                                inputField.RenderInputField(graphicElement.RenderPointerX, graphicElement.RenderPointerY);
                            }
                        }
                    }
                    else if (graphicElement.Graphic[graphicIndex] == '&')
                    { 
                        if (graphicElement.Buttons != null)
                        {
                            foreach (Button buttons in graphicElement.Buttons)
                            {
                                if (buttons.InterfaceIndexY == CurrentInterfaceIndexSelectorY && buttons.MenuInterfaceLevel == CurrentInterfaceLevel
                                    && buttons.InterfaceIndexX == CurrentInterfaceIndexSelectorX)
                                { buttons.IsInterfaceSelected = true; }
                                else { buttons.IsInterfaceSelected = false; }
                                buttons.RenderButton(graphicElement.RenderPointerX, graphicElement.RenderPointerY, 
                                    graphicElement, graphicIndex, CurrentInterfaceIndexSelectorY, CurrentInterfaceIndexSelectorX, CurrentInterfaceLevel);
                            }
                        }
                    }
                    else if (graphicElement.Graphic[graphicIndex] == '.')
                    {
                        Console.Write(" ");
                    }
                    else if (graphicElement.Graphic[graphicIndex] == '+')
                    {
                        if (graphicElement.Labels != null)
                        {
                            foreach (VariableLabel variableLabel in graphicElement.Labels)
                            {
                                variableLabel.RenderLabel(graphicElement.RenderPointerX, graphicElement.RenderPointerY,
                                        graphicElement, graphicIndex);
                            }                      
                        }
                    }
                    else
                    {
                        Console.Write(graphicElement.Graphic[graphicIndex]);
                    }


                    if (graphicIndex == (graphicElement.MaxWidth * graphicElement.MaxHeight) - 1 ) 
                    { graphicElement.RenderPointerX = 0; graphicElement.RenderPointerY = 0;  return true; }

                    if ((graphicIndex+1) % graphicElement.MaxWidth == 0 && graphicIndex >= graphicElement.MaxWidth-1) 
                    { ++graphicElement.RenderPointerY; graphicElement.RenderPointerX = 0;  return true; }

                    ++graphicElement.RenderPointerX;

                    return true;
                }
            }
            return false;
        }
    }

    public static class CustomDisplay
    {
        public static void DisplayColor(ConsoleColor color)
        {
            Console.BackgroundColor = color;
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void DisplayColoredText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void DisplayHighlightedText(string text)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write(text);
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static string CenterString(string input, int totalWidth)
        {
            if (string.IsNullOrEmpty(input) || totalWidth <= input.Length)
                return input;

            int spaces = totalWidth - input.Length;
            int padLeft = spaces / 2;
            int padRight = spaces - padLeft;

            return new string(' ', padLeft) + input + new string(' ', padRight);
        }

    }


}
