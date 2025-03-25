using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectServiceShared.Models;
using PoryectServiceApi.Server.Data;

namespace PoryectServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ClientController(ApplicationDbContext context)
        {
            _context = context;
        }
        //Estado de conexion con el servidor
        [HttpGet]
        [Route("ConexionServidor")]
        public async Task<ActionResult<string>> GetConexionServidor()
        {
            return Ok("Conectado");
        }
        //Estados de conexion con la tabla de la base de datos
        [HttpGet]
        [Route("ConexionDB")]
        public async Task<ActionResult<string>> GetConexionDB()
        {
            try
            {
                var usuarios = await _context.Clients.ToListAsync();
                return Ok("Buena Calidad");
            }
            catch (Exception ex)
            {
                return BadRequest("Mala calidad");
            }
        }
        //Aqui comienza el CRUD
        [HttpGet("Listado")]
        public async Task<ActionResult<List<Client>>> GetEjemplos()
        {
            try
            {
                var lista = await _context.Clients.ToListAsync();
                return Ok(lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("ConsutarId/{id}")]
        public async Task<ActionResult<List<Client>>> GetSingleEjemplo(int id)
        {
            try
            {
                var miobjeto = await _context.Clients.FirstOrDefaultAsync(ob => ob.Id == id);
                if (miobjeto == null)
                {
                    return NotFound(" :/");
                }
                return Ok(miobjeto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("Crear")]
        public async Task<ActionResult<string>> CreateClient(Client objeto)
        {
            try
            {
                _context.Clients.Add(objeto);
                await _context.SaveChangesAsync();
                return Ok("Creado con éxio");
            }
            catch (Exception ex)
            {
                return BadRequest("Error durante el proceso de almacenamiento");
            }
        }
        [HttpPost("AgregarListado")]
        public async Task<ActionResult<string>> AgregarListadoTanque(List<Client> listado)
        {
            try
            {
                foreach (var item in listado)
                {
                    var miobjeto = await _context.Clients.FirstOrDefaultAsync(ob => ob.Id == item.Id);
                    if (miobjeto == null)
                    {
                        _context.Clients.Add(item);
                        await _context.SaveChangesAsync();
                    }
                }
                return Ok("Creado con exito");
            }
            catch (Exception ex)
            {
                return BadRequest("Error durante el proceso de almacenamiento");
            }
        }
        [HttpPut("Actualizar/{id}")]
        public async Task<ActionResult<List<Client>>> UpdateEjemplo(Client objeto)
        {
            var DbObjeto = await _context.Clients.FindAsync(objeto.Id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.Nombre = objeto.Nombre;
            DbObjeto.Apellido = objeto.Apellido;
            DbObjeto.Telefono = objeto.Telefono;
            await _context.SaveChangesAsync();
            return Ok(await _context.Clients.ToListAsync());
        }
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<ActionResult<string>> DeleteEjemplo(int id)
        {
            var DbObjeto = await _context.Clients.FirstOrDefaultAsync(Ob => Ob.Id == id);
            if (DbObjeto == null)
            {
                return NotFound("no existe :/");
            }
            try
            {
                _context.Clients.Remove(DbObjeto);
                await _context.SaveChangesAsync();
                return Ok("Eliminado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest("No fué posible eliminar el objeto");
            }
        }
    }
}
