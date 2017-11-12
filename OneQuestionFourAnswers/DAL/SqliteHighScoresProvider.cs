﻿using LibraryClass;
using System.Collections.Generic;
using System.Data.SQLite;


namespace DAL
{
    public class SqliteHighScoresProvider : SqliteAdapter, IHighScoresProvider
    {
        public SqliteHighScoresProvider()
        {
            const string sql = "CREATE TABLE IF NOT EXISTS high_scores " +
                               "(name VARCHAR(15), score INT)";
            var command = new SQLiteCommand(sql, Connection);
            command.ExecuteNonQuery();
        }

        public bool Add(Record item)
        {
            try
            {
                var sql = "INSERT INTO high_scores (name, score) " +  $"values ('{item.Name}', {item.Score})";
                var command = new SQLiteCommand(sql, Connection);
                command.ExecuteNonQuery();
                sql = "SELECT COUNT(*) FROM high_scores";
                command = new SQLiteCommand(sql, Connection);
                var count = (long) command.ExecuteScalar();
                if (count <= 3)
                {
                    return true;
                }
                sql = "SELECT score FROM high_scores ORDER BY score ASC LIMIT 1";
                command = new SQLiteCommand(sql, Connection);
                var lowest_score = (int) command.ExecuteScalar();
                sql = $"DELETE FROM high_scores WHERE rowid = (SELECT rowid FROM high_scores WHERE score = {lowest_score} LIMIT 1)";
                command = new SQLiteCommand(sql, Connection);
                command.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public RecordsTable GetTable()
        {
            const string sql = "SELECT * FROM high_scores ORDER BY score DESC";
            var command = new SQLiteCommand(sql, Connection);
            var reader = command.ExecuteReader();
            var results = new List<Record>();
            while (reader.Read())
            {
                results.Add(new Record((string)reader["name"], (int)reader["score"]));
            }
            return new RecordsTable(results);
        }
    }
}