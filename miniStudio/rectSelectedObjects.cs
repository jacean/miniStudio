using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace miniStudio
{
    class rectSelectedObjects
    {
        public rectSelectedObjects(Control t)
        {
            tab = t;
        }
        Control tab = new Control();
        bool MouseIsDown = false;
        Rectangle MouseRect = Rectangle.Empty;
        List<Control> selectedList = new List<Control>();
        public List<Control> SelectedList
        {
            get { return selectedList; }

        }
        public void resetList()
        {
            selectedList.Clear();
        }
        public void frmMain_MouseUp(object sender, MouseEventArgs e)
        {
            tab.Capture = false;
            Cursor.Clip = Rectangle.Empty;
            MouseIsDown = false;
            DrawRectangle();

            foreach (Control ct in tab.Controls)
            {
                //if (MouseRect.Contains(ct.Location))
                if(MouseRect.IntersectsWith(ct.Bounds))
                {
                    selectedList.Add(ct);
                }
            }

            MouseRect = Rectangle.Empty;
        }
        public void frmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseIsDown)
                ResizeToRectangle(e.Location);
        }
        public void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
            MouseIsDown = true;
            DrawStart(e.Location);
        }
        private void ResizeToRectangle(Point p)
        {
            DrawRectangle();
            MouseRect.Width = p.X - MouseRect.Left;
            MouseRect.Height = p.Y - MouseRect.Top;
            DrawRectangle();
        }
        private void DrawRectangle()
        {
            Rectangle rect = tab.RectangleToScreen(MouseRect);
            ControlPaint.DrawReversibleFrame(rect, Color.White, FrameStyle.Dashed);
        }
        private void DrawStart(Point StartPoint)
        {
            tab.Capture = true;
            Cursor.Clip = tab.RectangleToScreen(new Rectangle(0, 0, tab.Width, tab.Height));

            MouseRect = new Rectangle(StartPoint.X, StartPoint.Y, 0, 0);
        }
    }
}
