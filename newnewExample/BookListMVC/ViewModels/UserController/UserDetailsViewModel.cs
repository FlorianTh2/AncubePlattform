using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListMVC.Models.User;

namespace BookListMVC.ViewModels.UserController
{
    //ViewModel = also Data Transfer Objects
    // Klasse für eig eine Methode eines Controllers (=also eine View), die
    // alle wichtigen Daten als Properties hällt, damit mit
    // strongly typed views gearbeitet werden kann
    // z.B. User mit Pagetitle, damit ein Object an die View gegeben werden kann
    public class UserDetailsViewModel
    {
        public User User { get; set; }
        public string PageTitle { get; set; }
    }
}
