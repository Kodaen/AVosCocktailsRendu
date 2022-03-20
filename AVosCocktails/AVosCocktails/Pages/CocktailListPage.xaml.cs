using AVosCocktails.Model;
using AVosCocktails.ViewModel;
using CocktailDB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


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
        //protected virtual void OnAppearing ()
        //{
        //    base.OnAppearing();
        //    foreach (var i in CocktailAPI.SearchByLetter('a'))
        //    {
        //        Debug.WriteLine(i.strDrink + " ;");
        //    }
        //}

    }
}