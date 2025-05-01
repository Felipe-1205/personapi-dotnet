USE persona_db;
GO

-- Insertar profesiones
INSERT INTO profesion (nom, des) VALUES
 ('Ingeniero', 'Profesional en ingeniería'),
 ('Médico',    'Profesional de la salud'),
 ('Profesor',  'Docente académico');
GO

-- Insertar personas
INSERT INTO persona (cc, nombre, apellido, genero, edad) VALUES
 (1001, 'Andres',  'Marroquin', 'M', 21),
 (1002, 'Juan', 'Granada',  'M', 22),
 (1003, 'Jaime','Lepelei', 'M', 22);
GO

-- Insertar estudios
INSERT INTO estudios (id_prof, cc_per, fecha, univer) VALUES
 (1, 1001, '2018-06-15', 'Uni Nacional'),
 (3, 1002, '2012-12-01', 'Universidad Javeriana'),
 (2, 1003, '2020-09-10', 'Facultad de Medicina');
GO

-- Insertar teléfonos
INSERT INTO telefono (num, oper, duenio) VALUES
 ('3001234567', 'Claro',    1001),
 ('3107654321', 'Movistar', 1002),
 ('3129998888', 'Tigo',     1003);
GO
