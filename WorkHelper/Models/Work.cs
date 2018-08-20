using CustomPresentationControls.Utilities;
using System.Collections.ObjectModel;

namespace WorkHelper.Models
{
    class Work : ObservableObject
    {
        private Story _story = new Story();
        private ObservableCollection<Task> _activeTasks = new ObservableCollection<Task>();
        private ObservableCollection<Task> _inProgressTasks = new ObservableCollection<Task>();
        private ObservableCollection<Task> _completedTasks = new ObservableCollection<Task>();
        public Story Story
        {
            get { return _story; }
            set { OnPropertyChanged(ref _story, value); }
        }
        ObservableCollection<Task> ActiveTasks
        {
            get { return _activeTasks; }
            set { OnPropertyChanged(ref _activeTasks, value); }
        }
        ObservableCollection<Task> InProgressTasks
        {
            get { return _inProgressTasks; }
            set { OnPropertyChanged(ref _inProgressTasks, value); }
        }
        ObservableCollection<Task> CompletedTasks
        {
            get { return _completedTasks; }
            set { OnPropertyChanged(ref _completedTasks, value); }
        }
    }
}
