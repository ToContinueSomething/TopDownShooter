using Sources.Infrastructure.Services;

namespace Sources.Logic.UI.Services.Factory
{
    public interface IUIFactory :  IService
    {
        void CreateMenu();
        void CreateSettings();
    }
}