using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProcedimientosAlmacenadosAPInetCore.Data;
using ProcedimientosAlmacenadosAPInetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcedimientosAlmacenadosAPInetCore.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class ValuesController: ControllerBase
    {
        private readonly ValuesRepository _repository;
        private readonly IConfiguration configuration;

        public ValuesController(ValuesRepository repository, IConfiguration configuration)
        {

            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet]
        public async Task<List<Value>> Get()
        {
            return await this._repository.GetAll();
        }

        [HttpGet ("ejemploIAction")]
        public ActionResult <string> GetAction ()
        {
            //si es un atributo interno es con [propiedad: propiedadInterna]
            return this.configuration["apellido"];
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Value>> Get(int id)
        {
            var response = await this._repository.GetById(id);
            if (response == null)
            {
                return NotFound();
            }
            return response;
        }

        [HttpPost]
        public async Task Post ([FromBody] Value value)
        {
            await this._repository.Insert(value);
        }

        [HttpDelete ("{id}")]
        public async Task Delete (int id)
        {
            await this._repository.DeleteById(id);
        }
    }
}
