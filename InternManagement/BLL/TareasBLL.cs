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
    public class TareasBLL
    {
        private ApplicationDbContext _contexto { get; set; }

        public TareasBLL(ApplicationDbContext contexto)
        {
            this._contexto = contexto;
        }

        public async Task<bool> Guardar(Tarea tarea)
        {
            if (!await Existe(tarea.TareaId))
                return await Insertar(tarea);
            else
                return await Modificar(tarea);
        }

        private async Task<bool> Existe(int tareaId)
        {
            bool ok = false;

            try
            {
                ok = await _contexto.Tareas.AnyAsync(p => p.TareaId == tareaId);
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Insertar(Tarea tarea)
        {
            bool ok = false;

            try
            {
                tarea.FechaCreacion = DateTime.Now;
                await _contexto.Tareas.AddAsync(tarea);
                ok = await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Modificar(Tarea tarea)
        {
            bool ok = false;

            try
            {
                _contexto.Database.ExecuteSqlRaw($"DELETE FROM RealizarTareas WHERE TareaId={tarea.TareaId}");
                foreach (var item in tarea.RealizarTareas)
                {
                    _contexto.Entry(item).State = EntityState.Added;
                }

                _contexto.Database.ExecuteSqlRaw($"DELETE FROM AsignarTareas WHERE TareaId={tarea.TareaId}");
                foreach (var item in tarea.AsignarTareas)
                {
                    _contexto.Entry(item).State = EntityState.Added;
                }

                _contexto.Entry(tarea).State = EntityState.Modified;

                ok = await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<Tarea> Buscar(int id)
        {
            Tarea tarea;

            try
            {
                tarea = await _contexto.Tareas.Where(t => t.TareaId == id)
                    .Include(p => p.AsignarTareas)
                    .Include(p => p.RealizarTareas)
                    .AsNoTracking().SingleOrDefaultAsync();

                var aux = _contexto.Set<Tarea>().Local.SingleOrDefault(t => t.TareaId == id);
                if (aux != null)
                {
                    _contexto.Entry(aux).State = EntityState.Detached;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return tarea;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool ok = false;
            try
            {
                var registro = await Buscar(id);
                if (registro != null)
                {
                    _contexto.Tareas.Remove(registro);
                    ok = await _contexto.SaveChangesAsync() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<List<Tarea>> GetTareas(Expression<Func<Tarea, bool>> criterio)
        {
            List<Tarea> lista = new List<Tarea>();

            try
            {
                lista = await _contexto.Tareas.ToListAsync();

                //lista = await _contexto.Tareas.Where(criterio)
                //.Include(p => p.AsignarTareas)
                //.Include(p => p.RealizarTareas)
                //.ToListAsync();

                //lista.Sort((x, y) => x.Descripcion.CompareTo(y.Descripcion));

            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }
    }
}
