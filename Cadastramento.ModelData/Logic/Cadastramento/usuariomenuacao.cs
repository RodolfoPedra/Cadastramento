//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cadastramento.ModelData.Logic.Cadastramento
{
    using System;
    using System.Collections.Generic;
    
    public partial class usuariomenuacao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuariomenuacao()
        {
            this.usuariomenuacaoperfilpermissao = new HashSet<usuariomenuacaoperfilpermissao>();
        }
    
        public int usuariomenuacaoid { get; set; }
        public int usuariomenuid { get; set; }
        public int menuacaoid { get; set; }
        public string situacao { get; set; }
        public Nullable<int> usuarioidinclusao { get; set; }
        public Nullable<int> usuarioidalteracao { get; set; }
        public Nullable<System.DateTime> datahorainclusao { get; set; }
        public Nullable<System.DateTime> datahoraalteracao { get; set; }
    
        public virtual menuacao menuacao { get; set; }
        public virtual usuariomenu usuariomenu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<usuariomenuacaoperfilpermissao> usuariomenuacaoperfilpermissao { get; set; }
    }
}