using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{
    internal class Car
    {
        //attributes
        private string make;
        private string model;
        private string yearOfManufacture;
        private string numberPlate;
        private string bodyType;
        private float costToRent;
        private bool isAvailable;
        private string emailOfCurrentRenter = "unavailable";
        //constructor
        public Car(string make, string model, string yearOfManufacture, string numberPlate, string bodyType, float costToRent, bool isAvailable)
        {
            this.make = make;
            this.model = model;
            this.numberPlate = numberPlate;
            this.bodyType = bodyType;
            this.yearOfManufacture = yearOfManufacture;
            this.costToRent = costToRent;
            this.isAvailable = isAvailable;
        }
        //get operations
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
        public string GetNumberPlate()
        {
            return numberPlate;
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
        public string GetEmailOfCurrentRenter()
        {
            return emailOfCurrentRenter;
        }
        //set operations
        public void SetEmailOfCurrentRenter(string email)
        {
            emailOfCurrentRenter = email;
        }
        public void SetAvailability(bool availability)
        {
            isAvailable = availability;
        }

        //this adds the instance of the car class to the binary file
        public void AddToFile(FileStream file)
        {
            BinaryWriter bw = new BinaryWriter(file);
            bw.Write(make);
            bw.Write(model);
            bw.Write(yearOfManufacture);
            bw.Write(numberPlate);
            bw.Write(bodyType);
            bw.Write(costToRent);
            bw.Write(isAvailable);
            bw.Write(emailOfCurrentRenter);
        }
    }
}
