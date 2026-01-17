using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagementSystem_Markov.Classes
{
    public class TypeOperation
    {
        public int id {  get; set; }   
        public string name { get; set; }   
        public string description { get; set; }

        public TypeOperation(int _id, string _name, string _description) 
        {
            this.id = _id;
            this.name = _name;  
            this.description = _description;
        }
        public static List<TypeOperation> AllTypeOperation()
        {
            List<TypeOperation> allTypeOperation = new List<TypeOperation>();
            allTypeOperation.Add(new TypeOperation(1, "Печать", ""));
            allTypeOperation.Add(new TypeOperation(2, "Копия", ""));
            allTypeOperation.Add(new TypeOperation(3, "Сканирование", ""));
            allTypeOperation.Add(new TypeOperation(4, "Ризограф", ""));

            return allTypeOperation;
        }
    }
}
