using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

class PhysicalInputLib
{
    [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetCursorPos(int X, int Y);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

    [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
    private static extern IntPtr FindWindow(string lpClassName,
        string lpWindowName);

    // Activate an application window.
    [DllImport("USER32.DLL")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
        public static implicit operator Point(POINT point)
        {
            return new Point(point.X, point.Y);
        }
    }
    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);
    private const int MOUSEEVENTF_LEFTDOWN = 0x02;
    private const int MOUSEEVENTF_LEFTUP = 0x04;
    private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
    private const int MOUSEEVENTF_RIGHTUP = 0x10;
    public static int[] GetCursorPositionInt()
    {
        int[] cords = new int[2];
        POINT lpPoint;
        GetCursorPos(out lpPoint);
        cords[0] = lpPoint.X;
        cords[1] = lpPoint.Y;
        return cords;
    }
    public static void DoSetCursorPos(int X, int Y)
    {
        SetCursorPos(X, Y);
    }
    public static void DoLeftClick(int X, int Y)
    {
        if (X == 0) X = Cursor.Position.X;
        if (Y == 0) Y = Cursor.Position.Y;
        SetCursorPos(X, Y);
        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
    }
    public static void DoRightClick(int X, int Y)
    {
        if (X == 0) X = Cursor.Position.X;
        if (Y == 0) Y = Cursor.Position.Y;
        SetCursorPos(X, Y);
        mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, X, Y, 0, 0);
    }
    public static void DoLeftClickDown(int X, int Y)
    {
        if (X == 0) X = Cursor.Position.X;
        if (Y == 0) Y = Cursor.Position.Y;
        SetCursorPos(X, Y);
        mouse_event(MOUSEEVENTF_LEFTDOWN , X, Y, 0, 0);
    }
    public static void DoLeftClickUp(int X, int Y)
    {
        if (X == 0) X = Cursor.Position.X;
        if (Y == 0) Y = Cursor.Position.Y;
        SetCursorPos(X, Y);
        mouse_event(MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
    }
    public static void DoRightClickDown(int X, int Y)
    {
        if (X == 0) X = Cursor.Position.X;
        if (Y == 0) Y = Cursor.Position.Y;
        SetCursorPos(X, Y);
        mouse_event(MOUSEEVENTF_RIGHTDOWN, X, Y, 0, 0);
    }
    public static void DoRightClickUp(int X, int Y)
    {
        if (X == 0) X = Cursor.Position.X;
        if (Y == 0) Y = Cursor.Position.Y;
        SetCursorPos(X, Y);
        mouse_event(MOUSEEVENTF_RIGHTUP, X, Y, 0, 0);
    }
    public static void DoWait(int milliseconds)
    {
        bool exit = false;
        DateTime starter = DateTime.Now;
        do
        {
            if (DateTime.Now>starter.AddMilliseconds(milliseconds))
            {
                System.Diagnostics.Debug.WriteLine("Expired");
                exit = true;
            }
            if (exit==true) break;
            Application.DoEvents();
        } while (1 == 1);
        //System.Threading.Thread.Sleep(milliseconds);
    }
    public static void DoTyping(string WhatToType)
    {

        DoTyping(WhatToType, 50);
    }
    public static void DoClickStartButton()
    {
        
        //Get Max Screen Height
        DoLeftClick(1, 899);
    }
    public static void DoTyping(string WhatToType, int millisconddelay)
    {
        /*
        http://www.autoitscript.com/autoit3/docs/appendix/SendKeys.htm
        + is Shift
        ^ is Ctrl
        % is Alt
        !!use lowercase!!
        SendKeys.Send("{TAB}") Navigate to next control (button, checkbox, etc)
        Send("+{TAB}") Navigate to previous control.
        Send("^{TAB}") Navigate to next WindowTab (on a Tabbed dialog window) 
        Send("^+{TAB}") Navigate to previous WindowTab. 
        Send("{SPACE}") Can be used to toggle a checkbox or click a button. 
        Send("{+}") Usually checks a checkbox (if it's a "real" checkbox.) 
        Send("{-}") Usually unchecks a checkbox. 
        Send("{NumPadMult}") Recursively expands folders in a SysTreeView32.  
        */
        string CharToExamine="";
        string CharsToType="";
        bool SpecialChars = false;
        bool AssembleString=false;
        for (int i = 0; i < WhatToType.Length; i++)
        {
            Application.DoEvents();
            CharToExamine = WhatToType.Substring(i, 1);
            if ((CharToExamine == "+") || (CharToExamine == "^") || (CharToExamine == "%"))
            {
                System.Diagnostics.Debug.WriteLine("Special Char");
                CharsToType = CharsToType + CharToExamine;
                SpecialChars = true;
                continue;
            }
            else if (CharToExamine == "{")
            {
                System.Diagnostics.Debug.WriteLine("AssembleString = true");
                AssembleString = true;
            }

            else if (CharToExamine == "}")
            {
                System.Diagnostics.Debug.WriteLine("AssembleString = false");
                AssembleString = false;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Should just be a standard character to send... <" + CharToExamine + ">");
            }
            CharsToType = CharsToType + CharToExamine;
            if (AssembleString == false)
            {
                if (SpecialChars == true)
                {
                    SendKeys.Send(CharsToType);
                }
                else
                {
                    SendKeys.SendWait(CharsToType);
                }
                DoWait(millisconddelay);
                System.Diagnostics.Debug.WriteLine(millisconddelay.ToString());
                CharsToType = "";
                SpecialChars = false;
            }
        }
        System.Diagnostics.Debug.WriteLine("Done Typing");
    }

}