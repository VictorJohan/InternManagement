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
    public class HabilidadesBLL
    {
        private ApplicationDbContext _contexto { get; set; }
        public HabilidadesBLL(ApplicationDbContext contexto)
        {
            this._contexto = contexto;
        }

        public async Task<bool> Guardar(Habilidade habilidad)
        {
            if (!await Existe(habilidad.HabilidadId))
                return await Insertar(habilidad);
            else
                return await Modificar(habilidad);
        }

        private async Task<bool> Existe(int id)
        {
            bool ok = false;

            try
            {
                ok = await _contexto.Habilidades.AnyAsync(h => h.HabilidadId == id);
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Insertar(Habilidade habilidad)
        {
            bool ok = false;

            try
            {
                habilidad.FechaCreacion = DateTime.Now;
                _contexto.Habilidades.Add(habilidad);
                ok = await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Modificar(Habilidade habilidad)
        {
            bool ok = false;
            try
            {
                _contexto.Entry(habilidad).State = EntityState.Modified;
                ok = await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<Habilidade> Buscar(int id)
        {
            Habilidade habilidad;

            try
            {
                habilidad = await _contexto.Habilidades.Where(h => h.HabilidadId == id).AsNoTracking().SingleOrDefaultAsync();

                var aux = _contexto.Set<Habilidade>().Local.SingleOrDefault(h => h.HabilidadId == id);
                if (aux != null)
                    _contexto.Entry(aux).State = EntityState.Detached;
            }
            catch (Exception)
            {

                throw;
            }

            return habilidad;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool ok = false;
            try
            {
                var registro = await Buscar(id);
                if(registro != null)
                {
                    _contexto.Habilidades.Remove(registro);
                    ok = await _contexto.SaveChangesAsync() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<List<Habilidade>> GetHabilidades(Expression<Func<Habilidade, bool>> criterio)
        {
            List<Habilidade> lista = new List<Habilidade>();

            try
            {
                lista = await _contexto.Habilidades.Where(criterio).ToListAsync();
                lista.Sort((x, y) => x.Descripcion.CompareTo(y.Descripcion));
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }
    }
}
