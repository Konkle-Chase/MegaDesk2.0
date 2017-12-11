//
//MainMain is the starting point of the program.
//
using System;
using System.IO;
using System.Windows.Forms;

namespace MegaDesk_4_ChaseKonkle
{
    //Initializes the MainMenu form.
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }
        //Hides this form and creates an instance of AddQuote and displays the addQuoteForm.
        private void addNewQuote_Click(object sender, EventArgs e)
        {
            AddQuote addNewQuoteForm = new AddQuote();
            addNewQuoteForm.Show();
            Hide();
        }

        //Hides this form and creates an instance of ViewAllQuotes and displays the viewAllQuotesForm if any quotes exist.
        private void ViewAllQuotes_Click(object sender, EventArgs e)
        {
            //Checks if any quotes exist.
            string cFile = @"desk_orders.txt";

            //If quote do exist they are then displays on the viewAllQuotesForm.
            if (File.Exists(cFile))
            {
                ViewAllQuotes viewAllQuotesForm = new ViewAllQuotes();
                viewAllQuotesForm.Show();
                Hide();
            }
            //If there aren't any quotes a message is given to the user and the mainMenuForm remains visible.
            else
            {
                MessageBox.Show("No quotes have been entered.");
            }
               
        }

        //Hides this form and creates an instance of SearchQuotes and displays the searchQuotesForm if any quotes exist.
        private void searchQuotes_Click(object sender, EventArgs e)
        {
            //Checks if any quotes exist.
            string cFile = @"desk_orders.txt";

            //If quote do exist they are then displays on the searchQuotesForm.
            if (File.Exists(cFile))
            {
                SearchQuotes searchQuotesForm = new SearchQuotes();
                searchQuotesForm.Show();
                Hide();
            }
            //If there aren't any quotes a message is given to the user and the mainMenuForm remains visible.
            else
            {
                MessageBox.Show("No quotes have been entered.");
            }
        }
        //Terminates the program.
        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
