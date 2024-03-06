using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Tarea
    {
        public static Dictionary<string,object>Add(ML.Tarea tarea)
        {
            string msg = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> (){ { "Resultado", false }, { "Mensaje", msg } };

            try
            {
                using (DL.ListaTareasContext context = new DL.ListaTareasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AgregarTarea '{tarea.Titulo}','{tarea.Descripcion}','{tarea.FechaVencimiento:yyyy-MM-dd}'");

                    if(query > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }catch(Exception e)
            {
                msg = e.Message;
                diccionario["Mensaje"] = msg;
            }
            return diccionario;
        }

        public static Dictionary<string, object> GetAll()
        {
            ML.Tarea tarea = new ML.Tarea();
            string msg = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { { "Resultado", false }, { "Mensaje", msg }, { "Tarea", tarea } };

            try
            {
                using (DL.ListaTareasContext context = new DL.ListaTareasContext())
                {
                    var query = (from tare in context.Tareas
                                 select new
                                 {
                                     IdTarea = tare.IdTarea,
                                     Titulo = tare.Titulo,
                                     Descripcion = tare.Descripcion,
                                     FechaVencimineto = tare.FechaVencimiento,
                                     Estado = tare.Estado

                                 }).ToList();

                    if(query != null)
                    {
                        tarea.Tareas = new List<ML.Tarea>();
                        foreach(var item in query)
                        {
                            ML.Tarea tarea1 = new ML.Tarea();
                            tarea1.IdTarea = item.IdTarea;
                            tarea1.Titulo = item.Titulo;
                            tarea1.Descripcion = item.Descripcion;
                            tarea1.FechaVencimiento = (DateTime)item.FechaVencimineto;

                            if(item.Estado == null)
                            {
                                tarea1.Estado = false;
                            }
                            else
                            {
                                tarea1.Estado = (bool)item.Estado.Value;
                            }
                            tarea.Tareas.Add(tarea1);
                        }

                        diccionario["Resultado"] = true;
                        diccionario["Tarea"] = tarea;
                    }


                }
            }catch( Exception e ) { 
                msg = e.Message;
                diccionario["Mensaje"] = msg;
            }

            return diccionario;
        }

        public static Dictionary<string, object> GetById(int idTarea)
        {
            ML.Tarea tarea = new ML.Tarea();
            string msg = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { { "Resultado", false }, { "Mensaje", msg }, { "Tarea", tarea } };

            try
            {
                using (DL.ListaTareasContext context = new DL.ListaTareasContext())
                {
                    var query = (from tare in context.Tareas where tare.IdTarea == idTarea
                                 select new
                                 {
                                     IdTarea = tare.IdTarea,
                                     Titulo = tare.Titulo,
                                     Descripcion = tare.Descripcion,
                                     FechaVencimineto = tare.FechaVencimiento,
                                     Estado = tare.Estado
                                 }).First();

                    if (query != null)
                    {
                        tarea.IdTarea = query.IdTarea;
                        tarea.Titulo = query.Titulo;
                        tarea.Descripcion = query.Descripcion;
                        tarea.FechaVencimiento = (DateTime)query.FechaVencimineto;
                        tarea.Estado = (bool)query.Estado;

                        diccionario["Resultado"] = true;
                        diccionario["Tarea"] = tarea;
                    }
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
                diccionario["Mensaje"] = msg;
            }

            return diccionario;
        }

        public static Dictionary<string, object> Delete(int idTarea)
        {
            string msg = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado",false},{"Mensaje",msg}};

            try
            {
                using (DL.ListaTareasContext context = new DL.ListaTareasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EliminarTarea {idTarea}");

                    if(query > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }catch(Exception e) { 
                msg = e.Message;

                diccionario["Mensaje"] = msg;
            }

            return diccionario;
        }

        public static Dictionary<string, object> Update(ML.Tarea tarea)
        {
            string msg = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Mensaje", msg } };

            try
            {
                using (DL.ListaTareasContext context = new DL.ListaTareasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EditarTarea {tarea.IdTarea},'{tarea.Titulo}','{tarea.Descripcion}','{tarea.FechaVencimiento:yyyy-MM-dd}'");

                    if (query > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }

            }catch(Exception e) {

                msg = e.Message;
                diccionario["Mensaje"] = msg;
            }

            return diccionario;
        }


        public static Dictionary<string, object> ChangeStatus(int idEstado, bool estado)
        {
            string mensaje = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Error", mensaje } };

            try
            {
                using (DL.ListaTareasContext context = new DL.ListaTareasContext())
                {
                    var filas = context.Database.ExecuteSqlRaw($"TareaChangeStatus {idEstado}, {estado}");

                    if (filas > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }

            }
            catch (Exception e)
            {
                diccionario["Mensaje"] = e.Message;
            }

            return diccionario;
        }
    }
}
