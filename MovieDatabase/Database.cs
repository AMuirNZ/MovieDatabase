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
        //Create Connection and Command,and an Adapter.
        private SqlConnection Connection = new SqlConnection();

        private SqlCommand Command = new SqlCommand();

        private SqlDataAdapter da = new SqlDataAdapter();

        //THE CONSTRUCTOR SETS THE DEFAULTS UPON LOADING THE CLASS
        public Database()
        {
            //change the connection string to run from your own music db
            string connectionString =
                @"Data Source = LAPTOP-TKI3D179;Initial Catalog=MoviesDSED02;Integrated Security=True";
            Connection.ConnectionString = connectionString;
            Command.Connection = Connection;
        }

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

        public DataTable FillDGMoviesWithMoviesClick(string Moviesvalue)
        {
            string SQL = "select Date from CustomerMovieRentals where MovieIDFK = '" + Moviesvalue + "' ";

            da = new SqlDataAdapter(SQL, Connection);
            //connect in to the DB and get the SQL
            DataTable dt = new DataTable();
            //create a datatable as we only have one table, the Owner
            Connection.Open();
            //open a connection to the DB
            da.Fill(dt);
            //fill the datatable from the SQL
            Connection.Close();
            //close the connection
            return dt;
        }

        public void AddCustomer(string FirstName, string LastName, string Address, string Phone)
        {
            // this puts the parameters into the code so that the data in the text boxes is added to the database



            string NewEntry = "INSERT INTO Customer (FirstName, LastName, Address, Phone) VALUES ( @FirstName, @LastName, @Address, @Phone)";



            SqlConnection Con = new SqlConnection();

            string connectionString = @"Data Source = LAPTOP-TKI3D179;Initial Catalog=MoviesDSED02;Integrated Security=True";

            Con.ConnectionString = connectionString;

            using (SqlCommand newdata = new SqlCommand(NewEntry, Con))

            {

                newdata.Parameters.AddWithValue("@FirstName", FirstName);

                newdata.Parameters.AddWithValue("@LastName", LastName);

                newdata.Parameters.AddWithValue("@Address", Address);

                newdata.Parameters.AddWithValue("@Phone", Phone);
                
                Con.Open(); //open a connection to the database

                //its a NONQuery as it doesn't return any data its only going up to the server

                newdata.ExecuteNonQuery();  //Run the Query

                //a happy message box

                MessageBox.Show("Data has been Inserted  !! ");
                Con.Close();
            }
        }

        public void AddMovie(string rating, string title, string year, string plot, string genre)
        {
            // this puts the parameters into the code so that the data in the text boxes is added to the database

            string NewEntry = "INSERT INTO Movies ( Rating, Title, Year, Plot, Genre) VALUES ( @Rating, @Title, @Year, @Plot, @Genre)";



            SqlConnection Con = new SqlConnection();

            string connectionString = @"Data Source = LAPTOP-TKI3D179;Initial Catalog=MoviesDSED02;Integrated Security=True";

            Con.ConnectionString = connectionString;

            using (SqlCommand newdata = new SqlCommand(NewEntry, Con))

            {



                newdata.Parameters.AddWithValue("@Rating", rating);

                newdata.Parameters.AddWithValue("@Title", title);

                newdata.Parameters.AddWithValue("@Year", year);




                newdata.Parameters.AddWithValue("@Plot", plot);

                newdata.Parameters.AddWithValue("@Genre", genre);





                Con.Open(); //open a connection to the database

                //its a NONQuery as it doesn't return any data its only going up to the server

                newdata.ExecuteNonQuery();  //Run the Query

                //a happy message box

                MessageBox.Show("Data has been Inserted  !! ");
                Con.Close();

            }
        }

        public void CheckOut(string MovieID, string CustID, DateTime Date)
        {
            string NewEntry = "INSERT INTO RentedMovies (MovieIDFK, CustIDFK, DateRented) VALUES ( @MovieIDFK, @CustIDFK, @DateRented)";



            SqlConnection Con = new SqlConnection();

            string connectionString = @"Data Source = LAPTOP-TKI3D179;Initial Catalog=MoviesDSED02;Integrated Security=True";

            Con.ConnectionString = connectionString;

            using (SqlCommand newdata = new SqlCommand(NewEntry, Con))

            {
                newdata.Parameters.AddWithValue("@MovieIDFK", MovieID);

                newdata.Parameters.AddWithValue("@CustIDFK", CustID);

                newdata.Parameters.AddWithValue("@DateRented", Date);

                Con.Open(); //open a connection to the database

                //its a NONQuery as it doesn't return any data its only going up to the server

                newdata.ExecuteNonQuery();  //Run the Query



                MessageBox.Show("Data has been Inserted!! ");
                Con.Close();
            }
        }

        public void UpdateMovie(string MovieID, string MovieRating, string Title, string Year, string Plot,
            string Genre)
        {
            //this updates existing data in the database where the ID of the data equals the ID in the text box

            string updatestatement = "Update Movies set Rating=@Rating, Title=@Title, Year=@Year, Plot=@Plot, Genre=@Genre where MovieID = @MovieID";



            SqlConnection Con = new SqlConnection();
            string connectionString = @"Data Source = LAPTOP-TKI3D179;Initial Catalog=MoviesDSED02;Integrated Security=True";
            Con.ConnectionString = connectionString;

            SqlCommand update = new SqlCommand(updatestatement, Con);
            // create the parameters and pass the data from the textboxes 
            update.Parameters.AddWithValue("@MovieID", MovieID);
            update.Parameters.AddWithValue("@Rating", MovieRating);
            update.Parameters.AddWithValue("@Title", Title);
            update.Parameters.AddWithValue("@Year", Year);
            update.Parameters.AddWithValue("@Plot", Plot);
            update.Parameters.AddWithValue("@Genre", Genre);


            Con.Open();
            //it's NonQuery as data is only going up

            update.ExecuteNonQuery();
            Con.Close();
        }

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

        public void DeleteCustomer(string CustomerID)
        {
            SqlConnection Con = new SqlConnection();
            string connectionString = @"Data Source = LAPTOP-TKI3D179;Initial Catalog=MoviesDSED02;Integrated Security=True";
            Con.ConnectionString = connectionString;

            string DeleteCommand = "Delete Customer where CustID = @CustID";

            SqlCommand DeleteData = new SqlCommand(DeleteCommand, Con);
            DeleteData.Parameters.AddWithValue("@CustID", CustomerID);

            Con.Open();
            DeleteData.ExecuteNonQuery();
            Con.Close();
        }

        public void UpdateCustomer(string CustID, string firstName, string lastName, string address, string phone)
        {
            //this updates existing data in the database where the ID of the data equals the ID in the text box

            string updatestatement = "Update Customer set FirstName=@FirstName, LastName=@LastName, Address=@Address, Phone=@Phone where CustID = @CustID";



            SqlConnection Con = new SqlConnection();
            string connectionString = @"Data Source = LAPTOP-TKI3D179;Initial Catalog=MoviesDSED02;Integrated Security=True";
            Con.ConnectionString = connectionString;

            SqlCommand update = new SqlCommand(updatestatement, Con);
            // create the parameters and pass the data from the textboxes 
            update.Parameters.AddWithValue("@CustID", CustID);
            update.Parameters.AddWithValue("@FirstName", firstName);
            update.Parameters.AddWithValue("@LastName", lastName);
            update.Parameters.AddWithValue("@Address", address);
            update.Parameters.AddWithValue("@Phone", phone);



            Con.Open();
            //it's NonQuery as data is only going up

            update.ExecuteNonQuery();
            Con.Close();
        }

        public void returnMovie(string rentalID, DateTime date)
        {
            //this updates existing data in the database where the ID of the data equals the ID in the ext box

            string updatestatement = "Update RentedMovies set DateReturned=@DateReturned where RMID=@RMID";



            SqlConnection Con = new SqlConnection();
            string connectionString = @"Data Source = LAPTOP-TKI3D179;Initial Catalog=MoviesDSED02;Integrated Security=True";
            Con.ConnectionString = connectionString;

            SqlCommand update = new SqlCommand(updatestatement, Con);
            // create the parameters and pass the data from the textboxes 
            update.Parameters.AddWithValue("@RMID", rentalID);
            update.Parameters.AddWithValue("@DateReturned", date);




            Con.Open();
            //it's NonQuery as data is only going up

            update.ExecuteNonQuery();
            Con.Close();
        }

        public void deleteMovie(string movieID)
        {
            SqlConnection Con = new SqlConnection();
            string connectionString = @"Data Source = LAPTOP-TKI3D179;Initial Catalog=MoviesDSED02;Integrated Security=True";
            Con.ConnectionString = connectionString;

            string DeleteCommand = "Delete Movies where MovieID = @MovieID";

            SqlCommand DeleteData = new SqlCommand(DeleteCommand, Con);
            DeleteData.Parameters.AddWithValue("@MovieID", movieID);

            Con.Open();
            DeleteData.ExecuteNonQuery();
            Con.Close();
        }


      
    }

    
}

