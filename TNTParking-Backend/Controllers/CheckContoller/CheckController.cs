using Context.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data.Common;
using System.Net.NetworkInformation;

namespace TNTGreenSpacesRegister_Backend.Controllers.CheckContoller
{
    [EnableCors("ExternalCorsPolicy")]
    [ApiController]
    [Route("")]
    public class CheckController : Controller
    {
        TntparkingContext _db;

        public CheckController(TntparkingContext db)
        {
            _db = db;
        }

        [HttpGet(Name = "check")]
        public string Get()
        {
            var list = new List<dynamic>();




            list.Add("date:" + DateTime.Now);
            list.Add("NET:");

            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                list.Add("Name: " + netInterface.Name);
                list.Add("Description: " + netInterface.Description);
                list.Add("Addresses: ");

                IPInterfaceProperties ipProps = netInterface.GetIPProperties();

                foreach (UnicastIPAddressInformation addr in ipProps.UnicastAddresses)
                {
                    list.Add(" " + addr.Address.ToString());
                }

                list.Add("");
            }

            list.Add("SQL:");
            try
            {

                list.Add("ProviderName " + _db.Database.ProviderName);
                list.Add("SQL can Connect: " + _db.Database.CanConnect());

                DbConnection conn = _db.Database.GetDbConnection();
                string maskedCs = MaskConnectionString(conn.ConnectionString);
                list.Add("DbConnection.Database: " + conn.Database);
                list.Add("DbConnection.DataSource: " + conn.DataSource);
                list.Add("ConnectionString (masked): " + maskedCs);

                if (conn is NpgsqlConnection npg)
                {
                    var csb = new NpgsqlConnectionStringBuilder(npg.ConnectionString);
                    list.Add($"Npgsql -> Host: {csb.Host}; Port: {csb.Port}; Database: {csb.Database}; Username: {csb.Username}");
                }

                var count = _db.ParkingAreas.Select(x => x.Id).Count();

                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                select current_database() as db,
                       current_schema()   as schema,
                       current_setting('search_path') as search_path;";
                    using var r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        list.Add("current_database(): " + r["db"]);
                        list.Add("current_schema(): " + r["schema"]);
                        list.Add("search_path: " + r["search_path"]);
                    }
                }

                using (var cmd2 = conn.CreateCommand())
                {
                    cmd2.CommandText = @"
                select table_schema, table_name
                from information_schema.tables
                where table_name ilike 'gsr_greenspaces'
                order by table_schema;";
                    using var r2 = cmd2.ExecuteReader();
                    var tables = new List<string>();
                    while (r2.Read())
                        tables.Add($"{r2["table_schema"]}.{r2["table_name"]}");
                    list.Add("Tables found for gsr_greenspaces: " + (tables.Count == 0 ? "<none>" : string.Join(", ", tables)));
                }

                list.Add("Count green spaces " + count);
                list.Add("OK");

                list.Add("Pentru test " + _db);

            }
            catch (Exception error)
            {
                list.Add("ERROR:" + error.Message + "## " + error.StackTrace);
            }
            return String.Join("\n", list);

        }

        private static string MaskConnectionString(string cs)
        {
            var builder = new DbConnectionStringBuilder { ConnectionString = cs };
            void mask(string key)
            {
                if (builder.ContainsKey(key)) builder[key] = "*****";
            }
            mask("Password"); mask("Pwd"); mask("User Id"); mask("Username");
            return builder.ConnectionString;
        }
    }
}
