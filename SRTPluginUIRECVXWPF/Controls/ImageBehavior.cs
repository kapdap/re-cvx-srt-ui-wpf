using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace SRTPluginUIRECVXWPF.Controls
{
    // https://thomaslevesque.com/2011/03/27/wpf-display-an-animated-gif-image/
    public static class ImageBehavior
    {
        #region AnimatedSource
        [AttachedPropertyBrowsableForType(typeof(Image))]
        public static ImageSource GetAnimatedSource(Image obj) =>
            (ImageSource)obj.GetValue(AnimatedSourceProperty);

        public static void SetAnimatedSource(Image obj, ImageSource value) =>
            obj.SetValue(AnimatedSourceProperty, value);

        public static readonly DependencyProperty AnimatedSourceProperty =
            DependencyProperty.RegisterAttached(
                "AnimatedSource",
                typeof(ImageSource),
                typeof(ImageBehavior),
                new UIPropertyMetadata(
                null,
                AnimatedSourceChanged));

        private static void AnimatedSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (!(o is Image imageControl))
                return;

            if (e.OldValue is ImageSource oldValue)
                imageControl.BeginAnimation(Image.SourceProperty, null);

            if (e.NewValue is ImageSource newValue)
                imageControl.DoWhenLoaded(InitAnimationOrImage);
        }

        private static void InitAnimationOrImage(Image imageControl)
        {
            BitmapSource source = GetAnimatedSource(imageControl) as BitmapSource;
            if (source != null)
            {
                if (GetDecoder(source) is GifBitmapDecoder decoder && decoder.Frames.Count > 1)
                {
                    ObjectAnimationUsingKeyFrames animation = new ObjectAnimationUsingKeyFrames();
                    TimeSpan totalDuration = TimeSpan.Zero;
                    BitmapSource prevFrame = null;
                    FrameInfo prevInfo = null;
                    foreach (BitmapFrame rawFrame in decoder.Frames)
                    {
                        FrameInfo info = GetFrameInfo(rawFrame);
                        BitmapSource frame = MakeFrame(
                            source,
                            rawFrame, info,
                            prevFrame, prevInfo);

                        DiscreteObjectKeyFrame keyFrame = new DiscreteObjectKeyFrame(frame, totalDuration);
                        animation.KeyFrames.Add(keyFrame);

                        totalDuration += info.Delay;
                        prevFrame = frame;
                        prevInfo = info;
                    }
                    animation.Duration = totalDuration;
                    animation.RepeatBehavior = RepeatBehavior.Forever;
                    if (animation.KeyFrames.Count > 0)
                        imageControl.Source = (ImageSource)animation.KeyFrames[0].Value;
                    else
                        imageControl.Source = decoder.Frames[0];
                    imageControl.BeginAnimation(Image.SourceProperty, animation);
                    return;
                }
            }
            imageControl.Source = source;
            return;
        }

        private static BitmapDecoder GetDecoder(BitmapSource image)
        {
            BitmapDecoder decoder = null;
            if (image is BitmapFrame frame)
                decoder = frame.Decoder;

            if (decoder == null)
            {
                if (image is BitmapImage bmp)
                {
                    if (bmp.StreamSource != null)
                    {
                        bmp.StreamSource.Position = 0;
                        decoder = BitmapDecoder.Create(bmp.StreamSource, bmp.CreateOptions, bmp.CacheOption);
                    }
                    else if (bmp.UriSource != null)
                    {
                        Uri uri = bmp.UriSource;
                        if (bmp.BaseUri != null && !uri.IsAbsoluteUri)
                            uri = new Uri(bmp.BaseUri, uri);
                        decoder = BitmapDecoder.Create(uri, bmp.CreateOptions, bmp.CacheOption);
                    }
                }
            }

            return decoder;
        }

        private static BitmapSource MakeFrame(
            BitmapSource fullImage,
            BitmapSource rawFrame, FrameInfo frameInfo,
            BitmapSource previousFrame, FrameInfo previousFrameInfo)
        {
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext context = visual.RenderOpen())
            {
                if (previousFrameInfo != null && previousFrame != null &&
                    previousFrameInfo.DisposalMethod == FrameDisposalMethod.Combine)
                {
                    Rect fullRect = new Rect(0, 0, fullImage.PixelWidth, fullImage.PixelHeight);
                    context.DrawImage(previousFrame, fullRect);
                }

                context.DrawImage(rawFrame, frameInfo.Rect);
            }
            RenderTargetBitmap bitmap = new RenderTargetBitmap(
                fullImage.PixelWidth, fullImage.PixelHeight,
                fullImage.DpiX, fullImage.DpiY,
                PixelFormats.Pbgra32);
            bitmap.Render(visual);
            return bitmap;
        }

        private class FrameInfo
        {
            public TimeSpan Delay { get; set; }
            public FrameDisposalMethod DisposalMethod { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }
            public double Left { get; set; }
            public double Top { get; set; }

            public Rect Rect
            {
                get => new Rect(Left, Top, Width, Height);
            }
        }

        private enum FrameDisposalMethod
        {
            Replace = 0,
            Combine = 1,
            RestoreBackground = 2,
            RestorePrevious = 3
        }

        private static FrameInfo GetFrameInfo(BitmapFrame frame)
        {
            FrameInfo frameInfo = new FrameInfo
            {
                Delay = TimeSpan.FromMilliseconds(100),
                DisposalMethod = FrameDisposalMethod.Replace,
                Width = frame.PixelWidth,
                Height = frame.PixelHeight,
                Left = 0,
                Top = 0
            };

            BitmapMetadata metadata;
            try
            {
                metadata = frame.Metadata as BitmapMetadata;
                if (metadata != null)
                {
                    const string delayQuery = "/grctlext/Delay";
                    const string disposalQuery = "/grctlext/Disposal";
                    const string widthQuery = "/imgdesc/Width";
                    const string heightQuery = "/imgdesc/Height";
                    const string leftQuery = "/imgdesc/Left";
                    const string topQuery = "/imgdesc/Top";

                    ushort? delay = metadata.GetQueryOrNull<ushort>(delayQuery);
                    if (delay.HasValue)
                        frameInfo.Delay = TimeSpan.FromMilliseconds(10 * delay.Value);

                    ushort? disposal = metadata.GetQueryOrNull<byte>(disposalQuery);
                    if (disposal.HasValue)
                        frameInfo.DisposalMethod = (FrameDisposalMethod)disposal.Value;

                    ushort? width = metadata.GetQueryOrNull<ushort>(widthQuery);
                    if (width.HasValue)
                        frameInfo.Width = width.Value;

                    ushort? height = metadata.GetQueryOrNull<ushort>(heightQuery);
                    if (height.HasValue)
                        frameInfo.Height = height.Value;

                    ushort? left = metadata.GetQueryOrNull<ushort>(leftQuery);
                    if (left.HasValue)
                        frameInfo.Left = left.Value;

                    ushort? top = metadata.GetQueryOrNull<ushort>(topQuery);
                    if (top.HasValue)
                        frameInfo.Top = top.Value;
                }
            }
            catch (NotSupportedException) { }

            return frameInfo;
        }

        private static T? GetQueryOrNull<T>(this BitmapMetadata metadata, string query)
            where T : struct
        {
            if (metadata.ContainsQuery(query))
            {
                object value = metadata.GetQuery(query);
                if (value != null)
                    return (T)value;
            }
            return null;
        }
        #endregion
    }

    public static class ImageBehaviorExtentions
    {
        // https://thomaslevesque.com/2011/03/27/wpf-display-an-animated-gif-image/
        public static void DoWhenLoaded<T>(this T element, Action<T> action)
            where T : FrameworkElement
        {
            if (element.IsLoaded)
                action(element);
            else
            {
                void handler(object sender, RoutedEventArgs e)
                {
                    element.Loaded -= handler;
                    action(element);
                }

                element.Loaded += handler;
            }
        }
    }
}
