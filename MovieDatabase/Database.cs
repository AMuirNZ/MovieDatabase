using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MovieDatabase
{
    class Database
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
            using (da = new SqlDataAdapter("select * from Movies ", Connection))
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
            using (da = new SqlDataAdapter("select * from RentedMovies ", Connection))
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
            string SQL = "select * from CustomerMovieRentals where MovieIDFK = '" + Moviesvalue + "' ";

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
    }
}

