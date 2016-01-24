using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace miniStudio
{
    class controlFunc
    {
        
        #region 矩形选框函数
        public static Rectangle MouseRect = Rectangle.Empty;//画矩形框
        public static void ResizeToRectangle(Point p, Control tab)
        {
            DrawRectangle(tab);
            MouseRect.Width = p.X - MouseRect.Left;
            MouseRect.Height = p.Y - MouseRect.Top;
            DrawRectangle(tab);
        }
        public static void DrawRectangle(Control tab)
        {
            Rectangle rect = tab.RectangleToScreen(MouseRect);
            ControlPaint.DrawReversibleFrame(rect, Color.White, FrameStyle.Dashed);
        }
        public static void DrawStart(Point StartPoint, Control tab)
        {
            tab.Capture = true;
            Cursor.Clip = tab.RectangleToScreen(new Rectangle(0, 0, tab.Width, tab.Height));

            MouseRect = new Rectangle(StartPoint.X, StartPoint.Y, 0, 0);
        }
        #endregion
    }
}
