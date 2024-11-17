using Habit_User_Data_Structures;

namespace UserInterface
{
    // @ = Green Color - Blank temporarily
    // . = Blank Space
    // % = Input Field
    // # = Next Line
    // $ = End
    // & = Button
    // + = Variable Label
    // Else = Button
    public abstract class GraphicElement
    {
        public string ID { get; set; } = "";

        public int RenderPointerX { get; set; } = 0;
        public int RenderPointerY { get; set; } = 0;

        public virtual int MaxWidth { get; set; } = 0;
        public virtual int MaxHeight { get; set; } = 0;
        public virtual int[][]? MaxIndexPerInterface { get; set; } // [[InterfaceLevel, MaxX, MaxY], [etc.]]

        public bool IsGraphicElementVisible { get; set; } = false;
        public bool IsGraphicElementVisibleDefault { get; set; } = true;
        public bool IsGraphicElementActive { get; set; } = false;
        public bool IsDynamic { get; set; } = false;

        public static bool IsHabitListEmpty { get; set; } = false;
        public static bool IsJournalListEmpty { get; set; } = false;
        public static bool IsToDoListEmpty { get; set; } = false;

        public static int StartingIndex { get; set; } = 0;

        public int AbsolutePositionX { get; set; } = 0;
        public int AbsolutePositionY { get; set; } = 0;

        public string InfoToken { get; set; } = "       "; // Token to be used to pass information from previus graphic to the next graphic
                                                           // 0 - Previous Interface Index Y 
                                                           // 1 - Add Mode/Edit Mode (1/2)
                                                           // 2-4 - Escape Key Graphic Element Linked to this graphic element
                                                           // 5 - Escape Graphic Starting Interface Index Y
                                                           // 6 - Escape Graphic Starting Interface Index X
                                                           // 7 - Escape Graphic Starting Interface Level
        public ProgramScreen PreviousScene { get; set; }

        public virtual string Graphic { get; set; } = "";
        public List<InputField>? InputFields { get; set; }
        public List<Button>? Buttons { get; set; }
        public List<VariableLabel>? Labels { get; set; }

        public InputField? GetCurrentActiveInputField()
        {
            if (InputFields == null) { return null; }
            foreach (InputField inputField in InputFields)
            {
                if (inputField.IsInterfaceSelected == true)
                {
                    return inputField;
                }
            }
            return null;
        }

        public Button? GetCurrentActiveButton()
        {
            if (Buttons == null) { return null; }
            foreach (Button button in Buttons)
            {
                if (button.IsInterfaceSelected == true)
                {
                    return button;
                }
            }
            return null;
        }

        public virtual void AdjustVariableData(ref UserData user)
        {
        }
    }

    #region Main Menu Scene Graphics
    public class MainMenuGraphic : GraphicElement
    {
        public override string Graphic { get; set; } =
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&&&&&&&&&&&&&&&&&&&@" + // USER LOGIN
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&&&&&&&&&&&&&&&&&&&@" + // CREATE ACCOUNT
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&&&&&&&&&&&&&&&&&&&@" + // View Statistics
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&&&&&&&&&&&&&&&&&&&@" + // Exit
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@";

        public override int MaxWidth { get; set; } = 28;
        public override int MaxHeight { get; set; } = 9;
        public override int[][]? MaxIndexPerInterface { get; set; } = [[0, 3]];

        public Button loginButton { get; set; } = new Button();
        public Button createUserButton { get; set; } = new Button();
        public Button StatisticsButton { get; set; } = new Button();
        public Button ExitButton { get; set; } = new Button();

        public MainMenuGraphic()
        {
            Buttons = [];
            ID = "012";

            loginButton.HorizontalLength = 26;
            loginButton.VerticalLength = 1;
            loginButton.XPosition = 1;
            loginButton.YPosition = 1;
            loginButton.MenuInterfaceLevel = 0;
            loginButton.ButtonText = "        USER LOGIN         ";
            loginButton.InterfaceIndexY = 0;
            loginButton.InterfaceIndexX = 0;
            loginButton.IsInvokable = true;
            loginButton.SetInvokedMethod(SwitchToLoginScreen);

            createUserButton.HorizontalLength = 26;
            createUserButton.VerticalLength = 1;
            createUserButton.XPosition = 1;
            createUserButton.YPosition = 3;
            createUserButton.MenuInterfaceLevel = 0;
            createUserButton.ButtonText = "      CREATE ACCOUNT      ";
            createUserButton.InterfaceIndexY = 1;
            createUserButton.InterfaceIndexX = 0;
            createUserButton.IsInvokable = true;
            createUserButton.SetInvokedMethod(SwitchToCreateAccountScreen);

            StatisticsButton.HorizontalLength = 26;
            StatisticsButton.VerticalLength = 1;
            StatisticsButton.XPosition = 1;
            StatisticsButton.YPosition = 5;
            StatisticsButton.MenuInterfaceLevel = 0;
            StatisticsButton.ButtonText = CustomDisplay.CenterString("STATISTICS", 26);
            StatisticsButton.InterfaceIndexY = 2;
            StatisticsButton.InterfaceIndexX = 0;
            StatisticsButton.IsInvokable = true;
            StatisticsButton.SetInvokedMethod(SwitchToStatisticScreen);

            ExitButton.HorizontalLength = 26;
            ExitButton.VerticalLength = 1;
            ExitButton.XPosition = 1;
            ExitButton.YPosition = 7;
            ExitButton.MenuInterfaceLevel = 0;
            ExitButton.ButtonText = CustomDisplay.CenterString("EXIT", 26);
            ExitButton.InterfaceIndexY = 3;
            ExitButton.InterfaceIndexX = 0;
            ExitButton.IsInvokable = true;
            ExitButton.SetInvokedMethod(SwitchToCreateAccountScreen);

            Buttons.Add(loginButton);
            Buttons.Add(createUserButton);
            Buttons.Add(StatisticsButton);
            Buttons.Add(ExitButton);
        }

        public string SwitchToLoginScreen()
        {
            return "1" + ((int)ProgramScreen.Login).ToString();
        }

        public string SwitchToCreateAccountScreen()
        {
            return "1" + ((int)ProgramScreen.CreateUser).ToString();
        }

        public string SwitchToStatisticScreen()
        {
            return "A  ";
        }
    }

    public class TitleGraphic : GraphicElement
    {
        public override string Graphic { get; set; } =
            "HH   HH     AAAAA    BBBBB   III  TTTTT      BBBBB   OOOOO   OOOOO  SSSSS  TTTTTTT" +
            "HH   HH    AA   AA   BB   B   II    T        BB   B  OO   OO OO   OO SS       T   " +
            "HHHHHHH   AAAAAAAAA  BBBBBB   II    T        BBBBBB  OO   OO OO   OO  SSSS    T   " +
            "HH   HH   AA     AA  BB   BB  II    T        BB   BB OO   OO OO   OO     SS   T   " +
            "HH   HH   AA     AA  BBBBBB  IIII   T        BBBBBB   OOOOO   OOOOO  SSSSS    T   ";

        public override int MaxWidth { get; set; } = 82;
        public override int MaxHeight { get; set; } = 5;
        public TitleGraphic()
        {
            ID = "013";
        }
    }

    public class StatisticsGraphic : GraphicElement
    {
        public override string Graphic { get; set; } =
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &&&&&&&&&&&& @ %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@                                                                          @" +
            "@                                                                          @" +
            "@    10 │ +  +  +  +  +  +  +       USERNAME   ++++++++++                  @" +
            "@  A  9 │ +  +  +  +  +  +  +       LEVEL      +++                         @" +
            "@  C  8 │ +  +  +  +  +  +  +       EXPERIENCE +++                         @" +
            "@  T  7 │ +  +  +  +  +  +  +                                              @" +
            "@  I  6 │ +  +  +  +  +  +  +       TOTAL NUMBER OF ACTIONS   +++          @" +
            "@  O  5 │ +  +  +  +  +  +  +       TOTAL NUMBER OF HABITS    +++          @" +
            "@  N  4 │ +  +  +  +  +  +  +       TOTAL NUMBER OF JOURNAL   +++          @" +
            "@  S  3 │ +  +  +  +  +  +  +       TOTAL NUMBER OF TASKS     +++          @" +
            "@     2 │ +  +  +  +  +  +  +                                              @" +
            "@     1 │ +  +  +  +  +  +  +                                              @" +
            "@       └──────────────────────                                            @" +
            "@         7  6  5  4  3  2  1                                              @" +
            "@           D A Y S  A G O       SCALE = +++++                             @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&& @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";
        public override int MaxWidth { get; set; } = 76;
        public override int MaxHeight { get; set; } = 21;
        public override int[][]? MaxIndexPerInterface { get; set; } = [[0, 1], [0, 1], [0, 1]];

