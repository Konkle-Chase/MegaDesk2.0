//
//DisplayOrder shows the user the information they have entered allowing them to double check and confirm their order.
//
using System;
using System.Windows.Forms;

namespace MegaDesk_4_ChaseKonkle
{
    public partial class DisplayOrder : Form
    {
        //Declarations
        public DeskOrder newOrder { get; set; }
        public Desk newDesk { get; set; }

        //Initializes the form and inserts the quote values 
        public DisplayOrder(Desk newDesk, DeskOrder newOrder)
        {
            InitializeComponent();
            dateDisplayBox.Text = newOrder.quoteDate.ToString();
            nameDisplayBox.Text = newOrder.firstName + " " + newOrder.lastName;
            widthDisplayBox.Text = newDesk.width.ToString() + "in.";
            depthDisplayBox.Text = newDesk.depth.ToString() + "in.";
            drawersDisplayBox.Text = newDesk.numDrawers.ToString();
            surfaceDisplayBox.Text = newDesk.surface.ToString();
            speedDisplaybox.Text = newOrder.rushDays;
            totalDisplayBox.Text = "$" + newOrder.quote.ToString() + ".00";
        }

        //Hides this form and creates a new mainMenuForm
        private void submitOrderButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thank you! Your order has been submitted.");
            MainMenu mainMenuForm = new MainMenu();
            mainMenuForm.Show();
            Hide();
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
