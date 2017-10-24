using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieDatabase
{
    public partial class Form1 : Form
    {
        //create an instance of the Database class
        Database myDatabase = new Database();

        public Form1()
        {
            
            InitializeComponent();
            loadDB();
        }

        public void loadDB()
        {
            //load the owner dgv
            DisplayDataGridViewMovies();
            DisplayDataGridViewCustomers();
            DisplayDataGridViewRentals();
        }

        //LOAD THE OWNER DATAGRID
        private void DisplayDataGridViewMovies()
        {

            txtDateTime2.Text = (DateTime.Now.Date.Year).ToString();
            txtDateTime.Text = (DateTime.Now).ToString();
        
            DGVMovies.DataSource = null;
            try
            {
                DGVMovies.DataSource = myDatabase.FillDGVMoviesWithMovies();
                //pass the datatable data to the DataGridView
                DGVMovies.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void DisplayDataGridViewCustomers()
        {
            //clear out the old data
            DGVCustomers.DataSource = null;
            try
            {
                DGVCustomers.DataSource = myDatabase.FillDGVCustomersWithCustomers();
                //pass the datatable data to the DataGridView
                DGVCustomers.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayDataGridViewRentals()
        {
            //clear out the old data
            DGVMoviesRented.DataSource = null;
            try
            {
                DGVMoviesRented.DataSource = myDatabase.FillDGVRentalsWithRentals();
                //pass the datatable data to the DataGridView
                DGVMoviesRented.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DGVCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            int CustID = 0;
            //these are the cell clicks for the values in the row that you click on

            CustID = (int)DGVCustomers.Rows[e.RowIndex].Cells[0].Value;
            txtCustFirstName.Text = DGVCustomers.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCustLastName.Text = DGVCustomers.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtCustAddress.Text = DGVCustomers.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtCustPhone.Text = DGVCustomers.Rows[e.RowIndex].Cells[4].Value.ToString();
           
            if (e.RowIndex >= 0)
            {
                //Fill the next CD DGV with the owner ID

                txtCustID.Text = CustID.ToString();

            }

        }

        private void DGVMovies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
            var today = DateTime.Now;
            try
            {
                //show the data in the DGV in the text boxes
                string newvalue = DGVMovies.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                //show the output on the header
                this.Text = "Row : " + e.RowIndex.ToString() + "  Col : " + e.ColumnIndex.ToString() + " Value = " + newvalue;
                //pass data to the text boxes
                txtMovieID.Text = DGVMovies.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtMovieRating.Text = DGVMovies.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtMovieTitle.Text = DGVMovies.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtMovieYear.Text = DGVMovies.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtMoviePlot.Text = DGVMovies.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtMovieGenre.Text = DGVMovies.Rows[e.RowIndex].Cells[5].Value.ToString();

                
                
            }
            catch
            {
            }
            int thisYear = Convert.ToInt16(txtDateTime2.Text);
            int Year = Convert.ToInt16(txtMovieYear.Text);

            int fee = myDatabase.FeeCalculation(Year, thisYear);



            txtMovieFee.Text = "$" + fee.ToString() + ".00";

        }

        private void btnAddMovie_Click(object sender, EventArgs e)
        {

            myDatabase.AddMovie(txtMovieRating.Text, txtMovieTitle.Text, txtMovieYear.Text, txtMoviePlot.Text,
            txtMovieGenre.Text);
            

            //Run the LoadDatabase method we made earler to see the new data.

            DisplayDataGridViewMovies();
        }

        private void btnUpdateMovie_Click(object sender, EventArgs e)
        {

            myDatabase.UpdateMovie(txtMovieID.Text, txtMovieRating.Text, txtMovieTitle.Text,
                txtMovieYear.Text, txtMoviePlot.Text, txtMovieGenre.Text);

            
            DisplayDataGridViewMovies();
        }

        private void btnDeleteMovie_Click(object sender, EventArgs e)
        {
            myDatabase.deleteMovie(txtMovieID.Text);

            
            DisplayDataGridViewMovies();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            myDatabase.AddCustomer(txtCustFirstName.Text, txtCustLastName.Text, txtCustAddress.Text, txtCustPhone.Text);

            DisplayDataGridViewCustomers();
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            myDatabase.DeleteCustomer(txtCustID.Text);


           
            DisplayDataGridViewCustomers();
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            myDatabase.UpdateCustomer(txtCustID.Text, txtCustFirstName.Text, txtCustLastName.Text, txtCustAddress.Text, txtCustPhone.Text);
            
            
            DisplayDataGridViewCustomers();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            DateTime Date = Convert.ToDateTime(txtDateTime.Text);
            myDatabase.CheckOut(txtMovieID.Text, txtCustID.Text, Date);
            

           DisplayDataGridViewRentals();
        }

        private void DGVMoviesRented_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //show the data in the DGV in the text boxes
                string newvalue = DGVMoviesRented.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                //show the output on the header
                this.Text = "Row : " + e.RowIndex.ToString() + "  Col : " + e.ColumnIndex.ToString() + " Value = " + newvalue;
                //pass data to the text boxes
                txtRentalID.Text = DGVMoviesRented.Rows[e.RowIndex].Cells[0].Value.ToString();
                
            }
            catch
            {
            }

        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            DateTime Date = Convert.ToDateTime(txtDateTime.Text);
            myDatabase.returnMovie(txtRentalID.Text, Date);
            
            


            
            DisplayDataGridViewRentals();
        }

        private void rdoShowMoviesOut_CheckedChanged(object sender, EventArgs e)
        {
            DGVMovies.DataSource = null;
            try
            {
                DGVMovies.DataSource = myDatabase.FillDGVMoviesWithMoviesOut();
                //pass the datatable data to the DataGridView
                DGVMovies.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rdoAllMovies_CheckedChanged(object sender, EventArgs e)
        {
            DisplayDataGridViewMovies();
        }
    }
    }