        public Button UserNameInputbutton { get; set; } = new Button();
        public Button SearchButton { get; set; } = new Button();
        public InputField UserNameInputButton { get; set; } = new InputField();

        public VariableLabel UsernameLabel { get; set; } = new VariableLabel();
        public VariableLabel LevelLabel { get; set; } = new VariableLabel();
        public VariableLabel ExperienceLabel { get; set; } = new VariableLabel();
        public VariableLabel TotalActionsLabel { get; set; } = new VariableLabel();
        public VariableLabel TotalHabitsLabel { get; set; } = new VariableLabel();
        public VariableLabel TotalJournalLabel { get; set; } = new VariableLabel();
        public VariableLabel TotalTasksLabel { get; set; } = new VariableLabel();
        public VariableLabel GraphScale { get; set; } = new VariableLabel();


        public StatisticsGraphic()
        { 
            Buttons = [];
            InputFields = [];

            ID = "011";
            InfoToken = " 1012000";

            #region Static Graphics
            UserNameInputbutton.HorizontalLength = 12;
            UserNameInputbutton.VerticalLength = 1;
            UserNameInputbutton.XPosition = 2;
            UserNameInputbutton.YPosition = 1;
            UserNameInputbutton.MenuInterfaceLevel = 1;
            UserNameInputbutton.ButtonText = CustomDisplay.CenterString("USERNAME", 12);
            UserNameInputbutton.InterfaceIndexY = 0;
            UserNameInputbutton.InterfaceIndexX = 0;
            UserNameInputbutton.BindedInterface = UserNameInputButton;

            SearchButton.HorizontalLength = 72;
            SearchButton.VerticalLength = 1;
            SearchButton.XPosition = 2;
            SearchButton.YPosition = 19;
            SearchButton.MenuInterfaceLevel = 1;
            SearchButton.ButtonText = CustomDisplay.CenterString("SEARCH", 72);
            SearchButton.InterfaceIndexY = 1;
            SearchButton.InterfaceIndexX = 0;
            SearchButton.IsInvokable = true;
            SearchButton.SetInvokedMethod(SearchUser);

            UserNameInputButton.HorizontalLength = 57;
            UserNameInputButton.VerticalLength = 1;
            UserNameInputButton.XPosition = 17;
            UserNameInputButton.YPosition = 1;
            UserNameInputButton.MenuInterfaceLevel = 2;
            UserNameInputButton.FieldText = "";
            UserNameInputButton.InterfaceIndexY = 0;
            UserNameInputButton.InterfaceIndexX = 0;
            UserNameInputButton.MaxFieldTextLength = 10;

            Buttons.Add(UserNameInputbutton);
            Buttons.Add(SearchButton);
            InputFields.Add(UserNameInputButton);
            #endregion
            UsernameLabel.HorizontalLength = 10;
            UsernameLabel.VerticalLength = 1;
            UsernameLabel.XPosition = 47;
            UsernameLabel.YPosition = 5;

            LevelLabel.HorizontalLength = 3;
            LevelLabel.VerticalLength = 1;
            LevelLabel.XPosition = 47;
            LevelLabel.YPosition = 6;

            ExperienceLabel.HorizontalLength = 3;
            ExperienceLabel.VerticalLength = 1;
            ExperienceLabel.XPosition = 47;
            ExperienceLabel.YPosition = 7;

            TotalActionsLabel.HorizontalLength = 3;
            TotalActionsLabel.VerticalLength = 1;
            TotalActionsLabel.XPosition = 62;
            TotalActionsLabel.YPosition = 9;

            TotalHabitsLabel.HorizontalLength = 3;
            TotalHabitsLabel.VerticalLength = 1;
            TotalHabitsLabel.XPosition = 62;
            TotalHabitsLabel.YPosition = 10;

            TotalJournalLabel.HorizontalLength = 3;
            TotalJournalLabel.VerticalLength = 1;
            TotalJournalLabel.XPosition = 62;
            TotalJournalLabel.YPosition = 11;

            TotalTasksLabel.HorizontalLength = 3;
            TotalTasksLabel.VerticalLength = 1;
            TotalTasksLabel.XPosition = 62;
            TotalTasksLabel.YPosition = 12;

            GraphScale.HorizontalLength = 5;
            GraphScale.VerticalLength = 1;
            GraphScale.XPosition = 41;
            GraphScale.YPosition = 17;


        }

        public override void AdjustVariableData(ref UserData user)
        {
            #region Dynamic Graphics
            Labels = [];

            if (user.Username != null)
            {
                UsernameLabel.LabelText = user.Username.PadRight(10) ?? "".PadRight(10);
                LevelLabel.LabelText = user.Level.ToString().PadRight(3) ?? "".PadRight(3);
                ExperienceLabel.LabelText = user.Experience.ToString().PadRight(3) ?? "".PadRight(3);

                TotalActionsLabel.LabelText = user.Actions.Sum(pair => pair.Value).ToString().PadRight(3) ?? "".PadRight(3);
                TotalHabitsLabel.LabelText = user.HabitList.Count.ToString().PadRight(3) ?? "".PadRight(3);
                TotalJournalLabel.LabelText = user.JournalList.Count.ToString().PadRight(3) ?? "".PadRight(3);
                TotalTasksLabel.LabelText = user.TaskList.Count.ToString().PadRight(3) ?? "".PadRight(3);
            }
            else
            {
                UsernameLabel.LabelText = "".PadRight(10, '_');
                LevelLabel.LabelText =  "".PadRight(3, '_');
                ExperienceLabel.LabelText = "".PadRight(3, '_');

                TotalActionsLabel.LabelText = "".PadRight(3, '_');
                TotalHabitsLabel.LabelText = "".PadRight(3, '_');
                TotalJournalLabel.LabelText = "".PadRight(3, '_');
                TotalTasksLabel.LabelText = "".PadRight(3, '_');
            }
            Labels.Add(UsernameLabel);
            Labels.Add(LevelLabel);
            Labels.Add(ExperienceLabel);
            Labels.Add(TotalActionsLabel);
            Labels.Add(TotalHabitsLabel);
            Labels.Add(TotalJournalLabel);
            Labels.Add(TotalTasksLabel);

            for (int i = 0; i < 7; i++)
            {
                if (user.Username == null)
                {
                    VariableLabel variable = new()
                    {
                        HorizontalLength = 1,
                        VerticalLength = 10,
                        XPosition = 10 + (i * 3),
                        YPosition = 5,
                        LabelText = " "
                    };
                    GraphScale.LabelText = "".PadRight(5, '_');
                    Labels.Add(GraphScale);
                    Labels.Add(variable);
                }
                else
                {
                    double factor = 0;
                    int[] data = CalculateGraph(user, ref factor);
                    char[] charArray = new char[10];
                    for (int j = 0; j < charArray.Length; j++)
                    {
                        charArray[j] = '0';
                    }
                    if (data[i] > 0 && data[i] <= 10)
                    {
                        charArray[10 - data[i]] = 'X';
                    }
                    string dataResult = new string(charArray);
                    VariableLabel variable = new()
                    {
                        HorizontalLength = 1,
                        VerticalLength = 10,
                        XPosition = 10 + (i * 3),
                        YPosition = 5,
                        LabelText = dataResult
                    };
                    GraphScale.LabelText = Math.Round(factor, 2).ToString().PadRight(5);
                    Labels.Add(GraphScale);
                    Labels.Add(variable);
                }    
            }
            #endregion
        }

        public int[] CalculateGraph(UserData user, ref double factor)
        {
            int[] graphValues = { 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 6; i >= 0; i--)
            {
                user.Actions.TryGetValue(DateTime.Now.Date.AddDays(-(i+1)), out graphValues[6 - i]);
            }
            factor = (double)graphValues.Max() / 10;
            for (int i = 0; i < 7; i++)
            {
                graphValues[i] = (int)Math.Round(graphValues[i] / factor);
            }
            return graphValues;
        }

        public string SearchUser()
        {
            string Token = "B";
            Token += UserNameInputButton.FieldText.PadRight(10, '~');
            return Token;
        }

    }

    #endregion
        #region Login Scene Graphics
    public class LoginGraphic : GraphicElement
    {
        public override string Graphic { get; set; } =
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&@%%%%%%%%%%%%%%%%%%%%@" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&@%%%%%%%%%%%%%%%%%%%%@" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&&&&&&&&&&&&&&&&&&&&&&@" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";
        public override int MaxWidth { get; set; } = 31;
        public override int MaxHeight { get; set; } = 7;
        public override int[][]? MaxIndexPerInterface { get; set; } = [[0, 2], [0, 1]];

        public InputField UsernameInput { get; set; } = new InputField();
        public InputField PasswordInput { get; set; } = new InputField();
        public Button Username { get; set; } = new Button();
        public Button Password { get; set; } = new Button();
        public Button Login { get; set; } = new Button();

