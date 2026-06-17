using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PresenterBETA
{
    public partial class MainWindow : Window
    {
        private int _viewCounter = 0;
        private Border _selectedThumbnail = null;

        // Variables for dragging
        private bool _isDragging = false;
        private Point _clickPosition;
        private FrameworkElement _draggedElement = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnCreateViewClick(object sender, RoutedEventArgs e)
        {
            _viewCounter++;

            // Create Thumbnail
            Border thumb = new Border
            {
                Width = 120,
                Height = 80,
                Background = Brushes.White,
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(10),
                BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(1),
                Tag = new List<UIElement>(), // This Tag stores the boxes for this slide
                Uid = "Slide"
            };

            thumb.Child = new TextBlock
            {
                Text = $"View {_viewCounter}",
                Foreground = Brushes.Black,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            thumb.MouseLeftButtonDown += (s, a) => SelectThumbnail(thumb);
            ViewContainer.Children.Add(thumb);
            SelectThumbnail(thumb);
        }

        private void SelectThumbnail(Border thumb)
        {
            if (_selectedThumbnail != null) _selectedThumbnail.BorderBrush = Brushes.Gray;
            _selectedThumbnail = thumb;
            _selectedThumbnail.BorderBrush = Brushes.DeepSkyBlue;

            // Clear Canvas and Load this slide's boxes
            EditorCanvas.Children.Clear();
            List<UIElement> boxes = (List<UIElement>)_selectedThumbnail.Tag;
            foreach (var box in boxes) EditorCanvas.Children.Add(box);
        }

        private void OnAddBoxClick(object sender, RoutedEventArgs e)
        {
            if (_selectedThumbnail == null) return;

            // 1. The Main Container (The Box)
            Border boxContainer = new Border
            {
                Width = 250,
                Height = 100,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.DeepSkyBlue,
                BorderThickness = new Thickness(1),
                Padding = new Thickness(2)
            };

            // 2. The Internal Layout
            Grid boxGrid = new Grid();
            boxGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) }); // Drag Bar
            boxGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }); // Content
            boxContainer.Child = boxGrid;

            // 3. The Drag Handle (Like the dotted line in PPT)
            Border dragHandle = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(240, 240, 240)),
                Opacity = 0.5,
                Cursor = Cursors.SizeAll,
                CornerRadius = new CornerRadius(2, 2, 0, 0)
            };
            Grid.SetRow(dragHandle, 0);
            boxGrid.Children.Add(dragHandle);

            // 4. The TextBox
            TextBox contentInput = new TextBox
            {
                Text = "Click to add text",
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(0),
                FontSize = 24,
                FontWeight = FontWeight.FromOpenTypeWeight(500),
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            Grid.SetRow(contentInput, 1);
            boxGrid.Children.Add(contentInput);

            // 5. Drag Events (Attached to Handle)
            dragHandle.MouseLeftButtonDown += (s, ev) => {
                _isDragging = true;
                _draggedElement = boxContainer;
                _clickPosition = ev.GetPosition(boxContainer);
                _draggedElement.CaptureMouse();
                ev.Handled = true; // Prevents the TextBox from taking focus immediately
            };
            dragHandle.MouseMove += Box_MouseMove;
            dragHandle.MouseLeftButtonUp += Box_MouseUp;

            // Set Initial Position
            Canvas.SetLeft(boxContainer, 100);
            Canvas.SetTop(boxContainer, 100);

            EditorCanvas.Children.Add(boxContainer);
            ((List<UIElement>)_selectedThumbnail.Tag).Add(boxContainer);
        }

        // --- DRAG LOGIC ---
        private void Box_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isDragging = true;
            _draggedElement = sender as FrameworkElement;
            _clickPosition = e.GetPosition(_draggedElement);
            _draggedElement.CaptureMouse();
        }

        private void Box_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && _draggedElement != null)
            {
                Point currentPos = e.GetPosition(EditorCanvas);

                double x = currentPos.X - _clickPosition.X;
                double y = currentPos.Y - _clickPosition.Y;

                Canvas.SetLeft(_draggedElement, x);
                Canvas.SetTop(_draggedElement, y);
            }
        }

        private void Box_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
            if (_draggedElement != null)
            {
                _draggedElement.ReleaseMouseCapture();
                _draggedElement = null;
            }
        }
    }
}