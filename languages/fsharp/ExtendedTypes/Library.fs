namespace ExtendedTypes

open CoreTypes

type CharacterBehavior =
    | A
    | B
    | C
    interface BehaviorBuilder

type WordBehavior =
    | Long
    | Medium
    | Short
    interface BehaviorCollection

type ExtendedTopic =
    | Words
    | Sentences
    | Paragraphs
    interface Topic

module Say =
    let hello name =
        printfn "Hello %s" name
