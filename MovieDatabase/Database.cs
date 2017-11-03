using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MovieDatabase
{
    public class Database
    {
        #region Set up SQL Connection

        //Create Connection and Command,and an Adapter.
        private SqlConnection Connection = new SqlConnection();

        private SqlCommand Command = new SqlCommand();
        private SqlDataAdapter da = new SqlDataAdapter();

        //THE CONSTRUCTOR SETS THE DEFAULTS UPON LOADING THE CLASS


        #endregion

        #region database information
        public Database()
        {
            //change the connection string to run from your own music db
            string connectionString =
                @"Data Source = LAPTOP-TKI3D179;Initial Catalog=MoviesDSED02;Integrated Security=True";
            Connection.ConnectionString = connectionString;
            Command.Connection = Connection;
        }
        #endregion



        #region All movies

        public DataTable FillDGVMoviesWithMovies()
        {
            //create a datatable as we only have one table, the Owner
            DataTable dt = new DataTable();
            using (da = new SqlDataAdapter("select * from MovieView ", Connection))
            {
                //connect in to the DB and get the SQL
                Connection.Open();
                //open a connection to the DB
                da.Fill(dt);
                //fill the datatable from the SQL
                Connection.Close(); //close the connection
            }
            return dt; //pass the datatable data to the DataGridView
        }

        #endregion

        #region Select all movies out
        public DataTable FillDGVMoviesWithMoviesOut()
        {
            //create a datatable as we only have one table, the Owner
            DataTable dt = new DataTable();
            using (da = new SqlDataAdapter("select * from MoviesOut where DateReturned IS NULL", Connection))
            {
                //connect in to the DB and get the SQL
                Connection.Open();
                //open a connection to the DB
                da.Fill(dt);
                //fill the datatable from the SQL
                Connection.Close(); //close the connection
            }
            return dt; //pass the datatable data to the DataGridView
        }

        #endregion

        #region customers tab

        public DataTable FillDGVCustomersWithCustomers()
        {
            //create a datatable as we only have one table, the Owner
            DataTable dt = new DataTable();
            using (da = new SqlDataAdapter("select * from Customer ", Connection))
            {
                //connect in to the DB and get the SQL
                Connection.Open();
                //open a connection to the DB
                da.Fill(dt);
                //fill the datatable from the SQL
                Connection.Close(); //close the connection
            }
            return dt; //pass the datatable data to the DataGridView
        }

        #endregion

        #region rentals tab

        public DataTable FillDGVRentalsWithRentals()
        {
            //create a datatable as we only have one table, the Owner
            DataTable dt = new DataTable();
            using (da = new SqlDataAdapter("select * from CustomerMoviesRentals ", Connection))

            {
                //connect in to the DB and get the SQL
                Connection.Open();
                //open a connection to the DB
                da.Fill(dt);
                //fill the datatable from the SQL
                Connection.Close(); //close the connection
            }
            return dt; //pass the datatable data to the DataGridView
        }

        #endregion

        #region fill form with database information

        public DataTable FillDGVTopCustomersWithTopCustomers()
        {
            //create a datatable as we only have one table, the Owner
            DataTable dt = new DataTable();
            using (da = new SqlDataAdapter("select * from TopCustomers ORDER BY MoviesRented DESC ", Connection))

            {
                //connect in to the DB and get the SQL
                Connection.Open();
                //open a connection to the DB
                da.Fill(dt);
                //fill the datatable from the SQL
                Connection.Close(); //close the connection
            }
            return dt; //pass the datatable data to the DataGridView
        }

        #endregion

        #region top movies
        public DataTable FillDGVTopMoviesWithTopMovies()
        {
            //create a datatable as we only have one table, the Owner
            DataTable dt = new DataTable();
            using (da = new SqlDataAdapter("select * from TopMovies ORDER BY RentalAmount DESC ", Connection))

            {
                //connect in to the DB and get the SQL
                Connection.Open();
                //open a connection to the DB
                da.Fill(dt);
                //fill the datatable from the SQL
                Connection.Close(); //close the connection
            }
            return dt; //pass the datatable data to the DataGridView
        }
        #endregion

        #region fill datagrid view
        //fill form with information from database
        public DataTable FillDGMoviesWithMoviesClick(string Moviesvalue)
        {
            string SQL = "select Date from CustomerMovieRentals where MovieIDFK = '" + Moviesvalue + "' ";
            da = new SqlDataAdapter(SQL, Connection);
            DataTable dt = new DataTable();
            Connection.Open();
            da.Fill(dt);
            Connection.Close();
            return dt;
        }

        #endregion


        #region add customer
        //Add customer to database when data entered in form
        public string AddCustomer(string FirstName, string LastName, string Address, string Phone)
        {

                // this puts the parameters into the code so that the data in the text boxes is added to the database
                string NewEntry =
                "INSERT INTO Customer (FirstName, LastName, Address, Phone) VALUES ( @FirstName, @LastName, @Address, @Phone)";

            try
            {
                
            

            
                var newdata = new SqlCommand(NewEntry, Connection);

           

            newdata.Parameters.AddWithValue("@FirstName", FirstName);
            newdata.Parameters.AddWithValue("@LastName", LastName);
            newdata.Parameters.AddWithValue("@Address", Address);
            newdata.Parameters.AddWithValue("@Phone", Phone);

            Connection.Open(); //open a connection to the database

            newdata.ExecuteNonQuery(); //Run the Query

            //a happy message box

            return "Data has been Inserted  !! ";
            Connection.Close();
            }
            catch (Exception ex)
            {
                // need to get it to close a second time as it jumps the first connection.close when ExecuteNonQuery fails.
                Connection.Close();
                return " has Failed with " + ex;
            }
        }




        #endregion


        #region Add Movie
        //Add movie to database when data entered into form.
        public string AddMovie(string rating, string title, string year, string plot, string genre)
        {
            // this puts the parameters into the code so that the data in the text boxes is added to the database

           
           string query = "INSERT INTO Movies ( Rating, Title, Year, Plot, Genre)" + " VALUES ( @Rating, @Title, @Year, @Plot, @Genre)";

            var newdata = new SqlCommand(query, Connection);

                newdata.Parameters.AddWithValue("@Rating", rating);
                newdata.Parameters.AddWithValue("@Title", title);
                newdata.Parameters.AddWithValue("@Year", year);
                newdata.Parameters.AddWithValue("@Plot", plot);
                newdata.Parameters.AddWithValue("@Genre", genre);

                Connection.Open(); //open a connection to the database

                //its a NONQuery as it doesn't return any data its only going up to the server

                newdata.ExecuteNonQuery(); //Run the Query

            //a happy message box
            Connection.Close();
            return "Movie Entered";
                

        }
        

        #endregion

        #region Check Out movie
        //check out movie when customer and movie information on form
        public void CheckOut(string MovieID, string CustID, DateTime Date)
        {


            string query =
               "INSERT INTO RentedMovies (MovieIDFK, CustIDFK, DateRented) VALUES ( @MovieIDFK, @CustIDFK, @DateRented)";



            var newdata = new SqlCommand(query, Connection);

            
                newdata.Parameters.AddWithValue("@MovieIDFK", MovieID);

                newdata.Parameters.AddWithValue("@CustIDFK", CustID);

                newdata.Parameters.AddWithValue("@DateRented", Date);

                Connection.Open(); //open a connection to the database

                //its a NONQuery as it doesn't return any data its only going up to the server

                newdata.ExecuteNonQuery(); //Run the Query



                
                Connection.Close();
                 MessageBox.Show("Data has been Inserted!! ");
        }
        

        #endregion

        #region Update Movie
        //Updates movie information from form
        public string UpdateMovie(string MovieID, string MovieRating, string Title, string Year, string Plot,
            string Genre)
        {
            //this updates existing data in the database where the ID of the data equals the ID in the text box

           var myCommand = new SqlCommand("Update Movies set Rating=@Rating, Title=@Title, Year=@Year, Plot=@Plot, Genre=@Genre where MovieID = @MovieID", Connection);
         
            // create the parameters and pass the data from the textboxes 
            myCommand.Parameters.AddWithValue("@MovieID", MovieID);
            myCommand.Parameters.AddWithValue("@Rating", MovieRating);
            myCommand.Parameters.AddWithValue("@Title", Title);
            myCommand.Parameters.AddWithValue("@Year", Year);
            myCommand.Parameters.AddWithValue("@Plot", Plot);
            myCommand.Parameters.AddWithValue("@Genre", Genre);

            Connection.Open();
           

            myCommand.ExecuteNonQuery();
            
            Connection.Close();
            return "Movie Updated";
        }

        #endregion

        #region Fee Calculation
        //Calculate fee based on age of movie
        public int FeeCalculation(int year, int thisYear)
        {
            int difference = (thisYear - year);

            if (difference > 5)
            {
                return 2;
            }
            else
            {
                return 5;
            }
        }

        #endregion


        #region Delete Customer
        //Delete Customer from database
        public void DeleteCustomer(string CustomerID)
        {
            
            var myCommand = new SqlCommand("Delete Customer where CustID = @CustID");

            myCommand.Connection = Connection;
            myCommand.Parameters.AddWithValue("@CustID", CustomerID);
            Connection.Open();
            myCommand.ExecuteNonQuery();
            Connection.Close();
        }

        #endregion

        #region Update Customer
        //Update customer information from form
        public void UpdateCustomer(string CustID, string firstName, string lastName, string address, string phone)
        {
            //this updates existing data in the database where the ID of the data equals the ID in the text box

            var myCommand = new SqlCommand("Update Customer set FirstName=@FirstName, LastName=@LastName, Address=@Address, Phone=@Phone where CustID = @CustID", Connection);
          
            // create the parameters and pass the data from the textboxes 
            myCommand.Parameters.AddWithValue("@CustID", CustID);
            myCommand.Parameters.AddWithValue("@FirstName", firstName);
            myCommand.Parameters.AddWithValue("@LastName", lastName);
            myCommand.Parameters.AddWithValue("@Address", address);
            myCommand.Parameters.AddWithValue("@Phone", phone);

            Connection.Open();
            //it's NonQuery as data is only going up

            myCommand.ExecuteNonQuery();
            Connection.Close();
        }

        #endregion

        #region return Movie

        public void returnMovie(string rentalID, DateTime date)
        {
            //this updates existing data in the database where the ID of the data equals the ID in the ext box

            var myCommand = new SqlCommand("Update RentedMovies set DateReturned=@DateReturned where RMID=@RMID", Connection);
            
            // create the parameters and pass the data from the textboxes 
            myCommand.Parameters.AddWithValue("@RMID", rentalID);
            myCommand.Parameters.AddWithValue("@DateReturned", date);

            Connection.Open();
            
            myCommand.ExecuteNonQuery();
            Connection.Close();
        }

        #endregion



        #region delete movie
        //delete movie from Database
        public void DeleteMovie(string MovieID)
        {
            
            
                var myCommand = new SqlCommand("Delete Movies where MovieID = @MovieID");

            myCommand.Connection = Connection;
            myCommand.Parameters.AddWithValue("@MovieID", MovieID);
            Connection.Open();
            myCommand.ExecuteNonQuery();
            Connection.Close();
            }
            
        }

        #endregion

    }



    


