using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class TareaController : Controller
    {
        public IActionResult GetAll()
        {
            Dictionary<string, object> result = BL.Tarea.GetAll();
            bool rs = (bool)result["Resultado"];

            if (rs)
            {
                ML.Tarea tarea = (ML.Tarea)result["Tarea"];
                return View(tarea);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Form(int? idTarea)
        {
            ML.Tarea tarea = new ML.Tarea();

            if(idTarea > 0)
            {
                Dictionary<string, object> result = BL.Tarea.GetById(idTarea.Value);
                bool rs = (bool)result["Resultado"];

                if (rs)
                {
                    tarea = (ML.Tarea)result["Tarea"];
                    return View(tarea);
                }
                else
                {
                    ViewBag.Mensaje = "No hay datos";
                    return PartialView("Modal");
                }
            }
            else
            {
                return View(tarea);
            }
        }

        [HttpPost]
        public IActionResult Form(ML.Tarea tarea)
        {

            if (tarea.IdTarea > 0)
            {
                Dictionary<string, object> result = BL.Tarea.Update(tarea);
                bool rs = (bool)result["Resultado"];

                if (rs)
                {
                    ViewBag.Mensaje = "Tarea actualizada";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "Tarea no actualizada";
                    return PartialView("Modal");
                }
            }
            else
            {
                Dictionary<string,object>result1 = BL.Tarea.Add(tarea);
                bool rss = (bool)result1["Resultado"];
                if(rss)
                {
                    ViewBag.Mensaje = "Tarea agregada";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "Tarea no agregada";
                    return PartialView("Modal");
                }
            }
        }


        public IActionResult Delete(int tarea)
        {
            Dictionary<string, object> result = BL.Tarea.Delete(tarea);
            bool rs = (bool)result["Resultado"];

            if (rs)
            {
                ViewBag.Mensaje = "Tarea Eliminada";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Mensaje = "Tarea no Eliminada";
                return PartialView("Modal");
            }
        }


        [HttpPost]
        public JsonResult ChangeStatus(int idEstado, bool estado)
        {
            Dictionary<string, object> resultado = BL.Tarea.ChangeStatus(idEstado, estado);
            bool result = (bool)resultado["Resultado"];
            return Json(result);
        }


    }
}
