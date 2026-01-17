using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagementSystem_Markov.Classes
{
    public class OperationRecord : TypeOperationsWindow
    {
        /// <summary> Дата операции
        public DateTime OperationDate { get; set; }

        /// <summary> Имя пользователя
        public string UserName { get; set; }

        public OperationRecord()
        {
            OperationDate = DateTime.Now;
        }

        public OperationRecord(TypeOperationsWindow operation, string userName)
        {
            typeOperationText = operation.typeOperationText;
            formatText = operation.formatText;
            colorText = operation.colorText;
            typeOperation = operation.typeOperation;
            format = operation.format;
            side = operation.side;
            color = operation.color;
            occupancy = operation.occupancy;
            count = operation.count;
            price = operation.price;
            OperationDate = DateTime.Now;
            UserName = userName;
        }
    }
}
