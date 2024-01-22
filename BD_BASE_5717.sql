IF EXISTS (SELECT*  FROM sysdatabases WHERE name = 'ROExpress')
                DROP DATABASE ROExpress
CREATE DATABASE ROExpress 

USE ROExpress 
IF OBJECT_ID('Statii','U') IS NOT NULL DROP TABLE Statii
CREATE TABLE Statii( 
     
    ID_Statie int IDENTITY(1,1) PRIMARY KEY, 
	Nume_Oras varchar(255) not null,
	Latitudine float not null, 
	Longitudine float not null
     
)


USE ROExpress 
IF OBJECT_ID('Calatorii','U') IS NOT NULL DROP TABLE Calatorii
CREATE TABLE Calatorii( 
     
	 ID_Calatorie int IDENTITY(1,1) PRIMARY KEY, 
	 ID_Statie_Plecare int FOREIGN KEY REFERENCES Statii(ID_Statie),  
	 Oras_Sosire varchar(255) not null, 
	 Ora_Plecare TIME not null, 
	 Ora_Sosire TIME  not null

)  


USE ROExpress 
IF OBJECT_ID('Trenuri','U') IS NOT NULL DROP TABLE Trenuri
CREATE TABLE Trenuri( 
     
      ID_Tren int PRIMARY KEY, 
	  Tip_Tren varchar(5) not NULL

)

USE ROExpress 
IF OBJECT_ID('Statii_Tren','U') IS NOT NULL DROP TABLE Statii_Tren
CREATE TABLE Statii_Tren( 
     
	 ID_Statie_Tren int IDENTITY(1,1) PRIMARY KEY, 
	 ID_Statie int FOREIGN KEY REFERENCES Statii(ID_Statie),  
	 ID_Tren int FOREIGN KEY REFERENCES Trenuri(ID_Tren), 
	 Ora_Plecare TIME not null, 
	 Ora_Sosire TIME  not null
) 


USE ROExpress 
IF OBJECT_ID('Tipuri_Vagoane','U') IS NOT NULL DROP TABLE Tipuri_Vagoane
CREATE TABLE Tipuri_Vagoane( 
     
	 ID_Vagon int IDENTITY(1,1) PRIMARY KEY, 
     Clasa_Vagon int not null, 
	 Nr_Locuri int not null
)       

USE ROExpress 
IF OBJECT_ID('Vagoane_Tren','U') IS NOT NULL DROP TABLE Vagoane_Tren
CREATE TABLE Vagoane_Tren( 
     
	 ID_Vagoane_Tren int IDENTITY(1,1) PRIMARY KEY, 
     ID_Tren int FOREIGN KEY REFERENCES Trenuri(ID_Tren),
	 ID_Vagon int FOREIGN KEY REFERENCES Tipuri_Vagoane(ID_Vagon), 
	 Nr_Vagon int not null
)       


USE ROExpress 
INSERT INTO Tipuri_Vagoane(Clasa_Vagon,Nr_Locuri) 
VALUES 
     (1,151), 
	 (2,142)

USE ROExpress
INSERT INTO Statii(Nume_Oras,Latitudine,Longitudine) 
VALUES 
	('Suceava',47.671485,26.260768),    --1 ID
	('Pascani',47.2569,26.7250),        --2 ID
	('Roman',46.935938,26.919193), 
	('Bacau',46.567442,26.892728), 
	('Adjud',46.0972,27.194345), 
	('Focsani',45.700142,27.1685),		     --6 ID
	('Buzau',45.142420,26.828907),
	('Ploiesti',44.9237,26.025189), 
	('Bucuresti Nord', 44.447296,26.072599), -- 9 ID
	('Fetesti',44.417624,27.823130),         --10 ID     
	('Cernavoda Pod',44.336614,28.026070),   --11 ID
	('Medgidia',44.252780,28.270178),        --12 ID
	('Constanta',44.169634,28.630793)        --13 ID

USE ROExpress 
INSERT INTO Trenuri(ID_Tren,Tip_Tren)
VALUES 
  (552,'IC'), 
  (1752,'IR'), 
  (1754,'IR'), 
  (1656,'IR'), 
  (5412,'R'), 
  (5002,'R-E'), 
  (1655,'IR'), 
  (1753,'IR'), 
  (551,'IC'),
  (5053,'R'),
  (1581,'IR'), 
  (1583,'IR'), 
  (581,'IC'), 
  (11081,'R-E'), 
  (582,'IC')


