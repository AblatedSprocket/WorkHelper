using CustomPresentationControls.Utilities;
using WorkHelper.TaskBoard;

namespace WorkHelper
{
    class MainWindowModel : ViewModel
    {
        public TaskBoardViewModel TaskBoardViewModel { get; }
        public MainWindowModel()
        {
            TaskBoardViewModel = new TaskBoardViewModel();
        }
    }
}
