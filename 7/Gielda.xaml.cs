using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Lecture10.WPF
{
    public partial class Gielda : Window
    {
        private DispatcherTimer _timer;
        
        
        private Dictionary<string, double> _stockPrices = new()
        {
            { "KGHM", 110.0 },
            { "PKO BP", 40.0 },
            { "LOTOS", 95.0 },
            { "ORLEN", 100.0 }
        };

        public Gielda()
        {
            InitializeComponent();
            
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1) 
            };
            _timer.Tick += OnTimerTick;
            _timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            foreach (var company in _stockPrices.Keys.ToList())
            {
                double change = ChangeGenerator.GenerateChange();
                _stockPrices[company] = Math.Round(_stockPrices[company] + change, 2);
                UpdateUI(company, _stockPrices[company], change);
            }
        }

        private void UpdateUI(string companyName, double stockPrice, double change)
        {
            TextBlock stockValueTextBlock = null;
            TextBlock stockChangeTextBlock = null;
            Border stockBorder = null;

            switch (companyName)
            {
                case "KGHM":
                    stockValueTextBlock = Stock1Value;
                    stockChangeTextBlock = Stock1Change;
                    stockBorder = Border1Border;
                    break;
                case "PKO BP":
                    stockValueTextBlock = Stock2Value;
                    stockChangeTextBlock = Stock2Change;
                    stockBorder = Border2Border;
                    break;
                case "LOTOS":
                    stockValueTextBlock = Stock3Value;
                    stockChangeTextBlock = Stock3Change;
                    stockBorder = Border3Border;
                    break;
                case "ORLEN":
                    stockValueTextBlock = Stock4Value;
                    stockChangeTextBlock = Stock4Change;
                    stockBorder = Border4Border;
                    break;
            }

            if (stockValueTextBlock != null && stockChangeTextBlock != null && stockBorder != null)
            {
                stockValueTextBlock.Text = $"{stockPrice:0.00} ZL";
                stockChangeTextBlock.Text = $"{change:+0.00;-0.00} ZL";
                
                if (change > 0)
                {
                    stockBorder.Background = new SolidColorBrush(Colors.LightGreen);
                }
                else if (change < 0)
                {
                    stockBorder.Background = new SolidColorBrush(Colors.Brown);
                }
                else
                {
                    stockBorder.Background = new SolidColorBrush(Colors.Gray);
                }
            }
        }
    }

    public static class ChangeGenerator
    {
        private static Random _random = new Random();

        public static double GenerateChange()
        {
            return Math.Round(_random.NextDouble() * 2 - 1, 2);
        }
    }
}
