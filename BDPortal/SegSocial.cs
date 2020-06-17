//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BDPortal
{
    using System;
    using System.Collections.Generic;
    
    public partial class SegSocial
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SegSocial()
        {
            this.Estabelecimentos = new HashSet<Estabelecimentos>();
            this.Funcionarios = new HashSet<Funcionarios>();
        }
    
        public string SegSocial1 { get; set; }
        public string Descricao { get; set; }
        public string Regime { get; set; }
        public float DescEmpregado { get; set; }
        public Nullable<float> DescEmpresa { get; set; }
        public string CodGuiaPag { get; set; }
        public string CodFixo { get; set; }
        public string EnderecoWeb { get; set; }
        public string Email { get; set; }
        public string BaseCotizacao { get; set; }
        public decimal ReducaoITEmpresa { get; set; }
        public decimal ReducaoITFuncionario { get; set; }
        public string TipoEntidadeCCT { get; set; }
        public string EntidadeCCT { get; set; }
        public string NIPC { get; set; }
        public bool DispComPltBASE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Estabelecimentos> Estabelecimentos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Funcionarios> Funcionarios { get; set; }
    }
}
