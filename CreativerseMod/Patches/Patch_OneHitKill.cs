using System;
using BuildVerse;
using Harmony;

namespace CreativerseMod.Patches
{
    [HarmonyPatch(typeof(Combat), "PlayerMeleeAttack", null)]
    internal class Patch_OneHitKill
    {
        public static void Prefix(Combat __instance)
        {
            MeleeWeapon meleeWeapon = Player.Local.Builder.EquippedTool as MeleeWeapon;
            if (meleeWeapon != null && meleeWeapon.GetMeleeTarget() != null)
            {
                Entity meleeTarget = meleeWeapon.GetMeleeTarget();
                NPC simComponent = meleeTarget.GetSimComponent<NPC>();
                if (!meleeTarget.IsPlayer && meleeTarget.IsMob && !simComponent.IsDomesticated && LoaderConfig.Instance.InstantKill)
                {
                    EntityManager.Instance.SendToServer(new CombatApplyDOT(Player.Local.Combat, meleeTarget.EntityID, DamageType.Fall, false));
                }
            }
        }
    }
}