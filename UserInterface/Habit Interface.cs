﻿using Habit_User_Data_Structures;

namespace UserInterface
{
    public abstract class HabitInterface
    {
        public int MenuInterfaceLevel { get; set; }
        public bool IsInterfaceSelected { get; set; }
        public int InterfaceIndexY { get; set; }
        public int InterfaceIndexX { get; set; }
        public int MaximumInterfaceSelector { get; set; }
        public int HorizontalLength { get; set; }
        public int VerticalLength { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int RenderPointerX { get; set; } = 0;
        public int RenderPointerY { get; set; } = 0;
        public bool IsInvokable { get; set; } = false;
        public bool HasSubInterface { get; set; } = false; // TODO
        public bool IsSubInterfaceSelected { get; set; } = false; // Implement highlighting of habit/journal/tasks if it is selected
    }

    public abstract class InterfaceOption : HabitInterface // Mother class for options like add, edit, delete
    {
        public required string DisplayText { get; set; }
        public virtual void InvokedAction() { }
    }


    public class VariableLabel : HabitInterface
    {
        public string LabelText { get; set; } = "";

        public void RenderLabel(int RelativeX, int RelativeY, GraphicElement graphicElement, int graphicIndex)
        {
            if (RelativeX == XPosition + RenderPointerX && RelativeY == YPosition + RenderPointerY)
            {
                int RenderIndex = (RenderPointerY * HorizontalLength) + RenderPointerX;
                ++RenderPointerX;

                if (RenderIndex == (HorizontalLength - 1) + ((HorizontalLength) * RenderPointerY))
                {
                    RenderPointerX = 0;
                    ++RenderPointerY;
                }

                if (LabelText.Length <= RenderIndex)
                {
                    Console.Write(" ");
                    return;
                }

                CustomDisplay.DisplayColoredText(LabelText[RenderIndex].ToString(), ConsoleColor.Red);

                if (RenderIndex == (HorizontalLength * VerticalLength) - 1)
                {
                    RenderPointerX = 0;
                    RenderPointerY = 0;
                    return;
                }
             
            }
        }
    }

    public class Button : HabitInterface
    {
        public string ButtonText { get; set; } = "";
        public ConsoleColor TextColor { get; set; } = ConsoleColor.Red;
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
            int CurrentInterfaceIndexSelectorY, int CurrentInterfaceIndexSelectorX, int CurrentInterfaceLevel)
        {
            if (RelativeX == XPosition + RenderPointerX && RelativeY == YPosition + RenderPointerY)
            {
                int RenderIndex = (RenderPointerY * HorizontalLength) + RenderPointerX;

                if (ButtonText.Length <= RenderIndex)
                {
                    Console.Write(" ");
                    ++RenderPointerX;
                    return;
                }

                if (RenderIndex == (HorizontalLength - 1) + ((HorizontalLength) * RenderPointerY))
                {
                    RenderPointerX = 0;
                    ++RenderPointerY;
                }

                if (CurrentInterfaceIndexSelectorY == InterfaceIndexY && CurrentInterfaceIndexSelectorX == InterfaceIndexX
                    && MenuInterfaceLevel == CurrentInterfaceLevel)
                {
                    CustomDisplay.DisplayHighlightedText(ButtonText[RenderIndex].ToString());
                }
                else
                {
                    CustomDisplay.DisplayColoredText(ButtonText[RenderIndex].ToString(), TextColor);
                }

                if (RenderIndex == (HorizontalLength * VerticalLength) - 1)
                {
                    RenderPointerX = 0;
                    RenderPointerY = 0;
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
        public List<string> TextRestrictedTo { get; set; } = [];

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
                if (InputIndex == (HorizontalLength - 1) + ((HorizontalLength) * RenderPointerY))
                {
                    RenderPointerX = 0;
                    ++RenderPointerY;
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

        public void ClearFieldText() { FieldText = ""; }
    }

}
