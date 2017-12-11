//
//SearchQuotes allows a user to search all the quote by their surface material.
//
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MegaDesk_4_ChaseKonkle
{
    public partial class SearchQuotes : Form
    {
        //Declarations
        string material;
        string[] quotes;
        string[] searchResults;
        List<string> search;

        //Initializes form and gets all the quotes.
        public SearchQuotes()       
        {
            InitializeComponent();

            List<SurfaceMaterial> surfaces = Enum.GetValues(typeof(SurfaceMaterial)).Cast<SurfaceMaterial>().ToList();
            surfaceBox.DataSource = surfaces;

            //Calls the public GetQuotes() to retrieve all the quotes.
            quotes = ViewAllQuotes.GetQuotes();           
        }

        //Search each quote to see if it matches the surface material selected in the drop down menu. 
        private string[] SearchRecord(string[] quotes, string material)
        {
            //If the default value for the drop down menu is passed to the SearchRecord() function all quotes are shown.
            if (material == "Materials")
            {
                searchResults = quotes;
                return searchResults;
            }
            //Searches each quote to see if it contains the surface material selected in the drop down menu
            else
            {
                search = new List<string>();
                //Use foreach loop to view each quote in the quotes array individually  
                foreach (string quote in quotes)
                {
                    //Checks the quote for the selected surface material and returns "true" or "false". 
                    bool result = quote.Contains(material);
                    //Adds quote if "result" is equal to "true".
                    if (result == true)
                    {
                        //Adds the quote to the other quotes that contain the selected surface material.
                        search.Add(quote);
                    }

                }
                searchResults = search.ToArray();
                return searchResults;
            }            
        }

        //Starts the quote search.
        private void searchButton_Click(object sender, EventArgs e)
        {
            //Gets the surface material form the drop down menu.
            material = surfaceBox.Text;
            //Calls the SearchRecord() function.
            searchResults = SearchRecord(quotes, material);
            //Inserts the quotes that contain the selected surface material into the form.
            searchQuotesBox.Lines = searchResults;
        }

        //Hides this form and creates a new mainMenuForm
        private void mainMenuButton_Click(object sender, EventArgs e)
        {
            MainMenu mainMenuForm = new MainMenu();
            mainMenuForm.Show();
            Hide();
        }
    }
}
