using System;
using System.ComponentModel;
using Mine.ViewModels;
using Xamarin.Forms;

namespace Mine.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    /// <summary>
    /// About Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        /// <summary>
        /// Constructor for About Page
        /// </summary>
        public AboutPage()
        {
            InitializeComponent();

            CurrentDateTime.Text = System.DateTime.Now.ToString("MM/dd/yy hh:mm:ss");
        }

        // 
        void DataSource_Toggled(object sender, AddingNewEventArgs e)
        {
            if (DataSourceValue.IsToggled == true)
            {
                ItemIndexViewModel.Instance.SetDataSource(1);
            }
            else
            {
                ItemIndexViewModel.Instance.SetDataSource(0);
            }
        }

        // Wipes the data in the current data source
        async void WipeDataList_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Delete Data", "Are you sure you want to delete all data?", "Yes", "No");
            if (answer)
            {
                MessagingCenter.Send(this, "WipeDataList", true);
            }
        }
    }
}