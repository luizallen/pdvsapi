CREATE EXTENSION postgis;
CREATE EXTENSION postgis_topology;

create table pdv(
	id varchar(32),
	tradingName varchar(100),
	ownerName varchar(100),
	document varchar(14),
	coverageAreaType varchar(10),
	coverageAreaCoordinates geometry(POINT, 4326),
	AddressType varchar(10),
	AddressCoordinates geometry(POINT, 4326));
