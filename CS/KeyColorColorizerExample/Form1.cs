using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;
using DevExpress.XtraCharts;

namespace KeyColorColorizerExample {
    public partial class Form1 : Form {
        const string filepath = "..//..//Data//GDP.xml";

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            #region #BarSeries
            // Create and customize a bar series.
            Series barSeries = new Series() {
                DataSource = LoadData(filepath),
                Colorizer = CreateColorizer(),
                ArgumentDataMember = "Country",
                ColorDataMember = "Region",
                View = new SideBySideBarSeriesView()
            };
            barSeries.ValueDataMembers.AddRange(new string[] { "Product" });
            #endregion #BarSeries

            // Add the series to the ChartControl's Series collection.
            chartControl1.Series.Add(barSeries);

            // Show a title for the values axis.
            ((XYDiagram)chartControl1.Diagram).AxisY.Title.Text = "GDP per capita, $";
            ((XYDiagram)chartControl1.Diagram).AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        }

        #region #KeyColorColorier
        // Creates the Key-Color colorizer for the bar chart.
        ChartColorizerBase CreateColorizer() {
            KeyColorColorizer colorizer = new KeyColorColorizer() {
                PaletteName = "Apex"
            };
            colorizer.Keys.Add("Africa");
            colorizer.Keys.Add("America");
            colorizer.Keys.Add("Asia");
            colorizer.Keys.Add("Australia");
            colorizer.Keys.Add("Europe");

            return colorizer;
        }
        #endregion #KeyColorColorier

        #region #DataLoad
        class HpiPoint {
            public string Country { get; set; }
            public double Product { get; set; }
            public string Region { get; set; }
        }

        // Loads data from an XML data source.
        static List<HpiPoint> LoadData(string filepath) {
            XDocument doc = XDocument.Load(filepath);
            List<HpiPoint> points = new List<HpiPoint>();
            foreach (XElement element in doc.Element("G20HPIs").Elements("CountryStatistics")) {
                points.Add(new HpiPoint() {
                    Country = element.Element("Country").Value,
                    Product = Convert.ToDouble(element.Element("Product").Value),
                    Region = element.Element("Region").Value
                });
            }
            return points;
        }
        #endregion #DataLoad
    }
}
