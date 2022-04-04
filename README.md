![SPELL](https://github.com/emanzione/SPELL/blob/main/logo.png)

# SPELL: The Spells Framework

![Build](https://github.com/emanzione/SPELL/workflows/Build/badge.svg)
[![Nuget](https://img.shields.io/nuget/v/MHLab.Spells)](https://www.nuget.org/packages/MHLab.Spells/)

`SPELL` is a framework for describing, composing and managing your game's spells and abilities.

- __*Component based*__. Costs, requirements, effects are all independent components: you can define and reuse them as you prefer
- __*Code first*__. Spells and components are defined as C# scripts: no need for external configuration files or different formats, the compiler will ensure the correctness of the spell definition
- __*Engine agnostic*__. It's just raw C#, it does not depend on any particular game engine

Its core is open source, so you can contribute and freely experiment with it.

But soon a set of samples and ready-to-use components will be for sale on Unity Asset Store.

## Create your first spell

Creating a new Spell is easy. You just need to implement `ISpellDefinition`:

```csharp
public class MySpellDefinition : ISpellDefinition
{
    public void SetMetadata(Spell spell)
    {
        // Set metadata here: like Cooldown, etc.
    }

    public void AddRequirements(SpellRequirementContainer container)
    {
        // Add requirements for the spell here, like enough mana, selected targets, etc.
        // If one of the requirements is not met, the spell cast fails.
        container.Add(new CasterHasEnoughMana(5));
    }

    public void AddCosts(SpellCostContainer container)
    {
        // Add costs for the spell here. Subtract mana from the caster, HP, gold, etc.
        container.Add(new ManaSpellCost(5));
    }

    public void AddEffects(SpellEffectContainer container)
    {
        // Add the effects here. What happens when the spell is successfully casted? Dealing damage, buffing/debuffing the targets, etc.
        container.Add<DirectDamage>();
    }
}
```

You can use the constructor to inject dependencies, if needed.

## Create the context

A `SpellsContext` is like the main container for your spells (and their runtime data). Just create a new instance of `SpellsContext` or inherit from it:

```csharp
public class MySpellsContext : SpellsContext
{
    public MySpellsContext() : base()
    {
        Spells.Add<MySpellDefinition>();
        // OR (if you need to pass parameters to the constructor):
        Spells.Add(new MySpellDefinition(myDependency));
    }
}
```

The context needs to be ticked in your game loop. For example, in Unity it is as simple as:

```csharp
private void Update()
{
    _spellsContext.Update(Time.deltaTime);
}
```

## Spell Components

Spells are composed by components. In particular: requirements, costs and effects.

### Requirements

You can create a requirement component by implementing `ISpellRequirement`:

```csharp
public class CasterHasEnoughMana : ISpellRequirement
{
    private const string RequirementErrorCode = "CASTER_HAS_NOT_ENOUGH_MANA";
    
    private readonly uint _requiredMana;

    public CasterHasEnoughMana(uint requiredMana)
    {
        _requiredMana = requiredMana;
    }
    
    public CheckRequirementResult IsMet(ISpellCaster caster, IEnumerable<ISpellTarget> targets, SpellDefinition spellDefinition, SpellsContext context)
    {
        var player = (MyPlayer)caster;

        if (player.Mana >= _requiredMana)
        {
            return new CheckRequirementResult()
            {
                Result = true,
                Error  = null
            };
        }

        return new CheckRequirementResult()
        {
            Result = false,
            Error  = RequirementErrorCode
        };
    }
}
```

### Costs

You can create a cost component by implementing `ISpellCost`:

```csharp
public class ManaSpellCost : ISpellCost
{
    private readonly uint _requiredMana;

    public ManaSpellCost(uint requiredMana)
    {
        _requiredMana = requiredMana;
    }
    
    public void ApplyCost(ISpellCaster caster, IEnumerable<ISpellTarget> targets, SpellDefinition spellDefinition)
    {
        var player = (MyPlayer)caster;

        player.Mana -= _requiredMana;
    }
}
```

### Effects

You can create an effect component by implementing `ISpellEffect`:

```csharp
public class DirectDamage : ISpellEffect
{    
    public void Setup(SpellEffectInstance effectInstance)
    {
    }

    public SpellEffectContinuationState ApplyEffect(SpellEffectInstance effectInstance, float deltaTime)
    {
        var player = (MyPlayer)effectInstance.SpellInstance.Caster;

        foreach (var target in effectInstance.SpellInstance.Targets)
        {
            var enemy = (MyTarget)target;
            enemy.ApplyDamage(player.Damage);
        }

        return SpellEffectContinuationState.Complete;
    }

    public void CleanUp(SpellEffectInstance effectInstance)
    {
    }
}
```

## Casting the spell

Defining your spell is useless if you cannot unleash it against your enemies! The framework offers a way to cast a spell:

```csharp

var castResult = _spellsContext.CasterSystem.Cast(myPlayer, myTargets, mySpellDefinition, out var spellInstance);
```

You can modify some options of the CasterSystem by accessing its Options property:

```csharp
// Regulates the requirements check. Useful for debugging or cheating. Default = true
_spellsContext.CasterSystem.Options.CheckRequirements
// Regulates the costs applying. Useful for debugging or cheating. Default = true
_spellsContext.CasterSystem.Options.ApplyCosts
```

## Tests

Check the [Tests](https://github.com/emanzione/SPELL/tree/main/MHLab.Spells.Tests) folder to see more samples.

## Roadmap

- Considering the feedback from the beta testers
- Completing the package for the Asset Store. It contains samples and ready-to-use components
- Optimizing: prepare a load test, profile it, remove runtime allocations, etc

## Contributing

Feel free to open PR or leaving feedback through the Issues. The framework is still in beta, we can shape it together!

If you want to chat with the community, feel free to join our [Discord server](https://discord.gg/jpteW4yaUN).

## License

The core of this framework is distributed under the terms of MIT license. You can use it for free, but please: if you adopt it in a commercial project, consider mentioning me (and even buying the full package from the Asset Store! :) ).
