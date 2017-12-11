using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MegaDesk_4_ChaseKonkle
{
    //Declarations 
    public partial class AddQuote : Form
    {
        //Declarations
        #region Declarations
        Desk newDesk;
        DeskOrder newOrder;
        int MAXWIDTH = 96;
        int MINWIDTH = 24;
        int MAXDEPTH = 48;
        int MINDEPTH = 12;
        int MAXDRAWERS = 7;
        int MINDRAWERS = 0;
        int width = 0;
        int depth = 0;
        int numDrawers = 0;
        string surface;
        string firstName;
        string lastName;
        string rushDays;
        bool rush = false;
        DateTime quoteDate;
        object[] deskOrders;

        #endregion
        //Initializes the AddQuotes form and gathers the necessary data.
        public AddQuote()
        {
            InitializeComponent();
            //Uses the Enum "SurfaceMaterial" to initialize the values of the "surfaceBox" drop down menu.
            List<SurfaceMaterial> surfaces = Enum.GetValues(typeof(SurfaceMaterial)).Cast<SurfaceMaterial>().ToList();
            surfaceBox.DataSource = surfaces;
        }

        //Validation for all the values entered by the user.
        #region Validation
        //Validates the user input for desk width.
        private void widthBox_Validating(object sender, CancelEventArgs e)
        {
            widthBox.BackColor = SystemColors.Window;
            try
            {
                width = int.Parse(widthBox.Text);
                if (width < MINWIDTH || width > MAXWIDTH)
                {
                    widthBox.BackColor = Color.LightSalmon;
                    MessageBox.Show("The desk width must be between " + MINWIDTH + " and " + MAXWIDTH + " inches.");
                    widthBox.Text = String.Empty;                  
                    widthBox.Focus();
                }
            }
            catch (Exception)
            {
                widthBox.BackColor = Color.LightSalmon;
                MessageBox.Show("Invalid Entry. Numbers must be entered in the proper format. (Example: 31)");
                widthBox.Text = String.Empty;                
                widthBox.Focus();
            }           
        }
        //This insures that the text box only excepts numbers.
        private void widthBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        //Validates the user input for desk depth.
        private void depthBox_Validating(object sender, CancelEventArgs e)
        {
            depthBox.BackColor = SystemColors.Window;
            try
            {
                depth = int.Parse(depthBox.Text);
                if (depth < MINDEPTH || depth > MAXDEPTH)
                {
                    depthBox.BackColor = Color.LightSalmon;
                    MessageBox.Show("The desk width must be between " + MINDEPTH + " and " + MAXDEPTH + " inches.");
                    depthBox.Text = String.Empty;                   
                    depthBox.Focus();
                }
            }
            catch (Exception)
            {
                depthBox.BackColor = Color.LightSalmon;
                MessageBox.Show("Invalid Entry. Numbers must be entered in the proper format. (Example: 31)");
                depthBox.Text = String.Empty;                
                depthBox.Focus();
            }
        }

        //This insures that the text box only excepts numbers.
        private void depthBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }            
        }

        //Validates the user input for the number of drawers.
        private void drawerBox_Validating(object sender, CancelEventArgs e)
        {
            drawerBox.BackColor = SystemColors.Window;
            try
            {
                numDrawers = int.Parse(drawerBox.Text);
                if (numDrawers < MINDRAWERS || numDrawers > MAXDRAWERS)
                {
                    drawerBox.BackColor = Color.LightSalmon;
                    MessageBox.Show("The number of drawers must be between " + MINDRAWERS + " and " + MAXDRAWERS + ".");
                    drawerBox.Text = String.Empty;                   
                    drawerBox.Focus();
                }
            }
            catch (Exception)
            {
                drawerBox.BackColor = Color.LightSalmon;
                MessageBox.Show("Invalid Entry. Numbers must be entered in the proper format. (Example: 31)");
                drawerBox.Text = String.Empty;              
                drawerBox.Focus();
            }
        }

        //This insures that the text box only excepts numbers.
        private void drawerBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        //This insures that only menu items can be entered.
        private void surfaceBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //This insures that only menu items can be entered.
        private void rushDaysBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        //This activates the rushdays drop down menu or deactivates the rushdays drop down menu. 
        private void rushButton_CheckedChanged(object sender, EventArgs e)
        {
            if (rushDaysBox.Enabled == false)
            {
                rushDaysBox.Enabled = true;
            }
            else
            {
                rushDaysBox.Enabled = false;
                rushDaysBox.Text = "Standard";
            }
        }
        #endregion
        //This calculates the cost of the desk using the users inputs; it also verifies that all the text boxes are completed; and it activates the "Submit" button.
        private void calculate_Click(object sender, EventArgs e)
        {
            //Gets all the values from the form
            firstName = firstNameBox.Text;
            lastName = lastNameBox.Text;
            surface = surfaceBox.Text;
            rushDays = rushDaysBox.Text;
            quoteDate = quoteDatePicker.Value;
            rush = rushButton.Checked;
            submitButton.Enabled = true;
            submitButton.BackColor = System.Drawing.SystemColors.Control;
            firstNameBox.BackColor = SystemColors.Window;
            lastNameBox.BackColor = SystemColors.Window;
            widthBox.BackColor = SystemColors.Window;
            depthBox.BackColor = SystemColors.Window;
            drawerBox.BackColor = SystemColors.Window;
            surfaceBox.BackColor = SystemColors.Window;
            rushDaysBox.BackColor = SystemColors.Window;
            
            //Prompts user if required box are empty.
            try
            {               
                if (firstName == String.Empty)
                {
                    firstNameBox.BackColor = Color.LightSalmon;
                    MessageBox.Show("Please enter a first name.");                   
                    firstNameBox.Focus();
                }
                else if (lastName == String.Empty)
                {
                    lastNameBox.BackColor = Color.LightSalmon;
                    MessageBox.Show("Please enter a last name.");                    
                    lastNameBox.Focus();
                }
                else if (widthBox.Text == String.Empty )
                {
                    widthBox.BackColor = Color.LightSalmon;
                    MessageBox.Show("Please enter a valid desk width value.");
                    widthBox.Focus();
                }
                else if (depthBox.Text == String.Empty)
                {
                    depthBox.BackColor = Color.LightSalmon;
                    MessageBox.Show("Please enter a valid desk depth value.");                    
                    depthBox.Focus();
                }
                else if (drawerBox.Text == String.Empty)
                {
                    drawerBox.BackColor = Color.LightSalmon;
                    MessageBox.Show("Please enter a valid number of drawers.");                    
                    drawerBox.Focus();
                }
                else if (surfaceBox.Text == "Materials")
                {
                    surfaceBox.BackColor = Color.LightSalmon;
                    MessageBox.Show("Please select a surface material.");                   
                    surfaceBox.Focus();
                }
                else if (rush == true && rushDays == "Standard")
                {
                    rushDaysBox.BackColor = Color.LightSalmon;
                    MessageBox.Show("Please select your desired rush speed option.");                   
                    rushDaysBox.Focus();
                }
                else
                {
                    //The submit button is enabled
                    submitButton.Enabled = true;
                    submitButton.BackColor = System.Drawing.Color.DarkSeaGreen;

                    //An instance of Desk is created and required values a passed. 
                    newDesk = new Desk(width, depth, numDrawers, surface);
                    //An instance of DeskOrder is created and required values a passed.
                    newOrder = new DeskOrder(newDesk, firstName, lastName, rushDays, quoteDate);

                    //The Calculated price of the quote is inserted into the form. 
                    priceBox.Text = "$"+newOrder.quote.ToString() + ".00";

                    //The array deskOrders is populated with the quote values.
                    deskOrders = new object[] { newOrder.quoteDate, newOrder.lastName, newOrder.firstName, newDesk.width, newDesk.depth, newDesk.numDrawers, newDesk.surface, newOrder.rushDays, newOrder.quote };
                }
            }
            catch (Exception)
            {
                throw;
            }                     
        }


        //Sends the quote information to the DisplayQuote view for customer conformation. 
        private void submitButton_Click(object sender, EventArgs e)
        {
            //Calls the RecordOrder() function.
            RecordOrder();
              
            //Create instance of DisplayOrder displays the displayOrderForm and hides the addQuoteForm.
            DisplayOrder orderForm = new DisplayOrder(newDesk, newOrder);
            orderForm.Show();
            Hide();
        }

        //Writes the submitted order to a text file.
        private void RecordOrder()
        {
            try
            {                
                //Create output JSON.

                //Store deskOrders into a text file.
                string cFile = @"desk_orders.txt";
                //Determines if the file already exists.
                //If the desk_orders file does not exist, it is created, populated with header information, and the quote that was submitted.
                if (!File.Exists(cFile))
                {
                    //Use StreamWriter to create a text file and write the quote to the file and closing the stream upon completion.
                    using (StreamWriter sw = File.CreateText("desk_orders.txt"))
                    {
                        //Header and format key
                        sw.WriteLine("Mega Escritorio - Desk Orders");
                        sw.WriteLine("(Date and Time, Last Name, First Name, Width, Depth, Number of Drawers, Surface, Production Speed, Price)");
                        sw.WriteLine("========================================================================================================");
                        //Iterates through deskOrders object array writing each value to the text file using csv format.
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == 8)
                            {
                                sw.WriteLine(deskOrders[i]);
                            }
                            else
                            {
                                sw.Write(deskOrders[i] + ", ");
                            }
                        }
                    }
                }
                //If the text file already exists the quote is just add.
                else
                {
                    //Use StreamWriter to append the text file with write the new quote and closing the stream upon completion.
                    using (StreamWriter sw = File.AppendText("desk_orders.txt"))
                    {
                        //Iterates through deskOrders object array writing each value to the text file using csv format.
                        for (int i = 0; i < 9; i++)
                        {
                            if (i == 8)
                            {
                                sw.WriteLine(deskOrders[i]);
                            }
                            else
                            {
                                sw.Write(deskOrders[i] + ", ");
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
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
