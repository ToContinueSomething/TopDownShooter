using Sources.Logic.UI.Elements;

namespace Sources.Logic
{
    public interface IHealth : IValue
    {
        void TakeDamage(int damage);
    }
}