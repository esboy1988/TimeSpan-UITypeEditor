using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace TimeSpanPropGridTest
{
    /// <summary>
    /// A simple panel that presents a TimeSpan's components using .Net numeric up/down
    /// spin controls.
    /// </summary>
    public partial class TimeSpanUIEditorControl : UserControl
    {
        /// <summary>The TimeSpan value that we're editing</summary>
        public TimeSpan TimeSpan { get; private set; }


        /// <summary>True if the user canceled the edit via the escape key</summary>
        public bool DidUserCancel { get; private set; }


        private IWindowsFormsEditorService editorService;


        /// <summary>
        /// Initialise
        /// </summary>
        public TimeSpanUIEditorControl(TimeSpan timeSpan, IWindowsFormsEditorService editorService)
        {
            InitializeComponent();
            this.TimeSpan = timeSpan;
            this.editorService = editorService;

            numDays.Value = timeSpan.Days;
            numHours.Value = timeSpan.Hours;
            numMinutes.Value = timeSpan.Minutes;
            numSeconds.Value = timeSpan.Seconds;
            numMS.Value = timeSpan.Milliseconds;
        }


        private void num_ValueChanged(object sender, EventArgs e)
        {
            SetNewTimeSpanFromUI();
        }


        private void SetNewTimeSpanFromUI()
        {
            this.TimeSpan = new TimeSpan(
                days: (int)numDays.Value,
                hours: (int)numHours.Value,
                minutes: (int)numMinutes.Value,
                seconds: (int)numSeconds.Value,
                milliseconds: (int)numMS.Value);
        }


        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DidUserCancel = true;
            }

            return base.ProcessDialogKey(keyData);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if(this.Visible)
            {
                numHours.Focus();
            }
        }

        private void timerSetFocusHelper_Tick(object sender, EventArgs e)
        {
            SetFocusToFirstNonZeroValue();
            timerSetFocusHelper.Stop();
        }


        private void SetFocusToFirstNonZeroValue()
        {
            Func<int, NumericUpDown, bool, bool> setFocus = (testValue, editor, alreadyDone) =>
            {
                bool done = alreadyDone;
                if (!done && (testValue > 0))
                {
                    editor.Focus();
                    done = true;
                }

                return done;
            };


            bool hasSetFocus = false;
            hasSetFocus = setFocus(this.TimeSpan.Days, numDays, hasSetFocus);
            hasSetFocus = setFocus(this.TimeSpan.Hours, numHours, hasSetFocus);
            hasSetFocus = setFocus(this.TimeSpan.Minutes, numMinutes, hasSetFocus);
            hasSetFocus = setFocus(this.TimeSpan.Seconds, numSeconds, hasSetFocus);
            setFocus(this.TimeSpan.Milliseconds, numMS, hasSetFocus);
        }
    }
}
