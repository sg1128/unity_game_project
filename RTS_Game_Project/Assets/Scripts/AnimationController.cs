using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class AnimationController : MonoBehaviour
{
    SkeletonAnimation skeletonAnimation;
    bool arrived;
    Vector2Int startPos, targetPos;
    float xScale;
    Tank_fsm fsm;
    public string currentAnim;
    string changeAnim;
    Unit unit;
    public int attackMotion;

    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        fsm = GetComponent<Tank_fsm>();
        unit = GetComponent<Unit>();
        attackMotion = UnityEngine.Random.Range(0, 2);
    }

    void Update()
    {
        xScale = transform.localScale.x;
        arrived = GetComponent<Tank_UnitMovement>().arrived;
        startPos = GetComponent<Tank_UnitMovement>().startPos;
        targetPos = GetComponent<Tank_UnitMovement>().targetPos;
        currentAnim = skeletonAnimation.AnimationName;

        if (unit.newChar == false && arrived == false && fsm.fight == false && unit.die == false)
        {
            if (startPos.x > targetPos.x && startPos.y == targetPos.y)
            {
                changeAnim = "knight_sf/knight_sf_run_sword_down";
                if(SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("knight_sf");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    //skeletonAnimation.AnimationName = "knight_sf/knight_sf_run_sword_down";
                    skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_run_sword_down", true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x < targetPos.x && startPos.y == targetPos.y)
            {
                changeAnim = "knight_sf/knight_sf_run_sword_down";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("knight_sf");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    //skeletonAnimation.AnimationName = "knight_sf/knight_sf_run_sword_down";
                    skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_run_sword_down", true);
                }
                if (xScale > 0) { ReverseScale(); }
            }
            else if (startPos.x == targetPos.x && startPos.y > targetPos.y)
            {
                changeAnim = "knight_f/knight_f_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("knight_f");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    //skeletonAnimation.AnimationName = "knight_f/knight_f_run";
                    skeletonAnimation.AnimationState.SetAnimation(0, "knight_f/knight_f_run", true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x == targetPos.x && startPos.y < targetPos.y)
            {
                changeAnim = "knight_b/knight_b_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("knight_b");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    //skeletonAnimation.AnimationName = "knight_b/knight_b_run";
                    skeletonAnimation.AnimationState.SetAnimation(0, "knight_b/knight_b_run", true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x < targetPos.x && startPos.y < targetPos.y)
            {
                changeAnim = "knight_sb/knight_sb_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("knight_sb");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    //skeletonAnimation.AnimationName = "knight_sb/knight_sb_run";
                    skeletonAnimation.AnimationState.SetAnimation(0, "knight_sb/knight_sb_run", true);
                }
                if (xScale > 0) { ReverseScale(); }
            }
            else if (startPos.x > targetPos.x && startPos.y < targetPos.y)
            {
                changeAnim = "knight_sb/knight_sb_run";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("knight_sb");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    //skeletonAnimation.AnimationName = "knight_sb/knight_sb_run";
                    skeletonAnimation.AnimationState.SetAnimation(0, "knight_sb/knight_sb_run", true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x > targetPos.x && startPos.y > targetPos.y)
            {
                changeAnim = "knight_sf/knight_sf_run_sword_down";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("knight_sf");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    //skeletonAnimation.AnimationName = "knight_sf/knight_sf_run_sword_down";
                    skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_run_sword_down", true);
                }
                if (xScale < 0) { ReverseScale(); }
            }
            else if (startPos.x < targetPos.x && startPos.y > targetPos.y)
            {
                changeAnim = "knight_sf/knight_sf_run_sword_down";
                if (SameAnimation(currentAnim, changeAnim))
                {
                    skeletonAnimation.Skeleton.SetSkin("knight_sf");
                    skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                    //skeletonAnimation.AnimationName = "knight_sf/knight_sf_run_sword_down";
                    skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_run_sword_down", true);
                }
                if (xScale > 0) { ReverseScale(); }
            }
        }

        if (unit.newChar == false && arrived == true && fsm.fight == false)
        {
            changeAnim = "knight_f/knight_f_idle";
            if (SameAnimation(currentAnim, changeAnim))
            {
                //skeletonAnimation.AnimationName = "knight_f/knight_f_idle";
                skeletonAnimation.AnimationState.SetAnimation(0, "knight_f/knight_f_idle", true);
            }
        }

        if (unit.newChar == false && unit.die == true)
        {
            changeAnim = "knight_sf/knight_sf_die";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.Skeleton.SetSkin("knight_sf");
                skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                //skeletonAnimation.AnimationName = "knight_sf/knight_sf_die";
                skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_die", false);
            }
        }

        if (unit.newChar == false && fsm.fight == true && fsm.currentTime >= 1.6f && fsm.currentTime <= 2.1f)
        {
            GameObject target = GetComponent<Tank_fsm>().target;
            Vector3 playerPos = GetComponent<Transform>().position;
            Vector3 targetPos = target.GetComponent<Transform>().position;

            if (unit.die == false && arrived == true && attackMotion == 1)
            {
                if (playerPos.x > targetPos.x && playerPos.y == targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_2";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_attack_2", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_f/knight_f_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale < 0) { ReverseScale(); }
                }
                else if (playerPos.x < targetPos.x && playerPos.y == targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_2";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_attack_2", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_f/knight_f_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale > 0) { ReverseScale(); }
                    
                }
                else if (playerPos.x == targetPos.x && playerPos.y > targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_2";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_f");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_attack_2", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_f/knight_f_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                }
                else if (playerPos.x == targetPos.x && playerPos.y < targetPos.y)
                {
                    changeAnim = "knight_sb/knight_sb_attack_2";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_b");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sb/knight_sb_attack_2", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_f/knight_f_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                }
                else if (playerPos.x < targetPos.x && playerPos.y < targetPos.y)
                {
                    changeAnim = "knight_sb/knight_sb_attack_2";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sb");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sb/knight_sb_attack_2", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_f/knight_f_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale > 0) { ReverseScale(); }
                }
                else if (playerPos.x > targetPos.x && playerPos.y < targetPos.y)
                {
                    changeAnim = "knight_sb/knight_sb_attack_2";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sb");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sb/knight_sb_attack_2", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_f/knight_f_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale < 0) { ReverseScale(); }
                }
                else if (playerPos.x > targetPos.x && playerPos.y > targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_2";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_attack_2", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_f/knight_f_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale < 0) { ReverseScale(); }
                }
                else if (playerPos.x < targetPos.x && playerPos.y > targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_2";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_attack_2", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_f/knight_f_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale > 0) { ReverseScale(); }
                }
            }
            else if (unit.die == false && arrived == true && attackMotion == 0)
            {
                if (playerPos.x > targetPos.x && playerPos.y == targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_1";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_attack_1", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_f/knight_f_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale < 0) { ReverseScale(); }
                }
                else if (playerPos.x < targetPos.x && playerPos.y == targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_1";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_attack_1", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_f/knight_f_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale > 0) { ReverseScale(); }

                }
                else if (playerPos.x == targetPos.x && playerPos.y > targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_1";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_f");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_attack_1", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_f/knight_f_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                }
                else if (playerPos.x == targetPos.x && playerPos.y < targetPos.y)
                {
                    changeAnim = "knight_sb/knight_sb_attack_1";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_b");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sb/knight_sb_attack_1", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_f/knight_f_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                }
                else if (playerPos.x < targetPos.x && playerPos.y < targetPos.y)
                {
                    changeAnim = "knight_sb/knight_sb_attack_1";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sb");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sb/knight_sb_attack_1", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_f/knight_f_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale > 0) { ReverseScale(); }
                }
                else if (playerPos.x > targetPos.x && playerPos.y < targetPos.y)
                {
                    changeAnim = "knight_sb/knight_sb_attack_1";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sb");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sb/knight_sb_attack_1", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_f/knight_f_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale < 0) { ReverseScale(); }
                }
                else if (playerPos.x > targetPos.x && playerPos.y > targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_1";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_attack_1", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_f/knight_f_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale < 0) { ReverseScale(); }
                }
                else if (playerPos.x < targetPos.x && playerPos.y > targetPos.y)
                {
                    changeAnim = "knight_sf/knight_sf_attack_1";
                    if (SameAnimation(currentAnim, changeAnim))
                    {
                        skeletonAnimation.Skeleton.SetSkin("knight_sf");
                        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                        skeletonAnimation.AnimationState.SetAnimation(0, "knight_sf/knight_sf_attack_1", false);
                        skeletonAnimation.AnimationState.AddAnimation(0, "knight_f/knight_f_idle", true, 0);
                        attackMotion = UnityEngine.Random.Range(0, 2);
                    }
                    if (xScale > 0) { ReverseScale(); }
                }
            }
        }
        if(unit.newChar == true)
        {
            changeAnim = "knight_f/knight_f_create";
            if (SameAnimation(currentAnim, changeAnim))
            {
                skeletonAnimation.Skeleton.SetSkin("knight_f");
                skeletonAnimation.Skeleton.SetSlotsToSetupPose();
                skeletonAnimation.AnimationState.SetAnimation(0, "knight_f/knight_f_create", false);
            }
        }
    }

    public bool SameAnimation(string currentAnim, string changeAnim)
    {
        if(currentAnim == changeAnim)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void ReverseScale()
    {
        float y = transform.localScale.y;
        float z = transform.localScale.z;
        transform.localScale = new Vector3(-xScale, y, z);
    }
}

//깃허브 연습중