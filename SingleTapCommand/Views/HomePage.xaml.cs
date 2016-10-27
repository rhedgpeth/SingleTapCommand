using System.Threading.Tasks;
using Xamarin.Forms;

namespace SingleTapCommand
{
	public partial class HomePage : ContentPage
	{
		public HomePage()
		{
			InitializeComponent();
			BindingContext = new HomeViewModel(NavigateToNextPage, NavigatToNextPageAsync);
		}

		async void NavigateToNextPage()
		{
			// Navigation.PushAsync(new NextPage()); /* I've also seen this quite a bit */

			// This will be kicked off, but not awaited before completing the containing method.
			await Navigation.PushAsync(new NextPage());
		}

		Task NavigatToNextPageAsync()
		{
			return Navigation.PushAsync(new NextPage());
		}
	}
}
