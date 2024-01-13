namespace TripExpenseManager;

public partial class App : Application
{
    private readonly SeedDataService _seedDataService;

    public App(SeedDataService seedDataService, AppViewModel viewModel)
	{
		InitializeComponent();

		MainPage = new MainPage(viewModel);
        _seedDataService = seedDataService;
    }

    protected override async void OnStart()
    {
        base.OnStart();
        await _seedDataService.SeedDataAsync();
    }
}
