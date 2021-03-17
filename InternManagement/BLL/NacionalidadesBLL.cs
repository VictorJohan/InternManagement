using InternManagement.Data;
using InternManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InternManagement.BLL
{
    public class NacionalidadesBLL
    {
        private ApplicationDbContext _contexto { get; set; }
        public NacionalidadesBLL(ApplicationDbContext contexto)
        {
            this._contexto = contexto;
        }

        public async Task<bool> Guardar(Nacionalidade nacionallidad)
        {
            if (!await Existe(nacionallidad.NacionalidadId))
                return await Insertar(nacionallidad);
            else
                return await Modificar(nacionallidad);
        }

        private async Task<bool> Existe(int nacionalidadId)
        {
            bool ok = false;
            try
            {
                ok = await _contexto.Nacionalidades.AnyAsync(n => n.NacionalidadId == nacionalidadId);
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Insertar(Nacionalidade nacionallidad)
        {
            bool ok = false;

            try
            {
               await _contexto.Nacionalidades.AddAsync(nacionallidad);
                ok = await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Modificar(Nacionalidade nacionallidad)
        {
            bool ok = false;
            try
            {
                _contexto.Entry(nacionallidad).State = EntityState.Modified;
                ok = await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<Nacionalidade> Buscar(int id)
        {
            Nacionalidade nacionalidad;

            try
            {
                nacionalidad = await _contexto.Nacionalidades.FindAsync(id);

                var aux = _contexto.Set<Nacionalidade>().Local.SingleOrDefault(n => n.NacionalidadId == id);
                if(aux != null)
                {
                    _contexto.Entry(aux).State = EntityState.Detached;
                }

            }
            catch (Exception)
            {

                throw;
            }

            return nacionalidad;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool ok = false;

            var registro = await Buscar(id);

            try
            {
                if(registro != null)
                {
                    _contexto.Nacionalidades.Remove(registro);
                    ok = await _contexto.SaveChangesAsync() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<List<Nacionalidade>> GetNacionalidades(Expression<Func<Nacionalidade, bool>> criterio)
        {
            List<Nacionalidade> lista = new List<Nacionalidade>();

            try
            {
                lista = await _contexto.Nacionalidades.Where(criterio).ToListAsync();
                lista.Sort((x, y) => x.Nacionalidad.CompareTo(y.Nacionalidad));
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }
    }
}
