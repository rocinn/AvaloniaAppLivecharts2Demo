using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using ReactiveUI;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace AvaloniaAppLivecharts2Demo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ISeries[] _Series;
        public ISeries[] Series { get => _Series; set => this.RaiseAndSetIfChanged(ref _Series, value); }

        public Axis[] XAxes { get; set; } =
        {
            new DateTimeAxis(TimeSpan.FromSeconds(3), date => date.ToString("HH:mm:ss"))
        };

        private List<ObservableCollection<DateTimePoint>> _ListData = new List<ObservableCollection<DateTimePoint>>();

        public MainWindowViewModel()
        {
            Series = new ISeries[] { };

            for (int i = 0; i < 3; i++)
            {
                Random r = new Random();
                ObservableCollection<DateTimePoint> list = new ObservableCollection<DateTimePoint>();
                for (int j = 1; j < 101; j++)
                {
                    list.Add(new DateTimePoint(DateTime.Now.AddSeconds(j), j * r.NextDouble()));
                }

                _ListData.Add(list);
            }
        }

        ISeries InitLine(ObservableCollection<DateTimePoint> DateTimePoints, int index)
        {
            var l = new LineSeries<DateTimePoint>
            {
                Values = DateTimePoints,
                LineSmoothness = 0,
                //GeometryStroke = null,
                GeometryFill = null,
                Fill = null,
                XToolTipLabelFormatter = (point) => point.Model.DateTime.ToString("HH:mm:ss"),
            };
            l.Tag = index;

            return l;
        }

        public void AddSeries1()
        {
            AddOrRemove(1);
        }

        public void AddSeries2()
        {
            AddOrRemove(2);
        }

        public void AddSeries3()
        {
            AddOrRemove(3);
        }

        void AddOrRemove(int index)
        {
            ISeries f = null;
            List<ISeries> ll = new List<ISeries>();
            foreach (var item in Series)
            {
                if (item.Tag != null && item.Tag.ToString() == index.ToString())
                {
                    f = item;
                }
                else
                {
                    ll.Add(item);
                }
            }

            if (f == null)
            {
                // add series
                var l = Series.ToList();
                l.Add(InitLine(_ListData[index - 1], index));
                Series = l.ToArray();
            }
            else
            {
                // remove series
                Series = ll.ToArray();
            }
        }
    }
}
