using System;
using System.Windows.Controls;

using Hearthstone_Deck_Tracker.Plugins;

namespace HearthstoneCardStats
{
    public class HearthstoneCardStatsPlugin : IPlugin
    {
        protected MenuItem MainMenuItem { get; set; }
        protected CardStatsWindow cardStatsWindow = new CardStatsWindow();

		public string Author
		{
			get { return "Kaustubh Garimella"; }
		}

		public string ButtonText
		{
			get { return "Hearthstone Card Statistics"; }
		}

		public string Description
		{
			get { return "Plugin that shows individual card statistics"; }
		}

		public MenuItem MenuItem
		{
            get {
                return MainMenuItem;
            }
		}

		public string Name
		{
			get { return "HearthstoneCardStats"; }
		}

		public void OnButtonPress()
		{
		}

		public void OnLoad()
		{
            MainMenuItem = new MenuItem()
            {
                Header = "Card Statistics"
            };


            MainMenuItem.Click += (sender, args) =>
            {
                cardStatsWindow.Show();
                cardStatsWindow.LoadCards();
            };
		}

		public void OnUnload()
		{
            cardStatsWindow.Shutdown();
		}

		public void OnUpdate()
		{
		}

		public Version Version
		{
			get { return new Version(0, 0, 1); }
		}
    }
}
