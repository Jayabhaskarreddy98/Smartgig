using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Web.Mvc.UI;

namespace InfionicCommonServices.Model
{
    public class ChartViewModel
    {

        public ChartProperties ChartPropertiesList
        {
            get;
            set;
        }

        public IList<ChartData> ChartDataList
        {
            get;
            set;
        }

        public ChartViewModel()
        {
            ChartPropertiesList = new ChartProperties();
            ChartDataList = new List<ChartData>();


        }
        public class ChartProperties
        {
            public int GraphId
            {
                get;
                set;
            }
            public string ChartName { get; set; }
            public bool ShowLegend { get; set; }
            public bool ShowTitle { get; set; }
            public bool ShowStack { get; set; }
            public string TitleText { get; set; }
            public ChartSeriesType SeriesType { get; set; }
            public ChartLegendPosition ChartLegendPosition { get; set; }
            public List<ChartData> ChartData { get; set; }
            public string SeriesLable1 { get; set; }
            public string SeriesLable2 { get; set; }
            public string SeriesLable3 { get; set; }
            public string SeriesLable4 { get; set; }
            public string SeriesLable5 { get; set; }
            public string SeriesLable6 { get; set; }
            public string SeriesLable7 { get; set; }
            public string SeriesLable8 { get; set; }
            public string SeriesLable9 { get; set; }
            public string SeriesLable10 { get; set; }
            public string SeriesLable11 { get; set; }
            public string SeriesLable12 { get; set; }
            public string SeriesLable13 { get; set; }
            public string SeriesLable14 { get; set; }
            public string SeriesLable15 { get; set; }
            public string SeriesLable16 { get; set; }
            public string SeriesLable17 { get; set; }
            public string SeriesLable18 { get; set; }
            public string SeriesLable19 { get; set; }
            public string SeriesLable20 { get; set; }
            public string Style { get; set; }
            public string ValueFormat { get; set; }
            public ChartPieLabelsAlign ChartPieLabelsAlign { get; set; }
            public ChartPieLabelsPosition ChartPieLabelsPosition { get; set; }
            public int StartAngle { get; set; }
            public int Padding { get; set; }
            public int XAxisLabelRotation { get; set; }
            public int YAxisLabelRotation { get; set; }
            public bool IsDateFiltersRequired { get; set; }
        }


        public class ChartData
        {
            public int? Id1;
            public int? Id2;
            public int? Id3;
            public int? Id4;
            public int? Id5;
            public int? Id6;
            public int? Id7;
            public int GraphId
            {
                get;
                set;
            }
            public string PortletTitle
            {
                get;
                set;
            }
            public string YAxisLable { get; set; }
            public string XAxisLable { get; set; }            

            public decimal? SeriesValue1 { get; set; }
            public decimal? SeriesValue2 { get; set; }
            public decimal? SeriesValue3 { get; set; }
            public decimal? SeriesValue4 { get; set; }
            public decimal? SeriesValue5 { get; set; }
            public decimal? SeriesValue6 { get; set; }
            public decimal? SeriesValue7 { get; set; }
            public decimal? SeriesValue8 { get; set; }
            public decimal? SeriesValue9 { get; set; }
            public decimal? SeriesValue10 { get; set; }
            public decimal? SeriesValue11 { get; set; }
            public decimal? SeriesValue12 { get; set; }
            public decimal? SeriesValue13 { get; set; }
            public decimal? SeriesValue14 { get; set; }
            public decimal? SeriesValue15 { get; set; }
            public decimal? SeriesValue16 { get; set; }
            public decimal? SeriesValue17 { get; set; }
            public decimal? SeriesValue18 { get; set; }
            public decimal? SeriesValue19 { get; set; }
            public decimal? SeriesValue20 { get; set; }
            public string SeriesValueString1 { get; set; }
        }

        public enum ChartSeriesType
        {
            bar, column, line, area, verticalLine, verticalArea, pie
        }


    }

}
