using System.Data;
using System.IO;
using System.Net;
using System.Text;

namespace testewappa.Integration
{
    public class GoogleAddress : IGoogleAddress
    {
        public string[] GetCoordinates(string address)
        {
            string[] coordenadas = new string[2];
            string url = "http://maps.google.com/maps/api/geocode/xml?address=" + address+ "&sensor=false";
            WebRequest request = WebRequest.Create(url);
            DataTable dtCoordenadas = new DataTable();
            using (WebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    DataSet dsResult = new DataSet();
                    dsResult.ReadXml(reader);
                    dtCoordenadas.Columns.AddRange(new DataColumn[4] { new DataColumn("Id", typeof(int)),
                            new DataColumn("Address", typeof(string)),
                            new DataColumn("Latitude",typeof(string)),
                            new DataColumn("Longitude",typeof(string)) });
                    foreach (DataRow row in dsResult.Tables["result"].Rows)
                    {
                        string geometry_id = dsResult.Tables["geometry"].Select("result_id = " + row["result_id"].ToString())[0]["geometry_id"].ToString();
                        DataRow location = dsResult.Tables["location"].Select("geometry_id = " + geometry_id)[0];
                        dtCoordenadas.Rows.Add(row["result_id"], row["formatted_address"], location["lat"], location["lng"]);
                    }
                }
                coordenadas[0] = dtCoordenadas.Rows[0][1].ToString();
                coordenadas[0] = dtCoordenadas.Rows[0][2].ToString();
                return coordenadas;
            }
        }
    }
}