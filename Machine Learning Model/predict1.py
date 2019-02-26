from keras.models import load_model
import numpy as np
import cv2
import argparse
import urllib.request

labels = {
    0: 'shoe',
    1: 'dress',
    2: 'pants',
    3: 'outerwear'
}


def url_to_image(url):
    resp = urllib.request.urlopen(url)
    image = np.asarray(bytearray(resp.read()), dtype="uint8")
    image = cv2.imdecode(image, cv2.IMREAD_COLOR)
    return image


parser = argparse.ArgumentParser(description='Predict dress from image.')
parser.add_argument('-i', '--image', help='Image input url', type=str)

args = vars(parser.parse_args())

img_url = args['image']

img = url_to_image(img_url)
img = cv2.resize(img, (300, 300))
img = img.reshape(1, 300, 300, 3)

model = load_model("my_model2.h5")
predicted = model.predict(img)

label = np.argmax(predicted)
print(labels[label])
