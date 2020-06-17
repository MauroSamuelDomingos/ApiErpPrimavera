using PortalApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalApi.Controllers
{
    public class RecibosController : ApiController
    {
        RecibosRepository cmd = new RecibosRepository();

        [Route("api/recibos/getByCod")]
        [HttpGet]
        public HttpResponseMessage get(string codfuncionario)
        {
            try
            {
                if (cmd.lista(codfuncionario) != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, cmd.lista(codfuncionario));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, cmd.lista(codfuncionario));
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Lamentamos ocorreu um erro");
            }
        }

        [Route("api/recibos/download")]
        [HttpGet]
        public HttpResponseMessage getRec(string codigo)
        {
            try
            {
                if (cmd.Recibo(codigo) != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, cmd.Recibo(codigo));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, cmd.Recibo(codigo));
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Lamentamos ocorreu um erro");
            }
        }
    }
}
