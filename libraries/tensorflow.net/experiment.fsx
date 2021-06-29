#r "nuget: TensorFlow.NET"
#r "nuget: TensorFlow.Keras"
#r "nuget: SciSharp.TensorFlow.Redist"
#r "nuget: NumSharp"

open NumSharp
open Tensorflow
open Tensorflow.Keras
open type Tensorflow.Binding
open type Tensorflow.KerasApi

let tf = New<tensorflow>()
tf.enable_eager_execution()

let layers = 
    [
        keras.layers.Dense(64, keras.activations.Relu)
        keras.layers.Dense(64, keras.activations.Relu)
        keras.layers.Dense(4)
    ]

let model = keras.Sequential(layers)
let lossFunction = keras.losses.SparseCategoricalCrossentropy(from_logits = true)
let optimizer = keras.optimizers.Adam()
model.compile(lossFunction, optimizer, [| "accuracy" |])

model.summary()
