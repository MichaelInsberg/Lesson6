using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Tools;

namespace Lesson6.MainApplication;

public class MainViewModel : INotifyPropertyChanged
{
    private ImageSource? _imageSource;
    private bool first;
    private int _loadingProgress;
    public ICommand LoadImage { get;  }


    public ImageSource?  ImageSource
    {
        get => _imageSource ;
        set
        {
            _imageSource = value;
            OnPropertyChanged();
        }
    }


    public int LoadingProgress
    {
        get => _loadingProgress;
        set
        {
            _loadingProgress = value;
            OnPropertyChanged();
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    public MainViewModel()
    {
        LoadImage = new RelayCommand(LoadImageFile);
        ImageSource = null;
        first = true;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void LoadImageFile(object obj)
    {
        var bitmapImage = new BitmapImage();
        bitmapImage.BeginInit();
        if (first)
        {
            bitmapImage.UriSource = new Uri("https://upload.wikimedia.org/wikipedia/commons/e/e4/World_map_2004_CIA_large.jpg");
            first = false;
        }
        else
        {
            
//            bitmapImage.UriSource = new Uri("https://miro.medium.com/max/1400/1*lzGIoyEjl3utHCDGRP3sig.png");
            
            bitmapImage.UriSource = new Uri("https://upload.wikimedia.org/wikipedia/commons/4/4e/Pleiades_large.jpg");
            //bitmapImage.UriSource = new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/3/3d/LARGE_elevation.jpg/2560px-LARGE_elevation.jpg");
            first = true;
        }
        
        bitmapImage.DownloadProgress += BitmapImageOnDownloadProgress;
        bitmapImage.EndInit();
        ImageSource = bitmapImage;
    }

    private void BitmapImageOnDownloadProgress(object? sender, DownloadProgressEventArgs eventArgs)
    {
        LoadingProgress = eventArgs.Progress;
        Trace.WriteLine($"Progress is {Thread.CurrentThread.Name} {eventArgs.Progress}");
    }
}