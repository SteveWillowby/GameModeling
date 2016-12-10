using System;

/* This class is fairly simple. It's a subcomponent of
 * an Action. It simply stores a function that performs
 * some operation on some Objects and a Player.
 *
 * Most of the heavy-lifting concerning effects is
 * coded in the EffectParser, which interprets model files
 * and translates effect lines into actual functions.
 */

public class Effect
{
    protected Action<Object[], Player> affector;

    public void Affect(Object[] o, Player p)
    {
        affector(o, p);
    }

    public Effect()
    {
        affector = (o, p) => {};
    }

    public Effect(Action<Object[], Player> a)
    {
        affector = a;
    }
}
