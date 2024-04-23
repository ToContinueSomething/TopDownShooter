using Sources.Logic.Player.Weapon;

namespace Sources.Logic.UI.Elements
{
    public class MagazineWeaponView : View<IWeapon>
    {
        protected override void OnValueChanged()
        {
            Text.text = $"x {Interface.CurrentValue} / {Interface.MaxValue}";
        }
    }
}