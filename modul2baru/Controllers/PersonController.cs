using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using modul2baru.Models;

namespace modul2baru.Controllers
{
    public class PersonController : Controller
    {
        public string __constr;
        public PersonController(IConfiguration configuration)
        {
            __constr = configuration.GetConnectionString("WebApiDatabase");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("api/person")]
        public ActionResult<Person> ListPerson()
        {
            PersonContext context = new PersonContext(this.__constr);
            List<Person> ListPerson = context.ListPerson();
            return Ok(ListPerson);
        }

        [HttpGet("api/person/{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult GetPersonById(int id)
        {
            PersonContext context = new PersonContext(this.__constr);
            var data = context.GetPersonById(id);

            if (data == null)
            {
                return NotFound(new
                {
                    status = "error",
                    message = "Data tidak ditemukan"
                });
            }

            return Ok(new
            {
                status = "success",
                data = data
            });
        }

        [HttpPost("api/person")]
        [Authorize(Roles = "admin")]
        public IActionResult CreatePerson(Person person)
        {
            try
            {
                if (person == null)
                {
                    return BadRequest(new
                    {
                        status = "error",
                        message = "Data person tidak boleh kosong"
                    });
                }

                PersonContext context = new PersonContext(this.__constr);
                context.CreatePerson(person);

                return Ok(new
                {
                    status = "success",
                    data = "Data berhasil ditambahkan"
                });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return BadRequest(new
                    {
                        status = "error",
                        message = "ID sudah digunakan"
                    });
                }

                return BadRequest(new
                {
                    status = "error",
                    message = ex.Message
                });
            }
        }

        [HttpPut("api/person/{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdatePerson(int id, Person person)
        {
            try
            {
                if (person == null || person.id_person != id)
                {
                    return BadRequest(new
                    {
                        status = "error",
                        message = "ID tidak valid"
                    });
                }

                PersonContext context = new PersonContext(this.__constr);
                int result = context.UpdatePerson(id, person);

                if (result == 0)
                {
                    return NotFound(new
                    {
                        status = "error",
                        message = "Data tidak ditemukan"
                    });
                }

                return Ok(new
                {
                    status = "success",
                    data = "Data berhasil diupdate"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = "error",
                    message = ex.Message
                });
            }
        }

        [HttpDelete("api/person/{id}/delete")]
        [Authorize(Roles = "admin")]
        public IActionResult DeletePerson(int id)
        {
            try
            {
                PersonContext context = new PersonContext(this.__constr);
                int result = context.DeletePerson(id);

                if (result == 0)
                {
                    return NotFound(new
                    {
                        status = "error",
                        message = "Data tidak ditemukan dan gagal dihapus"
                    });
                }

                return Ok(new
                {
                    status = "success",
                    data = "Data berhasil dihapus"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = "error",
                    message = ex.Message
                });
            }
        }
    }
}



