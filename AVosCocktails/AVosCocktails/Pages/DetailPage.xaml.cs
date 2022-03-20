using AVosCocktails.Model;
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
    public partial class DetailPage : ContentPage
    {
        public DetailPage(LocalCocktail Cocktail )
        {
            InitializeComponent();
            BindingContext = Cocktail;

            for (int i = 0; i < Cocktail.Tags.Length; i++)
                {
                Frame frametemp = new Frame { 
                    CornerRadius = 2,
                    Padding = new Thickness(8, 0, 8, 0),
                    Margin = new Thickness(20, 0, 20, 0)};
                Label labeltemp = new Label { 
                    Text = Cocktail.Tags[i],
                    BackgroundColor = Color.White,
                    TextColor = Color.Black,
                    FontSize = 15,
                    VerticalTextAlignment=TextAlignment.Center,
                    FontAttributes= FontAttributes.Bold | FontAttributes.Italic,
                    TextTransform = TextTransform.Uppercase };
                frametemp.Content = labeltemp;
                FlexTags.Children.Add(frametemp);
                }
           
            for (int i = 0; i < Cocktail.Ingredients.Length && Cocktail.Ingredients[i]!= ""; i++)
            {
                GridIngredients.Children.Add(new Label
                {
                    Text = Cocktail.Ingredients[i],
                    TextColor = Color.White,
                    FontSize = 15, //le texte est si petit à cause des problèmes de retour à la ligne
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                }, i%2, i/2); //Pour que dans la grid on est 3 colonnes maximum
            }

        }
    }
}