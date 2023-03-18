create database LoteriaMexicana;
go
use LoteriaMexicana;
go
--DROP TABLE Cartas
CREATE TABLE Cartas (
id_carta INT IDENTITY(1,1) PRIMARY KEY,
nombre VARCHAR(50) NOT NULL
);
go
--DROP TABLE Tablas
CREATE TABLE Tablas (
id_tabla INT IDENTITY(1,1) PRIMARY KEY,
nombre VARCHAR(50) 
);
go
--DROP TABLE CartasPorTabla
CREATE TABLE CartasPorTabla (
id_CartasPorTabla INT PRIMARY KEY IDENTITY,
id_carta INT,
id_tabla INT,
posicion_f INT,
posicion_c INT,
FOREIGN KEY (id_carta) REFERENCES Cartas(id_carta),
FOREIGN KEY (id_tabla) REFERENCES Tablas(id_tabla)
);
go
-- =============================REGISTRO===========================================
INSERT INTO cartas (nombre) VALUES 
('El gallo'),
('El diablito'),
('La dama'),
('El catrín'),
('El paraguas'),
('La sirena'),
('La escalera'),
('La botella'),
('El barril'),
('El árbol'),
('El melón'),
('El valiente'),
('El gorrito'),
('La muerte'),
('La pera'),
('La bandera'),
('El bandolón'),
('El violoncello'),
('La garza'),
('El pájaro'),
('La mano'),
('La bota'),
('La luna'),
('El cotorro'),
('El borracho'),
('El negrito'),
('El corazón'),
('La sandía'),
('El tambor'),
('El camaron'),
('Las jaras'),
('El músico'),
('La araña'),
('El soldado'),
('La estrella'),
('El cazo'),
('El mundo'),
('El apache'),
('El nopal'),
('El alacrán'),
('La rosa'),
('La calavera'),
('La campana'),
('El cantarito'),
('El venado'),
('El sol'),
('La corona'),
('La chalupa'),
('El pino'),
('El pescado'),
('La palma'),
('La maceta'),
('El arpa');
-- =======================PROCEDIMIENTO==============================

CREATE OR ALTER PROCEDURE spAgregarTabla (
  @nombre VARCHAR(255) 
)
AS
BEGIN
  INSERT INTO Tablas (nombre) 
  VALUES (@nombre);
END;



CREATE OR ALTER PROCEDURE sp_InsertarCartasPorTabla
    @id_carta INT,
    @id_tabla INT,
    @fila INT,
    @columna INT
AS
BEGIN
    INSERT INTO CartasPorTabla ( id_carta, id_tabla, posicion_f, posicion_c)
    VALUES ( @id_carta, @id_tabla, @fila, @columna);
END