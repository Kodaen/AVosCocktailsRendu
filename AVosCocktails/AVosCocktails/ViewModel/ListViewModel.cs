using AVosCocktails.Model;
using CocktailDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVosCocktails.ViewModel
{
    class ListViewModel : BaseViewModel
    {
        private static ListViewModel _instance = new ListViewModel();

        public static ListViewModel Instance
        {
            get { return _instance; }
        }

        public ObservableCollection<LocalCocktail> ListeDeCocktail
        {
            get => GetValue<ObservableCollection<LocalCocktail>>();
            set => SetValue(value);
        }


        public ListViewModel()
        {
            ListeDeCocktail = new ObservableCollection<LocalCocktail>();
            InitList();
        }


        public async void InitList()
        {

            //Supression de la base de donnée si besoin.
            //await App.Database.DeleteAllCocktails();

            if (App.Database.GetCocktailAsync().Result.Count == 0)
            {
                //Etant donné que notre API ne nous permet pas de récupérer l'entièreté de ses cocktails d'un coup
                //nous somme obligé de faire plusieurs requêtes.
                Cocktail[] requestedCocktail = CocktailAPI.SearchByLetter('a').ToArray()
                    .Concat(CocktailAPI.SearchByLetter('b').ToArray()).ToArray()
                    .Concat(CocktailAPI.SearchByLetter('c').ToArray()).ToArray()
                    .Concat(CocktailAPI.SearchByLetter('d').ToArray()).ToArray()
                    .Concat(CocktailAPI.SearchByLetter('e').ToArray()).ToArray()
                    .Concat(CocktailAPI.SearchByLetter('f').ToArray()).ToArray()
                    .Concat(CocktailAPI.SearchByLetter('g').ToArray()).ToArray()
                    .Concat(CocktailAPI.SearchByLetter('h').ToArray()).ToArray()
                    .Concat(CocktailAPI.SearchByLetter('i').ToArray()).ToArray()
                    .Concat(CocktailAPI.SearchByLetter('j').ToArray()).ToArray()
                    .Concat(CocktailAPI.SearchByLetter('k').ToArray()).ToArray()
                    .Concat(CocktailAPI.SearchByLetter('m').ToArray()).ToArray();


                BDCocktail MyCocktail = new BDCocktail();
                
                //Ajout d'un cocktail, on regarde si l'API nous fournis tous les 
                //attributs qu'il nous faut, autrement on passe au prochain.
                //La variable max nous permet d'en avoir bien 50 à la fin.
                int max = 49;
                for (int i = 0; i < requestedCocktail.Length && i < max; i++)
                {

                    if (requestedCocktail[i].strDrink != null)
                    {
                        MyCocktail.Name = requestedCocktail[i].strDrink;
                    }
                    else { max ++;  continue; }

                    if (requestedCocktail[i].strInstructions != null)
                    {
                        MyCocktail.Instructions = requestedCocktail[i].strInstructions;
                    }
                    else { max++; continue; }

                    if (requestedCocktail[i].strDrinkThumb != null)
                    {
                        MyCocktail.Image = requestedCocktail[i].strDrinkThumb;
                    }
                    else { max++; continue; }

                    if (requestedCocktail[i].strTags != null)
                    {
                        MyCocktail.Tags = requestedCocktail[i].strTags;
                    }
                    else { max++; continue; }

                    string[] IngredientsTemp = new string[13];

                    //Les ingredients ne sont pas stocké dans un array mais dans 13 variable différentes, on est obligé
                    //de faire ça pour tous les récupérer (pareil pour les mesures mais on les met dans un même string.
                    if (requestedCocktail[i].strIngredient1 != null && requestedCocktail[i].strIngredient1 != "")
                    {
                        IngredientsTemp[0] = requestedCocktail[i].strIngredient1;
                        if (requestedCocktail[i].strMeasure1 != null)
                        {
                            IngredientsTemp[0] = IngredientsTemp[0] + " : " + requestedCocktail[i].strMeasure1;
                        }
                    } else
                    { max++; continue; }
                    if (requestedCocktail[i].strIngredient2 != null && requestedCocktail[i].strIngredient2 != "")
                    {
                        IngredientsTemp[1] = requestedCocktail[i].strIngredient2;
                        if (requestedCocktail[i].strMeasure2 != null)
                        {
                            IngredientsTemp[1] = IngredientsTemp[1] + " : " + requestedCocktail[i].strMeasure2;
                        }
                    }
                    if (requestedCocktail[i].strIngredient3 != null && requestedCocktail[i].strIngredient3 != "")
                    {
                        IngredientsTemp[2] = requestedCocktail[i].strIngredient3;
                        if (requestedCocktail[i].strMeasure3 != null)
                        {
                            IngredientsTemp[2] = IngredientsTemp[2] + " : " + requestedCocktail[i].strMeasure3;
                        }
                    }
                    if (requestedCocktail[i].strIngredient4 != null && requestedCocktail[i].strIngredient4 != "")
                    {
                        IngredientsTemp[3] = requestedCocktail[i].strIngredient4;
                        if (requestedCocktail[i].strMeasure4 != null)
                        {
                            IngredientsTemp[3] = IngredientsTemp[3] + " : " + requestedCocktail[i].strMeasure4;
                        }
                    }
                    if (requestedCocktail[i].strIngredient5 != null && requestedCocktail[i].strIngredient5 != "")
                    {
                        IngredientsTemp[4] = requestedCocktail[i].strIngredient5;
                        if (requestedCocktail[i].strMeasure5 != null)
                        {
                            IngredientsTemp[4] = IngredientsTemp[4] + " : " + requestedCocktail[i].strMeasure5;
                        }
                    }
                    if (requestedCocktail[i].strIngredient6 != null && requestedCocktail[i].strIngredient6 != "")
                    {
                        IngredientsTemp[5] = requestedCocktail[i].strIngredient6;
                        if (requestedCocktail[i].strMeasure6 != null)
                        {
                            IngredientsTemp[5] = IngredientsTemp[5] + " : " + requestedCocktail[i].strMeasure6;
                        }
                    }
                    if (requestedCocktail[i].strIngredient7 != null && requestedCocktail[i].strIngredient7 != "")
                    {
                        IngredientsTemp[6] = requestedCocktail[i].strIngredient7;
                        if (requestedCocktail[i].strMeasure7 != null)
                        {
                            IngredientsTemp[6] = IngredientsTemp[6] + " : " + requestedCocktail[i].strMeasure7;
                        }
                    }
                    if (requestedCocktail[i].strIngredient8 != null && requestedCocktail[i].strIngredient8 != "")
                    {
                        IngredientsTemp[7] = requestedCocktail[i].strIngredient8;
                        if (requestedCocktail[i].strMeasure8 != null)
                        {
                            IngredientsTemp[7] = IngredientsTemp[7] + " : " + requestedCocktail[i].strMeasure8;
                        }
                    }
                    if (requestedCocktail[i].strIngredient9 != null && requestedCocktail[i].strIngredient9 != "")
                    {
                        IngredientsTemp[8] = requestedCocktail[i].strIngredient9;
                        if (requestedCocktail[i].strMeasure9 != null)
                        {
                            IngredientsTemp[8] = IngredientsTemp[8] + " : " + requestedCocktail[i].strMeasure9;
                        }
                    }
                    if (requestedCocktail[i].strIngredient10 != null && requestedCocktail[i].strIngredient10 != "")
                    {
                        IngredientsTemp[9] = requestedCocktail[i].strIngredient10;
                        if (requestedCocktail[i].strMeasure10 != null)
                        {
                            IngredientsTemp[9] = IngredientsTemp[9] + " : " + requestedCocktail[i].strMeasure10;
                        }
                    }
                    if (requestedCocktail[i].strIngredient11 != null && requestedCocktail[i].strIngredient11 != "")
                    {
                        IngredientsTemp[10] = requestedCocktail[i].strIngredient11;
                        if (requestedCocktail[i].strMeasure1 != null)
                        {
                            IngredientsTemp[10] = IngredientsTemp[10] + " : " + requestedCocktail[i].strMeasure11;
                        }
                    }
                    if (requestedCocktail[i].strIngredient12 != null && requestedCocktail[i].strIngredient12 != "")
                    {
                        IngredientsTemp[11] = requestedCocktail[i].strIngredient12;
                        if (requestedCocktail[i].strMeasure12 != null)
                        {
                            IngredientsTemp[11] = IngredientsTemp[11] + " : " + requestedCocktail[i].strMeasure12;
                        }
                    }
                    if (requestedCocktail[i].strIngredient13 != null && requestedCocktail[i].strIngredient13 != "")
                    {
                        IngredientsTemp[12] = requestedCocktail[i].strIngredient12;
                        if (requestedCocktail[i].strMeasure13 != null)
                        {
                            IngredientsTemp[12] = IngredientsTemp[12] + " : " + requestedCocktail[i].strMeasure12;
                        }
                    }
                    if (requestedCocktail[i].strIngredient14 != null && requestedCocktail[i].strIngredient14 != "")
                    {
                        IngredientsTemp[13] = requestedCocktail[i].strIngredient14;
                        if (requestedCocktail[i].strMeasure14 != null)
                        {
                            IngredientsTemp[13] = IngredientsTemp[13] + " : " + requestedCocktail[i].strMeasure13;
                        }
                    }


                    //Une fois qu'on a tous nos ingrédients on les concat dans un string pour la base.
                    //Cependant on remettra ses ingrédients dans un array pour les faire afficher dans les
                    //détails, d'où la présence de deux variable pour définir nos cocktails (BDCocktail et
                    //LocalCocktail)
                    MyCocktail.Ingredients = String.Join(",", IngredientsTemp);
                    //Debug.WriteLine(i + " + " + MyCocktail.Name + "/ id :" + MyCocktail.Id);

                    await App.Database.SaveCocktailAsync(MyCocktail);

                }
            }
            
            //On rajoute dans notre liste (observable) les cocktails de notre base de donnée.
            ObservableCollection<BDCocktail> ListeDeCocktailDBTemp = new ObservableCollection<BDCocktail>(App.Database.GetCocktailAsync().Result);
            List<LocalCocktail> ListeDeCocktailLocalTemp = new List<LocalCocktail>();
            //Comme indiqué précédemment, SQLite ne nous permet pas de stocker des arrays de string, or pour notre page de détails on en a besoin
            //Ce qui explique pourquoi on passe de nos BDCocktail de la base à nos LocalCocktail.
            foreach (BDCocktail CocktailDBTemp in ListeDeCocktailDBTemp) {
                //La condition sur l'Id sert à pourvoir afficher nos cocktails ajouté en haut de la liste
                if (CocktailDBTemp.Id < 50)
                {
                    ListeDeCocktailLocalTemp.Add(new LocalCocktail()
                    {
                        Id = CocktailDBTemp.Id,
                        Name = CocktailDBTemp.Name,
                        Instructions = CocktailDBTemp.Instructions,
                        Ingredients = CocktailDBTemp.Ingredients.Split(','),
                        Image = CocktailDBTemp.Image,
                        Tags = CocktailDBTemp.Tags.Split(',')
                    });
                } else
                {
                    ListeDeCocktailLocalTemp.Insert(0,new LocalCocktail()
                    {
                        Id = CocktailDBTemp.Id,
                        Name = CocktailDBTemp.Name,
                        Instructions = CocktailDBTemp.Instructions,
                        Ingredients = CocktailDBTemp.Ingredients.Split(','),
                        Image = CocktailDBTemp.Image,
                        Tags = CocktailDBTemp.Tags.Split(',')
                    });
                }
                
            };
            ListeDeCocktail = new ObservableCollection<LocalCocktail>(ListeDeCocktailLocalTemp);

        }

    }

}