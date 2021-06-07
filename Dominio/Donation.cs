using System;

namespace DomainDonation
{
    public class Donation
    {
        private string _typeOfDonation;
        private string _description;
        private DateTime _dateOfRegister;
        private bool _state;
        private double _courrierTax;
        private int _quantityDonation;

        public Donation(string typeOfDonation, string description, DateTime dateOfRegister, bool state, double courrierTax, int quantityDonation) 
        {
            _typeOfDonation = typeOfDonation;
            _description = description;
            _dateOfRegister = dateOfRegister;
            _state = state;
            _courrierTax = courrierTax;
            _quantityDonation = quantityDonation;
        }
 
        public string GetTypeOfDonation()
        {
            return _typeOfDonation;
        }

        public int GetQuantityItems()
        {
            return _quantityDonation;
        }

        public string GetDescription()
        {
            return _description;
        }

        public DateTime GetDateOfRegister()
        {
            return _dateOfRegister;
        }

        public bool GetState()
        {
            return _state;
        }

        public double GetCourrierTax()
        {
            return _courrierTax;
        }


        public int GetTimeOfRegister()
        {
            return SetTotalDaysInRegister(_dateOfRegister);
        }

        private int SetTotalDaysInRegister(DateTime dateOfRegister)
        {
            var today = DateTime.Now.Date;
            var registryDay = dateOfRegister.Date;
            var differenceOfDays = today - registryDay;
            var totalDaysInRegister = differenceOfDays.TotalDays;
            
            return (int)totalDaysInRegister;
        }

    }
}
