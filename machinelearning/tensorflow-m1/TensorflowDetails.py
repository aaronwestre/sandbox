import tensorflow


def accelerator_name():
    return tensorflow.test.gpu_device_name()
