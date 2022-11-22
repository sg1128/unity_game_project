using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class UseSkill : MonoBehaviour
{
    GameObject gCamera;
    public SkeletonAnimation skeletonAnimation;
    public bool corou_Runing_skill = false;
    public bool orderToStopAnim = false;

    // Start is called before the first frame update
    void Start()
    {
        gCamera = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        skeletonAnimation = transform.GetChild(0).GetComponent<SkeletonAnimation>();
        StartCor_Skill();
    }

    public void StartCor_Skill()
    {
        if (corou_Runing_skill == false)
        {
            StartCoroutine("SkillAnim");
        } 
    }

    private IEnumerator SkillAnim()
    {
        if (gCamera.GetComponent<Command>().command1On == true)
        {
            corou_Runing_skill = true;
            orderToStopAnim = true;
            //skeletonAnimation.AnimationName = "Deer_Order";
            skeletonAnimation.AnimationState.SetAnimation(0, "Deer_Order", true);
            yield return new WaitForSeconds(skeletonAnimation.timeScale);
            skeletonAnimation.AnimationState.ClearTrack(0);
            gCamera.GetComponent<Command>().command1On = false;
            StopCoroutine("SkillAnim");
            corou_Runing_skill = false;
            orderToStopAnim = false;
        }
        else if (gCamera.transform.GetComponent<Command>().command2On == true)
        {
            corou_Runing_skill = true;
            orderToStopAnim = true;
            //skeletonAnimation.AnimationName = "Wolf_Order";
            skeletonAnimation.AnimationState.SetAnimation(0, "Wolf_Order", true);
            yield return new WaitForSeconds(skeletonAnimation.timeScale);
            skeletonAnimation.AnimationState.ClearTrack(0);
            gCamera.GetComponent<Command>().command2On = false;
            StopCoroutine("SkillAnim");
            corou_Runing_skill = false;
            orderToStopAnim = false;
        }
        else if (gCamera.transform.GetComponent<Command>().command3On == true)
        {
            corou_Runing_skill = true;
            orderToStopAnim = true;
            //skeletonAnimation.AnimationName = "Bear_Order";
            skeletonAnimation.AnimationState.SetAnimation(0, "Bear_Order", true);
            yield return new WaitForSeconds(skeletonAnimation.timeScale);
            skeletonAnimation.AnimationState.ClearTrack(0);
            gCamera.GetComponent<Command>().command3On = false;
            StopCoroutine("SkillAnim");
            corou_Runing_skill = false;
            orderToStopAnim = false;
        }
        else if (gCamera.transform.GetComponent<Command>().command4On == true)
        {
            corou_Runing_skill = true;
            orderToStopAnim = true;
            //skeletonAnimation.AnimationName = "Elephant_Order";
            skeletonAnimation.AnimationState.SetAnimation(0, "Elephant_Order", true);
            yield return new WaitForSeconds(skeletonAnimation.timeScale);
            skeletonAnimation.AnimationState.ClearTrack(0);
            gCamera.GetComponent<Command>().command4On = false;
            StopCoroutine("SkillAnim");
            corou_Runing_skill = false;
            orderToStopAnim = false;
        }
    }
}
