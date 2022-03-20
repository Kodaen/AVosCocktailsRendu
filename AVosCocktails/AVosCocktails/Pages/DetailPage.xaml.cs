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

//Page contenant les détails d'un cocktail
namespace AVosCocktails.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        public DetailPage(LocalCocktail Cocktail )
        {
            InitializeComponent();
            BindingContext = Cocktail;

            //Pour afficher des tags avec un fond blanc,
            //pour chaque tag Cocktail.Tags[i], on crée
            //une frame qui contient un label avec le tag
            //en "text", et on met ce tag dans une flexview.
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
           
            //A peu près le même principe pour les ingrédients où on met chacun des strings
            //des ingrédients dans un Label, qu'on ajoute à une Grid de deux colonnes.
            for (int i = 0; i < Cocktail.Ingredients.Length && Cocktail.Ingredients[i]!= ""; i++)
            {
                GridIngredients.Children.Add(new Label
                {
                    Text = Cocktail.Ingredients[i],
                    TextColor = Color.White,
                    FontSize = 15,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                }, i%2, i/2); //Pour que dans la grid on est 2 colonnes maximum
            }

        }
    }
}