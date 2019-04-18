using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace iTunesArtworkFinder.Classes
{
    public class TextBoxBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            this.AssociatedObject.PreviewMouseLeftButtonDown += AssociatedObjectMouseLeftButtonDown;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.PreviewMouseLeftButtonDown -= AssociatedObjectMouseLeftButtonDown;
            base.OnDetaching();
        }

        private void AssociatedObjectMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 3)
            {
                this.AssociatedObject.SelectAll();
            }
        }
    }
}