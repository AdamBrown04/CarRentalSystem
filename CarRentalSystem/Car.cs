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
        
        public Car(string make, string model, string yearOfManufacture, string regPlate, string bodyType, float costToRent, bool isAvailable)
        {
            this.make = make;
            this.model = model;
            this.regPlate = regPlate;
            this.bodyType = bodyType;
            this.yearOfManufacture = yearOfManufacture;
            this.costToRent = costToRent;
            this.isAvailable = isAvailable;
        }

        public string GetMake()
        {
            return make;
        }
        public string GetModel()
        {
            return model;
        }
        public string GetYoM()
        {
            return yearOfManufacture;
        }
        public string GetRegPlate()
        {
            return regPlate;
        }
        public string GetBodyType()
        {
            return bodyType;
        }
        public float GetCostToRent()
        {
            return costToRent;
        }
        public bool GetAvailability()
        {
           return isAvailable;
        }
    }
}
