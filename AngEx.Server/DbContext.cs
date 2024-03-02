using System;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.ComponentModel;

namespace AngEx.Server
{
    public class DbContext
    {
        public string ConnectionString { get; set; }
        public DbContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
        public Task[] GetTasks()
        {
            List<Task> tasks = new List<Task>();
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Tasks", connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    Task t;
                    while (reader.Read())
                    {
                        t=new Task()
                        {
                            id = reader.GetInt32("id"),
                            name = reader.GetString("name"),
                            created=reader.GetDateTime("created"),
                            state = reader.GetInt32("state")
                        };
                        if (!reader.IsDBNull("parent")) t.parent=reader.GetInt32("parent");
                        if (!reader.IsDBNull("priority")) t.priority = reader.GetInt32("priority");
                        if (!reader.IsDBNull("tags")) t.tags = reader.GetString("tags");
                        if (!reader.IsDBNull("descr")) t.descr = reader.GetString("descr");
                        if (!reader.IsDBNull("deadl")) t.deadl = reader.GetDateTime("deadl");
                        tasks.Add(t);
                    }
                }
            }
            return tasks.ToArray();
        }
        public Task GetTask(int id)
        {
            Task t = null;
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Tasks where id="+id, connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    t = new Task()
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        created = reader.GetDateTime("created"),
                        state = reader.GetInt32("state")
                    };
                    if (!reader.IsDBNull("parent")) t.parent = reader.GetInt32("parent");
                    if (!reader.IsDBNull("priority")) t.priority = reader.GetInt32("priority");
                    if (!reader.IsDBNull("tags")) t.tags = reader.GetString("tags");
                    if (!reader.IsDBNull("descr")) t.descr = reader.GetString("descr");
                    if (!reader.IsDBNull("deadl")) t.deadl = reader.GetDateTime("deadl");
                }
            }
            return t;
        }
        public bool DeleteTask(int id)
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("delete from Tasks where id=" + id, connection);
                if (cmd.ExecuteNonQuery() < 1) return false;
            }
            return true;
        }
        public bool EditTask(int id, Task item)//not sure I really need this version
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"update Tasks set name='{item.name}'," +
                    $"created='{item.created.Value.Year}-{item.created.Value.Month}-{item.created.Value.Day} {item.created.Value.ToLongTimeString()}'," +
                    $"deadl={(item.deadl == null ? "NULL" : $"'{item.created.Value.Year}-{item.created.Value.Month}-{item.created.Value.Day} {item.created.Value.ToLongTimeString()}'")}," +
                    $" descr='{item.descr}',tags='{item.tags}',priority='{item.priority}',parent='{item.parent}',state='{item.state}' where Tasks.id={id}", connection);

                if (cmd.ExecuteNonQuery() < 1) return false;
            }
            return true;
        }
        public bool EditTask(Task item)
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"update Tasks set name='{item.name}'," +
    $"created='{item.created.Value.Year}-{item.created.Value.Month}-{item.created.Value.Day} {item.created.Value.ToLongTimeString()}'," +
    $"deadl={(item.deadl == null ? "NULL" : $"'{item.created.Value.Year}-{item.created.Value.Month}-{item.created.Value.Day} {item.created.Value.ToLongTimeString()}'")}," +
    $" descr='{item.descr}',tags='{item.tags}',priority='{item.priority}',parent='{item.parent}',state='{item.state}' where Tasks.id={item.id}", connection);
                if (cmd.ExecuteNonQuery() < 1) return false;
            }
            return true;
        }
        public long AddTask(Task item)
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("insert into Tasks (name,created,deadl,descr,tags,priority,parent,state) " +
                    $"values ('{item.name}','{item.created.Value.Year}-{item.created.Value.Month}-{item.created.Value.Day} {item.created.Value.ToLongTimeString()}'," +
                    $"{(item.deadl==null?"NULL": $"'{item.created.Value.Year}-{item.created.Value.Month}-{item.created.Value.Day} {item.created.Value.ToLongTimeString()}'")}," +
                    $"'{item.descr}','{item.tags}','{item.priority}','{item.parent}','{item.state}')", connection);
                
                if (cmd.ExecuteNonQuery()>0)
                    return cmd.LastInsertedId;
            }
            return -1;//if we fail
        }
    }
}
