using CustomPresentationControls.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WorkHelper.Controls;
using WorkHelper.Models;
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
            if (!e.Handled && sender is Panel target && e.Data.GetData("Object") is FrameworkElement element && element.FindParent<StackPanel>() is StackPanel source)
            {
                string sourceStory = source.FindNearestAncestorTag() as string;
                string sourceCollection = source.Tag as string;
                string destinationStory = target.FindNearestAncestorTag() as string;
                string destinationCollection = target.Tag as string;
                double cursorY = e.GetPosition(target).Y;
                double totalElementHeight = 0;
                int elementIndex = 0;
                foreach (FrameworkElement child in target.Children)
                {
                    if (totalElementHeight + child.ActualHeight / 2 + child.Margin.Top > cursorY)
                    {
                        break;
                    }
                    totalElementHeight += child.ActualHeight + child.Margin.Top + child.Margin.Bottom;
                    elementIndex++;
                }
                if (DataContext is ITaskBoard viewModel)
                {
                    viewModel.MoveWorkItem(element.DataContext, sourceCollection, destinationCollection, sourceStory, destinationStory, elementIndex);
                }
            }
        }
        private void TaskItem_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (_adorner != null)
            {
                Point pos = PointFromScreen(GetMousePosition());
                _adorner.UpdatePosition(pos);
            }
            else
            {
                e.Handled = false;
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
            bool canMove = false;
            if (sender is WorkItemControl control)
            {
                canMove = control.CanMove;
            }
            Point currentPosition = e.GetPosition(this);
            Vector diff = _startPoint - currentPosition;
            if (e.LeftButton == MouseButtonState.Pressed && canMove && sender is FrameworkElement source && source.DataContext is Task)
            {
                DataObject data = new DataObject();
                data.SetData("Object", source);

                WorkItemControl workItem = sender as WorkItemControl;
                _adorner = new DragAdorner(workItem, currentPosition);
                AdornerLayer.GetAdornerLayer(workItem).Add(_adorner);
                DragDrop.DoDragDrop(source, data, DragDropEffects.Move);
                AdornerLayer layer = AdornerLayer.GetAdornerLayer(workItem);
                if (layer != null)
                {
                    layer.Remove(_adorner);
                }
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
