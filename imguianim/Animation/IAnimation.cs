namespace imguianim.Animation;

public interface IAnimation
{
    Direction Direction { get; }
    Action? OnCompleted { get; internal set; }
    bool IsCompleted { get; }
    void Forward();
    void Stop();
    void Reverse();
    void Update(float dt);
}