## Instrucciones de integración y manejo del repositorio 

### Clonar el repositorio:
Esto permite descargar y acceder de manera local al código.

1. Abrir una consola de Git Bash en el folder dónde se desea guardar el proyecto
2. En la consola, ejecutar el siguiente comando:
```
git clone https://github.com/VivianGomez/proyecto75926.git
```
3. Abrir el proyecto en Unity 2019.4.2f1 (Si no se tiene esa vesión, se puede descargar desde https://unity3d.com/es/unity/whats-new/2019.4.2)

### Hacer commit y push:
Esto permite subir cambios estables al master, es decir, subir nuestros avances o finalizaciones de alguna tarea (issue) para que queden en la versión final del prototipo.

1. Abrir una consola de Git Bash en el folder dónde se tiene el proyecto
2. En la consola, ejecutar el comando que permite verificar los cambios que se han realizado de manera local
```
git status
```
3. Luego, tras verificar los cambios y asegurarnos de que no subirems ningún bug o posible error en el master. Ejecutar el comando que permite agregar TODOS los cambios locales al commit
```
git add .
```
4. Ejecutar el comando que permite crear el commit. Nombraremos los commits con oraciones claras, uqe describan de manera resumida el cambio que se está subiendo. Por ejemplo: "Se realizó modificación de ...", "Se creó el modelo de ...", "Se optimizó el Script ...", "Se resolvió el issue 12: corrección de texturas de ...". 
```
git commit -m"Se realizó modificación de la Escena principal, para agregar modelos finales de consola"
```
**IMPORTANTE!!!** Estar seguros de que estamos actualizados con master ANTES de hacer push, para evitar conflictos o corregirlos de manera local antes de subir nuestros cambios. Para ello, verificar que estamos al día con el último commit o comunicarnos con el equipo para saber si alguien modificó los mismos archivos que nosotros (esto no debería pasar, pero es una medida preventiva). 

Siempre hacer PULL antes de commit, esto permitirá estar "al día" con el master, antes de subir nuestros cambios. Para ello, ejecutar el comando 
```
git pull
```
5. Ejecutar el comando que permite subir nuestro commit y por ende nuestros cambios al maste
```
git push
```






