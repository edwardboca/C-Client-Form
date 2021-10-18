using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MidtermPart2.BLL;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MidtermPart2.DAL
{
    public static class EmployeeDB
    {
        //version 1 : Working but not good
        // why? Problem: Sql Injection
        //public static void SaveRecord(Employee emp)
        //{
        //    // Step 1: Connect the database
        //     SqlConnection conn = UtilityDB.ConnectDB();
        //    //SqlConnection conn = new SqlConnection();
        //    //conn = UtilityDB.ConnectDB();
        //    // Step 2: Perform INSERT Statement
        //    SqlCommand cmdInsert = new SqlCommand();
        //    cmdInsert.CommandText = "INSERT INTO Employees " +
        //                            "VALUES(" + emp.EmployeeId +
        //                             ",'" + emp.FirstName + "'," +
        //                             "'" +emp.LastName + "'," +
        //                             "'" + emp.JobTitle + "')";
        //    cmdInsert.Connection = conn;
        //    cmdInsert.ExecuteNonQuery();

        //    //Step 3: Close the DB connection
        //    conn.Close();
        //}

        //version 2 : Better : easy to write; avoid Sql Injection
        public static void SaveRecord(Employee emp)
        {
            // Step 1: Connect the database
            SqlConnection conn = UtilityDB.ConnectDB();
            //SqlConnection conn = new SqlConnection();
            //conn = UtilityDB.ConnectDB();
            // Step 2: Perform INSERT Statement
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.CommandText = "INSERT INTO Employees(EmployeeId,FirstName,LastName,JobTitle) " +
                                    " VALUES (@EmployeeId,@FirstName,@LastName,@JobTitle)";
            cmdInsert.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
            cmdInsert.Parameters.AddWithValue("@FirstName", emp.FirstName);
            cmdInsert.Parameters.AddWithValue("@LastName", emp.LastName);
            cmdInsert.Parameters.AddWithValue("@JobTitle", emp.JobTitle);
            cmdInsert.Connection = conn;
            cmdInsert.ExecuteNonQuery();

            //Step 3: Close the DB connection
            conn.Close();
        }

        public static Employee SearchRecord(int Id)
        {
            Employee emp = new Employee();
            SqlConnection conn = UtilityDB.ConnectDB();
            conn = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand();
            cmdSelect.CommandText = "SELECT * FROM Employees " +
                                    "WHERE EmployeeId = @EmployeeId";
            cmdSelect.Parameters.AddWithValue("@EmployeeId", Id);
            cmdSelect.Connection = conn;
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();
            if (sqlReader.Read()) // found
            {
                // store the data in the object
                emp.EmployeeId = Convert.ToInt32(sqlReader["EmployeeId"]);
                emp.FirstName = sqlReader["FirstName"].ToString();
                emp.LastName = sqlReader["LastName"].ToString();
                emp.JobTitle = sqlReader["JobTitle"].ToString();
            }
            else // not found
            {
                emp = null;
            }

            return emp;
        }

        //Overloaded methods
        public static List<Employee> SearchRecord(string name)
        {
            List<Employee> listEmp = new List<Employee>();
            //Employee emp = new Employee();
            SqlConnection conn = UtilityDB.ConnectDB();
            conn = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand();
            cmdSelect.CommandText = "SELECT * FROM Employees " +
                                    "WHERE FirstName = @FirstName " +
                                    " Or LastName = @LastName ";
            cmdSelect.Parameters.AddWithValue("@FirstName", name);
            cmdSelect.Parameters.AddWithValue("@LastName", name);
            cmdSelect.Connection = conn;
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();
            Employee emp;

            while (sqlReader.Read())
            {
                emp = new Employee();
                emp.EmployeeId = Convert.ToInt32(sqlReader["EmployeeId"]);
                emp.FirstName = sqlReader["FirstName"].ToString();
                emp.LastName = sqlReader["LastName"].ToString();
                emp.JobTitle = sqlReader["JobTitle"].ToString();
                listEmp.Add(emp);

            }
            return listEmp;
        }

        public static List<Employee> ListAllRecords()
        {
            List<Employee> listEmp = new List<Employee>();
            //Employee emp = new Employee();
            SqlConnection conn = UtilityDB.ConnectDB();
            conn = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand();
            cmdSelect.CommandText = "SELECT * FROM Employees ";
            cmdSelect.Connection = conn;
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();
            Employee emp;

            while (sqlReader.Read())
            {
                emp = new Employee();
                emp.EmployeeId = Convert.ToInt32(sqlReader["EmployeeId"]);
                emp.FirstName = sqlReader["FirstName"].ToString();
                emp.LastName = sqlReader["LastName"].ToString();
                emp.JobTitle = sqlReader["JobTitle"].ToString();
                listEmp.Add(emp);

            }
            return listEmp;
        }

        // Update a Record
        // 1. Search the record by Employee Id
        //    If found, display it; if not display error
        // 2. Change the data as required
        // 3. Update record

        public static void UpdateRecord(Employee emp)
        {
            // Step 1: Connect the database
            SqlConnection conn = UtilityDB.ConnectDB();
            // Step 2: Perform UPDATE Statement
            SqlCommand cmdUpdate = new SqlCommand();
            cmdUpdate.CommandText = "UPDATE Employees " +
                                    "SET    EmployeeId = @EmployeeId," +
                                    "       FirstName = @FirstName," +
                                    "       LastName = @LastName," +
                                    "       JobTitle = @JobTitle " +
                                    "WHERE  EmployeeId = @EmployeeId";
            cmdUpdate.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
            cmdUpdate.Parameters.AddWithValue("@FirstName", emp.FirstName);
            cmdUpdate.Parameters.AddWithValue("@LastName", emp.LastName);
            cmdUpdate.Parameters.AddWithValue("@JobTitle", emp.JobTitle);
            cmdUpdate.Connection = conn;
            cmdUpdate.ExecuteNonQuery();
            //Step 3: Close the DB connection
            conn.Close();
        }

        public static void DeleteRecord(int empId)
        {
            // Step 1: Connect the database
            SqlConnection conn = UtilityDB.ConnectDB();
            // Step 2: Perform DELETE Statement
            SqlCommand cmdDelete = new SqlCommand();
            cmdDelete.CommandText = "DELETE FROM Employees " +
                                    "WHERE EmployeeId= @EmployeeId";
            cmdDelete.Parameters.AddWithValue("@EmployeeId", empId);
            cmdDelete.Connection = conn;
            cmdDelete.ExecuteNonQuery();
            //Step 3: Close the DB connection
            conn.Close();
        }
        public static void DeleteRecord(Employee emp)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connDB;
            cmd.CommandText = ("DELETE FROM Employees "
                + "WHERE EmployeeId = (@EmployeeId)");
            cmd.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
            cmd.ExecuteNonQuery();
            connDB.Close();
        }

    }


}
