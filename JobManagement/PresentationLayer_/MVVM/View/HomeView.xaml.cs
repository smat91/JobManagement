using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Windows.Controls;

namespace PresentationLayer.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für HomeView.xaml
    /// </summary>
    
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            AddDataGridColumn();
            AddDataGridRow();
            

        }

        public static int Quarter(DateTime date)
        {
            return (date.Month + 2) / 3;
        }
        private void AddDataGridColumn()
        {
            DataGridTextColumn textColumn = new DataGridTextColumn();
            textColumn.Header = "Kategorie";
            //textColumn.Binding = new Binding("FirstName");
            DataGridJahresvergleich.Columns.Add(textColumn);

            //Aktuelles Datum holen und retour rechnen, um alle Quartale zu erhalten von den letzten 3 Jahren (dynamisch)
            for(int i = 0; i < 12; i++)
            {
                int quartal = Quarter(DateTime.Today.AddMonths(-i*3));
                DataGridTextColumn textColumn2 = new DataGridTextColumn();
                textColumn2.Header = DateTime.Today.AddMonths(-i * 3).Year + " Q" + quartal;
                //textColumn.Binding = new Binding("FirstName");
                DataGridJahresvergleich.Columns.Add(textColumn2);


                //int[] numbers = { 4, 5, 6, 7, 8, 9, 10 };
                //foreach(int c in numbers)
                //{
                //    DataGridRow row = new DataGridRow();
                //}
            }
            
            
        }

        private void AddDataGridRow()
        {
            int[] values = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            var row = new ExpandoObject() as IDictionary<String, Object>;

            for (int i = 0; i < DataGridJahresvergleich.Columns.Count; i++)
                row.Add(DataGridJahresvergleich.Columns[i].Header.ToString(), values[i]);

            DataGridJahresvergleich.Items.Add(row);

        }

    }

}
