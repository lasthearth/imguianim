namespace imguianim.Tween;

public class TweenFloat : ITween<float>
{
    public TweenFloat(float from = 0f, float to = 1f, Transform curve = null)
    {
        From = from;
        To = to;
        Transform = curve ?? Curves.Linear;
    }

    public float From { get; }
    public float To { get; }
    public Transform Transform { get; }

    public (float, float) Evaluate(float progress)
    {
        var p = Math.Clamp(progress, 0f, 1f);
        var cp = Transform(p);
        return (Lerp(From, To, cp), cp);
    }

    private float Lerp(float firstValue, float secondValue, float by)
    {
        return firstValue * (1 - by) + secondValue * by;
    }
}