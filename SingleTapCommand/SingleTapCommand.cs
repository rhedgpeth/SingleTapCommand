using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SingleTapCommand
{
	public sealed class SingleTapCommand<T> : SingleTapCommand
	{
		public SingleTapCommand(Func<T, Task> execute, int delay = 0) : base(o => execute((T)o), delay)
		{
			if (execute == null)
				throw new ArgumentNullException(nameof(execute));
		}
	}

	public class SingleTapCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		int _delay; // In milliseconds
		bool _isBusy;
		readonly Func<object, Task> _execute;

		public SingleTapCommand(Func<object, Task> execute, int delay = 0)
		{
			if (execute == null)
				throw new ArgumentNullException(nameof(execute));

			_delay = delay;
			_execute = execute;
		}

		public SingleTapCommand(Func<Task> execute, int delay = 0) : this(o => execute(), delay)
		{
			if (execute == null)
				throw new ArgumentNullException(nameof(execute));
		}

		public bool CanExecute(object paramter)
		{
			return !_isBusy;
		}

		public async void Execute(object parameter)
		{
			// Added another check because Buttons (for some reason) don't fire CanExecute before Execute
			if (!_isBusy)
			{
				_isBusy = true;

				await _execute.Invoke(parameter);

				if (_delay > 0)
				{
					// Delay to throttle the fastest of tapping
					await Task.Delay(_delay);
				}

				_isBusy = false;
			}
		}

		public void ChangeCanExecute()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
