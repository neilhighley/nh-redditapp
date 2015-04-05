using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using RedditApp1.DataModel;
using Studiomecha.Hotpage.Reddit;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HotpageReddit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RedditContent : Page
    {
        private RedditDataItem rdi;
        private string REDDIT_ROOT="https://reddit.com";

        public RedditContent()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var dc = (e.Parameter as FrameworkElement).DataContext as RedditDataItem;
            if (dc != null)
            {
                ShowPage(dc);
            }
        }

        //To see this code in action, add a call to RegisterForShare to your constructor or other
        //initializing function.
        private void RegisterForShare()
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += (sender, args) => ShareLinkHandler(sender,args);
        }

        private void ShowPage(RedditDataItem rda)
        {
            btnReddit.Visibility = Visibility.Visible;
            btnStory.Visibility=Visibility.Collapsed;

            RegisterForShare();
            rdi = rda;
            var url = new Uri(rdi.Url);
            wvReddit.Navigate(url);
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                return;
            }

            if (frame.CanGoBack)
            {
                frame.GoBack();
                e.Handled = true;
            }
        }


        private void RedditButton_Click(object sender, RoutedEventArgs e)
        {
            btnReddit.Visibility = Visibility.Collapsed;
            btnStory.Visibility = Visibility.Visible;
            
            var url = new Uri(REDDIT_ROOT + rdi.Permalink);
            wvReddit.Navigate(url);

        }
        private void StoryButton_Click(object sender, RoutedEventArgs e)
        {
            btnReddit.Visibility = Visibility.Visible;
            btnStory.Visibility = Visibility.Collapsed;

            var url = new Uri(rdi.Url);
            wvReddit.Navigate(url);

        }

        private void ShareButton_click(object sender, RoutedEventArgs e)
        {
            //share with phone api
           
            DataTransferManager.ShowShareUI();

        }
        private void ShareLinkHandler(DataTransferManager sender, DataRequestedEventArgs e)
        {
            DataRequest request = e.Request;

            // The Title is mandatory
            request.Data.Properties.Title = rdi.Title;
            

            // Now add the data you want to share.
            request.Data.SetWebLink(new Uri(rdi.Url));
        }

        private void BtnBack_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HubPage));
        }
    }
}
