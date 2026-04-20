using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Security.Cryptography.Xml;
using Npgsql;
using System.ComponentModel.DataAnnotations;
using modul2baru.Controllers;
using modul2baru.Helpers;
using modul2baru.Models;

namespace modul2baru.Models
{
    public class PersonContext
    {

        private string __constr;
        private string __ErrorMsg;

        public PersonContext(string pConstr)
        {
            __constr = pConstr; 
        }
        public List<Person> ListPerson()
        {
            List<Person> list1 = new List<Person>();
            string query = string.Format(@"Select nama
                    from person where id_status = 1;");
            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list1.Add(new Person()
                    {
                        nama = reader["nama"].ToString(),
                    });
                }

                cmd.Dispose();
                db.CloseConnection();
            }

            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }

            return list1;
        }

        public Person GetPersonById(int id)
        {
            Person person = null;

            string query = @"SELECT p.id_person, p.nama, p.alamat, p.email, p.password,
                            p.id_role_person, r.nama_role,
                            p.id_status, s.status
                     FROM person p
                     JOIN role_person r ON p.id_role_person = r.id_role_person
                     JOIN status s ON p.id_status = s.id_status
                     WHERE p.id_person = @id AND p.id_status = 1";

            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);

                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    person = new Person()
                    {
                        id_person = Convert.ToInt32(reader["id_person"]),
                        nama = reader["nama"].ToString(),
                        alamat = reader["alamat"].ToString(),
                        email = reader["email"].ToString(),
                        password = reader["password"].ToString(),
                        id_role_person = Convert.ToInt32(reader["id_role_person"]),

                        // tambahan (opsional kalau mau tampil detail)
                        nama_role = reader["nama_role"].ToString()
                    };
                }

                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }

            return person;
        }

        public void CreatePerson(Person person)
        {
            string query = @"INSERT INTO person 
            (id_person, nama, alamat, email, password, id_role_person) 
            VALUES (@id_person, @nama, @alamat, @email, @password, @id_role_person)";

            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);

                cmd.Parameters.AddWithValue("@id_person", person.id_person);
                cmd.Parameters.AddWithValue("@nama", person.nama);
                cmd.Parameters.AddWithValue("@alamat", person.alamat);
                cmd.Parameters.AddWithValue("@email", person.email);
                cmd.Parameters.AddWithValue("@password", person.password);
                cmd.Parameters.AddWithValue("@id_role_person", person.id_role_person);

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int UpdatePerson(int id, Person person)
        {
            string query = @"UPDATE person 
                    SET nama = @nama, 
                        alamat = @alamat, 
                        email = @email,
                        password = @password,
                        id_role_person = @id_role_person
                    WHERE id_person = @id_person";

            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);

                cmd.Parameters.AddWithValue("@id_person", id);
                cmd.Parameters.AddWithValue("@nama", person.nama);
                cmd.Parameters.AddWithValue("@alamat", person.alamat);
                cmd.Parameters.AddWithValue("@email", person.email);
                cmd.Parameters.AddWithValue("@password", person.password);
                cmd.Parameters.AddWithValue("@id_role_person", person.id_role_person);

                int rows = cmd.ExecuteNonQuery();

                cmd.Dispose();
                db.CloseConnection();

                return rows;
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
                return 0;
            }
        }

        public int DeletePerson(int id_person)
        {
            string query = @"UPDATE person 
                     SET id_status = 2 
                     WHERE id_person = @id_person";

            SqlDBHelper db = new SqlDBHelper(this.__constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id_person", id_person);

                int rows = cmd.ExecuteNonQuery();

                cmd.Dispose();
                db.CloseConnection();

                return rows;
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
                return 0;
            }
        }
    }
}