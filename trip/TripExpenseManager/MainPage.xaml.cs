namespace TripExpenseManager;

public partial class MainPage : ContentPage
{
	public MainPage(AppViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
