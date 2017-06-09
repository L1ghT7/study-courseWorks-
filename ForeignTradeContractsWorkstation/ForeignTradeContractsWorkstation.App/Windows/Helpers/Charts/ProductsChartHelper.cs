using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using ForeignTradeContractsWorkstation.App.Database;
using ForeignTradeContractsWorkstation.App.Database.Contexts.Directory;
using ForeignTradeContractsWorkstation.App.Database.Contexts.Directory.Entities;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Wpf.PieChart;

namespace ForeignTradeContractsWorkstation.App.Windows.Helpers.Charts
{
    public static class ProductsChartHelper
    {
        private static readonly int TOP_COUNT = 5;
        private static readonly int FONT_SIZE_DEFAULT = 12;

        public static void DrawProductsChart(PieChartExample control, DateTime? from, DateTime? to,int count)
        {
            var d = control.ToolTip;

            from = from ?? DateTime.Now;
            to = to ?? DateTime.Now; 

            var seriesCollection = new SeriesCollection();
            SetDefaultSettings(control);
            var preparedData = GetPreparedData(from.Value, to.Value,count).ToList();

            Func<double, int, int> getFontForSeriesSise = (pieSeriesWidth, labelLength) =>
            {
                if (!double.IsNaN(pieSeriesWidth))
                {
                    return ((int)pieSeriesWidth/labelLength)/5;
                }
                return FONT_SIZE_DEFAULT;
            };
            Func<ChartPoint, string> labelPoint = chartPoint =>
            {
                var renderLabel = $"{chartPoint.Y} рублей";
                foreach (var series in  chartPoint.ChartView.Series)
                {
                    var currentPie = series as PieSeries;
                    if (currentPie != null)
                    {
                       
                    }
                }
                return renderLabel;
            };

            seriesCollection.AddRange(preparedData.Select(x =>
            {
                var piesSeries = new PieSeries()
                {
                    Title = x.Name,
                    Values = new ChartValues<long>() { (long)x.TotalSum },
                    LabelPoint = labelPoint
                    
                };
                
                piesSeries.DataLabels = true;
               
                return piesSeries;
            }));
          
            control.DrawSeries(seriesCollection);
        }

        private static void SetDefaultSettings(PieChartExample control)
        {
            control.PieChartTest.ChartLegend.FontSize = 20;
        }

        private static IEnumerable<dynamic> GetPreparedData(DateTime from, DateTime to,int count)
        {
            Func<OrdersGoods, bool> dateMapper = (x) =>
            {
                return x.Order.date_of_order > from && x.Order.date_of_order < to;
            };

            Func<IGrouping<long, OrdersGoods>, dynamic> mapper = (x) =>
            {
                var list = x.ToList();
                return new
                {
                    TotalSum = list.Sum(y => y.Goods.accounting_price),
                    Name = list.First().Goods.full_name
                };
            };

            return DirectoryContextHelper.GetAllEntities<OrdersGoods>("Order", "Goods")
               .Where(dateMapper)
               .GroupBy(x => x.GoodsId)
               .OrderByDescending(x => x.ToList().Count)
               .Take(count)
               .Select(mapper);
        }
    }
}
