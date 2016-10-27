using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SingleTapCommand
{
	public class HomeViewModel
	{
		Action ProceedToNextView { get; set; }
		Func<Task> ProceedToNextViewAsync { get; set; }

		/// <summary>
		/// This will not prevent multiple execution with tap spamming.
		/// </summary>
		public ICommand NotSingleTapLimitingCommand
		{
			get
			{
				return new Command(ProceedToNextView);
			}
		}

		/// <summary>
		/// This is limit to a single execution when tapping several times for TapGestures, but NOT Xamarin.Forms Buttons.
		/// </summary>
		public ICommand SingleTapLimitingCommand1
		{
			get
			{
				return new Command(async () =>
				{
					await ProceedToNextViewAsync();
				});
			}
		}

		/// <summary>
		/// This is limit to a single execution when tapping several times for TapGestures, but NOT Xamarin.Forms Buttons.
		/// </summary>
		public ICommand SingleTapLimitingCommand2
		{
			get
			{
				return new SingleTapCommand(ProceedToNextViewAsync);
			}
		}

		/// <summary>
		/// This is limit to a single execution when tapping several times for TapGestures and Buttons.
		/// </summary>
		public ICommand SingleTapLimitingCommand3
		{
			get
			{
				return new SingleTapCommand(ProceedToNextViewAsync, 1000);
			}
		}

		public HomeViewModel(Action ProceedToNextView, Func<Task> ProceedToNextViewAsync)
		{
			this.ProceedToNextView = ProceedToNextView;
			this.ProceedToNextViewAsync = ProceedToNextViewAsync;
		}
	}
}
