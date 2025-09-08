namespace imguianim.Tween;

public interface ITween<T>
{
    (T Value, float Transform) Evaluate(float progress);
}