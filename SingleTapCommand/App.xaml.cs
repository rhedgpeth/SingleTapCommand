using Xamarin.Forms;

namespace SingleTapCommand
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			MainPage = new NavigationPage(new HomePage());
		}
	}
}
