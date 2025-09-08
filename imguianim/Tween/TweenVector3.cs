using System.Numerics;

namespace imguianim.Tween;

public class TweenVector3 : ITween<Vector3>
{
    public TweenVector3(Vector3 from, Vector3 to, Transform? curve = null)
    {
        From = from;
        To = to;
        Transform = curve ?? Curves.Linear;
    }

    public Vector3 From { get; }
    public Vector3 To { get; }
    public Transform Transform { get; }

    public (Vector3, float) Evaluate(float progress)
    {
        var p = Math.Clamp(progress, 0f, 1f);
        var c = Transform(p);
        var value = Vector3.Lerp(From, To, c);
        return (value, c);
    }
}