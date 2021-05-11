using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class LevelUpSystem : ComponentSystem
{
   protected override void OnUpdate(){
       Entities.ForEach((ref LevelComponent levelCompnent) =>{
           levelCompnent.level += 1f * Time.DeltaTime;
           //Debug.Log(levelCompnent.level);
       });
   }

}
