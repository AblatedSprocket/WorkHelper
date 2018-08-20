using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace WorkHelper.Utilities
{
    public class DragAdorner : Adorner
    {
        private Point _location;
        private Brush _visualBrush;
        private Point _offset;
        public DragAdorner(UIElement adornedElement, Point offset) : base(adornedElement)
        {
            _offset = offset;
            _visualBrush = new VisualBrush(AdornedElement);
            IsHitTestVisible = false;
        }
        public void UpdatePosition(Point location)
        {
            _location = location;
            InvalidateVisual();
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            Point point = _location;
            point.Offset(-_offset.X, -_offset.Y);
            drawingContext.DrawRectangle(_visualBrush, null, new Rect(point, RenderSize));
        }
    }
}
