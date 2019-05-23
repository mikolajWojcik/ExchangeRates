using Microcharts.Forms;
using ChartEntry = Microcharts.ChartEntry;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Microcharts;
using System.Linq;

namespace ExchangeRates.CustomControls
{
    public class MicroBarChart : ChartView
    {
        public static readonly BindableProperty ChartEntriesProperty = BindableProperty.Create(
            propertyName: nameof(ChartEntries),
            returnType: typeof(IEnumerable<ChartEntry>),
            declaringType: typeof(MicroBarChart),
            defaultValue: default(IEnumerable<ChartEntry>),
            propertyChanged: HandleChartEntriesChanged);

        public IEnumerable<ChartEntry> ChartEntries
        {
            get { return (IEnumerable<ChartEntry>)GetValue(ChartEntriesProperty); }
            set { SetValue(ChartEntriesProperty, value); }
        }

        public MicroBarChart()
        {
            this.BackgroundColor = Color.Transparent;
        }

        private static void HandleChartEntriesChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MicroBarChart)bindable;

            control.UpdateChartEntries();
        }

        private void UpdateChartEntries()
        {
            var maxChartEntry = ChartEntries.Max(x => x.Value);
            var minChartEntry = ChartEntries.Min(x => x.Value);

            Chart = new LineChart
            {
                Entries = ChartEntries,
                MaxValue = maxChartEntry + 0.005f,
                MinValue = minChartEntry - 0.01f,
                LineMode = LineMode.Spline,
                PointSize = 2,
                BackgroundColor = SkiaSharp.SKColor.Empty,
            };
        }
    }
}
