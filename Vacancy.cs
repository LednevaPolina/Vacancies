using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Vacancy
    {
        static int id = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hyperlink { get; set; }
        public string PlacementData{ get; set; }

        public Vacancy()
        {
            Id = id++;
            
        }

        public ObservableCollection<Vacancy>Vacancies { get; set; }=new ObservableCollection<Vacancy>();
    }
}
