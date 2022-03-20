using AVosCocktails.Model;
using AVosCocktails.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AVosCocktails.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MysteryPage : ContentPage
    {
        ListViewModel listViewModel = ListViewModel.Instance;
        public MysteryPage()
        {
            InitializeComponent();
            //List<LocalCocktail> ListeDeCocktailRecherche = new List<LocalCocktail>();
            //foreach (LocalCocktail Cocktail in listViewModel.ListeDeCocktail)
            //{
            //    if (Cocktail.Name.Contains(NameEntry.Text)) {
            //        ListeDeCocktailRecherche.Add(Cocktail);
            //    }
            //}
        }

    }
}