USE ROExpress 
INSERT INTO Vagoane_Tren 
VALUES 
      (552,1,1), 
	  (552,1,2), 
	  (552,1,3),
      (1752,2,1),
	  (1752,1,2),
	  (1752,2,3),
	  (1754,2,1),
	  (1754,1,2),
	  (1754,2,3),
	  (1656,2,1),
	  (1656,1,2),
	  (1656,2,3),
	  (5412,2,1),
	  (5412,2,2), 
	  (5002,2,1),
	  (5002,2,2), 
	  (1655,2,1),
	  (1655,2,2),
	  (1753,1,1), 
	  (1753,1,2), 
	  (1753,1,2),
	  (551,2,1),
	  (551,2,2),
	  (5053,2,1),
	  (5053,1,2),
	  (5053,2,3),
      (1581,2,1),
	  (1581,1,2),
	  (1581,2,3),
	  (1583,2,1),
	  (1583,1,2),
	  (1583,2,3),
	  (581,1,1), 
	  (581,1,2), 
	  (581,1,2),
	  (582,1,1), 
	  (582,1,2), 
	  (582,1,2),
	  (11081,2,1),
	  (11081,2,2) 

USE ROExpress 
INSERT INTO Calatorii(ID_Statie_Plecare,Oras_Sosire,Ora_Plecare,Ora_Sosire) 
VALUES 
      (1,'Bucuresti','05:45:00', '11:27:00'), 
	  (1,'Bucuresti','08:52:00', '14:55:00'), 
	  (1,'Bucuresti','12:46:00', '18:53:00'), 
	  (1,'Bucuresti','16:16:00', '22:39:00'),
      (2,'Adjud','05:12:00','07:48:00'), 
	  (6,'Bucuresti','04:20:00','07:14:00'), 
	  (9,'Suceava','06:49:00','12:34:00'), 
	  (9,'Suceava','14:00:00','14:00:00'),
	  (9,'Suceava','17:23:00','23:05:00'), 
	  (8,'Focsani','13:37:00','16:33:00'), 
	  (9,'Constanta','07:30:00','09:52:00'), 
	  (9,'Constanta','08:20:00','10:40:00'),
	  (9,'Constanta','17:30:00','19:30:00'), 
	  (9,'Constanta','20:45:00','23:20:00'),
	  (13,'Bucuresti Nord','10:50:00','12:50:00')

