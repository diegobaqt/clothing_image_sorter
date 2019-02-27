<h3>Clothing Image Sorter</h3>

<h4>Introducción</h4>

Debido a la movilización de actividades de la vida cotidiana hacia el internet en la era digital, negocios como el comercio electrónico comienzan a utilizar más herramientas para satisfacer a sus clientes. La industria de la moda representa uno de los grandes rubros del sector de comercio electrónico, donde sus clientes tienen necesidades que deben ser satisfechas. <br>

Inno Clothing Sorter, es una aplicación que utiliza un modelo de entrenamiento basado en redes neuronales convolucionales al cual se puede acceder mediante una aplicación móvil, que se comunica con el modelo mediante un RESTful API; donde el usuario de la aplicación podrá subir una imagen y el modelo de Machine Learning propuesto podrá clasificarla en las diferentes clases existenes (Dresses, Shoes, Outerwear y Pants). Sin embargo, es necesario mencionar que aún el proyecto dista mucho del objetivo que se quiere lograr, el cual es clasificar el tipo de ropa que una persona ha comprado, para brindarle recomendaciones acertadas prendas que posiblemente puede comprar, según sus gustos.

<h4>Modelo de Machine Learning</h4>

Se plantea el reto de la clasificación de imágenes de prendas de vestir según su categoría, implementamos un modelo de aprendizaje de máquina basado redes neuronales convolucionales basado en DenseNet 121 y un algoritmo de optimización RMSprop, haciendo uso de Keras y OpenCV. Adicionalmente, se realizó el procesamiento de 3260 de imágenes, donde el 80% de ellas fueron usadas para entrenamiento y el restante para testing y validación. El modelo fue desarrollado e implementado obteniendo una precisión (Accuracy) de aproximadamente 75\%.

<h4>Aplicación</h4>

La solución implementada hasta el momento está compuesta por los siguientes componentes:

* Aplicación Móvil
* API (Servicios expuestos a la aplicación)
* Flask (Ejecución del modelo)

