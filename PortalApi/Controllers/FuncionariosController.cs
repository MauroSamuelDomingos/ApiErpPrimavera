using BDPortal;
using ErpBS100;
using PortalApi.App_Start;
using PortalApi.Models;
using PortalApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalApi.Controllers
{
    public class FuncionariosController : ApiController
    {
        FuncionariosRepository cmd = new FuncionariosRepository();

        private ErpBS BSO;
        public FuncionariosController()
        {
            BSO = AssemblyResolve.AbrirEmpresaERPV10();
        }

        [Route("api/funcionarios/get")]
        [HttpGet]
        public HttpResponseMessage Lista()
        {
            try
            {
                if (cmd.Lista() != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, cmd.Lista());
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "Lista não encontrada");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Lamentamos ocorreu um erro."+ex.Message);
            }
        }

        [Route("api/funcionarios/getDepartamento")]
        [HttpGet]
        public HttpResponseMessage Departamento(string codigo)
        {
            try
            {
             return Request.CreateResponse(HttpStatusCode.OK, cmd.Departamento(codigo));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Lamentamos ocorreu um erro."+ex.Message);
            }
        }

        [Route("api/funcionarios/getid")]
        [HttpGet]
        public HttpResponseMessage ListaFuncionario(string codigo)
        {
            try
            {
                Funcionarios func = cmd.ListaFuncionario(codigo);
                if( func != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, func);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "Lista não encontrada");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Lamentamos ocorreu um erro."+ex.Message);
            }
        }

        [Route("api/funcionarios/AlteraCamposFichaColaborador")]
        [HttpPost]
        public HttpResponseMessage AlteraCampos(AlteraCamposFicha dados)
        {
            try
            {
                string message = "";
                message = cmd.AlteraCAmpos(dados);
                if (message == "ok")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Alterações efectuadas");
                }
                return Request.CreateResponse(HttpStatusCode.NotModified, "Alterações não efectuadas");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Lamentamos ocorreu um erro."+ex.Message);
            }
        }

        [Route("api/funcionarios/addferias")]
        [HttpPost]
        public HttpResponseMessage GravaFerias(FeriasAuxiliar dados)
        {
            try
            {
                string message = "";
                message = cmd.GravaFerias(BSO, dados);
                if (message == "ok")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Feria registada no ERP com sucesso.");
                }
                return Request.CreateResponse(HttpStatusCode.NotImplemented, "Ferias não registada no ERP.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Lamentamos ocorreu um erro." + ex.Message);
            }
        }

        [Route("api/funcionarios/AlteraAgregadoColaborador")]
        [HttpPost]
        public HttpResponseMessage AlteraAgregado(AlteraAgregadoFamiliar ag)
        {
            try
            {
                string message = "";
                message = cmd.AlteraAgregado(BSO, ag);
                if (message == "ok")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Alterações efectuadas");
                }
                return Request.CreateResponse(HttpStatusCode.NotModified, "Alterações não efectuadas");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Lamentamos ocorreu um erro." + ex.Message);
            }
        }

        [Route("api/funcionarios/GravaEmprestimo")]
        [HttpPost]
        public HttpResponseMessage GravaEmprestimo(SendEmprestimoERP ag)
        {
            try
            {
                string message = "";
                message = cmd.GravaEmprestimo(BSO, ag);
                if (message == "ok")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Pendente gerado com sucesso");
                }
                return Request.CreateResponse(HttpStatusCode.NotModified, "Pendente não gerado");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,ex.Message);
            }
        }

    }
}
