using CustomPresentationControls.Utilities;
using System.Collections.ObjectModel;
using WorkHelper.Controls;
using WorkHelper.Models;

namespace WorkHelper.TaskBoard
{
    class TaskBoardViewModel : ViewModel
    {
        #region Fields
        private ObservableCollection<Work> _work;
        private ObservableCollection<WorkItemControl> _activeTasks;
        private ObservableCollection<WorkItemControl> _inProgressTasks;
        private ObservableCollection<WorkItemControl> _completedTasks;
        #endregion
        #region Properties
        public string ModelText { get; set; }
        public ObservableCollection<Work> Work
        {
            get { return _work; }
            set { OnPropertyChanged(ref _work, value); }
        }
        ObservableCollection<WorkItemControl> ActiveTasks
        {
            get { return _activeTasks; }
            set { OnPropertyChanged(ref _activeTasks, value); }
        }
        ObservableCollection<WorkItemControl> InProgressTasks
        {
            get { return _inProgressTasks; }
            set { OnPropertyChanged(ref _inProgressTasks, value); }
        }
        ObservableCollection<WorkItemControl> CompletedTasks
        {
            get { return _completedTasks; }
            set { OnPropertyChanged(ref _completedTasks, value); }
        }
        #endregion
        #region Commands
        public RelayCommand<ObservableCollection<WorkItemControl>> AddTaskCommand { get; }
        #endregion
        public TaskBoardViewModel()
        {
            AddTaskCommand = new RelayCommand<ObservableCollection<WorkItemControl>>(OnAddTask);
        }
        #region Command Methods
        private void OnAddStory()
        {
            Work.Add(new Work());
            ModelText = "Task Board";
        }
        private void OnAddTask(ObservableCollection<WorkItemControl> taskCollection)
        {
            taskCollection.Add(new WorkItemControl());
        }
        #endregion
    }
}
