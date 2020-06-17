using PortalApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalApi.Controllers
{
    public class DepartamentoPortalController : ApiController
    {
        DepartamentoPortalRepository cmd = new DepartamentoPortalRepository();

        [Route("api/departamentos/listas")]
        [HttpGet]
        public HttpResponseMessage get()
        {
            try
            {
                if (cmd.lista() != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, cmd.lista());
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "Lista não encontrada");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Lamentamos ocorreu um erro");
            }
        }
    }
}
