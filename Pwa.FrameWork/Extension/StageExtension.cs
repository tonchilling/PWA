
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pwa.FrameWork.Bal.PwaSystem;

namespace Pwa.FrameWork.Extension
{
    public static class StageExtension
    {
        public static int GetInt(this ProcessStage stage)
        {
            return (int)stage;
        }

        public static CustomerProcessStage GetCustomerProcessStage(this ProcessStage stage)
        {
            if (stage.GetInt() == 0)
            {
                return CustomerProcessStage.UNKNOW;
            }
            else if (stage.GetInt() >= 1 && stage.GetInt() <= 8)
            {
                return CustomerProcessStage.PROCESSING;
            }
            else if (stage.GetInt() == 9)
            {
                return CustomerProcessStage.COMPLETED_CASE;
            }
            else if (stage.GetInt() >= 10)
            {
                return CustomerProcessStage.CANCEL_CASE;
            }
            else
            {
                return CustomerProcessStage.UNKNOW;
            }
        }


        public static int GetInt(this CustomerProcessStage stage)
        {
            return (int)stage;
        }
    }
}