        public LoginGraphic()
        {
            InputFields = [];
            Buttons = [];
            PreviousScene = ProgramScreen.MainMenu;

            UsernameInput.HorizontalLength = 20;
            UsernameInput.VerticalLength = 1;
            UsernameInput.XPosition = 10;
            UsernameInput.YPosition = 1;
            UsernameInput.MenuInterfaceLevel = 1;
            UsernameInput.FieldText = "";
            UsernameInput.IsHorizontalExpandable = true;
            UsernameInput.InterfaceIndexY = 0;
            UsernameInput.InterfaceIndexX = 0;
            UsernameInput.MaxFieldTextLength = 10;

            PasswordInput.HorizontalLength = 20;
            PasswordInput.VerticalLength = 1;
            PasswordInput.XPosition = 10;
            PasswordInput.YPosition = 3;
            PasswordInput.MenuInterfaceLevel = 1;
            PasswordInput.FieldText = "";
            PasswordInput.IsPrivate = true;
            UsernameInput.IsHorizontalExpandable = true;
            PasswordInput.InterfaceIndexY = 1;
            PasswordInput.InterfaceIndexX = 0;
            PasswordInput.MaxFieldTextLength = 10;

            Username.HorizontalLength = 8;
            Username.VerticalLength = 1;
            Username.XPosition = 1;
            Username.YPosition = 1;
            Username.MenuInterfaceLevel = 0;
            Username.ButtonText = "USERNAME";
            Username.BindedInterface = UsernameInput;
            Username.InterfaceIndexY = 0;
            Username.InterfaceIndexX = 0;

            Password.HorizontalLength = 8;
            Password.VerticalLength = 1;
            Password.XPosition = 1;
            Password.YPosition = 3;
            Password.MenuInterfaceLevel = 0;
            Password.BindedInterface = PasswordInput;
            Password.ButtonText = "PASSWORD";
            Password.InterfaceIndexY = 1;
            Password.InterfaceIndexX = 0;

            Login.HorizontalLength = 29;
            Login.VerticalLength = 1;
            Login.XPosition = 1;
            Login.YPosition = 5;
            Login.MenuInterfaceLevel = 0;
            Login.ButtonText = "           LOGIN             ";
            Login.InterfaceIndexY = 2;
            Login.InterfaceIndexX = 0;
            Login.IsInvokable = true;
            Login.SetInvokedMethod(LoginUser);

            InputFields.Add(UsernameInput);
            InputFields.Add(PasswordInput);

            Buttons.Add(Username);
            Buttons.Add(Password);
            Buttons.Add(Login);
        }

        public string LoginUser()
        {
            string LoginToken = "0";
            // Returns a string of int filled with information about the user input field
            // 0 - first digit signifies the invoke type, 0 for login, 1 for switch screen
            // 0 Login
            // 000000000000000... - Next 30 chars are the user ID
            // 0000000000 - next 10 chars are the user password

            // 1 Switch Screen
            LoginToken += UsernameInput.FieldText.PadRight(30, '~');
            LoginToken += PasswordInput.FieldText.PadRight(10, '~');
            foreach (InputField inputField in InputFields!)
            {
                inputField.ClearFieldText();
            }    
            return LoginToken;
        }
    }
    #endregion
    #region Create Account Scene Graphics
    public class CreateAccountGraphic : GraphicElement
    {
        public override string Graphic { get; set; } =
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&@%%%%%%%%%%%%%%%%%%%%@" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&@%%%%%%%%%%%%%%%%%%%%@" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&&&&&&&&&&&&&&&&&&&&&&@" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";
        public override int MaxWidth { get; set; } = 31;
        public override int MaxHeight { get; set; } = 7;
        public override int[][]? MaxIndexPerInterface { get; set; } = [[0, 2]];

        public InputField UsernameInput { get; set; } = new InputField();
        public InputField PasswordInput { get; set; } = new InputField();
        public Button Username { get; set; } = new Button();
        public Button Password { get; set; } = new Button();
        public Button Login { get; set; } = new Button();

        public CreateAccountGraphic()
        {
            InputFields = [];
            Buttons = [];
            PreviousScene = ProgramScreen.MainMenu;

            UsernameInput.HorizontalLength = 20;
            UsernameInput.VerticalLength = 1;
            UsernameInput.XPosition = 10;
            UsernameInput.YPosition = 1;
            UsernameInput.MenuInterfaceLevel = 1;
            UsernameInput.FieldText = "";
            UsernameInput.IsHorizontalExpandable = true;
            UsernameInput.InterfaceIndexY = 0;
            UsernameInput.InterfaceIndexX = 0;
            UsernameInput.MaxFieldTextLength = 10;

            PasswordInput.HorizontalLength = 20;
            PasswordInput.VerticalLength = 1;
            PasswordInput.XPosition = 10;
            PasswordInput.YPosition = 3;
            PasswordInput.MenuInterfaceLevel = 1;
            PasswordInput.FieldText = "";
            PasswordInput.IsPrivate = true;
            UsernameInput.IsHorizontalExpandable = true;
            PasswordInput.InterfaceIndexY = 1;
            PasswordInput.InterfaceIndexX = 0;
            PasswordInput.MaxFieldTextLength = 10;

            Username.HorizontalLength = 8;
            Username.VerticalLength = 1;
            Username.XPosition = 1;
            Username.YPosition = 1;
            Username.MenuInterfaceLevel = 0;
            Username.ButtonText = "USERNAME";
            Username.BindedInterface = UsernameInput;
            Username.InterfaceIndexY = 0;
            Username.InterfaceIndexX = 0;

            Password.HorizontalLength = 8;
            Password.VerticalLength = 1;
            Password.XPosition = 1;
            Password.YPosition = 3;
            Password.MenuInterfaceLevel = 0;
            Password.BindedInterface = PasswordInput;
            Password.ButtonText = "PASSWORD";
            Password.InterfaceIndexY = 1;
            Password.InterfaceIndexX = 0;

            Login.HorizontalLength = 29;
            Login.VerticalLength = 1;
            Login.XPosition = 1;
            Login.YPosition = 5;
            Login.MenuInterfaceLevel = 0;
            Login.ButtonText = "        CREATE ACCOUNT       ";
            Login.InterfaceIndexY = 2;
            Login.InterfaceIndexX = 0;
            Login.IsInvokable = true;
            Login.SetInvokedMethod(CreateUser);

            InputFields.Add(UsernameInput);
            InputFields.Add(PasswordInput);

            Buttons.Add(Username);
            Buttons.Add(Password);
            Buttons.Add(Login);
        }

        public string CreateUser()
        {
            string CreateUserToken = "5";
            // Returns a string of int filled with information about the user input field
            // 0 - first digit signifies the invoke type, 0 for login, 1 for switch screen
            // 0 Login
            // 000000000000000... - Next 30 chars are the user ID
            // 0000000000 - next 10 chars are the user password

            // 1 Switch Screen
            CreateUserToken += UsernameInput.FieldText.PadRight(30, '~');
            CreateUserToken += PasswordInput.FieldText.PadRight(10, '~');

            foreach (InputField inputField in InputFields!)
            {
                inputField.ClearFieldText();
            }
            return CreateUserToken;
        }
    }
    #endregion
    #region Main Scene Graphics
    public class TopBarGraphics : GraphicElement
    {
        public override string Graphic { get; set; } =
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@USERNAME ++++++++++     LVL +++     EXP +++++++@  &&&&&&&&&&   @   &&&&&&&&&&   @   &&&&&&&&&&   @" + //HABITS + JOURNAL + TODO
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";

        public override int MaxWidth { get; set; } = 99;
        public override int MaxHeight { get; set; } = 3;
        public override int[][]? MaxIndexPerInterface { get; set; } = [[2, 0]];

        public Button HabitsButton { get; set; } = new Button();
        public Button JournalButton { get; set; } = new Button();
        public Button ToDoButton { get; set; } = new Button();

        public VariableLabel UsernameLabel { get; set; } = new VariableLabel();
        public VariableLabel LevelLabel { get; set; } = new VariableLabel();
        public VariableLabel ExperienceLabel { get; set; } = new VariableLabel();

