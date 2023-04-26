using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMV.DTO.ModelData;

namespace TMV.DTO.Tr
{
    public class Request_TransportationRecords : ModelDTO
    {
        [DefaultValue("状态")]
        public TransportationRecordsState? State { get; set; }

    }
    public enum TransportationRecordsState
    {
        [DefaultValue("正常")]
        正常 = 1,
        [DefaultValue("告警")]
        告警 = 2 
    }
    public class TransportationRecordsModel
    {
         

    }

    public class TransportationRecordsDTO
    { 
    }
}
