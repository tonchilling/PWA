using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pwa.FrameWork.Dao
{
    public class Smart1662PostgresGISCForSocialDB : Smart1662PostgresDBBase    {
        public Smart1662PostgresGISCForSocialDB(string conn)
        {
            Connection = new Npgsql.NpgsqlConnection(conn);
            conStr = conn;
          
        }

        public override bool Delete(object obj)
        {
            throw new NotImplementedException();
        }

        public override List<object> FinByAll()
        {
            throw new NotImplementedException();
        }

        public override object FindByPK(object obj)
        {
            throw new NotImplementedException();
        }

        public override bool Insert(object obj)
        {
            throw new NotImplementedException();
        }

        public override bool Update(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
