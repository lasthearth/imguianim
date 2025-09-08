namespace imguianim;

public delegate float Transform(float t);

public static class Curves
{
    public static float Linear(float t)
    {
        return t;
    }

    public static float Ease(float t)
    {
        var cubic = new Cubic(
            0.25f,
            0.1f,
            0.25f,
            1f
        );
        return cubic.Transform(t);
    }

    public static float EaseIn(float t)
    {
        var cubic = new Cubic(
            0.42f,
            0.0f,
            1f,
            1f
        );
        return cubic.Transform(t);
    }

    public static float EaseOut(float t)
    {
        var cubic = new Cubic(
            0f,
            0f,
            0.58f,
            1f
        );
        return cubic.Transform(t);
    }

    public static float EaseInOut(float t)
    {
        var cubic = new Cubic(
            0.42f,
            0f,
            0.58f,
            1f
        );
        return cubic.Transform(t);
    }

    public static float EaseInOutCubic(float t)
    {
        var cubic = new Cubic(
            0.645f,
            0.045f,
            0.355f,
            1f
        );
        return cubic.Transform(t);
    }

    public static float FastLinearToSlowEaseIn(float t)
    {
        var cubic = new Cubic(
            0.18f,
            1f,
            0.04f,
            1f
        );
        return cubic.Transform(t);
    }
}

internal class Cubic
{
    private const double CubicErrorBound = 0.001;

    private readonly float _a;

    private readonly float _b;

    private readonly float _c;

    private readonly float _d;

    public Cubic(float a, float b, float c, float d)
    {
        _a = a;
        _b = b;
        _c = c;
        _d = d;
    }

    private static float EvaluateCubic(float a, float b, float m)
    {
        return 3 * a * (1 - m) * (1 - m) * m + 3 * b * (1 - m) * m * m + m * m * m;
    }

    public float Transform(double t)
    {
        var start = 0.0f;
        var end = 1.0f;
        while (true)
        {
            var midpoint = (start + end) / 2;
            var estimate = EvaluateCubic(_a, _c, midpoint);
            if (Math.Abs(t - estimate) < CubicErrorBound) return EvaluateCubic(_b, _d, midpoint);

            if (estimate < t)
                start = midpoint;
            else
                end = midpoint;
        }
    }
}