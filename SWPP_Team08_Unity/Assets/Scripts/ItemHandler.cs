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

    public abstract void apply(PlayerController playerController, Item item);

    public void trigger(PlayerController playerController, Item item)
    {
        apply(playerController, item);

        if (nextHandler != null)
            nextHandler.trigger(playerController, item);
    }
}

class ItemPlayerHandler : ItemHandler
{
    public override void apply(PlayerController playerController, Item item)
    {
        item.ApplyItemEffect(playerController);
    }
}

class ItemParticleHandler : ItemHandler
{
    public override void apply(PlayerController playerController, Item item)
    {
        // TODO
        // item.PlayParticleEffect();
    }
}

class ItemTimeHandler : ItemHandler
{
    // TODO - Handle for multiple items
    public override void apply(PlayerController playerController, Item item)
    {
        StartCoroutine(RemoveItem(playerController, item));
    }
    
    private IEnumerator RemoveItem(PlayerController playerController, Item item)
    {
        yield return new WaitForSeconds(item.GetTime());
        item.RemoveItemEffect(playerController);
    }
}