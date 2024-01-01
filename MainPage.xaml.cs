namespace DiceRoller
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        DiceRoller Roller;

        public MainPage()
        {
            InitializeComponent();

            Roller = new DiceRoller(1, 6);
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
