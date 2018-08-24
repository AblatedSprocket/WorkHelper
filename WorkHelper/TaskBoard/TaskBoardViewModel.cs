using CustomPresentationControls.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WorkHelper.Controls;
using WorkHelper.Models;

namespace WorkHelper.TaskBoard
{
    public interface ITaskBoard
    {
        void MoveWorkItem(object item, string sourceCollection, string targetCollection, string sourceStory, string targetStory, int insertIndex);
    }
    public class TaskBoardViewModel : ViewModel, ITaskBoard
    {
        #region Fields
        private ObservableCollection<Work> _work = new ObservableCollection<Work>();
        //private ObservableCollection<WorkItemControl> _activeTasks = new ObservableCollection<WorkItemControl>();
        //private ObservableCollection<WorkItemControl> _inProgressTasks = new ObservableCollection<WorkItemControl>();
        //private ObservableCollection<WorkItemControl> _completedTasks = new ObservableCollection<WorkItemControl>();
        #endregion
        #region Properties
        public string ModelText { get; set; }
        public ObservableCollection<Work> Work
        {
            get { return _work; }
            set { OnPropertyChanged(ref _work, value); }
        }
        #endregion
        #region Commands
        public RelayCommand<ObservableCollection<WorkItemControl>> AddTaskCommand { get; }
        public RelayCommand<Task> OpenTaskCommand { get; }
        #endregion
        public TaskBoardViewModel()
        {
            AddTaskCommand = new RelayCommand<ObservableCollection<WorkItemControl>>(OnAddTask);
            OpenTaskCommand = new RelayCommand<Task>(OnOpenTask);
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
        private void OnOpenTask(Task task)
        {

        }
        #endregion
        #region ITaskBoard Methods
        public void MoveWorkItem(object item, string sourceCollection, string targetCollection, string sourceStory, string targetStory, int insertIndex)
        {
            if (item is Task task && Work.FirstOrDefault(w => w.Story.Name == sourceStory) != null && Work.FirstOrDefault(w => w.Story.Name == targetStory) != null)
            {
                Work sourceWork = Work.FirstOrDefault(w => w.Story.Name == sourceStory);
                Work targetWork = Work.FirstOrDefault(w => w.Story.Name == targetStory);
                if (sourceWork != null && targetWork != null && Enum.TryParse(sourceCollection, out Status sourceStatus) && Enum.TryParse(targetCollection, out Status targetStatus))
                {
                    switch (sourceStatus)
                    {
                        case Status.Active:
                            sourceWork.ActiveTasks.Remove(task);
                            break;
                        case Status.InProgress:
                            sourceWork.InProgressTasks.Remove(task);
                            break;
                        case Status.Complete:
                            sourceWork.CompletedTasks.Remove(task);
                            break;
                    }
                    switch (targetStatus)
                    {
                        case Status.Active:
                            if (insertIndex < targetWork.ActiveTasks.Count) targetWork.ActiveTasks.Insert(insertIndex, task);
                            else targetWork.ActiveTasks.Add(task);
                            break;
                        case Status.InProgress:
                            if (insertIndex < targetWork.InProgressTasks.Count) targetWork.ActiveTasks.Insert(insertIndex, task);
                            else targetWork.InProgressTasks.Add(task);
                            break;
                        case Status.Complete:
                            if (insertIndex < targetWork.CompletedTasks.Count) targetWork.ActiveTasks.Insert(insertIndex, task);
                            else targetWork.CompletedTasks.Add(task);
                            break;
                    }
                    task.Status = targetStatus;
                }
            }
        }
        #endregion
    }
}
