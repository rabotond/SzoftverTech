--ha véletlenül eltűnnének az adatok a táblákból, futtasd le ezt midnen táblánál 
--cseréld ki a tábla és mezőnevet tetszőlegesre. az egyszerűség kedvéért az id-ken kívül minden nullable a táblákban, így csak ki kell töltögetnetek, nem kell id-ket beírogatnotok, de az idegen kulcsokra figyelni kell, 
--ahol az idegen kulcs ügyfélid vagy fajta(adományozó az adomány táblában avgy az előző gazdi az állatnál.vagy kennel típus, ami idegen kulcs az állatnál.de eprsze nem midnen állatnak van előző gazdija, szóval ez tettszőlegesen kitölthető), ott kopizzatok egyet az ügyféltáblából, vagy a kennelből a típust az állat fajtához

DECLARE @ct     int
SET @ct = 0
WHILE @ct < 50 BEGIN
    SET @ct = @ct + 1
    INSERT INTO [dbo].[ALLAT] ([ALLATID] VALUES (NEWID())
END
go
