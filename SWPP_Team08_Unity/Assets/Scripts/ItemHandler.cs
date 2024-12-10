using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handler for Item collision
abstract class ItemHandler : MonoBehaviour
{
    private ItemHandler nextHandler;

    public ItemHandler setNext(ItemHandler handler)
    {
        nextHandler = handler;
        return handler;
    }

    public abstract void apply(PlayerController playerController, Item item, EffectManager effectManager, ParticleSystem[] particleSystems, GameObject[] timeCanvasPrefabs);

    public void trigger(PlayerController playerController, Item item, EffectManager effectManager, ParticleSystem[] particleSystems, GameObject[] timeCanvasPrefabs)
    {
        apply(playerController, item, effectManager, particleSystems, timeCanvasPrefabs);

        if (nextHandler != null)
            nextHandler.trigger(playerController, item, effectManager, particleSystems, timeCanvasPrefabs);
    }
}

class ItemPlayerHandler : ItemHandler
{
    public override void apply(PlayerController playerController, Item item, EffectManager effectManager, ParticleSystem[] particleSystems, GameObject[] timeCanvasPrefabs)
    {
        item.ApplyItemEffect(playerController);
    }
}

class ItemSoundHandler : ItemHandler
{
    public override void apply(PlayerController playerController, Item item, EffectManager effectManager, ParticleSystem[] particleSystems, GameObject[] timeCanvasPrefabs)
    {
        item.PlaySoundEffect(effectManager);
    }
}

class ItemParticleHandler : ItemHandler
{
    public override void apply(PlayerController playerController, Item item, EffectManager effectManager, ParticleSystem[] particleSystems, GameObject[] timeCanvasPrefabs)
    {
        item.PlayParticleEffect(particleSystems);
    }
}

class ItemUIHandler : ItemHandler
{
    public override void apply(PlayerController playerController, Item item, EffectManager effectManager, ParticleSystem[] particleSystems, GameObject[] timeCanvasPrefabs)
    {
        item.ShowTimeUI(timeCanvasPrefabs);
    }
}

class ItemTimeHandler : ItemHandler
{
    public override void apply(PlayerController playerController, Item item, EffectManager effectManager, ParticleSystem[] particleSystems, GameObject[] timeCanvasPrefabs)
    {
        StartCoroutine(RemoveItem(playerController, item));
    }
    
    private IEnumerator RemoveItem(PlayerController playerController, Item item)
    {
        yield return new WaitForSeconds(item.GetTime());
        item.RemoveItemEffect(playerController);
    }
}