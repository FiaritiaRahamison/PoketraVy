using PoketraVy_frontoffice.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PoketraVy_frontoffice.Data
{
    public class MouvementRepository
    {
        private readonly string _connectionString;

        public MouvementRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get all Mouvement records
        public IEnumerable<Mouvement> GetAll()
        {
            var mouvements = new List<Mouvement>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("SELECT * FROM mouvement", connection);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mouvements.Add(new Mouvement
                            {
                                ID = (int)reader["ID"],
                                IdCategorieUtilisateurBudget = (int)reader["IdCategorieUtilisateurBudget"],
                                Designation = reader["Designation"].ToString(),
                                montant = (double)reader["Montant"],
                                daty = (DateTime)reader["Daty"]
                            });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw;
            }

            return mouvements;
        }

        // Get a single mouvement by ID
        public Mouvement GetById(int id)
        {
            Mouvement mouvement = null;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("SELECT * FROM mouvement WHERE ID = @ID", connection);
                    command.Parameters.AddWithValue("@ID", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            mouvement = new Mouvement
                            {
                                ID = (int)reader["ID"],
                                IdCategorieUtilisateurBudget = (int)reader["IdCategorieUtilisateurBudget"],
                                Designation = reader["Designation"].ToString(),
                                montant = (double)reader["Montant"],
                                daty = (DateTime)reader["Daty"]
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw;
            }

            return mouvement;
        }

        // Add a new mouvement
        public void Add(Mouvement mouvement)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand(
                        "INSERT INTO mouvement (IdCategorieUtilisateurBudget, Designation, montant, daty) " +
                        "VALUES (@IdCategorieUtilisateurBudget, @Designation, @montant, @daty)",
                        connection);

                    command.Parameters.AddWithValue("@IdCategorieUtilisateurBudget", mouvement.IdCategorieUtilisateurBudget);
                    command.Parameters.AddWithValue("@Designation", mouvement.Designation);
                    command.Parameters.AddWithValue("@montant", (double)mouvement.montant);
                    command.Parameters.AddWithValue("@daty", (DateTime)mouvement.daty);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw;
            }
        }

        // Update an existing mouvement
        public void Update(Mouvement mouvement)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand(
                        "UPDATE mouvement SET IdCategorieUtilisateurBudget = @IdCategorieUtilisateurBudget, Designation = @Designation, montant = @montant, daty = @daty " +
                        "WHERE ID = @ID",
                        connection);

                    command.Parameters.AddWithValue("@ID", mouvement.ID);
                    command.Parameters.AddWithValue("@IdCategorieUtilisateurBudget", mouvement.IdCategorieUtilisateurBudget);
                    command.Parameters.AddWithValue("@Designation", mouvement.Designation);
                    command.Parameters.AddWithValue("@montant", (double)mouvement.montant);
                    command.Parameters.AddWithValue("@daty", (DateTime)mouvement.daty);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw;
            }
        }

        // Delete a mouvement by ID
        public void Delete(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("DELETE FROM mouvement WHERE ID = @ID", connection);
                    command.Parameters.AddWithValue("@ID", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw;
            }
        }

        // Méthode pour obtenir les CategorieUtilisateurBudget par UtilisateurBudget ID
        public IEnumerable<Mouvement> GetByIdUser(int IdUser)
        {
            var mouvements = new List<Mouvement>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                String query = @"select m.* from mouvement m  join categorieutilisateurbudget  cub 
                      on m.IdCategorieUtilisateurBudget = cub.ID
                      join utilisateurbudget ub on ub.ID = cub.IdUtilisateurBudget
                      where ub.IdUtilisateur = @IdUser";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdUser", IdUser);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    mouvements.Add(new Mouvement
                    {
                        ID = (int)reader["ID"],
                        IdCategorieUtilisateurBudget = (int)reader["IdCategorieUtilisateurBudget"],
                        Designation = reader["Designation"].ToString(),
                        montant = (double)reader["montant"],
                        daty = (DateTime)reader["daty"]
                    });
                }
            }

            return mouvements;
        }
    }
}
