using CustomPresentationControls.Utilities;
using System;

namespace WorkHelper.Models
{
    public abstract class Item : ObservableObject
    {
        private string _name;
        private string _description;
        private double _hoursEstimated;
        private double _hoursRequired;
        private DateTime _startDate;
        private DateTime _completedDate;
        public string Name
        {
            get { return _name; }
            set { OnPropertyChanged(ref _name, value); }
        }
        public string Description
        {
            get { return _description; }
            set { OnPropertyChanged(ref _description, value); }
        }
        public double HoursEstimated
        {
            get { return _hoursEstimated; }
            set { OnPropertyChanged(ref _hoursEstimated, value); }
        }
        public double HoursRequired
        {
            get { return _hoursRequired; }
            set { OnPropertyChanged(ref _hoursRequired, value); }
        }
        public DateTime StartDate
        {
            get { return _startDate; }
            set { OnPropertyChanged(ref _startDate, value); }
        }
        public DateTime CompletionDate
        {
            get { return _completedDate; }
            set { OnPropertyChanged(ref _completedDate, value); }
        }
    }
}
