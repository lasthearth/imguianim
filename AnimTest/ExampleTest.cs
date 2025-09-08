using System.Numerics;
using imguianim;
using imguianim.Animation;
using imguianim.Tween;

namespace AnimTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void RepeatTest()
    {
        var vector = new Vector2();
        var controller = new AnimationController();
        var tween = new TweenVector2(Vector2.One, Vector2.One * 10, Curves.EaseInOutCubic);
        var clip = new AnimationClip<Vector2>(controller, tween, vector2 => vector = vector2);

        var repeat = new AnimationRepeat(clip);
        repeat.Forward();

        var idx = 0;
        while (idx != 4)
        {
            if (repeat.IsCompleted) idx++;
            repeat.Update(0.033f);

            Console.WriteLine(vector);
        }
    }

    [Test]
    public void OpacityTest()
    {
        var opacity = 0f;
        var controller = new AnimationController();
        var tween = new TweenFloat(
            0f,
            1f,
            Curves.EaseInOutCubic
        );
        var clip = new AnimationClip<float>(controller, tween, v => opacity = v);

        clip.Forward();

        while (!clip.IsCompleted)
        {
            clip.Update(0.033f);
            Console.WriteLine(opacity);
        }
        
        clip.Reverse();
        while (!clip.IsCompleted)
        {
            clip.Update(0.033f);
            Console.WriteLine(opacity);
        }
    }

    [Test]
    public void ColorTest()
    {
        var vector = new Vector4();
        var controller = new AnimationController();
        var tween = new TweenVector4(
            Vector4.Zero,
            new Vector4(
                1f,
                1f,
                1f,
                1
            ),
            Curves.EaseInOutCubic
        );
        var clip = new AnimationClip<Vector4>(controller, tween, vector4 => vector = vector4);

        clip.Forward();

        while (!clip.IsCompleted)
        {
            clip.Update(0.033f);
            Console.WriteLine(vector);
        }
    }
}