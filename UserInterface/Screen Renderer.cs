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
        public static int ScreenHeight { get; set; } = 20;

        public static int CurrentInterfaceLevel { get; set; } = 0;
        public static int CurrentInterfaceIndexSelector { get; set; } = 0;
        public static string UserInputStreamString { get; set; } = "";

        public static ProgramState CurrentProgramState { get; set; } = ProgramState.Browse;
        public static ProgramScreen CurrentProgramScreen { get; set; } = ProgramScreen.MainMenu;

        public static ConsoleKeyInfo UserInputStream { get; set; }

        public static LoginGraphic LoginGraphic { get; set; } = new LoginGraphic();
        public static MainMenuGraphic MainMenuGraphic { get; set; } = new MainMenuGraphic();

        public static GraphicElement? currentGraphicElement { get; set; }
        public static Button? currentActiveButton { get; set; }

        public static List<GraphicElement> MainMenuScene { get; set; } = [];
        public static List<GraphicElement> LoginScene { get; set; } = [];
        public static List<GraphicElement> CreateUserScene { get; set; } = [];

        public static GraphicElement? GetCurrentActiveGraphicElement(List<GraphicElement> Scenes) 
        {
            foreach (GraphicElement scene in Scenes)
            {
                if (scene.IsGraphicElementActive) { return scene; }
            }
            return null;
        }

        public static List<GraphicElement> GetCurrentActiveScene()
        {
            switch (CurrentProgramScreen)
            {
                case ProgramScreen.MainMenu:
                    return MainMenuScene;
                case ProgramScreen.Login:
                    return LoginScene;
                case ProgramScreen.CreateUser:
                    return CreateUserScene;
                default:
                    return MainMenuScene;
            }
        }

        public static bool IsLetterOrValidSymbol(char input)
        {
            string validSymbols = "!@#$%^&*()-_=+,.[];<>?/\\\'\"";
            if (!char.IsLetter(input) && !validSymbols.Contains(input) && !char.IsDigit(input)) { return false; }
            return true;
        }

        public static void InitiateGraphics()
        {
            Console.CursorVisible = false;

            LoginGraphic.AbsolutePositionX = 4;
            LoginGraphic.AbsolutePositionY = 2;
            LoginGraphic.IsGraphicElementActive = false;

            MainMenuGraphic.AbsolutePositionX = 4;
            MainMenuGraphic.AbsolutePositionY = 10;
            MainMenuGraphic.IsGraphicElementActive = true;

            MainMenuScene.Add(MainMenuGraphic);

            LoginScene.Add(LoginGraphic);
        }

        public static void ToggleAllGraphicElements(ref List<GraphicElement> graphicElements, bool toggle) 
        {
            foreach (GraphicElement graphicElement in graphicElements)
            {
                graphicElement.IsGraphicElementActive = toggle;
            }
        }

        public static void SwitchScreen(ProgramScreen programScreen)
        {
            // Set all scenes to false first
            List<GraphicElement> mainMenuScene = MainMenuScene;
            List<GraphicElement> loginScene = LoginScene;
            List<GraphicElement> createUserScene = CreateUserScene;

            ToggleAllGraphicElements(ref mainMenuScene, false);
            ToggleAllGraphicElements(ref loginScene, false);
            ToggleAllGraphicElements(ref createUserScene, false);

            // Then set the selected scene to true
            switch (programScreen)
            {
                case ProgramScreen.MainMenu:
                    ToggleAllGraphicElements(ref mainMenuScene, true);
                    CurrentProgramScreen = ProgramScreen.MainMenu; // Update the property if modified
                    MainMenuScene = mainMenuScene; // Update the property if modified
                    break;
                case ProgramScreen.Login:
                    ToggleAllGraphicElements(ref loginScene, true);
                    CurrentProgramScreen = ProgramScreen.Login;
                    LoginScene = loginScene;
                    break;
                case ProgramScreen.CreateUser:
                    ToggleAllGraphicElements(ref createUserScene, true);
                    CurrentProgramScreen = ProgramScreen.CreateUser;
                    CreateUserScene = createUserScene;
                    break;
                default:
                    ToggleAllGraphicElements(ref mainMenuScene, true);
                    CurrentProgramScreen = ProgramScreen.MainMenu;
                    MainMenuScene = mainMenuScene;
                    break;
            }
        }

        public static void RenderScreen()
        {
            Console.Clear();
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
                    y == graphicElement.AbsolutePositionY + graphicElement.RenderPointerY && graphicElement.IsGraphicElementActive == true)
                {
                    int graphicIndex = (graphicElement.RenderPointerY * graphicElement.MaxWidth) + graphicElement.RenderPointerX;

                    if (graphicElement.Graphic[graphicIndex] == '@')
                    { CustomDisplay.DisplayColoredText("+", ConsoleColor.Green); }
                    else if (graphicElement.Graphic[graphicIndex] == '%') 
                    {
                        if (graphicElement.InputFields != null)
                        {
                            foreach (InputField inputField in graphicElement.InputFields)
                            {
                                if (inputField.InterfaceIndex == CurrentInterfaceIndexSelector && inputField.MenuInterfaceLevel == CurrentInterfaceLevel)
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
                                if (buttons.InterfaceIndex == CurrentInterfaceIndexSelector && buttons.MenuInterfaceLevel == CurrentInterfaceLevel)
                                { buttons.IsInterfaceSelected = true; }
                                else { buttons.IsInterfaceSelected = false; }
                                buttons.RenderButton(graphicElement.RenderPointerX, graphicElement.RenderPointerY, 
                                    graphicElement, graphicIndex, CurrentInterfaceIndexSelector, CurrentInterfaceLevel);
                            }
                        }
                    }
                    else if (graphicElement.Graphic[graphicIndex] == '.')
                    {
                        Console.Write(" ");
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
