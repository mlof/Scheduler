namespace Scheduler.ViewModels
{
    public class ShellWindowDesignModel : ShellWindowViewModel
    {
        public static ShellWindowDesignModel Instance => new ShellWindowDesignModel();

        /// <inheritdoc />
        public ShellWindowDesignModel() : base(null)
        {
        }
    }
}