using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;
using Unity.Transforms;

public class BlockSystem : SystemBase
{
    EntityCommandBufferSystem ei_ECB; //Allows for utilizing main thread processing for performing repeatable actions; 
                                                        //more effecient.
    protected override void OnCreate()
    {
        ei_ECB = World.DefaultGameObjectInjectionWorld.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }
    protected override void OnUpdate()
    {
        EntityCommandBuffer.ParallelWriter ecb = ei_ECB.CreateCommandBuffer().AsParallelWriter();
        Entities.WithBurst(synchronousCompilation:true)
            .ForEach((Entity e, int entityInQueryIndex, in BlockData block, in LocalToWorld ltw) =>{
            for(int i=0; i<block.xGrid;i++){
                for(int j=0; j<block.yGrid; j++){
                    Entity defEntity = ecb.Instantiate(entityInQueryIndex, block.prefabToSpawn);
                    float3 pos = new float3(i*block.xPadding, block.offset, j*block.yPadding);
                    ecb.SetComponent(entityInQueryIndex, defEntity, new Translation {Value = pos});
                }
            }
            ecb.DestroyEntity(entityInQueryIndex, e);
        }).ScheduleParallel();
        ei_ECB.AddJobHandleForProducer(Dependency);
    }

}