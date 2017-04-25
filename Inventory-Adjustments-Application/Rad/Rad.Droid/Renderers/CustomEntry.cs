using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using color = Android.Graphics.Color;
using Android.Graphics;
using Rad.Droid.Renderers;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntry))]

namespace Rad.Droid.Renderers
{
    class CustomEntry : EntryRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                //GradientDrawable gd = new GradientDrawable();
                //gd.SetColor(color.White);
                //gd.SetCornerRadius(10);
                //gd.SetStroke(2, color.LightGray);
                //this.Control.SetBackgroundDrawable(gd);
                this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
            }
        }
    }
}