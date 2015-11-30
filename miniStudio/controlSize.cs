﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace miniStudio
{
    class controlSize
    {
        //调用方法
        //private void initProperty()
        //{
        //    controlSize cs = new controlSize(this);
        //    for (int i = 0; i < form.panel1.Controls.Count; i++)
        //    {
        //        form.panel1.Controls[i].MouseDown += new System.Windows.Forms.MouseEventHandler(MyMouseDown);
        //        form.panel1.Controls[i].MouseLeave += new System.EventHandler(MyMouseLeave);
        //        form.panel1.Controls[i].MouseMove += new System.Windows.Forms.MouseEventHandler(MyMouseMove);
        //    }

        //}  
        public controlSize(Form f)
        {
            form = f;
        }
        Form form = new Form();
        private enum EnumMousePointPosition
        {
            MouseSizeNone = 0, //'无  
            MouseSizeRight = 1, //'拉伸右边框  
            MouseSizeLeft = 2, //'拉伸左边框  
            MouseSizeBottom = 3, //'拉伸下边框  
            MouseSizeTop = 4, //'拉伸上边框  
            MouseSizeTopLeft = 5, //'拉伸左上角  
            MouseSizeTopRight = 6, //'拉伸右上角  
            MouseSizeBottomLeft = 7, //'拉伸左下角  
            MouseSizeBottomRight = 8, //'拉伸右下角  
            MouseDrag = 9   // '鼠标拖动  
        }
        const int Band = 5;
        const int MinWidth = 10;
        const int MinHeight = 10;
        private EnumMousePointPosition m_MousePointPosition;
        private Point p, p1;

        public void MyMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            p.X = e.X;
            p.Y = e.Y;
            p1.X = e.X;
            p1.Y = e.Y;
        }
        public void MyMouseLeave(object sender, System.EventArgs e)
        {
            m_MousePointPosition = EnumMousePointPosition.MouseSizeNone;
            form.Cursor= Cursors.Arrow;
        }
        private EnumMousePointPosition MousePointPosition(Size size, System.Windows.Forms.MouseEventArgs e)
        {

            if ((e.X >= -1 * Band) | (e.X <= size.Width) | (e.Y >= -1 * Band) | (e.Y <= size.Height))
            {
                if (e.X < Band)
                {
                    if (e.Y < Band) { return EnumMousePointPosition.MouseSizeTopLeft; }
                    else
                    {
                        if (e.Y > -1 * Band + size.Height)
                        { return EnumMousePointPosition.MouseSizeBottomLeft; }
                        else
                        { return EnumMousePointPosition.MouseSizeLeft; }
                    }
                }
                else
                {
                    if (e.X > -1 * Band + size.Width)
                    {
                        if (e.Y < Band)
                        { return EnumMousePointPosition.MouseSizeTopRight; }
                        else
                        {
                            if (e.Y > -1 * Band + size.Height)
                            { return EnumMousePointPosition.MouseSizeBottomRight; }
                            else
                            { return EnumMousePointPosition.MouseSizeRight; }
                        }
                    }
                    else
                    {
                        if (e.Y < Band)
                        { return EnumMousePointPosition.MouseSizeTop; }
                        else
                        {
                            if (e.Y > -1 * Band + size.Height)
                            { return EnumMousePointPosition.MouseSizeBottom; }
                            else
                            { return EnumMousePointPosition.MouseDrag; }
                        }
                    }
                }
            }
            else
            { return EnumMousePointPosition.MouseSizeNone; }
        }

        public void MyMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Control lCtrl = (sender as Control);

            if (e.Button == MouseButtons.Left)
            {
                switch (m_MousePointPosition)
                {
                    case EnumMousePointPosition.MouseDrag:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Top = lCtrl.Top + e.Y - p.Y;
                        break;
                    case EnumMousePointPosition.MouseSizeBottom:
                        lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点  
                        break;
                    case EnumMousePointPosition.MouseSizeBottomRight:
                        lCtrl.Width = lCtrl.Width + e.X - p1.X;
                        lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点  
                        break;
                    case EnumMousePointPosition.MouseSizeRight:
                        lCtrl.Width = lCtrl.Width + e.X - p1.X;
                        //       lCtrl.Height = lCtrl.Height + e.Y - p1.Y;  
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点  
                        break;
                    case EnumMousePointPosition.MouseSizeTop:
                        lCtrl.Top = lCtrl.Top + (e.Y - p.Y);
                        lCtrl.Height = lCtrl.Height - (e.Y - p.Y);
                        break;
                    case EnumMousePointPosition.MouseSizeLeft:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Width = lCtrl.Width - (e.X - p.X);
                        break;
                    case EnumMousePointPosition.MouseSizeBottomLeft:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Width = lCtrl.Width - (e.X - p.X);
                        lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点  
                        break;
                    case EnumMousePointPosition.MouseSizeTopRight:
                        lCtrl.Top = lCtrl.Top + (e.Y - p.Y);
                        lCtrl.Width = lCtrl.Width + (e.X - p1.X);
                        lCtrl.Height = lCtrl.Height - (e.Y - p.Y);
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点  
                        break;
                    case EnumMousePointPosition.MouseSizeTopLeft:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Top = lCtrl.Top + (e.Y - p.Y);
                        lCtrl.Width = lCtrl.Width - (e.X - p.X);
                        lCtrl.Height = lCtrl.Height - (e.Y - p.Y);
                        break;
                    default:
                        break;
                }
                if (lCtrl.Width < MinWidth) lCtrl.Width = MinWidth;
                if (lCtrl.Height < MinHeight) lCtrl.Height = MinHeight;

            }
            else
            {
                m_MousePointPosition = MousePointPosition(lCtrl.Size, e);   //'判断光标的位置状态  
                switch (m_MousePointPosition)   //'改变光标  
                {
                    case EnumMousePointPosition.MouseSizeNone:
                        form.Cursor = Cursors.Arrow;        //'箭头  
                        break;
                    case EnumMousePointPosition.MouseDrag:
                        form.Cursor = Cursors.SizeAll;      //'四方向  
                        break;
                    case EnumMousePointPosition.MouseSizeBottom:
                        form.Cursor = Cursors.SizeNS;       //'南北  
                        break;
                    case EnumMousePointPosition.MouseSizeTop:
                        form.Cursor = Cursors.SizeNS;       //'南北  
                        break;
                    case EnumMousePointPosition.MouseSizeLeft:
                        form.Cursor = Cursors.SizeWE;       //'东西  
                        break;
                    case EnumMousePointPosition.MouseSizeRight:
                        form.Cursor = Cursors.SizeWE;       //'东西  
                        break;
                    case EnumMousePointPosition.MouseSizeBottomLeft:
                        form.Cursor = Cursors.SizeNESW;     //'东北到南西  
                        break;
                    case EnumMousePointPosition.MouseSizeBottomRight:
                        form.Cursor = Cursors.SizeNWSE;     //'东南到西北  
                        break;
                    case EnumMousePointPosition.MouseSizeTopLeft:
                        form.Cursor = Cursors.SizeNWSE;     //'东南到西北  
                        break;
                    case EnumMousePointPosition.MouseSizeTopRight:
                        form.Cursor = Cursors.SizeNESW;     //'东北到南西  
                        break;
                    default:
                        break;
                }
            }

        }

       
    }
}
