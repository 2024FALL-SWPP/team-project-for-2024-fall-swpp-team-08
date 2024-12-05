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

    public abstract void apply(PlayerController playerController, Item item, GameObject[] timeCanvasPrefabs, ParticleSystem[] particleSystems, EffectManager effectManager);

    public void trigger(PlayerController playerController, Item item, GameObject[] timeCanvasPrefabs, ParticleSystem[] particleSystems, EffectManager effectManager)
    {
        apply(playerController, item, timeCanvasPrefabs, particleSystems, effectManager);

        if (nextHandler != null)
            nextHandler.trigger(playerController, item, timeCanvasPrefabs, particleSystems, effectManager);
    }
}

class ItemPlayerHandler : ItemHandler
{
    public override void apply(PlayerController playerController, Item item, GameObject[] timeCanvasPrefabs, ParticleSystem[] particleSystems, EffectManager effectManager)
    {
        item.ApplyItemEffect(playerController, effectManager);
    }
}

class ItemUIHandler : ItemHandler
{
    public override void apply(PlayerController playerController, Item item, GameObject[] timeCanvasPrefabs, ParticleSystem[] particleSystems, EffectManager effectManager)
    {
        item.ShowTimeUI(timeCanvasPrefabs);
    }
}

class ItemParticleHandler : ItemHandler
{
    public override void apply(PlayerController playerController, Item item, GameObject[] timeCanvasPrefabs, ParticleSystem[] particleSystems, EffectManager effectManager)
    {
        item.PlayParticleEffect(particleSystems);
    }
}

class ItemTimeHandler : ItemHandler
{
    // TODO - Handle for multiple items
    public override void apply(PlayerController playerController, Item item, GameObject[] timeCanvasPrefabs, ParticleSystem[] particleSystems, EffectManager effectManager)
    {
        StartCoroutine(RemoveItem(playerController, item));
    }
    
    private IEnumerator RemoveItem(PlayerController playerController, Item item)
    {
        yield return new WaitForSeconds(item.GetTime());
        item.RemoveItemEffect(playerController);
    }
}