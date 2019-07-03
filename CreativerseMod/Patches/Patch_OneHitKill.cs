using BuildVerse;
using HarmonyLib;

namespace CreativerseMod.Patches
{
    [HarmonyPatch(typeof(Combat), "PlayerMeleeAttack", null)]
    internal class Patch_OneHitKill
    {
        public static void Prefix(Combat __instance)
        {
            var meleeWeapon = Player.Local.Builder.EquippedTool as MeleeWeapon;
            if (meleeWeapon != null && meleeWeapon.GetMeleeTarget() != null)
            {
                var meleeTarget = meleeWeapon.GetMeleeTarget();
                var simComponent = meleeTarget.GetSimComponent<NPC>();
                if (!meleeTarget.IsPlayer && meleeTarget.IsMob && !simComponent.IsDomesticated &&
                    LoaderConfig.Instance.InstantKill)
                    EntityManager.Instance.SendToServer(new CombatApplyDOT(Player.Local.Combat, meleeTarget.EntityID,
                        DamageType.Fall));
            }
        }
    }
}