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
    
    public partial class usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuario()
        {
            this.usuarioperfil = new HashSet<usuarioperfil>();
        }
    
        public int usuarioid { get; set; }
        public string nome { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public string situacao { get; set; }
        public Nullable<int> usuarioidinclusao { get; set; }
        public Nullable<System.DateTime> datahorainclusao { get; set; }
        public Nullable<int> usuarioidalteracao { get; set; }
        public Nullable<System.DateTime> datahoraalteracao { get; set; }
        public Nullable<int> usuarioidsistemaantigo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<usuarioperfil> usuarioperfil { get; set; }
    }
}
