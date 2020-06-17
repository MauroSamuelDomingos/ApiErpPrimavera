using ErpBS100;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PortalApi.App_Start
{
    public class AssemblyResolve
    {
        const string PRIMAVERA_FILES_FOLDER = "PRIMAVERA\\SG100\\Apl";

        public static void Resolve() => AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

        static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            System.Reflection.AssemblyName assemblyName = new System.Reflection.AssemblyName(args.Name);
            string assemblyFullName = assemblyFullName = System.IO.Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), PRIMAVERA_FILES_FOLDER), assemblyName.Name + ".dll");

            if (System.IO.File.Exists(assemblyFullName))
                return System.Reflection.Assembly.LoadFile(assemblyFullName);
            else return null;

        }

        public static ErpBS AbrirEmpresaERPV10()
        {
            ErpBS bso = new ErpBS();
            bso.AbreEmpresaTrabalho(StdBE100.StdBETipos.EnumTipoPlataforma.tpEmpresarial, "PRH", "mauro", "934385007", null, "DEFAULT");
            return bso;
        }
    }
}