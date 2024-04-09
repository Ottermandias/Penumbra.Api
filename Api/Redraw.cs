using Dalamud.Game.ClientState.Objects.Types;
using Penumbra.Api.Enums;

namespace Penumbra.Api.Api;

public partial interface IPenumbraApi
{
    /// <summary>
    /// Queue redrawing of all actors of the given <paramref name="name"/> with the given RedrawType <paramref name="setting"/>.
    /// </summary>
    /// <param name="name" />
    /// <param name="setting" />
    public void RedrawObject(string name, RedrawType setting);

    /// <summary>
    /// Queue redrawing of the specific actor <paramref name="gameObject"/> with the given RedrawType <paramref name="setting"/>. Should only be used when the actor is sure to be valid.
    /// </summary>
    /// <param name="gameObject" />
    /// <param name="setting" />
    public void RedrawObject(GameObject gameObject, RedrawType setting);

    /// <summary>
    /// Queue redrawing of the actor with the given object <paramref name="tableIndex" />, if it exists, with the given RedrawType <paramref name="setting"/>.
    /// </summary>
    /// <param name="tableIndex" />
    /// <param name="setting" />
    public void RedrawObject(int tableIndex, RedrawType setting);

    /// <summary>
    /// Queue redrawing of all currently available actors with the given RedrawType <paramref name="setting"/>.
    /// </summary>
    /// <param name="setting" />
    public void RedrawAll(RedrawType setting);

    /// <summary>
    /// Triggered whenever a game object is redrawn via Penumbra.
    /// </summary>
    /// /<returns><inheritdoc cref="GameObjectRedrawnDelegate"/></returns>
    public event GameObjectRedrawnDelegate? GameObjectRedrawn;
}
