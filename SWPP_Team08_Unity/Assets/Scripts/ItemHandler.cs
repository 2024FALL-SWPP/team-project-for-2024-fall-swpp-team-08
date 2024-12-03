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

    public abstract void apply(PlayerController playerController, Item item, GameObject[] timeCanvasPrefabs, ParticleSystem[] particleSystems);

    public void trigger(PlayerController playerController, Item item, GameObject[] timeCanvasPrefabs, ParticleSystem[] particleSystems)
    {
        apply(playerController, item, timeCanvasPrefabs, particleSystems);

        if (nextHandler != null)
            nextHandler.trigger(playerController, item, timeCanvasPrefabs, particleSystems);
    }
}

class ItemPlayerHandler : ItemHandler
{
    public override void apply(PlayerController playerController, Item item, GameObject[] timeCanvasPrefabs, ParticleSystem[] particleSystems)
    {
        item.ApplyItemEffect(playerController);
    }
}

class ItemUIHandler : ItemHandler
{
    public override void apply(PlayerController playerController, Item item, GameObject[] timeCanvasPrefabs, ParticleSystem[] particleSystems)
    {
        item.ShowTimeUI(timeCanvasPrefabs);
    }
}

class ItemParticleHandler : ItemHandler
{
    public override void apply(PlayerController playerController, Item item, GameObject[] timeCanvasPrefabs, ParticleSystem[] particleSystems)
    {
        item.PlayParticleEffect(particleSystems);
    }
}

class ItemTimeHandler : ItemHandler
{
    // TODO - Handle for multiple items
    public override void apply(PlayerController playerController, Item item, GameObject[] timeCanvasPrefabs, ParticleSystem[] particleSystems)
    {
        StartCoroutine(RemoveItem(playerController, item));
    }
    
    private IEnumerator RemoveItem(PlayerController playerController, Item item)
    {
        yield return new WaitForSeconds(item.GetTime());
        item.RemoveItemEffect(playerController);
    }
}