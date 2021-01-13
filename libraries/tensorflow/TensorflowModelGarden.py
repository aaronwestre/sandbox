from official.vision.image_classification.resnet import resnet_model


def inspect_garden_model():
    print("Model Details:")
    model = resnet_model.resnet50(4)
    model.summary()


if __name__ == '__main__':
    inspect_garden_model()