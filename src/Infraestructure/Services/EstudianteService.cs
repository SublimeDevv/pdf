using Dapper;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using Domain.Entities;
using Infraestructure.Persistence;
using Domain.ViewModel;
using Microsoft.EntityFrameworkCore;
using DevExpress.DataAccess.ObjectBinding;
using ApplicationCore.VistasPDF;

namespace Infraestructure.Services
{
    public class EstudianteService : IEstudianteService
    {

        private readonly ApplicationDbContext _dbContext;

        public EstudianteService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Response<object>> CrearEstudiante(EstudianteDTO estudiante)
        {
            Response<object> response = new();

            try
            {
                var estudianteEntity = new Estudiante
                {
                    Nombre = estudiante.Nombre,
                    Correo = estudiante.Correo,
                    Edad = estudiante.Edad
                };

                await _dbContext.AddAsync(estudianteEntity);

                var result = await _dbContext.SaveChangesAsync();

                if (result > 0)
                {
                    response.Result = "Estudiante creado correctamente";
                }
                else
                {
                    response.Result = "Error al crear el estudiante";
                }
            }
            catch (Exception e)
            {
                response.Result = $"Error: {e.Message}";
            }

            return response;
        }


        public async Task<Response<object>> GetEstudiantes(int edad)
        {
          Response<object> response = new();

            try
            {
                var sql = @"SELECT Correo, Edad FROM Estudiantes WHERE Edad = @edad";
                var result = await _dbContext.Database.GetDbConnection().QueryAsync<EstudianteVM>(sql, new { edad });

                if (result != null)
                {
                    response.Result = result;
                }
                else
                {
                    response.Result = "No se encontraron estudiantes";
                }
            }
            catch (Exception e)
            {
                response.Result = $"Error: {e.Message}";
            }

            return response;

        }


        public async Task<Response<object>> DeleteEstudiante(int id)
        {
            Response<object> response = new();
            try
            {
                var sql = @"DELETE FROM Estudiantes WHERE Id = @id";
                var result = await _dbContext.Database.GetDbConnection().ExecuteAsync(sql, new { id });

                if (result > 0)
                {
                    response.Result = result;
                    response.Message = "Se eliminó correctamente el estudiante";
                }
                else
                {
                    response.Message = "No se eliminó el estudiante";
                }

            } catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<Response<object>> UpdateEstudiante(EstudianteDTO estudiante)
        {
            Response<object> response = new();
            try
            {
                var sql = @"UPDATE Estudiantes SET Nombre = @Nombre, Correo = @Correo, Edad = @Edad WHERE Id = @Id";
                var result = await _dbContext.Database.GetDbConnection().ExecuteAsync(sql, new { estudiante.Nombre, estudiante.Correo, estudiante.Edad, estudiante.Id });

                if (result > 0)
                {
                    response.Result = result;
                    response.Message = "Se actualizó correctamente el estudiante";
                }
                else
                {
                    response.Message = "No se actualizó el estudiante";
                }

            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

                return response;
            }


        public async Task<byte[]> GetEstudiantePDF()
        {
            ObjectDataSource source = new();
            var reporte = new EstudiantePDF();

            try
            {
                var sql = @"SELECT * FROM Estudiantes";
                var result = await _dbContext.Database.GetDbConnection().QueryAsync<Estudiante>(sql);

                EstudiantesListVM estudiante = new();

                estudiante.Fecha = DateTime.Now.ToString("dd/MM/yy");
                estudiante.Hora = DateTime.Now.ToString("HH:mm:ss");
                estudiante.Estudiante = result.ToList();

                source.DataSource = estudiante;
                reporte.DataSource = source;

                using (var memoryStream = new System.IO.MemoryStream())
                {
                    reporte.ExportToPdf(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            catch (Exception e)
            {
                return null;
            }


        }

        }
}
