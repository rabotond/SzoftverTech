-- ADOMANY t�bla adatai (ADOMANYID(id),TIPUS(char),ADOMANYOZO(id),MENNYISEG(int),DATUM(date))
INSERT INTO ADOMANY VALUES (1,'�tel',1,30,'17-NOV-1981');
INSERT INTO ADOMANY VALUES (1,'p�nz',2,12,'01-DEC-1934');
INSERT INTO ADOMANY VALUES (1,'�tel',2,2,'20-JAN-2002');


-- ALLAT t�bla adatai (ALLATID,FAJTA,NEV,SZULETESI_IDO,IVARTALANITOTT,SZIN,OLTVA,BETEGSEGEK,NOSTENY,TOMEG,MERET,CHIPES,ELOJEGYZETT,ELOZO_TULAJ,KEP)
--                     id,     char, char,  date      , bit          ,char,bit   ,char,     bit,   ,int ,  int,  bit,   bit,       id         ,char
INSERT INTO ALLAT VALUES (1,'wombat','szesszen�','22-FEB-1999',0,'barna foltos',1,'eddig semmi',1,15,40,0,1,1,'@\�llatok\wombat4.jpg');
INSERT INTO ALLAT VALUES (2,'v�r�stengeri hal','szesszen�','10-JAN-2010',0,'k�k',0,'nincs',0,1,1,0,0,2,'@\�llatok\vorostengeri_hal.bmp');
INSERT INTO ALLAT VALUES (3,'wombat','nyiszog�','01-MAR-2009',1,'barna',1,'nincs',1,7,15,1,1,3,'@\�llatok\wombat1.jpg');


--  ELEDEL t�bla adatai (FAJTA(char),RAKTARON(char))
INSERT INTO ELEDEL VALUES ('hal','10kg');
INSERT INTO ELEDEL VALUES ('wombat','3kg');
INSERT INTO ELEDEL VALUES ('kutya','50kg');


--  KENNEL t�bla adatai (TIPUS(char),SZABAD(int),FOGLALT(int),MAXDARAB(int))
INSERT INTO KENNEL VALUES ('kutya',3,2,10);
INSERT INTO KENNEL VALUES ('cica',1,1,10);
INSERT INTO KENNEL VALUES ('eg�r',1,1,10);


--  UGYFEL t�bla adatai (VEZETEKNEV,KERESZNEV,IRSZ(int),VAROS,UTCA,HAZSZAM(int),TELEFON,UGYFELID(id),EMAIL)
INSERT INTO UGYFEL VALUES ('Kov�cs','Andr�s',2000,'Budapest','J�zsa',20,36302555555,1,'kovacsandras@gmail.com');
INSERT INTO UGYFEL VALUES ('T�th','P�ter',2210,'Kemence','Attila',3134, 36302534423,2,'tothpeter@gmail.com');
INSERT INTO UGYFEL VALUES ('Fekete','P�k�',3210,'Major','Tiszai',53,36204555445,3,'feketepako@gmail.com');


--  USER t�bla adatai (name(char),password(char),C_isadmin(boolean))
INSERT INTO user VALUES ('FeketeP�k�','szarembervagyok',0);
INSERT INTO user VALUES ('T�th�d�m','legjobbjelszo',1);
INSERT INTO user VALUES ('Csacskaangyal','jelszo',0);















