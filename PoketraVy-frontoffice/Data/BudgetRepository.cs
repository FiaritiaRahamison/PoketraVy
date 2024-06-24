using PoketraVy_frontoffice.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PoketraVy_frontoffice.Data
{
    public class BudgetRepository
    {
        private readonly string _connectionString;

        public BudgetRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Budget> GetAllBudgets()
        {
            var budgets = new List<Budget>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM budget", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        budgets.Add(new Budget
                        {
                            ID = (int)reader["ID"],
                            Montant = (double)reader["Montant"],
                            Daty = (DateTime)reader["Daty"],
                            DatyFinPrevisionnel = (DateTime)reader["DatyFinPrevisionnel"],
                            Etat = (bool)reader["Etat"],
                            Remarque = reader["Remarque"].ToString()
                        });
                    }
                }
            }

            return budgets;
        }

        public IEnumerable<Budget> GetActiveBudgetsByIdUser(int IdUser)
        {
            var budgets = new List<Budget>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT b.* FROM budget b " +
                    "WHERE b.DatyFinPrevisionnel >= GETDATE() " +
                    "AND EXISTS( " +
                    "SELECT * " +
                    "FROM utilisateurbudget ub JOIN budget b1 on b1.ID = ub.IdBudget " +
                    "WHERE ub.IdUtilisateur = @IdUser AND b1.ID = ub.IdBudget" +
                    ")", connection);
                command.Parameters.AddWithValue("@IdUser", IdUser);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        budgets.Add(new Budget
                        {
                            ID = (int)reader["ID"],
                            Montant = (double)reader["Montant"],
                            Daty = (DateTime)reader["Daty"],
                            DatyFinPrevisionnel = (DateTime)reader["DatyFinPrevisionnel"],
                            Etat = (bool)reader["Etat"],
                            Remarque = reader["Remarque"].ToString()
                        });
                    }
                }
            }

            return budgets;
        }

        public Budget GetById(int id)
        {
            Budget budget = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM budget WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        budget = new Budget
                        {
                            ID = (int)reader["ID"],
                            Montant = (double)reader["Montant"],
                            Daty = (DateTime)reader["Daty"],
                            DatyFinPrevisionnel = (DateTime)reader["DatyFinPrevisionnel"],
                            Etat = (bool)reader["Etat"],
                            Remarque = reader["Remarque"].ToString()
                        };
                    }
                }
            }

            return budget;
        }

        public void Add(Budget budget)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand(
                        "INSERT INTO budget (Montant, Daty, DatyFinPrevisionnel, Etat, Remarque) " +
                        "VALUES (@Montant, @Daty, @DatyFinPrevisionnel, @Etat, @Remarque)",
                        connection);

                    command.Parameters.AddWithValue("@Montant", budget.Montant);
                    command.Parameters.AddWithValue("@Daty", budget.Daty);
                    command.Parameters.AddWithValue("@DatyFinPrevisionnel", budget.DatyFinPrevisionnel);
                    command.Parameters.AddWithValue("@Etat", budget.Etat);

                    if (string.IsNullOrEmpty(budget.Remarque))
                    {
                        command.Parameters.AddWithValue("@Remarque", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Remarque", budget.Remarque);
                    }

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


        public void Update(Budget budget)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE budget SET Montant = @Montant, Daty = @Daty, DatyFinPrevisionnel = @DatyFinPrevisionnel, Etat = @Etat, Remarque = @Remarque WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", budget.ID);
                command.Parameters.AddWithValue("@Montant", budget.Montant);
                command.Parameters.AddWithValue("@Daty", budget.Daty);
                command.Parameters.AddWithValue("@DatyFinPrevisionnel", budget.DatyFinPrevisionnel);
                command.Parameters.AddWithValue("@Etat", budget.Etat);
                if (string.IsNullOrEmpty(budget.Remarque))
                {
                    command.Parameters.AddWithValue("@Remarque", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@Remarque", budget.Remarque);
                }
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM budget WHERE ID = @ID", connection);
                command.Parameters.AddWithValue("@ID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
