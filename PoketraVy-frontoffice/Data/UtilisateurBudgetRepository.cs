using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using PoketraVy_frontoffice.Models;

namespace PoketraVy_frontoffice.Data
{
    public class UtilisateurBudgetRepository
    {
        private readonly string _connectionString;

        public UtilisateurBudgetRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get all UtilisateurBudget records
        public IEnumerable<UtilisateurBudget> GetAll()
        {
            var utilisateurBudgets = new List<UtilisateurBudget>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("SELECT * FROM utilisateurbudget", connection);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            utilisateurBudgets.Add(new UtilisateurBudget
                            {
                                ID = (int)reader["ID"],
                                IdUtilisateur = (int)reader["IdUtilisateur"],
                                IdBudget = (int)reader["IdBudget"],
                                Montant = reader["Montant"] as double?,
                                Remarque = reader["Remarque"].ToString()
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

            return utilisateurBudgets;
        }

        // Get a single UtilisateurBudget by ID
        public UtilisateurBudget GetById(int id)
        {
            UtilisateurBudget utilisateurBudget = null;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("SELECT * FROM utilisateurbudget WHERE ID = @ID", connection);
                    command.Parameters.AddWithValue("@ID", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            utilisateurBudget = new UtilisateurBudget
                            {
                                ID = (int)reader["ID"],
                                IdUtilisateur = (int)reader["IdUtilisateur"],
                                IdBudget = (int)reader["IdBudget"],
                                Montant = reader["Montant"] as double?,
                                Remarque = reader["Remarque"].ToString()
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

            return utilisateurBudget;
        }

        // Add a new UtilisateurBudget
        public void Add(UtilisateurBudget utilisateurBudget)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand(
                        "INSERT INTO utilisateurbudget (IdUtilisateur, IdBudget, Montant, Remarque) " +
                        "VALUES (@IdUtilisateur, @IdBudget, @Montant, @Remarque)",
                        connection);

                    command.Parameters.AddWithValue("@IdUtilisateur", utilisateurBudget.IdUtilisateur);
                    command.Parameters.AddWithValue("@IdBudget", utilisateurBudget.IdBudget);
                    command.Parameters.AddWithValue("@Montant", (object)utilisateurBudget.Montant ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Remarque", (object)utilisateurBudget.Remarque ?? DBNull.Value);

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

        // Update an existing UtilisateurBudget
        public void Update(UtilisateurBudget utilisateurBudget)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand(
                        "UPDATE utilisateurbudget SET IdUtilisateur = @IdUtilisateur, IdBudget = @IdBudget, Montant = @Montant, Remarque = @Remarque " +
                        "WHERE ID = @ID",
                        connection);

                    command.Parameters.AddWithValue("@ID", utilisateurBudget.ID);
                    command.Parameters.AddWithValue("@IdUtilisateur", utilisateurBudget.IdUtilisateur);
                    command.Parameters.AddWithValue("@IdBudget", utilisateurBudget.IdBudget);
                    command.Parameters.AddWithValue("@Montant", (object)utilisateurBudget.Montant ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Remarque", (object)utilisateurBudget.Remarque ?? DBNull.Value);

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

        // Delete a UtilisateurBudget by ID
        public void Delete(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("DELETE FROM utilisateurbudget WHERE ID = @ID", connection);
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
