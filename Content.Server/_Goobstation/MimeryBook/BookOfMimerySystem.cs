using Content.Shared.Interaction.Events;
using Content.Shared.Actions;
using Content.Shared.DoAfter;
using Content.Shared.Interaction.Events;
using Content.Shared.Magic;
//using Content.Shared.Magic.Components;
using Content.Shared.MimeryBook;
using Content.Shared.Mind;
using Content.Shared.Mindshield.Components;
using Robust.Shared.Network;


namespace Content.Server._Goobstation.MimeryBook;

public sealed class BookOfMimerySystem : EntitySystem
{

    [Dependency] private readonly SharedMindSystem _mind = default!;
    [Dependency] private readonly SharedDoAfterSystem _doAfter = default!;
    [Dependency] private readonly SharedActionsSystem _actions = default!;
    [Dependency] private readonly ActionContainerSystem _actionContainer = default!;
    [Dependency] private readonly INetManager _netManager = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<BookOfMimeryComponent, MapInitEvent>(OnInit);
        SubscribeLocalEvent<BookOfMimeryComponent, UseInHandEvent>(OnUse);
        SubscribeLocalEvent<BookOfMimeryComponent, MimeryBookDoAfterEvent>(OnDoAfter);
    }

    private void OnInit(Entity<BookOfMimeryComponent> ent, ref MapInitEvent args)
    {
        foreach (var (id, charges) in ent.Comp.SpellActions)
        {
            var spell = _actionContainer.AddAction(ent, id);
            if (spell == null)
                continue;

            int? charge = charges;
            if (_actions.GetCharges(spell) != null)
                charge = _actions.GetCharges(spell);

            _actions.SetCharges(spell, charge < 0 ? null : charge);
            ent.Comp.Spells.Add(spell.Value);
        }
    }

    private void OnUse(Entity<BookOfMimeryComponent> ent, ref UseInHandEvent args)
    {

        if (args.Handled)
            return;

        AttemptLearn(ent, args);

        args.Handled = true;
    }

    private void OnDoAfter<T>(Entity<BookOfMimeryComponent> ent, ref T args) where T : DoAfterEvent // Sometimes i despise this language
    {
        if (args.Handled || args.Cancelled)
            return;

        args.Handled = true;

        if (!ent.Comp.LearnPermanently)
        {
            _actions.GrantActions(args.Args.User, ent.Comp.Spells, ent);
            return;
        }

        if (_mind.TryGetMind(args.Args.User, out var mindId, out _))
        {
            var mindActionContainerComp = EnsureComp<ActionsContainerComponent>(mindId);

            if (_netManager.IsServer)
                _actionContainer.TransferAllActionsWithNewAttached(ent, mindId, args.Args.User, newContainer: mindActionContainerComp);
        }
        else
        {
            foreach (var (id, charges) in ent.Comp.SpellActions)
            {
                EntityUid? actionId = null;
                if (_actions.AddAction(args.Args.User, ref actionId, id))
                    _actions.SetCharges(actionId, charges < 0 ? null : charges);
            }
        }
        EnsureComp<MimeryPowersComponent>(args.Args.User);
        ent.Comp.SpellActions.Clear();
    }

    private void AttemptLearn(Entity<BookOfMimeryComponent> ent, UseInHandEvent args)
    {
        var doAfterEventArgs = new DoAfterArgs(EntityManager, args.User, ent.Comp.LearnTime, new MimeryBookDoAfterEvent(), ent, target: ent)
        {
            BreakOnMove = true,
            BreakOnDamage = true,
            NeedHand = true, //What, are you going to read with your eyes only??
        };

        _doAfter.TryStartDoAfter(doAfterEventArgs);
    }

}