        public TopBarGraphics()
        {
            ID = "003";
            Buttons = [];
            Labels = [];
            PreviousScene = ProgramScreen.MainMenu;

            IsDynamic = true;
            UsernameLabel.LabelText = "USERNAME11";
            UsernameLabel.XPosition = 10;
            UsernameLabel.YPosition = 1;
            UsernameLabel.HorizontalLength = 10;
            UsernameLabel.VerticalLength = 1;

            LevelLabel.LabelText = "LVL";
            LevelLabel.XPosition = 29;
            LevelLabel.YPosition = 1;
            LevelLabel.HorizontalLength = 3;
            LevelLabel.VerticalLength = 1;

            ExperienceLabel.LabelText = "EXP1111";
            ExperienceLabel.XPosition = 41;
            ExperienceLabel.YPosition = 1;
            ExperienceLabel.HorizontalLength = 7;
            ExperienceLabel.VerticalLength = 1;

            HabitsButton.HorizontalLength = 10;
            HabitsButton.VerticalLength = 1;
            HabitsButton.XPosition = 51;
            HabitsButton.YPosition = 1;
            HabitsButton.MenuInterfaceLevel = 0;
            HabitsButton.ButtonText = "  HABITS  ";
            HabitsButton.InterfaceIndexY = 0;
            HabitsButton.InterfaceIndexX = 0;
            HabitsButton.IsInvokable = true;
            HabitsButton.SetInvokedMethod(SetHabitListActive);

            JournalButton.HorizontalLength = 10;
            JournalButton.VerticalLength = 1;
            JournalButton.XPosition = 68;
            JournalButton.YPosition = 1;
            JournalButton.MenuInterfaceLevel = 0;
            JournalButton.ButtonText = "  JOURNAL ";
            JournalButton.InterfaceIndexY = 0;
            JournalButton.InterfaceIndexX = 1;
            JournalButton.IsInvokable = true;
            JournalButton.SetInvokedMethod(SetJournalListActive);

            ToDoButton.HorizontalLength = 10;
            ToDoButton.VerticalLength = 1;
            ToDoButton.XPosition = 85;
            ToDoButton.YPosition = 1;
            ToDoButton.MenuInterfaceLevel = 0;
            ToDoButton.ButtonText = "   TODO   ";
            ToDoButton.InterfaceIndexY = 0;
            ToDoButton.InterfaceIndexX = 2;
            ToDoButton.IsInvokable = true;
            ToDoButton.SetInvokedMethod(SetTaskListActive);

            Buttons.Add(HabitsButton);
            Buttons.Add(JournalButton);
            Buttons.Add(ToDoButton);

            Labels.Add(UsernameLabel);
            Labels.Add(LevelLabel);
            Labels.Add(ExperienceLabel);
        }

        public override void AdjustVariableData(ref UserData user)
        {
            UsernameLabel.LabelText = user?.Username?.PadRight(10) ?? "";
            LevelLabel.LabelText = user?.Level.ToString().PadRight(3) ?? "";
            ExperienceLabel.LabelText = user?.Experience.ToString().PadLeft(3, '0') + "/" + 100;
        }

        public string SetHabitListActive()
        {
            StartingIndex = 0;
            if (IsHabitListEmpty)
            {
                return "3" + "1" + "0" + InfoToken[0];
            }
            return "2" + "000" + "1" + "0" + "0" + "1" + "0" + "1" + "0" + "0"; // Type - ID to toggle-
                                                                                // 1/0 True/False -
                                                                                // InterfaceY index default -
                                                                                // InterfaceX index default-
                                                                                // Interface level default
                                                                                // 0 false 1 true, if turn off current active graphic
        }

        public string SetJournalListActive()
        {
            StartingIndex = 0;
            if (IsJournalListEmpty)
            {
                return "6" + "1" + "0" + InfoToken[0];
            }
            return "2" + "004" + "1" + "0" + "0" + "1" + "0" + "1" + "0" + "0"; // Type - ID to toggle-
                                                                                // 1/0 True/False -
                                                                                // InterfaceY index default -
                                                                                // InterfaceX index default-
                                                                                // Interface level default
                                                                                // 0 false 1 true, if turn off current active graphic
        }

        public string SetTaskListActive()
        {
            StartingIndex = 0;
            if (IsToDoListEmpty)
            {
                return "8" + "1" + "0" + InfoToken[0];
            }
            return "2" + "008" + "1" + "0" + "0" + "1" + "0" + "1" + "0" + "0";
        }
    }

    #region Habit List Graphics
    public class HabitListGraphics : GraphicElement
    {
        public override string Graphic { get; set; } =
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     +++++++++++++++++++++++++++++++++++++++++++++++++++++       @" + // 40 Character Limit for Habit Name NAME/FINISHED/UNFINISHED
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     +++++++++++++++++++++++++++++++++++++++++++++++++++++       @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     +++++++++++++++++++++++++++++++++++++++++++++++++++++       @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     +++++++++++++++++++++++++++++++++++++++++++++++++++++       @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     +++++++++++++++++++++++++++++++++++++++++++++++++++++       @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     +++++++++++++++++++++++++++++++++++++++++++++++++++++       @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     +++++++++++++++++++++++++++++++++++++++++++++++++++++       @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     +++++++++++++++++++++++++++++++++++++++++++++++++++++       @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     +++++++++++++++++++++++++++++++++++++++++++++++++++++       @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     +++++++++++++++++++++++++++++++++++++++++++++++++++++       @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";


        public override int MaxWidth { get; set; } = 69;
        public override int MaxHeight { get; set; } = 21;
        public override int[][]? MaxIndexPerInterface { get; set; } = [[0, 0], [0, 9]];

        public HabitListGraphics()
        {
            ID = "000";
            InfoToken = " 1003000";
            IsGraphicElementVisibleDefault = false;
            
        }

        public override void AdjustVariableData(ref UserData user)
        {
            Buttons = [];
            Labels = [];
            MaxIndexPerInterface![1][1] = Math.Min(user.HabitList.Count - 1, 9);
            StartingIndex = Math.Min(Math.Max(user.HabitList.Count - 10, 0), StartingIndex);
            for (int i = 0; i < 10; i++)
            {
                if (i >= user.HabitList.Count)
                {
                    VariableLabel habitLabels = new()
                    {
                        HorizontalLength = 53,
                        VerticalLength = 1,
                        XPosition = 8,
                        YPosition = 1 + (i * 2),
                        LabelText = "".PadRight(40) + "   " + "".PadRight(10)
                    };
                    Labels.Add(habitLabels);
                    Button habitButtons = new()
                    {
                        HorizontalLength = 1,
                        VerticalLength = 1,
                        XPosition = 2,
                        YPosition = 1 + (i * 2),
                        MenuInterfaceLevel = -1,
                        ButtonText = " ",
                        InterfaceIndexY = i,
                        InterfaceIndexX = 0
                    };
                    Buttons.Add(habitButtons);
                    continue;
                }
                Button habitButton = new()
                {
                    HorizontalLength = 1,
                    VerticalLength = 1,
                    XPosition = 2,
                    YPosition = 1 + (i * 2),
                    MenuInterfaceLevel = 1,
                    ButtonText = " ",
                    InterfaceIndexY = i,
                    InterfaceIndexX = 0,
                    IsInvokable = true,
                };
                habitButton.SetInvokedMethod(SetHabitOptionActive);
                Buttons.Add(habitButton);

                VariableLabel habitLabel = new()
                {
                    HorizontalLength = 53,
                    VerticalLength = 1,
                    XPosition = 8,
                    YPosition = 1 + (i * 2),
                    LabelText = user.HabitList[i + StartingIndex].Name.PadRight(40) + "   " + (user.HabitList[i + StartingIndex].Completed ? "FINISHED" : "UNFINISHED").PadRight(10)
                };
                Labels.Add(habitLabel);
            }
        }

        public string SetHabitOptionActive()
        {
            return "2" + "001" + "1" + "0" + "0" + "3" + "0" + "1" + "0" + "0"; // Type -
                                                                                // ID of the habit list -
                                                                                // 1/0 True/False -
                                                                                // InterfaceY index default -
                                                                                // InterfaceX index default-
                                                                                // Interface level default -
                                                                                // 0 false 1 true, if turn off current active graphic

        }
    }

    public class HabitOptionGraphics : GraphicElement
    {
        public override string Graphic { get; set; } =
            "@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&&&&&&&&&@" + //FINISH
            "@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&&&&&&&&&@" + //ADD
            "@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&&&&&&&&&@" + //EDIT
            "@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&&&&&&&&&@" + //DELETE
            "@@@@@@@@@@@@@@@@@@";

        public override int MaxWidth { get; set; } = 18;
        public override int MaxHeight { get; set; } = 9;
        public override int[][]? MaxIndexPerInterface { get; set; } = [[0, 0], [0, 0], [0, 0], [0, 3]];

        public Button FinishButton { get; set; } = new Button();
        public Button AddButton { get; set; } = new Button();
        public Button EditButton { get; set; } = new Button();
        public Button DeleteButton { get; set; } = new Button();

