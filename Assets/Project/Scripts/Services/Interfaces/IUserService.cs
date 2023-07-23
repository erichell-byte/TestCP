using System;

namespace RedPanda.Project.Services.Interfaces
{
    public interface IUserService
    {
        Action<int> OnCurrencyChanged { get; set; }
        void AddCurrency(int delta);
        void ReduceCurrency(int delta);
        bool HasCurrency(int amount);
    }
}