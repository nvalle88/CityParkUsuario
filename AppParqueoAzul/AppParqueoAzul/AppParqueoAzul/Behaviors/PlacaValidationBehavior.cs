using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppParqueoAzul.Behaviors
{
    public class PlacaValidationBehavior : Behavior<Entry>
    {
        //•	Se debe dar formato al ingreso de la placa: AAA – 1234

        const string placaRegex = @"[a-zA-Z]{3,3}-[0-9]{3,4}";
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += TextChanged;
            base.OnAttachedTo(entry);
        }
        bool isactive = true;
        // Valida si el texto introducido es una placa
        void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((Entry)sender).Text.Length > 8)
            {
                ((Entry)sender).Text = e.NewTextValue.Remove(e.NewTextValue.Length - 1);
                return;
            }

            if (((Entry)sender).Text.Length == 3 && isactive)
            {
                ((Entry)sender).Text = ((Entry)sender).Text + "-";
                isactive = false;
            }
            if (((Entry)sender).Text.Length < 3)
            {
                isactive = true;
            }


            bool valido = (Regex.IsMatch(e.NewTextValue, placaRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            ((Entry)sender).TextColor = valido ? Color.Green : Color.Red;

        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= TextChanged;
            base.OnDetachingFrom(entry);
        }
    }
}
