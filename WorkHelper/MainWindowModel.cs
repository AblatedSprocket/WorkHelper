using CustomPresentationControls.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHelper.Models;
using WorkHelper.TaskBoard;

namespace WorkHelper
{
    class MainWindowModel : ViewModel
    {
        public TaskBoardViewModel TaskBoardViewModel { get; }
        public MainWindowModel()
        {
            TaskBoardViewModel = new TaskBoardViewModel();
            TaskBoardViewModel.Work.Add(new Work());
        }
    }
}
