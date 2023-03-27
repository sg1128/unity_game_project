using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class Attack_Sword : MonoBehaviour
{
    SkeletonAnimation skeletonAnimation;
    public GameObject target;
    float x;
    Tank_fsm tank_fsm;
    Unit unit;

    void Start()
    {
        skeletonAnimation = GetComponentInParent<SkeletonAnimation>();
        tank_fsm = gameObject.transform.parent.GetComponent<Tank_fsm>();
        unit = gameObject.transform.parent.GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        //StopAnim();
        x = GetComponentInParent<Transform>().parent.transform.localScale.x;
        target = tank_fsm.target;
    }

    public void Swing()
    {
        //gameObject.transform.position = Vector3.MoveTowards(transform.position, new Vector2(transform.position.x , transform.position.y-1), Time.deltaTime * 1);
        StartCoroutine(Attack_Dmg());
        //StartCoroutine(Attack_Anim());
    }

    IEnumerator Attack_Dmg()
    {
        yield return new WaitForSeconds(1.0f);
        if(target != null)
            target.GetComponent<Enemy>().TakeDamage(unit.dmg);
        StopCoroutine(Attack_Dmg());
    }

    //IEnumerator Attack_Anim()
    //{
    //    Vector3 playerPos = GetComponentInParent<Transform>().position;
    //    Vector3 targetPos = target.GetComponent<Transform>().position;

    //    if (GetComponentInParent<Unit>().die == false)
    //    {
    //        if (playerPos.x > targetPos.x && playerPos.y == targetPos.y)
    //        {
    //            if (x < 0) { ReverseScale(); }
    //            skeletonAnimation.Skeleton.SetSkin("knight_sf");
    //            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
    //            yield return new WaitForSeconds(0.6f);
    //            StopAnim();
    //            skeletonAnimation.AnimationName = "knight_sf/knight_sf_attack_2";
    //        }
    //        else if (playerPos.x < targetPos.x && playerPos.y == targetPos.y)
    //        {
    //            if (x > 0) { ReverseScale(); }
    //            skeletonAnimation.Skeleton.SetSkin("knight_sf");
    //            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
    //            yield return new WaitForSeconds(0.6f);
    //            StopAnim();
    //            skeletonAnimation.AnimationName = "knight_sf/knight_sf_attack_2";
    //        }
    //        else if (playerPos.x == targetPos.x && playerPos.y > targetPos.y)
    //        {
    //            skeletonAnimation.Skeleton.SetSkin("knight_f");
    //            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
    //            yield return new WaitForSeconds(0.6f);
    //            StopAnim();
    //            skeletonAnimation.AnimationName = "knight_sf/knight_sf_attack_2";
    //        }
    //        else if (playerPos.x == targetPos.x && playerPos.y < targetPos.y)
    //        {
    //            skeletonAnimation.Skeleton.SetSkin("knight_b");
    //            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
    //            yield return new WaitForSeconds(0.6f);
    //            StopAnim();
    //            skeletonAnimation.AnimationName = "knight_sb/knight_sb_attack_2";
    //        }
    //        else if (playerPos.x < targetPos.x && playerPos.y < targetPos.y)
    //        {
    //            if (x > 0) { ReverseScale(); }
    //            skeletonAnimation.Skeleton.SetSkin("knight_sb");
    //            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
    //            yield return new WaitForSeconds(0.6f);
    //            StopAnim();
    //            skeletonAnimation.AnimationName = "knight_sb/knight_sb_attack_2";
    //        }
    //        else if (playerPos.x > targetPos.x && playerPos.y < targetPos.y)
    //        {
    //            if (x < 0) { ReverseScale(); }
    //            skeletonAnimation.Skeleton.SetSkin("knight_sb");
    //            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
    //            yield return new WaitForSeconds(0.6f);
    //            StopAnim();
    //            skeletonAnimation.AnimationName = "knight_sb/knight_sb_attack_2";
    //        }
    //        else if (playerPos.x > targetPos.x && playerPos.y > targetPos.y)
    //        {
    //            if (x < 0) { ReverseScale(); }
    //            skeletonAnimation.Skeleton.SetSkin("knight_sf");
    //            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
    //            yield return new WaitForSeconds(0.6f);
    //            StopAnim();
    //            skeletonAnimation.AnimationName = "knight_sf/knight_sf_attack_2";
    //        }
    //        else if (playerPos.x < targetPos.x && playerPos.y > targetPos.y)
    //        {
    //            if (x > 0) { ReverseScale(); }
    //            skeletonAnimation.Skeleton.SetSkin("knight_sf");
    //            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
    //            yield return new WaitForSeconds(0.6f);
    //            StopAnim();
    //            skeletonAnimation.AnimationName = "knight_sf/knight_sf_attack_2";
    //        }
    //    }

    //    yield return new WaitForSeconds(1.0f);
    //    if (GetComponentInParent<Unit>().die == false)
    //    {
    //        skeletonAnimation.AnimationName = "knight_sf/knight_sf_idle";
    //    }
    //    StopCoroutine(Attack_Anim());
    //}
    //public void ReverseScale()
    //{
    //    float y = GetComponentInParent<Transform>().parent.transform.localScale.y;
    //    float z = GetComponentInParent<Transform>().parent.transform.localScale.z;
    //    GetComponentInParent<Transform>().parent.transform.localScale = new Vector3(-x, y, z);
    //}

    //public void StopAnim()
    //{
    //    if (GetComponentInParent<Unit>().die == true)
    //    {
    //        StopCoroutine(Attack_Anim());
    //        skeletonAnimation.Skeleton.SetSkin("knight_sf");
    //        skeletonAnimation.Skeleton.SetSlotsToSetupPose();
    //        skeletonAnimation.AnimationName = "knight_sf/knight_sf_die";
    //    }
    //}
}
