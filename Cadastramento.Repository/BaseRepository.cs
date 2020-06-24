using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Cadastramento.ModelData.Logic.Cadastramento;

namespace Cadastramento.Repository
{
    public class BaseRepository<T> where T : class
    {
        public CadastramentoEntities Conexao;
        private readonly IDbSet<T> Contexto;

        public BaseRepository()
        {
            if (Conexao == null)
                Conexao = new CadastramentoEntities();
            Contexto = Conexao.Set<T>();
        }

        public interface IBaseRepository
        {
            /// <summary>
            /// Método padrão para Incluir
            /// </summary>
            /// <param name="entity"></param>
            void Incluir(T entity);

            /// <summary>
            /// Método padrão para Alterar
            /// </summary>
            /// <param name="entity"></param>
            void Alterar(T entity);

            /// <summary>
            /// Método padrão para Excluir
            /// </summary>
            /// <param name="entity"></param>
            void Excluir(T entity);

            /// <summary>
            /// Método padrão para Excluir utilizando expressão Linq
            /// </summary>
            /// <param name="where"></param>
            void Excluir(Expression<Func<T, bool>> where);

            /// <summary>
            /// Método padrão para Obter por PK do tipo long
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            T Obter(long id);

            /// <summary>
            /// Método padrão para Obter por PK do tipo string
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            T Obter(string id);

            /// <summary>
            /// Método padrão para Obter o primeiro registro que atende a expressão Linq
            /// </summary>
            /// <param name="where"></param>
            /// <returns></returns>
            T ObterPrimeiro(Expression<Func<T, bool>> where);

            /// <summary>
            /// Método padrão para obter todos os registros
            /// </summary>
            /// <returns></returns>
            IQueryable<T> ObterTodos();


            /// <summary>
            /// Método padrão para obter os registros de acordo com o script sql passado por parâmetro
            /// </summary>
            /// <param name="sql"></param>
            /// <returns></returns>
            IQueryable<T> ObterPorSql(string sql);

            /// <summary>
            /// Método padrão para Obter os registros que atendem a expressão Linq
            /// </summary>
            /// <param name="where"></param>
            /// <returns></returns>
            IQueryable<T> ObterVarios(Expression<Func<T, bool>> where);

            /// <summary>
            /// Método padrão para dar Commit
            /// </summary>
            void Salvar(string login);
        }

        public virtual void Incluir(T entity)
        {
            Contexto.Add(entity);
        }

        public virtual void Alterar(T entity)
        {
            Contexto.Attach(entity);
            Conexao.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Excluir(T entity)
        {
            Contexto.Remove(entity);
        }
        public virtual void Excluir(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = Contexto.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                Contexto.Remove(obj);
        }
        public virtual T Obter(long id)
        {
            return Contexto.Find(id);
        }

        public virtual T Obter(int id)
        {
            return Contexto.Find(id);
        }

        public virtual T Obter(string id)
        {
            return Contexto.Find(id);
        }

        public virtual T ObterPrimeiro(Expression<Func<T, bool>> where)
        {
            return Contexto.Where(where).FirstOrDefault<T>();
        }

        public virtual IQueryable<T> ObterTodos()
        {
            return Contexto;
        }

        public virtual IQueryable<T> ObterPorSql(string sql)
        {
            return Conexao.Database.SqlQuery<T>(sql).AsQueryable();
        }

        public virtual IQueryable<T> ObterVarios(Expression<Func<T, bool>> where)
        {
            return Contexto.Where(where);
        }

        public void Salvar(string login)
        {
            Conexao.SaveChanges(login);
        }

        public void Dispose()
        {
            Conexao.Dispose();
        }
    }
}
