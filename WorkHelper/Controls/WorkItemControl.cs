using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WorkHelper.Models;

namespace WorkHelper.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WorkHelper.Controls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WorkHelper.Controls;assembly=WorkHelper.Controls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:Task/>
    ///
    /// </summary>
    public class WorkItemControl : Control
    {
        //private Hyperlink _headerLink;
        //private RoutedEventHandler initialEvent;

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(WorkItemControl), new PropertyMetadata(null));
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(WorkItemControl), new PropertyMetadata(null));
        public static readonly DependencyProperty AccentProperty = DependencyProperty.Register("Accent", typeof(Brush), typeof(WorkItemControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(string), typeof(WorkItemControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsParentMeasure));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(WorkItemControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsParentMeasure));

        //public event RoutedEventHandler Click
        //{
        //    add
        //    {
        //        initialEvent = value;
        //        if (_headerLink != null && value != null)
        //        {
        //            _headerLink.Click += value;
        //        }
        //    }
        //    remove
        //    {
        //        initialEvent = null;
        //        if (_headerLink != null && value != null)
        //        {
        //            _headerLink.Click -= value;
        //        }
        //    }
        //}
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public Brush Accent
        {
            get { return (Brush)GetValue(AccentProperty); }
            set { SetValue(AccentProperty, value); }
        }
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        static WorkItemControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WorkItemControl), new FrameworkPropertyMetadata(typeof(WorkItemControl)));
        }
        public override void OnApplyTemplate()
        {
            object headerLink = GetTemplateChild("HeaderLink");
            if (headerLink is Hyperlink link)
            {
                link.Click += OnClick;
            }
            base.OnApplyTemplate();
        }
        private void OnClick(object sender, RoutedEventArgs e)
        {
            if (Command != null && Command.CanExecute(CommandParameter))
            {
                Command.Execute(CommandParameter);
            }
        }
    }
}
