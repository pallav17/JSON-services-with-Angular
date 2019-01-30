using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using System.Text;
using ChoETL;
using System.Web.Script.Serialization;

namespace WebAppService
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]


    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public Boolean InsertSessionData(int id, string name, string dept, int age, int salary, string location)
        {

            string connectionstring = @"Data Source = P01156006; Initial Catalog = master; Integrated Security = True";
            using (SqlConnection con = new SqlConnection(connectionstring))

            {
                con.Open();

                SqlCommand cmd = new SqlCommand("insert into employee(id,name,dept,age,salary,location)values(@id,@name,@dept,@age,@salary,@location)", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@dept", dept);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@salary", salary);
                cmd.Parameters.AddWithValue("@location", location);
                int i = 0;
                i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        [WebMethod]
        public void getEmployee()
        {
            
            StringBuilder sb = new StringBuilder();

            string connectionstring = @"Data Source = P01156006; Initial Catalog = master; Integrated Security = SSPI";
            //  string connectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True";

            using (var conn = new SqlConnection(connectionstring))
            //using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connectstr"].ToString())) ;
            {
                Context.Response.ContentType = "application/json";
                conn.Open();
                //var comm = new SqlCommand("SELECT * FROM employee where id = 2", conn);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "AngularDB";
                SqlDataReader rdr = cmd.ExecuteReader();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                // SqlDataAdapter adap = new SqlDataAdapter(rdr);

                DataTable dt = new DataTable("employee");
                //rdr.Fill(dt);

                using (var parser = new ChoJSONWriter(sb))
                   
                    parser.Write(rdr);
                String k = sb.ToString();

                Context.Response.Write(k);
                // return sb.ToString();
                //this.Context.Response.ContentType = "application/json; charset=utf-8";
                //this.Context.Response.Write(serializer.Serialize(k));
                

            }

            //this.Context.Response.ContentType = "application/json; charset=utf-8";
            //   this.Context.Response.Write(json);
            //    string sql = "SELECT * FROM employee";
            //    SqlDataAdapter da = new SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings["Connectstr"].ToString());
            //    DataSet ds = new DataSet();

            //    DataTable table = new DataTable();


            //    da.Fill(ds);
            //    var empList = ds.Tables[0].AsEnumerable().Select(dataRow => new Employee { Name = dataRow.Field<string>("Name") }).ToList()
            //    return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
            //}

            



           


            }
        }
}
