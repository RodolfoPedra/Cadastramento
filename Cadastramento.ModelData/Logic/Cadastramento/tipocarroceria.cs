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
    
    public partial class tipocarroceria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tipocarroceria()
        {
            this.carroceria = new HashSet<carroceria>();
        }
    
        public int tipocarroceriaid { get; set; }
        public string descricao { get; set; }
        public string status { get; set; }
        public Nullable<int> usuarioidalteracao { get; set; }
        public Nullable<System.DateTime> datahoraalteracao { get; set; }
        public Nullable<int> usuarioidinclusao { get; set; }
        public System.DateTime datahorainclusao { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<carroceria> carroceria { get; set; }
    }
}
