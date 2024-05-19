# Gestión de Proveedores y Productos

Este proyecto es una aplicación web para el manejo de proveedores y sus productos asociados, desarrollado con Visual Studio .NET utilizando C#. La aplicación permite realizar altas, bajas y cambios de proveedores, y gestionar un catálogo de productos por proveedor, incluyendo funcionalidades de alta y consulta de productos.

## Características del Desarrollo

- **Tecnología:** Visual Studio .NET
- **Lenguaje:** C#
- **Arquitectura:** Programación orientada a objetos en 3 capas (modelo, vista, controlador) utilizando ASP.Net Core o MVC
- **Manejo de errores:** Adecuado manejo de errores para mostrar mensajes adecuados al usuario en caso de fallos

## Base de Datos

- **Administrador de Base de Datos:** SQL Server
- **Estructura:** Creación de tablas, llaves, relaciones e índices necesarios
- **Consultas y Procedimientos:** Creación de consultas, funciones y stored procedures necesarios

## Validaciones Implementadas

### Validaciones para Proveedores

1. El código del proveedor es único.
2. El RFC del proveedor debe tener 13 caracteres (4 letras + 6 dígitos + 1 letra + 2 dígitos).
3. Al intentar eliminar un proveedor:
   - Si el proveedor tiene productos asociados, se solicita una confirmación antes de eliminarlo.
   - Si el proveedor no tiene productos asociados, se elimina sin solicitar confirmación.
   - Al eliminar un proveedor, se eliminan en cascada los productos asociados.

### Validaciones para Productos

1. El valor del costo del producto debe ser un valor decimal.

## Funcionalidades de la Aplicación

### Proveedores

- **Alta de Proveedores:** Permite agregar nuevos proveedores.
- **Baja de Proveedores:** Permite eliminar proveedores, con manejo de confirmación y eliminación en cascada.
- **Cambio de Proveedores:** Permite modificar la información de proveedores existentes.

### Productos

- **Alta de Productos:** Permite agregar nuevos productos asociados a proveedores.
- **Consulta de Productos:** Permite consultar los productos asociados a cada proveedor.

## Instalación y Configuración

1. Clonar el repositorio:
   git clone https://github.com/Yakidb/proveedoresAPP.git

2. Dentro del proyecto proveedoresAPP hay una carpeta llamada proveedoresDB la cual contiene el script para crear la base de datos

3. Configurar la cadena de conexión:
Abre el archivo appsettings.json y actualiza la cadena de conexión para que apunte a tu instancia de SQL Server.

4. Ejecutar la aplicación:
Abre el proyecto en Visual Studio y ejecuta la aplicación.
