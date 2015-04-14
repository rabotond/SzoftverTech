-- ADOMANY tábla adatai (ADOMANYID(id),TIPUS(char),ADOMANYOZO(id),MENNYISEG(int),DATUM(date))
INSERT INTO ADOMANY VALUES (1,'étel',1,30,'17-NOV-1981');
INSERT INTO ADOMANY VALUES (1,'pénz',2,12,'01-DEC-1934');
INSERT INTO ADOMANY VALUES (1,'étel',2,2,'20-JAN-2002');


-- ALLAT tábla adatai (ALLATID,FAJTA,NEV,SZULETESI_IDO,IVARTALANITOTT,SZIN,OLTVA,BETEGSEGEK,NOSTENY,TOMEG,MERET,CHIPES,ELOJEGYZETT,ELOZO_TULAJ,KEP)
--                     id,     char, char,  date      , bit          ,char,bit   ,char,     bit,   ,int ,  int,  bit,   bit,       id         ,char
INSERT INTO ALLAT VALUES (1,'wombat','szesszenõ','22-FEB-1999',0,'barna foltos',1,'eddig semmi',1,15,40,0,1,1,'@\állatok\wombat4.jpg');
INSERT INTO ALLAT VALUES (2,'vöröstengeri hal','szesszenõ','10-JAN-2010',0,'kék',0,'nincs',0,1,1,0,0,2,'@\állatok\vorostengeri_hal.bmp');
INSERT INTO ALLAT VALUES (3,'wombat','nyiszogó','01-MAR-2009',1,'barna',1,'nincs',1,7,15,1,1,3,'@\állatok\wombat1.jpg');


--  ELEDEL tábla adatai (FAJTA(char),RAKTARON(char))
INSERT INTO ELEDEL VALUES ('hal','10kg');
INSERT INTO ELEDEL VALUES ('wombat','3kg');
INSERT INTO ELEDEL VALUES ('kutya','50kg');


--  KENNEL tábla adatai (TIPUS(char),SZABAD(int),FOGLALT(int),MAXDARAB(int))
INSERT INTO KENNEL VALUES ('kutya',3,2,10);
INSERT INTO KENNEL VALUES ('cica',1,1,10);
INSERT INTO KENNEL VALUES ('egér',1,1,10);


--  UGYFEL tábla adatai (VEZETEKNEV,KERESZNEV,IRSZ(int),VAROS,UTCA,HAZSZAM(int),TELEFON,UGYFELID(id),EMAIL)
INSERT INTO UGYFEL VALUES ('Kovács','András',2000,'Budapest','Józsa',20,36302555555,1,'kovacsandras@gmail.com');
INSERT INTO UGYFEL VALUES ('Tóth','Péter',2210,'Kemence','Attila',3134, 36302534423,2,'tothpeter@gmail.com');
INSERT INTO UGYFEL VALUES ('Fekete','Pákó',3210,'Major','Tiszai',53,36204555445,3,'feketepako@gmail.com');


--  USER tábla adatai (name(char),password(char),C_isadmin(boolean))
INSERT INTO user VALUES ('FeketePákó','szarembervagyok',0);
INSERT INTO user VALUES ('TóthÁdám','legjobbjelszo',1);
INSERT INTO user VALUES ('Csacskaangyal','jelszo',0);















