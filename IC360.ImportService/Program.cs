using Pwa.FrameWork.Bal.Smart1662;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace IC360.ImportService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

            try
            {
                
                Pwa.FrameWork.Bal.IC360.Importer importer = new Pwa.FrameWork.Bal.IC360.Importer();

               importer.Run();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }


            /*
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);*/
        }
    }
}
