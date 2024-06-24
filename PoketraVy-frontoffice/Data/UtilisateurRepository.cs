using PoketraVy_frontoffice.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PoketraVy_frontoffice.Data
{
    public class UtilisateurRepository
    {
        private readonly string _connectionString;

        public UtilisateurRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Utilisateur> GetAllUtilisateurs()
        {
            var products = new List<Utilisateur>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM utilisateur", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new Utilisateur
                    {
                        ID = (int)reader["ID"],
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        Role = (bool)reader["Role"]
                    });
                }
            }

            return products;
        }

        public Utilisateur GetById(int id)
        {
            Utilisateur utilisateur = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM utilisateur WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        utilisateur = new Utilisateur
                        {
                            ID = (int)reader["ID"],
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            Role = (bool)reader["Role"]
                        };
                    }
                }
            }

            return utilisateur;
        }

        public void Add(Utilisateur utilisateur)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO utilisateur (Username, Password, Role) VALUES (@Username, @Password, @Role)", connection);
                command.Parameters.AddWithValue("@Username", utilisateur.Username);
                command.Parameters.AddWithValue("@Password", utilisateur.Password);
                command.Parameters.AddWithValue("@Role", utilisateur.Role);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Utilisateur utilisateur)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE utilisateur SET Username = @Username, Password = @Password, Role = @Role WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", utilisateur.ID);
                command.Parameters.AddWithValue("@Username", utilisateur.Username);
                command.Parameters.AddWithValue("@Password", utilisateur.Password);
                command.Parameters.AddWithValue("@Role", utilisateur.Role);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM utilisateur WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
