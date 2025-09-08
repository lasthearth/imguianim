namespace imguianim;

public enum Direction
{
    Stopped = 0,
    Reverse = -1,
    Forward = 1
}

/// <summary>
///     Controller for handling animations.
/// </summary>
public class AnimationController
{
    /// <summary>
    ///     Animation direction.
    /// </summary>
    internal Direction Direction = Direction.Stopped;

    public Action? OnCompleted;

    public AnimationController(float durationSeconds = 0.3f,
        float startProgress = 0f,
        Action? onCompleted = null)
    {
        OnCompleted = onCompleted;
        Duration = Math.Max(1e-6f, durationSeconds);
        Progress = Math.Clamp(startProgress, 0f, 1f);
    }

    public float Duration { get; private set; }
    public float Progress { get; private set; }

    public void SetDuration(float s)
    {
        Duration = Math.Max(1e-6f, s);
    }

    /// <summary>
    ///     Reset the animation to a specific progress and stop animating.
    /// </summary>
    /// <param name="to">Animation progress</param>
    public void Reset(float to = 0f)
    {
        Progress = Math.Clamp(to, 0f, 1f);
        Direction = Direction.Stopped;
    }

    /// <summary>
    ///     Start animating forward from the current progress.
    /// </summary>
    public void Forward()
    {
        Direction = Direction.Forward;
    }

    /// <summary>
    ///     Start animating in reverse from the current progress.
    /// </summary>
    public void Reverse()
    {
        Direction = Direction.Reverse;
    }

    /// <summary>
    ///     Stop the animation at the current progress.
    /// </summary>
    public void Stop()
    {
        Direction = 0;
    }

    /// <summary>
    ///     Used to update the animation progress. Call this method every frame with the delta time.
    /// </summary>
    /// <param name="dt"></param>
    public void Update(float dt)
    {
        if (Direction == Direction.Stopped) return;

        Progress += (int)Direction * dt / Duration;
        Progress = Math.Clamp(Progress, 0f, 1f);

        var isForwardComplete = Direction == Direction.Forward && Progress >= 1f;
        var isReverseComplete = Direction == Direction.Reverse && Progress <= 0f;

        if (!isForwardComplete && !isReverseComplete) return;

        Direction = Direction.Stopped;
        OnCompleted?.Invoke();
    }
}