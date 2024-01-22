CREATE PROCEDURE GetJourney(@Departures varchar(50), @Arrival varchar(50))
	
AS
	
WITH StatiePlecare as ( 
     SELECT Statii.Nume_Oras as 'Statie Plecare',Tren.Tip_Tren as 'TipTren', Statii_Tren.ID_Tren  as 'ID_Tren',Ora_Plecare as 'Ora Plecare' 
	 FROM Statii_Tren 
	 INNER JOIN Statii 
	 on Statii_Tren.ID_Statie=Statii.ID_Statie
	 INNER JOIN Trenuri Tren 
	 ON Tren.ID_Tren=Statii_Tren.ID_Tren
	 WHERE Statii.Nume_Oras=@Departures
) 
select STATIEPLECARE.TipTren, STATIEPLECARE.ID_Tren,STATIEPLECARE.[Statie Plecare],STATIEPLECARE.[Ora Plecare],STATIEDESTINATIE.Nume_Oras,STATII_TREN.Ora_Sosire 
from StatiePlecare STATIEPLECARE 
inner join Statii_Tren STATII_TREN 
ON STATIEPLECARE.ID_Tren=STATII_TREN.ID_Tren 
INNER JOIN Statii STATIEDESTINATIE ON STATIEDESTINATIE.ID_Statie=STATII_TREN.ID_Statie
WHERE STATIEDESTINATIE.Nume_Oras=@Arrival  and STATIEPLECARE.[Ora Plecare] < STATII_TREN.Ora_Sosire; 


if OBJECT_ID('GetCityHops', 'P') is not null Drop PROCEDURE GetCityHops  

CREATE PROC GetCityHops(@IDTren varchar(10))
as
 select tren.ID_Tren, statii.Nume_Oras,statii.Latitudine,statii.Longitudine
 from Trenuri tren  
 inner join Statii_Tren statii_tren 
 on tren.ID_Tren=statii_tren.ID_Tren 
 inner join Statii statii 
 on statii_tren.ID_Statie=statii.ID_Statie
 where CONCAT(tren.Tip_Tren,CONVERT(VARCHAR,tren.ID_Tren))=@IDTren 

 exec GetCityHops IC551;

 select * from Trenuri 


select ID_Tren, Statii_Tren.Ora_Plecare
from Statii_Tren inner join Statii 
on Statii_Tren.ID_Statie =Statii.ID_Statie 
where Statii.Nume_Oras= 'Pascani'  

select TOP(1) CONCAT(Trenuri.Tip_Tren,Trenuri.ID_Tren) as TrenID ,statii.Nume_Oras,statii_tren.Ora_Sosire
From Trenuri trenuri 
inner join Statii_Tren statii_tren 
on trenuri.ID_Tren =statii_tren.ID_Tren 
inner join Statii statii 
on statii.ID_Statie = statii_tren.ID_Statie
where trenuri.ID_Tren = 551
order by statii_tren.Ora_Sosire desc

select Top(1) CONCAT(Trenuri.Tip_Tren,Trenuri.ID_Tren) as TrenID ,statii.Nume_Oras,statii_tren.Ora_Sosire
From Trenuri trenuri 
inner join Statii_Tren statii_tren 
on trenuri.ID_Tren =statii_tren.ID_Tren 
inner join Statii statii 
on statii.ID_Statie = statii_tren.ID_Statie
where trenuri.ID_Tren = 551
order by statii_tren.Ora_Sosire asc

select * from Calatorii

select  Tipuri_Vagoane.Clasa_Vagon, SUM(Tipuri_Vagoane.Nr_Locuri)
from Trenuri inner join Vagoane_Tren
on Trenuri.ID_Tren = Vagoane_Tren.ID_Tren 
inner join Tipuri_Vagoane 
on Tipuri_Vagoane.ID_Vagon = Vagoane_Tren.ID_Vagon
Where  CONCAT(Trenuri.Tip_Tren,Trenuri.ID_Tren)= 'IR1752'
GROUP BY Tipuri_Vagoane.Clasa_Vagon

select * from Tipuri_Vagoane

select * from Vagoane_Tren


select Bilet_Tren.Clasa, COUNT(*)
from Bilet_Tren 
where Bilet_Tren.ID_Tren = 1752 and Bilet_Tren.DataTren = '12-10-2023'
Group By Bilet_Tren.Clasa

INSERT INTO Bilet_Tren(ID_Tren,Email,NumarVagon, LocVagon,Clasa,DataTren) 
VALUES  (1752,'andreiungureanu133@email.com',2,2,2,'12-10-2023')  

select ID_Bilet_Tren TOP(1)
from Bilet_Tren 
order by ID_Bilet_Tren desc
