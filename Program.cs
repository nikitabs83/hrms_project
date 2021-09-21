using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ADOExample2
{
    class DeptDAL
    {
//added to git
        public SqlDataReader GetDepartment()
        {
            SqlDataReader reader = null;
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Server=.;Database=HRMSDB;trusted_connection=true";
                con.Open();

                SqlCommand cmd = new SqlCommand("GetDeptData", con);
                cmd.CommandType = CommandType.StoredProcedure;

                reader = cmd.ExecuteReader();
                
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Exception " + ex.Message);
            }
            return reader;
        }

        public SqlDataReader GetDepartmentUsingDno(int deptno)
        {
            SqlDataReader reader = null;
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Server=.;Database=HRMSDB;trusted_connection=true";
                con.Open();

                SqlCommand cmd = new SqlCommand("Get_Dept_Using_Deptno", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("dno", deptno);
                cmd.Parameters.Add(param);

                reader = cmd.ExecuteReader();
                
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Exception " + ex.Message);
            }
            return reader;
        }

        public int InsertDepartment(int deptno,string dname,string location,string dhead)
        {
            int no = 0;
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Server=.;Database=HRMSDB;trusted_connection=true";
                con.Open();

                SqlCommand cmd = new SqlCommand("Insert_Department", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("dno", deptno);
                cmd.Parameters.AddWithValue("dnm", dname);
                cmd.Parameters.AddWithValue("location", location);
                cmd.Parameters.AddWithValue("dhead", dhead);

                no = cmd.ExecuteNonQuery();
                con.Close();
                
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Exception " + ex.Message);
            }
            return no;
        }

        public int UpdateDepartment(int deptno, string dname, string location, string dhead)
        {
            int no = 0;
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Server=.;Database=HRMSDB;trusted_connection=true";
                con.Open();

                SqlCommand cmd = new SqlCommand("Update_Department", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("dno", deptno);
                cmd.Parameters.AddWithValue("dnm", dname);
                cmd.Parameters.AddWithValue("location", location);
                cmd.Parameters.AddWithValue("dhead", dhead);

                no = cmd.ExecuteNonQuery();

                con.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Exception " + ex.Message);
            }
            return no;
        }

        public int DeleteDepartment(int deptno)
        {
            int no = 0;
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Server=.;Database=HRMSDB;trusted_connection=true";
                con.Open();

                SqlCommand cmd = new SqlCommand("Delete_Department", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("dno", deptno);
                no = cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Exception " + ex.Message);
            }
            return no;
        }
    }

    class Department
    {
        DeptDAL dal = new DeptDAL();
        public int Deptno
        {
            get;
            set;
        }

        public string Dname
        {
            get;
            set;
        }

        public string location
        {
            get;
            set;
        }

        public string dhead
        {
            get;
            set;
        }

        public void PrintDepartmentData()
        {
            SqlDataReader reader = dal.GetDepartment();
            Console.WriteLine("Deptno\tDname\tLocation\tDepthead");
            while (reader.Read())
            {
                Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3]);
            }
        }

        public void PrintDepartment()
        {
            SqlDataReader reader = dal.GetDepartmentUsingDno(Deptno);
            Console.WriteLine("Deptno\tDname\tLocation\tDepthead");
            while (reader.Read())
            {
                Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3]);
            }
        }

        public void InsertDepartment()
        {
            int no = dal.InsertDepartment(Deptno, Dname, location, dhead);
            if (no > 0)
            {
                Console.WriteLine("Data Inserted Successfully");
            }
        }

        public void UpdateDepartment()
        {
            int no = dal.UpdateDepartment(Deptno, Dname, location, dhead);
            if (no > 0)
            {
                Console.WriteLine("Data Updated Successfully");
            }
        }

        public void DeleteDepartment()
        {
            int no = dal.DeleteDepartment(Deptno);
            if (no > 0)
            {
                Console.WriteLine("Data Deleted Successfully");
            }
        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            int choice;
            char ch;

            do
            {
                Console.WriteLine("Menu");
                Console.WriteLine("1.Print All Departments");
                Console.WriteLine("2.Print Department based on deptno");
                Console.WriteLine("3.Insert Departments");
                Console.WriteLine("4.Update Departments");
                Console.WriteLine("5.Delete Departments");
                Console.WriteLine("Enter your choice");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1 :
                        Department d = new Department();
                        d.PrintDepartmentData();
                        break;

                    case 2 :
                        Department d1= new Department();
                        Console.WriteLine("Enter department no to view details");
                        d1.Deptno = Convert.ToInt32(Console.ReadLine());
                        d1.PrintDepartment();
                        break;

                    case 3:
                        Department d2 = new Department();
                        Console.WriteLine("Enter Department Details to Enter Deptno,dname,location,department head");
                        d2.Deptno = Convert.ToInt32(Console.ReadLine());
                        d2.Dname = Console.ReadLine();
                        d2.location = Console.ReadLine();
                        d2.dhead = Console.ReadLine();
                        d2.InsertDepartment();
                        break;

                    case 4:
                        Department d3 = new Department();
                        Console.WriteLine("Enter Department Details to Update Deptno,dname,location,department head");
                        d3.Deptno = Convert.ToInt32(Console.ReadLine());
                        d3.Dname = Console.ReadLine();
                        d3.location = Console.ReadLine();
                        d3.dhead = Console.ReadLine();
                        d3.UpdateDepartment();
                        break;

                    case 5:
                        Department d4 = new Department();
                        Console.WriteLine("Enter Department no to delete");
                        d4.Deptno = Convert.ToInt32(Console.ReadLine());
                        d4.DeleteDepartment();
                        break;

                    default:
                        Console.WriteLine("Invalid Case");
                        break;
                }

                Console.WriteLine("Enter y r Y to continue");
                ch = Convert.ToChar(Console.ReadLine());

            }
            while (ch == 'Y' || ch == 'y');
            Console.ReadLine();
        }
    }
}
