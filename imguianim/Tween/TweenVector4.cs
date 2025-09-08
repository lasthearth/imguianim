using System.Numerics;

namespace imguianim.Tween;

public class TweenVector4 : ITween<Vector4>
{
    public TweenVector4(Vector4 from, Vector4 to, Transform? curve = null)
    {
        From = from;
        To = to;
        Transform = curve ?? Curves.Linear;
    }

    public Vector4 From { get; }
    public Vector4 To { get; }
    public Transform Transform { get; }

    public (Vector4, float) Evaluate(float progress)
    {
        var p = Math.Clamp(progress, 0f, 1f);
        var c = Transform(p);
        var value = Vector4.Lerp(From, To, c);
        return (value, c);
    }
}