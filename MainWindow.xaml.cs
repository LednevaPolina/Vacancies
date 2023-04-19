using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
       
        
        public MainWindow()
        {
            InitializeComponent();
               
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                Vacancy vacancy = new Vacancy();
                string page = "";
                int counter;
                using HttpResponseMessage responsePage = await client.GetAsync("https://proglib.io/vacancies/all");
                string body = await responsePage.Content.ReadAsStringAsync();
                string patternPage = "data-total=\"(.+)\"";
                Regex regexPage = new Regex(patternPage);
                MatchCollection matchesPage = regexPage.Matches(body);
                foreach (Match match in matchesPage)
                {
                    page = match.Groups[1].Value;
                }
                counter = Convert.ToInt32(page);



                for (int i = 1; i <= counter; i++)
                {

                    label.Content = i;
                    string path = "https://proglib.io/vacancies/all?workType=all&workPlace=all&experience=&salaryFrom=&page=" + i;
                    Uri uri = new Uri(path);
                    using HttpResponseMessage response = await client.GetAsync(uri);
                    var htmlbody = await response.Content.ReadAsStringAsync();
                    string patternNameVacancy = "itemprop=\"title\">(.+)<\\/h2>";
                    string patternHyperLink = "<a href=\"(.+)\" class=\"no-link\"";
                    string patternPlacementDate = "data-slug=\"(.+)\"";
                    Regex regex = new Regex(patternNameVacancy);
                    MatchCollection matchesNameVacancy = regex.Matches(htmlbody);
                    regex = new Regex(patternHyperLink);
                    MatchCollection matchesHyperlink = regex.Matches(htmlbody);
                    regex = new Regex(patternPlacementDate);
                    MatchCollection matchesDate = regex.Matches(htmlbody);
                   
                    for (int j = 0; j < matchesNameVacancy.Count; j++)
                    {
                         
                        vacancy.Vacancies.Add(new Vacancy
                        {
                            Name = matchesNameVacancy[j].Groups[1].Value,
                            Hyperlink = matchesHyperlink[j].Groups[1].Value,
                            PlacementData = matchesDate[j].Groups[1].Value.Substring(matchesDate[j].Groups[1].Value.ToString().Length-10)
                        });
                    }


                    grid.ItemsSource = vacancy.Vacancies;
                     
                    
                }
            }


        }

    }
        //data-slug="(.+)"
        //<a href="(.+)" class="no-link">

}
