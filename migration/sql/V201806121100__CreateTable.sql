

CREATE TABLE motorista.brands(
    ID SERIAL  PRIMARY KEY, 
    NAME VARCHAR(300)   NOT NULL,
    REGISTRATION_DATA DATE   NOT NULL
);

CREATE SEQUENCE S_vehicle START 2;

CREATE TABLE motorista.cars(
    ID SERIAL  PRIMARY KEY, 
    NAME VARCHAR(300)   NOT NULL,
    BRAND_ID integer REFERENCES  motorista.brands (ID),
    REGISTRATION_DATA DATE   NOT NULL
);

CREATE TABLE motorista.vehicle(
    ID SERIAL  PRIMARY KEY,
    CAR_ID integer REFERENCES  motorista.cars (ID),
    LICENSE_PLATE VARCHAR(300)   NOT NULL
);

CREATE TABLE motorista.driver(
    ID SERIAL PRIMARY KEY, 
    FIRST_NAME VARCHAR(300)   NOT NULL,
    LAST_NAME VARCHAR(300)   NOT NULL,
    LOGIN VARCHAR(100)   NOT NULL,
    EMAIL VARCHAR(300)   NOT NULL,  
    VEHICLE_ID integer REFERENCES  motorista.vehicle (ID),
    REGISTRATION_DATA DATE   NOT NULL,
    STREET VARCHAR(300),
    STREET_NUMBER VARCHAR(300),
    ADDRESS_LATITUDE double precision, 
    ADDRESS_LONGITUDE double precision
);




INSERT INTO motorista.brands(id, NAME, REGISTRATION_DATA) VALUES (1, 'volkswagen', current_timestamp);


INSERT INTO motorista.cars(id, NAME, BRAND_ID, REGISTRATION_DATA) VALUES (1, 'GOL', 1,current_timestamp );


INSERT INTO motorista.vehicle(id, CAR_ID, LICENSE_PLATE) VALUES (1, 1, 'ABX1234');

INSERT INTO motorista.driver(first_name, LAST_NAME, login, email, VEHICLE_ID, REGISTRATION_DATA,STREET,STREET_NUMBER,ADDRESS_LATITUDE,ADDRESS_LONGITUDE) VALUES ('Elisa', 'Pereira', '1234','teste@teste', 1,current_timestamp ,'rua teste',40,23.3333,24.3333 );