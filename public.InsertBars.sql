CREATE OR REPLACE PROCEDURE public."InsertBar"(timeutc timestamp without time zone, tickerid integer, open real, high real, low real, close real)
 LANGUAGE sql
AS $procedure$


INSERT INTO "bar"
("Timestamp", "TickerId", "Open", "High", "Low", "Close")

SELECT
timeutc, id, open, high, low, close
FROM public."bar"
JOIN public."tickersEnum"
ON "bar"."TickerId" = "tickersEnum"."id"

$procedure$
