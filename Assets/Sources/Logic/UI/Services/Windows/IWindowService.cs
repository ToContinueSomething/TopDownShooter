using Sources.Infrastructure.Services;

namespace Sources.Logic.UI.Services.Windows
{
  public interface IWindowService : IService
  {
    void Open(WindowId windowId);
  }
}