        public HabitOptionGraphics()
        {
            ID = "001";
            InfoToken = "  000001";
            IsGraphicElementVisibleDefault = false;
            Buttons = [];

            FinishButton.HorizontalLength = 16;
            FinishButton.VerticalLength = 1;
            FinishButton.XPosition = 1;
            FinishButton.YPosition = 1;
            FinishButton.MenuInterfaceLevel = 3;
            FinishButton.ButtonText = "     FINISH     ";
            FinishButton.InterfaceIndexY = 0;
            FinishButton.InterfaceIndexX = 0;
            FinishButton.IsInvokable = true;
            FinishButton.SetInvokedMethod(FinishSelectedHabit);

            AddButton.HorizontalLength = 16;
            AddButton.VerticalLength = 1;
            AddButton.XPosition = 1;
            AddButton.YPosition = 3;
            AddButton.MenuInterfaceLevel = 3;
            AddButton.ButtonText = "      ADD       ";
            AddButton.InterfaceIndexY = 1;
            AddButton.InterfaceIndexX = 0;
            AddButton.IsInvokable = true;
            AddButton.SetInvokedMethod(AddHabit);

            EditButton.HorizontalLength = 16;
            EditButton.VerticalLength = 1;
            EditButton.XPosition = 1;
            EditButton.YPosition = 5;
            EditButton.MenuInterfaceLevel = 3;
            EditButton.ButtonText = "      EDIT      ";
            EditButton.InterfaceIndexY = 2;
            EditButton.InterfaceIndexX = 0;
            EditButton.IsInvokable = true;
            EditButton.SetInvokedMethod(EditHabit);

            DeleteButton.HorizontalLength = 16;
            DeleteButton.VerticalLength = 1;
            DeleteButton.XPosition = 1;
            DeleteButton.YPosition = 7;
            DeleteButton.MenuInterfaceLevel = 3;
            DeleteButton.ButtonText = "     DELETE     ";
            DeleteButton.InterfaceIndexY = 3;
            DeleteButton.InterfaceIndexX = 0;
            DeleteButton.IsInvokable = true;
            DeleteButton.SetInvokedMethod(DeleteSelectedHabit);

            Buttons.Add(FinishButton);
            Buttons.Add(AddButton);
            Buttons.Add(EditButton);
            Buttons.Add(DeleteButton);
        }

        public string FinishSelectedHabit()
        {
            return "3" + "4" + "1" + InfoToken[0]; // Type 3 (Operation) - 
                                                   // Add, Delete, Edit, Finish
                                                   // Disable Current Element
                                                   // Habit Index
        }

        public string DeleteSelectedHabit()
        {
            return "3" + "2" + "1" + InfoToken[0]; // Type 3 (Operation) -
                                                   // Add, Delete, Edit, Finish
                                                   // Disable Current Element
                                                   // Habit Index
        }

        public string AddHabit()
        {
            return "3" + "1" + "1" + InfoToken[0]; // Type 3 (Operation) -
                                                   // Add, Delete, Edit, Finish
                                                   // Disable Current Element
                                                   // Habit Index
        }
        public string EditHabit()
        {
            return "3" + "3" + "1" + InfoToken[0]; // Type 3 (Operation) -
                                                   // Add, Delete, Edit, Finish
                                                   // Disable Current Element
                                                   // Habit Index
        }
    }

    public class HabitEditInterfaceGraphics : GraphicElement
    {
        public override string Graphic { get; set; } =
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&&&&&&&&&&&&&&&&&&&&&@" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@%%%%%%%%%%%%%%%%%%%%%%%%%%%%@" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&&&&&&&&&&&&&&&&&&&&&@" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@%%%%%%%%%%%%%%%%%%%%%%%%%%%%@" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@@&&&&&&&&&&&&&&&&&&&&&&&&&&@@" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";

        public override int MaxWidth { get; set; } = 30;
        public override int MaxHeight { get; set; } = 12;
        public override int[][]? MaxIndexPerInterface { get; set; } = [[0, 0], [0, 0], [0, 0], [0, 3], [0, 2]];

        public Button HabitUserName { get; set; } = new Button();
        public Button HabitDifficulty { get; set; } = new Button();
        public Button ConfirmButton { get; set; } = new Button();

        public InputField HabitNameInput { get; set; } = new InputField();
        public InputField HabitDifficultyInput { get; set; } = new InputField();

        public HabitEditInterfaceGraphics()
        {
            ID = "002";
            InfoToken = " 1000102";
            IsGraphicElementVisibleDefault = false;
            Buttons = [];
            InputFields = [];

            HabitUserName.HorizontalLength = 28;
            HabitUserName.VerticalLength = 1;
            HabitUserName.XPosition = 1;
            HabitUserName.YPosition = 1;
            HabitUserName.MenuInterfaceLevel = 4;
            HabitUserName.ButtonText = CustomDisplay.CenterString("HABIT NAME", 28);
            HabitUserName.InterfaceIndexY = 0;
            HabitUserName.InterfaceIndexX = 0;
            HabitUserName.BindedInterface = HabitNameInput;

            HabitDifficulty.HorizontalLength = 28;
            HabitDifficulty.VerticalLength = 1;
            HabitDifficulty.XPosition = 1;
            HabitDifficulty.YPosition = 6;
            HabitDifficulty.MenuInterfaceLevel = 4;
            HabitDifficulty.ButtonText = "       HABIT DIFFICULTY       ";
            HabitDifficulty.InterfaceIndexY = 1;
            HabitDifficulty.InterfaceIndexX = 0;
            HabitDifficulty.BindedInterface = HabitDifficultyInput;

            ConfirmButton.HorizontalLength = 26;
            ConfirmButton.VerticalLength = 1;
            ConfirmButton.XPosition = 2;
            ConfirmButton.YPosition = 10;
            ConfirmButton.MenuInterfaceLevel = 4;
            ConfirmButton.ButtonText = CustomDisplay.CenterString("CONFIRM", 26);
            ConfirmButton.InterfaceIndexY = 2;
            ConfirmButton.InterfaceIndexX = 0;
            ConfirmButton.IsInvokable = true;
            ConfirmButton.SetInvokedMethod(ConfirmAddHabit);

            HabitNameInput.HorizontalLength = 28;
            HabitNameInput.VerticalLength = 1;
            HabitNameInput.XPosition = 1;
            HabitNameInput.YPosition = 3;
            HabitNameInput.MenuInterfaceLevel = 5;
            HabitNameInput.FieldText = "";
            HabitNameInput.IsHorizontalExpandable = true;
            HabitNameInput.InterfaceIndexY = 0;
            HabitNameInput.InterfaceIndexX = 0;
            HabitNameInput.MaxFieldTextLength = 40;

            HabitDifficultyInput.HorizontalLength = 28;
            HabitDifficultyInput.VerticalLength = 1;
            HabitDifficultyInput.XPosition = 1;
            HabitDifficultyInput.YPosition = 8;
            HabitDifficultyInput.MenuInterfaceLevel = 5;
            HabitDifficultyInput.FieldText = "";
            HabitDifficultyInput.IsHorizontalExpandable = true;
            HabitDifficultyInput.InterfaceIndexY = 1;
            HabitDifficultyInput.InterfaceIndexX = 0;
            HabitDifficultyInput.MaxFieldTextLength = 10;

            Buttons.Add(HabitUserName);
            Buttons.Add(HabitDifficulty);
            Buttons.Add(ConfirmButton);

            InputFields.Add(HabitNameInput);
            InputFields.Add(HabitDifficultyInput);
        }

        public string ConfirmAddHabit()
        {
            string HabitToken = "4";



            // The next 40 characters are the habit name
            // The next 8 characters are the habit difficulty

            //TODO check if difficulty input text is valid
            HabitToken += HabitNameInput.FieldText.PadRight(40, '~');
            HabitToken += HabitDifficultyInput.FieldText.PadRight(8, '~');

            foreach (InputField inputField in InputFields)
            {
                inputField.FieldText = "";
            }

            if (InfoToken[1] == '2')
            {
                HabitToken += "0" + InfoToken[0];
            }
            else
            {
                HabitToken += " ";
            }
            return HabitToken;
        }

    }
    #endregion

    #region Journal List Graphics
    public class JournalListGraphic : GraphicElement
    {
        public override string Graphic { get; set; } =
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     +++++++++++++++++++++++++++++++++++++++++++++++++++++ @" + // ADD VIEW EDIT DELETE
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     +++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     +++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     +++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     +++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     +++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     +++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     +++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     +++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     +++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";


        public override int MaxWidth { get; set; } = 63;
        public override int MaxHeight { get; set; } = 21;
        public override int[][]? MaxIndexPerInterface { get; set; } = [[0, 0], [0, 9]];

        public JournalListGraphic()
        {
            ID = "004";
            InfoToken = " 1003010";
            IsGraphicElementVisibleDefault = false;
        }

