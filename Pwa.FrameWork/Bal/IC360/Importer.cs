using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Bal.IC360
{
    public class Importer
    {
       

        public void Run()
        {
            Mapper mapper = new Mapper();
            Pwa.FrameWork.Dao.ADO.IC360Dao dao = new Pwa.FrameWork.Dao.ADO.IC360Dao();
            var incidents = dao.GetImportData();

            incidents.ForEach(inc =>
            {
                try
                {
                    Console.Write($"Import {inc.INCIDENT_ID} : ");
                    var mappingIncident = mapper.GetMappingData(inc);

                    Pwa.FrameWork.Bal.Smart1662.IncidentManager manager = new FrameWork.Bal.Smart1662.IncidentManager();
                    manager.Add(mappingIncident);

                    Console.WriteLine("Success");
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"FAILED!!! {ex.Message}  {ex.StackTrace}");
                }
                

            });

          
        }
    }
}
