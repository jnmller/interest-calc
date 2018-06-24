using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Zinsrechner {
  /// <summary>
  /// Interaktionslogik für MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    public MainWindow() {
      InitializeComponent();
    }

    private void btnBerechnen_Click(object sender, RoutedEventArgs e) {
      double
        dDarlehen = 0,
        dZinsen = 0,
        dZinssatz = 0,
        dTilgung = 0,
        dRestschuld = 0,
        dMonatsrate = 0;
      int
        iMonat = 0,
        iJahr = 0,
        iJahrRest = 0;
      string
        sFortschritt = "";

      try {
        dDarlehen = Convert.ToDouble(tbxDarlehen.Text);
      } catch { }
      try {
        dZinssatz = Convert.ToDouble(tbxZinssatz.Text);
      } catch { }
      try {
        dTilgung = Convert.ToDouble(tbxTilgungsrate.Text);
      } catch { }
      dRestschuld = dDarlehen;

      ///adfhgfgh 
      if(( dDarlehen > 0 ) && ( dZinssatz > 0 ) && ( dTilgung > 0 )) {
        //Monatsrate berechnen
        dMonatsrate = Math.Round(dRestschuld * ( dZinssatz + dTilgung ) / 100 / 12, 2);
        tbxMonatsrate.Text = dMonatsrate.ToString("0.00");
        //Daten neu berechnen
        List<Darlehensmonat> lstDarlehen = new List<Darlehensmonat>();
        while(dRestschuld > 0) {
          iMonat++;
          iJahr = iMonat / 12;
          iJahrRest = iMonat % 12;
          sFortschritt = iJahr.ToString() + " Jahr(e), " + iJahrRest.ToString() + " Monat(e)";
          dZinsen = Math.Round(dRestschuld * dZinssatz / 100 / 12, 2);
          dTilgung = dMonatsrate - dZinsen;
          dRestschuld -= dTilgung;
          //dgvDaten.Items.Add(new Darlehensmonat(iMonat.ToString(), dZinsen.ToString("0.00"), dTilgung.ToString("0.00"), dRestschuld.ToString("0.00")));
          lstDarlehen.Add(new Darlehensmonat(sFortschritt, dZinsen.ToString("0.00"), dTilgung.ToString("0.00"), dRestschuld.ToString("0.00")));
        }
        //DAten zuweisen
        dgvDaten.ItemsSource = lstDarlehen;

      }
    }
  }

  public class Darlehensmonat {

    public Darlehensmonat(string monat, string zins, string tilgung, string restschuld) {
      Monat = monat;
      Zins = zins;
      Tilgung = tilgung;
      Restschuld = restschuld;
    }

    public string Monat { get; set; }
    public string Zins { get; set; }
    public string Tilgung { get; set; }
    public string Restschuld { get; set; }
  }

}
