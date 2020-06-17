using BDPortal;
using PortalApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PortalApi.Repository
{
    public class RecibosRepository
    {
        ClsConectBd cmd = new ClsConectBd();
        public List<RecibosVencimento> lista(string codigo)
        {
            List<RecibosVencimento> list = new List<RecibosVencimento>();
            try
            {
                string mes = string.Empty;

                DataTable dt = cmd.select(" select CodFunc,Ano,Mes,TotalDeDescontos,TotalDeRemuneracoes,TotalLiquido, NumProc from Recibos where CodFunc = '"+codigo+"' ");
                if (dt != null)
                {
                    foreach (DataRow linhas in dt.Rows)
                    {
                        switch (int.Parse(linhas["Mes"].ToString()))
                        {
                            case 1:
                                mes = "Janeiro";
                                break;
                            case 2:
                                mes = "Fevereiro";
                                break;
                            case 3:
                                mes = "março";
                                break;
                            case 4:
                                mes = "Abril";
                                break;
                            case 5:
                                mes = "Maio";
                                break;
                            case 6:
                                mes = "Junho";
                                break;
                            case 7:
                                mes = "Julho";
                                break;
                            case 8:
                                mes = "Agosto";
                                break;
                            case 9:
                                mes = "Setembro";
                                break;
                            case 10:
                                mes = "Outubro";
                                break;
                            case 11:
                                mes = "Novembro";
                                break;
                            case 12:
                                mes = "Dezembro";
                                break;
                            default:
                                mes = "Janeiro";
                                break;

                        }

                        list.Add(new RecibosVencimento { Codigo = linhas["CodFunc"].ToString(), Ano = int.Parse(linhas["Ano"].ToString()), Mes = int.Parse(linhas["Ano"].ToString()), Id = int.Parse(linhas["NumProc"].ToString()), TotalRemuneracao = decimal.Parse(linhas["TotalDeRemuneracoes"].ToString()),
                        TotalDesconto = decimal.Parse(linhas["TotalDeDescontos"].ToString()), TotalLiquido = decimal.Parse(linhas["TotalLiquido"].ToString()), MesExtenso = mes });
                    }
                }
                else
                {
                    list = null;
                }
            }
            catch (Exception ex)
            {
                list = null;
            }
            return list;
        }

        public RecibosAuxiliar Recibo(string numproc)
        {
            RecibosAuxiliar rec = new RecibosAuxiliar();
            try
            {
                DataTable dt = cmd.select(" select Pdf from Recibos where NumProc = '" + numproc + "' ");
                if (dt != null)
                {
                    foreach(DataRow rows in dt.Rows)
                    {
                        rec.pdf = (byte[])rows["Pdf"];
                    }
                }
                rec = null;
            }
            catch (Exception)
            {
                rec = null;
            }
            return rec;
        }
    }
}