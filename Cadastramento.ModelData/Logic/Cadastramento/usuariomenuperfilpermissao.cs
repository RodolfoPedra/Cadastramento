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
    
    public partial class usuariomenuperfilpermissao
    {
        public int usuariomenuperfilpermissaoid { get; set; }
        public int usuariomenuid { get; set; }
        public int perfilid { get; set; }
    
        public virtual perfil perfil { get; set; }
        public virtual usuariomenu usuariomenu { get; set; }
    }
}