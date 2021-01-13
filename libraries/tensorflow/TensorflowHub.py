from tensorflow import keras
import tensorflow_hub


def inspect_hub_model():
    print("Model Details:")
    model = keras.Sequential([
        tensorflow_hub.KerasLayer("https://tfhub.dev/tensorflow/resnet_50/classification/1")
    ])
    model.build(input_shape=[None, 224, 224, 3])
    model.summary()


if __name__ == '__main__':
    inspect_hub_model()