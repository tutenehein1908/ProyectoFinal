namespace Licencia___PF.Controllers
{
    using Licencia___PF.Model;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Licencias___PF.Model.DTO;

    [Route("api/[controller]")]
    [ApiController]
    public class LicenciaController : ControllerBase
    {
        private readonly LicenciaContext _context;

        public LicenciaController(LicenciaContext context)
        {
            _context = context;
        }

        //Hice un metodo mas para practicar pasando parametros
        [HttpGet("{id}")]
        public async Task<ActionResult<Licencia>> Get(int id)
        {
            try
            {
                //FirstOrDefaultAsync devuelve el primer elemento que satisfaga la condicion, en este caso el id 
                var licencia = await _context.Licencias.FirstOrDefaultAsync(l => l.Id == id);

                //si la licencia es nula quiere decir que no coincide ningun Id de licencias que coincida con el id que paso el usuario
                if (licencia == null)
                {
                    //retorno un error con un msj 
                    return NotFound($"No se encontró una licencia con el ID: {id}");
                }

                // si coinciden entonces devuelvo un ok y la licencia (que guardamos en  var licencia)
                return Ok(licencia);
            }
            catch (Exception ex)
            {

                //si algo sale mal, enviamos un msj de error
                return BadRequest($"Ocurrió un error interno: {ex.Message}");
            }
        }



        //Otro método Get pero ahora quiero que me devuelva todas las licencias registradas, por esta razon utilizo IEnumerable y ToListAsync, por todo lo demas misma logica que el método anterior
        [HttpGet("varias")]
        public async Task<ActionResult<IEnumerable<Licencia>>> Get()
        {
            try
            {
                var licencias = await _context.Licencias.ToListAsync();
                return Ok(licencias);
            }
            catch(Exception ex)
            {
                return BadRequest($"Ocurrió un error interno: {ex.Message}");
            }
            

        }


        // Ahora creo un método para eliminar una licencia ya existente mediante la coincidencia del id del parametro con el Id de la DB
        [HttpDelete("{id}")]
        public async Task<ActionResult<Licencia>> Delete(int id)
        {
            //aca filtro con Where (siendo licencias el objeto, tomo el Id de esa licencia y si coincide con el id que me paso el usuario por parametro ejecuto el Delete que ademas me devuelve el numero de filas alteradas (en este caso 1 si se ejecuta (encontro coincidencia) y 0 (si no encontro))
            var filasAlteradas = await _context.Licencias.Where(l => l.Id == id).ExecuteDeleteAsync();

            //Si filasAlteradas es == 0 es porque no encontro coincidencia
            if (filasAlteradas == 0)
            {
                //devuelvo un NotFound
                return NotFound(new { mensaje = "Licencia no encontrada." });
            }

            //En este caso si encontro, yo elegi devolver los registros alterados (eliminados) y un mensaje confirmando la eliminacion
            return Ok(new
            {
                filasAlteradas = filasAlteradas,
                mensaje = "Licencia eliminada correctamente."
            });

        }


        // En este POST estoy haciendo uso de un DTO porque no quiero que el usuario cargue el Id (es identity en sqlServer)
        [HttpPost]
        public async Task<ActionResult> InsertarLicencia([FromBody] LicenciaCreateDto dto)
        {

            //mi intencion fue hacer uso de guard clause primero validando y luego seguir con el grueso del codigo
            //encontre esta manera de validar el Model que realice mediante anotaciones de datos, a mi me resulto lo mas sencillo para configurar mi clase
            //entiendo que verifica que el Model sea valido (por ejemplo en formato) y cumpla con mis anotaciones
            if (!ModelState.IsValid)
            {
                //Si no es valido, entonces devuelvo especificamente el error
                return BadRequest(ModelState);
            }
                
            //si es valido entonces creo un nuevo objeto llamado nuevaLicencia de tipo Licencia (porque sino SQL no la podria mappear de ser del tipo dto) pero los campos que pido por body son del dto 
            var nuevaLicencia = new Licencia
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Dni = dto.Dni,
                TipoDeLicencia = dto.TipoDeLicencia,
                Provincia = dto.Provincia,
                Localidad = dto.Localidad,
                Direccion = dto.Direccion,
                OD = dto.OD,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin
            };

            //realizó el Add
            _context.Licencias.Add(nuevaLicencia);
            //y aca lo hago efectivo con SaveChangesAsync
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Licencia agregada correctamente.", id = nuevaLicencia.Id });
        }


        //Actualizar licencia, para eso le pido al usuario el id de la licencia existente que quiere actualizar y del body obtengo la licenciaActualizada, use un DTO para que no pueda modificar el id
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarLicencia(int id, [FromBody] LicenciaCreateDto dto)
        {

            //mi intencion fue hacer uso de guard clause primero validando y luego seguir con el grueso del codigo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //muy parecido al Get aca, obtengo o no coincidencias
            var licencia = await _context.Licencias.FirstOrDefaultAsync(l => l.Id == id);
            //mi intencion fue hacer uso de guard clause primero validando y luego seguir con el grueso del codigo
            if (licencia == null)
            {
                return NotFound(new { mensaje = "Licencia no encontrada." });
            }

            //Si hay coincidencia entonces actualizo
            licencia.Nombre = dto.Nombre;
            licencia.Apellido = dto.Apellido;
            licencia.Dni = dto.Dni;
            licencia.TipoDeLicencia = dto.TipoDeLicencia;
            licencia.Provincia = dto.Provincia;
            licencia.Localidad = dto.Localidad;
            licencia.Direccion = dto.Direccion;
            licencia.OD = dto.OD;
            licencia.FechaInicio = dto.FechaInicio;
            licencia.FechaFin = dto.FechaFin;

            //y aca hago efectiva la carga
            await _context.SaveChangesAsync();

            //retorno un OK con un msj
            return Ok(new { mensaje = "Licencia actualizada correctamente." });
        }

    }

}
