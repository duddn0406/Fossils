using Leap;
using UnityEngine;

public class LeapHandTracker
{
    private Controller controller;

    public Hand LeftHand { get; private set; }
    public Hand RightHand { get; private set; }

    public LeapHandTracker()
    {
        controller = new Controller();
    }

    public void Update()
    {
        Frame frame = controller.Frame();
        LeftHand = null;
        RightHand = null;

        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsLeft) LeftHand = hand;
            if (hand.IsRight) RightHand = hand;
        }
    }
}
