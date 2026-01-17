using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagementSystem_Markov.Classes
{
    public class TypeOperationsWindow
    {
        public string typeOperationText { get; set; }

        /// <summary> Формат операции
        /// Ссылок: 2
        public string formatText { get; set; }

        /// <summary> Цвет операции
        /// Ссылок: 5
        public string colorText { get; set; }

        /// <summary> Тип операции
        /// Ссылок: 2
        public int typeOperation { get; set; }

        /// <summary> Формат операции
        /// Ссылок: 2
        public int format { get; set; }

        /// <summary> Кол-во сторон
        /// Ссылок: 4
        public int side { get; set; }

        /// <summary> Цветная печать
        /// Ссылок: 3
        public bool color { get; set; }

        /// <summary> Прозрачность 50%
        /// Ссылок: 2
        public bool occupancy { get; set; }

        /// <summary> Кол-во страниц
        /// Ссылок: 2
        public int count { get; set; }

        /// <summary> Цена печати
        /// Ссылок: 3
        public float price { get; set; }
    }
}
