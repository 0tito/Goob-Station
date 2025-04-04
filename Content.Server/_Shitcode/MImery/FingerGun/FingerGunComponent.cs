using Robust.Shared.GameStates;

namespace Content.Server._Shitcode.Mimery;

[RegisterComponent, NetworkedComponent]
[AutoGenerateComponentState]
public sealed partial class FingerGunComponent : Component
{
    [DataField]
    [AutoNetworkedField]
    public EntityUid FingerGun;

    [DataField]
    [AutoNetworkedField]
    public bool OnHand = false;

    [DataField]
    [AutoNetworkedField]
    public bool FingerGunExists = false;

    public const int MagazineSize = 5;

    public int Shots = 5;

    public const float FireRate = 0.5f;

    public const float RechargeRate = 4f;
}

