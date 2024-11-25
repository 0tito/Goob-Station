using Content.Server.Popups;
using Content.Shared.Abilities.Mime;
using Content.Shared.Actions;
using Content.Shared.Actions.Events;
using Content.Shared.Alert;
using Content.Shared.Coordinates.Helpers;
using Content.Shared.Maps;
using Content.Shared.Physics;
using Robust.Shared.Containers;
using Robust.Shared.Map;
using Robust.Shared.Timing;
using Content.Shared.Speech.Muting;

namespace Content.Server._Goobstation.MimeryBook
{
    public sealed class MimeryPowersSystem : EntitySystem
    {
        [Dependency] private readonly PopupSystem _popupSystem = default!;
        [Dependency] private readonly SharedActionsSystem _actionsSystem = default!;
        [Dependency] private readonly AlertsSystem _alertsSystem = default!;
        [Dependency] private readonly TurfSystem _turf = default!;
        [Dependency] private readonly IMapManager _mapMan = default!;
        [Dependency] private readonly SharedContainerSystem _container = default!;
        [Dependency] private readonly IGameTiming _timing = default!;

        public override void Initialize()
        {
            base.Initialize();
            SubscribeLocalEvent<MimeryPowersComponent, ComponentInit>(OnComponentInit);
            //SubscribeLocalEvent<MimeryPowersComponent, InvisibleWallActionEvent>(OnInvisibleWall);

        }

        public override void Update(float frameTime)
        {
            base.Update(frameTime);


            var query = EntityQueryEnumerator<MimeryPowersComponent>();

        }

        private void OnComponentInit(EntityUid uid, MimeryPowersComponent component, ComponentInit args)
        {
            EnsureComp<MutedComponent>(uid);
            _actionsSystem.AddAction(uid, ref component.MimeryeWallActionEntity, component.MimeryWallAction, uid);
        }


    }
}
