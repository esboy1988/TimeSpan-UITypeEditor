using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace TimeSpanPropGridTest
{
    /// <summary>
    /// Utility to allow a <see cref="TimeSpan"/> to become editable in a property grid via
    /// a dropdown panel.
    /// </summary>
    public class TimeSpanUIEditor : UITypeEditor
    {
        /// <summary>
        /// Tell .Net we can present an editor, our <see cref="TimeSpanUIEditorControl"/>, via a dropdown action.
        /// </summary>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }


        /// <summary>
        /// Edit the TimeSpan via a dropdown instance of our <see cref="TimeSpanUIEditorControl"/>.
        /// </summary>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService = null;
            editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (editorService != null)
            {
                var timeSpan = (TimeSpan)value;
                var editor = new TimeSpanUIEditorControl(timeSpan, editorService);
                editorService.DropDownControl(editor);

                if (editor.DidUserCancel == false)
                {
                    value = editor.TimeSpan;
                }
            }

            return value;
        }
    }
}
