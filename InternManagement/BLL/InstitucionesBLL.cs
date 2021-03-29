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
    public class InstitucionesBLL
    {
        private ApplicationDbContext _contexto { get; set; }
        public InstitucionesBLL(ApplicationDbContext contexto)
        {
            this._contexto = contexto;
        }

        public async Task<bool> Guardar(Institucione instituciones)
        {
            if (!await Existe(instituciones.InstitucionId))
                return await Insertar(instituciones);
            else
                return await Modificar(instituciones);
                
        }

        private async Task<bool> Existe(int id)
        {
            bool ok = false;

            try
            {
                ok = await _contexto.Instituciones.AnyAsync(i => i.InstitucionId == id);
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Insertar(Institucione instituciones)
        {
            bool ok = false;
            try
            {
                await _contexto.AddAsync(instituciones);
                ok = await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Modificar(Institucione instituciones)
        {
            bool ok = false;

            try
            {
                _contexto.Entry(instituciones).State = EntityState.Modified;
                ok = await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<Institucione> Buscar(int id)
        {
            Institucione institucione;

            try
            {
                institucione = await _contexto.Instituciones.Where(i => i.InstitucionId == id).AsNoTracking().SingleOrDefaultAsync();

                var aux = _contexto.Set<Institucione>().Local.SingleOrDefault(h => h.InstitucionId == id);
                if (aux != null)
                    _contexto.Entry(aux).State = EntityState.Detached;
            }
            catch (Exception)
            {

                throw;
            }

            return institucione;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool ok = false;
            try
            {
                var registro = await Buscar(id);
                if (registro != null)
                {
                    _contexto.Instituciones.Remove(registro);
                    ok = await _contexto.SaveChangesAsync() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<List<Institucione>> GetInstituciones(Expression<Func<Institucione, bool>> criterio)
        {
            List<Institucione> lista = new List<Institucione>();

            try
            {
                lista = await _contexto.Instituciones.Where(criterio).ToListAsync();
                lista.Sort((x, y) => x.Nombre.CompareTo(y.Nombre));
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }
    }
}

