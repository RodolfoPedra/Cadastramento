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
    
    public partial class usuariomenuacaoperfilpermissao
    {
        public int usuariomenuacaoperfilpermissaoid { get; set; }
        public int usuariomenuacaoid { get; set; }
        public int perfilid { get; set; }
    
        public virtual perfil perfil { get; set; }
        public virtual usuariomenuacao usuariomenuacao { get; set; }
    }
}
