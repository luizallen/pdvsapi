CREATE EXTENSION postgis;
CREATE EXTENSION postgis_topology;

create table Pdv(
	id text,
	tradingName text,
	ownerName text,
	document text,
	coverageArea geometry(MULTIPOLYGON, 4326),
	address geometry(POINT, 4326));
