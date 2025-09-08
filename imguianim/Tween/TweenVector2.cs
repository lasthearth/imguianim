using System.Numerics;

namespace imguianim.Tween;

public class TweenVector2 : ITween<Vector2>
{
    public TweenVector2(Vector2 from, Vector2 to, Transform? curve = null)
    {
        From = from;
        To = to;
        Transform = curve ?? Curves.Linear;
    }

    public Vector2 From { get; }
    public Vector2 To { get; }
    public Transform Transform { get; }

    public (Vector2, float) Evaluate(float progress)
    {
        var p = Math.Clamp(progress, 0f, 1f);
        var c = Transform(p);
        var value = Vector2.Lerp(From, To, c);
        return (value, c);
    }
}