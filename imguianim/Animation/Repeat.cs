namespace imguianim.Animation;

/// <summary>
///     Repeats all provided animations, until stopped
/// </summary>
public class AnimationRepeat : IAnimation
{
    private readonly IAnimation[] _animations;

    public AnimationRepeat(params IAnimation[] anims)
    {
        _animations = anims;
    }

    public Action? OnCompleted { get; set; }

    public Direction Direction { get; private set; }
    public bool IsCompleted => _animations.All(animation => animation.IsCompleted);

    public void Forward()
    {
        foreach (var animation in _animations) animation.Forward();

        Direction = Direction.Forward;
    }

    public void Stop()
    {
        foreach (var animation in _animations) animation.Stop();

        Direction = Direction.Stopped;
    }

    public void Reverse()
    {
        foreach (var animation in _animations) animation.Reverse();

        Direction = Direction.Reverse;
    }

    public void Update(float dt)
    {
        // if (IsCompleted) return;
        foreach (var animation in _animations)
        {
            if (animation.IsCompleted)
            {
                animation.Reverse();
                animation.Update(float.MaxValue);
                animation.Forward();
            }

            if (!animation.IsCompleted)
                animation.Update(dt);
        }
    }
}