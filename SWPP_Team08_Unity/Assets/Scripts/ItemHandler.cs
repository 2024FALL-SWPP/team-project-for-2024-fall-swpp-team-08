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

    public abstract void apply(PlayerController playerController, Item item, GameObject[] timeCanvasPrefabs);

    public void trigger(PlayerController playerController, Item item, GameObject[] timeCanvasPrefabs)
    {
        apply(playerController, item, timeCanvasPrefabs);

        if (nextHandler != null)
            nextHandler.trigger(playerController, item, timeCanvasPrefabs);
    }
}

class ItemPlayerHandler : ItemHandler
{
    public override void apply(PlayerController playerController, Item item, GameObject[] timeCanvasPrefabs)
    {
        item.ApplyItemEffect(playerController);
    }
}

class ItemUIHandler : ItemHandler
{
    public override void apply(PlayerController playerController, Item item, GameObject[] timeCanvasPrefabs)
    {
        item.ShowTimeUI(timeCanvasPrefabs);
    }
}

class ItemParticleHandler : ItemHandler
{
    public override void apply(PlayerController playerController, Item item, GameObject[] timeCanvasPrefabs)
    {
        // TODO
        // item.PlayParticleEffect();
    }
}

class ItemTimeHandler : ItemHandler
{
    // TODO - Handle for multiple items
    public override void apply(PlayerController playerController, Item item, GameObject[] timeCanvasPrefabs)
    {
        StartCoroutine(RemoveItem(playerController, item));
    }
    
    private IEnumerator RemoveItem(PlayerController playerController, Item item)
    {
        yield return new WaitForSeconds(item.GetTime());
        item.RemoveItemEffect(playerController);
    }
}