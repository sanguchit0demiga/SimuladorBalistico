 Simulador de Fisica Balística en 3D
Este es un proyecto de simulación balística en 3D, donde podes controlar un cañón para derribar una pared de cubos.


Cómo jugar:

1.  **Ajusta el Cañón:**
    * Usa los **sliders** en la interfaz para modificar el **ángulo** y la **fuerza** del disparo.
    * Selecciona la **masa del proyectil** con el menú desplegable.
    * Usa los **botones de flecha** (o los de la UI) para rotar el cañón horizontalmente.

2.  **Dispara:**
    * Presiona la tecla **[Espacio]** para lanzar el proyectil.

3.  **Reinicia:**
    * Haz clic en el botón de **[Reiniciar]** para reconstruir la pared y comenzar una nueva ronda.

 Características y Reporte de Tiro:

Al final de cada disparo, verás un reporte en pantalla que incluye:
* **Tiempo de Vuelo:** El tiempo que el proyectil tardó en impactar el objetivo.
* **Velocidad Relativa:** La velocidad del proyectil en el momento de la colisión.
* **Puntuación:** Tu puntaje total, que aumenta a medida que derribas cubos.
  
 Versión de Unity:
 
 Este proyecto fue desarrollado en **Unity 6 **.

 Criterios de evaluación:
 
 Objetivo:
 
Construir un simulador balístico donde el jugador regula ángulo, fuerza y masa del proyectil para derribar objetivos conectados con Joints. El disparo y las colisiones deben estar gobernados por Rigidbody y el sistema de físicas de Unity. Registrar resultados de impacto para evaluar precisión y potencia.

Requisitos mínimos:

Controles de disparo en pantalla:
Ángulo y fuerza con Slider o InputField.
Masa del proyectil seleccionable

Disparo físico:
Proyectil con Rigidbody y Collider.
Lanzamiento por AddForce o velocity según el ángulo configurado.

Escena de objetivos:
Estructuras armadas con Rigidbodies y Joints (FixedJoint, HingeJoint o SpringJoint).
Estabilidad inicial correcta. Si se cae sola, está mal configurada.

Registro del resultado:
Guardar datos como tiempo de vuelo, punto de impacto, velocidad relativa, impulso de colisión y piezas derribadas.
Mostrar al final de cada intento: puntuación y un breve “reporte de tiro”.

Link video de YouTube:
https://youtu.be/LmHyzrVbwic?si=55iIrYuuoQF_ZIsU
