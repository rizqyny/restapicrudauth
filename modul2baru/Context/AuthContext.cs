using System;
using modul2baru.Helpers;
using modul2baru.Models;
using Npgsql;

namespace modul2baru.Context
{
    public class AuthContext
    {
        private string __constr;
        private string __ErrorMsg;

        public AuthContext(string pCOnstr)
        {
            __constr = pCOnstr;
        }

        public Person Authenticate(string email, string password, out string roleName)
        {
            Person authenticatedUser = null;
            roleName = string.Empty;

            string query = @"
                SELECT p.id_person, p.nama, p.alamat, p.email, r.nama_role 
                FROM person p
                LEFT JOIN role_person r ON p.id_role_person = r.id_role_person
                WHERE p.email = @email AND p.password = @password";

            SqlDBHelper db = new SqlDBHelper(this.__constr);
            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);

                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    authenticatedUser = new Person
                    {
                        id_person = reader.GetInt32(0),
                        nama = reader.GetString(1),
                        alamat = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        email = reader.GetString(3)
                    };

                    if (!reader.IsDBNull(4))
                    {
                        roleName = reader.GetString(4);
                    }
                }
                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                __ErrorMsg = ex.Message;
            }

            return authenticatedUser;
        }
    }
}