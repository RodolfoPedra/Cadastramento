﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CadastramentoEntities : DbContext
    {
        public CadastramentoEntities()
            : base("name=CadastramentoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<menuacao> menuacao { get; set; }
        public virtual DbSet<analise> analise { get; set; }
        public virtual DbSet<auditlog> auditlog { get; set; }
        public virtual DbSet<auditlogdetail> auditlogdetail { get; set; }
        public virtual DbSet<auditlogexcecao> auditlogexcecao { get; set; }
        public virtual DbSet<cadeiaprodutiva> cadeiaprodutiva { get; set; }
        public virtual DbSet<consideracao> consideracao { get; set; }
        public virtual DbSet<consultor> consultor { get; set; }
        public virtual DbSet<empresa> empresa { get; set; }
        public virtual DbSet<empresaprojeto> empresaprojeto { get; set; }
        public virtual DbSet<logacesso> logacesso { get; set; }
        public virtual DbSet<municipio> municipio { get; set; }
        public virtual DbSet<perfil> perfil { get; set; }
        public virtual DbSet<processo> processo { get; set; }
        public virtual DbSet<processosituacao> processosituacao { get; set; }
        public virtual DbSet<processotipo> processotipo { get; set; }
        public virtual DbSet<situacaocadastro> situacaocadastro { get; set; }
        public virtual DbSet<tipocarga> tipocarga { get; set; }
        public virtual DbSet<tipocarroceria> tipocarroceria { get; set; }
        public virtual DbSet<tipoincentivo> tipoincentivo { get; set; }
        public virtual DbSet<tipoveiculo> tipoveiculo { get; set; }
        public virtual DbSet<unidademedida> unidademedida { get; set; }
        public virtual DbSet<usuario> usuario { get; set; }
        public virtual DbSet<usuariomenu> usuariomenu { get; set; }
        public virtual DbSet<usuariomenuacao> usuariomenuacao { get; set; }
        public virtual DbSet<usuariomenuacaoperfilpermissao> usuariomenuacaoperfilpermissao { get; set; }
        public virtual DbSet<usuariomenuperfilpermissao> usuariomenuperfilpermissao { get; set; }
        public virtual DbSet<usuarioperfil> usuarioperfil { get; set; }
        public virtual DbSet<carroceria> carroceria { get; set; }
        public virtual DbSet<motorista> motorista { get; set; }
        public virtual DbSet<veiculo> veiculo { get; set; }
    }
}
