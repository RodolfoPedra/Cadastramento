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
    
    public partial class motorista
    {
        public int motoristaid { get; set; }
        public string protocolo { get; set; }
        public string nome { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string rg { get; set; }
        public string rguf { get; set; }
        public string rgorgaoexpedidor { get; set; }
        public string cpf { get; set; }
        public string cnh { get; set; }
        public string ufcnh { get; set; }
        public string categoriacnh { get; set; }
        public System.DateTime dataemissaocnh { get; set; }
        public System.DateTime datavalidadecnh { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }
        public Nullable<System.DateTime> datavalidadecadastro { get; set; }
        public int situacaocadastroid { get; set; }
        public string documentourl { get; set; }
        public Nullable<int> usuarioidalteracao { get; set; }
        public Nullable<System.DateTime> datahoraalteracao { get; set; }
        public Nullable<int> usuarioidinclusao { get; set; }
        public Nullable<System.DateTime> datahorainclusao { get; set; }
    
        public virtual situacaocadastro situacaocadastro { get; set; }
    }
}
