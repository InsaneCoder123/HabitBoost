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

        public bool IsGraphicElementVisible { get; set; } = false;
        public bool IsGraphicElementVisibleDefault { get; set; } = true;
        public bool IsGraphicElementActive { get; set; } = false;
        public bool IsDynamic { get; set; } = false;

        public static bool IsHabitListEmpty { get; set; } = false;
        public static bool IsJournalListEmpty { get; set; } = false;
        public static bool IsToDoListEmpty { get; set; } = false;


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

        public InputField? GetCurrentActiveInputField ()
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
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@";

        public override int MaxWidth { get; set; } = 28;
        public override int MaxHeight { get; set; } = 5;

        public Button loginButton { get; set; } = new Button();
        public Button createUserButton { get; set; } = new Button();

        public MainMenuGraphic()
        {
            Buttons = [];
            
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

            Buttons.Add(loginButton);
            Buttons.Add(createUserButton);
        }

        public string SwitchToLoginScreen()
        {
            return "1" + ((int)ProgramScreen.Login).ToString();
        }

        public string SwitchToCreateAccountScreen()
        {
            return "1" + ((int)ProgramScreen.CreateUser).ToString();
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
        }

        public string SetHabitListActive()
        {
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
            if (IsJournalListEmpty)
            {
                return "3" + "1" + "0" + InfoToken[0];
            }
            return "2" + "004" + "1" + "0" + "0" + "1" + "0" + "1" + "0" + "0"; // Type - ID to toggle-
                                                                    // 1/0 True/False -
                                                                    // InterfaceY index default -
                                                                    // InterfaceX index default-
                                                                    // Interface level default
                                                                    // 0 false 1 true, if turn off current active graphic
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
                    LabelText = user.HabitList[i].Name.PadRight(40) + "   " + (user.HabitList[i].Completed ? "FINISHED" : "UNFINISHED").PadRight(10)
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
            "@@&&&&&&&&&&&&@@&&&&&&&&&&&&@@" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";

        public override int MaxWidth { get; set; } = 30;
        public override int MaxHeight { get; set; } = 12;

        public Button HabitUserName { get; set; } = new Button();
        public Button HabitDifficulty { get; set; } = new Button();
        public Button ConfirmButton { get; set; } = new Button();
        public Button CancelButton { get; set; } = new Button();

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
            HabitUserName.ButtonText = "          HABIT NAME          ";
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

            ConfirmButton.HorizontalLength = 12;
            ConfirmButton.VerticalLength = 1;
            ConfirmButton.XPosition = 2;
            ConfirmButton.YPosition = 10;
            ConfirmButton.MenuInterfaceLevel = 4;
            ConfirmButton.ButtonText = "  CONFIRM   ";
            ConfirmButton.InterfaceIndexY = 2;
            ConfirmButton.InterfaceIndexX = 0;
            ConfirmButton.IsInvokable = true;
            ConfirmButton.SetInvokedMethod(ConfirmAddHabit);

            CancelButton.HorizontalLength = 12;
            CancelButton.VerticalLength = 1;
            CancelButton.XPosition = 16;
            CancelButton.YPosition = 10;
            CancelButton.MenuInterfaceLevel = 4;
            CancelButton.ButtonText = "   CANCEL   ";
            CancelButton.InterfaceIndexY = 2;
            CancelButton.InterfaceIndexX = 1;
            CancelButton.IsInvokable = true;
            CancelButton.SetInvokedMethod(CancelAddHabit);

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
            Buttons.Add(CancelButton);

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

        public string CancelAddHabit() {
            foreach (InputField inputField in InputFields)
            {
                inputField.FieldText = "";
            }
            return "-1";
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
                    LabelText = user.JournalList[i].Name.PadRight(30) + "   " + user.JournalList[i].DateCreated.ToShortDateString().PadRight(20)
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
            return "6" + "2" + "1" + InfoToken[0]; // Type 3 (Operation) -
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

        public override int MaxWidth { get; set; } = 35;
        public override int MaxHeight { get; set; } = 3;
    }

    #endregion
        #endregion
}
