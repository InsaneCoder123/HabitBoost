namespace UserInterface
{
    // @ = Green Color
    // . = Blank Space
    // % = Input Field
    // # = Next Line
    // $ = End
    // & = Button
    // + = Variable Label
    // Else = Button
    public abstract class GraphicElement 
    {
        public int RenderPointerX { get; set; } = 0;
        public int RenderPointerY { get; set; } = 0;

        public virtual int MaxWidth { get; set; } = 0;
        public virtual int MaxHeight { get; set; } = 0;

        public bool IsGraphicElementActive { get; set; } = false;

        public int AbsolutePositionX { get; set; } = 0;
        public int AbsolutePositionY { get; set; } = 0;

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
            //loginButton.SetInvokedMethod(LoginUser);

            Buttons.Add(loginButton);
            Buttons.Add(createUserButton);
        }

        public string SwitchToLoginScreen()
        {
            return "1" + ((int)ProgramScreen.Login).ToString();
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
        public InputField UsernameInput { get; set; } = new InputField();
        public InputField PasswordInput { get; set; } = new InputField();
        public Button Username { get; set; } = new Button();
        public Button Password { get; set; } = new Button();
        public Button Login { get; set; } = new Button();

        public LoginGraphic()
        {
            InputFields = [];
            Buttons = [];

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

    #region Main Scene Graphics
    public class TopBarGraphics : GraphicElement
    {
        public override string Graphic { get; set; } =
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@USERNAME ++++++++++     LVL +++     EXP +++++++@&&&&&&@&&&&&&&@&&&&@" + //HABITS + JOURNAL + TODO
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";

        public override int MaxWidth { get; set; } = 69;
        public override int MaxHeight { get; set; } = 3;

        public Button HabitsButton { get; set; } = new Button();
        public Button JournalButton { get; set; } = new Button();
        public Button ToDoButton { get; set; } = new Button();

        public VariableLabel UsernameLabel { get; set; } = new VariableLabel();
        public VariableLabel LevelLabel { get; set; } = new VariableLabel();
        public VariableLabel ExperienceLabel { get; set; } = new VariableLabel();

        public TopBarGraphics()
        {
            Buttons = [];
            Labels = [];
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

            HabitsButton.HorizontalLength = 6;
            HabitsButton.VerticalLength = 1;
            HabitsButton.XPosition = 49;
            HabitsButton.YPosition = 1;
            HabitsButton.MenuInterfaceLevel = 0;
            HabitsButton.ButtonText = "HABITS";
            HabitsButton.InterfaceIndexY = 0;
            HabitsButton.InterfaceIndexX = 0;

            JournalButton.HorizontalLength = 7;
            JournalButton.VerticalLength = 1;
            JournalButton.XPosition = 56;
            JournalButton.YPosition = 1;
            JournalButton.MenuInterfaceLevel = 0;
            JournalButton.ButtonText = "JOURNAL";
            JournalButton.InterfaceIndexY = 0;
            JournalButton.InterfaceIndexX = 1;

            ToDoButton.HorizontalLength = 4;
            ToDoButton.VerticalLength = 1;
            ToDoButton.XPosition = 64;
            ToDoButton.YPosition = 1;
            ToDoButton.MenuInterfaceLevel = 0;
            ToDoButton.ButtonText = "TODO";
            ToDoButton.InterfaceIndexY = 0;
            ToDoButton.InterfaceIndexX = 2;

            Buttons.Add(HabitsButton);
            Buttons.Add(JournalButton);
            Buttons.Add(ToDoButton);

            Labels.Add(UsernameLabel);
            Labels.Add(LevelLabel);
            Labels.Add(ExperienceLabel);
        }
    }

    public class HabitListGraphics : GraphicElement
    {
        public override string Graphic { get; set; } =
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@       &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&   ++++++++++       @" + // 40 Character Limit for Habit Name NAME/FINISHED/UNFINISHED
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@       &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&   ++++++++++       @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@       &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&   ++++++++++       @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@       &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&   ++++++++++       @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@       &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&   ++++++++++       @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@       &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&   ++++++++++       @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@       &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&   ++++++++++       @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@       &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&   ++++++++++       @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@       &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&   ++++++++++       @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
            "@       &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&   ++++++++++       @" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";


        public override int MaxWidth { get; set; } = 69;
        public override int MaxHeight { get; set; } = 3;
    }
    #endregion
}
