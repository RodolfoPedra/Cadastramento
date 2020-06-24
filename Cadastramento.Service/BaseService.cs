using Cadastramento.Core;
using Cadastramento.Repository;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Cadastramento.Service
{
    public class BaseService<T> where T : class
    {
        public RetornoServico retornoServico;
        public readonly BaseRepository<T> repositorioGenerico;

        public BaseService()
        {
            this.retornoServico = new RetornoServico();
            this.repositorioGenerico = new BaseRepository<T>();
        }

        public interface IService
        {
            /// <summary>
            /// Método de inclusão
            /// </summary>
            /// <param name="entity"></param>
            /// <returns></returns>
            RetornoServico Incluir(T entity);

            /// <summary>
            /// Método de alteração
            /// </summary>
            /// <param name="entity"></param>
            void Alterar(T entity);

            /// <summary>
            /// Excluir o registro passado por parâmetro
            /// </summary>
            /// <param name="entity"></param>
            void Excluir(T entity);

            /// <summary>
            /// Excluir os registros que atendem a expressão Linq
            /// </summary>
            /// <param name="where"></param>
            void Excluir(Expression<Func<T, bool>> where);

            /// <summary>
            /// Obter pelo id (long)
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            T Obter(long id);

            /// <summary>
            /// Obter pelo id em string
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            T Obter(string id);

            /// <summary>
            /// Obter o primeiro registro que atender a expressão Linq
            /// </summary>
            /// <param name="where"></param>
            /// <returns></returns>
            T ObterPrimeiro(Expression<Func<T, bool>> where);

            /// <summary>
            /// Obter um IQueryable de todos os registros
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
            /// Obter um IQueryable através da expressão Linq
            /// </summary>
            /// <param name="where"></param>
            /// <returns></returns>
            IQueryable<T> ObterVarios(Expression<Func<T, bool>> where);

            /// <summary>
            /// Commit
            /// </summary>
            void Salvar(string login);
        }

        public void Salvar(object sessaousuario)
        {
            throw new NotImplementedException();
        }

        public virtual RetornoServico Incluir(T entity)
        {
            repositorioGenerico.Incluir(entity);
            return retornoServico;
        }

        public virtual void Alterar(T entity)
        {
            repositorioGenerico.Alterar(entity);
        }

        public virtual void Excluir(T entity)
        {
            repositorioGenerico.Excluir(entity);
        }

        public virtual void Excluir(Expression<Func<T, bool>> where)
        {
            repositorioGenerico.Excluir(where);
        }
        public virtual T Obter(long id)
        {
            return repositorioGenerico.Obter(id);
        }

        public virtual T Obter(int id)
        {
            return repositorioGenerico.Obter(id);
        }

        public virtual T Obter(string id)
        {
            return repositorioGenerico.Obter(id);
        }

        public virtual T ObterPrimeiro(Expression<Func<T, bool>> where)
        {
            return repositorioGenerico.ObterPrimeiro(where);
        }

        public virtual IQueryable<T> ObterTodos()
        {
            return repositorioGenerico.ObterTodos();
        }

        public virtual IQueryable<T> ObterPorSql(string sql)
        {
            return repositorioGenerico.ObterPorSql(sql);
        }

        public virtual IQueryable<T> ObterVarios(Expression<Func<T, bool>> where)
        {
            return repositorioGenerico.ObterVarios(where);
        }

        public void Salvar(string login)
        {
            repositorioGenerico.Salvar(login);
        }

        public void Dispose()
        {
            repositorioGenerico.Dispose();
        }
    }
}
