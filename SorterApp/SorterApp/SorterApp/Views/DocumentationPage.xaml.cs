﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SorterApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DocumentationPage : ContentPage
	{
		public DocumentationPage ()
		{
			InitializeComponent ();
            var browser = new WebView
            {
                Source = "https://github.com/diegobaqt/clothing_image_sorter/blob/master/README.md"
            };

            Content = browser;
        }
	}
}