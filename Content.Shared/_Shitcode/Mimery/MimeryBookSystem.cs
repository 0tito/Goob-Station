using Content.Shared.Actions;
using Content.Shared.DoAfter;
using Content.Shared.Interaction.Events;
using Content.Shared.Mind;
using Robust.Shared.Network;
using Content.Shared._Shitcode.Mimery;

namespace Content.Shared._Shitcode.Mimery;

public sealed class MimeryBookSystem : EntitySystem
{
    [Dependency] private readonly SharedMindSystem _mind = default!;
    [Dependency] private readonly SharedDoAfterSystem _doAfter = default!;
    [Dependency] private readonly SharedActionsSystem _actions = default!;
    [Dependency] private readonly ActionContainerSystem _actionContainer = default!;
    [Dependency] private readonly INetManager _netManager = default!;
    public override void Initialize()
    {
        SubscribeLocalEvent<MimeryBookComponent, UseInHandEvent>(OnUse);
        //SubscribeLocalEvent<MimeryBookComponent, MapInitEvent>(OnInit);
        SubscribeLocalEvent<MimeryBookComponent, MimeryBookDoAfterEvent>(OnDoAfter);
    }

    private void OnUse(Entity<MimeryBookComponent> ent, ref UseInHandEvent args)
    {
        if (args.Handled)
            return;

        AttemptLearn(ent, args);

        args.Handled = true;
    }


    private void OnDoAfter(Entity<MimeryBookComponent> ent, ref MimeryBookDoAfterEvent args)
    {
        if (args.Handled || args.Cancelled)
            return;

        args.Handled = true;

        EnsureComp<MimeryPowersComponent>(args.User);
        RaiseLocalEvent(args.User, new MimeryPowersGrantedEvent());
    }

    private void AttemptLearn(Entity<MimeryBookComponent> ent, UseInHandEvent args)
    {
        var doAfterEventArgs = new DoAfterArgs(EntityManager, args.User, ent.Comp.LearnTime, new MimeryBookDoAfterEvent(), ent, target: ent)
        {
            BreakOnMove = true,
            BreakOnDamage = true,
            NeedHand = true, //What, are you going to read with your eyes only??
            MultiplyDelay = false, // Goobstation
            Event = new MimeryBookDoAfterEvent()
        };

        _doAfter.TryStartDoAfter(doAfterEventArgs);
    }

}
