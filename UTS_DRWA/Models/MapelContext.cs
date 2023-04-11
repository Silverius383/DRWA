using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace UTS_DRWA.Models
{
    public class MapelContext : DbContext
    {
        public MapelContext(DbContextOptions<MapelContext> options) : base(options)
        {
        }
        public string ConnectionString { get; set; }
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection("Server = localhost; Database = webapi; Uid = root; Pwd =");
        }

        public List<MapelItem> GetAllMapel()
        {
            List<MapelItem> list = new List<MapelItem>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM mapel", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new MapelItem()
                        {
                            id_mapel = reader.GetInt32("id_mapel"),
                            nama_mapel = reader.GetString("nama_mapel"),
                            deskripsi = reader.GetString("deskripsi")
                        });
                    }
                }
            }
            return list;
        }

        public List<MapelItem> GetMapel(string id)
        {
            List<MapelItem> list = new List<MapelItem>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM mapel WHERE id_mapel = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new MapelItem()
                        {
                            id_mapel = reader.GetInt32("id_mapel"),
                            nama_mapel = reader.GetString("nama_mapel"),
                            deskripsi = reader.GetString("deskripsi")
                        });
                    }
                }
            }
            return list;
        }

        public MapelItem AddMapel(MapelItem KI)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO mapel (nama_mapel, deskripsi) VALUES (@nama_mapel, @deskripsi)", conn);
                cmd.Parameters.AddWithValue("@nama_mapel", KI.nama_mapel);
                cmd.Parameters.AddWithValue("@deskripsi", KI.deskripsi);

                cmd.ExecuteReader();
            }
            return KI;
        }
    }
}
