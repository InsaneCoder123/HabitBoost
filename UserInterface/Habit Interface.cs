using Habit_User_Data_Structures;

namespace UserInterface
{
    public abstract class HabitInterface
    {
        public int MenuInterfaceLevel { get; set; }
        public bool IsInterfaceSelected { get; set; }
        public int InterfaceIndex { get; set; }
        public int MaximumInterfaceSelector { get; set; }
        public int HorizontalLength { get; set; }
        public int VerticalLength { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int RenderPointerX { get; set; } = 0;
        public int RenderPointerY { get; set; } = 0;
        public bool IsInvokable { get; set; } = false;
    }

    public abstract class InterfaceOption : HabitInterface // Mother class for options like add, edit, delete
    {
        public required string DisplayText { get; set; }
        public virtual void InvokedAction() { }
    }

    public class HabitSelectable : HabitInterface // Selectables like the list of habits, todos, or journal entries
    {
        public bool IsOptionSelectable { get; set; } // If the user can enter the selectable and select an option
        public required InterfaceOption Option { get; set; }
        public required HabitBoostData Content { get; set; }
    }

    public class HabitSubMenu : HabitInterface // Submenus like the habit menu, todo menu, or journal menu
    {
        public required string SubMenuName { get; set; }
        public required List<HabitSelectable> Selectables { get; set; }
    }

    public class Button : HabitInterface
    {
        public string ButtonText { get; set; } = "";
        public int ButtonXPosition { get; set; }
        public int ButtonYPosition { get; set; }
        public HabitInterface? BindedInterface { get; set; }
        private Func<string>? AtInvoked;

        public void SetInvokedMethod(Func<string>? method)
        {
            if (method == null) { return; }
            AtInvoked = method;
        }

        public string InvokeButton()
        {
            if (AtInvoked == null) { return ""; }
            return AtInvoked();
        }

        public void RenderButton(int RelativeX, int RelativeY, GraphicElement graphicElement, int graphicIndex, 
            int CurrentInterfaceIndexSelector, int CurrentInterfaceLevel)
        {
            if (RelativeX == XPosition + RenderPointerX && RelativeY == YPosition + RenderPointerY)
            {
                int InputIndex = (RenderPointerY * HorizontalLength) + RenderPointerX;

                if (ButtonText.Length <= InputIndex)
                {
                    Console.Write(" ");
                    ++RenderPointerX;
                    return;
                }

                if (CurrentInterfaceIndexSelector == InterfaceIndex && MenuInterfaceLevel == CurrentInterfaceLevel)
                {
                    CustomDisplay.DisplayHighlightedText(ButtonText[InputIndex].ToString());
                }
                else
                {
                    CustomDisplay.DisplayColoredText(ButtonText[InputIndex].ToString(), ConsoleColor.Red);
                }

                if (InputIndex == (HorizontalLength * VerticalLength) - 1)
                {
                    RenderPointerX = 0;
                    RenderPointerY = 0;
                    return;
                }

                if (InputIndex == HorizontalLength - 1)
                {
                    RenderPointerX = 0;
                    ++RenderPointerY;
                    return;
                }

                ++RenderPointerX;
            }
        }
}

public class InputField : HabitInterface
    {
        public string FieldText { get; set; } = "";
        public int StartingIndex { get; set; } = 0;
        public bool IsPrivate { get; set; } = false;
        public bool IsHorizontalExpandable { get; set; } = false;
        public int MaxFieldTextLength { get; set; } = -1;

        public void RenderInputField(int RelativeX, int RelativeY) 
        { 
            if (RelativeX == XPosition + RenderPointerX && RelativeY == YPosition + RenderPointerY)
            {
                int InputIndex = (RenderPointerY * HorizontalLength) + RenderPointerX;
                ++RenderPointerX;
                if (InputIndex == (HorizontalLength * VerticalLength) - 1)
                {
                    RenderPointerX = 0;
                    RenderPointerY = 0;
                    Console.Write(" ");
                    return;
                }

                if (FieldText.Length <= InputIndex) 
                {
                    if (IsInterfaceSelected && FieldText.Length == 0)
                    {
                        CustomDisplay.DisplayColor(ConsoleColor.Green);
                        return;
                    }
                    Console.Write(" ");
                    return;
                }


                // TODO fix the last char not highlighting when it is private
                if (IsInterfaceSelected && StartingIndex + InputIndex == FieldText.Length - 1 )
                {
                    if (IsPrivate) { CustomDisplay.DisplayHighlightedText("*"); return; }
                    CustomDisplay.DisplayHighlightedText(FieldText[StartingIndex + InputIndex].ToString());
                    return;
                }
                if (IsPrivate) { Console.Write("*"); }
                else { Console.Write(FieldText[StartingIndex + InputIndex]); }


                if (InputIndex == HorizontalLength - 1)
                {
                    RenderPointerX = 0;
                    ++RenderPointerY;
                    return;
                }

            }
        }

        public void AddFieldText(char input)
        {
            if (MaxFieldTextLength != -1 && FieldText.Length >= MaxFieldTextLength) { return; }
            FieldText += input;
            if (IsHorizontalExpandable && StartingIndex + FieldText.Length >= HorizontalLength)
            {
                ++StartingIndex;
            }
        }
        public void RemoveFieldText() 
        {
            FieldText = FieldText.Remove(FieldText.Length - 1);
            if (IsHorizontalExpandable && StartingIndex > 0)
            {
                --StartingIndex;
            }
        }
    }

}
