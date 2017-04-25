using System;
using System.Collections.Generic;
using System.Text;
using Rad.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntry))]
namespace Rad.iOS.Renderers
{
    class CustomEntry : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            var textField = (UITextField)Control;
            textField.AutocorrectionType = UITextAutocorrectionType.No;
            textField.SpellCheckingType = UITextSpellCheckingType.No;
            textField.AutocapitalizationType = UITextAutocapitalizationType.None;
        }
    }
}
