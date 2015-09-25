using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro;
using System.ComponentModel;


namespace HearthstoneCardStats
{
    /// <summary>
    /// Interaction logic for CardStatsWindow.xaml
    /// </summary>
    public partial class CardStatsWindow : MetroWindow
    {
        bool _appIsClosing = false;
        public CardStatsWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_appIsClosing)
                return;
            e.Cancel = true;
            Hide();
        }

        internal void Shutdown()
        {
            _appIsClosing = true;
            Close();
        }

        public async void LoadCards()
        {
            DataGridCardStats.Items.Clear();
            DataGridCardStats.Items.Refresh();
            TextBlockLoading.Visibility = Visibility.Visible;

            Dictionary<String, CardStats> cardStats =  await Task.Run(() => ReadReplay.LoadFile());
            if (cardStats != null)
            {
                foreach (KeyValuePair<String, CardStats> stat in cardStats)
                {
                    DataGridCardStats.Items.Add(stat.Value);
                }
            }
            TextBlockLoading.Visibility = Visibility.Hidden;
        }

        private void MetroWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void DGridOverall_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void DGridOverall_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
