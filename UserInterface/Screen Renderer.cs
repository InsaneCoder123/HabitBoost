namespace UserInterface
{
    public static class ScreenRenderer
    {
        public enum ProgramState
        {
            Browse,
            Edit,
            View
        }

        public enum ProgramScreen
        {
            Login,
            Main
        }

        public static int ScreenWidth { get; set; } = 100;
        public static int ScreenHeight { get; set; } = 20;

        public static int CurrentInterfaceLevel { get; set; } = 0;
        public static int CurrentInterfaceIndexSelector { get; set; } = 2;
        public static string UserInputStreamString { get; set; } = "";

        public static ProgramState CurrentProgramState { get; set; } = ProgramState.Browse;
        public static ConsoleKeyInfo UserInputStream { get; set; }
        public static ProgramScreen CurrentProgramScreen { get; set; } = ProgramScreen.Login;

        public static LoginGraphic LoginGraphic { get; set; } = new LoginGraphic();

        public static List<GraphicElement> GraphicElement { get; set; } = new List<GraphicElement>();

        public static void UserInput()
        {
            UserInputStreamString = "";
            UserInputStream = Console.ReadKey(intercept: true);
            UserInputStreamString = UserInputStream.KeyChar.ToString();

            if (UserInputStream.Key == ConsoleKey.UpArrow && CurrentInterfaceIndexSelector != 0)
            {
                UserInputStreamString = "";
                --CurrentInterfaceIndexSelector;
            }
            else if (UserInputStream.Key == ConsoleKey.DownArrow)
            {
                UserInputStreamString = "";
                ++CurrentInterfaceIndexSelector;
            }
            else if (UserInputStream.Key == ConsoleKey.Enter)
            {
                UserInputStreamString = "";
                ++CurrentInterfaceLevel;
                CurrentInterfaceIndexSelector = 0;
            }
            else
            {
                foreach (GraphicElement graphicElement in GraphicElement)
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
                                else if (UserInputStream.Key == ConsoleKey.Enter)
                                {
                                    UserInputStreamString = "";
                                    ++CurrentInterfaceLevel;
                                    CurrentInterfaceIndexSelector = 0;
                                }
                                else
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

        public static void RenderScreen()
        {
            switch (CurrentProgramScreen)
            {
                case ProgramScreen.Login:
                    RenderLoginScreen();
                    break;
                case ProgramScreen.Main:
                    RenderMainScreen();
                    break;
            }
        }

        public static void InitiateGraphics()
        {
            Console.CursorVisible = false;

            LoginGraphic.AbsolutePositionX = 4;
            LoginGraphic.AbsolutePositionY = 2;
            LoginGraphic.IsGraphicElementActive = true;
            GraphicElement.Add(LoginGraphic);
        }

        public static bool AtGraphicElement(int x, int y)
        {
            foreach (GraphicElement graphicElement in GraphicElement)
            {
                if (x == graphicElement.AbsolutePositionX + graphicElement.RenderPointerX &&
                    y == graphicElement.AbsolutePositionY + graphicElement.RenderPointerY)
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

        public static void RenderLoginScreen()
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
                    else if (AtGraphicElement(x, y))
                    {
                        continue;
                    }
                    else { Console.Write(" ");}
                }
                Console.WriteLine();
            }
        }

        public static void RenderMainScreen()
        {

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
