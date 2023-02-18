using GetJobAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GetJobAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;
        public UsuariosController(ILogger<UsuariosController> logger)
        {
            _logger = logger;
        }

        // GET: api/usuarios
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UsuarioDTO>> GetUsuarios()
        {
            _logger.LogInformation("Obtener los usuarios");
            var usuarios = new List<UsuarioDTO>
            {
                new UsuarioDTO{ Id = 1, Nombre = "Sergio"},
                new UsuarioDTO{ Id = 2, Nombre = "Ane"},
            };

            // Retornar código 200 Ok
            return Ok(usuarios);
        }

        // GET: api/usuarios/{id}
        [HttpGet("id:int", Name = "GetUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UsuarioDTO> GetUsuario(int id)
        {
            var usuarios = new List<UsuarioDTO>
            {
                new UsuarioDTO{ Id = 1, Nombre = "Sergio" },
                new UsuarioDTO{ Id = 2, Nombre = "Ane" },
            };

            if (id == 0) { return BadRequest(); }

            var usuario = usuarios.FirstOrDefault(usuario => usuario.Id == id);

            if (usuario == null) { return NotFound(); }

            return Ok(usuario);
        }

        // POST: api/usuarios
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UsuarioDTO> AddUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            var usuarios = new List<UsuarioDTO>
            {
                new UsuarioDTO{ Id = 1, Nombre = "Sergio" },
                new UsuarioDTO{ Id = 2, Nombre = "Ane" },
            };

            // Comprobar si se cumplen las validaciones de las restricciones del modelo
            if(!ModelState.IsValid) { return BadRequest(ModelState); }

            // Comprobar si se cumple que no existe un usuario con el mismo nombre (Validación personalizada)
            if (usuarios.FirstOrDefault(u => u.Nombre.ToLower() == usuarioDTO.Nombre.ToLower()) != null) {
                ModelState.AddModelError("NombreExiste", "El usuario con ese nombre ya existe");
                return BadRequest(ModelState);
            }

            // Comprobar si no se pasa un usuario
            if (usuarioDTO == null) { return BadRequest(usuarioDTO); }

            // Comprobar si se pasa un Id con el usuario
            if (usuarioDTO.Id > 0) { return StatusCode(StatusCodes.Status500InternalServerError); }

            // Asignar al usuario el Id del último usuario + 1 
            usuarioDTO.Id = usuarios.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;

            // Añadir usuario a la BBDD
            usuarios.Add(usuarioDTO);

            // Retornar la URI y el usuario añadido 
            return CreatedAtRoute("GetUsuario", new {id = usuarioDTO.Id}, usuarioDTO);
        }

        // DELETE: api/usuarios/{id}
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUsuario(int id)
        {
            var usuarios = new List<UsuarioDTO>
            {
                new UsuarioDTO{ Id = 1, Nombre = "Sergio"},
                new UsuarioDTO{ Id = 2, Nombre = "Ane"},
            };

            if (id == 0) { return BadRequest(); }

            var usuario = usuarios.FirstOrDefault(usuario => usuario.Id == id);

            if (usuario == null) { return NotFound(); }

            usuarios.Remove(usuario);

            return NoContent();
        }

        // UPDATE: api/usuarios/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateUsuario(int id, [FromBody] UsuarioDTO usuarioDTO)
        {
            var usuarios = new List<UsuarioDTO>
            {
                new UsuarioDTO{ Id = 1, Nombre = "Sergio"},
                new UsuarioDTO{ Id = 2, Nombre = "Ane"},
            };

            // Comprobar si no se pasa un usuario o el Id pasado es diferente al Id del usuario
            if (usuarioDTO == null || id != usuarioDTO.Id) { return BadRequest(); }

            // Recuperar usuario
            var usuario = usuarios.FirstOrDefault(usuario => usuario.Id == id);

            // Actualizar propiedades del usuario
            usuario.Nombre = usuarioDTO.Nombre;

            //usuarios.Remove(usuario);

            return NoContent();
        }
    }
}
