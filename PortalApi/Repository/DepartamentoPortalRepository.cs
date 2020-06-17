using BDPortal;
using PortalApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PortalApi.Repository
{
    public class DepartamentoPortalRepository
    {
        ClsConectBd cmd = new ClsConectBd();

        public List<DepartamentoPortal> lista()
        {
            List<DepartamentoPortal> list = new List<DepartamentoPortal>();
            try
            {
                DataTable dt = cmd.select(" Select departamento,descricao from departamentos ");
                if (dt != null)
                {
                    foreach (DataRow linhas in dt.Rows)
                    {
                        list.Add(new DepartamentoPortal { Codigo = linhas["departamento"].ToString(), Descricao = linhas["descricao"].ToString() });
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
    }
}