using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WorkHelper.Controls;
using WorkHelper.Utilities;

namespace WorkHelper.TaskBoard
{
    /// <summary>
    /// Interaction logic for TaskBoardView.xaml
    /// </summary>
    public partial class TaskBoardView : UserControl
    {
        private Point _startPoint;
        private DragAdorner _adorner;
        public TaskBoardView()
        {
            InitializeComponent();
        }
        private void Panel_Drop(object sender, DragEventArgs e)
        {
            if (!e.Handled && sender is Panel destination && e.Data.GetData("Object") is FrameworkElement element && VisualTreeHelper.GetParent(element) is Panel origin)
            {
                double cursorY = e.GetPosition(destination).Y;
                double totalElementHeight = 0;
                int elementCount = 0;
                foreach (FrameworkElement child in destination.Children)
                {
                    if (totalElementHeight + child.ActualHeight/2+child.Margin.Top > cursorY)
                    {
                        destination.Children.Insert(elementCount, element);
                        return;
                    }
                    totalElementHeight += child.ActualHeight + child.Margin.Top + child.Margin.Bottom;
                    elementCount++;
                }
                origin.Children.Remove(element);
                destination.Children.Add(element);
            }
        }
        private void TaskItem_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (_adorner != null)
            {
                Point pos = PointFromScreen(GetMousePosition());
                _adorner.UpdatePosition(pos);
            }
        }
        private void TaskItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }
        private void TaskItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(this);
        }
        private void TaskItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                UIElement source = sender as UIElement;
                DataObject data = new DataObject();
                data.SetData("Object", source);

                WorkItemControl workItem = sender as WorkItemControl;
                Point currentPosition = e.GetPosition(this);
                _adorner = new DragAdorner(workItem, currentPosition);
                AdornerLayer.GetAdornerLayer(workItem).Add(_adorner);
                DragDrop.DoDragDrop(source, data, DragDropEffects.Move);
                AdornerLayer.GetAdornerLayer(workItem).Remove(_adorner);
                _startPoint = currentPosition;
            }
        }
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        }
        public static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }

    }
}