        public override void AdjustVariableData(ref UserData user)
        {
            Buttons = [];
            Labels = [];
            MaxIndexPerInterface![1][1] = Math.Min(user.JournalList.Count - 1, 9);
            StartingIndex = Math.Min(Math.Max(user.JournalList.Count - 10, 0), StartingIndex);

            for (int i = 0; i < 10; i++)
            {
                if (i >= user.JournalList.Count)
                {
                    VariableLabel journalTitles = new()
                    {
                        HorizontalLength = 53,
                        VerticalLength = 1,
                        XPosition = 8,
                        YPosition = 1 + (i * 2),
                        LabelText = "".PadRight(40) + "   " + "".PadRight(10)
                    };
                    Labels.Add(journalTitles);
                    Button journalButton = new()
                    {
                        HorizontalLength = 1,
                        VerticalLength = 1,
                        XPosition = 2,
                        YPosition = 1 + (i * 2),
                        MenuInterfaceLevel = -1,
                        ButtonText = " ",
                        InterfaceIndexY = i,
                        InterfaceIndexX = 0
                    };
                    Buttons.Add(journalButton);
                    continue;
                }
                Button journalButtons = new()
                {
                    HorizontalLength = 1,
                    VerticalLength = 1,
                    XPosition = 2,
                    YPosition = 1 + (i * 2),
                    MenuInterfaceLevel = 1,
                    ButtonText = " ",
                    InterfaceIndexY = i,
                    InterfaceIndexX = 0,
                    IsInvokable = true,
                };
                journalButtons.SetInvokedMethod(SetJournalOptionViewActive);
                Buttons.Add(journalButtons);

                VariableLabel journalTitle = new()
                {
                    HorizontalLength = 53,
                    VerticalLength = 1,
                    XPosition = 8,
                    YPosition = 1 + (i * 2),
                    LabelText = user.JournalList[i + StartingIndex].Name.PadRight(30) + "   " + user.JournalList[i + StartingIndex].DateCreated.ToShortDateString().PadRight(20)
                };
                Labels.Add(journalTitle);
            }
        }

        public static string SetJournalOptionViewActive()
        {
            return "2" + "005" + "1" + "0" + "0" + "3" + "0" + "1" + "0" + "1"; // Type -
                                                                                // ID-
                                                                                // 1/0 True/False -
                                                                                // InterfaceY index default -
                                                                                // InterfaceX index default-
                                                                                // Interface level default -
                                                                                // 0 false 1 true, if turn off current active graphic
                                                                                // Activate at xcoordinate as active?
                                                                                // Activate at ycoordinate as active?

        }
    }

    public class JournalOperations : GraphicElement
    {
        public override string Graphic { get; set; } =
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &&&&&&& &&&&&&& &&&&&&& &&&&&&& @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";


        public override int MaxWidth { get; set; } = 35;
        public override int MaxHeight { get; set; } = 3;
        public override int[][]? MaxIndexPerInterface { get; set; } = [[0, 0], [0, 0], [0, 0], [3, 0]];

        public Button AddButton { get; set; } = new Button();
        public Button ViewButton { get; set; } = new Button();
        public Button EditButton { get; set; } = new Button();
        public Button DeleteButton { get; set; } = new Button();


        public JournalOperations()
        {
            ID = "005";
            InfoToken = "  004001";
            IsGraphicElementVisibleDefault = false;
            Buttons = [];

            AddButton.HorizontalLength = 7;
            AddButton.VerticalLength = 1;
            AddButton.XPosition = 2;
            AddButton.YPosition = 1;
            AddButton.MenuInterfaceLevel = 3;
            AddButton.ButtonText = "  ADD  ";
            AddButton.InterfaceIndexY = 0;
            AddButton.InterfaceIndexX = 0;
            AddButton.IsInvokable = true;
            AddButton.SetInvokedMethod(AddJournal);

            ViewButton.HorizontalLength = 7;
            ViewButton.VerticalLength = 1;
            ViewButton.XPosition = 10;
            ViewButton.YPosition = 1;
            ViewButton.MenuInterfaceLevel = 3;
            ViewButton.ButtonText = "  VIEW ";
            ViewButton.InterfaceIndexY = 0;
            ViewButton.InterfaceIndexX = 1;
            ViewButton.IsInvokable = true;
            ViewButton.SetInvokedMethod(ViewJournal);

            EditButton.HorizontalLength = 7;
            EditButton.VerticalLength = 1;
            EditButton.XPosition = 18;
            EditButton.YPosition = 1;
            EditButton.MenuInterfaceLevel = 3;
            EditButton.ButtonText = " EDIT  ";
            EditButton.InterfaceIndexY = 0;
            EditButton.InterfaceIndexX = 2;
            EditButton.IsInvokable = true;
            EditButton.SetInvokedMethod(EditJournal);

            DeleteButton.HorizontalLength = 7;
            DeleteButton.VerticalLength = 1;
            DeleteButton.XPosition = 26;
            DeleteButton.YPosition = 1;
            DeleteButton.MenuInterfaceLevel = 3;
            DeleteButton.ButtonText = "DELETE ";
            DeleteButton.InterfaceIndexY = 0;
            DeleteButton.InterfaceIndexX = 3;
            DeleteButton.IsInvokable = true;
            DeleteButton.SetInvokedMethod(DeleteJournal);

            Buttons.Add(AddButton);
            Buttons.Add(ViewButton);
            Buttons.Add(EditButton);
            Buttons.Add(DeleteButton);

        }

        public string AddJournal()
        {
            return "6" + "1" + "1" + InfoToken[0]; // Type 3 (Operation) -
                                                   // Add, Delete, Edit, Finish
                                                   // Disable Current Element
                                                   // Journal Index
        }
        public string ViewJournal()
        {
            return "6" + "4" + "0" + InfoToken[0]; // Type 3 (Operation) -
                                                   // Add, Delete, Edit, Finish
                                                   // Disable Current Element
                                                   // Journal Index
        }
        public string EditJournal()
        {
            return "6" + "3" + "1" + InfoToken[0]; // Type 3 (Operation) -
                                                   // Add, Delete, Edit, Finish
                                                   // Disable Current Element
                                                   // Journal Index
        }
        public string DeleteJournal()
        {
            return "6" + "2" + "1" + InfoToken[0]; // Type 3 (Operation) -
                                                   // Add, Delete, Edit, Finish
                                                   // Disable Current Element
                                                   // Journal Index
        }
    }

    public class AddJournalInterfaceGraphics : GraphicElement
    {
        public override string Graphic { get; set; } =
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &&&&&&&&&&& @ %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&& @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% @" +
            "@ %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% @" +
            "@ %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% @" +
            "@ %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% @" +
            "@ %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% @" +
            "@ %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% @" +
            "@ %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% @" +
            "@ %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% @" +
            "@ %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% @" +
            "@ %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% @" +
            "@ %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% @" +
            "@ %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% @" +
            "@ %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&& @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";

        public override int MaxWidth { get; set; } = 63;
        public override int MaxHeight { get; set; } = 21;
        public override int[][]? MaxIndexPerInterface { get; set; } = [[0, 0], [0, 0], [0, 0], [0, 3], [0, 2], [0, 2]];

        public InputField TitleInput { get; set; } = new InputField();
        public InputField EntryInput { get; set; } = new InputField();

        public Button ConfirmButton { get; set; } = new Button();
        public Button TitleButton { get; set; } = new Button();
        public Button EntryButton { get; set; } = new Button();

        public AddJournalInterfaceGraphics()
        {
            ID = "006";
            InfoToken = " 1004001";
            InputFields = [];
            Buttons = [];
            IsGraphicElementVisibleDefault = false;

            TitleInput.HorizontalLength = 45;
            TitleInput.VerticalLength = 1;
            TitleInput.XPosition = 16;
            TitleInput.YPosition = 1;
            TitleInput.MenuInterfaceLevel = 5;
            TitleInput.FieldText = "";
            TitleInput.IsHorizontalExpandable = false;
            TitleInput.InterfaceIndexY = 0;
            TitleInput.InterfaceIndexX = 0;
            TitleInput.MaxFieldTextLength = 40;

            EntryInput.HorizontalLength = 59;
            EntryInput.VerticalLength = 13;
            EntryInput.XPosition = 2;
            EntryInput.YPosition = 5;
            EntryInput.MenuInterfaceLevel = 5;
            EntryInput.IsHorizontalExpandable = false;
            EntryInput.InterfaceIndexY = 1;
            EntryInput.InterfaceIndexX = 0;
            EntryInput.MaxFieldTextLength = 500;

            TitleButton.HorizontalLength = 11;
            TitleButton.VerticalLength = 1;
            TitleButton.XPosition = 2;
            TitleButton.YPosition = 1;
            TitleButton.MenuInterfaceLevel = 4;
            TitleButton.ButtonText = CustomDisplay.CenterString("TITLE", 11);
            TitleButton.InterfaceIndexY = 0;
            TitleButton.InterfaceIndexX = 0;
            TitleButton.BindedInterface = TitleInput;
            TitleButton.IsInvokable = false;

            EntryButton.HorizontalLength = 59;
            EntryButton.VerticalLength = 1;
            EntryButton.XPosition = 2;
            EntryButton.YPosition = 3;
            EntryButton.MenuInterfaceLevel = 4;
            EntryButton.ButtonText = CustomDisplay.CenterString("ENTRY", 59);
            EntryButton.InterfaceIndexY = 1;
            EntryButton.InterfaceIndexX = 0;
            EntryButton.BindedInterface = EntryInput;
            EntryButton.IsInvokable = false;


            ConfirmButton.HorizontalLength = 59;
            ConfirmButton.VerticalLength = 1;
            ConfirmButton.XPosition = 2;
            ConfirmButton.YPosition = 19;
            ConfirmButton.MenuInterfaceLevel = 4;
            ConfirmButton.ButtonText = CustomDisplay.CenterString("CONFIRM", 59);
            ConfirmButton.InterfaceIndexY = 2;
            ConfirmButton.InterfaceIndexX = 0;
            ConfirmButton.IsInvokable = true;
            ConfirmButton.SetInvokedMethod(ConfirmAddJournal);


            InputFields.Add(TitleInput);
            InputFields.Add(EntryInput);

            Buttons.Add(ConfirmButton);
            Buttons.Add(TitleButton);
            Buttons.Add(EntryButton);
        }

