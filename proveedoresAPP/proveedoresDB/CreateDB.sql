CREATE DATABASE ProveedoresDB;
GO

-- Usar la base de datos recién creada
USE ProveedoresDB;
GO

-- Crear tabla Proveedores
CREATE TABLE Proveedores (
    idProveedor INT IDENTITY(1,1) PRIMARY KEY,
    Codigo NVARCHAR(20) UNIQUE,
    RazonSocial NVARCHAR(150),
    RFC CHAR(13)
);

-- Crear tabla Productos
CREATE TABLE Productos (
    idProducto INT IDENTITY(1,1) PRIMARY KEY,
    idProveedor INT,
    Codigo NVARCHAR(20) UNIQUE,
    Descripcion NVARCHAR(150),
    Unidad NVARCHAR(3),
    Costo DECIMAL (10,2),
    CONSTRAINT FK_Proveedor_Producto FOREIGN KEY (idProveedor) REFERENCES Proveedores(idProveedor)
);

-- Crear índice para la llave foránea
CREATE INDEX IX_Proveedor_Producto ON Productos (idProveedor);
