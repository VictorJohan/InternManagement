using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using InternManagement.Data;
using InternManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace InternManagement.BLL
{
    public class PasantesBLL
    {
        private ApplicationDbContext _contexto { get; set; }

        public PasantesBLL(ApplicationDbContext contexto)
        {
            this._contexto = contexto;
        }

        public async Task<bool> Guardar(Pasante pasante)
        {
            if (!await Existe(pasante.PasanteId))
                return await Insertar(pasante);
            else
                return await Modificar(pasante);
        }

        private async Task<bool> Existe(int pasanteId)
        {
            bool ok = false;

            try
            {
                ok = await _contexto.Pasantes.AnyAsync(p => p.PasanteId == pasanteId);
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Insertar(Pasante pasante)
        {
            bool ok = false;

            try
            {
                pasante.FechaCreacion = DateTime.Now;
                await _contexto.Pasantes.AddAsync(pasante);
                ok = await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Modificar(Pasante pasante)
        {
            bool ok = false;

            try
            {
                _contexto.Database.ExecuteSqlRaw($"DELETE FROM RealizarTareas WHERE PasanteId={pasante.PasanteId}");
                foreach (var item in pasante.RealizarTareas)
                {
                    _contexto.Entry(item).State = EntityState.Added;
                }

                _contexto.Database.ExecuteSqlRaw($"DELETE FROM AsignarTareas WHERE PasanteId={pasante.PasanteId}");
                foreach (var item in pasante.AsignarTareas)
                {
                    _contexto.Entry(item).State = EntityState.Added;
                }

                _contexto.Entry(pasante).State = EntityState.Modified;

                ok = await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<Pasante> Buscar(int id)
        {
            Pasante pasante;

            try
            {
                pasante = await _contexto.Pasantes.Where(p => p.PasanteId == id)
                    .Include(p => p.AsignarTareas)
                    .Include(p => p.RealizarTareas)
                    .AsNoTracking().SingleOrDefaultAsync();

                var aux = _contexto.Set<Pasante>().Local.SingleOrDefault(p => p.PasanteId == id);
                if(aux != null)
                {
                    _contexto.Entry(aux).State = EntityState.Detached;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return pasante;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool ok = false;
            try
            {
                var registro = await Buscar(id);
                if(registro != null)
                {
                    _contexto.Pasantes.Remove(registro);
                    ok = await _contexto.SaveChangesAsync() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<List<Pasante>> GetPasantes(Expression<Func<Pasante, bool>> criterio)
        {
            List<Pasante> lista = new List<Pasante>();

            try
            {
                lista = await _contexto.Pasantes.Where(criterio)
                    .Include(p => p.AsignarTareas)
                    .Include(p => p.RealizarTareas)
                    .ToListAsync();

                lista.Sort((x, y) => x.Nombres.CompareTo(y.Nombres));

            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }
    }
}
