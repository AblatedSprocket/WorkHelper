using CustomPresentationControls.Utilities;
using System;

namespace WorkHelper.Models
{
    public enum Status
    {
        Active = 0,
        InProgress = 1,
        Complete = 2
    }
    public abstract class Item : ObservableObject
    {
        private int _id;
        private string _name;
        private string _description;
        private Status _status;
        private string _hoursEstimated;
        private double _hoursRequired;
        private DateTime _startDate;
        private DateTime _completedDate;
        public int Id
        {
            get { return _id; }
            set { OnPropertyChanged(ref _id, value); }
        }
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
        public Status Status
        {
            get { return _status; }
            set { OnPropertyChanged(ref _status, value); }
        }
        public string HoursEstimated
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
