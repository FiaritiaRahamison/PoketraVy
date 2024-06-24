using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using PoketraVy_frontoffice.Models;

namespace PoketraVy_frontoffice.Data
{
    public class CategorieUtilisateurBudgetRepository
    {
        private readonly string _connectionString;

        public CategorieUtilisateurBudgetRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get all CategorieUtilisateurBudget records
        public IEnumerable<CategorieUtilisateurBudget> GetAll()
        {
            var categorieUtilisateurBudgets = new List<CategorieUtilisateurBudget>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("SELECT * FROM categorieutilisateurbudget", connection);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categorieUtilisateurBudgets.Add(new CategorieUtilisateurBudget
                            {
                                ID = (int)reader["ID"],
                                IdUtilisateurBudget = (int)reader["IdUtilisateurBudget"],
                                Designation = reader["Designation"].ToString(),
                                Categorie = reader["Categorie"] as Categorie?,
                                Montant = (double)reader["Montant"],
                                Daty = (DateTime)reader["Daty"]
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

            return categorieUtilisateurBudgets;
        }

        // Get a single CategorieUtilisateurBudget by ID
        public CategorieUtilisateurBudget GetById(int id)
        {
            CategorieUtilisateurBudget categorieUtilisateurBudget = null;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("SELECT * FROM categorieutilisateurbudget WHERE ID = @ID", connection);
                    command.Parameters.AddWithValue("@ID", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            categorieUtilisateurBudget = new CategorieUtilisateurBudget
                            {
                                ID = (int)reader["ID"],
                                IdUtilisateurBudget = (int)reader["IdUtilisateurBudget"],
                                Designation = reader["Designation"].ToString(),
                                Categorie = reader["Categorie"] as Categorie?,
                                Montant = (double)reader["Montant"],
                                Daty = (DateTime)reader["Daty"]
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

            return categorieUtilisateurBudget;
        }

        // Add a new CategorieUtilisateurBudget
        public void Add(CategorieUtilisateurBudget categorieUtilisateurBudget)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand(
                        "INSERT INTO categorieutilisateurbudget (IdUtilisateurBudget, Designation, Categorie, Montant, Daty) " +
                        "VALUES (@IdUtilisateurBudget, @Designation, @Categorie, @Montant, @Daty)",
                        connection);

                    command.Parameters.AddWithValue("@IdUtilisateurBudget", categorieUtilisateurBudget.IdUtilisateurBudget);
                    command.Parameters.AddWithValue("@Designation", categorieUtilisateurBudget.Designation);
                    command.Parameters.AddWithValue("@Categorie", (object)categorieUtilisateurBudget.Categorie ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Montant", categorieUtilisateurBudget.Montant);
                    command.Parameters.AddWithValue("@Daty", categorieUtilisateurBudget.Daty);

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

        // Update an existing CategorieUtilisateurBudget
        public void Update(CategorieUtilisateurBudget categorieUtilisateurBudget)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand(
                        "UPDATE categorieutilisateurbudget SET IdUtilisateurBudget = @IdUtilisateurBudget, Designation = @Designation, Categorie = @Categorie, Montant = @Montant, Daty = @Daty " +
                        "WHERE ID = @ID",
                        connection);

                    command.Parameters.AddWithValue("@ID", categorieUtilisateurBudget.ID);
                    command.Parameters.AddWithValue("@IdUtilisateurBudget", categorieUtilisateurBudget.IdUtilisateurBudget);
                    command.Parameters.AddWithValue("@Designation", categorieUtilisateurBudget.Designation);
                    command.Parameters.AddWithValue("@Categorie", (object)categorieUtilisateurBudget.Categorie ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Montant", categorieUtilisateurBudget.Montant);
                    command.Parameters.AddWithValue("@Daty", categorieUtilisateurBudget.Daty);

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

        // Delete a CategorieUtilisateurBudget by ID
        public void Delete(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("DELETE FROM categorieutilisateurbudget WHERE ID = @ID", connection);
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
    }
}
