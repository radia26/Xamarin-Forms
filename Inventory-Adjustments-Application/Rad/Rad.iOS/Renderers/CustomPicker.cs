using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using Rad.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Picker), typeof(CustomPicker))]

namespace Rad.iOS.Renderers
{
    class CustomPicker : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null && Element != null)
            {
                var picker = this.Element;
                this.Control.TextAlignment = UITextAlignment.Center;
                this.Control.BorderStyle = UITextBorderStyle.RoundedRect;
            }
        }

      
    }

}