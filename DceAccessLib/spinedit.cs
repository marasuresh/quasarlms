
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace DCEAccessLib
{

   public class SpinEdit : System.Windows.Forms.NumericUpDown
   {

      protected override void OnTextChanged(EventArgs e)
      {
         base.OnTextChanged (e);
         this.ValidateEditText();
      }

      public SpinEdit()
      {
      }

   }
}