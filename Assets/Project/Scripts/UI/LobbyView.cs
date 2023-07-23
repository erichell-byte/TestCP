using RedPanda.Project.Services;
using RedPanda.Project.Services.Interfaces;

namespace RedPanda.Project.UI
{
    public sealed class LobbyView : View
    {
        public void CloseLobby()
        {
            UIService.Close("_");
        }

        public void OpenPromo()
        {
            UIService.Show("PromoView");
        }
    }
}