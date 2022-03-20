using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

using Xamarin.Forms;

namespace AVosCocktails.Model
{
    //Cocktails affichés dans la liste
    public class LocalCocktail
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public string Instructions { get; set;}
        public string[] Ingredients { get; set;}
        public string Image { get; set; }
        public string[] Tags { get; set; }


    }
}