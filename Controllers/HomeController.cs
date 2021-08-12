using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChallengeSuperLiga.Models;
using Model;
using Services;

namespace ChallengeSuperLiga.Controllers
{
    public class HomeController : Controller
    {
        private HomeService homeService;
        public static HttpPostedFileBase httpPostedFileBase;

        public HomeController()
        {

        }

        public ActionResult Index()
        {
            return View(this.GetSocioViewModel(0,""));
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase File)
        {
            var viewModel = this.GetSocioViewModel(0,"");
            try
            {
                if (File != null && File.ContentLength > 0)
                {
                    string filePath = string.Empty;

                    if (File != null)
                    {
                        string path = Server.MapPath("~/FilesUploads/");//guardo el archivo en el directorio de Web

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        filePath = path + Path.GetFileName(File.FileName);
                        string extension = Path.GetExtension(File.FileName);

                        //guarda la ruta relativa en el viewModel para no mostrar en la vista ni scripts la ruta absoluta del servidor
                        File.SaveAs(filePath);

                        viewModel.File = "~/FilesUploads/" + Path.GetFileName(File.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return View(viewModel);
        }

        public ActionResult PartialView1(string stringCSV)
        {
            return PartialView("PartialView1", this.GetSocioViewModel(1, stringCSV)) ;
        }

        public ActionResult PartialView2(string stringCSV)
        {
            return PartialView("PartialView2", this.GetSocioViewModel(2, stringCSV));
        }

        public ActionResult PartialView3(string stringCSV)
        {
            return PartialView("PartialView3", this.GetSocioViewModel(3, stringCSV));
        }

        public ActionResult PartialView4(string stringCSV)
        {
            return PartialView("PartialView4", this.GetSocioViewModel(4, stringCSV));
        }

        public ActionResult PartialView5(string stringCSV)
        {
            return PartialView("PartialView5", this.GetSocioViewModel(5, stringCSV));
        }

        private SocioViewModel GetSocioViewModel(int desafio, string directory)
        {
            SocioViewModel socioViewModel = new SocioViewModel();

            if (!string.IsNullOrEmpty(directory))
            {
               var stringCSV = this.GetStringCSV(directory);

                switch (desafio)
                {
                    case 0:

                        break;
                    case 1:
                        homeService = new HomeService(stringCSV);
                        socioViewModel.CantPersonasRegistradas = homeService.GetCantPersonasRegistradas();
                        break;

                    case 2:
                        homeService = new HomeService(stringCSV);
                        socioViewModel.PromedioEdad = homeService.GetPromEdadRacing();
                        break;

                    case 3:
                        homeService = new HomeService(stringCSV);
                        socioViewModel.CasadosUniversitarios = homeService.GetCasados();
                        break;

                    case 4:
                        homeService = new HomeService(stringCSV);
                        socioViewModel.NombresRiver = homeService.GetNombresRiver();
                        break;

                    case 5:
                        homeService = new HomeService(stringCSV);
                        socioViewModel.SociosEdad = homeService.GetSociosEdad();
                        break;
                }
            }

            return socioViewModel;
        }

        private string GetStringCSV(string File)
        {
            string csvString = "";
            if (!string.IsNullOrEmpty(File))
            {
                //después de obtener la ruta relativa, se mapea desde el servidor para encotrar la absoluta y el archivo
                csvString = System.IO.File.ReadAllText(Server.MapPath(File), System.Text.Encoding.Default);
            }
            return csvString;
        }

    }
}