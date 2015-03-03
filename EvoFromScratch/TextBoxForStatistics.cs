using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EvoFromScratch
{
    public class FormatTextBox
    {
        MainForm Form;
        public FormatTextBox(MainForm _Form)
        {
            Form = _Form;
        }

        public void format (TextBox Text, TextBox Value, string Name)
        {
            Text.Location = new System.Drawing.Point(Form.Width - 120, Form.StatisticLocY);
            Text.Name = "ColoniCount";
            Text.ShortcutsEnabled = true;
            Text.ReadOnly = true;
            Text.BorderStyle = BorderStyle.None;
            Text.Size = new System.Drawing.Size(70, 10);
            Text.Text = Name;
            Text.Cursor = System.Windows.Forms.Cursors.No;


            Value.Location = new System.Drawing.Point(Form.Width - 50, Form.StatisticLocY);
            Value.Name = "ColoniCount:";
            Value.ShortcutsEnabled = true;
            Value.ReadOnly = true;
            Value.TextAlign = HorizontalAlignment.Left;
            Value.BorderStyle = BorderStyle.None;
            Value.Size = new System.Drawing.Size(45, 5);

            Form.Controls.Add(Text);
            Form.Controls.Add(Value);
            Text.BringToFront();
            Value.BringToFront();

            Form.StatisticLocY += 15;
        }
    }
}
