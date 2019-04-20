using System;
using System.IO;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace InterfacesCalico
{
    class Program
    {
        public static SqlConnection openConnection()
        {
            ConnectionStringSettings mySQLConSettings = ConfigurationManager.ConnectionStrings["database"];
            SqlConnection mySQLConn = new SqlConnection(mySQLConSettings.ConnectionString);
            mySQLConn.Open();
            return mySQLConn;
        }

        static void Main(string[] args)
        {
            SqlConnection mySQLConn = openConnection();

            SqlCommand mySQLCommand = new SqlCommand("select * from HIJOS", mySQLConn);
            SqlDataReader mySQLDataReader = mySQLCommand.ExecuteReader();
            mySQLDataReader.Read();
            String valor = mySQLDataReader[0].ToString();
            System.Console.WriteLine("Valor: " + valor);
            mySQLConn.Close();


            String fecha = "2011804";
            // HttpWebRequest request = WebRequest.Create("http://localhost:8080/OSDEPYM/obtenerClientes?fecha=" + fecha) as HttpWebRequest;
            // http://localhost:8080/calico/rest/message/"texto"
            HttpWebRequest request = WebRequest.Create("http://localhost:8080/calico/rest/message/" + fecha) as HttpWebRequest;
            request.Method = "GET";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string body = reader.ReadToEnd();

            System.Console.WriteLine("Datos recibidos: " + body);
            /* Autenticacion */
            /* string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("usuario:clave"));
            request.Headers.Add("Authorization", "Basic " + credentials);*/

        }
    }
}
