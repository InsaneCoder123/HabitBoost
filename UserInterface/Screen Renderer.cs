using Habit_User_Data_Structures;

namespace UserInterface
{
    public enum ProgramScreen
    {
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


        public static int ScreenWidth { get; set; } = 100;
        public static int ScreenHeight { get; set; } = 26;

        public static int CurrentInterfaceLevel { get; set; } = 0;
        public static int CurrentInterfaceIndexSelectorY { get; set; } = 0;
        public static int CurrentInterfaceIndexSelectorX { get; set; } = 0;
        public static string UserInputStreamString { get; set; } = "";

        public static ProgramState CurrentProgramState { get; set; } = ProgramState.Browse;
        public static ProgramScreen CurrentProgramScreen { get; set; } = ProgramScreen.MainMenu;

        public static ConsoleKeyInfo UserInputStream { get; set; }

        #region Graphics

        public static LoginGraphic LoginGraphic { get; set; } = new LoginGraphic();

        public static MainMenuGraphic MainMenuGraphic { get; set; } = new MainMenuGraphic();

        public static TopBarGraphics TopBarGraphics { get; set; } = new TopBarGraphics();
        public static HabitListGraphics HabitListGraphics { get; set; } = new HabitListGraphics();
        public static HabitOptionGraphics HabitOptionGraphics { get; set; } = new HabitOptionGraphics();
        #endregion

        public static GraphicElement? currentGraphicElement { get; set; }
        public static Button? currentActiveButton { get; set; }

        public static List<GraphicElement> MainMenuScene = [];
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

            LoginGraphic.AbsolutePositionX = 4;
            LoginGraphic.AbsolutePositionY = 2;
            LoginGraphic.IsGraphicElementVisible = false;

            MainMenuGraphic.AbsolutePositionX = 4;
            MainMenuGraphic.AbsolutePositionY = 10;
            MainMenuGraphic.IsGraphicElementActive = true;
            MainMenuGraphic.IsGraphicElementVisible = true;

            TopBarGraphics.AbsolutePositionX = 1;
            TopBarGraphics.AbsolutePositionY = 1;
            TopBarGraphics.IsGraphicElementVisible = false;

            HabitListGraphics.AbsolutePositionX = 1;
            HabitListGraphics.AbsolutePositionY = 4;
            HabitListGraphics.IsDynamic = true;
            HabitListGraphics.AdjustVariableData(ref user);

            HabitOptionGraphics.AbsolutePositionX = 75;
            HabitOptionGraphics.AbsolutePositionY = 4;


            MainMenuScene.Add(MainMenuGraphic);

            LoginScene.Add(LoginGraphic);

            MainScene.Add(TopBarGraphics);
            MainScene.Add(HabitListGraphics);
            MainScene.Add(HabitOptionGraphics);

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

        public static void ToggleSpecificGraphicElement(string ID, bool toggle, string token, bool disableCurrentElement = false)
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
                    }
                    nextGraphicElement.IsGraphicElementActive = toggle;
                    nextGraphicElement.IsGraphicElementVisible = toggle;


                    string infoToken = nextGraphicElement.InfoToken;
                    nextGraphicElement.InfoToken = token + infoToken[1..];
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
            for (int y = 0; y < ScreenHeight; y++)
            {
                for (int x = 0; x < ScreenWidth; x++)
                {
                    if (y == 0 || y == ScreenHeight - 1 || x == 0 || x == ScreenWidth - 1)
                    {
                        CustomDisplay.DisplayColor(ConsoleColor.Green);
                        continue;
                    }
                    else if (AtGraphicElement(x, y, GetCurrentActiveScene()))
                    {
                        continue;
                    }
                    else { Console.Write(" "); }
                }
                Console.WriteLine();
            }
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
                    { CustomDisplay.DisplayColoredText(" ", ConsoleColor.Green); }
                    else if (graphicElement.Graphic[graphicIndex] == '%') 
                    {
                        if (graphicElement.InputFields != null)
                        {
                            foreach (InputField inputField in graphicElement.InputFields)
                            {
                                if (inputField.InterfaceIndexY == CurrentInterfaceIndexSelectorY && inputField.MenuInterfaceLevel == CurrentInterfaceLevel)
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
                                if (buttons.InterfaceIndexY == CurrentInterfaceIndexSelectorY && buttons.MenuInterfaceLevel == CurrentInterfaceLevel)
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
    }
}
