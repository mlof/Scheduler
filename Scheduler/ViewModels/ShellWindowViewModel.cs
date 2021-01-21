using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Chroniton;

namespace Scheduler.ViewModels
{
    public class ShellWindowViewModel : INotifyPropertyChanged
    {
        private readonly ISingularity _singularity;

        public ShellWindowViewModel(ISingularity singularity)
        {
            _singularity = singularity;
            this.JobCollection = new ObservableCollection<Job>();
            this.Load();
        }

        public ObservableCollection<Job> JobCollection { get; set; }

        private async Task Load()
        {
            JobCollection = new ObservableCollection<Job>();
        }

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;
    }
}