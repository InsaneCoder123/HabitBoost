namespace UserInterface
{
    public static class ProgramManager
    {
        public static bool DebugMode { get; set; } = false;

        public static bool isProgramRunning { get; set; }
        public static ConsoleKeyInfo keyPressedInfo { get; set; }
        public static DateTime currentDate { get; set; }

        public static void StartProgram()
        {
            isProgramRunning = true;
            currentDate = DateTime.Today;
        }

        public static void AttemptLogin() 
        {
            // Login
            // - Check if user exists
            // - Check if password is correct
            // - If user does not exist, create a new user
            // - If password is incorrect, prompt user to re-enter password
        }

        public static void CreatePassword()
        {
            // Create password
            // - Check if password is strong
            // - If password is weak, prompt user to re-enter password
            // If password is strong, create a new user
            // Then store user data to a file contained in a folder named "UserData" in the User's folder
        }
    }
}
