from tensorflow import keras


def inspect_applications_model():
    resnet_model = keras.applications.resnet50.ResNet50(weights=None)
    resnet_model.summary()
    input_layer = keras.Input(shape=[None, 224, 224, 3])
    model_layers = resnet_model.layers.pop()(input_layer)
    output_layer = keras.layers.Dense(units=4, activation="relu")(model_layers)
    model = keras.Model(inputs=input_layer, outputs=output_layer, name="sheet_classifier")
    model.summary()


if __name__ == '__main__':
    inspect_applications_model()