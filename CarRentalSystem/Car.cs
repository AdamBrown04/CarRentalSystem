using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{
    internal class Car
    {
        private string make;
        private string model;
        private string yearOfManufacture;
        private string regPlate;
        private string bodyType;
        private float costToRent;
        private bool isAvailable;     
        
        public void ViewMake()
        {
            Console.Write(make);
        }
        public void ViewModel()
        {
            Console.Write(model);
        }
        public void ViewYoM()
        {
            Console.Write(yearOfManufacture);
        }
        public void ViewRegPlate()
        {
            Console.Write(regPlate);
        }
        public void ViewBodyType()
        {
            Console.Write(bodyType);
        }
        public void ViewCostToRent()
        {
            Console.Write(costToRent);
        }
        public void ViewAvailability()
        {
            if (isAvailable)
            {
                Console.Write("Is available to rent");
            }
            else
            {
                Console.Write("Is unavailable to rent");
            }
        }

        public void GetMake(string Make)
        {
            make = Make;
        }
        public void GetModel(string Model)
        {
            model = Model;
        }
        public void GetYoM(string YearOfManufacture)
        {
            yearOfManufacture = YearOfManufacture;
        }
        public void GetRegPlate(string Reg)
        {
            regPlate = Reg;
        }
        public void GetBodyType(string BodyType)
        {
            bodyType = BodyType;
        }
        public void GetCostToRent(float rentCost)
        {
            costToRent = rentCost;
        }
        public void GetAvailabilty(bool availability)
        {
            isAvailable = availability;
        }
    }
}