        public string ConfirmAddJournal()
        {
            string JournalToken = "7";



            // The next 40 characters are the habit name
            // The next 8 characters are the habit difficulty

            //TODO check if difficulty input text is valid
            JournalToken += TitleInput.FieldText.PadRight(30, '~');
            JournalToken += EntryInput.FieldText.PadRight(500, '~');

            foreach (InputField inputField in InputFields!)
            {
                inputField.FieldText = "";
            }

            if (InfoToken[1] == '2')
            {
                JournalToken += "0" + InfoToken[0];
            }
            else
            {
                JournalToken += " ";
            }
            return JournalToken;
        }

    }

    public class ViewJournalEntry : GraphicElement
    {
        public override string Graphic { get; set; } =
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ +++++++++++ @ +++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@ +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@ +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@ +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@ +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@ +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@ +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@ +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@ +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@ +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@ +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@ +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@ +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";

        public override int MaxWidth { get; set; } = 63;
        public override int MaxHeight { get; set; } = 19;


        public VariableLabel Title { get; set; } = new VariableLabel();
        public VariableLabel Entry { get; set; } = new VariableLabel();
        public VariableLabel TitleLabel { get; set; } = new VariableLabel();
        public VariableLabel EntryLabel { get; set; } = new VariableLabel();


        public ViewJournalEntry()
        {
            ID = "007";
            InfoToken = " 1004001";

            Labels = [];
            IsGraphicElementVisibleDefault = false;

            TitleLabel.HorizontalLength = 11;
            TitleLabel.VerticalLength = 1;
            TitleLabel.XPosition = 2;
            TitleLabel.YPosition = 1;
            TitleLabel.LabelText = CustomDisplay.CenterString("TITLE", 11);

            EntryLabel.HorizontalLength = 59;
            EntryLabel.VerticalLength = 1;
            EntryLabel.XPosition = 2;
            EntryLabel.YPosition = 3;
            EntryLabel.LabelText = CustomDisplay.CenterString("ENTRY", 59);

            Labels.Add(TitleLabel);
            Labels.Add(EntryLabel);
            Labels.Add(Title);
            Labels.Add(Entry);
        }

        public override void AdjustVariableData(ref UserData user)
        {
            if (InfoToken[0] != 32)
            {
                Title.HorizontalLength = 45;
                Title.VerticalLength = 1;
                Title.XPosition = 16;
                Title.YPosition = 1;
                Title.LabelText = user.JournalList[InfoToken[0] - '0'].Name.PadRight(45);

                Entry.HorizontalLength = 59;
                Entry.VerticalLength = 13;
                Entry.XPosition = 2;
                Entry.YPosition = 5;
                Entry.LabelText = user.JournalList[InfoToken[0] - '0'].Entry.PadRight(767);

            }

        }
    }
    #endregion

    #region ToDo List Graphics
    public class TaskListGraphics : GraphicElement
    {
        public override string Graphic { get; set; } =
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@ &     ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";


        public override int MaxWidth { get; set; } = 76;
        public override int MaxHeight { get; set; } = 21;
        public override int[][]? MaxIndexPerInterface { get; set; } = [[0, 0], [0, 9], [0, 0], [0, 3], [0, 2]];

        public TaskListGraphics()
        {
            ID = "008";
            InfoToken = " 1003020";
            IsGraphicElementVisibleDefault = false;
        }

        public override void AdjustVariableData(ref UserData user)
        {
            Buttons = [];
            Labels = [];

            MaxIndexPerInterface![1][1] = Math.Min(user.TaskList.Count - 1, 9);
            StartingIndex = Math.Min(Math.Max(user.TaskList.Count - 10, 0), StartingIndex);

            for (int i = 0; i < 10; i++)
            {
                if (i >= user.TaskList.Count)
                {
                    VariableLabel taskLists = new()
                    {
                        HorizontalLength = 66,
                        VerticalLength = 1,
                        XPosition = 8,
                        YPosition = 1 + (i * 2),
                        LabelText = "".PadRight(66)
                    };
                    Labels.Add(taskLists);
                    Button taskButton = new()
                    {
                        HorizontalLength = 1,
                        VerticalLength = 1,
                        XPosition = 2,
                        YPosition = 1 + (i * 2),
                        MenuInterfaceLevel = -1,
                        ButtonText = " ",
                        InterfaceIndexY = i,
                        InterfaceIndexX = 0
                    };
                    Buttons.Add(taskButton);
                    continue;
                }
                Button taskButtons = new()
                {
                    HorizontalLength = 1,
                    VerticalLength = 1,
                    XPosition = 2,
                    YPosition = 1 + (i * 2),
                    MenuInterfaceLevel = 1,
                    ButtonText = " ",
                    InterfaceIndexY = i,
                    InterfaceIndexX = 0,
                    IsInvokable = true,
                };
                taskButtons.SetInvokedMethod(SetTaskOptionActive);
                Buttons.Add(taskButtons);

                string taskStatus = "INCOMPLETE";
                if (user.TaskList[i + StartingIndex].DateDue < DateTime.Now.Date)
                {
                    taskStatus = "DUE";
                }
                VariableLabel taskTitle = new()
                {
                    HorizontalLength = 66,
                    VerticalLength = 1,
                    XPosition = 8,
                    YPosition = 1 + (i * 2),
                    LabelText = user.TaskList[i + StartingIndex].Name.PadRight(30) + "   " +
                    user.TaskList[i + StartingIndex].Difficulty.ToString().PadRight(8) + "   " + CustomDisplay.CenterString(taskStatus, 10) + "  " + user.TaskList[i + StartingIndex].DateDue.ToShortDateString().PadRight(10)
                };
                Labels.Add(taskTitle);
            }
        }

        public static string SetTaskOptionActive()
        {
            return "2" + "009" + "1" + "0" + "0" + "3" + "0" + "1" + "0" + "0"; // Type -
                                                                                // ID-
                                                                                // 1/0 True/False -
                                                                                // InterfaceY index default -
                                                                                // InterfaceX index default-
                                                                                // Interface level default -
                                                                                // 0 false 1 true, if turn off current active graphic
                                                                                // Activate at xcoordinate as active?
                                                                                // Activate at ycoordinate as active?

        }
    }


    public class TaskOptionGraphics : GraphicElement
    {
        public override string Graphic { get; set; } =
            "@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&&&&&&&&&@" + // ADD
            "@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&&&&&&&&&@" + //DONE
            "@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&&&&&&&&&@" + //EDIT
            "@@@@@@@@@@@@@@@@@@";

        public override int MaxWidth { get; set; } = 18;
        public override int MaxHeight { get; set; } = 7;
        public override int[][]? MaxIndexPerInterface { get; set; } = [[0, 0], [0, 0], [0, 0], [0, 2], [0, 2]];

        public Button AddButton { get; set; } = new Button();
        public Button DoneButton { get; set; } = new Button();
        public Button EditButton { get; set; } = new Button();

