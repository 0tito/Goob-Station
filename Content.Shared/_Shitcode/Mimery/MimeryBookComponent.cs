using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._Shitcode.Mimery;

[RegisterComponent, NetworkedComponent]

public sealed partial class MimeryBookComponent : Component
{
    [DataField]
    [ViewVariables(VVAccess.ReadWrite)]
    public float LearnTime = 2f;
}