Use ROExpress 
INSERT INTO Statii_Tren(ID_Statie,ID_Tren,Ora_Sosire,Ora_Plecare) 
VALUES 
    -- nord --> sud
      (1,552,'05:45:00','05:45:00'),
      (2,552,'06:55:00','06:56:00'),
	  (3,552,'07:34:00','07:35:00'),
	  (4,552,'08:05:00','08:06:00'), 
	  (6,552,'08:50:00','08:51:00'), 
	  (7,552,'09:40:00','09:42:00'), 
	  (8,552,'10:35:00','10:40:00'),
	  (9,552,'11:27:00','23:59:59'), 
	  (1,1752,'08:52:00','08:52:00'), 
	  (2,1752,'09:32:00','09:32:00'),
	  (3,1752,'10:01:00','10:03:00'), 
	  (4,1752,'10:52:00','10:55:00'), 
	  (5,1752,'11:02:00','11:03:00'), 
	  (6,1752,'12:01:00','12:02:00'),  
	  (7,1752,'13:02:00','13:05:00'), 
	  (8,1752,'14:01:00','14:02:00'),
	  (9,1752,'14:55:00','23:59:59'),
	  (1,1754,'12:46:00','12:46:00'), 
	  (2,1754,'13:02:00','13:02:00'),
	  (3,1754,'14:01:00','14:03:00'), 
	  (4,1754,'15:52:00','15:55:00'), 
	  (5,1754,'16:02:00','16:03:00'), 
	  (6,1754,'17:01:00','17:02:00'),  
	  (7,1754,'18:02:00','18:05:00'), 
	  (8,1754,'18:21:00','18:21:00'),
	  (9,1754,'18:53:00','23:59:59'), 
	  (1,1656,'16:16:00','16:16:00'), 
	  (2,1656,'17:02:00','17:02:00'),
	  (3,1656,'18:01:00','18:03:00'), 
	  (4,1656,'19:52:00','19:55:00'), 
	  (5,1656,'20:02:00','20:03:00'), 
	  (6,1656,'21:01:00','21:02:00'),  
	  (7,1656,'21:20:00','21:25:00'), 
	  (8,1656,'22:21:00','22:21:00'),
	  (9,1656,'22:39:00','23:59:59'),

	  (2,5412,'05:12:00','05:12:00'),
	  (3,5412,'06:12:00','06:13:00'),
	  (4,5412,'06:50:00','06:51:00'), 
	  (5,5412,'07:48:00','23:59:00'),

	  (6,5002,'04:20:00','04:20:00'),
	  (7,5002,'05:23:00','05:23:00'), 
	  (8,5002,'07:23:00','07:23:00'),
	  (9,5002,'07:14:00','23:59:59'),
	  
	  -- sud --> nord 
	  (9,1655,'06:49:00','06:49:00'), 
	  (8,1655,'07:34:00','07:35:00'),
	  (7,1655,'08:41:00','08:42:00'),
	  (6,1655,'09:40:00','09:40:00'),
	  (5,1655,'10:20:00','10:21:00'),
	  (4,1655,'10:49:00','10:49:00'),
	  (3,1655,'11:49:00','11:49:00'),
	  (2,1655,'12:00:00','12:00:00'), 
	  (1,1655,'12:34:00','23:59:59'), 

	  (9,1753,'14:00:00','14:00:00'), 
	  (8,1753,'14:40:00','14:40:00'),
	  (7,1753,'15:41:00','15:42:00'),
	  (6,1753,'16:45:00','16:45:00'),
	  (5,1753,'17:20:00','17:21:00'),
	  (4,1753,'18:50:00','18:49:00'),
	  (3,1753,'19:20:00','19:20:00'),
	  (2,1753,'19:30:00','19:30:00'), 
	  (1,1753,'19:58:00','23:59:59'),  


	  (9,551,'17:23:00','17:23:00'), 
	  (8,551,'17:45:00','17:46:00'),
	  (7,551,'18:35:00','18:35:00'),
	  (6,551,'18:55:00','18:55:00'),
	  (5,551,'19:30:00','19:31:00'),
	  (4,551,'20:10:00','20:12:00'),
	  (3,551,'21:20:00','21:20:00'),
	  (2,551,'22:30:00','22:30:00'), 
	  (1,551,'23:05:00','23:59:59'), 

	  (8,5053,'13:37:00','13:37:00'),
	  (7,5053,'14:48:00','14:51:00'),
	  (6,5053,'16:33:00','23:59:59'), 
	  -- Bucuresti -->Constanta 

	  (9,1581,'07:30:00','07:30:00'), 
	  (10,1581,'08:12:00','08:13:00'), 
	  (11,1581,'09:01:00','09:02:00'),
	  (12,1581,'09:31:00','09:32:00'),
	  (13,1581,'09:52:00','23:59:59'), 

	  (9,1583,'08:20:00','08:20:00'), 
	  (10,1583,'08:52:00','08:53:00'), 
	  (11,1583,'09:22:00','09:22:00'),
	  (12,1583,'10:35:00','10:35:00'),
	  (13,1583,'10:40:00','23:59:59'), 
	  
	  -- IC 
	  (9,581,'17:30:00','17:30:00'), 
	  (13,581,'19:30:00','23:59:59'), 

	  (9,11081,'20:45:00','20:45:00'), 
	  (10,11081,'21:23:00','21:24:00'), 
	  (11,11081,'22:22:00','22:22:00'),
	  (12,11081,'22:59:00','22:59:00'),
	  (13,11081,'13:20:00','23:59:59'), 


	  -- Constanta Bucuresti 
	  (13,582,'10:50:00','10:50:00'), 
	  (9,582,'12:50:00','23:59:59')



CREATE Procedure sp_NumarBiletePerTren 
@idTren int                                 -- pun si data aici si merge sa fac si pe cele consumate
begin 
   select tren.ID_Tren,tipuri.Clasa_Vagon,SUM (tipuri.Nr_Locuri)
   from Trenuri tren 
   inner join Vagoane_Tren legatura 
   on tren.ID_Tren=legatura.ID_Tren 
   inner join Tipuri_Vagoane tipuri 
   on tipuri.ID_Vagon=legatura.ID_Vagon 
  where tren.ID_Tren=1583
  group by tren.ID_Tren,tipuri.Clasa_Vagon 
  
end