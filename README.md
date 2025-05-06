
# PersonAPI

## Descripción

PersonAPI es una aplicación desarrollada en .NET Core que permite gestionar personas, profesiones, estudios y teléfonos. Este proyecto incluye una API REST y una interfaz web para interactuar con los datos.

---

## Cómo ejecutar el proyecto

El proyecto está configurado para ejecutarse fácilmente utilizando **Docker Compose**. Sigue estos pasos:

1. Asegúrate de tener **Docker** y **Docker Compose** instalados en tu máquina.
2. Abre una terminal en la raíz del proyecto.
3. Ejecuta el siguiente comando:

```bash
docker-compose up
```

Esto levantará la aplicación y estará disponible en `http://localhost:5000`.

---

## Archivos DDL y DML

Los scripts necesarios para inicializar la base de datos se encuentran en la carpeta `Scripts`:

-**DDL (Definición de la base de datos):**

- Archivo: `init.sql`
- Ubicación: `/scripts/init.sql`

-**DML (Datos iniciales):**

- Archivo: `data.sql`
- Ubicación: `/scripts/data.sql`

Estos scripts definen la estructura de la base de datos y cargan datos iniciales como personas, profesiones, estudios y teléfonos.

---

## Cómo usar la aplicación

### 1. Acceso a la interfaz web

Puedes acceder a la interfaz web para gestionar los datos desde tu navegador en la siguiente dirección:

-**Página principal:**

  `http://localhost:5000/`

Desde aquí, podrás navegar por las diferentes secciones de la aplicación, como personas, profesiones, estudios y teléfonos.

### 2. Acceso a la API REST

La API REST está documentada con **Swagger**. Puedes acceder a la documentación interactiva en:

-**Swagger UI:**

  `http://localhost:5000/swagger/index.html`

Desde Swagger, puedes probar los endpoints de la API directamente, como crear, editar, eliminar y consultar datos.

---

## Pruebas y validación

1.**Interfaz web:**

- Navega a `http://localhost:5000/` y utiliza las opciones disponibles para gestionar los datos.
- Por ejemplo, puedes crear una nueva persona, asignarle un teléfono o registrar un estudio.

2.**API REST:**

- Abre `http://localhost:5000/swagger/index.html` para probar los endpoints de la API.
- Prueba operaciones como:

  - Crear una nueva persona (`POST /api/persona/create`).
  - Consultar detalles de un estudio (`GET /api/estudio/details/{idProf}/{ccPer}`).
  - Eliminar un teléfono (`DELETE /api/telefono/delete/{num}`).

---

## Notas adicionales

- Asegúrate de que los contenedores de Docker estén corriendo antes de intentar acceder a la aplicación.
- Si necesitas reiniciar la base de datos, puedes volver a ejecutar los scripts `init.sql` y `data.sql` en tu gestor de base de datos favorito.

---

¡Gracias por usar PersonAPI! 🚀