        public TaskOptionGraphics()
        {
            ID = "009";
            InfoToken = "  000001";
            IsGraphicElementVisibleDefault = false;
            Buttons = [];

            AddButton.HorizontalLength = 16;
            AddButton.VerticalLength = 1;
            AddButton.XPosition = 1;
            AddButton.YPosition = 1;
            AddButton.MenuInterfaceLevel = 3;
            AddButton.ButtonText = CustomDisplay.CenterString("ADD", 16);
            AddButton.InterfaceIndexY = 0;
            AddButton.InterfaceIndexX = 0;
            AddButton.IsInvokable = true;
            AddButton.SetInvokedMethod(AddTask);

            DoneButton.HorizontalLength = 16;
            DoneButton.VerticalLength = 1;
            DoneButton.XPosition = 1;
            DoneButton.YPosition = 3;
            DoneButton.MenuInterfaceLevel = 3;
            DoneButton.ButtonText = CustomDisplay.CenterString("DONE", 16);
            DoneButton.InterfaceIndexY = 1;
            DoneButton.InterfaceIndexX = 0;
            DoneButton.IsInvokable = true;
            DoneButton.SetInvokedMethod(FinishTask);

            EditButton.HorizontalLength = 16;
            EditButton.VerticalLength = 1;
            EditButton.XPosition = 1;
            EditButton.YPosition = 5;
            EditButton.MenuInterfaceLevel = 3;
            EditButton.ButtonText = CustomDisplay.CenterString("EDIT", 16);
            EditButton.InterfaceIndexY = 2;
            EditButton.InterfaceIndexX = 0;
            EditButton.IsInvokable = true;
            EditButton.SetInvokedMethod(EditTask);


            Buttons.Add(AddButton);
            Buttons.Add(DoneButton);
            Buttons.Add(EditButton);
        }

        public string AddTask()
        {
            return "8" + "1" + "1" + InfoToken[0]; // Type 3 (Operation) - 
                                                   // Add, Delete, Edit, Finish
                                                   // Disable Current Element
                                                   // Habit Index
        }

        public string FinishTask()
        {
            return "8" + "2" + "1" + InfoToken[0]; // Type 3 (Operation) -
                                                   // Add, Delete, Edit, Finish
                                                   // Disable Current Element
                                                   // Habit Index
        }
        // Habit Index
        public string EditTask()
        {
            return "8" + "3" + "1" + InfoToken[0]; // Type 3 (Operation) -
                                                   // Add, Delete, Edit, Finish
                                                   // Disable Current Element
                                                   // Habit Index
        }
    }

    public class TaskEditInterface : GraphicElement
    {
        public override string Graphic { get; set; } =
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&&&&&&&&&&&&&&&&&&&&&@" + // TASK NAME
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@%%%%%%%%%%%%%%%%%%%%%%%%%%%%@" + // TASK NAME INPUT
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&&&&&&&&&&&&&&&&&&&&&@" + // TASK DUE DATE
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@%%%%%%%%%%%%%%%%%%%%%%%%%%%%@" + // TASK DUE DATE INPUT
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&&&&&&&&&&&&&&&&&&&&&@" + // TASK DIFFICULTY
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@%%%%%%%%%%%%%%%%%%%%%%%%%%%%@" + // TASK DIFFICULTY INPUT
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@&&&&&&&&&&&&&&&&&&&&&&&&&&&&@" + // CONFIRM BUTTON
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";

        #region Properties
        public override int MaxWidth { get; set; } = 30;
        public override int MaxHeight { get; set; } = 15;
        public override int[][]? MaxIndexPerInterface { get; set; } = [[0, 0], [0, 0], [0, 0], [0, 3], [0, 4], [0, 2]];

        public Button TaskNameButton { get; set; } = new Button();
        public Button TaskDueDateButton { get; set; } = new Button();
        public Button TaskDifficultyButton { get; set; } = new Button();
        public Button ConfirmButton { get; set; } = new Button();

        public InputField TaskNameInput { get; set; } = new InputField();
        public InputField TaskDueDateInput { get; set; } = new InputField();
        public InputField TaskDifficultyInput { get; set; } = new InputField();
        #endregion

        public TaskEditInterface()
        {
            ID = "010";
            InfoToken = " 1000102";
            IsGraphicElementVisibleDefault = false;
            Buttons = [];
            InputFields = [];

            TaskNameButton.HorizontalLength = 28;
            TaskNameButton.VerticalLength = 1;
            TaskNameButton.XPosition = 1;
            TaskNameButton.YPosition = 1;
            TaskNameButton.MenuInterfaceLevel = 4;
            TaskNameButton.ButtonText = CustomDisplay.CenterString("TASK NAME", 28);
            TaskNameButton.InterfaceIndexY = 0;
            TaskNameButton.InterfaceIndexX = 0;
            TaskNameButton.BindedInterface = TaskNameInput;

            TaskNameInput.HorizontalLength = 28;
            TaskNameInput.VerticalLength = 1;
            TaskNameInput.XPosition = 1;
            TaskNameInput.YPosition = 3;
            TaskNameInput.MenuInterfaceLevel = 5;
            TaskNameInput.FieldText = "";
            TaskNameInput.IsHorizontalExpandable = true;
            TaskNameInput.InterfaceIndexY = 0;
            TaskNameInput.InterfaceIndexX = 0;
            TaskNameInput.MaxFieldTextLength = 20;

            TaskDueDateButton.HorizontalLength = 28;
            TaskDueDateButton.VerticalLength = 1;
            TaskDueDateButton.XPosition = 1;
            TaskDueDateButton.YPosition = 5;
            TaskDueDateButton.MenuInterfaceLevel = 4;
            TaskDueDateButton.ButtonText = CustomDisplay.CenterString("TASK DUE DATE", 28);
            TaskDueDateButton.InterfaceIndexY = 1;
            TaskDueDateButton.InterfaceIndexX = 0;
            TaskDueDateButton.BindedInterface = TaskDueDateInput;

            TaskDueDateInput.HorizontalLength = 28;
            TaskDueDateInput.VerticalLength = 1;
            TaskDueDateInput.XPosition = 1;
            TaskDueDateInput.YPosition = 7;
            TaskDueDateInput.MenuInterfaceLevel = 5;
            TaskDueDateInput.FieldText = "";
            TaskDueDateInput.IsHorizontalExpandable = true;
            TaskDueDateInput.InterfaceIndexY = 2;
            TaskDueDateInput.InterfaceIndexX = 0;
            TaskDueDateInput.MaxFieldTextLength = 10;

            TaskDifficultyButton.HorizontalLength = 28;
            TaskDifficultyButton.VerticalLength = 1;
            TaskDifficultyButton.XPosition = 1;
            TaskDifficultyButton.YPosition = 9;
            TaskDifficultyButton.MenuInterfaceLevel = 4;
            TaskDifficultyButton.ButtonText = CustomDisplay.CenterString("TASK DIFFICULTY", 28);
            TaskDifficultyButton.InterfaceIndexY = 2;
            TaskDifficultyButton.InterfaceIndexX = 0;
            TaskDifficultyButton.BindedInterface = TaskDifficultyInput;

            TaskDifficultyInput.HorizontalLength = 28;
            TaskDifficultyInput.VerticalLength = 1;
            TaskDifficultyInput.XPosition = 1;
            TaskDifficultyInput.YPosition = 11;
            TaskDifficultyInput.MenuInterfaceLevel = 5;
            TaskDifficultyInput.FieldText = "";
            TaskDifficultyInput.IsHorizontalExpandable = true;
            TaskDifficultyInput.InterfaceIndexY = 3;
            TaskDifficultyInput.InterfaceIndexX = 0;
            TaskDifficultyInput.MaxFieldTextLength = 8;

            ConfirmButton.HorizontalLength = 28;
            ConfirmButton.VerticalLength = 1;
            ConfirmButton.XPosition = 1;
            ConfirmButton.YPosition = 13;
            ConfirmButton.MenuInterfaceLevel = 4;
            ConfirmButton.ButtonText = CustomDisplay.CenterString("CONFIRM", 28);
            ConfirmButton.InterfaceIndexY = 3;
            ConfirmButton.InterfaceIndexX = 0;
            ConfirmButton.IsInvokable = true;
            ConfirmButton.SetInvokedMethod(ConfirmAddTask);

            Buttons.Add(TaskNameButton);
            Buttons.Add(TaskDueDateButton);
            Buttons.Add(TaskDifficultyButton);
            Buttons.Add(ConfirmButton);

            InputFields.Add(TaskNameInput);
            InputFields.Add(TaskDueDateInput);
            InputFields.Add(TaskDifficultyInput);
        }

        public string ConfirmAddTask()
        {
            string TaskToken = "9";

            TaskToken += TaskNameInput.FieldText.PadRight(20, '~');
            TaskToken += TaskDueDateInput.FieldText.PadRight(10, '~');
            TaskToken += TaskDifficultyInput.FieldText.PadRight(8, '~');

            foreach (InputField inputField in InputFields!)
            {
                inputField.FieldText = "";
            }

            if (InfoToken[1] == '2')
            {
                TaskToken += "0" + InfoToken[0];
            }
            else
            {
                TaskToken += " ";
            }
            return TaskToken;
        }
    }
    #endregion
    #endregion
}
