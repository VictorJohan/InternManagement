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
    public class PasanteHabilidadesBLL
    {
        private ApplicationDbContext _contexto { get; set; }
        public PasanteHabilidadesBLL(ApplicationDbContext contexto)
        {
            this._contexto = contexto;
        }

        public async Task<bool> Guardar(PasanteHabilidade pasanteSkill)
        {
            if (!await Existe(pasanteSkill.PasanteId))
                return await Insertar(pasanteSkill);
            else
                return await Modificar(pasanteSkill);
        }

        private async Task<bool> Existe(int? id)
        {
            bool ok = false;

            try
            {
                ok = await _contexto.PasanteHabilidades.AnyAsync(p => p.PasanteId == id);
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Insertar(PasanteHabilidade pasanteSkill)
        {
            bool ok = false;

            try
            {
               await _contexto.PasanteHabilidades.AddAsync(pasanteSkill);
                ok = await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Modificar(PasanteHabilidade pasanteSkill)
        {
            bool ok = false;
            try
            {
                _contexto.Entry(pasanteSkill).State = EntityState.Modified;
                ok = await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<PasanteHabilidade> Buscar(int id)
        {
            PasanteHabilidade pasanteSkill;

            try
            {
                pasanteSkill = await _contexto.PasanteHabilidades.Where(p => p.PasanteId == id).AsNoTracking().SingleOrDefaultAsync();

                var aux = _contexto.Set<PasanteHabilidade>().Local.SingleOrDefault(p => p.PasanteId == id);
                if (aux != null)
                    _contexto.Entry(aux).State = EntityState.Detached;
            }
            catch (Exception)
            {

                throw;
            }

            return pasanteSkill;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool ok = false;
            try
            {
                var registro = await Buscar(id);
                if (registro != null)
                {
                    _contexto.PasanteHabilidades.Remove(registro);
                    ok = await _contexto.SaveChangesAsync() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<List<PasanteHabilidade>> GetPasanteHabilidades(Expression<Func<PasanteHabilidade, bool>> criterio)
        {
            List<PasanteHabilidade> lista = new List<PasanteHabilidade>();

            try
            {
                lista = await _contexto.PasanteHabilidades.Where(criterio)
                    .Include(s => s.Pasante)
                    .Include(s => s.Habilidad)
                    .ToListAsync();
                lista.Sort((x, y) => x.Pasante.Nombres.CompareTo(y.Pasante.Nombres));
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }
    }
}
