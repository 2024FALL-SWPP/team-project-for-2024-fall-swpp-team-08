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

    public abstract void apply(Item item, ItemData itemData);

    public void trigger(Item item, ItemData itemData)
    {
        apply(item, itemData);

        if (nextHandler != null)
            nextHandler.trigger(item, itemData);
    }
}

class ItemPlayerHandler : ItemHandler
{
    public override void apply(Item item, ItemData itemData)
    {
        item.ApplyItemEffect(itemData.playerController);
    }
}

class ItemSoundHandler : ItemHandler
{
    public override void apply(Item item, ItemData itemData)
    {
        item.PlaySoundEffect(itemData.effectManager);
    }
}

class ItemParticleHandler : ItemHandler
{
    public override void apply(Item item, ItemData itemData)
    {
        item.PlayParticleEffect(itemData.particleSystems);
    }
}

class ItemUIHandler : ItemHandler
{
    public override void apply(Item item, ItemData itemData)
    {
        item.ShowTimeUI(itemData.timeCanvasPrefabs);
    }
}

class ItemTimeHandler : ItemHandler
{
    public override void apply(Item item, ItemData itemData)
    {
        StartCoroutine(RemoveItem(itemData.playerController, item));
    }
    
    private IEnumerator RemoveItem(PlayerController playerController, Item item)
    {
        yield return new WaitForSeconds(item.GetTime());
        item.RemoveItemEffect(playerController);
    }
}