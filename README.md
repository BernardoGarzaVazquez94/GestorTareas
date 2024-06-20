#Descripción
Gestor de Tareas es una aplicación web de mini gestión de tareas que permite a los usuarios crear, editar, eliminar y listar tareas. 
La aplicación utiliza .NET para el backend y Blazor para el frontend, con Radzen como complemento para componentes de interfaz de usuario.

#Características
1)Autenticación de Usuarios:
	Registro e inicio de sesión de usuarios.
	Autenticación mediante JWT (JSON Web Tokens).
2)Gestión de Tareas:
	Crear tareas con título, descripción y fecha de vencimiento.
	Listar todas las tareas del usuario.
	Editar detalles de tareas existentes.
	Eliminar tareas no necesarias.
3)Interfaz de Usuario:
	Interfaz limpia, funcional y responsiva.
	Utilización de Radzen para mejorar la experiencia de usuario.

# GestorTareas
Aplicación para la generación de tareas
La apliación fue diseña base una aplicación de blazor webassembly, la aplicación como tal cuenta con dos aplicaciones.
Una aplicación es del lado del cliente y otra aplicación es del lado del servidor, esto con el objetivo de crear un CORE de API para que multiples sitios pudieran consumir los servicio de la aplicación server,ademas asegurando una escabilidad del proyecto.
La aplicación tiene una capa de seguridad mediante token JWT usando los data anotation [Authorize] en los controller, asegurando que solamente las personas actividad de TOKEN puedan consumir los recursos de la API.
La aplicación cuenta con un performance de inyección de dependcias para que la manera del escalado de la programación sea de manera modular y se puedan reutilizar las funciones en direntes partes del codigo.


Ventajas de Blazor
-Reutilización de código: Al usar C# tanto en el frontend como en el backend, se puede compartir más código entre la lógica de la aplicación y el servidor, reduciendo la duplicación de código y facilitando el mantenimiento.
-Componentes reutilizables: Blazor permite crear componentes reutilizables que pueden ser utilizados en diferentes partes de la aplicación, mejorando la modularidad y la mantenibilidad del código.
-Modelo de programación unificado: Blazor utiliza un modelo de programación similar al de ASP.NET MVC, lo que facilita la curva de aprendizaje para los desarrolladores que ya están familiarizados con este framework.
-Soporte para WebAssembly: Blazor WebAssembly permite ejecutar aplicaciones web directamente en el navegador utilizando WebAssembly, lo que puede mejorar el rendimiento y la experiencia del usuario.

Ventajas de .NET Core
-Multiplataforma: .NET Core es compatible con Windows, macOS y Linux, lo que permite desarrollar y ejecutar aplicaciones en diferentes sistemas operativos sin cambios significativos en el código.
-Alto rendimiento: .NET Core está diseñado para ser rápido y eficiente, lo que lo hace ideal para aplicaciones de alto rendimiento y microservicios.
-Microservicios: .NET Core es una opción excelente para construir arquitecturas basadas en microservicios gracias a su capacidad de crear aplicaciones pequeñas y desplegables de manera independiente.
-Contenedores: .NET Core se integra bien con Docker, facilitando la creación, despliegue y escalado de aplicaciones en contenedores
-Open Source: Al ser de código abierto, .NET Core tiene una comunidad activa y en crecimiento, lo que contribuye a la mejora continua y a la disponibilidad de recursos y soporte.




#Ficha tecnica
-Lenguaje .net Core 6 
-IDE visual studio 2022
-Base de datos SQL SERVER 2019
-Frameworks Blazor y componentes de radzen
-Entity Framework SQL



#Instrucciones de ejecución de la aplicación.
1.-Como primer paso es necesario crear una base de datos con el nombre de GestionTareas
2.-Una vez creada la base de datos es necesario crear las tablas de usuarios y de tareas (Scripts en la parte de abajo)
3.-Abrir la solución del proyecto, el proyecto esta generado usando blazor web assamby en el cual se tienen dos aplicaciones, una es el cliente y otra es el servidor.
4.-Buscar el archivo con nombre appsettings.json en el proyecto de GestorTareas.Server y cambiar las credenciales de la conexión.
5.-Ejecutar la aplicación y probar la funcionalidad de crear tareas.

#Uso de la Aplicación
1.-Registro e Inicio de Sesión
2.-Abre la aplicación en tu navegador y navega a la página de registro para crear una nueva cuenta.
3-Inicia sesión con tus credenciales.
4-Una vez autenticado, podrás crear nuevas tareas desde la página de creación de tareas.
5-Podrás ver todas tus tareas en la página de listado de tareas.
6-Podrás editar y eliminar tareas desde la página de detalle de cada tarea.


#Validar el token JWT desde postman
ruta:de tipo [POST] https://localhost:7128/api/Auth/login
body 
{
    "Username":"Bernardo",
    "Password":"garza123"
}

#Scripts para la generación de la base de datos
-- Crear la base de datos GestionTareas
CREATE DATABASE GestionTareas
ON PRIMARY (
    NAME = GestionTareas_Data,
    FILENAME = 'C:\GestionTareas\GestorTareas\GestionTareas_Data.mdf',  -- Cambia la ruta según tu entorno
    SIZE = 10MB,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 1MB
)
LOG ON (
    NAME = GestionTareas_Log,
    FILENAME = 'C:\GestionTareas\GestorTareas\GestionTareas_Log.ldf',  -- Cambia la ruta según tu entorno
    SIZE = 5MB,
    MAXSIZE = 2GB,
    FILEGROWTH = 10%
);
GO

-- Crear tabla de usuarios
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL
);

-- Crear tabla de tareas
CREATE TABLE Tareas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Titulo NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255),
    FechaVencimiento DATETIME,
    UsuarioId INT NOT NULL,
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);
GO