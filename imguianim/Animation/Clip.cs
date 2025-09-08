using imguianim.Tween;

namespace imguianim.Animation;

public class AnimationClip<T> : IAnimation
{
    private readonly Action<T> _setter;

    public AnimationClip(AnimationController controller, ITween<T> tween, Action<T> setter)
    {
        Controller = controller ?? throw new ArgumentNullException(nameof(controller));
        Tween = tween ?? throw new ArgumentNullException(nameof(tween));
        _setter = setter ?? throw new ArgumentNullException(nameof(setter));
    }

    public AnimationController Controller { get; }
    public ITween<T> Tween { get; }

    public bool IsCompleted
    {
        get
        {
            var (_, tweenMaxProgress) = Tween.Evaluate(1f);
            if (Controller.Progress >= tweenMaxProgress) return true;
            if (Controller.Progress < 0f) return false;
            if (Controller.Direction != Direction.Stopped) return false;

            return true;
        }
    }

    public void Forward()
    {
        Controller.Forward();
    }

    public void Stop()
    {
        Controller.Stop();
    }

    public void Reverse()
    {
        Controller.Reverse();
    }

    public void Update(float dt)
    {
        Controller.Update(dt);
        var (value, _) = Tween.Evaluate(Controller.Progress);
        _setter(value);
    }

    public Direction Direction => Controller.Direction;

    public Action? OnCompleted
    {
        get => Controller.OnCompleted;
        set => Controller.OnCompleted = value;
    }
}