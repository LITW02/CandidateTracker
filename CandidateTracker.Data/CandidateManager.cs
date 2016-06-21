using System.Collections.Generic;
using System.Data.SqlClient;

namespace CandidateTracker.Data
{
    public class CandidateManager
    {
        private string _connectionString;

        public CandidateManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Candidate> GetCandidates(Status status)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Candidates WHERE Status = @status";
                command.Parameters.AddWithValue("@status", status);
                connection.Open();
                var reader = command.ExecuteReader();
                List<Candidate> candidates = new List<Candidate>();
                while (reader.Read())
                {
                    Candidate c = new Candidate
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Email = (string)reader["Email"],
                        PhoneNumber = (string)reader["PhoneNumber"],
                        Notes = (string)reader["Notes"],
                        Status = (Status)reader["Status"]
                    };
                    candidates.Add(c);
                }

                return candidates;
            }
        }

        public void AddCandidate(Candidate candidate)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Candidates (FirstName, LastName, Email, PhoneNumber, Notes, Status) VALUES " +
                                      "(@firstName, @lastName, @email, @phone, @notes, @status)";
                command.Parameters.AddWithValue("@firstName", candidate.FirstName);
                command.Parameters.AddWithValue("@lastName", candidate.LastName);
                command.Parameters.AddWithValue("@email", candidate.Email);
                command.Parameters.AddWithValue("@phone", candidate.PhoneNumber);
                command.Parameters.AddWithValue("@notes", candidate.Notes);
                command.Parameters.AddWithValue("@status", Status.Pending);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateStatus(int candidateId, Status status)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE Candidates SET Status = @status WHERE Id = @id";
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@id", candidateId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public CandidateCounts GetCounts()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var counts = new CandidateCounts();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM Candidates WHERE Status = @status";
                command.Parameters.AddWithValue("@status", Status.Pending);
                connection.Open();
                counts.Pending = (int)command.ExecuteScalar();
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@status", Status.Confirmed);
                counts.Confirmed = (int)command.ExecuteScalar();
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@status", Status.Refused);
                counts.Refused = (int)command.ExecuteScalar();
                return counts;
            }
        }

        public Candidate GetCandidate(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Candidates WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                reader.Read();
                Candidate candidate = new Candidate
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Email = (string)reader["Email"],
                    PhoneNumber = (string)reader["PhoneNumber"],
                    Notes = (string)reader["Notes"],
                    Status = (Status)reader["Status"]
                };
                return candidate;
            }
        }
    }
}