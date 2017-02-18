using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSpanPropGridTest
{
    /// <summary>
    /// Example for testing the TimeSpan property grid editing
    /// </summary>
    class Data
    {
        [TypeConverter(typeof(TimeSpanStringConverter))]
        [Editor(typeof(TimeSpanUIEditor), typeof(UITypeEditor))]
        [DefaultValue(typeof(TimeSpan), "1.02:03:04.005")]
        [DisplayName("Custom 1")]
        public TimeSpan A { get; set; } = new TimeSpan(1, 2, 3, 4, 5);


        [TypeConverter(typeof(TimeSpanStringConverter))]
        [Editor(typeof(TimeSpanUIEditor), typeof(UITypeEditor))]
        [DefaultValue(typeof(TimeSpan), "0.01:02:03.004")]
        [DisplayName("Custom 2")]
        public TimeSpan B { get; set; } = new TimeSpan(0, 1, 2, 3, 4);


        [TypeConverter(typeof(TimeSpanStringConverter))]
        [Editor(typeof(TimeSpanUIEditor), typeof(UITypeEditor))]
        [DefaultValue(typeof(TimeSpan), "0.00:01:02.003")]
        [DisplayName("Custom 3")]
        public TimeSpan C { get; set; } = new TimeSpan(0, 0, 1, 2, 3);


        [TypeConverter(typeof(TimeSpanStringConverter))]
        [Editor(typeof(TimeSpanUIEditor), typeof(UITypeEditor))]
        [DefaultValue(typeof(TimeSpan), "0.00:00:01.002")]
        [DisplayName("Custom 4")]
        public TimeSpan D { get; set; } = new TimeSpan(0, 0, 0, 1, 2);


        [TypeConverter(typeof(TimeSpanStringConverter))]
        [Editor(typeof(TimeSpanUIEditor), typeof(UITypeEditor))]
        [DefaultValue(typeof(TimeSpan), "0.00:00:00.001")]
        [DisplayName("Custom 5")]
        public TimeSpan E { get; set; } = new TimeSpan(0, 0, 0, 0, 1);


        [TypeConverter(typeof(TimeSpanStringConverter))]
        [Editor(typeof(TimeSpanUIEditor), typeof(UITypeEditor))]
        [DefaultValue(typeof(TimeSpan), "0.00:00:00.000")]
        [DisplayName("Custom 6")]
        public TimeSpan F { get; set; } = new TimeSpan(0, 0, 0, 0, 0);


        [DefaultValue(typeof(TimeSpan), "1.02:03:04.005")]
        [DisplayName("Native")]
        public TimeSpan Z { get; set; } = new TimeSpan(1, 2, 3, 4, 5);
    }
}
