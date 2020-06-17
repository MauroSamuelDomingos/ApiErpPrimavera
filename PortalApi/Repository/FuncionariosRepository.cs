using BDPortal;
using PortalApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace PortalApi.Repository
{
    public class FuncionariosRepository
    {
        PRIPRHEntities db = new PRIPRHEntities();
        ClsConectBd clsConectBd = new ClsConectBd();
        public List<FuncionarioAuxiliar> Lista()
        {
            try
            {
                int numero = 0;
                string email = string.Empty;

                var dados = db.Funcionarios.ToList();
                List<FuncionarioAuxiliar> lista = new List<FuncionarioAuxiliar>();
                foreach (var linhas in dados)
                {
                    if (linhas.Telefone == null)
                        numero = 0;
                    else
                        numero = int.Parse(linhas.Telefone);

                    if (linhas.Email == null)
                        email = "";
                    else
                        email = linhas.Email;

                    lista.Add(new FuncionarioAuxiliar { Codigo = linhas.Codigo, DataAdmissao = linhas.DataAdmissao.ToString(), DataNascimento = linhas.DataNascimento.ToString(), Email = linhas.Email, Nome = linhas.Nome, Telefone = numero });
                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Funcionarios ListaFuncionario(string codigo)
        {
            try
            {
                Funcionarios func = db.Funcionarios.Find(codigo);
                return func;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DepartamentoPortal Departamento(string codigo)
        {
            try
            {
                DepartamentoPortal dep = new DepartamentoPortal();
                var dados = db.Funcionarios.Find(codigo);
                dep.Codigo = dados.CodDepartamento;
                dep.Descricao = dados.Departamentos.Descricao;
                return dep;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string AlteraCAmpos(AlteraCamposFicha dados)
        {
            string message = string.Empty;
            try
            {               
                if(dados.TipoCampo == "Morada")
                {
                  string result =  clsConectBd.comand(" Update Funcionarios set morada = '" + dados.Descricao + "' where codigo = '" + dados.Funcionario + "' ");
                    if (result.Equals("1"))
                        message = "ok";
                    else
                        message = result;
                }

                if (dados.TipoCampo == "Localidade")
                {
                    string result = clsConectBd.comand(" Update Funcionarios set Localidade = '" + dados.Descricao + "' where codigo = '" + dados.Funcionario + "' ");
                    if (result.Equals("1"))
                        message = "ok";
                    else
                        message = result;
                }

                if (dados.TipoCampo == "Telefone")
                {
                    string result = clsConectBd.comand(" Update Funcionarios set Telefone = '" + dados.Descricao + "' where codigo = '" + dados.Funcionario + "' ");
                    if (result.Equals("1"))
                        message = "ok";
                    else
                        message = result;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        public string AlteraAgregado(ErpBS100.ErpBS BSO, AlteraAgregadoFamiliar dados)
        {
            string message = string.Empty;
            RhpBE100.RhpBEFuncAgregado rhpBEFuncAgregado = new RhpBE100.RhpBEFuncAgregado();

            try
            {
                rhpBEFuncAgregado.Funcionario = dados.Funcionario;
                rhpBEFuncAgregado.Nome = dados.NomeAgregado;
                if (dados.Estudante == 1)
                    rhpBEFuncAgregado.Estudante = true;
                else
                    rhpBEFuncAgregado.Estudante = false;

                rhpBEFuncAgregado.NIF = dados.NIF;
                rhpBEFuncAgregado.DataNasc = dados.DataNasc;
                rhpBEFuncAgregado.DataValidadeBI = dados.DataBI;
                rhpBEFuncAgregado.NumeroBI = dados.NumBI;
                rhpBEFuncAgregado.TipoAfinidade = (byte)dados.Afinidade;
                var lista = BSO.RecursosHumanos.FuncAgregados.ListaFuncAgregados(dados.Funcionario).LastOrDefault();
                if (lista != null)
                    rhpBEFuncAgregado.Agregado = Convert.ToString(Convert.ToInt32(lista.Agregado.ToString()) + 1);
                else
                    rhpBEFuncAgregado.Agregado = "1";
                BSO.RecursosHumanos.FuncAgregados.Actualiza(rhpBEFuncAgregado);
                message = "ok";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }


        public string GravaFerias(ErpBS100.ErpBS BSO, FeriasAuxiliar fe)
        {
            string message = string.Empty;
            RhpBE100.RhpBEFeria ferias = new RhpBE100.RhpBEFeria();

            try
            {
                ferias.Ano = fe.Ano;
                ferias.DataFeria = fe.DataFeria;
                ferias.EstadoGozo = false;
                ferias.Funcionario = fe.Funcionario;
                ferias.OriginouFalta = false;
                ferias.OriginouFaltaSubAlim = false;
                ferias.TipoMarcacao = (byte)fe.TipoMarcacao;
                BSO.RecursosHumanos.Ferias.Actualiza(ferias);
                message = "ok";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        public string GravaEmprestimo(ErpBS100.ErpBS BSO, SendEmprestimoERP emp)
        {
            string message = string.Empty;
            CctBE100.CctBEPendente Pendente = new CctBE100.CctBEPendente();
            CctBE100.CctBELinhaPendente Linhapendente = new CctBE100.CctBELinhaPendente();
            CctBE100.CctBELinhasPendentes LinhasPendentes = new CctBE100.CctBELinhasPendentes();

            try
            {
                Pendente.Estado = "PEN";
                Pendente.Entidade = emp.Codigo;
                Pendente.TipoEntidade = "U";
                Pendente.DataDoc = DateTime.Now.Date;
                Pendente.DataIntroducao = DateTime.Now.Date;
                Pendente.DataVenc = DateTime.Now.Date;
                Pendente.Tipodoc = ConfigurationManager.AppSettings["DocPendente"].ToString();

                string serie = BSO.Base.Series.DaSerieDefeito("M", ConfigurationManager.AppSettings["DocPendente"].ToString());
                string numdoc = BSO.Base.Series.ProximoNumero("M", ConfigurationManager.AppSettings["DocPendente"].ToString(), serie).ToString();
                Pendente.Serie = serie;
                Pendente.NumDoc = numdoc;
                Pendente.NumDocInt = int.Parse(numdoc);

                BSO.PagamentosRecebimentos.Pendentes.PreencheDadosRelacionados(Pendente);
                Linhapendente.Descricao = "Pendente de colaborador vindo do portal.";
                Linhapendente.Incidencia = (double)emp.Valor;
                Linhapendente.Total = (double)emp.Valor;
                Linhapendente.CodIva = ConfigurationManager.AppSettings["CodIva"].ToString();
                LinhasPendentes.Insere(Linhapendente);
                Pendente.Linhas = LinhasPendentes;
                BSO.PagamentosRecebimentos.Pendentes.Actualiza(Pendente);
                message = "ok";
            }
            catch (Exception ex)
            {
                message = ""+ex.Message;
            }
            return message;
        }
    }
}