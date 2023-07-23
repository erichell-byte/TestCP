using System.Collections.Generic;
using System.Linq;
using RedPanda.Project.Data;
using RedPanda.Project.Interfaces;
using RedPanda.Project.Services.Interfaces;
using UnityEngine;

namespace RedPanda.Project.UI
{
    public class PromoView : View
    {
        [SerializeField]
        private Transform chestsParent;
        
        [SerializeField]
        private Transform crystalsParent;
        
        [SerializeField]
        private Transform specialsParent;

        [SerializeField]
        private PromoElement elementTemplate;
        
        [SerializeField, Space]
        private Wallet wallet;
        
        private IReadOnlyList<IPromoModel> _promos;

        private List<IPromoModel> _chests   = new List<IPromoModel>();
        private List<IPromoModel> _crystals =  new List<IPromoModel>();
        private List<IPromoModel> _specials = new List<IPromoModel>();
        
        private void Start()
        {
            var promoService = Container.Locate<IPromoService>();
            _promos = promoService.GetPromos();

            var userService = Container.Locate<IUserService>();
            wallet.Init(userService);
            
            Init();
            
        }

        private void Init()
        {
            FillPromoLists();
            SortPromoListByRarity();
            ShowPromoItems();
        }
        
        private void FillPromoLists()
        {
            if (_promos == null) return;
            
            foreach (var promo in _promos)
            {
                switch (promo.Type)
                {
                    case PromoType.Chest:
                        _chests.Add(promo);
                        break;
                    case PromoType.Special:
                        _specials.Add(promo);
                        break;
                    case PromoType.InApp:
                        _crystals.Add(promo);
                        break;
                }
            }
        }
        
        private void SortPromoListByRarity()
        {
            _chests = _chests.OrderBy(e => e.Rarity).ToList();
            _chests.Reverse();
            _crystals = _crystals.OrderBy(e => e.Rarity).ToList();
            _crystals.Reverse();
            _specials = _specials.OrderBy(e => e.Rarity).ToList();
            _specials.Reverse();
        }
        
        private void ShowPromoItems()
        {
            ShowElements(_chests, chestsParent);
            ShowElements(_crystals, crystalsParent);
            ShowElements(_specials, specialsParent);
        }

        private void ShowElements(List<IPromoModel> elements, Transform parent)
        {
            foreach (var element in elements)
            {
                var newElement = Instantiate(elementTemplate, parent);
                newElement.Init(element);
                newElement.OnButtonClick(() => TryToBuyPromo(element.Cost, element.Title));
            }
        }
        private void TryToBuyPromo(int cost, string elementName)
        {
            elementName = elementName.Replace("\n", "");
            
            var userService = Container.Locate<IUserService>();
            if (userService.HasCurrency(cost))
            {
                userService.ReduceCurrency(cost);
                Debug.Log($"{elementName} has been buy successfully");
            }
            else
            {
                Debug.LogError($"{elementName} was not bought");
            }
        }
    }
}