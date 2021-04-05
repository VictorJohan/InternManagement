using InternManagement.Data;
using InternManagement.Models;
using InternManagement.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InternManagement.BLL
{
    public class AsignarTareasBLL
    {
        private ApplicationDbContext _contexto { get; set; }
        public AsignarTareasBLL(ApplicationDbContext contexto)
        {
            this._contexto = contexto;
        }

        public async Task<bool> Guardar(AsignarTarea pasanteTask)
        {
            if (!await Existe(pasanteTask.PasanteId))
                return await Insertar(pasanteTask);
            else
                return await Modificar(pasanteTask);
        }

        private async Task<bool> Existe(int? id)
        {
            bool ok = false;

            try
            {
                ok = await _contexto.AsignarTareas.AnyAsync(p => p.PasanteId == id);
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Insertar(AsignarTarea pasanteTask)
        {
            bool ok = false;

            try
            {
                await _contexto.AsignarTareas.AddAsync(pasanteTask);
                ok = await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Modificar(AsignarTarea pasanteTask)
        {
            bool ok = false;
            try
            {
                _contexto.Entry(pasanteTask).State = EntityState.Modified;
                ok = await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<AsignarTarea> Buscar(int id)
        {
            AsignarTarea pasanteTask;

            try
            {
                pasanteTask = await _contexto.AsignarTareas.Where(p => p.PasanteId == id).AsNoTracking().SingleOrDefaultAsync();

                var aux = _contexto.Set<AsignarTarea>().Local.SingleOrDefault(p => p.PasanteId == id);
                if (aux != null)
                    _contexto.Entry(aux).State = EntityState.Detached;
            }
            catch (Exception)
            {

                throw;
            }

            return pasanteTask;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool ok = false;
            try
            {
                var registro = await Buscar(id);
                if (registro != null)
                {
                    _contexto.AsignarTareas.Remove(registro);
                    ok = await _contexto.SaveChangesAsync() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<List<TareasViewModels>> GetAsignarTareas(Expression<Func<TareasViewModels, bool>> criterio)
        {
            List<TareasViewModels> lista = new List<TareasViewModels>();
            try
            {
                lista = await _contexto.Tareas.Where(p =>true).Select(t => new TareasViewModels()
                {
                    TareaId = t.TareaId,
                    Descripcion = t.Descripcion,
                    Requerimiento = t.Requerimiento,
                    TiempoAproximado = t.TiempoAproximado

                }).ToListAsync();
                //lista.Sort((x, y) => x.Descripcion.CompareTo(y.Descripcion));
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }

        public async Task<List<RealizarTarea>> GetTareasPendientes(int clienteId)
        {
            var pendientes = new List<RealizarTarea>();

            var TareasPendientes = await _contexto.AsignarTareas
                .Where(t => t.PasanteId == clienteId && t.TareaId > 0)
                .AsNoTracking()
                .ToListAsync();

            foreach (var item in TareasPendientes)
            {
                pendientes.Add(new RealizarTarea
                {
                    TareaId = item.TareaId,
  
                });
            }

            return pendientes;
        }
    }
}
