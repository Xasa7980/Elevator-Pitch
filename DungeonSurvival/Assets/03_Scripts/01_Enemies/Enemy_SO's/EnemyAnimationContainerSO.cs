using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAnimationsContainer", menuName = "Dungeon Survival/Animations/EnemyAnimations")]
public class EnemyAnimationContainerSO : AnimationClipContainerSO
{
    public AnimationClip deathAnimationClip;
    public AnimationClip idleAnimationClip;
    public AnimationClip idleCombatAnimationClip;
    public AnimationClip runForwardAnimationClip;
    public AnimationClip walkForwardAnimationClip;

    public const string DEATH_ANIMATION = "Death_Animation";
    public const string IDLE_ANIMATION = "Idle_Animation";
    public const string IDLECOMBAT_ANIMATION = "IdleCombat_Animation";
    public const string RUN_FORWARD_ANIMATION = "Run_Forward_Animation";
    public const string WALK_FORWARD_ANIMATION = "Walk_Forward_Animation";

    public void ChangeCurrentBasicAnimations ( AnimatorOverrideController animatorOverrideController, MonsterStats monsterStats )
    {
        LimitMonsterAnimationsByMonsterRank(monsterStats);

        animatorOverrideController[DEATH_ANIMATION] = deathAnimationClip;
        animatorOverrideController[IDLE_ANIMATION] = idleAnimationClip;
        animatorOverrideController[IDLECOMBAT_ANIMATION] = idleCombatAnimationClip;
        animatorOverrideController[RUN_FORWARD_ANIMATION] = runForwardAnimationClip;
        animatorOverrideController[WALK_FORWARD_ANIMATION] = walkForwardAnimationClip;
    }
    public void ChangeCurretCombatAnimations ( AnimatorOverrideController animatorOverrideController, EquipmentDataSO equipmentDataSO )
    {
        LoadBasicAttackAnimationsOnOverride(animatorOverrideController, equipmentDataSO.equipmentAnimationClips.basicAttackClips);
        LoadChargedAttackAnimationsOnOverride(animatorOverrideController, equipmentDataSO.equipmentAnimationClips.chargedAttackClips);
        LoadSpecialAttackAnimationsOnOverride(animatorOverrideController, equipmentDataSO.equipmentAnimationClips.specialAttackClips);
        LoadSkillAttackAnimationsOnOverride(animatorOverrideController, equipmentDataSO.equipmentAnimationClips.skillAttackClips);
    }
    public void DualSwordOverride ( AnimatorOverrideController animatorOverrideController )
    {
        LoadBasicAttackAnimationsOnOverride(animatorOverrideController, basicAttackDualSwordWeapon);
        LoadChargedAttackAnimationsOnOverride(animatorOverrideController, chargedAttackDualSwordWeapon);
        LoadSpecialAttackAnimationsOnOverride(animatorOverrideController, specialAttackDualSwordWeapon);
        LoadSkillAttackAnimationsOnOverride(animatorOverrideController, skillAttackDualSwordWeapon);
    }
    public void DualDaggerOverride ( AnimatorOverrideController animatorOverrideController )
    {
        LoadBasicAttackAnimationsOnOverride(animatorOverrideController, basicAttackDualDaggerWeapon);
        LoadChargedAttackAnimationsOnOverride(animatorOverrideController, chargedAttackDualDaggerWeapon);
        LoadSpecialAttackAnimationsOnOverride(animatorOverrideController, specialAttackDualDaggerWeapon);
        LoadSkillAttackAnimationsOnOverride(animatorOverrideController, skillAttackDualDaggerWeapon);
    }
    public void ShieldOverride ( AnimatorOverrideController animatorOverrideController )
    {
        LoadBasicAttackAnimationsOnOverride(animatorOverrideController, basicAttackShield);
        LoadChargedAttackAnimationsOnOverride(animatorOverrideController, chargeAttackShield);
        LoadSpecialAttackAnimationsOnOverride(animatorOverrideController, specialAttackShield);
        LoadSkillAttackAnimationsOnOverride(animatorOverrideController, skillAttackShield);
    }
    public void NoWeaponOverride ( AnimatorOverrideController animatorOverrideController )
    {
        LoadBasicAttackAnimationsOnOverride(animatorOverrideController, basicAttackNoWeapons);
        LoadChargedAttackAnimationsOnOverride(animatorOverrideController, chargedAttackNoWeapons);
        LoadSpecialAttackAnimationsOnOverride(animatorOverrideController, skillAttackNoWeapons);
        LoadSkillAttackAnimationsOnOverride(animatorOverrideController, skillAttackNoWeapons);
    }

    [OnValueChanged("LimitMonsterAnimationsByMonsterRank")]
    public void LimitMonsterAnimationsByMonsterRank ( MonsterStats monsterStats )
    {
        if (monsterStats != null)
        {
            switch (monsterStats.monsterRank)
            {
                case MonsterRank.Minion:

                    maxNumberOfBasicAttacks = 3;
                    maxNumberOfChargedAttacks = 1;
                    maxNumberOfSpecialAttacks = 1;
                    maxNumberOfSkillAttacks = 0;
                    break;

                case MonsterRank.Soldier:

                    maxNumberOfBasicAttacks = 3;
                    maxNumberOfChargedAttacks = 2;
                    maxNumberOfSpecialAttacks = 1;
                    maxNumberOfSkillAttacks = 0;
                    break;

                case MonsterRank.Guardian:

                    maxNumberOfBasicAttacks = 4;
                    maxNumberOfChargedAttacks = 2;
                    maxNumberOfSpecialAttacks = 2;
                    maxNumberOfSkillAttacks = 1;
                    break;

                case MonsterRank.General:

                    maxNumberOfBasicAttacks = 4;
                    maxNumberOfChargedAttacks = 2;
                    maxNumberOfSpecialAttacks = 1;
                    maxNumberOfSkillAttacks = 2;
                    break;

                case MonsterRank.Boss:

                    maxNumberOfBasicAttacks = 4;
                    maxNumberOfChargedAttacks = 3;
                    maxNumberOfSpecialAttacks = 2;
                    maxNumberOfSkillAttacks = 3;
                    break;

                case MonsterRank.EliteBoss:

                    maxNumberOfBasicAttacks =  4;
                    maxNumberOfChargedAttacks = 3;
                    maxNumberOfSpecialAttacks = 3;
                    maxNumberOfSkillAttacks = 4;
                    break;

                default:
                    break;
            }
        }
    }

}
