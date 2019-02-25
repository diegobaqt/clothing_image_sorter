using Plugin.Media;
using Plugin.Media.Abstractions;
using SorterApp.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SorterApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoadImagePage : ContentPage
	{
        private readonly PredictService _predictService = new PredictService();
        private string _path = "";
        private string _fileName = "";

		public LoadImagePage ()
		{
			InitializeComponent ();
            DisabledAuxButtons();
            Loading.IsRunning = false;
            LayoutClass.IsVisible = false;

            Reset.Clicked += (sender, args) =>
            {
                Image.Source = "";
                _path = "";
                _fileName = "";
                ImageName.Text = "";
                LayoutClass.IsVisible = false;
                DisabledAuxButtons();
            };

            PickPhoto.Clicked += async (sender, args) =>
            {
                Loading.IsRunning = true;
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                    return;
                }
                var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,
                });
                if (file == null) return;
                _path = file.Path;
                var split = _path.Split('/');
                _fileName = split[split.Length - 1];
                ImageName.Text = _fileName;

                var imageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });

                Image.Source = imageSource;
                EnabledButtons();

                Loading.IsRunning = false;
            };

            TakePhoto.Clicked += async (sender, args) =>
            {
                Loading.IsRunning = true;
                DisabledButtons();

                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "SorterAppImages",
                    Name = "Sorter_" + DateTime.Now.Ticks + ".jpg",
                    CompressionQuality = 92,
                    PhotoSize = PhotoSize.Custom,
                    CustomPhotoSize = 90
                });

                if (file == null) return;
                _path = file.Path;
                var split = _path.Split('/');
                _fileName = split[split.Length - 1];
                ImageName.Text = _fileName;

                var imageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });

                Image.Source = imageSource;
                EnabledButtons();

                Loading.IsRunning = false;
            };

            Send.Clicked += async (sender, args) =>
            {
                Loading.IsRunning = true;
                DisabledButtons();
                var result = await _predictService.PredictClassImage(_path);
                if (result == "Error")
                {
                    await DisplayAlert("Error :(", "A connection error has occurred", "OK");
                }
                else
                {
                    LabelClass.Text = "Class: " + result;
                }

                EnabledButtons();
                LayoutClass.IsVisible = true;
                Loading.IsRunning = false;
            };
        }

        private void DisabledButtons()
        {
            Reset.IsEnabled = false;
            Send.IsEnabled = false;
            PickPhoto.IsEnabled = false;
            TakePhoto.IsEnabled = false;
        }

        private void EnabledButtons()
        {
            Reset.IsEnabled = true;
            Send.IsEnabled = true;
            PickPhoto.IsEnabled = true;
            TakePhoto.IsEnabled = true;
        }

        private void DisabledAuxButtons()
        {
            Reset.IsEnabled = false;
            Send.IsEnabled = false;
        }
    }
}