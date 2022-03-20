using AVosCocktails.Model;
using AVosCocktails.ViewModel;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class AddPage : ContentPage
    {
        ListViewModel listViewModel = ListViewModel.Instance;
        public AddPage()
        {
            InitializeComponent();

            //On initialise notre picker
            var photoMode = new List<string>();
            photoMode.Add("From URL");
            photoMode.Add("From Galery");

            picker.ItemsSource = photoMode;
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text)
                    && !string.IsNullOrWhiteSpace(instructionsEntry.Text)
                    && !string.IsNullOrWhiteSpace(ingredientsEntry.Text)
                    && !string.IsNullOrWhiteSpace(imageEntry.Text)
                    && !string.IsNullOrWhiteSpace(tagsEntry.Text))
            {
                //On rajoute le nouveau cocktail dans la base de donnée
                BDCocktail NouveauBDCocktail = new BDCocktail()
                {
                    Name = nameEntry.Text, 
                    Instructions = instructionsEntry.Text,
                    Ingredients = ingredientsEntry.Text,
                    Image = imageEntry.Text,
                    Tags = tagsEntry.Text
                };
                await App.Database.SaveCocktailAsync(NouveauBDCocktail);

                //Comme les cocktails de ma base de données doivent être différents
                //des cocktails que j'affiche, je le convertie ici
                LocalCocktail NouveauLocalCocktail = new LocalCocktail()
                {
                    Id = NouveauBDCocktail.Id,
                    Name = NouveauBDCocktail.Name,
                    Instructions = NouveauBDCocktail.Instructions,
                    Ingredients = NouveauBDCocktail.Ingredients.Split(','),
                    Image = NouveauBDCocktail.Image,
                    Tags = NouveauBDCocktail.Tags.Split(',')
                };
                //On l'ajoute à notre observable ListeDeCocktail pour que la liste (l'observable) se mette à jour
                listViewModel.ListeDeCocktail.Insert(0, NouveauLocalCocktail);

                await DisplayAlert("Success !", "Your cocktail was added succesfully, you can now see it on the list", "Ok");

                //On reset tous les champs de texte
                nameEntry.Text = "";
                instructionsEntry.Text = "";
                ingredientsEntry.Text = "";
                imageEntry.Text = "";
                tagsEntry.Text = "";
            } else
            {
                await DisplayAlert("Couldn't add", "Fill all entries if you want to add a cocktail", "Ok");
                return;
            }   
        }

        //Fonction permettant de choisir une image de la galerie
        async void SelectImage(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if(!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Not supported", "Your device does not support this functionnality", "Ok");
                return;
            }

         
            var mediaOptions = new PickMediaOptions(){PhotoSize = PhotoSize.Medium};

            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);
            
            if (selectedImage == null)
            {
                await DisplayAlert("Error", "Could not get the image, please try again.", "Ok");
                return;
            }

            string selectedImagePath = selectedImageFile.Path;
            imageEntry.Text = selectedImagePath;
        }

        //Fonction permettant d'actualiser la vue en fonction de ce qu'on choisi dans le picker
        private void picker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (picker.SelectedItem == "From URL") {
                imageEntry.IsVisible = true;
                imageEntryGalery.IsVisible = false;
            } else
            {
                imageEntry.IsVisible = false;
                imageEntryGalery.IsVisible = true;
            }
        }

        //Permet d'actualiser l'image affiché lorsque l'on rentre une URL
        private void imageEntry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            selectedImage.Source = imageEntry.Text;
        }
    }
}