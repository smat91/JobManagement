using System;
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
            }
            
            
        }

    }

}
