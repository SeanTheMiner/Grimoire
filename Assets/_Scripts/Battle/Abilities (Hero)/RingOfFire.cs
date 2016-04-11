using Procs;

public class RingOfFire : HeroAbility {

    public DamageProc damageProc = new DamageProc();

    public RingOfFire() {

        abilityName = "Ring of Fire";
        abilityType = AbilityType.Burst;
        targetScope = TargetScope.FreeTargetAOE;
        primaryDamageType = DamageType.Magical;
        manaCost = 120;

        canBeDefault = false;

        chargeDuration = 4.0f;
        cooldownDuration = 12.0f;
        radiusOfAOE = 4;

        damageProc.procDamage = 200;
        damageProc.damageType = DamageProc.DamageType.Magical;

        //effectStacksApplied = 8;

    } //end Constructor()


    public override void AbilityMap() {

        CheckAOETargets();
        DetermineHitOutcomeMultiple(abilityOwner, damageProc);
        //ApplyEffectMultiple(effectApplied);
        ExitAbility();

    } //end AbilityMap()


} //end RingOfFire class
