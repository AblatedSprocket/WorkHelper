using CustomPresentationControls.Utilities;
using System.Collections.ObjectModel;
using System.Windows;
using WorkHelper.Models;
using WorkHelper.TaskBoard;

namespace WorkHelper.UI.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public RelayCommand<Task> DoCommand { get; }
        public MainWindow()
        {
            InitializeComponent();
            var task = new TaskBoardViewModel();
            task.Work.Add(new Work
            {
                Story = new Story
                {
                    Name = "Story Name",
                    Description = "A Brief Description"
                },
                ActiveTasks = new ObservableCollection<Task>
                {
                    new Task
                    {
                        Name ="First Task",
                        Description = "First description"
                    },
                    new Task
                    {
                        Name ="Second Task",
                        Description = "Second description"
                    }
                },
                InProgressTasks = new ObservableCollection<Task>
                {
                    new Task
                    {
                        Name="In Progress!",
                        Description = "This task is in progress."
                    }
                }
            });
            DataContext = task;
        }
    }
}
