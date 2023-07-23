using System;
using RedPanda.Project.Services.Interfaces;

namespace RedPanda.Project.Services
{
    public sealed class UserService : IUserService
    {
        public  int               Currency { get; private set; }

        public UserService()
        {
            Currency = 1000;
        }

        public Action<int> OnCurrencyChanged { get; set; }

        void IUserService.AddCurrency(int delta)
        {
            Currency += delta;
            OnCurrencyChanged.Invoke(Currency);
        }

        void IUserService.ReduceCurrency(int delta)
        {
            Currency -= delta;
            OnCurrencyChanged.Invoke(Currency);
        }
        
        bool IUserService.HasCurrency(int amount)
        {
            return Currency >= amount;
        }
    }
}