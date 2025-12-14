


CREATE TABLE Hogar (
    UsuarioID INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Correo VARCHAR(100) UNIQUE NOT NULL,
    NumeroCasa VARCHAR(20) NOT NULL
);


CREATE TABLE MaterialReciclable (
    MaterialID INT PRIMARY KEY IDENTITY(1,1),
    Tipo VARCHAR(50) NOT NULL,         
    NombreMaterial VARCHAR(100) NOT NULL,
    PuntosPorUnidad DECIMAL(10,2) NOT NULL
);


CREATE TABLE RegistroReciclaje (
    RegistroID INT PRIMARY KEY IDENTITY(1,1),
    UsuarioID INT NOT NULL,
    MaterialID INT NOT NULL,
    Cantidad DECIMAL(10,2) NOT NULL,       
    PuntosUnitarios DECIMAL(10,2) NOT NULL, 
    PuntosTotales DECIMAL(10,2) NOT NULL,   
    CONSTRAINT FK_Usuario FOREIGN KEY (UsuarioID) REFERENCES Hogar(UsuarioID),
    CONSTRAINT FK_Material FOREIGN KEY (MaterialID) REFERENCES MaterialReciclable(MaterialID)
);


-- Tabla hogar
-- CREATE
INSERT INTO Hogar (Nombre, Correo, NumeroCasa) VALUES ('Juan Pérez', 'juanperez@mail.com', 'A-12');

-- READ
SELECT * FROM Hogar;

-- UPDATE
UPDATE Hogar
SET Nombre = 'Juan Ramírez', Correo = 'juanramirez@mail.com' WHERE UsuarioID = 1;

-- DELETE
DELETE FROM Hogar
WHERE UsuarioID = 1;

--Tabla MaterialReciclable

-- CREATE
INSERT INTO MaterialReciclable (Tipo, NombreMaterial, PuntosPorUnidad) VALUES ('Plástico', 'Botella ', 2.5);

-- READ
SELECT * FROM MaterialReciclable;

-- UPDATE
UPDATE MaterialReciclable SET PuntosPorUnidad = 3.0 WHERE MaterialID = 1;

-- DELETE
DELETE FROM MaterialReciclable WHERE MaterialID = 1;




--Tabla Registro
INSERT INTO RegistroReciclaje (UsuarioID, MaterialID, Cantidad, PuntosUnitarios, PuntosTotales) VALUES (1, 2, 10, 2.5, 25.0);

-- READ
SELECT rr.RegistroID, h.Nombre, m.NombreMaterial, rr.Cantidad, rr.PuntosUnitarios, rr.PuntosTotales
FROM RegistroReciclaje rr
JOIN Hogar h ON rr.UsuarioID = h.UsuarioID
JOIN MaterialReciclable m ON rr.MaterialID = m.MaterialID;

-- UPDATE
UPDATE RegistroReciclaje
SET Cantidad = 12, PuntosTotales = 30.0 WHERE RegistroID = 1;

-- DELETE
DELETE FROM RegistroReciclaje WHERE RegistroID = 1;