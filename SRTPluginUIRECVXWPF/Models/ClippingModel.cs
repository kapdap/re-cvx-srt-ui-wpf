using SRTPluginProviderRECVX.Models;
using System;

namespace SRTPluginUIRECVXWPF.Models
{
    public class ClippingModel : BaseNotifyModel
    {
        private int _x;
        public int X
        {
            get => _x;
            set => SetField(ref _x, value, "X", "Clipping");
        }

        private int _y;
        public int Y
        {
            get => _y;
            set => SetField(ref _y, value, "Y", "Clipping");
        }

        private int _width;
        public int Width
        {
            get => _width;
            set => SetField(ref _width, value, "Width", "Clipping");
        }

        private int _height;
        public int Height
        {
            get => _height;
            set => SetField(ref _height, value, "Height", "Clipping");
        }

        public string Clipping =>
            String.Format("{0},{1},{2},{3}", X, Y, Width, Height);

        public int ImageWidth { get; private set; } = 48;
        public int ImageHeight { get; private set; } = 48;

        public ClippingModel() { }

        public ClippingModel(int imageWidth, int imageHeight)
        {
            ImageWidth = imageWidth;
            ImageHeight = imageHeight;
        }

        public void Update(int[] clip)
        {
            X = clip[0];
            Y = clip[1];
            Width = clip[2];
            Height = clip[3];
        }

        public void Update(int column, int row, int columns = 1, int rows = 1) =>
            Update(column, row, ImageWidth, ImageHeight, columns, rows);

        public void Update(int column, int row, int width, int height, int columns = 1, int rows = 1)
        {
            X = width * column;
            Y = height * row;
            Width = width * columns;
            Height = height * rows;
        }
    }
}
