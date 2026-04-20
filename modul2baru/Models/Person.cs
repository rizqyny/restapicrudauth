using System.Runtime.InteropServices;

namespace modul2baru.Models
{
    public class Person
    {
        public int id_person { get; set; }
        public string nama { get; set; }
        public string alamat { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int id_role_person { get; set; }

        public string nama_role { get; set; }
    }
}