using Sources.Logic.UI.Services.Factory;

namespace Sources.Logic.UI.Services.Windows
{
  public class WindowService : IWindowService
  {
    private readonly IUIFactory _uiFactory;

    public WindowService(IUIFactory uiFactory)
    {
      _uiFactory = uiFactory;
    }

    public void Open(WindowId windowId)
    {
      switch (windowId)
      {
        case WindowId.None:
          break;
        case WindowId.Settings:
          _uiFactory.CreateSettings();
          break;
      }
    }
  }
}