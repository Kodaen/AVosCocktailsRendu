using AVosCocktails.Model;
using AVosCocktails.ViewModel;
using CocktailDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//Page contenant la liste des cocktails
namespace AVosCocktails.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class CocktailListPage : ContentPage
    {
        ListViewModel listViewModel = ListViewModel.Instance;
        public CocktailListPage()
        {
            InitializeComponent();
            BindingContext = listViewModel;
        }

        //Fonction permettant de créer une nouvelle page de détail correspondant au cocktail
        //choisi.
        async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cocktail = e.CurrentSelection.FirstOrDefault() as LocalCocktail;
            if (cocktail == null)
            {
                return;
            } 
              

            await Navigation.PushAsync(new DetailPage(cocktail));

            ((CollectionView)sender).SelectedItem = null;
     
        }

        private void nameEntry_Completed(object sender, EventArgs e)
        {
            //Si le champ de recherche n'est pas vide
            if (nameEntry.Text.Count() !=0)
            {
                //On crée un nouveau ListViewModel notre liste de recherche
                ListViewModel SearchList = new ListViewModel();
                //On le vide
                SearchList.ListeDeCocktail.Clear();
                foreach (LocalCocktail Cocktail in listViewModel.ListeDeCocktail)
                {
                    //Et pour tous les cocktails, si le nom contient un string de ce qu'on recherche,
                    //alors on l'ajoute à la liste de recherche
                    if (Cocktail.Name.Contains(nameEntry.Text))
                    {
                        SearchList.ListeDeCocktail.Add(Cocktail);
                    }
                }
                //Et on bind cette nouvelle list
                BindingContext = SearchList;
            }
            //Sinon on revient sur notre list initiale
            else
            {
                BindingContext = this.listViewModel;
            }
        }
    }
}