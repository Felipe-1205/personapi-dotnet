-- 1. Crear la base de datos
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'persona_db')
BEGIN
    CREATE DATABASE persona_db;
END;
GO

USE persona_db;
GO

-- 2. Tabla profesion
CREATE TABLE profesion (
    id   INT IDENTITY(1,1) PRIMARY KEY,
    nom  VARCHAR(90)       NOT NULL,
    des  VARCHAR(MAX)      NULL
);
GO

-- 3. Tabla persona
CREATE TABLE persona (
    cc        INT           PRIMARY KEY,
    nombre    VARCHAR(45)   NOT NULL,
    apellido  VARCHAR(45)   NOT NULL,
    genero    CHAR(1)       NOT NULL
                 CONSTRAINT chk_genero CHECK (genero IN ('M','F')),
    edad      INT           NULL
);
GO

-- 4. Tabla estudios (PK compuesta)
CREATE TABLE estudios (
    id_prof   INT           NOT NULL,
    cc_per    INT           NOT NULL,
    fecha     DATE          NULL,
    univer    VARCHAR(50)   NULL,
    CONSTRAINT PK_estudios PRIMARY KEY (id_prof, cc_per),
    CONSTRAINT FK_estud_prof FOREIGN KEY (id_prof)  REFERENCES profesion(id),
    CONSTRAINT FK_estud_per  FOREIGN KEY (cc_per)  REFERENCES persona(cc)
);
GO

-- 5. Tabla telefono
CREATE TABLE telefono (
    num    VARCHAR(15) PRIMARY KEY,
    oper   VARCHAR(45) NOT NULL,
    duenio INT         NOT NULL,
    CONSTRAINT FK_tel_per FOREIGN KEY (duenio) REFERENCES persona(cc)
);
GO
