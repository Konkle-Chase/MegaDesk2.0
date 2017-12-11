//
//   ViewAllQuotes displays all the quotes that have been submitted by reading a text file where all the quotes have been compiled.
//
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MegaDesk_4_ChaseKonkle
{
    public partial class ViewAllQuotes : Form
    {
        //Declarations
        public static string[] quotes;

        //Initializes the form, calls GetQuotes function, and inserts the data into the form.
        public ViewAllQuotes()
        {
            InitializeComponent();

            quotes = GetQuotes();
            allQuotesBox.Lines = quotes;
        }
        //Reads a text file storing each line into a an array and adds the arrays to an array list.
        public static string[] GetQuotes()
        {
            List<string> quoteList = new List<string>();

            //Creates an instance of FileStream and pass the text files location
            var fileStream = new FileStream(@"desk_orders.txt", FileMode.Open, FileAccess.Read);

            //Creates a instance of StreamReader which closes upon finishing the loop.
            using (var streamReader = new StreamReader(fileStream))
            {
                string quote;

                //Stores each line of the text file into an array
                while ((quote = streamReader.ReadLine()) != null)
                {
                    quoteList.Add(quote);
                }
            }
            quotes = quoteList.ToArray();
            return quotes;
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
