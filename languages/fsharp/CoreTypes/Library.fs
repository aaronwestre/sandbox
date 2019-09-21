namespace CoreTypes

type Behavior = float -> float

type BehaviorBuilder = interface end

type WaveBehavior =
    | Sine
    | Triangle
    | Sawtooth
    interface BehaviorBuilder

type BehaviorCollection = interface end

type ScalarBehavior =
    | Wave
    | Envelope
    | Scale
    interface BehaviorCollection

type Topic = interface end

type CoreTopic =
    | Scalar
    | Vector
    | Matrix
    interface Topic
    
module topic =
    let topicToString (topic : Topic) = topic.ToString()
    