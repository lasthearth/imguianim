namespace imguianim.Animation;

/// <summary>
///     Runs animations one after the other
/// </summary>
public class AnimationSequence : IAnimation
{
    private readonly IAnimation[] _anims;
    private int _idx;

    public AnimationSequence(params IAnimation[] anims)
    {
        _anims = anims ?? throw new ArgumentNullException(nameof(anims));
    }

    public void Forward()
    {
        _idx = 0;
        if (!IsHaveAnims()) return;
        if (!IsIndexValid()) return;

        Direction = Direction.Forward;

        _anims[_idx].Forward();
    }

    public void Stop()
    {
        _idx = 0;
        if (!IsHaveAnims()) return;
        if (!IsIndexValid()) return;
        _anims[_idx].Stop();
    }

    public void Reverse()
    {
        _idx = _anims.Length - 1;
        if (!IsHaveAnims()) return;
        if (!IsIndexValid()) return;

        Direction = Direction.Reverse;

        _anims[_idx].Reverse();
    }

    public void Update(float dt)
    {
        if (!IsIndexValid()) return;

        _anims[_idx].Update(dt);
        if (!_anims[_idx].IsCompleted) return;
        switch (Direction)
        {
            case Direction.Forward:
                if (IsHaveNext()) _idx += 1;


                if (!IsIndexValid()) return;
                _anims[_idx].Forward();
                break;
            case Direction.Reverse:
                if (IsHavePrev()) _idx -= 1;

                if (!IsIndexValid()) return;
                _anims[_idx].Reverse();

                break;
        }
    }

    public Direction Direction { get; private set; } = Direction.Forward;

    public Action? OnCompleted
    {
        get => !IsIndexValid() ? null : _anims[_idx].OnCompleted;
        set
        {
            if (IsIndexValid()) _anims[_idx].OnCompleted = value;
        }
    }

    public bool IsCompleted => _anims.All(animation => animation.IsCompleted);

    private bool IsHaveAnims()
    {
        return _anims.Any();
    }

    private bool IsIndexValid()
    {
        return _idx >= 0 && _idx <= _anims.Length - 1;
    }

    private bool IsHaveNext()
    {
        return _idx + 1 <= _anims.Length;
    }

    private bool IsHavePrev()
    {
        return _anims.Length - _idx - 1 >= 0;
    }
}