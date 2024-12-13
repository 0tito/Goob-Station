using Content.Shared.Alert;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;

namespace Content.Server._Goobstation.MimeryBook
{
    /// <summary>
    /// Lets its owner entity use mime powers, like placing invisible walls.
    /// </summary>
    [RegisterComponent]
    public sealed partial class MimeryPowersComponent : Component
    {
        /// <summary>
        /// Whether this component is active or not.
        /// </summarY>
        [DataField("enabled")]
        public bool Enabled = true;

        /// <summary>
        /// The wall prototype to use.
        /// </summary>
        [DataField("wallPrototype", customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
        public string WallPrototype = "WallInvisible";

        [DataField("MimeryWallAction", customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
        public string? MimeryWallAction = "ActionMimeryWall";

        [DataField("MimeryWallActionEntity")] public EntityUid? MimeryWallActionEntity;

        [ViewVariables(VVAccess.ReadOnly)] public bool FingerGunActive = false;
        [ViewVariables(VVAccess.ReadOnly)] public bool FingerGunExists = false;

        public EntityUid FingerGun;

    }
}
