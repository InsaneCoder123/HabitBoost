namespace UserInterface
{
    // @ = Green Color
    // % = Input Field
    // # = Next Line
    // $ = End
    // & = Button
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
            UsernameInput.InterfaceIndex = 0;

            PasswordInput.HorizontalLength = 20;
            PasswordInput.VerticalLength = 1;
            PasswordInput.XPosition = 10;
            PasswordInput.YPosition = 3;
            PasswordInput.MenuInterfaceLevel = 1;
            PasswordInput.FieldText = "";
            PasswordInput.IsPrivate = true;
            UsernameInput.IsHorizontalExpandable = true;
            PasswordInput.InterfaceIndex = 1;

            Username.HorizontalLength = 8;
            Username.VerticalLength = 1;
            Username.XPosition = 1;
            Username.YPosition = 1;
            Username.MenuInterfaceLevel = 0;
            Username.ButtonText = "USERNAME";
            Username.BindedInterface = UsernameInput;
            Username.InterfaceIndex = 0;

            Password.HorizontalLength = 8;
            Password.VerticalLength = 1;
            Password.XPosition = 1;
            Password.YPosition = 3;
            Password.MenuInterfaceLevel = 0;
            Password.BindedInterface = PasswordInput;
            Password.ButtonText = "PASSWORD";
            Password.InterfaceIndex = 1;

            Login.HorizontalLength = 29;
            Login.VerticalLength = 1;
            Login.XPosition = 1;
            Login.YPosition = 5;
            Login.MenuInterfaceLevel = 0;
            Login.ButtonText = "           LOGIN             ";
            Login.InterfaceIndex = 2;
            Login.IsInvokable = true;
            Login.SetInvokedMethod(LoginUser);

            InputFields.Add(UsernameInput);
            InputFields.Add(PasswordInput);

            Buttons.Add(Username);
            Buttons.Add(Password);
            Buttons.Add(Login);
        }

        public static object LoginUser()
        {
            // Login
            // - Check if user exists
            // - Check if password is correct
            // - If user does not exist, create a new user
            // - If password is incorrect, prompt user to re-enter password
            return new object();
        }
    }
}
