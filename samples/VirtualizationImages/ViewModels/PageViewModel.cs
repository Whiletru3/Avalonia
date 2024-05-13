using System;
using System.IO;
using MiniMvvm;
#nullable disable
namespace VirtualizationImages.ViewModels;

public class PageViewModel : ViewModelBase
{
    private string _prefix;
    private int _index;
    private double _height = double.NaN;
    private double _width = double.NaN;
    private int _imageIndex;

    public PageViewModel(int index, string prefix = "Item")
    {
        _prefix = prefix;
        _index = index;
        _imageIndex = (int)Random.Shared.NextInt64(1, 4);
        Height = 150;
        Width = 150;
    }

    public string Header => $"{_prefix} {_index}";

    public double Height
    {
        get => _height;
        set => this.RaiseAndSetIfChanged(ref _height, value);
    }
    public double Width
    {
        get => _width;
        set => this.RaiseAndSetIfChanged(ref _width, value);
    }

    public byte[] _displayImage;

    public byte[] DisplayImage
    {
        get
        {
            if (_displayImage == null)
            {
               
                string path = $"image{_imageIndex}.png";
                _displayImage =  File.ReadAllBytes(path);
            }

            return _displayImage;
        }
        set
        {
            _displayImage = value;
        }
    }
}
