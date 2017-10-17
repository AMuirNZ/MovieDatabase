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

            txtDateTime.Text = (DateTime.Now).ToString();
            //clear out the old data
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

        }

        private void btnAddMovie_Click(object sender, EventArgs e)
        {
            // this puts the parameters into the code so that the data in the text boxes is added to the database

            string NewEntry = "INSERT INTO Movies ( Rating, Title, Year, Plot, Genre) VALUES ( @Rating, @Title, @Year, @Plot, @Genre)";



            SqlConnection Con = new SqlConnection();

            string connectionString = @"Data Source = LAPTOP-TKI3D179;Initial Catalog=MoviesDSED02;Integrated Security=True";

            Con.ConnectionString = connectionString;

            using (SqlCommand newdata = new SqlCommand(NewEntry, Con))

            {



                newdata.Parameters.AddWithValue("@Rating", txtMovieRating.Text);

                newdata.Parameters.AddWithValue("@Title", txtMovieTitle.Text);

                newdata.Parameters.AddWithValue("@Year", txtMovieYear.Text);




                newdata.Parameters.AddWithValue("@Plot", txtMoviePlot.Text);

                newdata.Parameters.AddWithValue("@Genre", txtMovieGenre.Text);





                Con.Open(); //open a connection to the database

                //its a NONQuery as it doesn't return any data its only going up to the server

                newdata.ExecuteNonQuery();  //Run the Query

                //a happy message box

                MessageBox.Show("Data has been Inserted  !! ");
                Con.Close();

            }

            //Run the LoadDatabase method we made earler to see the new data.

            DisplayDataGridViewMovies();
        }

        private void btnUpdateMovie_Click(object sender, EventArgs e)
        {
            //this updates existing data in the database where the ID of the data equals the ID in the text box

            string updatestatement = "Update Movies set Rating=@Rating, Title=@Title, Year=@Year, Plot=@Plot, Genre=@Genre where MovieID = @MovieID";

            // Rating, Title, Year, Plot, Genre
            // Rating, Title, Year, Plot, Genre
            // Rating, Title, Year, Plot, Genre


            SqlConnection Con = new SqlConnection();
            string connectionString = @"Data Source = LAPTOP-TKI3D179;Initial Catalog=MoviesDSED02;Integrated Security=True";
            Con.ConnectionString = connectionString;

            SqlCommand update = new SqlCommand(updatestatement, Con);
            // create the parameters and pass the data from the textboxes 
            update.Parameters.AddWithValue("@MovieID", txtMovieID.Text);
            update.Parameters.AddWithValue("@Rating", txtMovieRating.Text);
            update.Parameters.AddWithValue("@Title", txtMovieTitle.Text);
            update.Parameters.AddWithValue("@Year", txtMovieYear.Text);
            update.Parameters.AddWithValue("@Plot", txtMoviePlot.Text);
            update.Parameters.AddWithValue("@Genre", txtMovieGenre.Text);


            Con.Open();
            //it's NonQuery as data is only going up

            update.ExecuteNonQuery();
            Con.Close();
            DisplayDataGridViewMovies();
        }

        private void btnDeleteMovie_Click(object sender, EventArgs e)
        {
            SqlConnection Con = new SqlConnection();
            string connectionString = @"Data Source = LAPTOP-TKI3D179;Initial Catalog=MoviesDSED02;Integrated Security=True";
            Con.ConnectionString = connectionString;

            string DeleteCommand = "Delete Movies where MovieID = @MovieID";

            SqlCommand DeleteData = new SqlCommand(DeleteCommand, Con);
            DeleteData.Parameters.AddWithValue("@MovieID", txtMovieID.Text);

            Con.Open();
            DeleteData.ExecuteNonQuery();
            Con.Close();
            DisplayDataGridViewMovies();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            // this puts the parameters into the code so that the data in the text boxes is added to the database

            string NewEntry = "INSERT INTO Customer (FirstName, LastName, Address, Phone) VALUES ( @FirstName, @LastName, @Address, @Phone)";



            SqlConnection Con = new SqlConnection();

            string connectionString = @"Data Source = LAPTOP-TKI3D179;Initial Catalog=MoviesDSED02;Integrated Security=True";

            Con.ConnectionString = connectionString;

            using (SqlCommand newdata = new SqlCommand(NewEntry, Con))

            {



                newdata.Parameters.AddWithValue("@FirstName", txtCustFirstName.Text);

                newdata.Parameters.AddWithValue("@LastName", txtCustLastName.Text);

                newdata.Parameters.AddWithValue("@Address", txtCustAddress.Text);




                newdata.Parameters.AddWithValue("@Phone", txtCustPhone.Text);

               





                Con.Open(); //open a connection to the database

                //its a NONQuery as it doesn't return any data its only going up to the server

                newdata.ExecuteNonQuery();  //Run the Query

                //a happy message box

                MessageBox.Show("Data has been Inserted  !! ");
                Con.Close();
            }

            DisplayDataGridViewCustomers();
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            SqlConnection Con = new SqlConnection();
            string connectionString = @"Data Source = LAPTOP-TKI3D179;Initial Catalog=MoviesDSED02;Integrated Security=True";
            Con.ConnectionString = connectionString;

            string DeleteCommand = "Delete Customer where CustID = @CustID";

            SqlCommand DeleteData = new SqlCommand(DeleteCommand, Con);
            DeleteData.Parameters.AddWithValue("@CustID", txtCustID.Text);

            Con.Open();
            DeleteData.ExecuteNonQuery();
            Con.Close();
            DisplayDataGridViewCustomers();
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            //this updates existing data in the database where the ID of the data equals the ID in the text box

            string updatestatement = "Update Customer set FirstName=@FirstName, LastName=@LastName, Address=@Address, Phone=@Phone where CustID = @CustID";

            

            SqlConnection Con = new SqlConnection();
            string connectionString = @"Data Source = LAPTOP-TKI3D179;Initial Catalog=MoviesDSED02;Integrated Security=True";
            Con.ConnectionString = connectionString;

            SqlCommand update = new SqlCommand(updatestatement, Con);
            // create the parameters and pass the data from the textboxes 
            update.Parameters.AddWithValue("@CustID", txtCustID.Text);
            update.Parameters.AddWithValue("@FirstName", txtCustFirstName.Text);
            update.Parameters.AddWithValue("@LastName", txtCustLastName.Text);
            update.Parameters.AddWithValue("@Address", txtCustAddress.Text);
            update.Parameters.AddWithValue("@Phone", txtCustPhone.Text);



            Con.Open();
            //it's NonQuery as data is only going up

            update.ExecuteNonQuery();
            Con.Close();
            DisplayDataGridViewCustomers();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            DateTime Date = Convert.ToDateTime(txtDateTime.Text);

            // this puts the parameters into the code so that the data in the text boxes is added to the database

            string NewEntry = "INSERT INTO RentedMovies (MovieIDFK, CustIDFK, DateRented) VALUES ( @MovieIDFK, @CustIDFK, @DateRented)";



            SqlConnection Con = new SqlConnection();

            string connectionString = @"Data Source = LAPTOP-TKI3D179;Initial Catalog=MoviesDSED02;Integrated Security=True";

            Con.ConnectionString = connectionString;

            using (SqlCommand newdata = new SqlCommand(NewEntry, Con))
//
            {



                newdata.Parameters.AddWithValue("@MovieIDFK", txtMovieID.Text);

                newdata.Parameters.AddWithValue("@CustIDFK", txtCustID.Text);

                newdata.Parameters.AddWithValue("@DateRented", Date);



                






                Con.Open(); //open a connection to the database

                //its a NONQuery as it doesn't return any data its only going up to the server

                newdata.ExecuteNonQuery();  //Run the Query

                //a happy message box

                MessageBox.Show("Data has been Inserted  !! ");
                Con.Close();
            }

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
            //this updates existing data in the database where the ID of the data equals the ID in the ext box

            string updatestatement = "Update RentedMovies set DateReturned=@DateReturned where RMID=@RMID";



            SqlConnection Con = new SqlConnection();
            string connectionString = @"Data Source = LAPTOP-TKI3D179;Initial Catalog=MoviesDSED02;Integrated Security=True";
            Con.ConnectionString = connectionString;

            SqlCommand update = new SqlCommand(updatestatement, Con);
            // create the parameters and pass the data from the textboxes 
            update.Parameters.AddWithValue("@RMID", txtRentalID.Text);
            update.Parameters.AddWithValue("@DateReturned", Date);
            



            Con.Open();
            //it's NonQuery as data is only going up

            update.ExecuteNonQuery();
            Con.Close();
            DisplayDataGridViewRentals();
        }
    }
    }





